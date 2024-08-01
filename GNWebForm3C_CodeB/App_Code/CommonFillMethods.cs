using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using GNForm3C.BAL;
using System.Data.SqlTypes;
using GNForm3C.ENT;
using System.Collections.Generic;

namespace GNForm3C
{
    public class CommonFillMethods
    {
        public CommonFillMethods()
        {
        }
        public static void FillDropDownListTransactionID(DropDownList ddl)
        {
            ACC_TransactionBAL balACC_Transaction = new ACC_TransactionBAL();
            ddl.DataSource = balACC_Transaction.SelectComboBox();
            ddl.DataValueField = "TransactionID";
            ddl.DataTextField = "Patient";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Transaction", "-99"));
        }
        public static void FillDropDownListExpenseTypeID(DropDownList ddl)
        {
            MST_ExpenseTypeBAL balMST_ExpenseType = new MST_ExpenseTypeBAL();
            ddl.DataSource = balMST_ExpenseType.SelectComboBox();
            ddl.DataValueField = "ExpenseTypeID";
            ddl.DataTextField = "ExpenseType";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Expense Type", "-99"));
        }
        public static void FillDropDownListExpenseTypeIDByFinYearID(DropDownList ddl, SqlInt32 FinYearID, SqlInt32 HospitalID)
        {
            MST_ExpenseTypeBAL balMST_ExpenseType = new MST_ExpenseTypeBAL();
            ddl.DataSource = balMST_ExpenseType.SelectComboBoxByFinYearID(FinYearID, HospitalID);
            ddl.DataValueField = "ExpenseTypeID";
            ddl.DataTextField = "ExpenseType";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Expense Type", "-99"));
        }
        
        public static void FillDropDownListFinYearIDByHospitalID(DropDownList ddl, SqlInt32 HospitalID)
        {
            MST_FinYearBAL balMST_FinYear = new MST_FinYearBAL();
            ddl.DataSource = balMST_FinYear.SelectComboBoxByHospitalID(HospitalID);
            ddl.DataValueField = "FinYearID";
            ddl.DataTextField = "FinYearName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Fin Year", "-99"));
        }
        public static void FillDropDownListFinYearID(DropDownList ddl)
        {
            MST_FinYearBAL balMST_FinYear = new MST_FinYearBAL();
            ddl.DataSource = balMST_FinYear.SelectComboBox();
            ddl.DataValueField = "FinYearID";
            ddl.DataTextField = "FinYearName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Fin Year", "-99"));
        }
        public static void FillDropDownListExpenseFinYearIDByHospitalID(DropDownList ddl, SqlInt32 HospitalID)
        {
            MST_FinYearBAL balMST_FinYear = new MST_FinYearBAL();
            ddl.DataSource = balMST_FinYear.SelectComboBoxByHospitalIDExpense(HospitalID);
            ddl.DataValueField = "FinYearID";
            ddl.DataTextField = "FinYearName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Fin Year", "-99"));
        }
        public static void FillSingleDropDownListFinYearID(DropDownList ddl)
        {
            MST_FinYearBAL balMST_FinYear = new MST_FinYearBAL();
            ddl.DataSource = balMST_FinYear.SelectComboBox();
            ddl.DataValueField = "FinYearID";
            ddl.DataTextField = "FinYearName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Fin Year", "-99"));
        }
        public static void FillDropDownListHospitalID(DropDownList ddl)
        {
            MST_HospitalBAL balMST_Hospital = new MST_HospitalBAL();
            ddl.DataSource = balMST_Hospital.SelecComboBox();
            ddl.DataValueField = "HospitalID";
            ddl.DataTextField = "Hospital";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Hospital", "-99"));
        }

        public static void FillDropDownIsDefault(DropDownList ddl)
        {
            // Add "Yes" and "No" items
            ddl.Items.Add(new ListItem("Yes", "1"));
            ddl.Items.Add(new ListItem("No", "0"));

            // Set "Yes" as the default selected item
            ddl.SelectedValue = "1";
        }

        public static void FillDropDownListEmployeeTypeID(DropDownList ddl)
        {
            EMP_EmployeeTypeBAL balMEP_EmployeeType = new EMP_EmployeeTypeBAL();
            ddl.DataSource = balMEP_EmployeeType.SelecComboBox();
            ddl.DataValueField = "EmployeeTypeID";
            ddl.DataTextField = "EmployeeTypeName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select EmployeeType", "-99"));
        }

        public static void FillDropDownListSemester(DropDownList ddl)
        {
            //STU_StudentBAL balSTU_StudentSem = new STU_StudentBAL();
            //ddl.DataSource = balSTU_StudentSem.SelecComboBox();
            //ddl.DataValueField = "CurrentSem";
            //ddl.DataTextField = "CurrentSem";
            //ddl.DataBind();
            //ddl.Items.Insert(0, new ListItem("Select CurrentSem", "-99"));
            ddl.Items.Clear();

            List<string> semesters = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8" };

            ddl.DataSource = semesters;
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Select Semester", "-99"));
        }

        public static void FillDropDownListGender(DropDownList ddl)
        {
            ddl.Items.Clear();

            List<string> genders = new List<string> { "Male", "Female", "Other" };

            ddl.DataSource = genders;
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Select Gender", "-99"));
        }

        public static void FillDropDownListIncomeTypeIDByFinYearID(DropDownList ddl, SqlInt32 FinYearID, SqlInt32 HospitalID)
        {
            MST_IncomeTypeBAL balMST_IncomeType = new MST_IncomeTypeBAL();
            ddl.DataSource = balMST_IncomeType.SelectComboBoxByFinYearID(FinYearID, HospitalID);
            ddl.DataValueField = "IncomeTypeID";
            ddl.DataTextField = "IncomeType";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Income Type", "-99"));
        }
        public static void FillSingleDropDownListIncomeTypeID(DropDownList ddl)
        {
            MST_IncomeTypeBAL balMST_IncomeType = new MST_IncomeTypeBAL();
            ddl.DataSource = balMST_IncomeType.SelectSingleComboBox();
            ddl.DataValueField = "IncomeTypeID";
            ddl.DataTextField = "IncomeType";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Income Type", "-99"));
        }
        public static void FillDropDownListReceiptTypeID(DropDownList ddl)
        {
            MST_ReceiptTypeBAL balMST_ReceiptType = new MST_ReceiptTypeBAL();
            ddl.DataSource = balMST_ReceiptType.SelectComboBox();
            ddl.DataValueField = "ReceiptTypeID";
            ddl.DataTextField = "ReceiptTypeName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Receipt Type", "-99"));
        }
        public static void FillDropDownListSubTreatmentID(DropDownList ddl)
        {
            MST_SubTreatmentBAL balMST_SubTreatment = new MST_SubTreatmentBAL();
            ddl.DataSource = balMST_SubTreatment.SelectComboBox();
            ddl.DataValueField = "SubTreatmentID";
            ddl.DataTextField = "SubTreatmentName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Sub Treatment", "-99"));
        }
        public static void FillDropDownListTreatmentID(DropDownList ddl)
        {
            MST_TreatmentBAL balMST_Treatment = new MST_TreatmentBAL();
            ddl.DataSource = balMST_Treatment.SelectComboBox();
            ddl.DataValueField = "TreatmentID";
            ddl.DataTextField = "Treatment";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Treatment", "-99"));
        }
        public static void FillDropDownListUserID(DropDownList ddl)
        {
            SEC_UserBAL balSEC_User = new SEC_UserBAL();
            ddl.DataSource = balSEC_User.SelectComboBox();
            ddl.DataValueField = "UserID";
            ddl.DataTextField = "UserName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select User", "-99"));
        }
    }
}
