using GNForm3C.BAL;
using GNForm3C;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using iTextSharp.text;
using System.Activities.Expressions;
using System.Drawing;
using System.Reflection.Emit;
using System.Globalization;

public partial class AdminPanel_Account_ACC_ExpInm_Ledger_ACC_ExpInm_LedgerList : System.Web.UI.Page
{
    #region 11.0 Variables

    String FormName = "MST_LedgerList";
    static Int32 PageRecordSize = CV.PageRecordSize;//Size of record per page
    Int32 PageDisplaySize = CV.PageDisplaySize;
    Int32 DisplayIndex = CV.DisplayIndex;

    #endregion 11.0 Variables

    #region 12.0 Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 12.0 Check User Login

        if (Session["UserID"] == null)
            Response.Redirect(CV.LoginPageURL);

        #endregion 12.0 Check User Login

        if (!Page.IsPostBack)
        {
            #region 12.1 DropDown List Fill Section

            FillDropDownList();

            #endregion 12.1 DropDown List Fill Section

            #region 12.2 Set Default Value

            lblSearchHeader.Text = CV.SearchHeaderText;
            lblSearchResultHeader.Text = CV.SearchResultHeaderText;
            upr.DisplayAfter = CV.UpdateProgressDisplayAfter;

            #endregion 12.2 Set Default Value

            Search(1);

            #region 12.3 Set Help Text
            ucHelp.ShowHelp("Help Text will be shown here");
            #endregion 12.3 Set Help Text
        }
    }
    #endregion 12.0 Page Load Event

    #region 13.0 FillLabels

    private void FillLabels(String FormName)
    {
    }

    #endregion

    #region 14.0 DropDownList

    #region 14.1 Fill DropDownList

    private void FillDropDownList()
    {
        CommonFunctions.GetDropDownPageSize(ddlPageSizeBottom);
        ddlPageSizeBottom.SelectedValue = PageRecordSize.ToString();
    }

    #endregion 14.1 Fill DropDownList

    #endregion 14.0 DropDownList

    #region 15.0 Search

    #region 15.1 Button Search Click Event

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search(1);
    }

    #endregion 15.1 Button Search Click Event

    #region 15.2 Search Function

    private void Search(int PageNo)
    {
        #region Parameters
        SqlDateTime FromDate = SqlDateTime.Null;
        SqlDateTime ToDate = SqlDateTime.Null;
        SqlDecimal TotalBalance = 0;
        Int32 Offset = (PageNo - 1) * PageRecordSize;
        Int32 TotalRecords = 0;
        Int32 TotalPages = 1;

        #endregion Parameters

        #region Gather Data

        #region NavigateLogic

        if (dtpFromDate.Text.Trim() != String.Empty)
            FromDate = Convert.ToDateTime(dtpFromDate.Text.Trim());

        if (dtpToDate.Text.Trim() != String.Empty)
            ToDate = SqlDateTime.Parse(dtpToDate.Text.Trim());
        #endregion NavigateLogic


        #endregion Gather Data

        ACC_LedgerBAL balACC_Ledger = new ACC_LedgerBAL();

        DataTable dt = balACC_Ledger.SelectPage(Offset, PageRecordSize, out TotalRecords, FromDate, ToDate);


        foreach (DataRow row in dt.Rows)
        {
            if (row.Field<String>("LedgerType") == "Income")
                TotalBalance += row.Field<decimal>("LedgerAmount");
            else
                TotalBalance -= row.Field<decimal>("LedgerAmount");
        }

        if (PageRecordSize == 0 && dt.Rows.Count > 0)
        {
            PageRecordSize = dt.Rows.Count;
            TotalPages = (int)Math.Ceiling((double)((decimal)TotalRecords / Convert.ToDecimal(PageRecordSize)));
        }
        else
            TotalPages = (int)Math.Ceiling((double)((decimal)TotalRecords / Convert.ToDecimal(PageRecordSize)));

        if (dt != null && dt.Rows.Count > 0)
        {
            Div_SearchResult.Visible = true;
            Div_ExportOption.Visible = true;
            rpData.DataSource = dt;
            rpData.DataBind();
            decimal totalBalance = Convert.ToDecimal(TotalBalance.ToString());
            txtTotalBalance.Text = String.Format(GNForm3C.CV.DefaultCurrencyFormatWithDecimalPoint, totalBalance);

            if (TotalBalance >= 0)
            {
                txtTotalBalance.Style["color"] = "green";
            }
            else
            {
                txtTotalBalance.Style["color"] = "red";
            }


            //if (Request.QueryString["HospitalID"] != null)
            //{
            //    EmployeeTypeID = CommonFunctions.DecryptBase64Int32(Request.QueryString["HospitalID"]);
            //    ddlEmployeeTypeID.SelectedIndex = Convert.ToInt32(EmployeeTypeID.ToString());
            //}

            if (PageNo > TotalPages)
                PageNo = TotalPages;

            ViewState["TotalPages"] = TotalPages;
            ViewState["CurrentPage"] = PageNo;

            CommonFunctions.BindPageList(TotalPages, TotalRecords, PageNo, PageDisplaySize, DisplayIndex, rpPagination, liPrevious, lbtnPrevious, liFirstPage, lbtnFirstPage, liNext, lbtnNext, liLastPage, lbtnLastPage);

            lblRecordInfoBottom.Text = CommonMessage.PageDisplayMessage(Offset, dt.Rows.Count, TotalRecords, PageNo, TotalPages);
            lblRecordInfoTop.Text = CommonMessage.PageDisplayMessage(Offset, dt.Rows.Count, TotalRecords, PageNo, TotalPages);

            lbtnExportExcel.Visible = true;
            if (TotalRecords <= CV.SmallestPageSize)
            {
                Div_Pagination.Visible = false;
                Div_GoToPageNo.Visible = false;
                Div_PageSize.Visible = false;
            }
            else
            {
                Div_Pagination.Visible = true;
                Div_GoToPageNo.Visible = true;
                Div_PageSize.Visible = true;
            }
        }

        else if (TotalPages < PageNo && TotalPages > 0)
            Search(TotalPages);

        else
        {
            Div_SearchResult.Visible = false;
            lbtnExportExcel.Visible = false;

            ViewState["TotalPages"] = 0;
            ViewState["CurrentPage"] = 1;

            rpData.DataSource = null;
            rpData.DataBind();

            lblRecordInfoBottom.Text = CommonMessage.NoRecordFound();
            lblRecordInfoTop.Text = CommonMessage.NoRecordFound();

            CommonFunctions.BindPageList(0, 0, PageNo, PageDisplaySize, DisplayIndex, rpPagination, liPrevious, lbtnPrevious, liFirstPage, lbtnFirstPage, liNext, lbtnNext, liLastPage, lbtnLastPage);

            ucMessage.ShowError(CommonMessage.NoRecordFound());
        }
    }

    #endregion 15.2 Search Function

    #endregion 15.0 Search

    #region 16.0 Repeater Events

    #region 16.1 Item Command Event

    protected void rpData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
    }
    #endregion 16.1 Item Command Event

    #endregion 16.0 Repeater Events

    #region 17.0 Pagination

    #region 17.1 Pagination Events

    #region ItemDataBound Event

    protected void rpPagination_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lb = (LinkButton)e.Item.FindControl("lbtnPageNo");
            HtmlGenericControl hgc = (HtmlGenericControl)e.Item.FindControl("liPageNo");
            if (Convert.ToInt32(ViewState["CurrentPage"]) == Convert.ToInt32(lb.CommandArgument))
            {
                hgc.Attributes["class"] = CSSClass.PaginationButtonActive;
                lb.Enabled = false;
            }
            else
                hgc.Attributes["class"] = CSSClass.PaginationButton;
        }
    }

    #endregion ItemDataBound Event

    #region PageChange Event

    protected void PageChange_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = (LinkButton)(sender);
        int Value = Convert.ToInt32(lbtn.CommandArgument);
        String Name = lbtn.CommandName.ToString();

        if (Name == "PageNo" || Name == "FirstPage")
            Search(Value);

        else if (Name == "PreviousPage")
            Search(Convert.ToInt32(ViewState["CurrentPage"]) - Value);

        else if (Name == "NextPage")
            Search(Convert.ToInt32(ViewState["CurrentPage"]) + Value);

        else if (Name == "LastPage")
            Search(Convert.ToInt32(ViewState["TotalPages"]));

        else if (Name == "GoPageNo")
        {
            if (txtPageNo.Text.Trim() == String.Empty)
            {
                ucMessage.ShowError(CommonMessage.ErrorRequiredField("Page No"));
                return;
            }
            else
            {
                Value = Convert.ToInt32(txtPageNo.Text);
                if (Value > Convert.ToInt32(ViewState["TotalPages"]))
                {
                    ucMessage.ShowError(CommonMessage.ErrorInvalidField("Page No"));
                    return;
                }
                Search(Value);
            }
        }
    }

    #endregion PageChange Event

    #endregion 17.1 Pagination Events

    #endregion 17.0 Pagination

    #region 18.0 Button Delete Click Event


    #endregion 18.0 Button Delete Click Event

    #region 19.0 Export Data

    #region 19.1 Excel Export Button Click Event

    protected void lbtnExport_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = (LinkButton)(sender);
        String ExportType = lbtn.CommandArgument.ToString();

        int TotalReceivedRecord = 0;

        SqlDateTime FromDate = SqlDateTime.Null;
        SqlDateTime ToDate = SqlDateTime.Null;

        if (dtpFromDate.Text.Trim() != String.Empty)
            FromDate = Convert.ToDateTime(dtpFromDate.Text.Trim());

        if (dtpToDate.Text.Trim() != String.Empty)
            ToDate = Convert.ToDateTime(dtpToDate.Text.Trim());


        Int32 Offset = 0;

        if (ViewState["CurrentPage"] != null)
            Offset = (Convert.ToInt32(ViewState["CurrentPage"]) - 1) * PageRecordSize;

        ACC_LedgerBAL balACC_Ledger = new ACC_LedgerBAL();
        DataTable dtACC_Ledger = balACC_Ledger.SelectPage(Offset, PageRecordSize, out TotalReceivedRecord, FromDate, ToDate);
        if (dtACC_Ledger != null && dtACC_Ledger.Rows.Count > 0)
        {
            Session["ExportTable"] = dtACC_Ledger;
            Response.Redirect("~/Default/Export.aspx?ExportType=" + ExportType);
        }
    }

    #endregion 19.1 Excel Export Button Click Event

    #endregion 19.0 Export Data

    #region 20.0 Cancel Button Event

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    #endregion 20.0 Cancel Button Event

    #region 21.0 ddlPageSize Selected Index Changed Event

    protected void ddlPageSizeBottom_SelectedIndexChanged(object sender, EventArgs e)
    {
        PageRecordSize = Convert.ToInt32(ddlPageSizeBottom.SelectedValue);
        Search(Convert.ToInt32(ViewState["CurrentPage"]));
    }

    protected void ddlPageSizeTop_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPageSizeBottom.SelectedValue = PageRecordSize.ToString();
        Search(Convert.ToInt32(ViewState["CurrentPage"]));
    }

    #endregion 21.0 ddlPageSize Selected Index Changed Event

    #region 22.0 ClearControls
    private void ClearControls()
    {
        dtpFromDate.Text = String.Empty;
        dtpToDate.Text = String.Empty;
        Search(1);
    }

    #endregion 22.0 ClearControls
}
//{
//k
//#region 11.0 Variables

//String FormName = "ACC_LedgerList";
//static Int32 PageRecordSize = CV.PageRecordSize;//Size of record per page
//Int32 PageDisplaySize = CV.PageDisplaySize;
//Int32 DisplayIndex = CV.DisplayIndex;

//#endregion 11.0 Variables

//#region 12.0 Page Load Event
//protected void Page_Load(object sender, EventArgs e)
//{
//    #region 12.0 Check User Login

//    if (Session["UserID"] == null)
//        Response.Redirect(CV.LoginPageURL);

//    #endregion 12.0 Check User Login

//    if (!Page.IsPostBack)
//    {
//        #region 12.1 DropDown List Fill Section

//        FillDropDownList();

//        #endregion 12.1 DropDown List Fill Section

//        Search(1);

//        #region 12.2 Set Default Value

//        lblSearchHeader.Text = CV.SearchHeaderText;
//        lblSearchResultHeader.Text = CV.SearchResultHeaderText;
//        upr.DisplayAfter = CV.UpdateProgressDisplayAfter;

//        #endregion 12.2 Set Default Value

//        #region 12.3 Set Help Text
//        ucHelp.ShowHelp("Help Text will be shown here");
//        #endregion 12.3 Set Help Text
//    }
//}

//#endregion 12.0 Page Load Event

//#region 13.0 FillLabels

//private void FillLabels(String FormName)
//{
//}

//#endregion

//#region 14.0 DropDownList

//#region 14.1 Fill DropDownList

//private void FillDropDownList()
//{
//    CommonFunctions.GetDropDownPageSize(ddlPageSizeBottom);
//    ddlPageSizeBottom.SelectedValue = PageRecordSize.ToString();
//}

//#endregion 14.1 Fill DropDownList

//#endregion 14.0 DropDownList

//#region 15.0 Search

//#region 15.1 Button Search Click Event

//protected void btnSearch_Click(object sender, EventArgs e)
//{
//    Search(1);
//}

//#endregion 15.1 Button Search Click Event

//#region 15.2 Search Function

//private void Search(int PageNo)
//{
//    #region Parameters

//    SqlDateTime FromDate = SqlDateTime.Null;
//    SqlDateTime ToDate = SqlDateTime.Null;
//    SqlDecimal TotalBalance = 0;
//    Int32 Offset = (PageNo - 1) * PageRecordSize;
//    Int32 TotalRecords = 0;
//    Int32 TotalPages = 1;

//    #endregion Parameters

//    #region Gather Data

//    if (dtpFromDate.Text.Trim() != String.Empty)
//        FromDate = Convert.ToDateTime(dtpFromDate.Text.Trim());

//    if (dtpToDate.Text.Trim() != String.Empty)
//        ToDate = Convert.ToDateTime(dtpToDate.Text.Trim());

//    #endregion Gather Data

//    ACC_LedgerBAL balACC_Ledger = new ACC_LedgerBAL();

//    DataTable dt = balACC_Ledger.SelectPage(Offset, PageRecordSize, out TotalRecords, FromDate, ToDate);


//    foreach (DataRow row in dt.Rows)
//    {
//        if (row.Field<String>("LedgerType") == "Income")
//            TotalBalance += row.Field<decimal>("LedgerAmount");
//        else
//            TotalBalance -= row.Field<decimal>("LedgerAmount");
//    }

//    if (PageRecordSize == 0 && dt.Rows.Count > 0)
//    {
//        PageRecordSize = dt.Rows.Count;
//        TotalPages = (int)Math.Ceiling((double)((decimal)TotalRecords / Convert.ToDecimal(PageRecordSize)));
//    }
//    else
//        TotalPages = (int)Math.Ceiling((double)((decimal)TotalRecords / Convert.ToDecimal(PageRecordSize)));

//    if (dt != null && dt.Rows.Count > 0)
//    {
//        Div_SearchResult.Visible = true;
//        rpData.DataSource = dt;
//        rpData.DataBind();
//        txtTotalBalance.Text = TotalBalance.ToString();
//        if (TotalBalance >= 0)
//        {
//            txtTotalBalance.Style["color"] = "green";
//        }
//        else
//        {
//            txtTotalBalance.Style["color"] = "red";
//        }

//        if (PageNo > TotalPages)
//            PageNo = TotalPages;

//        ViewState["TotalPages"] = TotalPages;
//        ViewState["CurrentPage"] = PageNo;

//        CommonFunctions.BindPageList(TotalPages, TotalRecords, PageNo, PageDisplaySize, DisplayIndex, rpPagination, liPrevious, lbtnPrevious, liFirstPage, lbtnFirstPage, liNext, lbtnNext, liLastPage, lbtnLastPage);

//        lblRecordInfoBottom.Text = CommonMessage.PageDisplayMessage(Offset, dt.Rows.Count, TotalRecords, PageNo, TotalPages);
//        lblRecordInfoTop.Text = CommonMessage.PageDisplayMessage(Offset, dt.Rows.Count, TotalRecords, PageNo, TotalPages);


//        if (TotalRecords <= CV.SmallestPageSize)
//        {
//            Div_Pagination.Visible = false;
//            Div_GoToPageNo.Visible = false;
//            Div_PageSize.Visible = false;
//        }
//        else
//        {
//            Div_Pagination.Visible = true;
//            Div_GoToPageNo.Visible = true;
//            Div_PageSize.Visible = true;
//        }
//    }

//    else if (TotalPages < PageNo && TotalPages > 0)
//        Search(TotalPages);

//    else
//    {
//        Div_SearchResult.Visible = false;

//        ViewState["TotalPages"] = 0;
//        ViewState["CurrentPage"] = 1;

//        rpData.DataSource = null;
//        rpData.DataBind();

//        lblRecordInfoBottom.Text = CommonMessage.NoRecordFound();
//        lblRecordInfoTop.Text = CommonMessage.NoRecordFound();

//        CommonFunctions.BindPageList(0, 0, PageNo, PageDisplaySize, DisplayIndex, rpPagination, liPrevious, lbtnPrevious, liFirstPage, lbtnFirstPage, liNext, lbtnNext, liLastPage, lbtnLastPage);

//        ucMessage.ShowError(CommonMessage.NoRecordFound());
//    }
//}

//#endregion 15.2 Search Function

//#endregion 15.0 Search

//#region 16.0 Repeater Events

//#region 16.1 Item Command Event

//protected void rpData_ItemCommand(object source, RepeaterCommandEventArgs e)
//{

//}

//#endregion 16.1 Item Command Event

//#endregion 16.0 Repeater Events

//#region 17.0 Pagination

//#region 17.1 Pagination Events

//#region ItemDataBound Event

//protected void rpPagination_ItemDataBound(object sender, RepeaterItemEventArgs e)
//{
//    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
//    {
//        LinkButton lb = (LinkButton)e.Item.FindControl("lbtnPageNo");
//        HtmlGenericControl hgc = (HtmlGenericControl)e.Item.FindControl("liPageNo");
//        if (Convert.ToInt32(ViewState["CurrentPage"]) == Convert.ToInt32(lb.CommandArgument))
//        {
//            hgc.Attributes["class"] = CSSClass.PaginationButtonActive;
//            lb.Enabled = false;
//        }
//        else
//            hgc.Attributes["class"] = CSSClass.PaginationButton;
//    }
//}

//#endregion ItemDataBound Event

//#region PageChange Event

//protected void PageChange_Click(object sender, EventArgs e)
//{
//    LinkButton lbtn = (LinkButton)(sender);
//    int Value = Convert.ToInt32(lbtn.CommandArgument);
//    String Name = lbtn.CommandName.ToString();

//    if (Name == "PageNo" || Name == "FirstPage")
//        Search(Value);

//    else if (Name == "PreviousPage")
//        Search(Convert.ToInt32(ViewState["CurrentPage"]) - Value);

//    else if (Name == "NextPage")
//        Search(Convert.ToInt32(ViewState["CurrentPage"]) + Value);

//    else if (Name == "LastPage")
//        Search(Convert.ToInt32(ViewState["TotalPages"]));

//    else if (Name == "GoPageNo")
//    {
//        if (txtPageNo.Text.Trim() == String.Empty)
//        {
//            ucMessage.ShowError(CommonMessage.ErrorRequiredField("Page No"));
//            return;
//        }
//        else
//        {
//            Value = Convert.ToInt32(txtPageNo.Text);
//            if (Value > Convert.ToInt32(ViewState["TotalPages"]))
//            {
//                ucMessage.ShowError(CommonMessage.ErrorInvalidField("Page No"));
//                return;
//            }
//            Search(Value);
//        }
//    }
//}

//#endregion PageChange Event

//#endregion 17.1 Pagination Events

//#endregion 17.0 Pagination

//#region 18.0 Button Delete Click Event


//#endregion 18.0 Button Delete Click Event

//#region 19.0 Export Data

//#region 19.1 Excel Export Button Click Event

//protected void lbtnExport_Click(object sender, EventArgs e)
//{
//    //LinkButton lbtn = (LinkButton)(sender);
//    //String ExportType = lbtn.CommandArgument.ToString();

//    //int TotalReceivedRecord = 0;

//    //SqlInt32 ExpenseTypeID = SqlInt32.Null;
//    //SqlDecimal Amount = SqlDecimal.Null;
//    //SqlDateTime ExpenseDate = SqlDateTime.Null;
//    //SqlInt32 HospitalID = SqlInt32.Null;
//    //SqlInt32 FinYearID = SqlInt32.Null;
//    //SqlString TagName = SqlString.Null;

//    //if (ddlExpenseTypeID.SelectedIndex > 0)
//    //    ExpenseTypeID = Convert.ToInt32(ddlExpenseTypeID.SelectedValue);

//    //if (txtAmount.Text.Trim() != String.Empty)
//    //    Amount = Convert.ToDecimal(txtAmount.Text.Trim());

//    //if (dtpExpenseDate.Text.Trim() != String.Empty)
//    //    ExpenseDate = Convert.ToDateTime(dtpExpenseDate.Text.Trim());

//    //if (ddlHospitalID.SelectedIndex > 0)
//    //    HospitalID = Convert.ToInt32(ddlHospitalID.SelectedValue);

//    //if (ddlFinYearID.SelectedIndex > 0)
//    //    FinYearID = Convert.ToInt32(ddlFinYearID.SelectedValue);

//    //if (dtpTagName.Text.Trim() != String.Empty)
//    //    TagName = Convert.ToString(dtpTagName.Text.Trim());


//    //Int32 Offset = 0;

//    //if (ViewState["CurrentPage"] != null)
//    //    Offset = (Convert.ToInt32(ViewState["CurrentPage"]) - 1) * PageRecordSize;

//    //ACC_ExpenseBAL balACC_Expense = new ACC_ExpenseBAL();
//    //DataTable dtACC_Expense = balACC_Expense.SelectPage(Offset, PageRecordSize, out TotalReceivedRecord, ExpenseTypeID, Amount, ExpenseDate, HospitalID, FinYearID, TagName);
//    //if (dtACC_Expense != null && dtACC_Expense.Rows.Count > 0)
//    //{
//    //    Session["ExportTable"] = dtACC_Expense;
//    //    Response.Redirect("~/Default/Export.aspx?ExportType=" + ExportType);
//    //}
//}

//#endregion 19.1 Excel Export Button Click Event

//#endregion 19.0 Export Data

//#region 20.0 Cancel Button Event

//protected void btnClear_Click(object sender, EventArgs e)
//{
//    ClearControls();
//}

//#endregion 20.0 Cancel Button Event

//#region 21.0 ddlPageSize Selected Index Changed Event

//protected void ddlPageSizeBottom_SelectedIndexChanged(object sender, EventArgs e)
//{
//    PageRecordSize = Convert.ToInt32(ddlPageSizeBottom.SelectedValue);
//    Search(Convert.ToInt32(ViewState["CurrentPage"]));
//}

//protected void ddlPageSizeTop_SelectedIndexChanged(object sender, EventArgs e)
//{
//    ddlPageSizeBottom.SelectedValue = PageRecordSize.ToString();
//    Search(Convert.ToInt32(ViewState["CurrentPage"]));
//}

//#endregion 21.0 ddlPageSize Selected Index Changed Event

//#region 22.0 ClearControls

//private void ClearControls()
//{
//    dtpFromDate.Text = String.Empty;
//    dtpToDate.Text = String.Empty;
//    CommonFunctions.BindEmptyRepeater(rpData);
//    Div_SearchResult.Visible = false;
//    lblRecordInfoBottom.Text = CommonMessage.NoRecordFound();
//    lblRecordInfoTop.Text = CommonMessage.NoRecordFound();
//}

//#endregion 22.0 ClearControls

//#region 23.0 Fill Finyear Dropdown From Hopital
//protected void ddlHospitalID_SelectedIndexChanged1(object sender, EventArgs e)
//{
//    //if (ddlHospitalID.SelectedIndex > 0)
//    //{
//    //    ddlExpenseTypeID.SelectedIndex = 0;
//    //    SqlInt32 HospitalID = SqlInt32.Null;

//    //    HospitalID = Convert.ToInt32(ddlHospitalID.SelectedValue);
//    //    CommonFillMethods.FillDropDownListExpenseFinYearIDByHospitalID(ddlFinYearID, HospitalID);

//    //}
//    //else
//    //{
//    //    ddlFinYearID.Items.Clear();
//    //    ddlFinYearID.Items.Insert(0, new ListItem("Select Fin Year", "-99"));
//    //    ddlExpenseTypeID.Items.Clear();
//    //    ddlExpenseTypeID.Items.Insert(0, new ListItem("Select Expense Type", "-99"));

//    //}
//}
//#endregion 23.0 Fill Finyear Dropdown From Hopital

//#region 24.0 Fill ExpenseType Dropdown From Finyear
//protected void ddlFinYearID_SelectedIndexChanged1(object sender, EventArgs e)
//{
//    //if (ddlFinYearID.SelectedIndex > 0)
//    //{
//    //    SqlInt32 FinYearID = SqlInt32.Null;
//    //    SqlInt32 HospitalID = SqlInt32.Null;

//    //    FinYearID = Convert.ToInt32(ddlFinYearID.SelectedValue);
//    //    HospitalID = Convert.ToInt32(ddlHospitalID.SelectedValue);
//    //    CommonFillMethods.FillDropDownListExpenseTypeIDByFinYearID(ddlExpenseTypeID, FinYearID, HospitalID);

//    //}
//    //else
//    //{
//    //    ddlExpenseTypeID.Items.Clear();
//    //    ddlExpenseTypeID.Items.Insert(0, new ListItem("Select Income Type", "-99"));
//    //}
//}
//#endregion 24.0 Fill ExpenseType Dropdown From Finyear





//<%@ Page Title = "" Language="C#" MasterPageFile="~/Default/MasterPage.master" AutoEventWireup="true" CodeFile="ACC_ExpInm_LedgerList.aspx.cs" Inherits="AdminPanel_Account_ACC_ExpInm_Ledger_ACC_ExpInm_LedgerList" %>

//<asp:Content ID = "Content1" ContentPlaceHolderID="head" runat="Server">
//</asp:Content>
//<asp:Content ID = "Content2" ContentPlaceHolderID="cphPageHeader" runat="Server">
//    <asp:Label ID = "lblPageHeader_XXXXX" runat="server" Text="Ledger"></asp:Label>
//    <small>
//        <asp:Label ID = "lblPageHeaderInfo_XXXXX" runat="server" Text="List"></asp:Label></small>
//    <span class="pull-right">
//        <small>
//            <asp:HyperLink ID = "hlShowHelp" SkinID="hlShowHelp" runat="server"></asp:HyperLink>
//        </small>
//    </span>
//</asp:Content>
//<asp:Content ID = "Content3" ContentPlaceHolderID="cphBreadcrumb" runat="Server">
//    <li>
//        <i class="fa fa-home"></i>
//        <asp:HyperLink ID = "hlHome" runat="server" NavigateUrl="~/AdminPanel/Default.aspx" Text="Home"></asp:HyperLink>
//        <i class="fa fa-angle-right"></i>
//    </li>
//    <li class="active">
//        <asp:Label ID = "lblBreadCrumbLast" runat="server" Text="Expense"></asp:Label>
//    </li>
//</asp:Content>
//<asp:Content ID = "Content4" ContentPlaceHolderID="cphPageContent" runat="Server">
//    <!--Help Text-->
//    <ucHelp:ShowHelp ID = "ucHelp" runat="server" />
//    <!--Help Text End-->
//    <asp:ScriptManager ID = "sm" runat="server">
//    </asp:ScriptManager>

//    <%-- Search --%>
//    <asp:UpdatePanel ID = "upApplicationFeature" runat="server">
//        <ContentTemplate>
//            <div class="portlet light">
//                <div class="portlet-title">
//                    <div class="caption">
//                        <asp:Label SkinID = "lblSearchHeaderIcon" runat="server"></asp:Label>
//                        <asp:Label ID = "lblSearchHeader" SkinID="lblSearchHeaderText" runat="server"></asp:Label>
//                    </div>
//                    <div class="tools">
//                        <a href = "javascript:;" class="collapse pull-right"></a>
//                    </div>
//                </div>
//                <div class="portlet-body form">
//                    <div role = "form" >
//                        < div class="form-body">
//                            <div class="row">

//                                <div class="col-md-4">
//                                    <div class="form-group">
//                                        <div class="input-group date date-picker" data-date-format="dd-mm-yyyy">
//                                            <span class="input-group-btn">
//                                                <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
//                                            </span>
//                                            <asp:TextBox ID = "dtpFromDate" CssClass="form-control" runat="server" placeholder="From Date"></asp:TextBox>
//                                        </div>
//                                    </div>
//                                </div>

//                                <div class="col-md-4">
//                                    <div class="form-group">
//                                        <div class="input-group date date-picker" data-date-format="dd-mm-yyyy">
//                                            <span class="input-group-btn">
//                                                <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
//                                            </span>
//                                            <asp:TextBox ID = "dtpToDate" CssClass="form-control" runat="server" placeholder="To Date"></asp:TextBox>
//                                        </div>
//                                    </div>
//                                </div>
//                            </div>

//                            <div class="form-actions">
//                                <div class="row">
//                                    <div class="col-md-9">
//                                        <asp:Button ID = "btnSearch" SkinID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
//                                        <asp:Button ID = "btnClear" runat="server" SkinID="btnClear" Text="Clear" OnClick="btnClear_Click" />
//                                    </div>
//                                </div>
//                            </div>
//                        </div>
//                    </div>
//                </div>
//        </ContentTemplate>
//    </asp:UpdatePanel>
//    <%-- End Search --%>

//    <%-- List --%>
//    <asp:UpdatePanel ID = "upList" runat="server" UpdateMode="Conditional">
//        <ContentTemplate>
//            <div class="row">
//                <div class="col-md-12">
//                    <ucMessage:ShowMessage ID = "ucMessage" runat="server" ViewStateMode="Disabled" />
//                </div>
//            </div>
//            <div class="row">
//                <div class="col-md-12">
//                    <!-- BEGIN EXAMPLE TABLE PORTLET-->
//                    <div class="portlet light">
//                        <div class="portlet-title">
//                            <div class="caption">
//                                <asp:Label SkinID = "lblSearchResultHeaderIcon" runat="server"></asp:Label>
//                                <asp:Label ID = "lblSearchResultHeader" SkinID="lblSearchResultHeaderText" runat="server"></asp:Label>
//                                <label class="control-label">&nbsp;</label>
//                                <label class="control-label pull-right">
//                                    <asp:Label ID = "lblRecordInfoTop" Text="No entries found" CssClass="pull-right" runat="server"></asp:Label>
//                                </label>
//                            </div>
//                        </div>
//                        <div class="portlet-body">
//                            <div class="row" runat="server" id="Div_SearchResult" visible="false">
//                                <div class="col-md-12">
//                                    <div id = "TableContent" >
//                                        < table class="table table-bordered table-advanced table-striped table-hover" id="sample_1">
//                                            <%-- Table Header --%>
//                                            <thead>
//                                                <tr class="TRDark row">
//                                                    <th class="text-center col-4">
//                                                        <asp:Label ID = "lbhDate" runat="server" Text="Date"></asp:Label>
//                                                    </th>
//                                                    <th class="text-center col-4">
//                                                        <asp:Label ID = "lbhType" runat="server" Text="Type (Income/Expense)"></asp:Label>
//                                                    </th>
//                                                    <th class="text-left col-4">
//                                                        <asp:Label ID = "lbhAmount" runat="server" Text="Amount"></asp:Label>
//                                                    </th>
//                                                    <th class="text-right col-4">
//                                                        <asp:Label ID = "lbhNote" runat="server" Text="Note"></asp:Label>
//                                                    </th>
//                                                </tr>
//                                            </thead>
//                                            <%-- END Table Header --%>

//                                            <tbody>
//                                                <asp:Repeater ID = "rpData" runat="server" OnItemCommand="rpData_ItemCommand">
//                                                    <ItemTemplate>
//                                                        <%-- Table Rows --%>
//                                                        <tr class="odd gradeX row" style='<%# Eval("LedgerType").ToString() == "Income" ? "background-color: lightgreen;" : "background-color: red;" %>'>
//                                                           <td class="text-center col-4">
//                                                                <%#Eval("LedgerDate", GNForm3C.CV.DefaultDateFormatForGrid) %>
//                                                            </td>
//                                                              <td class="text-center col-4">
//                                                                <%#Eval("LedgerType") %>
//                                                            </td>
//                                                            <td class="text-left col-4">
//                                                                <%#Eval("LedgerAmount") %>
//                                                            </td>
//                                                            <td class="text-right col-4">
//                                                                <%#Eval("LedgerNote") %>
//                                                            </td>
//                                                        </tr>
//                                                        <%-- END Table Rows --%>
//                                                    </ItemTemplate>
//                                                </asp:Repeater>
//                                                <tr class="row text-right">
//                                                    <td class="col-8" colspan="2">
//                                                        <h6>Total Balance</h6>
//                                                    </td>
//                                                    <td class="col-4">
//                                                        <asp:Label ID = "txtTotalBalance" runat="server"></asp:Label>
//                                                    </td>
//                                                </tr>
//                                            </tbody>

//                                        </table>
//                                    </div>

//                                    <%-- Pagination --%>
//                                    <div class="row">
//                                        <div class="col-md-4">
//                                            <label class="control-label">
//                                                <asp:Label ID = "lblRecordInfoBottom" Text="No entries found" runat="server"></asp:Label></label>
//                                        </div>
//                                        <div class="col-md-5">
//                                            <div class="dataTables_paginate paging_simple_numbers" runat="server" id="Div_Pagination">
//                                                <ul class="pagination">
//                                                    <li class="paginate_button previous disabled" id="liFirstPage" runat="server">
//                                                        <asp:LinkButton ID = "lbtnFirstPage" Enabled="false" OnClick="PageChange_Click" CommandName="FirstPage" CommandArgument="1" runat="server"><i class="fa fa-angle-double-left"></i></asp:LinkButton></li>
//                                                    <li class="paginate_button previous disabled" id="liPrevious" runat="server">
//                                                        <asp:LinkButton ID = "lbtnPrevious" Enabled="false" OnClick="PageChange_Click" CommandArgument="1" CommandName="PreviousPage" runat="server"><i class="fa fa-angle-left"></i></asp:LinkButton></li>
//                                                    <asp:Repeater ID = "rpPagination" runat="server" OnItemDataBound="rpPagination_ItemDataBound">
//                                                        <ItemTemplate>
//                                                            <li>
//                                                                <li class="paginate_button" id="liPageNo" runat="server">
//                                                                    <asp:LinkButton ID = "lbtnPageNo" runat="server" OnClick="PageChange_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PageNo")%>' CommandName="PageNo"><%# DataBinder.Eval(Container.DataItem, "PageNo")%></asp:LinkButton></li>
//                                                            </li>
//                                                        </ItemTemplate>
//                                                    </asp:Repeater>
//                                                    <li class="paginate_button next disabled" id="liNext" runat="server">
//                                                        <asp:LinkButton ID = "lbtnNext" Enabled="false" OnClick="PageChange_Click" CommandArgument="1" CommandName="NextPage" runat="server"><i class="fa fa-angle-right"></i></asp:LinkButton></li>
//                                                    <li class="paginate_button previous disabled" id="liLastPage" runat="server">
//                                                        <asp:LinkButton ID = "lbtnLastPage" Enabled="false" OnClick="PageChange_Click" CommandName="LastPage" CommandArgument="-99" runat="server"><i class="fa fa-angle-double-right"></i></asp:LinkButton></li>
//                                                </ul>
//                                            </div>
//                                        </div>
//                                        <div class="col-md-3 pull-right">
//                                            <div class="btn-group" runat="server" id="Div_GoToPageNo">
//                                                <asp:TextBox ID = "txtPageNo" placeholder="Page No" onkeypress="return IsNumeric(event)" runat="server" CssClass="pagination-panel-input form-control input-xsmall input-inline col-md-4" MaxLength="9"></asp:TextBox>
//                                                <asp:LinkButton ID = "lbtnGoToPageNo" runat="server" CssClass="btn btn-default input-inline col-md-4" CommandName="GoPageNo" CommandArgument="0" OnClick="PageChange_Click">Go</asp:LinkButton>
//                                            </div>
//                                            <div class="btn-group pull-right" runat="server" id="Div_PageSize">
//                                                <label class="control-label">Page Size</label>
//                                                <asp:DropDownList ID = "ddlPageSizeBottom" AutoPostBack= "true" CssClass= "form-control input-xsmall" runat= "server" OnSelectedIndexChanged= "ddlPageSizeBottom_SelectedIndexChanged" >
//                                                </ asp:DropDownList>
//                                            </div>
//                                        </div>
//                                    </div>
//                                    <div class="row">
//                                    </div>
//                                    <%-- END Pagination --%>
//                                </div>
//                            </div>
//                        </div>
//                    </div>
//                    <!-- END EXAMPLE TABLE PORTLET-->
//                </div>
//            </div>
//        </ContentTemplate>
//        <Triggers>
//            <asp:AsyncPostBackTrigger ControlID = "btnSearch" EventName="Click" />
//            <asp:AsyncPostBackTrigger ControlID = "btnClear" EventName="Click" />
//        </Triggers>
//    </asp:UpdatePanel>
//    <%-- END List --%>

//    <%-- Loading  --%>
//    <asp:UpdateProgress ID = "upr" runat="server">
//        <ProgressTemplate>
//            <div class="divWaiting">
//                <asp:Label ID = "lblWait" runat="server" Text=" Please wait... " />
//                <asp:Image ID = "imgWait" runat="server" SkinID="UpdatePanelLoding" />
//            </div>
//        </ProgressTemplate>
//    </asp:UpdateProgress>
//    <%-- END Loading  --%>
//</asp:Content>
//<asp:Content ID = "Content5" ContentPlaceHolderID="cphScripts" runat="Server">
//    <script>

//       <%-- $(window).keydown(function (e) {
//        GNWebKeyEvents(e.keyCode, '<%=hlAddNew.ClientID%>', '<%=btnSearch.ClientID%>');
//    });--%>

//        SearchGridUI('<%=btnSearch.ClientID%>', 'sample_1', 1);
//    </script>
//</asp:Content>
//}