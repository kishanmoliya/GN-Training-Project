using GNForm3C.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RPT_ACC_IncomeList
/// </summary>
/// 
namespace GNForm3C.BAL
{
    public class RPT_ACC_IncomeListBALBase
    {
        public RPT_ACC_IncomeListBALBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region RDLC (Reports)
        public DataTable Report_ACC_Income_ByFinYear()
        {
            RPT_ACC_IncomeListDAL dalACC_Income = new RPT_ACC_IncomeListDAL();
            return dalACC_Income.Report_ACC_Income_ByFinYear();
        }

        #endregion RDLC (Reports)
    }
}