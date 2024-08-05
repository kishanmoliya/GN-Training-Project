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

        #region Select BranchIntake Data
        public DataTable GetBranchIntakeData()
        {
            BR_BranchIntakeDAL dalBR_BranchIntake = new BR_BranchIntakeDAL();

            return dalBR_BranchIntake.GetBranchIntakeData();
        }


        #endregion Select BranchIntake Data

        #region Insert/Update Intake DATA

        public Boolean SaveBranchIntakeData(DataTable branchIntakeTable)
        {
            BR_BranchIntakeDAL dalBR_BranchIntake = new BR_BranchIntakeDAL();
            if (dalBR_BranchIntake.SaveBranchIntakeData(branchIntakeTable))
            {
                return true;
            }
            else
            {
                Message = dalBR_BranchIntake.Message;
                return false;
            }
        }
        #endregion Insert/Update Intake DATA

        #region Delete BranchIntake Data
        public void DeleteBranchIntakeData(string branch)
        {
            BR_BranchIntakeDAL dalBR_BranchIntake = new BR_BranchIntakeDAL();

            dalBR_BranchIntake.DeleteBranchIntakeData(branch);
        }

        #endregion Delete BranchIntake Data
    }
}
