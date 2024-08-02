using GNForm3C.DAL;
using GNForm3C.ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BR_BranchIntakeBALBase
/// </summary>
/// 
namespace GNForm3C.BAL
{
    public class BR_BranchIntakeBALBase
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
        public BR_BranchIntakeBALBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Select Operation
        public DataTable SelectPage()
        {
            BR_BranchIntakeDAL dalBR_BranchIntake = new BR_BranchIntakeDAL();
            return dalBR_BranchIntake.SelectPage();
        }
        #endregion Select Operation

        #region UpdateOperation

        public Boolean Update(String Branch, int Intake, int Year)
        {
            BR_BranchIntakeDAL dalBR_BranchIntake = new BR_BranchIntakeDAL();
            if (dalBR_BranchIntake.Update(Branch, Intake, Year))
            {
                return true;
            }
            else
            {
                this.Message = dalBR_BranchIntake.Message;
                return false;
            }
        }

        #endregion UpdateOperation
    }
}