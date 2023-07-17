using StoreRequisition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreRequisition
{
    public partial class BoostrapSite : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNames.Text = (string)Session["lblnames"];

            string assemblyVersion = Assembly.GetExecutingAssembly()
                                    .GetCustomAttribute<AssemblyFileVersionAttribute>()
                                    .Version;
            // Use the assemblyVersion string as needed in your code
            // For example, you can assign it to a label or use it to set the title tag.

            Page.Title = assemblyVersion;
        }



        public string getAuthority()
        {
            Boolean Results = false;
            string strResults = "Other";
            try
            {
                UserAD userAD = (UserAD)HttpContext.Current.Session["UserAD"];
                if (userAD != null)
                {
                    //if (userAD.Departments.ToLower() == "store")
                    //{
                    //    Results = true;
                    //    strResults = "store";
                    //}else if(userAD.Departments.ToLower() == "warehouse")
                    //{
                    //    Results = true;
                    //    strResults = "warehouse";
                    //}


                    strResults = userAD.Departments.ToLower();




                }
                // return Results;
                return strResults;
            }
            catch (Exception )
            {
                return "Other";
            }
        }
    }

}