using StoreRequisition.Class;
using StoreRequisition.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreRequisition.Page
{
    public partial class Frm_Login_R : System.Web.UI.Page
    {
        ClassDB db = new ClassDB();
        clsAuthenticate aAuthent = new clsAuthenticate();

        protected void Page_Load(object sender, EventArgs e)
        {
            IDictionaryEnumerator allCaches = HttpRuntime.Cache.GetEnumerator();

            while (allCaches.MoveNext())
            {
                Cache.Remove(allCaches.Key.ToString());
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //dt = new DataTable();
            //db.TSql = "APPS.MMCT_ORA_USER.VALIDATED_USERID";
            //dt = db.ValidatedUserId(txtUsername.Text, txtPassword.Text);
            //if (dt.Rows.Count > 0)
            //{

            //}
            //else
            //{

            //}


        }



        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!aAuthent.SetDomain("mmct_domain"))
            {
                lblMSG.Text = "โดมเมนในระบบผิดพลาด โปรดแจ้งผู้ดูแลระบบ";
                return;
                // throw new Exception("โดมเมนในระบบผิดพลาด โปรดแจ้งผู้ดูแลระบบ");
            }

            // check if user supplied username properly
            //if (!aAuthent.SetUser(txt_UserName.Text.ToLower()))
            if (!aAuthent.SetUser(txtUsername.Text.ToLower()))
            {
                throw new Exception("Please provide an username");
                //Login1.InstructionText = "Please provide an username"
                //MessageBox.Show("Please provide an username")                
            }

            // now check if password is supplied
            if (!aAuthent.SetPass(txtPassword.Text.Trim()))
            {
                lblMSG.Text = "Please provide a password";
                return;
                //throw new Exception("Please provide a password");
            }

            if (aAuthent.IsAuthenticated("mmct_domain", txtUsername.Text.ToLower(), txtPassword.Text.Trim()) == false)
            {
                clsSQLscript objRun = new clsSQLscript();
                DataTable dt = new DataTable();
                dt = objRun.getuserGuest(txtUsername.Text.ToLower(), txtPassword.Text.Trim().ToLower());
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    UserAD userAD = new UserAD(); 
                    userAD.InitName = dr["ID"].ToString();
                    userAD.EN = dr["EN"].ToString();
                    userAD.Departments = dr["DEPARTMENTS"].ToString();
                    userAD.Authority = "Guest";


                    Session["UserAD"] = userAD;

                    userAD = new UserAD();
                    userAD = (UserAD)Session["UserAD"];
                    if (userAD.InitName == null)
                    {
                        lblMSG.Text = "รหัสผ่านไม่ถูกโปรดใส่ใหม่อีกครั้ง";
                        return;
                    }

                    Session["lblnames"] = userAD.Departments;

                    Response.Redirect("~/Page/Frm_Requisition_List.aspx?jobType=ActOwner");
                }
                else
                {
                    lblMSG.Text = "รหัสผ่านไม่ถูกโปรดใส่ใหม่อีกครั้ง";
                    return;
                }



                // throw new Exception("รหัสผ่านไม่ถูกโปรดใส่ใหม่อีกครั้ง");
            }
            else
            {

                Session["UserAD"] = aAuthent.GetAuthenticated("mmct_domain", txtUsername.Text.ToLower(), txtPassword.Text.Trim());
                UserAD userAD = new UserAD();
                userAD = (UserAD)Session["UserAD"];
                if (userAD.InitName == null)
                {
                    lblMSG.Text = "รหัสผ่านไม่ถูกโปรดใส่ใหม่อีกครั้ง";
                    return;
                }

                Session["lblnames"] = userAD.Departments;
                //else
                //{
                //    Session["UserData"] = aAuthent.GetUserData((string)Session["UserAD"]);
                //}


                txtUsername.Text.ToLower();
                //  Response.Redirect("~/Page/Frm_Send_Requisition.aspx");

                Response.Redirect("~/Page/Frm_Requisition_List.aspx?jobType=ActOwner");
            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }
    }
}