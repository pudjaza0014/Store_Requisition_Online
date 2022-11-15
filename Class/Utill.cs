using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
//using System.Net;
using System.Net.Mail;
using System.Net.Security;
//using System.Security.Cryptography.X509Certificates;


public class Utill
{
    ClassDB db = new ClassDB();
    string strNewDate;
    public string mailFrom { get; set; }
    public string mailTo { get; set; }
    public string mailTo2 { get; set; }
    public string mailCc { get; set; }
    public string mailSubject { get; set; }
    public string mailBody { get; set; }
    public string ErrorMaessage { get; set; }
    public string ApproveName { get; set; }
    public string Attachment1 { get; set; }
    public string Attachment2 { get; set; }
    public string Attachment3 { get; set; }
    public MailMessage MailMsg { get; set; }
    DateTime newdate;

    public DateTime ConvertStringToDate(string pSdate)
    {
        try
        {
            string[] strDate = pSdate.Split('/');
            strNewDate = strDate[1] + "/" + strDate[0] + "/" + strDate[2];
            newdate = Convert.ToDateTime(strNewDate);
        }
        catch (Exception ex)
        {

        }
        return newdate;
    }

    public String StringToDateFormatOnly(string pSdate)
    {

        try
        {
            strNewDate = DateTime.Parse(pSdate).ToShortDateString();
        }
        catch (Exception ex)
        {

        }
        return strNewDate;
    }

    public DateTime AddMonthDateTime(string pSdate, int addMonth)
    {
        DateTime newdate = DateTime.Now;
        try
        {
            string[] strDate = pSdate.Split('-');
            int date = int.Parse(strDate[0]);
            int month = int.Parse(strDate[1]) + addMonth;
            int Year = int.Parse(strDate[2]);

            newdate = new DateTime(Year, month, date, 0, 0, 0);
        }
        catch (Exception ex)
        {

        }
        return newdate;
    }

    public int SentMail()
    {
        int sentComplete = 1;
        try
        {
            MailMessage MailMsg = new MailMessage();

            MailMsg.From = new MailAddress(mailFrom);
            if (mailTo != null)
            {
                MailMsg.To.Add(new MailAddress(mailTo));
            }

            if (mailTo2 != null)
            {
                MailMsg.To.Add(new MailAddress(mailTo2));
            }

            MailMsg.IsBodyHtml = true;
            MailMsg.Subject = mailSubject;
            MailMsg.Body = mailBody;

            //System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment(Attachment1);
            //myMail.Attachments.Add(attachment);

            //System.Net.Mail.Attachment attachment2;
            //attachment2 = new System.Net.Mail.Attachment(Attachment2);
            //myMail.Attachments.Add(attachment2);

            //System.Net.Mail.Attachment attachment3;
            //attachment3 = new System.Net.Mail.Attachment(Attachment3);
            //myMail.Attachments.Add(attachment3);

            SmtpClient smtp = new SmtpClient("cpt-mail.compart-grp.com", 25);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = true;
            smtp.Send(MailMsg);
            sentComplete = 1;
        }
        catch (Exception ex)
        {
            ErrorMaessage = ex.Message.ToString();
            sentComplete = 0;
        }
        return sentComplete;
    }
    public string GetApproveName(int EmployeeId)
    {
        //DataTable dt = new DataTable();
        //db.TSql = "exec sps_GetEmployeeDetail '" + EmployeeId + "'";
        //dt = db.GetData();

        //if (dt.Rows.Count > 0)
        //{
        //    ApproveName = dt.Rows[0]["NameEn"].ToString();
        //    //txtDepartMent.Text = dt.Rows[0]["Cmb1NameT"].ToString();
        //    //txtDepartmentID.Text = dt.Rows[0]["cmb1ID"].ToString();
        //}
        return ApproveName;
    }
}