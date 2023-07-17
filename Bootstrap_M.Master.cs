using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreRequisition
{
    public partial class Bootstrap_M : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string assemblyVersion = Assembly.GetExecutingAssembly()
                                   .GetCustomAttribute<AssemblyFileVersionAttribute>()
                                   .Version;
            // Use the assemblyVersion string as needed in your code
            // For example, you can assign it to a label or use it to set the title tag.

            Page.Title = assemblyVersion;
        }
    }
}