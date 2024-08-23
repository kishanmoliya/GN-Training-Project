using GNForm3C.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ACC_LedgerBALBase
/// </summary>
/// 
namespace GNForm3C.BAL
{
    public class ACC_LedgerBALBase
    {
        public ACC_LedgerBALBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable SelectPage(SqlInt32 PageOffset, SqlInt32 PageSize, out Int32 TotalRecords, SqlDateTime FromDate, SqlDateTime ToDate)
        {
            ACC_LedgerDAL dalACC_Ledger = new ACC_LedgerDAL();
            return dalACC_Ledger.SelectPage(PageOffset, PageSize, out TotalRecords, FromDate, ToDate);
        }

        #region Rerports
        public DataTable RPT_HospitalWiseFinyearWiseIncomeExpense()
        {
            ACC_LedgerDALBase dalACC_IncExp = new ACC_LedgerDALBase();
            return dalACC_IncExp.RPT_HospitalWiseFinyearWiseIncomeExpense();
        }

        public DataTable RPT_IncomeExpenseLedger(SqlInt32 FinYearID, SqlInt32 HospitalID)
        {
            ACC_LedgerDALBase dalACC_IncExp = new ACC_LedgerDALBase();
            return dalACC_IncExp.RPT_IncomeExpenseLedger(FinYearID, HospitalID);
        }
        #endregion
    }
}