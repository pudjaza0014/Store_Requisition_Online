using StoreRequisition.Class;
using StoreRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;

namespace StoreRequisition.Page.UserControls
{
    public partial class ucPickingCenters : System.Web.UI.UserControl
    {
        clsSQLscript Objrun = new clsSQLscript();
        protected void Page_Load(object sender, EventArgs e)
        {
            UserAD userAD = (UserAD)Session["UserAD"];
            if (userAD.Departments.ToLower() != null)
            {
                if (userAD.Departments.ToLower() == "packing")
                {
                     

                    //Response.Write("MachineName :" + PCName);
                  //  Session["ComputerName"] = PCName;


                    String ThisMachine;
                    ThisMachine = GetComputerName(GetIPAddress()); 

                    vlDeliveryStation12.Text = ThisMachine;

                    List<string> arrResult = new List<string>();
                    ClassDB db = new ClassDB();
                    DataTable dt = new DataTable();
                    string req_num = (String)Session["RequisitionNumber"];

                    dt = Objrun.getRequisitionInfo(req_num);
                    if (dt.Rows.Count != 0)
                    {
                        if (dt.Rows[0]["REQ_STATUS"].ToString() == "APPROVED")
                        {
                            arrResult = db.StartPicking(Convert.ToInt32(req_num), "", (UserAD)Session["UserAD"], "", "", "", "");

                            if (arrResult[0] == "OK")
                            {
                                lblMSG.Text = "";
                                lblMSG.Visible = false;
                            }
                            else
                            {
                                lblMSG.Visible = false;
                                lblMSG.Text = arrResult[0] + " : " + arrResult[1];
                            }
                        }
                    }




                }

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
                    htmlStr += "<tr class='auto-style24'><td class='auto-style26'>" + DT.Rows[i][0].ToString().Trim() + "</td><td class='auto-style25'>" + DT.Rows[i][1].ToString().Trim() + "</td><td class='auto-style25'>" + DT.Rows[i]["SHELF"].ToString().Trim() + "</td><td class='auto-style27 col-h'>" + DT.Rows[i]["EXPIRE"].ToString().Trim() + "</td><td class='auto-style26 '>" + DT.Rows[i][2].ToString().Trim() + "</td><td class='auto-style27'>" + DT.Rows[i][3].ToString() + "</td><td class=' auto-style27 '><input type='number' class='form-control form-control-sm numberonly '   id='txtPick_" + DT.Rows[i][0].ToString().Trim() + "' placeholder='##' value='" + (DT.Rows[i]["ACTUAL"].ToString().Trim()== "0" ? "": DT.Rows[i]["ACTUAL"].ToString().Trim()) + "' ></td><td class='auto-style27 col-h'><input class='form-control' type='hidden'  id='txtReqNo_" + DT.Rows[i][0].ToString().Trim() + "' value='" + (String)Session["RequisitionNumber"] + "' ></td> </tr>";
                    x = x + Convert.ToDouble(DT.Rows[i][3].ToString().Trim());
                }

                htmlStr += "<tr class='auto-style24'><td></td><td></td><td></td><td class='col-h'></td> <td class='auto-style27' ><strong><span class='auto-style27'>Total</span> : </strong></td><td class='auto-style27'>" + x.ToString("#,##0.00") + "</td><td id='spTotal' class='auto-style27'> </td><td  class='col-h'>&nbsp;&nbsp;&nbsp;</td></tr>";
                //htmlStr += "<tr class='auto-style24'><td class='auto-style27' colspan='5' rowspan='0'><strong><span class='auto-style27'>Total</span> : </strong></td><td class='auto-style27'>" + x.ToString("#,##0.00") + "</td><td class='col-h'>&nbsp;&nbsp;&nbsp;</td><td  class='col-h'>&nbsp;&nbsp;&nbsp;</td></tr>";
            }


            return htmlStr;
        }


        public string GetComputerName(string clientIP)
        {
            try
            {
                var hostEntry = Dns.GetHostEntry(clientIP);
                return hostEntry.HostName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public static string GetIPAddress()
        {
            return GetIPAddress(new HttpRequestWrapper(HttpContext.Current.Request));
        }

        internal static string GetIPAddress(HttpRequestBase request)
        {
            // handle standardized 'Forwarded' header
            string forwarded = request.Headers["Forwarded"];
            if (!String.IsNullOrEmpty(forwarded))
            {
                foreach (string segment in forwarded.Split(',')[0].Split(';'))
                {
                    string[] pair = segment.Trim().Split('=');
                    if (pair.Length == 2 && pair[0].Equals("for", StringComparison.OrdinalIgnoreCase))
                    {
                        string ip = pair[1].Trim('"');

                        // IPv6 addresses are always enclosed in square brackets
                        int left = ip.IndexOf('['), right = ip.IndexOf(']');
                        if (left == 0 && right > 0)
                        {
                            return ip.Substring(1, right - 1);
                        }

                        // strip port of IPv4 addresses
                        int colon = ip.IndexOf(':');
                        if (colon != -1)
                        {
                            return ip.Substring(0, colon);
                        }

                        // this will return IPv4, "unknown", and obfuscated addresses
                        return ip;
                    }
                }
            }

            // handle non-standardized 'X-Forwarded-For' header
            string xForwardedFor = request.Headers["X-Forwarded-For"];
            if (!String.IsNullOrEmpty(xForwardedFor))
            {
                return xForwardedFor.Split(',')[0];
            }

            return request.UserHostAddress;
        }
    }
}