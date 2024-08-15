using GNForm3C;
using GNForm3C.BAL;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Reports_RPT_ACC_Expense_RPT_ACC_Expense : System.Web.UI.Page
{
    #region private variable 
    DataTable dtACC_Expense = new DataTable();
    private ds_ACC_Expense objAcc_Expense = new ds_ACC_Expense();
    #endregion private variable 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ShowReport();

            #region Fill Lables
            SetDefaultDateTime();
            #endregion
        }
    }


    #region 23.0 SetDefaultDateTime
    private void SetDefaultDateTime()
    {
        DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        dtpFromDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
        dtpToDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
    }
    #endregion SetDefaultDateTime

    #region ShowReport
    protected void ShowReport()
    {
        #region Parameters
        SqlDateTime FromDate = SqlDateTime.Null;
        SqlDateTime ToDate = SqlDateTime.Null;

        #endregion Parameters

        #region Gather Data

        #region NavigateLogic

        if (dtpFromDate.Text.Trim() != String.Empty)
            FromDate = Convert.ToDateTime(dtpFromDate.Text.Trim());

        if (dtpToDate.Text.Trim() != String.Empty)
            ToDate = SqlDateTime.Parse(dtpToDate.Text.Trim());
        #endregion NavigateLogic


        #endregion Gather Data

        ACC_ExpenseBAL balACC_Expense = new ACC_ExpenseBAL();
        DataTable dt = balACC_Expense.HospitalWiseExpenseList(FromDate, ToDate);
        dtACC_Expense = dt.Copy();
        FillDataSet();
    }
    #endregion ShowReport

    #region FillDataSet
    protected void FillDataSet()
    {
        foreach (DataRow dr in dtACC_Expense.Rows)
        {
            ds_ACC_Expense.dt_ACC_ExpenseRow drACC_Expense = objAcc_Expense.dt_ACC_Expense.Newdt_ACC_ExpenseRow();

            if (!dr["ExpenseDate"].Equals(System.DBNull.Value))
            {
                drACC_Expense.ExpenseDate = Convert.ToDateTime(dr["ExpenseDate"]).ToString("dd-mm-yyyy");
            }
            if (!dr["ExpenseType"].Equals(System.DBNull.Value))
            {
                drACC_Expense.ExpenseType = Convert.ToString(dr["ExpenseType"]);
            }
            if (!dr["Amount"].Equals(System.DBNull.Value))
            {
                drACC_Expense.Amount = Convert.ToDecimal(dr["Amount"]);
            }
            if (!dr["TagName"].Equals(System.DBNull.Value))
            {
                drACC_Expense.TagName = Convert.ToString(dr["TagName"]);
            }
            if (!dr["Remarks"].Equals(System.DBNull.Value))
            {
                drACC_Expense.Remarks = Convert.ToString(dr["Remarks"]);
            }
            if (!dr["Hospital"].Equals(System.DBNull.Value))
            {
                drACC_Expense.Hospital = Convert.ToString(dr["Hospital"]);
            }

            objAcc_Expense.dt_ACC_Expense.Rows.Add(drACC_Expense);
        }
        SetReportParameters();
        this.rvExpenseReport.LocalReport.DataSources.Clear();
        this.rvExpenseReport.LocalReport.DataSources.Add(new ReportDataSource("dt_ACC_Expense", (DataTable)objAcc_Expense.dt_ACC_Expense));
        this.rvExpenseReport.LocalReport.Refresh();
    }
    #endregion FillDataSet

    #region SetReportParameters
    private void SetReportParameters()
    {
        String ReportTitle = "Hospital Wise Expense";

        ReportParameter rptReportTitle = new ReportParameter("ReportTitle", ReportTitle);
        this.rvExpenseReport.LocalReport.SetParameters(new ReportParameter[] { rptReportTitle });
    }
    #endregion SetReportParameters

    #region 15.1 Button Search Click Event

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    #endregion 15.1 Button Search Click Event


    protected void Search()
    {
        #region Parameters
        SqlDateTime FromDate = SqlDateTime.Null;
        SqlDateTime ToDate = SqlDateTime.Null;

        #endregion Parameters

        #region Gather Data

        #region NavigateLogic

        if (dtpFromDate.Text.Trim() != String.Empty)
            FromDate = Convert.ToDateTime(dtpFromDate.Text.Trim());

        if (dtpToDate.Text.Trim() != String.Empty)
            ToDate = SqlDateTime.Parse(dtpToDate.Text.Trim());
        #endregion NavigateLogic


        #endregion Gather Data

        ACC_ExpenseBAL balACC_Expense = new ACC_ExpenseBAL();

        DataTable dt = balACC_Expense.HospitalWiseExpenseList(FromDate, ToDate);

        if (dt != null && dt.Rows.Count > 0)
        {
            Div_SearchResult.Visible = true;
            //Div_ExportOption.Visible = true;
            rpData.DataSource = dt;
            rpData.DataBind();

            ShowReport();

            lblRecordInfoBottom.Text = String.Empty;
            lblRecordInfoTop.Text = String.Empty;
        }
        else
        {
            rpData.DataSource = null;
            rpData.DataBind();
            lblRecordInfoBottom.Text = CommonMessage.NoRecordFound();
            lblRecordInfoTop.Text = CommonMessage.NoRecordFound();

            ucMessage.ShowError(CommonMessage.NoRecordFound());
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    #region 22.0 ClearControls
    private void ClearControls()
    {
        dtpFromDate.Text = String.Empty;
        dtpToDate.Text = String.Empty;
    }

    #endregion 22.0 ClearControls
}