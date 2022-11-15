using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using StoreRequisition.Class;
using StoreRequisition.Models;
using System.Security.Cryptography;
using System.Web.Script.Services;


namespace StoreRequisition.Page
{

    public partial class Frm_Requisition_List : System.Web.UI.Page
    {

        public string jobType { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserAD"] == null)
            {
                //Response.Redirect("~/Page/Frm_Login.aspx");
                Response.Redirect("~/Page/Frm_Login_R.aspx");
            }

        jobType = Request.QueryString["jobType"];

            string pageName = string.Empty;

            switch (jobType)
            {
                case "ActOwner":
                    pageName = " My Activity Owner";
                    break;
                case "jobOwner":
                    pageName = "My job Owner";
                    break;
                default:
                    pageName = "All job Request";
                    break;
            }


            lblpageName.Text = pageName;


        }


        public Boolean CheckingGuest()
        {
            Boolean results = true;

            if (Session["UserAD"] == null)
            { 
                Response.Redirect("~/Page/Frm_Login_R.aspx");
                results = false;
            }
            else
            {
                UserAD userAD = new UserAD();
                userAD = (UserAD)Session["UserAD"];

                if (userAD.Authority == "Guest")
                {
                    results = false;
                }
            }



            return results;

        }


        [WebMethod] 
        public static IEnumerable<Requisition_List> GetData(String param1)
        {
            HttpContext.Current.Session["author"] = param1;
            clsSQLscript Objrun = new clsSQLscript();
            DataTable dt = new DataTable();
            clsAuthenticate aAuthent = new clsAuthenticate();
            UserAD userAD = (UserAD)HttpContext.Current.Session["UserAD"];
            dt = Objrun.GetRequisitionListAll(param1,  userAD);
            List<Requisition_List> Results = new List<Requisition_List>();

            Requisition_List Result = new Requisition_List();

            foreach (DataRow row in dt.Rows)
            { 
                string Packing_by = "";
                if (userAD.Departments.ToLower() == row["ACTIVITY_EN"].ToString())
                {
                    if (row["PROGRESS"].ToString() == "APPROVED")
                    {
                        Packing_by = row["ACTIVITY_EN"].ToString();
                    }
                    else if (row["PROGRESS"].ToString() == "ON PROCESS PICKING")
                    {
                        Packing_by = "on Process";
                    }

                }


                string Approve_by = "";
                if (userAD.InitName == row["ACTIVITY_EN"].ToString())
                {
                    if (row["PROGRESS"].ToString() == "ISSUE")
                    {
                        Approve_by = "issue";
                    }
                    else {
                        Approve_by = row["ACTIVITY_EN"].ToString();
                    }

                }



              //  string Approve_by = (userAD.InitName == row["ACTIVITY_EN"].ToString() ? row["ACTIVITY_EN"].ToString() : "");
                Results.Add(new Requisition_List()
                {
                    REQ_NUM = Convert.ToInt32(row["REQ_NUM"]),
                    //  REQ_BY = row["REQ_BY"].ToString(),
                    //SEQ_APPROVED_BY = row["APPROVE_BY"].ToString(),
                    APPROVED_BY = row["APPROVE_BY"].ToString(),
                    SUB_INV = row["SUB_INV"].ToString(),
                    TRANSFER_TO = row["TRANSFER_TO"].ToString(),
                    STATUS = row["PROGRESS"].ToString(),
                    STATE_NEW = row["StateNew"].ToString(),
                    DELIVERY_STATE = row["DELIVERY_STATION"].ToString(),
                    REQ_APPROVE_BY = Approve_by,
                    PACKING = Packing_by
                });
            }
            return Results.ToList();
        }



        [WebMethod]
        public  static IEnumerable<Requisition_List> GetDataXR(String param1, String param2 ,String param3)
        {
            HttpContext.Current.Session["author"] = param1;
            clsSQLscript Objrun = new clsSQLscript();
            DataTable dt = new DataTable();
            clsAuthenticate aAuthent = new clsAuthenticate();


            try
            {

            


            UserAD userAD = (UserAD)HttpContext.Current.Session["UserAD"];
                List<Requisition_List> Results = new List<Requisition_List>();

                if (userAD == null)
                {
                  return  Results.ToList();
                }
            dt = Objrun.GetRequisitionListAll(param1, param2, param3, userAD);
            

            Requisition_List Result = new Requisition_List();

            foreach (DataRow row in dt.Rows)
            {
                string Packing_by = "";
                if (userAD.Departments.ToLower() == row["ACTIVITY_EN"].ToString())
                {
                    if (row["PROGRESS"].ToString() == "APPROVED")
                    {
                        // Packing_by = row["ACTIVITY_EN"].ToString();
                        Packing_by = "<a href='Frm_report_R.aspx?RequestNo="+ row["REQ_NUM"].ToString() + "' class='btn btn-warning  mb-1 mb-lg-0 '><i class='fas fa-cart-arrow-down'></i> Picking</a>";
                    }
                    else if (row["PROGRESS"].ToString() == "ON PROCESS PICKING")
                    {
                        // Packing_by = "on Process";

                        Packing_by = "<a href='Frm_report_R.aspx?RequestNo=" + row["REQ_NUM"].ToString() + "' class='btn btn-warning  mb-1 mb-lg-0 disabled'  ><i class='fas fa-cart-arrow-down' ></i>in Process Picking</a>";
                    }

                }

                string Approve_by = "";
                if (userAD.InitName == row["ACTIVITY_EN"].ToString())
                {
                    if (row["PROGRESS"].ToString() == "ISSUE")
                    {                      //   Approve_by = "issue";
                        Approve_by = "<a href='Frm_report_R.aspx?RequestNo=" + row["REQ_NUM"].ToString() + "' class='btn btn-danger mb-1 mb-lg-0'><i class='fas fa-dolly-flatbed'></i> Recieve</a>";
                    }
                    else
                    {
                        // Approve_by = row["ACTIVITY_EN"].ToString();
                        Approve_by = "<a href='Frm_report_R.aspx?RequestNo=" + row["REQ_NUM"].ToString() + "' class='btn btn-primary mb-1 mb-lg-0'><i class='fas fa-file-signature'></i> Approve</a>";
                    }

                }


                if(param1 == "ReversePicking" && userAD.Departments.ToLower()=="store" && row["PROGRESS"].ToString() == "ON PROCESS PICKING")
                {
                    Approve_by = "<a href='#' onClick='ReversePicking(" + row["REQ_NUM"].ToString() + ")' class='btn btn-danger mb-1 mb-lg-0'><i class='fas fa-history'></i> REVERSE</a>";

                }

                string Packing_flag = (row["PROGRESS"].ToString() == "PACKING" ? "1" : "0");
              //  string Approve_by = (userAD.InitName == row["ACTIVITY_EN"].ToString() ? row["ACTIVITY_EN"].ToString() : "");
                Results.Add(new Requisition_List()
                {
                    REQ_NUM = Convert.ToInt32(row["REQ_NUM"]),
                    //  REQ_BY = row["REQ_BY"].ToString(),
                    //SEQ_APPROVED_BY = row["APPROVE_BY"].ToString(),
                    APPROVED_BY = row["APPROVE_BY"].ToString(),
                    SUB_INV = row["SUB_INV"].ToString(),
                    TRANSFER_TO = row["TRANSFER_TO"].ToString(),
                    STATUS = row["PROGRESS"].ToString(),
                    STATE_NEW = row["StateNew"].ToString(),
                    DELIVERY_STATE = row["DELIVERY_STATION"].ToString(),
                    REQ_APPROVE_BY = Approve_by,
                    PACKING = Packing_by
                });
            }
            return Results.ToList();

            }
            catch (Exception ex)
            {
                HttpContext.Current.Session.Clear(); 
                throw;
            }
        }


        [WebMethod]
        public static IEnumerable<string> ReversePickingProcess(string Requisition)
        {
            try
            {
                List<string> arrResult = new List<string>();
                try
                {

                    UserAD userAD = (UserAD)HttpContext.Current.Session["UserAD"];
                    //string req_num = (string)HttpContext.Current.Session["RequisitionNumber"];
                    string req_num = Requisition;

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
            catch (Exception ex)
            {

                throw;
            }
        }


        [WebMethod]
        public static Boolean SignOut()
        {

            HttpContext.Current.Session.Clear();

            return true;

        }


    }
}