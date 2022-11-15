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
    public partial class Frm_Report_R : System.Web.UI.Page
    {
        clsSQLscript Objrun = new clsSQLscript();
        DataTable dtReq = new DataTable();
        public string Authority { get; set; }
        Employee employee = new Employee();
        clsAuthenticate aAuthent = new clsAuthenticate();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserAD"] == null)
            { 
                Response.Redirect("~/Page/Frm_Login_R.aspx");
            }
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
                vlApprovedBy.Text = dt.Rows[0]["REQ_APPROVE_NAME_BY"].ToString();
                vlRequestBy.Text = dt.Rows[0]["REQ_BY"].ToString();
                vlRequestTel.Text = dt.Rows[0]["REQ_BY_TEL"].ToString();
                vlRequestTel.Text = dt.Rows[0]["REQ_BY_TEL"].ToString();
                vlRequestDate.Text = dt.Rows[0]["REQ_DATE"].ToString();
                vlRequestTime.Text = dt.Rows[0]["REQ_TIME"].ToString();
                vlIssueType.Text = dt.Rows[0]["ISSUE_TYPE"].ToString();
                vlSubInventory.Text = dt.Rows[0]["SUB_INV"].ToString();
                vlRemark.Text = dt.Rows[0]["REQ_REMARK"].ToString();
                vStatus.Text = dt.Rows[0]["PROCESS_NAME"].ToString();
                // vStatus2.Text = dt.Rows[0]["PROCESS_NAME"].ToString();

                int Seq_num = Convert.ToInt32( ( dt.Rows[0]["SEQ_NO"].ToString()!=""? dt.Rows[0]["SEQ_NO"].ToString() : "00"));

                dtReq = dt;

                UserAD userAD = (UserAD)Session["UserAD"];
                UserAD userAD_EN = new UserAD();
                clsAuthenticate aAuthent = new clsAuthenticate();
                userAD_EN = aAuthent.getUserAD(dt.Rows[0]["REQ_APPROVE_BY"].ToString());
                dt = new DataTable();
                dt = Objrun.GetUsersData(userAD_EN.EN);
                if (Seq_num >20)
                {
                    vlApprovedLicense.Visible = true;
                    vlApprovedLicense.Text = dt.Rows[0]["EMP_NAME_ENG"].ToString();
                    vlApprovedBy.Text = "( " + dt.Rows[0]["EMP_NAME_ENG"].ToString() + " )";
                }
                else if(Seq_num > 10)
                {
                      vlApprovedLicense.Visible = true;
                        vlApprovedLicense.Text = "__________________";
                        vlApprovedBy.Text = "( " + dt.Rows[0]["EMP_NAME_ENG"].ToString() + " )";
                    
                }
                else
                {
                    vlApprovedLicense.Visible = true;
                    vlApprovedLicense.Text = "__________________";
                    vlApprovedBy.Text = "( APPROVER )";
                }
              

            }
            catch (Exception ex )
            {
                vlRemark.Text = ex.Message.ToString();
                //throw;
            }
        }


        public string getAuthority()
        {
            try
            {
                string strReturn = null;
                DataTable dt = new DataTable();
                clsSQLscript Objrun = new clsSQLscript();
                Employee employee = new Employee();

                UserAD userAD = (UserAD)Session["UserAD"];

                if (userAD.Departments.ToLower()!=null)
                {
                    if (userAD.Departments.ToLower() == "packing")
                    {
                        // strReturn = userAD.Departments.ToLower();
                        strReturn = (chkApprover(userAD.Departments.ToLower()) == true ? "packing" : "");
                    }
                    else if (userAD.Departments.ToLower() == "store")
                    {
                        // strReturn = userAD.Departments.ToLower();
                       // string ChkProgress = chkProgress(userAD.InitName);
                        string ChkProgress = chkProgress(userAD.Departments.ToLower());
                        if (ChkProgress == "approved")
                        {
                            strReturn = (chkApprover(userAD.Departments.ToLower()) == true ? "store" : "");
                        }
                        else
                        {
                            ChkProgress = chkProgress(userAD.InitName.ToLower());
                            if (ChkProgress == "approved")
                            {
                                strReturn = (chkApprover(userAD.Departments.ToLower()) == true ? "store" : "");
                            }
                            else
                            {
                                strReturn = ChkProgress;
                            }
                        };
                    }
                    else
                    {


                        //  strReturn = (chkProgress(userAD.InitName) == true ? "approved" : "");
                        strReturn = (chkProgress(userAD.InitName));
                        // strReturn = (chkApprover(userAD.InitName) == true ? "approved" : "");
                    }
                }
                return strReturn;
            }
            catch (Exception e)
            {
                string ex = e.Message;
                string script = "alert('" + ex + "'); console.log('"+ ex+"')";
                ClientScript.RegisterStartupScript(this.GetType(), "UpdateTime", script, true);
                return "";
             //   throw;
            } 
        } 
        public Boolean chkApprover(string InitName)
        {
            DataTable dt = new DataTable();
            Boolean results = false;
            try
            {
                dt = new DataTable();
                dt = Objrun.getStore_info_approved((string)Session["RequisitionNumber"], InitName);
                if (dt.Rows.Count > 0)
                {
                    results = true;
                }               
            }
            catch (Exception)
            {
                throw;
            }
            return results;
        }


        public string chkProgress(string InitName)
        {
            DataTable dt = new DataTable();
            // Boolean results = false;
            string results = "";
            try
            {

                dt = new DataTable();
                dt = Objrun.getStore_info_approved((string)Session["RequisitionNumber"], InitName);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["PROCESS_NAME"].ToString()=="ISSUE")
                    {
                        results = "issue";
                    }
                    else
                    {
                        results = "approved";
                    } 
                }
            }
            catch (Exception)
            {

                throw;
            }
            return results;
        }


        [WebMethod]
        public static IEnumerable<string> ConfirmDecision(string ddl_decision , string txt_remark)
        {
            List<string> arrResult = new List<string>();
            try
            {
                ClassDB db = new ClassDB();

                db.TSql = "APPS.MMT_STORE_PROGRESS_MAKING";
                arrResult = null;
                arrResult = db.Requisition_Progress_Making(Convert.ToInt32((string)HttpContext.Current.Session["RequisitionNumber"]), "", (UserAD)HttpContext.Current.Session["UserAD"], ddl_decision.ToUpper(), txt_remark, "", "");

            }
            catch (Exception ex)
            {
                arrResult = new List<string>();
                arrResult.Add("ERROR");
                arrResult.Add(ex.Message.ToString()); 
               //arrResult = {"ERORR", ex.Message.ToString()];

                
            }
            return arrResult;

        }

        [WebMethod]
        public static IEnumerable<string> SaveItemsPicked(List<PickingItems> pickingItems)
        {
            List<string> arrResult = new List<string>();
            try
            {
                //Employee employee = new Employee(); 
                //employee = (Employee)HttpContext.Current.Session["UserData"];
                UserAD userAD = (UserAD)HttpContext.Current.Session["UserAD"];
                ClassDB db = new ClassDB();
                arrResult = db.RequisitionSaveItemsPicked(userAD, pickingItems);
            }
            catch (Exception ex)
            {
                arrResult = new List<string>();
                arrResult.Add("ERROR");
                arrResult.Add(ex.Message.ToString());
            }
            return arrResult;
        }

        [WebMethod]
        public static IEnumerable<string> SaveItem(List<PickingItems> pickingItems)
        {
            List<string> arrResult = new List<string>();
            try
            {
                //Employee employee = new Employee(); 
                //employee = (Employee)HttpContext.Current.Session["UserData"];
                UserAD userAD = (UserAD)HttpContext.Current.Session["UserAD"];
                ClassDB db = new ClassDB();
                arrResult = db.RequisitionSaveItem(userAD, pickingItems);
            }
            catch (Exception ex)
            {
                arrResult = new List<string>();
                arrResult.Add("ERROR");
                arrResult.Add(ex.Message.ToString());
            }
            return arrResult;
        }

        [WebMethod]
        public static IEnumerable<string> CanclePicking()
        {
            // string test = "123";
            List<string> arrResult = new List<string>();
            try
            {

                UserAD userAD = (UserAD)HttpContext.Current.Session["UserAD"];
                string req_num = (string)HttpContext.Current.Session["RequisitionNumber"];


                ClassDB db = new ClassDB();
                arrResult = db.Requisition_cancel_picking(userAD, req_num);
            }
            catch (Exception ex)
            {
                
                arrResult.Add("ERROR");
                arrResult.Add(ex.Message.ToString());
            }
            return arrResult;


        }

        [WebMethod]
        public static IEnumerable<string> StoreEdititemQty(List<PickingItems> pickingItems)
        {
            List<string> arrResult = new List<string>();
            try
            {
                //Employee employee = new Employee(); 
                //employee = (Employee)HttpContext.Current.Session["UserData"];
                UserAD userAD = (UserAD)HttpContext.Current.Session["UserAD"];
                ClassDB db = new ClassDB();
                arrResult = db.RequisitionSaveItem(userAD, pickingItems);
            }
            catch (Exception ex)
            {
                arrResult = new List<string>();
                arrResult.Add("ERROR");
                arrResult.Add(ex.Message.ToString());
            }
            return arrResult;
        }




        [WebMethod]
        public static IEnumerable<string> storeIssue()
        {
            // string test = "123";
            List<string> arrResult = new List<string>();
            try
            {

                UserAD userAD = (UserAD)HttpContext.Current.Session["UserAD"];
                string req_num = (string)HttpContext.Current.Session["RequisitionNumber"];

              
                ClassDB db = new ClassDB();
                arrResult = db.StartPicking(Convert.ToInt32(req_num), "", userAD, "", "", "", "");

                // arrResult = db.Requisition_cancel_picking(userAD, req_num);
            }
            catch (Exception ex)
            {

                arrResult.Add("ERROR");
                arrResult.Add(ex.Message.ToString());
            }
            return arrResult; 
        }



        public string getREQ_NUM()
        {
            string REQ_NUM = (string)Session["RequisitionNumber"];

            return REQ_NUM;
        }
    }
}