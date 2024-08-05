using GNForm3C.BAL;
using GNForm3C.ENT;
using GNForm3C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Student_STU_Student_STU_StudentAddEditPopUp : System.Web.UI.Page
{
    #region 11.0 Page Load Event 

    protected void Page_Load(object sender, EventArgs e)
    {
        #region 11.1 Check User Login 

        if (Session["UserID"] == null)
            Response.Redirect(CV.LoginPageURL);

        #endregion 11.1 Check User Login 

        if (!Page.IsPostBack)
        {
            #region 11.3 DropDown List Fill Section 

            FillDropDownList();

            #endregion 11.3 DropDown List Fill Section 

            #region 11.4 Set Control Default Value 
            txtStudentName.Focus();
            //upr.DisplayAfter = CV.UpdateProgressDisplayAfter;
            #endregion 11.4 Set Control Default Value 

            #region 11.5 Fill Controls 

            FillControls();

            #endregion 11.5 Fill Controls 

            #region 11.6 Set Help Text 

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
        CommonFillMethods.FillDropDownListSemester(ddlCurrentSem);
        CommonFillMethods.FillDropDownListGender(ddlGender);
    }

    #endregion 13.0 Fill DropDownList

    #region 14.0 FillControls By PK  

    private void FillControls()
    {
        if (Request.QueryString["StudentID"] != null)
        {
            STU_StudentBAL STU_StudentDetails = new STU_StudentBAL();
            STU_StudentENT entSTU_StudentDetails = new STU_StudentENT();
            entSTU_StudentDetails = STU_StudentDetails.SelectPK(CommonFunctions.DecryptBase64Int32(Request.QueryString["StudentID"]));

            if (!entSTU_StudentDetails.StudentName.IsNull)
                txtStudentName.Text = entSTU_StudentDetails.StudentName.Value.ToString();

            if (!entSTU_StudentDetails.EnrollmentNo.IsNull)
                txtEnrollmentNo.Text = entSTU_StudentDetails.EnrollmentNo.Value.ToString();

            if (!entSTU_StudentDetails.RollNo.IsNull)
                txtRollNo.Text = entSTU_StudentDetails.RollNo.Value.ToString();

            if (!entSTU_StudentDetails.CurrentSem.IsNull)
                ddlCurrentSem.SelectedValue = entSTU_StudentDetails.CurrentSem.Value.ToString();

            if (!entSTU_StudentDetails.Gender.IsNull)
                ddlGender.SelectedValue = entSTU_StudentDetails.Gender.Value.ToString();

            if (!entSTU_StudentDetails.EmailInstitute.IsNull)
                txtEmailInstitute.Text = entSTU_StudentDetails.EmailInstitute.Value.ToString();

            if (!entSTU_StudentDetails.EmailPersonal.IsNull)
                txtEmailPersonal.Text = entSTU_StudentDetails.EmailPersonal.Value.ToString();

            if (!entSTU_StudentDetails.ContactNo.IsNull)
                txtContactNo.Text = entSTU_StudentDetails.ContactNo.Value.ToString();

            if (!entSTU_StudentDetails.BirthDate.IsNull)
                dtpBirthDate.Text = entSTU_StudentDetails.BirthDate.Value.ToString(GNForm3C.CV.DefaultSQLDateFormat);
        }
    }

    #endregion 14.0 FillControls By PK 

    #region 15.0 Save Button Event 

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            try
            {
                STU_StudentBAL balSTU_StudentDetails = new STU_StudentBAL();
                STU_StudentENT entSTU_StudentDetails = new STU_StudentENT();

                #region 15.1 Validate Fields 

                String ErrorMsg = String.Empty;
                if (txtStudentName.Text.Trim() == String.Empty)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredField("Student Name");
                if (ddlCurrentSem.SelectedIndex == 0)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredFieldDDL("Current Semester");
                if (ddlGender.SelectedIndex == 0)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredFieldDDL("Gender");
                if (txtEnrollmentNo.Text.Trim() == String.Empty)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredField("Enrollment No.");
                if (txtContactNo.Text.Trim() == String.Empty)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredField("Contact No.");
                if (txtEmailPersonal.Text.Trim() == String.Empty)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredField("Personal Email");
                if (dtpBirthDate.Text.Trim() == String.Empty)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredField("BirthDate");

                if (ErrorMsg != String.Empty)
                {
                    ErrorMsg = CommonMessage.ErrorPleaseCorrectFollowing() + ErrorMsg;
                    //ucMessage.ShowError(ErrorMsg);
                    return;
                }

                #endregion 15.1 Validate Fields

                #region 15.2 Gather Data 

                if (txtStudentName.Text.Trim() != String.Empty)
                    entSTU_StudentDetails.StudentName = txtStudentName.Text.Trim();

                if (ddlCurrentSem.SelectedIndex > 0)
                    entSTU_StudentDetails.CurrentSem = Convert.ToInt32(ddlCurrentSem.SelectedValue);

                if (ddlGender.SelectedIndex > 0)
                    entSTU_StudentDetails.Gender = ddlGender.SelectedValue;

                if (txtEnrollmentNo.Text.Trim() != String.Empty)
                    entSTU_StudentDetails.EnrollmentNo = txtEnrollmentNo.Text.Trim();

                if (txtContactNo.Text.Trim() != String.Empty)
                    entSTU_StudentDetails.ContactNo = txtContactNo.Text.Trim();

                if (txtEmailPersonal.Text.Trim() != String.Empty)
                    entSTU_StudentDetails.EmailPersonal = txtEmailPersonal.Text.Trim();

                if (txtEmailInstitute.Text.Trim() != String.Empty)
                    entSTU_StudentDetails.EmailInstitute = txtEmailInstitute.Text.Trim();

                if (dtpBirthDate.Text.Trim() != String.Empty)
                    entSTU_StudentDetails.BirthDate = Convert.ToDateTime(dtpBirthDate.Text.Trim());

                if (txtRollNo.Text.Trim() != String.Empty)
                    entSTU_StudentDetails.RollNo = Convert.ToInt32(txtRollNo.Text.Trim());

                entSTU_StudentDetails.UserID = Convert.ToInt32(Session["UserID"]);

                entSTU_StudentDetails.Created = DateTime.Now;

                entSTU_StudentDetails.Modified = DateTime.Now;


                #endregion 15.2 Gather Data 

                #region 15.3 Insert,Update,Copy 

                if (Request.QueryString["StudentID"] != null && Request.QueryString["Copy"] == null)
                {
                    entSTU_StudentDetails.StudentID = CommonFunctions.DecryptBase64Int32(Request.QueryString["StudentID"]);
                    if (balSTU_StudentDetails.Update(entSTU_StudentDetails))
                    {
                        Response.Redirect("STU_StudentList.aspx");
                    }
                    else
                    {
                        ucMessage.ShowError(balSTU_StudentDetails.Message);
                    }
                }
                else
                {
                    if (Request.QueryString["StudentID"] == null || Request.QueryString["Copy"] != null)
                    {
                        if (balSTU_StudentDetails.Insert(entSTU_StudentDetails))
                        {
                            ucMessage.ShowSuccess(CommonMessage.RecordSaved());
                            ClearControls();
                        }
                    }
                }

                #endregion 15.3 Insert,Update,Copy
            }
            catch (Exception ex)
            {
                //ucMessage.ShowError(ex.Message);
            }
        }
    }

    #endregion 15.0 Save Button Event 

    #region 16.0 Clear Controls 
    private void ClearControls()
    {
        txtStudentName.Focus();
        txtStudentName.Text = String.Empty;
        txtEnrollmentNo.Text = String.Empty;
        txtEmailInstitute.Text = String.Empty;
        txtEmailPersonal.Text = String.Empty;
        txtContactNo.Text = String.Empty;
        txtRollNo.Text = String.Empty;
        ddlCurrentSem.SelectedIndex = 0;
        ddlGender.SelectedIndex = 0;
    }

    #endregion 16.0 Clear Controls 
}