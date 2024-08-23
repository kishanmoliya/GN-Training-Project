using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using GNForm3C;
using GNForm3C.DAL;
using GNForm3C.ENT;

namespace GNForm3C.BAL
{
    public abstract class MST_PatientBALBase
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

        #region Constructor

        public MST_PatientBALBase()
        {

        }

        #endregion Constructor

        #region Insert
        public SqlInt32 InsertPatient(ACC_GNPatientENT entMST_Patient)
        {
            MST_PatientDAL dalMST_Patient = new MST_PatientDAL();
            SqlInt32 PatientID = dalMST_Patient.InsertPatient(entMST_Patient);

            if (PatientID > 0)
            {
                return PatientID;
            }
            else
            {
                this.Message = dalMST_Patient.Message;
                return PatientID;
            }
        }

        public DataTable SelectView(SqlInt32 PatientID)
        {
            MST_PatientDAL dalMST_Patient = new MST_PatientDAL();
            return dalMST_Patient.SelectView(PatientID);
        }
        #endregion

        #region Rerports
        public DataTable RPT_Patient_ICard()
        {
            MST_PatientDAL dalMST_Patient = new MST_PatientDAL();
            return dalMST_Patient.RPT_Patient_ICard();
        }
        #endregion
    }
}