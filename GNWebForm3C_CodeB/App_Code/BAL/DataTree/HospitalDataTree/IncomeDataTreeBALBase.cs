using GNForm3C.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IncomeDataTreeBALBase
/// </summary>
public class IncomeDataTreeBALBase
{
    public IncomeDataTreeBALBase()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Select Operation
    public DataTable IncomeDataTreeSelectPage(SqlInt32? HospitalID, SqlInt32? FinYearID)
    {
        IncomeDataTreeDAL dalDataTree = new IncomeDataTreeDAL();
        return dalDataTree.IncomeDataTreeSelectPage(HospitalID, FinYearID);
    }
    #endregion Select Operation
}