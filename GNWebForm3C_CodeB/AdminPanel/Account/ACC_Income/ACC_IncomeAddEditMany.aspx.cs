﻿using GNForm3C.BAL;
using GNForm3C.ENT;
using GNForm3C;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Account_ACC_Income_ACC_IncomeAddEditMany : System.Web.UI.Page
{
    #region 10.0 Local Variables

    String FormName = "ACC_IncomeAddEdit";

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
            if (Request.QueryString["HospitalID"] != null)
            {
                btnShow_Click(sender, e);
            }
            #region 11.2 Fill Labels

            FillLabels(FormName);

            #endregion 11.2 Fill Labels

            #region 11.3 DropDown List Fill Section

            FillDropDownList();

            #endregion 11.3 DropDown List Fill Section

            #region 11.4 Set Control Default Value

            lblFormHeader.Text = CV.PageHeaderMany + " Income";
            #endregion 11.4 Set Control Default Value
        }
    }
    #endregion Pageload

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
        CommonFillMethods.FillSingleDropDownListIncomeTypeID(ddlIncomeTypeID);
    }

    #endregion 13.0 Fill DropDownList

    #region 14.0 Show Button Event
    protected void btnShow_Click(object sender, EventArgs e)
    {
        SqlInt32 HospitalID = SqlInt32.Null;
        SqlInt32 IncomeTypeID = SqlInt32.Null;
        SqlInt32 FinYearID = SqlInt32.Null;

        #region NavigateLogic
        if (Request.QueryString["HospitalID"] != null && Request.QueryString["FinYearID"] != null && Request.QueryString["IncomeTypeID"] != null)
        {
            if (!Page.IsPostBack)
            {
                HospitalID = CommonFunctions.DecryptBase64Int32(Request.QueryString["HospitalID"]);
                IncomeTypeID = CommonFunctions.DecryptBase64Int32(Request.QueryString["IncomeTypeID"]);
                FinYearID = CommonFunctions.DecryptBase64Int32(Request.QueryString["FinYearID"]);
            }
            else
            {
                if (ddlHospitalID.SelectedIndex > 0)
                    HospitalID = Convert.ToInt32(ddlHospitalID.SelectedValue);
                if (ddlIncomeTypeID.SelectedIndex > 0)
                    IncomeTypeID = Convert.ToInt32(ddlIncomeTypeID.SelectedValue);
                if (ddlFinYearID.SelectedIndex > 0)
                    FinYearID = Convert.ToInt32(ddlFinYearID.SelectedValue);
            }
        }
        else
        {
            if (ddlHospitalID.SelectedIndex > 0)
                HospitalID = Convert.ToInt32(ddlHospitalID.SelectedValue);
            if (ddlIncomeTypeID.SelectedIndex > 0)
                IncomeTypeID = Convert.ToInt32(ddlIncomeTypeID.SelectedValue);
            if (ddlFinYearID.SelectedIndex > 0)
                FinYearID = Convert.ToInt32(ddlFinYearID.SelectedValue);
        }
        #endregion NavigateLogic

        ACC_IncomeBAL balACC_Income = new ACC_IncomeBAL();

        DataTable dt = balACC_Income.SelectShow(HospitalID, IncomeTypeID, FinYearID);

        if (Request.QueryString["HospitalID"] != null)
            ddlHospitalID.SelectedValue = CommonFunctions.DecryptBase64(Request.QueryString["HospitalID"]);
        if (Request.QueryString["IncomeTypeID"] != null)
            ddlIncomeTypeID.SelectedValue = CommonFunctions.DecryptBase64(Request.QueryString["IncomeTypeID"]);
        if (Request.QueryString["FinYearID"] != null)
            ddlFinYearID.SelectedValue = CommonFunctions.DecryptBase64(Request.QueryString["FinYearID"]);

        foreach (DataColumn dtc in dt.Columns)
        {
            dtc.AutoIncrement = false;
            dtc.AllowDBNull = true;
        }
        dt.AcceptChanges();

        int count = 10 - dt.Rows.Count;
        for (int i = 1; i <= count; i++)
        {
            dt.Rows.Add();
        }

        rpData.DataSource = dt;
        rpData.DataBind();
        Div_ShowResult.Visible = true;

    }

    #endregion 14.0 Show Button Event

    protected void rpData_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList ddlUser = e.Item.FindControl("ddlUser") as DropDownList;

            if (ddlUser != null)
            {
                CommonFillMethods.FillDropDownListUserID(ddlUser);

                DataRowView drv = e.Item.DataItem as DataRowView;

                if (drv != null)
                {
                    string selectedUserID = drv["UserID"].ToString();

                    if (!string.IsNullOrEmpty(selectedUserID))
                    {
                        ddlUser.SelectedValue = selectedUserID;
                    }
                }
            }
        }
    }

    #region 15.0 Save Button Event DataTable

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            SqlInt32 HospitalID = SqlInt32.Null;
            if (ddlHospitalID.SelectedIndex > 0)
                HospitalID = Convert.ToInt32(ddlHospitalID.SelectedValue);

            ACC_IncomeBAL balACC_Income = new ACC_IncomeBAL();
            ACC_IncomeENT entACC_Income = new ACC_IncomeENT();

            DataTable dtIncomeTable = new DataTable();
            dtIncomeTable.Columns.Add("IncomeID", typeof(SqlInt32));
            dtIncomeTable.Columns.Add("IncomeTypeID", typeof(SqlInt32));
            dtIncomeTable.Columns.Add("Amount", typeof(SqlDecimal));
            dtIncomeTable.Columns.Add("IncomeDate", typeof(SqlDateTime));
            dtIncomeTable.Columns.Add("Note", typeof(SqlString));
            //  dtIncomeTable.Columns.Add("Remarks", typeof(SqlString));
            dtIncomeTable.Columns.Add("HospitalID", typeof(SqlInt32));
            dtIncomeTable.Columns.Add("FinYearID", typeof(SqlInt32));
            dtIncomeTable.Columns.Add("UserID", typeof(SqlInt32));
            dtIncomeTable.Columns.Add("Created", typeof(SqlDateTime));
            dtIncomeTable.Columns.Add("Modified", typeof(SqlDateTime));
            dtIncomeTable.Columns.Add("Operation", typeof(string)); // 'I', 'U', or 'D'

            try
            {
                foreach (RepeaterItem items in rpData.Items)
                {
                    #region FindControl

                    var ddlFinYearID = (DropDownList)items.FindControl("ddlFinYearID");
                    var ddlIncomeTypeID = (DropDownList)items.FindControl("ddlIncomeTypeID");

                    var dtpIncomeDate = (TextBox)items.FindControl("dtpIncomeDate");

                    TextBox txtAmount = (TextBox)items.FindControl("txtAmount");
                    HiddenField Hdfiled = (HiddenField)items.FindControl("hdIncomeID");
                    TextBox txtNote = (TextBox)items.FindControl("txtNote");
                    CheckBox chkIsSelected = (CheckBox)items.FindControl("chkIsSelected");

                    #endregion FindControl

                    #region 15.1.1 Gather Data
                    if (chkIsSelected.Checked)
                    {
                        entACC_Income.HospitalID = Convert.ToInt32(ddlHospitalID.SelectedValue);
                        entACC_Income.FinYearID = Convert.ToInt32(ddlFinYearID.SelectedValue);
                        entACC_Income.IncomeTypeID = Convert.ToInt32(ddlIncomeTypeID.SelectedValue);
                        entACC_Income.IncomeDate = Convert.ToDateTime(dtpIncomeDate.Text);
                        entACC_Income.Amount = Convert.ToDecimal(txtAmount.Text);
                        entACC_Income.Note = Convert.ToString(txtNote.Text);
                        entACC_Income.UserID = Convert.ToInt32(Session["UserID"]);
                        entACC_Income.Created = DateTime.Now;
                        entACC_Income.Modified = DateTime.Now;
                    }
                    #endregion 15.1.1 Gather Data

                    if (Hdfiled.Value != string.Empty)
                    {
                        if (chkIsSelected.Checked)
                        {
                            #region 15.1.2 Update Data
                            if (ddlIncomeTypeID.Text.Trim() == string.Empty)
                            {
                                ddlIncomeTypeID.Focus();
                                ucMessage.ShowError("Enter Income Type");
                                break;
                            }
                            else
                            {
                                entACC_Income.IncomeID = Convert.ToInt32(Hdfiled.Value);
                                dtIncomeTable.Rows.Add(
                                    entACC_Income.IncomeID,
                                    entACC_Income.IncomeTypeID,
                                    entACC_Income.Amount,
                                    entACC_Income.IncomeDate,
                                    entACC_Income.Note,
                                    //      entACC_Income.Remarks,
                                    entACC_Income.HospitalID,
                                    entACC_Income.FinYearID,
                                    entACC_Income.UserID,
                                    entACC_Income.Created,
                                    entACC_Income.Modified,
                                    'U'
                                    );
                            }
                            #endregion 15.1.2 Update Data
                        }
                        else
                        {
                            #region 15.1.3 Delete Data
                            if (ddlIncomeTypeID.Text.Trim() == string.Empty)
                            {
                                ddlIncomeTypeID.Focus();
                                ucMessage.ShowError("Enter Income Type");
                                break;
                            }
                            else
                            {
                                entACC_Income.IncomeID = Convert.ToInt32(Hdfiled.Value);
                                dtIncomeTable.Rows.Add(
                                    entACC_Income.IncomeID,
                                    entACC_Income.IncomeTypeID,
                                    entACC_Income.Amount,
                                    entACC_Income.IncomeDate,
                                    entACC_Income.Note,
                                    //  entACC_Income.Remarks,
                                    entACC_Income.HospitalID,
                                    entACC_Income.FinYearID,
                                    entACC_Income.UserID,
                                    entACC_Income.Created,
                                    entACC_Income.Modified,
                                    'D'
                                    );
                            }
                            #endregion 15.1.3 Delete Data
                        }
                    }
                    else
                    {

                        if (chkIsSelected.Checked)
                        {
                            #region 15.1.4 Insert Data
                            if (ddlIncomeTypeID.Text.Trim() == string.Empty && txtAmount.Text.Trim() != string.Empty)
                            {
                                ddlIncomeTypeID.Focus();
                                ucMessage.ShowError("Enter Income Type");
                            }
                            else
                            {
                                if (ddlIncomeTypeID.Text.Trim() != string.Empty)
                                {
                                    //if (balACC_Income.Insert(entACC_Income))
                                    //{
                                    //    Div_ShowResult.Visible = false;
                                    //    ucMessage.ShowSuccess(CommonMessage.RecordSaved());
                                    //}
                                    dtIncomeTable.Rows.Add(
                                    entACC_Income.IncomeID,
                                    entACC_Income.IncomeTypeID,
                                    entACC_Income.Amount,
                                    entACC_Income.IncomeDate,
                                    entACC_Income.Note,
                                    // entACC_Income.Remarks,
                                    entACC_Income.HospitalID,
                                    entACC_Income.FinYearID,
                                    entACC_Income.UserID,
                                    entACC_Income.Created,
                                    entACC_Income.Modified,
                                    'I'
                                    );

                                }
                            }
                            #endregion  15.1.4 Insert Data
                        }
                    }
                    Div_ShowResult.Visible = false;

                }

                if (balACC_Income.Upsert(dtIncomeTable))
                {
                    ucMessage.ShowSuccess(CommonMessage.RecordSaved());
                    ClearControls();
                }
                else
                {
                    ucMessage.ShowError(balACC_Income.Message);
                }

            }
            catch (Exception ex)
            {
                ucMessage.ShowError(ex.Message);
            }

        }
    }

    #endregion 15.0 Save Button Event DataTable

    #region 16.0 Clear Controls
    private void ClearControls()
    {
        ddlHospitalID.SelectedIndex = 0;
        ddlIncomeTypeID.SelectedIndex = 0;
        ddlFinYearID.SelectedIndex = 0;
    }

    #endregion 16.0 Clear Controls

    #region 17.0 Add Row Button
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("IncomeDate");
        dt.Columns.Add("Amount");
        dt.Columns.Add("Note");
        dt.Columns.Add("IncomeID");
        dt.Columns.Add("UserID");

        foreach (RepeaterItem rp in rpData.Items)
        {
            TextBox dtpIncomeDate = (TextBox)rp.FindControl("dtpIncomeDate");
            TextBox txtAmount = (TextBox)rp.FindControl("txtAmount");
            TextBox txtNote = (TextBox)rp.FindControl("txtNote");
            HiddenField hdIncomeID = (HiddenField)rp.FindControl("hdIncomeID");
            DropDownList ddlUser = (DropDownList)rp.FindControl("ddlUser");

            DataRow dr = dt.NewRow();
            dr["IncomeDate"] = dtpIncomeDate.Text.Trim();
            dr["Amount"] = txtAmount.Text.Trim();
            dr["Note"] = txtNote.Text.Trim();
            dr["IncomeID"] = hdIncomeID.Value.ToString();

            if (ddlUser != null && ddlUser.SelectedIndex > 0) // Check if a valid user is selected
            {
                dr["UserID"] = ddlUser.SelectedValue;
            }
            else
            {
                dr["UserID"] = DBNull.Value; // Or handle no selection case appropriately
            }

            dt.Rows.Add(dr);
        }
        int count = 0;
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["IncomeDate"].ToString() != "")
                count++;
        }
        if (count == dt.Rows.Count)
            dt.Rows.Add();

        rpData.DataSource = dt;
        rpData.DataBind();
    }
    #endregion 17.0 Add Row Button
}