using GNForm3C.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Linq;
using System.Web;
using iTextSharp.text;

/// <summary>
/// Summary description for IncomeDataTreeDALBase
/// </summary>
public class IncomeDataTreeDALBase : DataBaseConfig
{
    #region Private Fields

    private string _Message;

    #endregion Private Fields

    #region Public Properties

    public string Message
    {
        get
        {
            return _Message;
        }
        set
        {
            _Message = value;
        }
    }

    #endregion Public Properties

    public IncomeDataTreeDALBase()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Select Opration
    public DataTable IncomeDataTreeSelectPage(SqlInt32? HospitalID, SqlInt32? FinYearID)
    {
        try
        {
            SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_ACC_HospitalDataTree_SelectPage");
            sqlDB.AddInParameter(dbCMD, "@HospitalID", SqlDbType.Int, HospitalID);
            sqlDB.AddInParameter(dbCMD, "@FinYearID", SqlDbType.Int, FinYearID);

            DataTable dtIncomeDataTree = new DataTable("PR_ACC_HospitalDataTree_SelectPage");

            DataBaseHelper DBH = new DataBaseHelper();
            DBH.LoadDataTable(sqlDB, dbCMD, dtIncomeDataTree);

            return dtIncomeDataTree;
        }
        catch (SqlException sqlex)
        {
            Message = SQLDataExceptionMessage(sqlex);
            if (SQLDataExceptionHandler(sqlex))
                throw;
            return null;
        }
        catch (Exception ex)
        {
            Message = ExceptionMessage(ex);
            if (ExceptionHandler(ex))
                throw;
            return null;
        }
    }

    #endregion SelectOperation
}