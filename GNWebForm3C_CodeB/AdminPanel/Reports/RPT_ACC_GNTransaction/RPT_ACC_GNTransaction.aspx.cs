using GNForm3C.BAL;
using GNForm3C;
using System;
using System.Data.SqlTypes;
using System.Web.UI;
using System.Data;
using Microsoft.Reporting.WebForms;

public partial class AdminPanel_Reports_RPT_ACC_GNTransaction_RPT_ACC_GNTransaction : System.Web.UI.Page
{
    #region 11.0 Local Variable 
    private ds_ACC_GNTransaction objdsACC_GNTransaction = new ds_ACC_GNTransaction();
    #endregion 11.0 Local Variable

    #region 12.0 Page Load Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["TransactionID"] != null && Request.QueryString["ReportType"] != null)
            {
                ShowReport();
            }
        }
    }

    #endregion 12.0 Page Load Event

    #region 18.0 Export Data

    #region 18.1 Excel Export Button Click Event

    private void ExportReport(string format)
    {
        try
        {
            string mimeType, encoding, extension;
            Warning[] warnings;
            string[] streamIds;

            byte[] bytes = rvPatientReceipt.LocalReport.Render(format,
                                                        null,
                                                        out mimeType,
                                                        out encoding,
                                                        out extension,
                                                        out streamIds,
                                                        out warnings);

            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("Content-Disposition", "attachment; filename=report." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
        }

    }

    #endregion 18.1 Excel Export Button Click Event

    #endregion 18.0 Export Data

    #region 22.0 REPORT

    #region  22.1  ShowReport

    protected void ShowReport()
    {
        if (Request.QueryString["TransactionID"] != null && Request.QueryString["ReportType"] != null)
        {
            SqlInt32 TransactionID = CommonFunctions.DecryptBase64Int32(Request.QueryString["TransactionID"]);
            SqlString ReportType = CommonFunctions.DecryptBase64(Request.QueryString["ReportType"]);

            ACC_GNTransactionBAL balACC_GNTransaction = new ACC_GNTransactionBAL();

            DataTable dtPatientReceipt = balACC_GNTransaction.PatientReceiptByGNTransactionID(TransactionID);

            if (dtPatientReceipt != null)
            {
                FillDataSet(dtPatientReceipt);
            }

            ExportReport(ReportType.ToString());

        }
    }

    #endregion 22.1 ShowReport 

    #region 22.2 FillDataSet

    protected void FillDataSet(DataTable dtPatientReceipt)
    {
        foreach (DataRow dr in dtPatientReceipt.Rows)
        {
            ds_ACC_GNTransaction.dsACC_GNTransactionRow drPatientReceipt = objdsACC_GNTransaction.dsACC_GNTransaction.NewdsACC_GNTransactionRow();

            if (!dr["PatientName"].Equals(System.DBNull.Value))
                drPatientReceipt.PatientName = Convert.ToString(dr["PatientName"]);

            if (!dr["Age"].Equals(System.DBNull.Value))
                drPatientReceipt.Age = Convert.ToInt32(dr["Age"]);

            if (!dr["MobileNo"].Equals(System.DBNull.Value))
                drPatientReceipt.MobileNo = Convert.ToString(dr["MobileNo"]);

            if (!dr["DateOfAdmission"].Equals(System.DBNull.Value))
                drPatientReceipt.DateOfAdmission = Convert.ToDateTime(dr["DateOfAdmission"]);

            if (!dr["DateOfDischarge"].Equals(System.DBNull.Value))
                drPatientReceipt.DateOfDischarge = Convert.ToDateTime(dr["DateOfDischarge"]);

            if (!dr["Hospital"].Equals(System.DBNull.Value))
                drPatientReceipt.Hospital = Convert.ToString(dr["Hospital"]);

            if (!dr["ReferenceDoctor"].Equals(System.DBNull.Value))
                drPatientReceipt.ReferenceDoctor = Convert.ToString(dr["ReferenceDoctor"]);

            if (!dr["ReceiptNo"].Equals(System.DBNull.Value))
                drPatientReceipt.ReceiptNo = Convert.ToString(dr["ReceiptNo"]);

            if (!dr["ReceiptTypeName"].Equals(System.DBNull.Value))
                drPatientReceipt.ReceiptTypeName = Convert.ToString(dr["ReceiptTypeName"]);

            if (!dr["Treatment"].Equals(System.DBNull.Value))
                drPatientReceipt.Treatment = Convert.ToString(dr["Treatment"]);

            if (!dr["Rate"].Equals(System.DBNull.Value))
                drPatientReceipt.Rate = Convert.ToDecimal(dr["Rate"]);

            if (!dr["Quantity"].Equals(System.DBNull.Value))
                drPatientReceipt.Quantity = Convert.ToInt32(dr["Quantity"]);

            if (!dr["Amount"].Equals(System.DBNull.Value))
                drPatientReceipt.Amount = Convert.ToDecimal(dr["Amount"]);

            if (!dr["TreatmentDate"].Equals(System.DBNull.Value))
                drPatientReceipt.TreatmentDate = Convert.ToDateTime(dr["TreatmentDate"]);


            objdsACC_GNTransaction.dsACC_GNTransaction.Rows.Add(drPatientReceipt);

        }

        SetReportParamater();
        this.rvPatientReceipt.LocalReport.DataSources.Clear();
        this.rvPatientReceipt.LocalReport.DataSources.Add(new ReportDataSource("dsACC_GNTransaction", (DataTable)objdsACC_GNTransaction.dsACC_GNTransaction));
        this.rvPatientReceipt.LocalReport.Refresh();
    }
    #endregion 22.2 FillDataSet

    #region 22.3 SetReportParamater
    protected void SetReportParamater()
    {
        String ReportTitle = "SHRI G.T SHETH MEDICAL FOUNDATION";
        String ReportSubTitle = "PHYSIOTHERAPY CENTER";
        DateTime PrintDate = DateTime.Now;
        ReportParameter rptReportTitle = new ReportParameter("ReportTitle", ReportTitle);
        ReportParameter rptReportSubTitle = new ReportParameter("ReportSubTitle", ReportSubTitle);
        ReportParameter rptPrintDate = new ReportParameter("PrintDate", PrintDate.ToString());

        this.rvPatientReceipt.LocalReport.SetParameters(new ReportParameter[] { rptReportTitle, rptReportSubTitle, rptPrintDate });

    }
    #endregion 22.3 SetReportParamater 

    #endregion 22.0 REPORT
}