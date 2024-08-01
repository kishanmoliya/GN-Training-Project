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
/// Summary description for STU_StudentDALBase
/// </summary>
/// 
namespace GNForm3C.DAL
{
    public class STU_StudentDALBase : DataBaseConfig
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
        public STU_StudentDALBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region DeleteOperation
        public Boolean Delete(SqlInt32 StudentID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_STU_Student_Delete");

                sqlDB.AddInParameter(dbCMD, "@StudentID", SqlDbType.Int, StudentID);

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
        public STU_StudentENT SelectPK(SqlInt32 StudentID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_STU_Student_SelectPK");

                sqlDB.AddInParameter(dbCMD, "@StudentID", SqlDbType.Int, StudentID);

                STU_StudentENT entSTU_StudentDetails = new STU_StudentENT();
                DataBaseHelper DBH = new DataBaseHelper();
                using (IDataReader dr = DBH.ExecuteReader(sqlDB, dbCMD))
                {
                    while (dr.Read())
                    {
                        if (!dr["StudentID"].Equals(System.DBNull.Value))
                            entSTU_StudentDetails.StudentID = Convert.ToInt32(dr["StudentID"]);

                        if (!dr["StudentName"].Equals(System.DBNull.Value))
                            entSTU_StudentDetails.StudentName = Convert.ToString(dr["StudentName"]);

                        if (!dr["EnrollmentNo"].Equals(System.DBNull.Value))
                            entSTU_StudentDetails.EnrollmentNo = Convert.ToString(dr["EnrollmentNo"]);

                        if (!dr["RollNo"].Equals(System.DBNull.Value))
                            entSTU_StudentDetails.RollNo = Convert.ToInt32(dr["RollNo"]);

                        if (!dr["CurrentSem"].Equals(System.DBNull.Value))
                            entSTU_StudentDetails.CurrentSem = Convert.ToInt32(dr["CurrentSem"]);

                        if (!dr["EmailInstitute"].Equals(System.DBNull.Value))
                            entSTU_StudentDetails.EmailInstitute = Convert.ToString(dr["EmailInstitute"]);

                        if (!dr["EmailPersonal"].Equals(System.DBNull.Value))
                            entSTU_StudentDetails.EmailPersonal = Convert.ToString(dr["EmailPersonal"]);

                        if (!dr["ContactNo"].Equals(System.DBNull.Value))
                            entSTU_StudentDetails.ContactNo = Convert.ToString(dr["ContactNo"]);

                        if (!dr["BirthDate"].Equals(System.DBNull.Value))
                            entSTU_StudentDetails.BirthDate = Convert.ToDateTime(dr["BirthDate"]);

                        if (!dr["Gender"].Equals(System.DBNull.Value))
                            entSTU_StudentDetails.Gender = Convert.ToString(dr["Gender"]);

                        if (!dr["UserID"].Equals(System.DBNull.Value))
                            entSTU_StudentDetails.UserID = Convert.ToInt32(dr["UserID"]);

                        if (!dr["Created"].Equals(System.DBNull.Value))
                            entSTU_StudentDetails.Created = Convert.ToDateTime(dr["Created"]);

                        if (!dr["Modified"].Equals(System.DBNull.Value))
                            entSTU_StudentDetails.Modified = Convert.ToDateTime(dr["Modified"]);

                    }
                }
                return entSTU_StudentDetails;
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

        public DataTable SelectPage(SqlInt32 PageOffset, SqlInt32 PageSize, out Int32 TotalRecords, SqlString StudentName, SqlString EnrollmentNo, SqlInt32 CurrentSem, SqlString EmailInstitute, SqlString EmailPersonal, SqlString Gender, SqlString ContactNo, SqlInt32 RollNo)
        {
            TotalRecords = 0;
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_STU_Student_SelectPage");
                sqlDB.AddInParameter(dbCMD, "@PageOffset", SqlDbType.Int, PageOffset);
                sqlDB.AddInParameter(dbCMD, "@PageSize", SqlDbType.Int, PageSize);
                sqlDB.AddInParameter(dbCMD, "@StudentName", SqlDbType.VarChar, StudentName);
                sqlDB.AddInParameter(dbCMD, "@EnrollmentNo", SqlDbType.VarChar, EnrollmentNo);
                sqlDB.AddInParameter(dbCMD, "@EmailInstitute", SqlDbType.VarChar, EmailInstitute);
                sqlDB.AddInParameter(dbCMD, "@EmailPersonal", SqlDbType.VarChar, EmailPersonal);
                sqlDB.AddInParameter(dbCMD, "@ContactNo", SqlDbType.VarChar, ContactNo);
                sqlDB.AddInParameter(dbCMD, "@Gender", SqlDbType.VarChar, Gender);
                sqlDB.AddInParameter(dbCMD, "@CurrentSem", SqlDbType.Int, CurrentSem);
                sqlDB.AddInParameter(dbCMD, "@RollNo", SqlDbType.Int, RollNo);
                sqlDB.AddOutParameter(dbCMD, "@TotalRecords", SqlDbType.Int, 4);

                DataTable dtSTU_StudentList = new DataTable("PR_STU_Student_SelectPage");

                DataBaseHelper DBH = new DataBaseHelper();
                DBH.LoadDataTable(sqlDB, dbCMD, dtSTU_StudentList);

                TotalRecords = Convert.ToInt32(dbCMD.Parameters["@TotalRecords"].Value);

                return dtSTU_StudentList;
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

        #region ComboBox
        public DataTable SelectComboBox()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_STU_StudentSemester_SelectComboBox");

                DataTable dtSTU_StudentSem = new DataTable("PR_STU_StudentSemester_SelectComboBox");

                DataBaseHelper DBH = new DataBaseHelper();
                DBH.LoadDataTable(sqlDB, dbCMD, dtSTU_StudentSem);

                return dtSTU_StudentSem;
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
        #endregion ComboBox


        #region UpdateOperation

        public Boolean Update(STU_StudentENT entSTU_StudentDetails)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_STU_Student_Update");

                sqlDB.AddInParameter(dbCMD, "@StudentID", SqlDbType.Int, entSTU_StudentDetails.StudentID);
                sqlDB.AddInParameter(dbCMD, "@StudentName", SqlDbType.VarChar, entSTU_StudentDetails.StudentName);
                sqlDB.AddInParameter(dbCMD, "@EnrollmentNo", SqlDbType.VarChar, entSTU_StudentDetails.EnrollmentNo);
                sqlDB.AddInParameter(dbCMD, "@RollNo", SqlDbType.Int, entSTU_StudentDetails.RollNo);
                sqlDB.AddInParameter(dbCMD, "@CurrentSem", SqlDbType.Int, entSTU_StudentDetails.CurrentSem);
                sqlDB.AddInParameter(dbCMD, "@EmailInstitute", SqlDbType.VarChar, entSTU_StudentDetails.EmailInstitute);
                sqlDB.AddInParameter(dbCMD, "@EmailPersonal", SqlDbType.VarChar, entSTU_StudentDetails.EmailPersonal);
                sqlDB.AddInParameter(dbCMD, "@BirthDate", SqlDbType.DateTime, entSTU_StudentDetails.BirthDate);
                sqlDB.AddInParameter(dbCMD, "@ContactNo", SqlDbType.VarChar, entSTU_StudentDetails.ContactNo);
                sqlDB.AddInParameter(dbCMD, "@Gender", SqlDbType.VarChar, entSTU_StudentDetails.Gender);
                sqlDB.AddInParameter(dbCMD, "@UserID", SqlDbType.Int, entSTU_StudentDetails.UserID);

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
        public Boolean Insert(STU_StudentENT entSTU_StudentDetails)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_STU_Student_Insert");

                sqlDB.AddOutParameter(dbCMD, "@StudentID", SqlDbType.Int, 4);
                sqlDB.AddInParameter(dbCMD, "@StudentName", SqlDbType.VarChar, entSTU_StudentDetails.StudentName);
                sqlDB.AddInParameter(dbCMD, "@EnrollmentNo", SqlDbType.VarChar, entSTU_StudentDetails.EnrollmentNo);
                sqlDB.AddInParameter(dbCMD, "@RollNo", SqlDbType.Int, entSTU_StudentDetails.RollNo);
                sqlDB.AddInParameter(dbCMD, "@CurrentSem", SqlDbType.Int, entSTU_StudentDetails.CurrentSem);
                sqlDB.AddInParameter(dbCMD, "@EmailInstitute", SqlDbType.VarChar, entSTU_StudentDetails.EmailInstitute);
                sqlDB.AddInParameter(dbCMD, "@EmailPersonal", SqlDbType.VarChar, entSTU_StudentDetails.EmailPersonal);
                sqlDB.AddInParameter(dbCMD, "@BirthDate", SqlDbType.DateTime, entSTU_StudentDetails.BirthDate);
                sqlDB.AddInParameter(dbCMD, "@ContactNo", SqlDbType.VarChar, entSTU_StudentDetails.ContactNo);
                sqlDB.AddInParameter(dbCMD, "@Gender", SqlDbType.VarChar, entSTU_StudentDetails.Gender);
                sqlDB.AddInParameter(dbCMD, "@UserID", SqlDbType.Int, entSTU_StudentDetails.UserID);

                DataBaseHelper DBH = new DataBaseHelper();
                DBH.ExecuteNonQuery(sqlDB, dbCMD);

                entSTU_StudentDetails.StudentID = (SqlInt32)Convert.ToInt32(dbCMD.Parameters["@StudentID"].Value);

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