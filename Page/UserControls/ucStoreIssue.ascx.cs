using StoreRequisition.Class;
using System;
using System.Data;
using System.Web.Services;

namespace StoreRequisition.Page.UserControls
{
    public partial class ucStoreIssue : System.Web.UI.UserControl
    {
        clsSQLscript Objrun = new clsSQLscript();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string getReqNumber()
        {
            return "*" + (String)Session["RequisitionNumber"] + "*";
        }

        public string getSubInventory()
        {
            return "*" + (String)Session["SubInventory"] + "*";
        }
        public string getWhileLoopData()
        {
            string htmlStr = "";
            Double x = 0;

            DataTable DT = new DataTable();
            DT = Objrun.GetStore_req_item((String)Session["RequisitionNumber"]);

            if (DT.Rows.Count > 0)
            {
                //for (int i = 0; i < DT.Rows.Count; i++)
                //{
                //    htmlStr += "<tr class='auto-style24'><td class='auto-style26'>" + DT.Rows[i]["ITEM_NUM"].ToString().Trim() + " </td><td class='auto-style25'>" + DT.Rows[i]["PART_NUM"].ToString().Trim() + "</td><td class='auto-style25'>" + DT.Rows[i]["SHELF"].ToString().Trim() + "</td><td class='auto-style27 col-h'>" + DT.Rows[i]["EXPIRE"].ToString().Trim() + "</td><td class='auto-style26 '>" + DT.Rows[i]["UOM"].ToString().Trim() + "</td><td class='auto-style27'>" + DT.Rows[i]["QTY_REQ"].ToString().Trim() + " </td><td class='auto-style27'>" + DT.Rows[i]["ACTUAL"].ToString().Trim() + "</td><td class='auto-style27 col-h'><input class='form-control' type='hidden'  id='txtReqNo_" + DT.Rows[i][0].ToString().Trim() + "' value='" + (String)Session["RequisitionNumber"] + "' ></td></tr>";
                //    x = x + Convert.ToDouble(DT.Rows[i][3].ToString().Trim());
                //}
                //htmlStr += "<tr class='auto-style24'><td class='auto-style27' colspan='5' rowspan='0'><strong><span class='auto-style27'>Total</span> : </strong></td><td class='auto-style27'>" + x.ToString("#,##0.00") + "</td><td class='col-h'>&nbsp;&nbsp;&nbsp;</td><td  class='col-h'>&nbsp;&nbsp;&nbsp;</td></tr>";

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    htmlStr += "<tr class='auto-style24'><td class='auto-style26'>" + DT.Rows[i][0].ToString().Trim() + "</td><td class='auto-style25'>" + DT.Rows[i][1].ToString().Trim() + "</td><td class='auto-style25'>" + DT.Rows[i]["SHELF"].ToString().Trim() + "</td><td class='auto-style27 col-h'>" + DT.Rows[i]["EXPIRE"].ToString().Trim() + "</td><td class='auto-style26 '>" + DT.Rows[i][2].ToString().Trim() + "</td><td class='auto-style27'>" + DT.Rows[i][3].ToString().Trim() + "</td><td class=' auto-style27 '><input type='number' class='form-control form-control-sm numberonly '   id='txtPick_" + DT.Rows[i][0].ToString().Trim() + "' placeholder='##' value='" + DT.Rows[i]["ACTUAL"].ToString().Trim()  + "' ></td><td class='auto-style27 col-h'><input class='form-control' type='hidden'  id='txtReqNo_" + DT.Rows[i][0].ToString().Trim() + "' value='" + (String)Session["RequisitionNumber"] + "' ></td> </tr>";
                    x = x + Convert.ToDouble(DT.Rows[i][3].ToString().Trim());
                }

                htmlStr += "<tr class='auto-style24'><td></td><td></td><td></td><td class='col-h'></td> <td class='auto-style27' ><strong><span class='auto-style27'>Total</span> : </strong></td><td class='auto-style27'>" + x.ToString("#,##0.00") + "</td><td id='spTotal' class='auto-style27'> </td><td  class='col-h'>&nbsp;&nbsp;&nbsp;</td></tr>";
            }


            return htmlStr;
        }
    }
}