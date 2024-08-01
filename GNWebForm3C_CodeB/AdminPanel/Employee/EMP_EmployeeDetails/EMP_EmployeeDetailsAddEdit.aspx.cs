using GNForm3C.BAL;
using GNForm3C.ENT;
using GNForm3C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Employee_EMP_EmployeeDetails_EMP_EmployeeAddEdit : System.Web.UI.Page
{
    #region 10.0 Local Variables 

    String FormName = "EMP_EmployeeDetailsAddEdit";

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

            lblFormHeader.Text = CV.PageHeaderAdd + " Employee ";
            upr.DisplayAfter = CV.UpdateProgressDisplayAfter;
            txtEmployeeName.Focus();

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
        CommonFillMethods.FillDropDownListEmployeeTypeID(ddlEmployeeTypeID);
    }

    #endregion 13.0 Fill DropDownList

    #region 14.0 FillControls By PK  

    private void FillControls()
    {
        if (Request.QueryString["EmployeeID"] != null)
        {
            lblFormHeader.Text = CV.PageHeaderEdit + " Employee Details ";
            EMP_EmployeeDetailsBAL EMP_EmployeeDetails = new EMP_EmployeeDetailsBAL();
            EMP_EmployeeDetailsENT entEMP_EmployeeDetails = new EMP_EmployeeDetailsENT();
            entEMP_EmployeeDetails = EMP_EmployeeDetails.SelectPK(CommonFunctions.DecryptBase64Int32(Request.QueryString["EmployeeID"]));

            if (!entEMP_EmployeeDetails.EmployeeName.IsNull)
                txtEmployeeName.Text = entEMP_EmployeeDetails.EmployeeName.Value.ToString();

            if (!entEMP_EmployeeDetails.EmployeeTypeID.IsNull)
                ddlEmployeeTypeID.SelectedValue = entEMP_EmployeeDetails.EmployeeTypeID.Value.ToString();

            if (!entEMP_EmployeeDetails.Remark.IsNull)
                txtRemark.Text = entEMP_EmployeeDetails.Remark.Value.ToString();

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
                EMP_EmployeeDetailsBAL balEMP_EmployeeDetails = new EMP_EmployeeDetailsBAL();
                EMP_EmployeeDetailsENT entEMP_EmployeeDetails = new EMP_EmployeeDetailsENT();

                #region 15.1 Validate Fields 

                String ErrorMsg = String.Empty;
                if (txtEmployeeName.Text.Trim() == String.Empty)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredField("Employee Name");
                if (ddlEmployeeTypeID.SelectedIndex == 0)
                    ErrorMsg += " - " + CommonMessage.ErrorRequiredFieldDDL("Employee Type");

                if (ErrorMsg != String.Empty)
                {
                    ErrorMsg = CommonMessage.ErrorPleaseCorrectFollowing() + ErrorMsg;
                    ucMessage.ShowError(ErrorMsg);
                    return;
                }

                #endregion 15.1 Validate Fields

                #region 15.2 Gather Data 


                if (txtEmployeeName.Text.Trim() != String.Empty)
                    entEMP_EmployeeDetails.EmployeeName = txtEmployeeName.Text.Trim();

                if (ddlEmployeeTypeID.SelectedIndex > 0)
                    entEMP_EmployeeDetails.EmployeeTypeID = Convert.ToInt32(ddlEmployeeTypeID.SelectedValue);

                if (txtRemark.Text.Trim() != String.Empty)
                    entEMP_EmployeeDetails.Remark = txtRemark.Text.Trim();

                entEMP_EmployeeDetails.UserID = Convert.ToInt32(Session["UserID"]);

                entEMP_EmployeeDetails.Created = DateTime.Now;

                entEMP_EmployeeDetails.Modified = DateTime.Now;


                #endregion 15.2 Gather Data 


                #region 15.3 Insert,Update,Copy 

                if (Request.QueryString["EmployeeID"] != null && Request.QueryString["Copy"] == null)
                {
                    entEMP_EmployeeDetails.EmployeeID = CommonFunctions.DecryptBase64Int32(Request.QueryString["EmployeeID"]);
                    if (balEMP_EmployeeDetails.Update(entEMP_EmployeeDetails))
                    {
                        Response.Redirect("EMP_EmployeeDetailsList.aspx");
                    }
                    else
                    {
                        ucMessage.ShowError(balEMP_EmployeeDetails.Message);
                    }
                }
                else
                {
                    if (Request.QueryString["EmployeeID"] == null || Request.QueryString["Copy"] != null)
                    {
                        if (balEMP_EmployeeDetails.Insert(entEMP_EmployeeDetails))
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
                ucMessage.ShowError(ex.Message);
            }
        }
    }

    #endregion 15.0 Save Button Event 

    #region 16.0 Clear Controls 

    private void ClearControls()
    {
        txtEmployeeName.Text = String.Empty;
        ddlEmployeeTypeID.SelectedIndex = 0;
        txtRemark.Text = String.Empty;
        txtEmployeeName.Focus();
    }

    #endregion 16.0 Clear Controls 
}