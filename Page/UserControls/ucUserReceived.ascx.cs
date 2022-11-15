using StoreRequisition.Class;
using System;
using System.Data;
using System.Web.Services;

namespace StoreRequisition.Page.UserControls
{
    public partial class ucUserReceived : System.Web.UI.UserControl
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
            Double x = 0;

            DataTable DT = new DataTable();
            DT = Objrun.GetStore_req_item((String)Session["RequisitionNumber"]);

            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    htmlStr += "<tr class='auto-style24'><td class='auto-style26'>" + DT.Rows[i]["ITEM_NUM"].ToString().Trim() + " </td><td class='auto-style25'>" + DT.Rows[i]["PART_NUM"].ToString().Trim() + "</td><td class='auto-style25'>" + DT.Rows[i]["SHELF"].ToString().Trim() + "</td><td class='auto-style27 col-h'>" + DT.Rows[i]["EXPIRE"].ToString().Trim() + "</td><td class='auto-style26 '>" + DT.Rows[i]["UOM"].ToString().Trim() + "</td><td class='auto-style27'>" + DT.Rows[i]["QTY_REQ"].ToString().Trim() + " </td><td class='auto-style27'>" + DT.Rows[i]["ACTUAL"].ToString().Trim() + "</td><td class='auto-style27 col-h'><input class='form-control' type='hidden'  id='txtReqNo_" + DT.Rows[i][0].ToString().Trim() + "' value='" + (String)Session["RequisitionNumber"] + "' ></td></tr>";
                    x = x + Convert.ToDouble(DT.Rows[i][3].ToString().Trim());
                }
                htmlStr += "<tr class='auto-style24'><td class='auto-style27' colspan='5' rowspan='0'><strong><span class='auto-style27'>Total</span> : </strong></td><td class='auto-style27'>" + x.ToString("#,##0.00") + "</td><td class='col-h'>&nbsp;&nbsp;&nbsp;</td><td  class='col-h'>&nbsp;&nbsp;&nbsp;</td></tr>";
            }

            return htmlStr;
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
                        strHtml += "<td> " + (i + 1).ToString() + " </td> ";
                        strHtml += "<td> " + dtissue.Rows[i]["SEGMENT1"].ToString() + " </td> ";
                        strHtml += "<td> " + dtissue.Rows[i]["SUB_FROM"].ToString() + " </td> ";
                        strHtml += "<td> " + dtissue.Rows[i]["TRANS_UOM"].ToString() + " </td> ";
                        strHtml += "<td> " + dtissue.Rows[i]["TRANS_QTY"].ToString() + " </td> ";
                        strHtml += "<td> " + dtissue.Rows[i]["LOT_NUMBER"].ToString() + " </td> ";
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