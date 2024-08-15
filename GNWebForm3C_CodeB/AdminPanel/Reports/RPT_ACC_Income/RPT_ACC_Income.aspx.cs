using GNForm3C.BAL;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Reports_RPT_ACC_Income_RPT_ACC_Income : System.Web.UI.Page
{
    #region private variable 
    DataTable dtACC_Income = new DataTable();
    private ds_ACC_Income objAcc_income = new ds_ACC_Income();
    #endregion private variable 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ShowReport();
        }
    }
    #region ShowReport
    protected void ShowReport()
    {
        RPT_ACC_IncomeListBAL balACC_Income = new RPT_ACC_IncomeListBAL();
        DataTable dt = balACC_Income.Report_ACC_Income_ByFinYear();
        dtACC_Income = dt.Copy();
        FillDataSet();

    }
    #endregion ShowReport

    #region FillDataSet
    protected void FillDataSet()
    {
        foreach (DataRow dr in dtACC_Income.Rows)
        {
            ds_ACC_Income.dt_ACC_IncomeRow drACC_Income = objAcc_income.dt_ACC_Income.Newdt_ACC_IncomeRow();
         
            if (!dr["FinYearName"].Equals(System.DBNull.Value))
            {
                drACC_Income.FinYear = Convert.ToString(dr["FinYearName"]);
            }
            if (!dr["Hospital"].Equals(System.DBNull.Value))
            {
                drACC_Income.Hospital = Convert.ToString(dr["Hospital"]);
            }
            if (!dr["IncomeType"].Equals(System.DBNull.Value))
            {
                drACC_Income.IncomeType = Convert.ToString(dr["IncomeType"]);
            }
            if (!dr["Amount"].Equals(System.DBNull.Value))
            {
                drACC_Income.Amount = Convert.ToDecimal(dr["Amount"]);
            }
            if (!dr["IncomeDate"].Equals(System.DBNull.Value))
            {
                drACC_Income.IncomeDate = Convert.ToDateTime(dr["IncomeDate"]).ToString("dd-mm-yyyy");
            }

            objAcc_income.dt_ACC_Income.Rows.Add(drACC_Income);

        }
        SetReportParameters();
        this.rvIncomeReport.LocalReport.DataSources.Clear();
        this.rvIncomeReport.LocalReport.DataSources.Add(new ReportDataSource("dt_ACC_Income", (DataTable)objAcc_income.dt_ACC_Income));
        this.rvIncomeReport.LocalReport.Refresh();
    }
    #endregion FillDataSet
    #region SetReportParameters
    private void SetReportParameters()
    {
        String ReportTitle = "Income Report";

        ReportParameter rptReportTitle = new ReportParameter("ReportTitle", ReportTitle);
        this.rvIncomeReport.LocalReport.SetParameters(new ReportParameter[] { rptReportTitle });
    }
    #endregion SetReportParameters
}