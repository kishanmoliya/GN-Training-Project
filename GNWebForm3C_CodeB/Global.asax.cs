using System;
using System.Globalization;
using System.Threading;
using System.Web;

public class Global : HttpApplication
{
    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        // Set the culture for the application
        CultureInfo newCulture = new CultureInfo("en-GB");
        newCulture.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
        newCulture.DateTimeFormat.LongDatePattern = "dd-MM-yyyy";

        Thread.CurrentThread.CurrentCulture = newCulture;
        Thread.CurrentThread.CurrentUICulture = newCulture;
    }

    // Other application-level events can be handled here
}