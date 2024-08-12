﻿using GNForm3C.BAL;
using GNForm3C.ENT;
using GNForm3C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;
using System.Web.Services;
using System.Web.Script.Serialization;

public partial class AdminPanel_Account_ACC_GNTransaction_ACC_GNTransactionAddEdit : System.Web.UI.Page
{
    #region 10.0 Local Variables 

    String FormName = "ACC_GNTransactionAddEdit";

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
            #region 11.2 Fill Labels 

            FillLabels(FormName);

            #endregion 11.2 Fill Labels 

            #region 11.3 DropDown List Fill Section 
            FillDropDownList();


            #endregion 11.3 DropDown List Fill Section 

            #region 11.4 Set Control Default Value 

            lblFormHeader.Text = CV.PageHeaderAdd + " Transaction";
            upr.DisplayAfter = CV.UpdateProgressDisplayAfter;
            dtpDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            dtpDateOfAdmission.Text = DateTime.Now.ToString("dd-MM-yyyy");
            ddlPatientID.Focus();

            #endregion 11.4 Set Control Default Value 

            #region 11.5 Fill Controls 

            FillControls();

            #endregion 11.5 Fill Controls 

            #region 11.6 Set Help Text 

            ucHelp.ShowHelp("Help Text will be shown here");

            #endregion 11.6 Set Help Text 

        }

    }

    #endregion 11.0 Page Load Event

    #region 12.0 FillLabels 


    private void FillLabels(String FormName)
    {
    }

    #endregion 12.0 FillLabels 

    #region 13.0 Fill DropDownList 

    private void FillDropDownList()
    {
        CommonFillMethods.FillDropDownListHospitalID(ddlHospitalID);
        CommonFillMethods.FillDropDownListFinYearID(ddlFinYearID);
        CommonFillMethods.FillDropDownListReceiptTypeID(ddlReceiptTypeID);
        CommonFillMethods.FillDropDownListPatientID(ddlPatientID);

        ddlTreatmentID.Items.Clear();
        ddlTreatmentID.Items.Insert(0, new ListItem("Select Treatment", "-99"));

    }

    #endregion 13.0 Fill DropDownList

    #region 14.0 FillControls By PK  

    private void FillControls()
    {
        if (Request.QueryString["TransactionID"] != null)
        {
            lblFormHeader.Text = CV.PageHeaderEdit + " Transaction";
            ACC_GNTransactionBAL balACC_GNTransaction = new ACC_GNTransactionBAL();
            ACC_GNTransactionENT entACC_GNTransaction = new ACC_GNTransactionENT();
            entACC_GNTransaction = balACC_GNTransaction.SelectPK(CommonFunctions.DecryptBase64Int32(Request.QueryString["TransactionID"]));

            if (!entACC_GNTransaction.PatientID.IsNull)
                ddlPatientID.Text = entACC_GNTransaction.PatientID.Value.ToString();

            if (!entACC_GNTransaction.TreatmentID.IsNull)
                ddlTreatmentID.SelectedValue = entACC_GNTransaction.TreatmentID.Value.ToString();

            if (!entACC_GNTransaction.Amount.IsNull)
                txtAmount.Text = entACC_GNTransaction.Amount.Value.ToString();

            if (!entACC_GNTransaction.ReferenceDoctor.IsNull)
                txtReferenceDoctor.Text = entACC_GNTransaction.ReferenceDoctor.Value.ToString();

            if (!entACC_GNTransaction.Count.IsNull)
                txtCount.Text = entACC_GNTransaction.Count.Value.ToString();

            if (!entACC_GNTransaction.Date.IsNull)
                dtpDate.Text = entACC_GNTransaction.Date.Value.ToString(CV.DefaultDateFormat);

            if (!entACC_GNTransaction.DateOfAdmission.IsNull)
                dtpDateOfAdmission.Text = entACC_GNTransaction.DateOfAdmission.Value.ToString(CV.DefaultDateFormat);

            if (!entACC_GNTransaction.DateOfDischarge.IsNull)
                dtpDateOfDischarge.Text = entACC_GNTransaction.DateOfDischarge.Value.ToString(CV.DefaultDateFormat);

            if (!entACC_GNTransaction.Deposite.IsNull)
                txtDeposite.Text = entACC_GNTransaction.Deposite.Value.ToString();

            if (!entACC_GNTransaction.NetAmount.IsNull)
                txtNetAmount.Text = entACC_GNTransaction.NetAmount.Value.ToString();

            //if (!entACC_GNTransaction.NoOfDays.IsNull)
            //	txtNoOfDays.Text = entACC_GNTransaction.NoOfDays.Value.ToString();

            if (!entACC_GNTransaction.Quantity.IsNull)
                txtQuantity.Text = entACC_GNTransaction.Quantity.Value.ToString();

            //if (!entACC_GNTransaction.Rate.IsNull)
            //    txtRate.Text = entACC_GNTransaction.Rate.Value.ToString();

            if (!entACC_GNTransaction.Remarks.IsNull)
                txtRemarks.Text = entACC_GNTransaction.Remarks.Value.ToString();

            if (!entACC_GNTransaction.HospitalID.IsNull)
                ddlHospitalID.SelectedValue = entACC_GNTransaction.HospitalID.Value.ToString();

            if (!entACC_GNTransaction.FinYearID.IsNull)
                ddlFinYearID.SelectedValue = entACC_GNTransaction.FinYearID.Value.ToString();

            if (!entACC_GNTransaction.ReceiptTypeID.IsNull)
                ddlReceiptTypeID.SelectedValue = entACC_GNTransaction.ReceiptTypeID.Value.ToString();

        }
    }

    #endregion 14.0 FillControls By PK 

    #region 15.0 Save Button Event 

    #region 15.1 Save Transaction
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Page.Validate("vgTransaction");
        if (Page.IsValid)
        {
            try
            {
                ACC_GNTransactionBAL balACC_GNTransaction = new ACC_GNTransactionBAL();
                ACC_GNTransactionENT entACC_GNTransaction = new ACC_GNTransactionENT();

                #region 15.1 Validate Fields 

                String ErrorMsg = String.Empty;
                if (ddlPatientID.SelectedIndex == 0)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredField("Patient");
                if (ddlTreatmentID.SelectedIndex == 0)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredFieldDDL("Treatment");
                if (txtAmount.Text.Trim() == String.Empty)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredField("Amount");
                if (dtpDate.Text.Trim() == String.Empty)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredField("Date");
                if (ddlHospitalID.SelectedIndex == 0)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredFieldDDL("Hospital");
                if (ddlFinYearID.SelectedIndex == 0)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredFieldDDL("Fin Year");

                if (ErrorMsg != String.Empty)
                {
                    ErrorMsg = CommonMessage.ErrorPleaseCorrectFollowing() + ErrorMsg;
                    ucMessage.ShowError(ErrorMsg);
                    return;
                }

                #endregion 15.1 Validate Fields

                #region 15.2 Gather Data 



                if (ddlPatientID.SelectedIndex > 0)
                    entACC_GNTransaction.PatientID = Convert.ToInt32(ddlPatientID.SelectedValue);

                if (ddlTreatmentID.SelectedIndex > 0)
                    entACC_GNTransaction.TreatmentID = Convert.ToInt32(ddlTreatmentID.SelectedValue);

                if (txtAmount.Text.Trim() != String.Empty)
                    entACC_GNTransaction.Amount = Convert.ToDecimal(txtAmount.Text.Trim());

                if (txtReferenceDoctor.Text.Trim() != String.Empty)
                    entACC_GNTransaction.ReferenceDoctor = txtReferenceDoctor.Text.Trim();

                if (txtCount.Text.Trim() != String.Empty)
                    entACC_GNTransaction.Count = Convert.ToInt32(txtCount.Text.Trim());

                if (dtpDate.Text.Trim() != String.Empty)
                    entACC_GNTransaction.Date = Convert.ToDateTime(dtpDate.Text.Trim());

                if (dtpDateOfAdmission.Text.Trim() != String.Empty)
                    entACC_GNTransaction.DateOfAdmission = Convert.ToDateTime(dtpDateOfAdmission.Text.Trim());

                if (dtpDateOfDischarge.Text.Trim() != String.Empty)
                    entACC_GNTransaction.DateOfDischarge = Convert.ToDateTime(dtpDateOfDischarge.Text.Trim());

                if (txtDeposite.Text.Trim() != String.Empty)
                    entACC_GNTransaction.Deposite = Convert.ToDecimal(txtDeposite.Text.Trim());

                if (txtNetAmount.Text.Trim() != String.Empty)
                    entACC_GNTransaction.NetAmount = Convert.ToDecimal(txtNetAmount.Text.Trim());

                //if (txtNoOfDays.Text.Trim() != String.Empty)
                //	entACC_GNTransaction.NoOfDays = Convert.ToInt32(txtNoOfDays.Text.Trim());

                if (txtQuantity.Text.Trim() != String.Empty)
                    entACC_GNTransaction.Quantity = Convert.ToInt32(txtQuantity.Text.Trim());

                //if (txtRate.Text.Trim() != String.Empty)
                //    entACC_GNTransaction.Rate = Convert.ToDecimal(txtRate.Text.Trim());

                if (txtRemarks.Text.Trim() != String.Empty)
                    entACC_GNTransaction.Remarks = txtRemarks.Text.Trim();

                if (ddlHospitalID.SelectedIndex > 0)
                    entACC_GNTransaction.HospitalID = Convert.ToInt32(ddlHospitalID.SelectedValue);

                if (ddlFinYearID.SelectedIndex > 0)
                    entACC_GNTransaction.FinYearID = Convert.ToInt32(ddlFinYearID.SelectedValue);

                if (ddlReceiptTypeID.SelectedIndex > 0)
                    entACC_GNTransaction.ReceiptTypeID = Convert.ToInt32(ddlReceiptTypeID.SelectedValue);

                entACC_GNTransaction.UserID = Convert.ToInt32(Session["UserID"]);

                entACC_GNTransaction.Created = DateTime.Now;

                entACC_GNTransaction.Modified = DateTime.Now;


                #endregion 15.2 Gather Data 


                #region 15.3 Insert,Update,Copy 

                if (Request.QueryString["TransactionID"] != null && Request.QueryString["Copy"] == null)
                {
                    entACC_GNTransaction.TransactionID = CommonFunctions.DecryptBase64Int32(Request.QueryString["TransactionID"]);
                    if (balACC_GNTransaction.Update(entACC_GNTransaction))
                    {
                        Response.Redirect("ACC_GNTransactionList.aspx");
                    }
                    else
                    {
                        ucMessage.ShowError(balACC_GNTransaction.Message);
                    }
                }
                else
                {
                    if (Request.QueryString["TransactionID"] == null || Request.QueryString["Copy"] != null)
                    {
                        if (balACC_GNTransaction.Insert(entACC_GNTransaction))
                        {
                            ucMessage.ShowSuccess(CommonMessage.RecordSaved("Transaction"));
                            ClearControls();
                            //ClearPatientControls();

                        }
                    }
                }

                #endregion 15.3 Insert,Update,Copy

            }
            catch (Exception ex)
            {
                ucMessage.ShowError(ex.Message);

            }

        }

    }

    #endregion 15.1 Save Transaction

    #region  15.2 Save Patient

    protected void btnSavePatient_Click(object sender, EventArgs e)
    {
        Page.Validate("vgPatient");
        if (Page.IsValid)
        {
            try
            {
                MST_PatientBAL balACC_GNTransaction = new MST_PatientBAL();
                ACC_GNPatientENT entMST_Patient = new ACC_GNPatientENT();

                #region 15.1 Validate Fields 

                String ErrorMsg = String.Empty;
                if (txtPatientName.Text.Trim() == String.Empty)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredField("Patient Name");
                if (txtAge.Text.Trim() == String.Empty)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredFieldDDL("Age");
                if (txtMobileNo.Text.Trim() == String.Empty)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredField("Mobile No");
                if (dtpDOB.Text.Trim() == String.Empty)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredField("DOB");

                if (ErrorMsg != String.Empty)
                {
                    ErrorMsg = CommonMessage.ErrorPleaseCorrectFollowing() + ErrorMsg;
                    ucMessage.ShowError(ErrorMsg);
                    return;
                }

                #endregion 15.1 Validate Fields

                #region 15.2 Gather Data 


                if (txtPatientName.Text.Trim() != String.Empty)
                    entMST_Patient.PatientName = txtPatientName.Text.Trim();

                if (txtMobileNo.Text.Trim() != String.Empty)
                    entMST_Patient.MobileNo = txtMobileNo.Text.Trim();

                if (txtAge.Text.Trim() != String.Empty)
                    entMST_Patient.Age = Convert.ToInt32(txtAge.Text.Trim());

                if (dtpDOB.Text.Trim() != String.Empty)
                    entMST_Patient.DOB = Convert.ToDateTime(dtpDOB.Text.Trim());

                if (txtPrimaryDesc.Text.Trim() != String.Empty)
                    entMST_Patient.PrimaryDesc = txtPrimaryDesc.Text.Trim();


                entMST_Patient.UserID = Convert.ToInt32(Session["UserID"]);

                entMST_Patient.Created = DateTime.Now;

                entMST_Patient.Modified = DateTime.Now;


                #endregion 15.2 Gather Data 


                #region 15.3 Insert,Update,Copy 

                if (Request.QueryString["PatientID"] != null && Request.QueryString["Copy"] == null)
                {
                    //entMST_Patient.PatientID = CommonFunctions.DecryptBase64Int32(Request.QueryString["PatientID"]);
                    //if (balACC_GNTransaction.Update(entMST_Patient))
                    //{
                    //    Response.Redirect("ACC_GNTransactionList.aspx");
                    //}
                    //else
                    //{
                    //    ucMessage.ShowError(balACC_GNTransaction.Message);
                    //}
                }
                else
                {
                    if (Request.QueryString["PatientID"] == null || Request.QueryString["Copy"] != null)
                    {
                        SqlInt32 InsertedPatientID = balACC_GNTransaction.InsertPatient(entMST_Patient);

                        if (InsertedPatientID > 0)
                        {
                            ucMessage.ShowSuccess(CommonMessage.RecordSaved("Patient"));
                            ClearPatientControls();

                            CommonFillMethods.FillDropDownListPatientID(ddlPatientID);
                            ddlPatientID.SelectedValue = InsertedPatientID.ToString();
                            ucPatient.ShowPatient(Convert.ToInt32(InsertedPatientID.ToString()));




                        }
                    }
                }

                #endregion 15.3 Insert,Update,Copy

            }
            catch (Exception ex)
            {
                ucMessage.ShowError(ex.Message);

            }


        }
    }
    #endregion  15.2 Save Patient

    #endregion 15.0 Save Button Event 

    #region 16.0 Clear Controls 

    private void ClearControls()
    {
        ddlPatientID.SelectedIndex = 0;
        ddlTreatmentID.SelectedIndex = 0;
        txtAmount.Text = String.Empty;
        txtReferenceDoctor.Text = String.Empty;
        txtCount.Text = String.Empty;
        dtpDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        dtpDateOfAdmission.Text = DateTime.Now.ToString("dd-MM-yyyy");
        dtpDateOfDischarge.Text = String.Empty;
        txtDeposite.Text = String.Empty;
        txtNetAmount.Text = String.Empty;
        txtQuantity.Text = String.Empty;
        txtRemarks.Text = String.Empty;
        ddlHospitalID.SelectedIndex = 0;
        ddlFinYearID.SelectedIndex = 0;
        ddlReceiptTypeID.SelectedIndex = 0;
        ddlPatientID.Focus();
    }


    private void ClearPatientControls()
    {
        txtPatientName.Text = String.Empty;
        txtAge.Text = String.Empty;
        txtMobileNo.Text = String.Empty;
        txtPrimaryDesc.Text = String.Empty;
        dtpDOB.Text = String.Empty;
    }

    #endregion 16.0 Clear Controls 

    #region 17.0 FillTreatmentCombobox

    protected void FillTreatmentCombobox(object sender, EventArgs e)
    {
        if (ddlHospitalID.SelectedIndex > 0)
        {
            SqlInt32 HospitalID = SqlInt32.Null;

            HospitalID = Convert.ToInt32(ddlHospitalID.SelectedValue);
            CommonFillMethods.FillDropDownListTreatmentIDByHospitalID(ddlTreatmentID, HospitalID);
        }
        else
        {
            ddlTreatmentID.Items.Clear();
            ddlTreatmentID.Items.Insert(0, new ListItem("Select Treatment", "-99"));
        }

    }
    #endregion 17.0 FillTreatmentCombobox
    protected void FillPatientView(object sender, EventArgs e)
    {
        int PatientID = 0;


        if (int.TryParse(ddlPatientID.SelectedValue, out PatientID))
        {
            if (PatientID > 0)
            {
                ucPatient.ShowPatient(PatientID);
            }
        }
    }
}