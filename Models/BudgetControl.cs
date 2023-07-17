using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreRequisition.Models
{
    public class BudgetControl
    {
        public string EN { get; set; }
        public DateTime Client_Time { get; set; }
        public string Operation { get; set; }
        public BudgetControlPeriod Period { get; set; }
        public BudgetControlTrans transection { get; set; }

    }
    public class BudgetControl_Display
    {
        public string EN { get; set; }
        public DateTime Client_Time { get; set; }
        public string Operation { get; set; }
        public BudgetControlPeriod_display Period { get; set; }
        public List<BudgetControlTrans_Display> transection { get; set; }

    }

    public class BudgetControlPeriod
    {
        public string PerioldName { get; set; }
        public string BudgetName { get; set; }
        public decimal InitialBudgetAmount { get; set; }
        public decimal AdditionalBudgetAmount { get; set; }
        public decimal ReduceBudgetAmount { get; set; }
        public decimal RequisitionUseAmount { get; set; }
        public decimal IssueUseAmount { get; set; }
        public decimal AvailableBudget { get; set; }
        public string Remark { get; set; }
        public string Operator { get; set; }
    }

    public class BudgetControlPeriod_display
    {
        public string PerioldName { get; set; }
        public string BudgetName { get; set; }
        public string InitialBudgetAmount { get; set; }
        public string AdditionalBudgetAmount { get; set; }
        public string ReduceBudgetAmount { get; set; }
        public string totalBudgetAmount { get; set; }
        public string RequisitionUseAmount { get; set; }
        public string IssueUseAmount { get; set; }
        public string totaluseAmount { get; set; }
        public string AvailableBudget { get; set; }
        public string Remark { get; set; }
        public string Operator { get; set; }
    }

    public class BudgetControlTrans
    {
        public DateTime DocumentTransDate { get; set; }
        public string DocumentNumber { get; set; }
        public decimal Amount { get; set; }
        public string Document_Type { get; set; } = "BUDGET";
        public string Transaction_Type { get; set; }
        public string Action { get; set; }
        public string ReferDocumentNumber { get; set; }
        public string Remark { get; set; }
    }

    public class BudgetControlTrans_Display
    {
        public string DocumentTransDate { get; set; }
        public string DocumentNumber { get; set; }
        public string Amount { get; set; }
        public string Document_Type { get; set; } = "BUDGET";
        public string Transaction_Type { get; set; }
        public string Action { get; set; }
        public string ReferDocumentNumber { get; set; }
        public string Remark { get; set; }
    }




}