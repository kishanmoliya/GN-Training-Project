﻿using GNForm3C;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GNForm3C.BAL;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Configuration;

public partial class AdminPanel_BranchIntake_BR_BranchIntake_BR_BranchIntakeList : System.Web.UI.Page
{
    #region Variables

    String FormName = "Branch Intake";
    static Int32 PageRecordSize = CV.PageRecordSize; // Size of record per page
    Int32 PageDisplaySize = CV.PageDisplaySize;
    Int32 DisplayIndex = CV.DisplayIndex;

    #endregion Variables

    #region Page Load event

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Check User Login

        if (Session["UserID"] == null)
            Response.Redirect(CV.LoginPageURL);

        #endregion Check User Login

        if (!Page.IsPostBack)
        {
            Search(1);

            #region Set Help Text
            //ucHelp.ShowHelp("Help Text will be shown here");
            #endregion Set Help Text
        }
    }

    #endregion Page Load event

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

        BR_BranchIntakeBAL balBR_BranchIntake = new BR_BranchIntakeBAL();
        DataTable dt = balBR_BranchIntake.GetBranchIntakeData();


        if (dt != null && dt.Rows.Count > 0)
        {

            rpAddmissionYearHead.DataSource = CommonFunctions.ColumnOfDataTable(dt);
            rpAddmissionYearHead.DataBind();
            rpIntakeData.DataSource = dt;
            rpIntakeData.DataBind();

        }
        else
        {

            ucMessage.ShowError(CommonMessage.NoRecordFound());
        }
    }

    #endregion 15.2 Search Function

    #region 15.3 rpIntake_ItemDataBound
    protected void rpIntake_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater rpAddmissionYearBody = (Repeater)e.Item.FindControl("rpAddmissionYearBody");
            DataRowView drv = (DataRowView)e.Item.DataItem;

            if (rpAddmissionYearBody != null && drv != null)
            {
                // Retrieve column names excluding the "Branch" column
                DataTable dt = drv.DataView.Table;
                List<string> yearColumns = CommonFunctions.ColumnOfDataTable(dt).GetRange(1, dt.Columns.Count - 1);

                // Create a data source with year and intake pairs for binding
                List<YearIntakePair> yearIntakePairs = new List<YearIntakePair>();
                foreach (string year in yearColumns)
                {
                    yearIntakePairs.Add(new YearIntakePair
                    {
                        Year = year,
                        Intake = drv[year].ToString()
                    });
                }

                rpAddmissionYearBody.DataSource = yearIntakePairs;
                rpAddmissionYearBody.DataBind();
            }
        }
    }

    // Helper class to store year and intake pairs
    public class YearIntakePair
    {
        public string Year { get; set; }
        public string Intake { get; set; }
    }


    protected string BindIntakeData(string year, object dataItem)
    {
        DataRowView rowView = dataItem as DataRowView;
        if (rowView != null && rowView.Row.Table.Columns.Contains(year))
        {
            return rowView[year].ToString();
        }
        return string.Empty;
    }

    #endregion 15.3 rpIntake_ItemDataBound

    #endregion 15.0 Search

    #region 16.0 MST_BranchIntake Add/Edit
    protected void btnSave_Click(object sender, EventArgs e)
    {
        BR_BranchIntakeBAL balBR_BranchIntake = new BR_BranchIntakeBAL();

        foreach (RepeaterItem item in rpIntakeData.Items)
        {
            Label lblBranch = (Label)item.FindControl("lblBranch");

            if (lblBranch != null)
            {
                string branch = lblBranch.Text;
                Repeater rpAddmissionYearBody = (Repeater)item.FindControl("rpAddmissionYearBody");

                if (rpAddmissionYearBody != null)
                {
                    Dictionary<int, int> yearIntakeData = new Dictionary<int, int>();

                    foreach (RepeaterItem yearItem in rpAddmissionYearBody.Items)
                    {
                        TextBox txtIntake = (TextBox)yearItem.FindControl("txtIntake");
                        Label lblYear = (Label)yearItem.FindControl("lblYear");

                        if (txtIntake != null && lblYear != null)
                        {
                            int intake;
                            int year;

                            if (int.TryParse(txtIntake.Text, out intake) && int.TryParse(lblYear.Text, out year))
                            {
                                yearIntakeData[year] = intake;
                            }
                        }
                    }

                    // Save the intake data for the branch
                    balBR_BranchIntake.SaveBranchIntakeData(branch, yearIntakeData);
                }
            }
        }

        // Refresh the data
        Search(1);
    }

    #endregion 16.0 MST_BranchIntake Add/Edit

    #region 17.0 Clear Button
    protected void btnClear_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in rpIntakeData.Items)
        {
            Repeater rpAddmissionYearBody = (Repeater)item.FindControl("rpAddmissionYearBody");

            if (rpAddmissionYearBody != null)
            {
                foreach (RepeaterItem yearItem in rpAddmissionYearBody.Items)
                {
                    TextBox txtIntake = (TextBox)yearItem.FindControl("txtIntake");

                    if (txtIntake != null)
                    {
                        txtIntake.Text = string.Empty;
                    }
                }
            }
        }
    }
    #endregion  17.0 Clear Button
    
}