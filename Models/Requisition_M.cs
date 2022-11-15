using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreRequisition.Models
{
    public class Requisition_M
    {
        public string ResultStatus { get; set; }
        public string ErrorMsg { get; set; }

        public IEnumerable<Requisition_ListM> requisition_Lists { get; set; }
        public IEnumerable<ProcessGroup> processGroups { get; set; }
    }

    public class ProcessGroup
    {
        public string ProcessCode { get; set; }
        public string ProcessName { get; set; }
        public string processColor { get; set; }
        public int RequisitionAmount { get; set; }
    }


    



}