using GNForm3C;
using System;
using System.Data.SqlTypes;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class AdminPanel_DataTree_HospitalDataTree_IncomeDataTree : System.Web.UI.Page
{
    #region 11.0 Variables

    String FormName = "Income Tree";
    static Int32 PageRecordSize = CV.PageRecordSize;//Size of record per page
    Int32 PageDisplaySize = CV.PageDisplaySize;
    Int32 DisplayIndex = CV.DisplayIndex;

    public SqlInt32 Offset { get; private set; }

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

            Search(1);

            #region 12.2 Set Default Value

            lblSearchHeader.Text = CV.SearchHeaderText;
            lblSearchResultHeader.Text = CV.SearchResultHeaderText;
            upr.DisplayAfter = CV.UpdateProgressDisplayAfter;

            #endregion 12.2 Set Default Value

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

        SqlString Hospital = SqlString.Null;
        SqlString PrintName = SqlString.Null;
        SqlString PrintLine1 = SqlString.Null;
        SqlInt32 HospitalID = SqlInt32.Null;
        SqlInt32 FinYearID = SqlInt32.Null;
        #endregion Parameters

        #region Gather Data

        if (txtHospital.Text.Trim() != String.Empty)
            Hospital = txtHospital.Text.Trim();

        if (txtPrintName.Text.Trim() != String.Empty)
            PrintName = txtPrintName.Text.Trim();

        #endregion Gather Data

        IncomeDataTreeBAL balIncomeDataTree = new IncomeDataTreeBAL();

        DataTable dt = balIncomeDataTree.IncomeDataTreeSelectPage(HospitalID, FinYearID);

        if (dt != null && dt.Rows.Count > 0)
        {
            Div_SearchResult.Visible = true;
            rpData.DataSource = dt;
            rpData.DataBind();
        }
    }

    #endregion 15.2 Search Function

    #endregion 15.0 Search

    #region 18.0 Button Delete Click Event


    #endregion 18.0 Button Delete Click Event

    #region 20.0 Cancel Button Event

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    #endregion 20.0 Cancel Button Event

    #region 22.0 ClearControls

    private void ClearControls()
    {
        txtHospital.Text = String.Empty;
        txtPrintName.Text = String.Empty;
        CommonFunctions.BindEmptyRepeater(rpData);
        Div_SearchResult.Visible = false;
    }
    #endregion 22.0 ClearControls

    protected void rptHospitals_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "LoadFinYears")
        {
            int hospitalID = Convert.ToInt32(e.CommandArgument);

            // Find the nested repeater for Financial Years
            Repeater rptFinYears = (Repeater)e.Item.FindControl("rptFinYears");

            // Fetch Financial Years for the selected HospitalID
            IncomeDataTreeBAL aCC_IncomeListBAL = new IncomeDataTreeBAL();
            DataTable dtFinYears = aCC_IncomeListBAL.IncomeDataTreeSelectPage(hospitalID, null);

            // Bind the Financial Years data to the nested repeater
            rptFinYears.DataSource = dtFinYears;
            rptFinYears.DataBind();
        }
    }

    protected void rptFinYears_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "LoadIncomes")
        {
            int finYearID = Convert.ToInt32(e.CommandArgument);

            // Find the parent RepeaterItem of this financial year (to get HospitalID)
            RepeaterItem parentItem = (RepeaterItem)((Repeater)sender).NamingContainer;
            HiddenField hdnHospitalID = (HiddenField)parentItem.FindControl("hdnHospitalID");
            int hospitalID = Convert.ToInt32(hdnHospitalID.Value);

            // Find the nested repeater for Income
            Repeater rptIncomes = (Repeater)e.Item.FindControl("rptIncomes");

            // Fetch Incomes for the selected HospitalID and FinYearID
            IncomeDataTreeBAL aCC_IncomeListBAL = new IncomeDataTreeBAL();
            DataTable dtIncomes = aCC_IncomeListBAL.IncomeDataTreeSelectPage(hospitalID, finYearID);

            // Bind the Income data to the nested repeater
            rptIncomes.DataSource = dtIncomes;
            rptIncomes.DataBind();
        }
    }
}
}