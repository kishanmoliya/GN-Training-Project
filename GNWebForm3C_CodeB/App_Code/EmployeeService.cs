using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using GNForm3C.BAL;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
[ScriptService]
public class EmployeeService : WebService
{
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<string> GetEmployeeNames(string prefixText, string contextKey)
    {
        int? employeeTypeID = null;

        if (!string.IsNullOrEmpty(contextKey))
        {
            employeeTypeID = Convert.ToInt32(contextKey);
        }

        EMP_EmployeeDetailsBAL empName = new EMP_EmployeeDetailsBAL();

        List<string> employeeNames = empName.GetEmployeeNames(prefixText, employeeTypeID);

        return employeeNames;
    }
}

