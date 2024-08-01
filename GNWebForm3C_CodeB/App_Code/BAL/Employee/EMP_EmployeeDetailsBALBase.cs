using GNForm3C.DAL;
using GNForm3C.ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EMP_EmployeeDetailsBALBase
/// </summary>
/// 

namespace GNForm3C.BAL
{
    public abstract class EMP_EmployeeDetailsBALBase
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
        public EMP_EmployeeDetailsBALBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region DeleteOperation

        public Boolean Delete(SqlInt32 EmployeeID)
        {
            EMP_EmployeeDetailsDAL dalEMP_EmployeeDetails = new EMP_EmployeeDetailsDAL();
            if (dalEMP_EmployeeDetails.Delete(EmployeeID))
            {
                return true;
            }
            else
            {
                this.Message = dalEMP_EmployeeDetails.Message;
                return false;
            }
        }

        #endregion DeleteOperationa

        #region Select Operation

        public EMP_EmployeeDetailsENT SelectPK(SqlInt32 EmployeeID)
        {
            EMP_EmployeeDetailsDAL dalEMP_EmployeeDetails= new EMP_EmployeeDetailsDAL();
            return dalEMP_EmployeeDetails.SelectPK(EmployeeID);
        }


        public DataTable SelectView(SqlInt32 EmployeeID)
        {
            EMP_EmployeeDetailsDAL dalEMP_EmployeeDetails = new EMP_EmployeeDetailsDAL();
            return dalEMP_EmployeeDetails.SelectView(EmployeeID);
        }

        public DataTable SelectPage(SqlInt32 PageOffset, SqlInt32 PageSize, out Int32 TotalRecords, SqlString EmployeeName, SqlInt32 EmployeeTypeID)
        {
            EMP_EmployeeDetailsDAL dalMST_EmployeeDetails = new EMP_EmployeeDetailsDAL();
            return dalMST_EmployeeDetails.SelectPage(PageOffset, PageSize, out TotalRecords, EmployeeName, EmployeeTypeID);
        }

        public DataTable SelectShow(SqlInt32 EmployeeTypeID)
        {
            EMP_EmployeeDetailsDAL dalEMP_EmployeeDetails = new EMP_EmployeeDetailsDAL();
            return dalEMP_EmployeeDetails.SelectShow(EmployeeTypeID);
        }
        #endregion

        #region UpdateOperation

        public Boolean Update(EMP_EmployeeDetailsENT entEMP_EmployeeDetails)
        {
            EMP_EmployeeDetailsDAL dalEMP_EmployeeDetails = new EMP_EmployeeDetailsDAL();
            if (dalEMP_EmployeeDetails.Update(entEMP_EmployeeDetails))
            {
                return true;
            }
            else
            {
                this.Message = dalEMP_EmployeeDetails.Message;
                return false;
            }
        }

        #endregion UpdateOperation

        #region InsertOperation

        public Boolean Insert(EMP_EmployeeDetailsENT entEMP_EmployeeDetails)
        {
            EMP_EmployeeDetailsDAL dalEMP_EmployeeDetails = new EMP_EmployeeDetailsDAL();
            if (dalEMP_EmployeeDetails.Insert(entEMP_EmployeeDetails))
            {
                return true;
            }
            else
            {
                this.Message = dalEMP_EmployeeDetails.Message;
                return false;
            }
        }

        #endregion InsertOperation
    }
}