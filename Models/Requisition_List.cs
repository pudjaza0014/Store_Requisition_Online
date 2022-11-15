using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreRequisition.Models
{
    public class Requisition_List
    {
        public int REQ_NUM { get; set; }
        public string REQ_BY { get; set; }
        public string SEQ_APPROVED_BY { get; set; }
        public string APPROVED_BY { get; set; }
        public string TRANSFER_TO { get; set; }


        public string STATUS { get; set; }
        public string SUB_INV { get; set; }
        public string DELIVERY_STATE { get; set; }
        public string STATE_NEW { get; set; }
        public string REQ_APPROVE_BY { get; set; }
        public string PACKING { get; set; }

        public string PROCESS_COLORS { get; set; }


    }

    public class Requisition_ListM
    {
        public int REQ_NUM { get; set; }
        public DateTime REQ_DATE { get; set; }
        public string REQ_TIME { get; set; }
        public string APPROVED_BY { get; set; }
        public string LOCATION { get; set; }
        public string SUB_INV { get; set; }
        public string DELIVERY_STATE { get; set; }
        public string TRANSFER_TO { get; set; }
        public string STATE_NEW { get; set; }
        public string STATUS { get; set; }
        public string PROCESS_COLORS { get; set; }
    }
}