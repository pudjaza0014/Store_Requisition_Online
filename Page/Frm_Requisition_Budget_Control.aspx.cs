using iTextSharp.text.pdf;
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
    public partial class Frm_Requisition_Budget_Control : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             
            if (!IsPostBack)
            { 
                if (Session["UserAD"] == null)
                {
                    Response.Redirect("~/Page/Frm_Login_R.aspx");
                }
                string assemblyVersion = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
                lblpageName.Text = assemblyVersion;
            }
        }

        [WebMethod]
        public static List<BudgetControlPeriod_display> getData_PeriodList(string param1)
        {
            string PeriodName = param1.Trim();
            List<BudgetControlPeriod_display> data_ = new List<BudgetControlPeriod_display>();
            clsSQLscript Objrun = new clsSQLscript();
            DataTable dt = new DataTable();
            try
            {
                dt = Objrun.getBudgetPeriodList(PeriodName);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        decimal TotalBudget = Convert.ToDecimal(item["INITIAL_BUDGET_AMOUNT"]) + Convert.ToDecimal(item["ADDITIONAL_BUDGET_AMOUNT"]) - Convert.ToDecimal(item["REDUCE_BUDGET_AMOUNT"]),
                            TotalUse = Convert.ToDecimal(item["REQUISITION_USE_AMOUNT"]) - Convert.ToDecimal(item["ISSUE_SLIP_USE_AMOUNT"]),
                            Avai_amount = TotalBudget - TotalUse;


                        data_.Add(new BudgetControlPeriod_display()
                        {
                            PerioldName = item["PERIOD_NAME"].ToString(),
                            BudgetName = item["BUDGET_NAME"].ToString(),
                            InitialBudgetAmount = Convert.ToDecimal(item["INITIAL_BUDGET_AMOUNT"]).ToString("N2"),
                            AdditionalBudgetAmount = Convert.ToDecimal(item["ADDITIONAL_BUDGET_AMOUNT"]).ToString("N2"),
                            ReduceBudgetAmount = Convert.ToDecimal(item["REDUCE_BUDGET_AMOUNT"]).ToString("N2"),
                            RequisitionUseAmount = Convert.ToDecimal(item["REQUISITION_USE_AMOUNT"]).ToString("N2"),
                            IssueUseAmount = Convert.ToDecimal(item["ISSUE_SLIP_USE_AMOUNT"]).ToString("N2"),
                            AvailableBudget = Avai_amount.ToString("N2"),
                            Remark = item["PERIOD_REMARK"].ToString(),
                            Operator = item["UPD_DATE"].ToString(),
                        });




                    }
                }





                return data_;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}