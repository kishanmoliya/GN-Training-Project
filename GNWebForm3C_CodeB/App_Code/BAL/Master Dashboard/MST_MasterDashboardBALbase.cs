using GNForm3C.DAL;
using GNForm3C.ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MST_MasterDashboardBALbase
/// </summary>
public class MST_MasterDashboardBALbase
{
    public MST_MasterDashboardBALbase()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Select
  
    public DataTable SelectMSTCount(int HospitalID)
    {
        MST_MasterDashboardDAL dalMST_Count = new MST_MasterDashboardDAL();
        return dalMST_Count.SelectMSTCount(HospitalID);
    }

    public DataTable SelectMST_DSB_Income(SqlInt32 HospitalID)
    {
        MST_MasterDashboardDAL dalMST_Income = new MST_MasterDashboardDAL();
        return dalMST_Income.SelectMST_DSB_Income(HospitalID);
    }

    public DataTable SelectMST_DSB_Expense(SqlInt32 HospitalID)
    {
        MST_MasterDashboardDAL dalMST_Expense = new MST_MasterDashboardDAL();
        return dalMST_Expense.SelectMST_DSB_Expense(HospitalID);
    }

    public DataTable SelectMST_DSB_TreatmentSummary(SqlInt32 HospitalID)
    {
        MST_MasterDashboardDAL dalMST_TreatmentSummary = new MST_MasterDashboardDAL();
        return dalMST_TreatmentSummary.SelectMST_DSB_TreatmentSummary(HospitalID);
    }
    #endregion Select
}