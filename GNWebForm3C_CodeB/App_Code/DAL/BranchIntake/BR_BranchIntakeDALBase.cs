using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Linq;
using System.Web;
using GNForm3C.ENT;

/// <summary>
/// Summary description for BR_BranchIntakeDALBase
/// </summary>
/// 
namespace GNForm3C.DAL
{
    public class BR_BranchIntakeDALBase : DataBaseConfig
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

        public BR_BranchIntakeDALBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Select Operation

        public DataTable SelectPage()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_BR_BranchIntake_SelectPage");

                DataTable dtBR_BranchIntake = new DataTable("PR_BR_BranchIntake_SelectPage");

                DataBaseHelper DBH = new DataBaseHelper();
                DBH.LoadDataTable(sqlDB, dbCMD, dtBR_BranchIntake);


                return dtBR_BranchIntake ;
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

        #endregion Select Operation


        #region UpdateOperation

        public Boolean Update(String Branch, int Intake, int Year)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_BR_BranchIntake_Update");

                sqlDB.AddInParameter(dbCMD, "@Intake", SqlDbType.Int, Intake);
                sqlDB.AddInParameter(dbCMD, "@Year", SqlDbType.Int, Year);
                sqlDB.AddInParameter(dbCMD, "@Branch", SqlDbType.NVarChar, Branch);
               
                DataBaseHelper DBH = new DataBaseHelper();
                DBH.ExecuteNonQuery(sqlDB, dbCMD);

                return true;
            }
            catch (SqlException sqlex)
            {
                Message = SQLDataExceptionMessage(sqlex);
                if (SQLDataExceptionHandler(sqlex))
                    throw;
                return false;
            }
            catch (Exception ex)
            {
                Message = ExceptionMessage(ex);
                if (ExceptionHandler(ex))
                    throw;
                return false;
            }
        }

        #endregion UpdateOperation

    }
}