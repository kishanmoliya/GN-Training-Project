using GNForm3C.BAL;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Reports_RPT_ACC_Ledger_RPT_ACC_HospitalWise_FinYearWise_IncomeExpenseList : System.Web.UI.Page
{
    #region private variable 
    DataTable dtACC_IncExp = new DataTable();
    private ds_ACC_IncomeExpense objAcc_incomeExpense = new ds_ACC_IncomeExpense();

    #endregion private variable 

    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ShowReport();
        }
    }

    #endregion Page Load Event

    #region ShowReport
    protected void ShowReport()
    {
        ACC_LedgerBAL balACC_IncomeExpense = new ACC_LedgerBAL();
        DataTable dt = balACC_IncomeExpense.RPT_HospitalWiseFinyearWiseIncomeExpense();
        dtACC_IncExp = dt.Copy();
        FillDataSet();

    }
    #endregion ShowReport

    #region FillDataSet
    protected void FillDataSet()
    {
        foreach (DataRow dr in dtACC_IncExp.Rows)
        {
            ds_ACC_IncomeExpense.dsACC_IncomeExpenseRow drACC_Income = objAcc_incomeExpense.dsACC_IncomeExpense.NewdsACC_IncomeExpenseRow();

            if (!dr["Hospital"].Equals(System.DBNull.Value))
            {
                drACC_Income.Hospital = Convert.ToString(dr["Hospital"]);
            }
            if (!dr["TotalIncome"].Equals(System.DBNull.Value))
            {
                drACC_Income.TotalIncome = Convert.ToDecimal(dr["TotalIncome"]);
            }
            if (!dr["TotalExpense"].Equals(System.DBNull.Value))
            {
                drACC_Income.TotalExpense = Convert.ToDecimal(dr["TotalExpense"]);
            }
            if (!dr["TotalPatients"].Equals(System.DBNull.Value))
            {
                drACC_Income.TotalPatients = Convert.ToInt32(dr["TotalPatients"]);
            }
            if (!dr["FinYearName"].Equals(System.DBNull.Value))
            {
                drACC_Income.FinYearName = Convert.ToString(dr["FinYearName"]);
            }

            objAcc_incomeExpense.dsACC_IncomeExpense.Rows.Add(drACC_Income);
        }
        SetReportParameters();
        this.rvIncomeExpenseReport.LocalReport.DataSources.Clear();
        this.rvIncomeExpenseReport.LocalReport.DataSources.Add(new ReportDataSource("dsACC_IncomeExpense", (DataTable)objAcc_incomeExpense.dsACC_IncomeExpense));
        this.rvIncomeExpenseReport.LocalReport.Refresh();
    }
    #endregion FillDataSet
    #region SetReportParameters
    private void SetReportParameters()
    {
        String ReportTitle = "Financial year wise Hospital wise Income Expemse";

        ReportParameter rptReportTitle = new ReportParameter("ReportTitle", ReportTitle);
        this.rvIncomeExpenseReport.LocalReport.SetParameters(new ReportParameter[] { rptReportTitle });
    }
    #endregion SetReportParameters
}