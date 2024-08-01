using GNForm3C.BAL;
using GNForm3C;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Employee_EMP_EmployeeDetailsView : System.Web.UI.Page
{
    #region Page Load Event 

    protected void Page_Load(object sender, EventArgs e)
    {
        #region 10.1 Check User Login 

        if (Session["UserID"] == null)
            Response.Redirect(CV.LoginPageURL);

        #endregion 10.1 Check User Login 

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["EmployeeID"] != null)
            {
                FillControls();
            }
        }
    }

    #endregion

    #region FillControls
    private void FillControls()
    {
        if (Request.QueryString["EmployeeID"] != null)
        {
            EMP_EmployeeDetailsBAL balEMP_EmployeeDetails = new EMP_EmployeeDetailsBAL();
            DataTable dtEMP_EmployeeDetails = balEMP_EmployeeDetails.SelectView(CommonFunctions.DecryptBase64Int32(Request.QueryString["EmployeeID"]));
            if (dtEMP_EmployeeDetails != null)
            {
                foreach (DataRow dr in dtEMP_EmployeeDetails.Rows)
                {

                    if (!dr["EmployeeName"].Equals(DBNull.Value))
                        lblEmployeeName.Text = Convert.ToString(dr["EmployeeName"]);

                    if (!dr["EmployeeTypeName"].Equals(DBNull.Value))
                        lblEmployeeTypeName.Text = Convert.ToString(dr["EmployeeTypeName"]);

                    if (!dr["Remark"].Equals(DBNull.Value))
                        lblRemark.Text = Convert.ToString(dr["Remark"]);

                    if (!dr["UserName"].Equals(DBNull.Value))
                        lblUserID.Text = Convert.ToString(dr["UserName"]);

                    if (!dr["Created"].Equals(DBNull.Value))
                        lblCreated.Text = Convert.ToDateTime(dr["Created"]).ToString(CV.DefaultDateTimeFormat);

                    if (!dr["Modified"].Equals(DBNull.Value))
                        lblModified.Text = Convert.ToDateTime(dr["Modified"]).ToString(CV.DefaultDateTimeFormat);

                }
            }
        }
    }
    #endregion FillControls
}