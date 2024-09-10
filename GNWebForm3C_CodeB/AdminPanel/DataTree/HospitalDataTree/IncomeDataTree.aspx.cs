using GNForm3C;
using System;
using System.Data.SqlTypes;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class AdminPanel_DataTree_HospitalDataTree_IncomeDataTree : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadHospitals();
        }
    }

    private void LoadHospitals()
    {

        // Call the stored procedure without parameters to get all hospitals
        IncomeDataTreeBAL aCC_IncomeListBAL = new IncomeDataTreeBAL();
        DataTable dtHospitals = aCC_IncomeListBAL.IncomeDataTreeSelectPage(null, null);

        // Bind the hospital data to the repeater
        rptHospitals.DataSource = dtHospitals;
        rptHospitals.DataBind();
    }

    protected void rptHospitals_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "LoadFinYears")
        {

            Panel panelFinYears = (Panel)e.Item.FindControl("pnlFinYears");

            if (panelFinYears != null)
            {
                if (panelFinYears.Visible)
                {
                    panelFinYears.Visible = false;
                }
                else
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
                    panelFinYears.Visible = true;
                }
                // Set the Panel visibility to true

            }



        }
    }

    protected void rptFinYears_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "LoadIncomes")
        {
            Panel panelIncomes = (Panel)e.Item.FindControl("pnlIncomes");

            if (panelIncomes != null)
            {
                if (panelIncomes.Visible)
                {
                    panelIncomes.Visible = false;
                }
                else
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

                    panelIncomes.Visible = true;

                }
            }
        }
    }
}

