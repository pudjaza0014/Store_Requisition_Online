using StoreRequisition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        }



        public Boolean getAuthority()
        {
            Boolean Results = false;
            try
            {
                UserAD userAD = (UserAD)HttpContext.Current.Session["UserAD"];
                if (userAD != null)
                {
                    if (userAD.Departments.ToLower() == "store")
                    {
                        Results = true;
                    }
                }
                return Results;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}