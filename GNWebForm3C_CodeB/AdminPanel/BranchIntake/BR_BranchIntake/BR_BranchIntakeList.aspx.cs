using GNForm3C;
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

public partial class AdminPanel_BranchIntake_BR_BranchIntake_BR_BranchIntakeList : System.Web.UI.Page
{
    #region 11.0 Variables

    String FormName = "BR_BranchIntake";

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
            #region 12.2 Set Default Value

            upr.DisplayAfter = CV.UpdateProgressDisplayAfter;

            #endregion 12.2 Set Default Value

            #region 12.3 Set Help Text
            ucHelp.ShowHelp("Help Text will be shown here");
            #endregion 12.3 Set Help Text

            BindGridView();
        }
    }
    #endregion 12.0 Page Load Event

    private void BindGridView()
    {
        BR_BranchIntakeBAL balBR_BranchIntake = new BR_BranchIntakeBAL();

        DataTable dt = balBR_BranchIntake.SelectPage();

        if (dt.Columns.Count > 0)
        {
            // Create BoundField for each column except the first one (Branch)
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (i == 0)
                {
                    // Branch Column
                    BoundField branchField = new BoundField
                    {
                        DataField = dt.Columns[i].ColumnName,
                        HeaderText = dt.Columns[i].ColumnName
                    };
                    grdBranchIntake.Columns.Add(branchField);
                }
                else
                {
                    // Dynamic year columns with TextBox
                    TemplateField yearField = new TemplateField
                    {
                        HeaderText = dt.Columns[i].ColumnName,
                        ItemTemplate = new TextBoxTemplate(dt.Columns[i].ColumnName)
                    };
                    grdBranchIntake.Columns.Add(yearField);
                }
            }

            // Bind the data to the GridView
            grdBranchIntake.DataSource = dt;
            grdBranchIntake.DataBind();
        }

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdBranchIntake.Rows)
        {
            string branch = row.Cells[0].Text;

            for (int i = 1; i < row.Cells.Count; i++)
            {
                string yearColumn = grdBranchIntake.HeaderRow.Cells[i].Text;
                int year = int.Parse(yearColumn);

                // Use the custom method instead of FindControl
                TextBox txtIntake = FindTextBoxById(row.Cells[i], "txt" + yearColumn);

                if (txtIntake != null)
                {
                    int intake;
                    if (int.TryParse(txtIntake.Text, out intake))
                    {
                        BR_BranchIntakeBAL balBR_BranchIntake = new BR_BranchIntakeBAL();
                        balBR_BranchIntake.Update(branch, intake, year);
                    }
                }
                else
                {
                    // Debugging: Inspect the control hierarchy and IDs
                    // System.Diagnostics.Debug.WriteLine($"TextBox with ID txt{yearColumn} not found in cell {i}.");
                }
            }
        }

        // Re-bind the GridView to reflect updated data
        //BindGridView();
    }


    private TextBox FindTextBoxById(Control parent, string id)
    {
        foreach (Control child in parent.Controls)
        {
            if (child is TextBox && child.ID == id)
            {
                return (TextBox)child;
            }

            // Recursively search in child controls
            TextBox foundControl = FindTextBoxById(child, id);
            if (foundControl != null)
            {
                return foundControl;
            }
        }

        // Debugging: Log or inspect why control was not found
        // System.Diagnostics.Debug.WriteLine($"TextBox with ID {id} not found in control hierarchy.");
        return null;
    }



    protected void grdBranchIntake_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            for (int i = 1; i < drv.Row.ItemArray.Length; i++)
            {
                // Find the TextBox in the current cell
                TextBox txt = e.Row.Cells[i].FindControl("txt" + grdBranchIntake.HeaderRow.Cells[i].Text) as TextBox;
                if (txt != null)
                {
                    txt.Text = drv[i].ToString();
                }
            }
        }
    }

    // Custom TemplateField class to add TextBox in GridView
    public class TextBoxTemplate : ITemplate
    {
        private string columnName;

        public TextBoxTemplate(string colname)
        {
            columnName = colname;
        }

        public void InstantiateIn(Control container)
        {
            TextBox txt = new TextBox
            {
                ID = "txt" + columnName // Ensure this ID matches what's used in FindTextBoxById
            };
            container.Controls.Add(txt);
        }
    }


}