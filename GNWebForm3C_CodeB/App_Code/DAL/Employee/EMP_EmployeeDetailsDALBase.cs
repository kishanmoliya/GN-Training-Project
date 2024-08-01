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
/// Summary description for EMP_EmployeeDetailsDALBase
/// </summary>
/// 

namespace GNForm3C.DAL
{
    public abstract class EMP_EmployeeDetailsDALBase : DataBaseConfig
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
        public EMP_EmployeeDetailsDALBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region DeleteOperation

        public Boolean Delete(SqlInt32 EmployeeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_EMP_EmployeeDetails_Delete");

                sqlDB.AddInParameter(dbCMD, "@EmployeeID", SqlDbType.Int, EmployeeID);

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

        #endregion DeleteOperation

        #region Select Operation

        public EMP_EmployeeDetailsENT SelectPK(SqlInt32 EmployeeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_EMP_EmployeeDetails_SelectPK");

                sqlDB.AddInParameter(dbCMD, "@EmployeeID", SqlDbType.Int, EmployeeID);

                EMP_EmployeeDetailsENT entEMP_EmployeeDetails = new EMP_EmployeeDetailsENT();
                DataBaseHelper DBH = new DataBaseHelper();
                using (IDataReader dr = DBH.ExecuteReader(sqlDB, dbCMD))
                {
                    while (dr.Read())
                    {
                        if (!dr["EmployeeID"].Equals(System.DBNull.Value))
                            entEMP_EmployeeDetails.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);

                        if (!dr["EmployeeName"].Equals(System.DBNull.Value))
                            entEMP_EmployeeDetails.EmployeeName = Convert.ToString(dr["EmployeeName"]);

                        if (!dr["EmployeeTypeID"].Equals(System.DBNull.Value))
                            entEMP_EmployeeDetails.EmployeeTypeID = Convert.ToInt32(dr["EmployeeTypeID"]);

                        if (!dr["Remark"].Equals(System.DBNull.Value))
                            entEMP_EmployeeDetails.Remark = Convert.ToString(dr["Remark"]);

                        if (!dr["UserID"].Equals(System.DBNull.Value))
                            entEMP_EmployeeDetails.UserID = Convert.ToInt32(dr["UserID"]);

                        if (!dr["Created"].Equals(System.DBNull.Value))
                            entEMP_EmployeeDetails.Created = Convert.ToDateTime(dr["Created"]);

                        if (!dr["Modified"].Equals(System.DBNull.Value))
                            entEMP_EmployeeDetails.Modified = Convert.ToDateTime(dr["Modified"]);

                    }
                }
                return entEMP_EmployeeDetails;
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

        public DataTable SelectView(SqlInt32 EmployeeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_EMP_EmployeeDetails_SelectView");

                sqlDB.AddInParameter(dbCMD, "@EmployeeID", SqlDbType.Int, EmployeeID);

                DataTable dtMST_ExpenseType = new DataTable("PR_EMP_EmployeeDetails_SelectView");

                DataBaseHelper DBH = new DataBaseHelper();
                DBH.LoadDataTable(sqlDB, dbCMD, dtMST_ExpenseType);

                return dtMST_ExpenseType;
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

        public DataTable SelectPage(SqlInt32 PageOffset, SqlInt32 PageSize, out Int32 TotalRecords, SqlString EmployeeName, SqlInt32 EmployeeTypeID)
        {
            TotalRecords = 0;
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_EMP_EmployeeDetails_SelectPage");
                sqlDB.AddInParameter(dbCMD, "@PageOffset", SqlDbType.Int, PageOffset);
                sqlDB.AddInParameter(dbCMD, "@PageSize", SqlDbType.Int, PageSize);
                sqlDB.AddInParameter(dbCMD, "@EmployeeName", SqlDbType.VarChar, EmployeeName);
                sqlDB.AddInParameter(dbCMD, "@EmployeeTypeID", SqlDbType.Int, EmployeeTypeID);
                sqlDB.AddOutParameter(dbCMD, "@TotalRecords", SqlDbType.Int, 4);

                DataTable dtEMP_EmployeeDetails = new DataTable("PR_EMP_EmployeeDetails_SelectPage");

                DataBaseHelper DBH = new DataBaseHelper();
                DBH.LoadDataTable(sqlDB, dbCMD, dtEMP_EmployeeDetails);

                TotalRecords = Convert.ToInt32(dbCMD.Parameters["@TotalRecords"].Value);

                return dtEMP_EmployeeDetails;
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

        public DataTable SelectShow(SqlInt32 EmployeeTypeID)
        {

            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_EMP_EmployeeDetails_SelectShow");
                sqlDB.AddInParameter(dbCMD, "@EmployeeTypeID", SqlDbType.Int, EmployeeTypeID);

                DataTable dtEMP_EmployeeDetailsShow = new DataTable("PR_EMP_EmployeeDetails_SelectShow");

                DataBaseHelper DBH = new DataBaseHelper();
                DBH.LoadDataTable(sqlDB, dbCMD, dtEMP_EmployeeDetailsShow);

                return dtEMP_EmployeeDetailsShow;
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

        #region UpdateOperation

        public Boolean Update(EMP_EmployeeDetailsENT entEMP_EmployeeDetails)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_EMP_EmployeeDetails_Update");

                sqlDB.AddInParameter(dbCMD, "@EmployeeID", SqlDbType.Int, entEMP_EmployeeDetails.EmployeeID);
                sqlDB.AddInParameter(dbCMD, "@EmployeeName", SqlDbType.NVarChar, entEMP_EmployeeDetails.EmployeeName);
                sqlDB.AddInParameter(dbCMD, "@EmployeeTypeID", SqlDbType.Int, entEMP_EmployeeDetails.EmployeeTypeID);
                sqlDB.AddInParameter(dbCMD, "@Remark", SqlDbType.NVarChar, entEMP_EmployeeDetails.Remark);
                sqlDB.AddInParameter(dbCMD, "@UserID", SqlDbType.Int, entEMP_EmployeeDetails.UserID);
                sqlDB.AddInParameter(dbCMD, "@Created", SqlDbType.DateTime, entEMP_EmployeeDetails.Created);
                sqlDB.AddInParameter(dbCMD, "@Modified", SqlDbType.DateTime, entEMP_EmployeeDetails.Modified);

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

        #region InsertOperation
        public Boolean Insert(EMP_EmployeeDetailsENT entEMP_EmployeeDetails)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_EMP_EmployeeDetails_Insert");

                sqlDB.AddOutParameter(dbCMD, "@EmployeeID", SqlDbType.Int, 4);
                sqlDB.AddInParameter(dbCMD, "@EmployeeName", SqlDbType.NVarChar, entEMP_EmployeeDetails.EmployeeName);
                sqlDB.AddInParameter(dbCMD, "@EmployeeTypeID", SqlDbType.Int, entEMP_EmployeeDetails.EmployeeTypeID);
                sqlDB.AddInParameter(dbCMD, "@Remark", SqlDbType.NVarChar, entEMP_EmployeeDetails.Remark);
                sqlDB.AddInParameter(dbCMD, "@UserID", SqlDbType.Int, entEMP_EmployeeDetails.UserID);
                sqlDB.AddInParameter(dbCMD, "@Created", SqlDbType.DateTime, entEMP_EmployeeDetails.Created);
                sqlDB.AddInParameter(dbCMD, "@Modified", SqlDbType.DateTime, entEMP_EmployeeDetails.Modified);

                DataBaseHelper DBH = new DataBaseHelper();
                DBH.ExecuteNonQuery(sqlDB, dbCMD);

                entEMP_EmployeeDetails.EmployeeID = (SqlInt32)Convert.ToInt32(dbCMD.Parameters["@EmployeeID"].Value);

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

        #endregion InsertOperation
    }
}