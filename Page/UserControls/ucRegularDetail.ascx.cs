using StoreRequisition.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreRequisition.Page.UserControls
{
    public partial class ucRegularDetail : System.Web.UI.UserControl
    {
        clsSQLscript Objrun = new clsSQLscript();
        DataTable dtissue = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            dtissue = Objrun.GetStore_req_issue_items((string)Session["RequisitionNumber"]);

            if (dtissue.Rows.Count > 0)
            {
                lblDate.Text = Convert.ToDateTime(dtissue.Rows[0]["TRANSACTION_DATE"]).ToString("dd MMMM yyyy");
                lblTime.Text = dtissue.Rows[0]["TRANSACTION_TIME"].ToString();
                issTO.Text = dtissue.Rows[0]["SUB_TO"].ToString();
                issNO.Text = dtissue.Rows[0]["STORE_SLIP_NO"].ToString();
                issREF.Text = dtissue.Rows[0]["TRANSACTION_SET_ID"].ToString();

            }    
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
            Double x = 0, totalPicking=0;

            DataTable DT = new DataTable();
            DT = Objrun.GetStore_req_item((String)Session["RequisitionNumber"]);

            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    htmlStr += "<tr class='auto-style24'><td class='auto-style26'>" + DT.Rows[i]["ITEM_NUM"].ToString().Trim() + " </td><td class='auto-style25'>" + DT.Rows[i]["PART_NUM"].ToString().Trim() + "</td><td class='auto-style25'>" + DT.Rows[i]["SHELF"].ToString().Trim() + "</td><td class='auto-style27 col-h'>" + DT.Rows[i]["EXPIRE"].ToString().Trim() + "</td><td class='auto-style26  col-h'>" + DT.Rows[i]["UOM"].ToString().Trim() + "</td><td class='auto-style27'>" + DT.Rows[i]["QTY_REQ"].ToString().Trim() + " </td><td class='auto-style27'>" + DT.Rows[i]["ACTUAL"].ToString().Trim() + "</td><td class='auto-style27 col-h'><input class='form-control' type='hidden'  id='txtReqNo_" + DT.Rows[i][0].ToString().Trim() + "' value='" + (String)Session["RequisitionNumber"] + "' ></td></tr>";
                    x = x + Convert.ToDouble(DT.Rows[i][3].ToString().Trim());
                    


                    totalPicking += Convert.ToDouble((DT.Rows[i]["ACTUAL"].ToString().Trim() == ""?"0": DT.Rows[i]["ACTUAL"].ToString().Trim()));
                } 
                htmlStr += "<tr class='auto-style24'><td></td><td></td><td class='col-h'></td><td class='col-h'></td> <td class='auto-style27 ' ><strong><span class='auto-style27'>Total</span> : </strong></td><td class='auto-style27'>" + x.ToString("#,##0.00") + "</td><td id='spTotal' class='auto-style27'>" + totalPicking.ToString("#,##0.00") + "</td><td  class='col-h'>&nbsp;&nbsp;&nbsp;</td></tr>";
            }

            return htmlStr;
        }

        public string getDataProgress()
        {
            string strHtml = "";
            DataTable dt = new DataTable();
            dt = Objrun.getDataProgress((string)Session["RequisitionNumber"]);
            if (dt.Rows.Count > 0)
            {
                
                foreach (DataRow item in dt.Rows)
                {

                    var flag_process = item["FLAG_PICK"].ToString();



                    if (flag_process == "1")
                    {
                        flag_process = "text-white text-cur";
                    }
                    else if (flag_process == "0")
                    {
                        flag_process = "text-secondary  ";
                    }
                    else
                    {
                        flag_process = "text-white text-progress";
                    }

                    strHtml += "<div class='col-12 col-md-2 text-center'>                                                  ";
                    strHtml += "    <div> <span class='display-4 "+ flag_process + " '>"+ item["PROCESS_ICONS"].ToString() + "</span></div>";
                    strHtml += "    <span class='pt-5'>"+item["PROCESS_NAME"].ToString()+"</span>                                       ";
                    strHtml += "    <p class='small'>"+ item["APPROVED_DATE"].ToString() + "</p>                                            ";
                    strHtml += "</div>                                                                                     ";
                }
               
            }




            return strHtml;

        }

        public string getMaterialIssue()
        {
            string strHtml = "";
            DataTable dt = new DataTable();
            try
            {
                //dt = Objrun.GetStore_req_issue_items((String)Session["RequisitionNumber"]);
                if (dtissue.Rows.Count > 0)
                {



                    



                    for (int i = 0; i < dtissue.Rows.Count; i++)
                    {
                        strHtml += "<tr>";
                        strHtml += "<td> " + (i+1).ToString() + " </td> ";
                        strHtml += "<td> " +dtissue.Rows[i]["SEGMENT1"].ToString() + " </td> ";
                        strHtml += "<td> " +dtissue.Rows[i]["SUB_FROM"].ToString() + " </td> ";
                        strHtml += "<td> " +dtissue.Rows[i]["TRANS_UOM"].ToString() + " </td> ";
                        strHtml += "<td> " +dtissue.Rows[i]["TRANS_QTY"].ToString() + " </td> ";
                        strHtml += "<td> " +dtissue.Rows[i]["LOT_NUMBER"].ToString() + " </td> ";
                        strHtml += "<td> " + dtissue.Rows[i]["LOT_INVOCE_NO"].ToString() + " </td> ";
                        strHtml += "<td> " + dtissue.Rows[i]["LOT_SUPPLIER_LOT"].ToString() + " </td> ";
                        strHtml += "</tr>";
                    }
                }
                else
                {
                    strHtml += "<tr>";
                    strHtml += "<td colspan='8' class='text-center'>  <i class='far fa-folder'></i> data not found </td> ";
                    strHtml += "</tr>";
                }



                return strHtml;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}