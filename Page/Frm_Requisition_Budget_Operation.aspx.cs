using StoreRequisition.Class;
using StoreRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreRequisition.Page
{
    public partial class Frm_Requisition_Budget_Operation : System.Web.UI.Page
    {
        public static string strOperation;
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                if (Session["UserAD"] == null)
                {
                    Response.Redirect("~/Page/Frm_Login_R.aspx");
                }
                string assemblyVersion = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
                strOperation = Request.QueryString["jobType"].ToString();
                string strOperater = string.Empty;
                 

                lblpageName.Text = strOperation + " budget period";
            }
        }
        public static string GetOperation()
        {
            return strOperation;
        }

        [WebMethod]
        public static IEnumerable<string> getBudgetNameAll()
        {
            DataTable dt = new DataTable();
            try
            {
                clsSQLscript Objrun = new clsSQLscript();
                dt = Objrun.getBudgetNameAll();


                List<string> list = new List<string>();
                foreach (DataRow item in dt.Rows)
                {
                    list.Add(item["FLEX_VALUE"].ToString());
                }

                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [WebMethod]
        public static string SaveNewData(string param1, string param2, string param3 ,string param4 ,string param5)
        {
            string strResult = "OK",
                Operator = string.Empty,
                OperatorDept = string.Empty;

            string PerioldName = param1.Trim(),
                    BudgetName = param2.Trim().ToUpper(),
                    Operation = param4?? GetOperation(),
                    Actions = string.Empty,
                    D_Remark = (param5??"").Trim();

            decimal BudgetAmount = Convert.ToDecimal(param3.Replace(",", ""));
            clsSQLscript Objrun = new clsSQLscript();
            ClassDB objClassDB = new ClassDB();
            BudgetControl budgetControl;
            BudgetControlTrans trans;
            BudgetControlPeriod period;

            try
            {
                UserAD userAD = (UserAD)HttpContext.Current.Session["UserAD"];
                if (userAD != null)
                {
                    Operator = userAD.EN.ToUpper();
                    OperatorDept = userAD.Departments.ToLower();
                }
                

                period = new BudgetControlPeriod()
                {
                    PerioldName = PerioldName,
                    BudgetName = BudgetName,
                    InitialBudgetAmount = BudgetAmount,
                    Operator = Operator,
                };

                switch (Operation)
                {
                    case "New":
                        Actions ="BUDGET INITIAL";
                        break;
                    case "ADDITIONAL":
                        Actions = "BUDGET ADD";
                        break;
                    case "REDUCE":
                        Actions = "BUDGET REDUCE";
                        break;                 }
                 
                trans = new BudgetControlTrans()
                {
                    DocumentTransDate = DateTime.Now,
                    DocumentNumber = PerioldName + "-" + BudgetName,
                    Amount = BudgetAmount,
                    Action = Actions,
                    Remark = D_Remark

                };

                budgetControl = new BudgetControl()
                {
                    EN = Operator,
                    Client_Time = DateTime.Now,
                    Operation = Operation,
                    Period = period,
                    transection = trans
                };

                objClassDB.BudgetControl_Operation(budgetControl);
                return strResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [WebMethod]
        public static BudgetControl_Display GetDataBudget_Detail(string PeriodName , string BudgetName)
        {
            DataTable dt = new DataTable();
            clsSQLscript objRun = new clsSQLscript(); 


            try
            {
                dt= objRun.getBudgetPeriod(PeriodName, BudgetName);



                if (dt.Rows.Count == 0)
                {
                    throw new Exception($"PeriodName :'{PeriodName}'\r\nBudgetName: '{BudgetName}'\r\nIs not found Please go to Home Page");
                }
                DataRow item = dt.Rows[0];
                decimal TotalBudget = Convert.ToDecimal(item["INITIAL_BUDGET_AMOUNT"]) + Convert.ToDecimal(item["ADDITIONAL_BUDGET_AMOUNT"]) - Convert.ToDecimal(item["REDUCE_BUDGET_AMOUNT"]),
                    TotalUse = Convert.ToDecimal(item["REQUISITION_USE_AMOUNT"]) - Convert.ToDecimal(item["ISSUE_SLIP_USE_AMOUNT"]),
                    Avai_amount = TotalBudget - TotalUse;
                BudgetControlPeriod_display period = new BudgetControlPeriod_display()
                {
                    PerioldName = item["PERIOD_NAME"].ToString(),
                    BudgetName = item["BUDGET_NAME"].ToString(),
                    InitialBudgetAmount = Convert.ToDecimal(item["INITIAL_BUDGET_AMOUNT"]).ToString("N2"),
                    AdditionalBudgetAmount = Convert.ToDecimal(item["ADDITIONAL_BUDGET_AMOUNT"]).ToString("N2"),
                    ReduceBudgetAmount = Convert.ToDecimal(item["REDUCE_BUDGET_AMOUNT"]).ToString("N2"),
                    totalBudgetAmount = TotalBudget.ToString("N2"),
                    RequisitionUseAmount = Convert.ToDecimal(item["REQUISITION_USE_AMOUNT"]).ToString("N2"),
                    IssueUseAmount = Convert.ToDecimal(item["ISSUE_SLIP_USE_AMOUNT"]).ToString("N2"),
                    totaluseAmount = TotalUse.ToString("N2"),

                    AvailableBudget = Avai_amount.ToString("N2"),
                    Remark = item["PERIOD_REMARK"].ToString(),
                    Operator = item["UPD_DATE"].ToString(),
                }; 
                dt = new DataTable();
                dt = objRun.getBudgetTrans(PeriodName, BudgetName);
                List<BudgetControlTrans_Display> trans = new List<BudgetControlTrans_Display>();
                foreach (DataRow i in dt.Rows)
                {
                    trans.Add(new BudgetControlTrans_Display()
                    {
                        DocumentTransDate = Convert.ToDateTime(i["DOCUMENT_TRANSACTION_DATE"]).ToString("dd MMM yyyy HH:mm:ss"),
                        DocumentNumber = i["DOCUMENT_NUMBER"].ToString(),
                        Amount = Convert.ToDecimal(i["DOCUMENT_AMOUNT"]).ToString("N2"),
                        Document_Type = i["DOCUMENT_TYPE"].ToString(),
                        Transaction_Type = i["DOCUMENT_TRANSACTION_TYPE"].ToString(),
                        Action = i["DOCUMENT_ACTION"].ToString(),
                        ReferDocumentNumber = i["REFER_DOCUMENT_NUMBER"].ToString(),

                    }); ;
                }
                BudgetControl_Display budget = new BudgetControl_Display()
                {
                    Period = period,
                    transection = trans
                };
                 
                return budget;


            }
            catch (Exception)
            {

                throw;
            }
        }


        public string getjobtype()
        {
            return strOperation;
        }
    }
}