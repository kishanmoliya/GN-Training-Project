using GNForm3C.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RPT_ACC_IncomeListDALBase
/// </summary>
public class RPT_ACC_IncomeListDALBase : DataBaseConfig
{
    #region Properties

    private string _Message;
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

    #endregion Properties
    public RPT_ACC_IncomeListDALBase()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region RDLC (Reports)
    public DataTable Report_ACC_Income_ByFinYear()
    {
        try
        {
            SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PP_ACC_Income_ByFinyear");

            DataTable dtACC_Income = new DataTable("PP_ACC_Income_ByFinyear");

            DataBaseHelper DBH = new DataBaseHelper();
            DBH.LoadDataTable(sqlDB, dbCMD, dtACC_Income);

            return dtACC_Income;
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

    #endregion RDLC (Reports)
}