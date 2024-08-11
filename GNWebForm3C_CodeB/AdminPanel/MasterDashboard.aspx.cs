using GNForm3C;
using GNForm3C.BAL;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using iTextSharp.text;

public partial class AdminPanel_MasterDashboard : System.Web.UI.Page
{
    #region 10.0 Local Variables

    String FormName = "Master Dashboard";

    #endregion 10.0 Variables

    #region 11.0 Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 11.1 Check User Login

        if (Session["UserID"] == null)
            Response.Redirect(CV.LoginPageURL);

        #endregion 11.1 Check User Login

        if (!Page.IsPostBack)
        {
            #region 11.2 DropDown List Fill Section

            FillDropDownList();

            #endregion 11.2 DropDown List Fill Section

            #region 11.3 Set Control Default Value

            lblFormHeader.Text = "Search";
            //upr.DisplayAfter = CV.UpdateProgressDisplayAfter;
            #endregion 11.3 Set Control Default Value

            #region 11.4 Set Help Text
            ucHelp.ShowHelp("Help Text will be shown here");
            #endregion 11.4 Set Help Text
        }
    }
    #endregion Pageload

    #region 13.0 Fill DropDownList
    private void FillDropDownList()
    {
        CommonFillMethods.FillDropDownListHospitalID(ddlHospitalID);
    }

    #endregion 13.0 Fill DropDownList

    #region 14.0 Show Button Event
    protected void btnShow_Click(object sender, EventArgs e)
    {
        #region Parameters
        Int32 HospitalID = 0;
        #endregion Parameters

        #region Gather Data

        #region NavigateLogic
        if (Request.QueryString["HospitalID"] != null)
        {
            if (!Page.IsPostBack)
            {
                HospitalID = (int)CommonFunctions.DecryptBase64Int32(Request.QueryString["HospitalID"]);
            }
            else
            {
                if (ddlHospitalID.SelectedIndex > 0)
                    HospitalID = Convert.ToInt32(ddlHospitalID.SelectedValue);
            }
        }
        else
        {
            if (ddlHospitalID.SelectedIndex > 0)
                HospitalID = Convert.ToInt32(ddlHospitalID.SelectedValue);
        }
        #endregion NavigateLogic
        #endregion Gather Data

        #region DSB Counts
        MST_MasterDashboardBAL balMST_Count = new MST_MasterDashboardBAL();
        DataTable dtCount = balMST_Count.SelectMSTCount(HospitalID);

        decimal totalIncome = Convert.ToDecimal(dtCount.Rows[0]["TotalIncome"]);
        lblIncomeCount.Text = String.Format(GNForm3C.CV.DefaultCurrencyFormatWithDecimalPoint, totalIncome);

        decimal TotalExpense = Convert.ToDecimal(dtCount.Rows[0]["TotalExpense"]);
        lblExpenseCount.Text = String.Format(GNForm3C.CV.DefaultCurrencyFormatWithDecimalPoint, TotalExpense);

        decimal TotalSubTreatment = Convert.ToDecimal(dtCount.Rows[0]["TotalSubTreatment"]);
        lblSubTreatmentCount.Text = String.Format(GNForm3C.CV.DefaultCurrencyFormatWithDecimalPoint, TotalSubTreatment);
        #endregion DSB Counts

        #region 14.1 Income Detailst;
        IncomeDetails(HospitalID, totalIncome);
        #endregion

        #region 14.2 expense details
        ExpenceDetails(HospitalID, TotalExpense);
        #endregion

        #region 14.3 TreatmentType Summary
        TreatmentTypeSummary(HospitalID);
        #endregion

        upCount.Update();
    }

    #endregion 14.0 Show Button Event

    #region 15.0 IncomeDetails
    private void IncomeDetails(Int32 HospitalID, decimal totalIncome)
    {
        MST_MasterDashboardBAL balMST_Income = new MST_MasterDashboardBAL();
        DataTable dtIncome = balMST_Income.SelectMST_DSB_Income(HospitalID);

        if (dtIncome.Rows.Count > 0 && totalIncome > 0)
        {
            grdIncome.DataSource = dtIncome;
            grdIncome.DataBind();
            grdIncome.Visible = true;      // Ensure the GridView is visible
            lblNoIncome.Visible = false;  // Hide the "No Record Found" message
        }
        else
        {
            grdIncome.Visible = false;     // Hide the GridView
            lblNoIncome.Visible = true;   // Show the "No Record Found" message
        }
    }
    #endregion

    #region 16.0 ExpenceDetails
    private void ExpenceDetails(Int32 HospitalID, decimal TotalExpense)
    {
        MST_MasterDashboardBAL balMST_Expense = new MST_MasterDashboardBAL();
        DataTable dtExpense = balMST_Expense.SelectMST_DSB_Expense(HospitalID);

        if (dtExpense.Rows.Count > 0 && TotalExpense > 0)
        {
            grdExpense.DataSource = dtExpense;
            grdExpense.DataBind();
            grdExpense.Visible = true;      // Ensure the GridView is visible
            lblNoExpense.Visible = false;  // Hide the "No Record Found" message
        }
        else
        {
            grdExpense.Visible = false;     // Hide the GridView
            lblNoExpense.Visible = true;   // Show the "No Record Found" message
        }
    }
    #endregion

    #region 17.0 TreatmentType Summary
    private void TreatmentTypeSummary(Int32 HospitalID)
    {
        MST_MasterDashboardBAL balMST_Treatment = new MST_MasterDashboardBAL();
        DataTable dt = balMST_Treatment.SelectMST_DSB_TreatmentSummary(HospitalID);

        if (dt != null && dt.Rows.Count > 0)
        {
            rpData.DataSource = dt;
            rpData.DataBind();
            pnlRepeater.Visible = true;     // Ensure the Repeater panel is visible
            lblNoRecords.Visible = false;   // Hide the "No Records Found" message
        }
        else
        {
            rpData.DataSource = null;
            rpData.DataBind();
            pnlRepeater.Visible = false;    // Hide the Repeater panel
            lblNoRecords.Visible = true;    // Show the "No Records Found" message
        }
    }
    #endregion

    #region 18.0 IncExp Formating
    protected void grdIncExp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            // Right-align the headers for the month columns
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                TableCell cell = e.Row.Cells[i];
                cell.CssClass = "right-align";
            }
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Check if this is the total row
            bool isTotalRow = e.Row.Cells[0].Text == "Total";

            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                TableCell cell = e.Row.Cells[i];
                string cellText = cell.Text.Replace("₹", "").Replace(",", "").Trim();
                decimal value;

                if (!isTotalRow && decimal.TryParse(cellText, out value) && value != 0)
                {
                    cell.BackColor = System.Drawing.Color.LightGray;  // Highlight non-zero cells
                }

                if (isTotalRow)
                {
                    cell.Font.Bold = true;  // Make total row values bold
                }

                cell.HorizontalAlign = HorizontalAlign.Right;
            }

            if (isTotalRow)
            {
                e.Row.Cells[0].Font.Bold = true;
            }
        }
    }
    #endregion
}