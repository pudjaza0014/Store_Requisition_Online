using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StoreRequisition.Class;
using System.Web.Services;
using StoreRequisition.Models;

namespace StoreRequisition.Page
{
    public partial class Frm_Report_R_Print : System.Web.UI.Page
    {
        clsSQLscript Objrun = new clsSQLscript();

        public string Authority { get; set; }
        Employee employee = new Employee();
        clsAuthenticate aAuthent = new clsAuthenticate();
        protected void Page_Load(object sender, EventArgs e)
        {

            string Gxg = Request.QueryString["RequestNo"];
            if (!string.IsNullOrEmpty(Gxg))
            {
                Session["RequisitionNumber"] = Gxg;
            }
            String Date_Time = (String)Session["RequestDate"];
            vlRequisitionNumber.Text = (String)Session["RequisitionNumber"];
            getDataDetail((String)Session["RequisitionNumber"]);
        }



        public string getReqNumber()
        {
            return "*" + (String)Session["RequisitionNumber"] + "*";
        }

        public string getSubInventory()
        {
            // return "*" + (String)Session["SubInventory"] + "*";


            return "*" + vlSubInventory.Text + "*";
        }


        public void getDataDetail(string strReq_Num)
        {
            try
            {
                clsSQLscript Objrun = new clsSQLscript();
                DataTable dt;
                dt = new DataTable();
                dt = Objrun.GetStore_Req_Info(strReq_Num);
                vlLocation.Text = dt.Rows[0]["REQ_LOCATION"].ToString();
                vlDeliveryStation.Text = dt.Rows[0]["DELIVERY_STATION"].ToString();
               // vlApprovedBy.Text = dt.Rows[0]["REQ_APPROVE_NAME_BY"].ToString();
                vlRequestBy.Text = dt.Rows[0]["REQ_BY"].ToString();
                vlRequestTel.Text = dt.Rows[0]["REQ_BY_TEL"].ToString();
                vlRequestTel.Text = dt.Rows[0]["REQ_BY_TEL"].ToString();
                vlRequestDate.Text = dt.Rows[0]["REQ_DATE"].ToString();
                vlRequestTime.Text = dt.Rows[0]["REQ_TIME"].ToString();
                vlIssueType.Text = dt.Rows[0]["ISSUE_TYPE"].ToString();
                vlSubInventory.Text = dt.Rows[0]["SUB_INV"].ToString();
                vlRemark.Text = dt.Rows[0]["REQ_REMARK"].ToString();
                //vStatus.Text = dt.Rows[0]["PROCESS_NAME"].ToString();
                // vStatus2.Text = dt.Rows[0]["PROCESS_NAME"].ToString();


                UserAD userAD = (UserAD)Session["UserAD"];
                UserAD userAD_EN = new UserAD();
                clsAuthenticate aAuthent = new clsAuthenticate();
                userAD_EN = aAuthent.getUserAD(dt.Rows[0]["REQ_APPROVE_BY"].ToString());
                dt = new DataTable();
              //  dt = Objrun.GetUsersData(userAD_EN.EN);
               // vlApprovedBy.Text = dt.Rows[0]["EMP_NAME_ENG"].ToString();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string getWhileLoopData()
        {
            string htmlStr = "";
            Double x = 0, totalPicking = 0;

            DataTable DT = new DataTable();
            DT = Objrun.GetStore_req_item((String)Session["RequisitionNumber"]);

            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    htmlStr += "<tr class='auto-style24'><td class='auto-style26'>" + DT.Rows[i]["ITEM_NUM"].ToString().Trim() + " </td><td class='auto-style25'>" + DT.Rows[i]["PART_NUM"].ToString().Trim() + "</td><td class='auto-style25'>" + DT.Rows[i]["SHELF"].ToString().Trim() + "</td><td class='auto-style27 col-h'>" + DT.Rows[i]["EXPIRE"].ToString().Trim() + "</td><td class='auto-style26  col-h'>" + DT.Rows[i]["UOM"].ToString().Trim() + "</td><td class='auto-style27'>" + DT.Rows[i]["QTY_REQ"].ToString().Trim() + " </td><td class='auto-style27'>" + DT.Rows[i]["ACTUAL"].ToString().Trim() + "</td><td class='auto-style27 col-h'><input class='form-control' type='hidden'  id='txtReqNo_" + DT.Rows[i][0].ToString().Trim() + "' value='" + (String)Session["RequisitionNumber"] + "' ></td></tr>";
                    x = x + Convert.ToDouble(DT.Rows[i][3].ToString().Trim());



                    totalPicking += Convert.ToDouble((DT.Rows[i]["ACTUAL"].ToString().Trim() == "" ? "0" : DT.Rows[i]["ACTUAL"].ToString().Trim()));
                }
                htmlStr += "<tr class='auto-style24'><td></td><td></td><td class='col-h'></td><td class='col-h'></td> <td class='auto-style27 ' ><strong><span class='auto-style27'>Total</span> : </strong></td><td class='auto-style27'>" + x.ToString("#,##0.00") + "</td><td id='spTotal' class='auto-style27'>" + totalPicking.ToString("#,##0.00") + "</td><td  class='col-h'>&nbsp;&nbsp;&nbsp;</td></tr>";
            }

            return htmlStr;
        }
    }
}