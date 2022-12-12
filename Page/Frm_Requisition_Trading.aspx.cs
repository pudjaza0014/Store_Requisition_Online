using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreRequisition.Page
{
    public partial class Frm_Requisition_Trading : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<string> arr = new List<string>();
            ClassDB db = new ClassDB();
            try
            {
                string Due_Date = DatePick2.Text;

                arr = db.Requisition_Trading_Generate(Due_Date);
                if (arr[0].ToString() != "OK")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + arr[1] + "');", true);
                }

                Response.Redirect("~/Page/Frm_Report_R.aspx?RequestNo="+arr[1]);


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('"+ex.Message+"');", true);
                
            }
           
        }
    }
}