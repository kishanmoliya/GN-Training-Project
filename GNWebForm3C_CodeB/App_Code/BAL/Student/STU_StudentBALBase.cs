using GNForm3C.DAL;
using GNForm3C.ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for STU_StudentBALBase
/// </summary>
/// 
namespace GNForm3C.BAL
{
    public class STU_StudentBALBase
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
        public STU_StudentBALBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region DeleteOperation
        public Boolean Delete(SqlInt32 StudentID)
        {
            STU_StudentDAL dalSTU_Student = new STU_StudentDAL();
            if (dalSTU_Student.Delete(StudentID))
            {
                return true;
            }
            else
            {
                this.Message = dalSTU_Student.Message;
                return false;
            }
        }

        #endregion DeleteOperationa

        #region Select Operation
        public STU_StudentENT SelectPK(SqlInt32 StudentID)
        {
            STU_StudentDAL dalSTU_StudentDetails = new STU_StudentDAL();
            return dalSTU_StudentDetails.SelectPK(StudentID);
        }

        public DataTable SelectPage(SqlInt32 PageOffset, SqlInt32 PageSize, out Int32 TotalRecords, SqlString StudentName, SqlString EnrollmentNo, SqlInt32 CurrentSem, SqlString EmailInstitute, SqlString EmailPersonal, SqlString Gender, SqlString ContactNo, SqlInt32 RollNo)
        {
            STU_StudentDAL dalSTU_StudentList= new STU_StudentDAL();
            return dalSTU_StudentList.SelectPage(PageOffset, PageSize, out TotalRecords, StudentName, EnrollmentNo, CurrentSem, EmailInstitute, EmailPersonal, Gender, ContactNo, RollNo);
        }
        #endregion

        #region UpdateOperation

        public Boolean Update(STU_StudentENT entSTU_StudentDetails)
        {
            STU_StudentDAL dalSTU_StudentDetails = new STU_StudentDAL();
            if (dalSTU_StudentDetails.Update(entSTU_StudentDetails))
            {
                return true;
            }
            else
            {
                this.Message = dalSTU_StudentDetails.Message;
                return false;
            }
        }

        #endregion UpdateOperation

        #region InsertOperation

        public Boolean Insert(STU_StudentENT entSTU_StudentDetails)
        {
            STU_StudentDAL dalSTU_StudentDetails = new STU_StudentDAL();
            if (dalSTU_StudentDetails.Insert(entSTU_StudentDetails))
            {
                return true;
            }
            else
            {
                this.Message = dalSTU_StudentDetails.Message;
                return false;
            }
        }

        #endregion InsertOperation

        #region ComboBox

        public DataTable SelecComboBox()
        {
            STU_StudentDAL dalSTU_StudentSem = new STU_StudentDAL();
            return dalSTU_StudentSem.SelectComboBox();
        }

        #endregion ComboBox
    }
}