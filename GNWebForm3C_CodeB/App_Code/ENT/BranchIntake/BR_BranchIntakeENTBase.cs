using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BR_BranchIntakeENTBase
/// </summary>
/// 
namespace GNForm3C.ENT
{
    public class BR_BranchIntakeENTBase
    {
        #region Properties

        protected SqlString _Branch;
        public SqlString Branch
        {
            get
            {
                return _Branch;
            }
            set
            {
                _Branch = value;
            }
        }

        protected SqlInt32 _AdmissionYear;
        public SqlInt32 AdmissionYear
        {
            get
            {
                return _AdmissionYear;
            }
            set
            {
                _AdmissionYear = value;
            }
        }

        protected SqlInt32 _Intake;
        public SqlInt32 Intake
        {
            get
            {
                return _Intake;
            }
            set
            {
                _Intake = value;
            }
        }

        #endregion Properties

        #region Constructor

        public BR_BranchIntakeENTBase()
        {

        }

        #endregion Constructor

        #region ToString

        public override String ToString()
        {
            String BR_BranchIntakeENT_String = String.Empty;

           
            if (!Branch.IsNull)
                BR_BranchIntakeENT_String += "| Branch = " + Branch.Value;

            if (!AdmissionYear.IsNull)
                BR_BranchIntakeENT_String += "| AdmissionYear = " + AdmissionYear.Value.ToString();

            if (!Intake.IsNull)
                BR_BranchIntakeENT_String += "| Intake = " + Intake.Value.ToString();

            BR_BranchIntakeENT_String = BR_BranchIntakeENT_String.Trim();

            return BR_BranchIntakeENT_String;
        }

        #endregion ToString

    }
}