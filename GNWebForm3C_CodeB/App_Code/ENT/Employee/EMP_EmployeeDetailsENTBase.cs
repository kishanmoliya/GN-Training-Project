using System;
using System.Data.SqlTypes;

namespace GNForm3C.ENT
{
    public abstract class EMP_EmployeeDetailsENTBase
    {
        #region Properties


        protected SqlInt32 _EmployeeID;
        public SqlInt32 EmployeeID
        {
            get
            {
                return _EmployeeID;
            }
            set
            {
                _EmployeeID = value;
            }
        }

        protected SqlString _EmployeeName;
        public SqlString EmployeeName
        {
            get
            {
                return _EmployeeName;
            }
            set
            {
                _EmployeeName = value;
            }
        }

        protected SqlInt32 _EmployeeTypeID;
        public SqlInt32 EmployeeTypeID
        {
            get
            {
                return _EmployeeTypeID;
            }
            set
            {
                _EmployeeTypeID = value;
            }
        }

        protected SqlString _Remark;
        public SqlString Remark
        {
            get
            {
                return _Remark;
            }
            set
            {
                _Remark = value;
            }
        }

        protected SqlInt32 _UserID;
        public SqlInt32 UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = value;
            }
        }

        protected SqlDateTime _Created;
        public SqlDateTime Created
        {
            get
            {
                return _Created;
            }
            set
            {
                _Created = value;
            }
        }

        protected SqlDateTime _Modified;
        public SqlDateTime Modified
        {
            get
            {
                return _Modified;
            }
            set
            {
                _Modified = value;
            }
        }

        #endregion Properties

        #region Constructor

        public EMP_EmployeeDetailsENTBase()
        {

        }

        #endregion Constructor

        #region ToString

        public override String ToString()
        {
            String EMP_EmployeeDetailsENT_String = String.Empty;

            if (!EmployeeID.IsNull)
                EMP_EmployeeDetailsENT_String += " EmployeeID = " + EmployeeID.Value.ToString();

            if (!EmployeeName.IsNull)
                EMP_EmployeeDetailsENT_String += "| EmployeeName = " + EmployeeName.Value;

            if (!EmployeeTypeID.IsNull)
                EMP_EmployeeDetailsENT_String += "| EmployeeTypeID = " + EmployeeTypeID.Value.ToString();

            if (!Remark.IsNull)
                EMP_EmployeeDetailsENT_String += "| Remark = " + Remark.Value;

            if (!UserID.IsNull)
                EMP_EmployeeDetailsENT_String += "| UserID = " + UserID.Value.ToString();

            if (!Created.IsNull)
                EMP_EmployeeDetailsENT_String += "| Created = " + Created.Value.ToString("dd-MM-yyyy");

            if (!Modified.IsNull)
                EMP_EmployeeDetailsENT_String += "| Modified = " + Modified.Value.ToString("dd-MM-yyyy");


            EMP_EmployeeDetailsENT_String = EMP_EmployeeDetailsENT_String.Trim();

            return EMP_EmployeeDetailsENT_String;
        }

        #endregion ToString
    }

}