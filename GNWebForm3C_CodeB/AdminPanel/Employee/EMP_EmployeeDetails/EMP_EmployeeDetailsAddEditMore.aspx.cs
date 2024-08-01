using GNForm3C.BAL;
using GNForm3C.ENT;
using GNForm3C;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Employee_EMP_EmployeeDetails_EMP_EmployeeDetailsAddEditMore : System.Web.UI.Page
{
    #region 10.0 Local Variables

    String FormName = "MST_EmployeeDetailsAddEditMore";

    #endregion 10.0 Variables

    #region 11.0 Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 11.1 Check User Login

        if (Session["UserID"] == null)
            Response.Redirect(CV.LoginPageURL);

        #endregion 11.1 Check User Login

        #region 11.2 viewstate
        if (ViewState["DataTable"] == null)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("EmployeeTypeName");
            dt.Columns.Add("EmployeeTypeID");
            dt.Columns.Add("Remark");
            ViewState["DataTable"] = dt;
        }

        #endregion 11.2 viewstate

        if (!Page.IsPostBack)
        {
            #region 11.3 Fill Labels

            FillLabels(FormName);

            #endregion 11.3 Fill Labels

            #region 11.4 DropDown List Fill Section

            FillDropDownList();

            #endregion 11.4 DropDown List Fill Section

            #region 11.5 Set Control Default Value

            lblFormHeader.Text = CV.PageHeaderAdd + "/Edit Employee Name";
            upr.DisplayAfter = CV.UpdateProgressDisplayAfter;
            txtEmployeeName.Focus();

            #endregion 11.5 Set Control Default Value

            #region 11.6 Fill Controls

            // FillControls();

            #endregion 11.6 Fill Controls

            #region 11.7 Set Help Text

            ucHelp.ShowHelp("Help Text will be shown here");

            #endregion 11.7 Set Help Text
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

    #region 14.0 btnAddMore
    protected void btnAddMore_Click(object sender, EventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            try
            {
                if (txtEmployeeName.Text.Trim() != string.Empty && ddlEmployeeTypeID.SelectedIndex > 0)
                {
                    DataTable dt = (DataTable)ViewState["DataTable"];
                    DataRow dr = dt.NewRow();
                    dr["EmployeeName"] = txtEmployeeName.Text.Trim();
                    dr["EmployeeTypeID"] = ddlEmployeeTypeID.SelectedValue;
                    dr["EmployeeTypeName"] = ddlEmployeeTypeID.SelectedItem;
                    dr["Remark"] = txtRemark.Text.Trim();

                    dt.Rows.Add(dr);

                    rpData.DataSource = dt;
                    rpData.DataBind();
                    Div_ShowResult.Visible = true;
                    ClearControls();
                }
                else if (txtEmployeeName.Text.Trim() == string.Empty && ddlEmployeeTypeID.SelectedIndex == 0)
                {
                    ucMessage.ShowError(CommonMessage.ErrorRequiredField("EmployeeName"));
                    ucMessage2.ShowError(CommonMessage.ErrorRequiredFieldDDL("EmployeeTypeName"));
                }
                else if (txtEmployeeName.Text.Trim() == string.Empty)
                {
                    ucMessage.ShowError(CommonMessage.ErrorRequiredField("EmployeeName"));
                }
                else if (ddlEmployeeTypeID.SelectedIndex == 0)
                {
                    ucMessage2.ShowError(CommonMessage.ErrorRequiredFieldDDL("EmployeeTypeName"));
                }
            }
            catch (Exception ex)
            {
                ucMessage.ShowError(ex.Message);
            }
        }
    }
    #endregion 14.0 btnAddMore

    #region 15.0 btnUpdate
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        btnAddMore.Visible = true;
        btnUpdate.Visible = false;
        btnAddMore_Click(sender, e);
    }
    #endregion 15.0 btnUpdate

    #region 16.0 btnSave
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            EMP_EmployeeDetailsBAL balEmp_EmployeeDetails = new EMP_EmployeeDetailsBAL();
            EMP_EmployeeDetailsENT entEmp_EmployeeDetails = new EMP_EmployeeDetailsENT();

            foreach (RepeaterItem items in rpData.Items)
            {
                try
                {
                    #region 16.1 FindControl

                    Label lblEmployeeName = (Label)items.FindControl("lblEmployeeName");
                    Label lblEmployeeTypeName = (Label)items.FindControl("lblEmployeeTypeName");
                    Label lblRemark = (Label)items.FindControl("lblRemark");
                    HiddenField hfEmployeeTypeID = (HiddenField)items.FindControl("hfEmployeeTypeID");

                    #endregion 16.1 FindControl

                    #region 16.2 Gather Data

                    entEmp_EmployeeDetails.EmployeeTypeID = Convert.ToInt32(hfEmployeeTypeID.Value);
                    entEmp_EmployeeDetails.EmployeeName = lblEmployeeName.Text.Trim();
                    entEmp_EmployeeDetails.Remark = lblRemark.Text.Trim();
                    entEmp_EmployeeDetails.UserID = Convert.ToInt32(Session["UserID"]);
                    entEmp_EmployeeDetails.Created = DateTime.Now;
                    entEmp_EmployeeDetails.Modified = DateTime.Now;

                    #endregion 16.2 Gather Data

                    #region 16.3 Insert DataentEmp_EmployeeDetails
                    if (balEmp_EmployeeDetails.Insert(entEmp_EmployeeDetails))
                    {
                        ucMessage.ShowSuccess(CommonMessage.RecordSaved());
                    }
                    #endregion 16.3 Insert Data
                    Div_ShowResult.Visible = false;
                }
                catch (Exception ex)
                {
                    ucMessage.ShowError(ex.Message);
                }
            }
            DataTable dt = (DataTable)ViewState["DataTable"];
            dt.Clear();
        }
    }
    #endregion 16.0 btnSave

    #region 17.0 rpData_ItemCommand
    protected void rpData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            #region 17.1 Edit Record
            if (e.CommandName == "EditRecord")
            {

                if (txtEmployeeName.Text.Trim() == string.Empty && ddlEmployeeTypeID.SelectedIndex == 0)
                {
                    btnAddMore.Visible = false;
                    btnUpdate.Visible = true;

                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    DataTable dt = (DataTable)ViewState["DataTable"];
                    txtEmployeeName.Text = dt.Rows[rowIndex]["EmployeeName"].ToString();
                    ddlEmployeeTypeID.SelectedIndex = Convert.ToInt32(dt.Rows[rowIndex]["EmployeeTypeID"]);
                    txtRemark.Text = dt.Rows[rowIndex]["Remark"].ToString();

                    dt.Rows.RemoveAt(rowIndex);
                    ViewState["DataTable"] = dt;
                    rpData.DataSource = dt;
                    rpData.DataBind();
                    if (dt.Rows.Count == 0)
                    {
                        Div_ShowResult.Visible = false;
                    }
                }
            }
            #endregion 17.1 Edit Record

            #region 17.2 Delete Record
            if (e.CommandName == "DeleteRecord")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                DataTable dt = (DataTable)ViewState["DataTable"];
                dt.Rows.RemoveAt(rowIndex);
                ViewState["DataTable"] = dt;
                rpData.DataSource = dt;
                rpData.DataBind();
                if (dt.Rows.Count == 0)
                {
                    Div_ShowResult.Visible = false;
                }
            }
            #endregion 17.2 Delete Record
        }
        catch (Exception ex)
        {
            ucMessage.ShowError(ex.Message);
        }
    }
    #endregion 17.0 rpData_ItemCommand

    #region  18.0 Clear Controls
    private void ClearControls()
    {
        txtEmployeeName.Text = "";
        ddlEmployeeTypeID.SelectedIndex = 0;
        txtRemark.Text = "";
    }

    #endregion 18.0 Clear Controlsol
}