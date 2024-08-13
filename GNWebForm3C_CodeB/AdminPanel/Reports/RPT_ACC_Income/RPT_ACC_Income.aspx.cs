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
    #region Private Variables
    private DataTable dt = new DataTable();
    private ds_ACC_Income objdsACC_Income = new ds_ACC_Income();

    #endregion Private Variables

    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowReport();
        }
    }
    #endregion Page Load Event

    #region Show Report
    protected void ShowReport()
    {
        try
        {
            RPT_ACC_IncomeListBAL rptIncome = new RPT_ACC_IncomeListBAL();
            DataTable dt = rptIncome.Report_ACC_Income_ByFinYear();
            

            FillDataSet();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    #endregion Show Report

    #region FillDataSet
    protected void FillDataSet()
    {
        foreach (DataRow dr in dt.Rows)
        {
            ds_ACC_Income.dt_ACC_IncomeRow drACC_Income = objdsACC_Income.dt_ACC_Income.Newdt_ACC_IncomeRow();

            if (!dr["Hospital"].Equals(System.DBNull.Value))
                drACC_Income.Hospital = Convert.ToString(dr["Hospital"]);

            if (!dr["FinYear"].Equals(System.DBNull.Value))
                drACC_Income.FinYear = Convert.ToString(dr["FinYear"]);

            if (!dr["IncomeType"].Equals(System.DBNull.Value))
                drACC_Income.IncomeType = Convert.ToString(dr["IncomeType"]);

            if (!dr["Amount"].Equals(System.DBNull.Value))
                drACC_Income.Amount = Convert.ToDecimal(dr["Amount"]);

            if (!dr["IncomeDate"].Equals(System.DBNull.Value))
                drACC_Income.IncomeDate = Convert.ToDateTime(dr["IncomeDate"]);

            objdsACC_Income.dt_ACC_Income.Rows.Add(drACC_Income);
        }

        SetReportParamater();
        this.rvIncome.LocalReport.DataSources.Clear();
        this.rvIncome.LocalReport.DataSources.Add(new ReportDataSource("dtACC_Income", (DataTable)objdsACC_Income.dt_ACC_Income));
        this.rvIncome.LocalReport.Refresh();
    }
    #endregion

    #region Set Report Parameters
    protected void SetReportParamater()
    {
        String ReportTitle = "Income List";
        ReportParameter rptReportTitle = new ReportParameter("ReportTitle", ReportTitle);
        this.rvIncome.LocalReport.SetParameters(new ReportParameter[] { rptReportTitle });

    }
    #endregion Set Report Parameters
}