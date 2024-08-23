using GNForm3C.BAL;
using GNForm3C;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data.SqlTypes;

public partial class AdminPanel_Reports_RPT_ACC_Income_RPT_IncomeExpenseLedger : System.Web.UI.Page
{
    #region private variable 
    DataTable dtACC_Ledger = new DataTable();
    private ds_ACC_Ledger objAcc_Ledger = new ds_ACC_Ledger();

    #endregion private variable 

    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 12.1 DropDown List Fill Section

            FillDropDownList();

            #endregion 12.1 DropDown List Fill Section

            #region Fill Lables
            SetDefaultDateTime();
            #endregion
        }
    }

    #endregion Page Load Event

    #region 14.0 DropDownList

    #region 14.1 Fill DropDownList

    private void FillDropDownList()
    {
        CommonFillMethods.FillDropDownListFinYearID(ddlFinYearID);
        CommonFillMethods.FillDropDownListHospitalID(ddlHospitalID);
    }

    #endregion 14.1 Fill DropDownList   

    #endregion 14.0 DropDownList

    #region 23.0 SetDefaultDateTime
    private void SetDefaultDateTime()
    {
        //    ddlFinYearID.SelectedIndex = 1;
    }
    #endregion SetDefaultDateTime

    #region ShowReport
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowReport();
    }
    protected void ShowReport()
    {
        #region Parameters
        SqlInt32 FinYearID = SqlInt32.Null;
        SqlInt32 HospitalID = SqlInt32.Null;

        #endregion Parameters

        #region Gather Data

        if (ddlFinYearID.Text.Trim() != String.Empty)
            FinYearID = Convert.ToInt32(ddlFinYearID.SelectedValue);

        if (ddlHospitalID.Text.Trim() != String.Empty)
            HospitalID = Convert.ToInt32(ddlHospitalID.SelectedValue);

        #endregion Gather Data

        Page.Validate();
        if (Page.IsValid)
        {
            try
            {
                ACC_LedgerBAL balACC_IncomeExpense = new ACC_LedgerBAL();
                dtACC_Ledger = balACC_IncomeExpense.RPT_IncomeExpenseLedger(FinYearID, HospitalID);
                FillDataSet();
            }
            catch (Exception ex)
            {
                ucMessage.ShowError(ex.Message);
            }
        }
    }
    #endregion ShowRepor

    #region FillDataSet
    protected void FillDataSet()
    {
        foreach (DataRow dr in dtACC_Ledger.Rows)
        {
            ds_ACC_Ledger.dsACC_LedgerRow drACC_Income = objAcc_Ledger.dsACC_Ledger.NewdsACC_LedgerRow();

            if (!dr["FinYearName"].Equals(System.DBNull.Value))
            {
                drACC_Income.FinYearName = Convert.ToString(dr["FinYearName"]);
            }
            if (!dr["Hospital"].Equals(System.DBNull.Value))
            {
                drACC_Income.Hospital = Convert.ToString(dr["Hospital"]);
            }
            if (!dr["TransactionDate"].Equals(System.DBNull.Value))
            {
                drACC_Income.TransactionDate = Convert.ToDateTime(dr["TransactionDate"]);
            }
            if (!dr["TransactionType"].Equals(System.DBNull.Value))
            {
                drACC_Income.TransactionType = Convert.ToString(dr["TransactionType"]);
            }
            if (!dr["IncomeAmount"].Equals(System.DBNull.Value))
            {
                drACC_Income.IncomeAmount = Convert.ToDecimal(dr["IncomeAmount"]);
            }
            if (!dr["ExpenseAmount"].Equals(System.DBNull.Value))
            {
                drACC_Income.ExpenseAmount = Convert.ToDecimal(dr["ExpenseAmount"]);
            }
            if (!dr["Balance"].Equals(System.DBNull.Value))
            {
                drACC_Income.Balance = Convert.ToDecimal(dr["Balance"]);
            }
            if (!dr["IncomeType"].Equals(System.DBNull.Value))
            {
                drACC_Income.IncomeType = Convert.ToString(dr["IncomeType"]);
            }
            if (!dr["ExpenseType"].Equals(System.DBNull.Value))
            {
                drACC_Income.ExpenseType = Convert.ToString(dr["ExpenseType"]);
            }

            objAcc_Ledger.dsACC_Ledger.Rows.Add(drACC_Income);
        }
        SetReportParameters();
        this.rvLedgerReport.LocalReport.DataSources.Clear();
        this.rvLedgerReport.LocalReport.DataSources.Add(new ReportDataSource("dsACC_Ledger", (DataTable)objAcc_Ledger.dsACC_Ledger));
        this.rvLedgerReport.LocalReport.Refresh();
    }
    #endregion FillDataSet
    #region SetReportParameters
    private void SetReportParameters()
    {
        String ReportTitle = ddlHospitalID.SelectedValue;
        String ReportSubTitle = ddlFinYearID.SelectedValue;

        Microsoft.Reporting.WebForms.ReportParameter rptReportTitle = new ReportParameter("ReportTitle", ReportTitle);
        Microsoft.Reporting.WebForms.ReportParameter rptReportSubTitle = new ReportParameter("SubTitle", ReportSubTitle);
        this.rvLedgerReport.LocalReport.SetParameters(new ReportParameter[] { rptReportTitle, rptReportSubTitle });
    }
    #endregion SetReportParameters

    #region 20.0 Cancel Button Event

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    #endregion 20.0 Cancel Button Event

    #region 22.0 ClearControls
    private void ClearControls()
    {
        ddlFinYearID.SelectedIndex = 0;
        ddlHospitalID.SelectedIndex = 0;
    }

    #endregion 22.0 ClearControls
}