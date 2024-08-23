using GNForm3C;
using GNForm3C.BAL;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Reports_RPT_Patient_ICard_RPT_MST_Patient_ICard : System.Web.UI.Page
{
    #region private variable 
    DataTable dtMST_PatientICard = new DataTable();
    private ds_MST_PatientICard objMst_PatientICard = new ds_MST_PatientICard();

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
        MST_PatientBAL balMST_PatientICard = new MST_PatientBAL();
        DataTable dt = balMST_PatientICard.RPT_Patient_ICard();
        dtMST_PatientICard = dt.Copy();
        FillDataSet();

    }
    #endregion ShowReport

    #region FillDataSet
    protected void FillDataSet()
    {
        foreach (DataRow dr in dtMST_PatientICard.Rows)
        {
            ds_MST_PatientICard.ds_PatientICardRow drMST_Patient = objMst_PatientICard.ds_PatientICard.Newds_PatientICardRow();

            if (!dr["PatientID"].Equals(System.DBNull.Value))
            {
                drMST_Patient.PatientID = Convert.ToInt32(dr["PatientID"]);
            }
            if (!dr["PatientID"].Equals(System.DBNull.Value))
            {
                drMST_Patient.Barcode = CommonFunctions.GenerateBarcode(drMST_Patient.PatientID.ToString());
            }
            if (!dr["PatientName"].Equals(System.DBNull.Value))
            {
                drMST_Patient.PatientName = Convert.ToString(dr["PatientName"]);
            }
            if (!dr["DOB"].Equals(System.DBNull.Value))
            {
                drMST_Patient.DOB = Convert.ToDateTime(dr["DOB"]);
            }
            if (!dr["Age"].Equals(System.DBNull.Value))
            {
                drMST_Patient.Age = Convert.ToInt32(dr["Age"]);
            }
            if (!dr["MobileNo"].Equals(System.DBNull.Value))
            {
                drMST_Patient.MobileNo = Convert.ToString(dr["MobileNo"]);
            }
            if (!dr["PrimaryDesc"].Equals(System.DBNull.Value))
            {
                drMST_Patient.PrimaryDesc = Convert.ToString(dr["PrimaryDesc"]);
            }
            if (!dr["HospitalID"].Equals(System.DBNull.Value))
            {
                drMST_Patient.HospitalID = Convert.ToInt32(dr["HospitalID"]);
            }
            if (!dr["FinYearID"].Equals(System.DBNull.Value))
            {
                drMST_Patient.FinYearID = Convert.ToInt32(dr["FinYearID"]);
            }
            if (!dr["Hospital"].Equals(System.DBNull.Value))
            {
                drMST_Patient.Hospital = Convert.ToString(dr["Hospital"]);
            }
            if (!dr["FinYearName"].Equals(System.DBNull.Value))
            {
                drMST_Patient.FinYearName = Convert.ToString(dr["FinYearName"]);
            }
            if (!dr["PatientPhotoPath"].Equals(System.DBNull.Value))
                drMST_Patient.PatientPhoto = CommonFunctions.ConvertImagePathToPngBytes(Convert.ToString(dr["PatientPhotoPath"]));
            else
                drMST_Patient.PatientPhoto = CommonFunctions.ConvertImagePathToPngBytes(CV.DefaultNoImagePath);

            objMst_PatientICard.ds_PatientICard.Rows.Add(drMST_Patient);
        }
        SetReportParameters();
        this.rvPatientICard.LocalReport.DataSources.Clear();
        this.rvPatientICard.LocalReport.DataSources.Add(new ReportDataSource("ds_PatientICard", (DataTable)objMst_PatientICard.ds_PatientICard));
        this.rvPatientICard.LocalReport.Refresh();
    }
    #endregion FillDataSet
    #region SetReportParameters
    private void SetReportParameters()
    {
        String ReportTitle = "Darshan Institute of Engineering & Technology";

        ReportParameter rptReportTitle = new ReportParameter("ReportTitle", ReportTitle);
        this.rvPatientICard.LocalReport.SetParameters(new ReportParameter[] { rptReportTitle });
    }
    #endregion SetReportParameters
}