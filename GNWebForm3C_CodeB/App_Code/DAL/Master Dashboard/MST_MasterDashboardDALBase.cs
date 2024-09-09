using GNForm3C.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using GNForm3C.ENT;
using System.Data.SqlTypes;

/// <summary>
/// Summary description for MST_MasterDashboardDALBase
/// </summary>
public class MST_MasterDashboardDALBase : DataBaseConfig
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
    public MST_MasterDashboardDALBase()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Select Count
    public DataTable SelectMSTCount(int HospitalID)
    {
        try
        {
            SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_DSB_Count");
            sqlDB.AddInParameter(dbCMD, "@HospitalID", SqlDbType.Int, HospitalID);

            DataTable dtCount = new DataTable("PR_MST_DSB_Count");

            DataBaseHelper DBH = new DataBaseHelper();
            DBH.LoadDataTable(sqlDB, dbCMD, dtCount);

            return dtCount;
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

    #endregion Select

    #region Select Income
    public DataTable SelectMST_DSB_Income(SqlInt32 HospitalID)
    {
        try
        {
            SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_DSB_Income");

            sqlDB.AddInParameter(dbCMD, "@HospitalID", SqlDbType.Int, HospitalID);

            DataTable dtMST_Income = new DataTable("PR_MST_DSB_Income");

            DataBaseHelper DBH = new DataBaseHelper();
            DBH.LoadDataTable(sqlDB, dbCMD, dtMST_Income);

            return dtMST_Income;
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
    #endregion

    #region Select Expense
    public DataTable SelectMST_DSB_Expense(SqlInt32 HospitalID)
    {
        try
        {
            SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_DSB_Expense");

            sqlDB.AddInParameter(dbCMD, "@HospitalID", SqlDbType.Int, HospitalID);

            DataTable dtMST_Income = new DataTable("PR_MST_DSB_Expense");

            DataBaseHelper DBH = new DataBaseHelper();
            DBH.LoadDataTable(sqlDB, dbCMD, dtMST_Income);

            return dtMST_Income;
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
    #endregion

    #region Treatment Summary
    public DataTable SelectMST_DSB_TreatmentSummary(SqlInt32 HospitalID)
    {
        try
        {
            SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_DSB_Treatment");

            sqlDB.AddInParameter(dbCMD, "@HospitalID", SqlDbType.Int, HospitalID);

            DataTable dtMST_TreatmentSummary = new DataTable("PR_MST_DSB_Treatment");

            DataBaseHelper DBH = new DataBaseHelper();
            DBH.LoadDataTable(sqlDB, dbCMD, dtMST_TreatmentSummary);

            return dtMST_TreatmentSummary;
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

    #endregion

    #region Chart
    public DataTable IncomeExpenseSumHospitalWise(SqlInt32 FinYearID)
    {
        try
        {
            SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_DSB_IncomeExpenseSumHospitalWise");
            //DbCommand dbCMD = sqlDB.GetStoredProcCommand("GetIncomePivot");

            DataTable dtCount = new DataTable("PR_MST_DSB_IncomeExpenseSumHospitalWise");
            sqlDB.AddInParameter(dbCMD, "@FinYearID", SqlDbType.Int, FinYearID);

            DataBaseHelper DBH = new DataBaseHelper();
            DBH.LoadDataTable(sqlDB, dbCMD, dtCount);

            return dtCount;
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
    #endregion
}