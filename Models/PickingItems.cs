using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreRequisition.Models
{
    public class PickingItems
    {
        public int ITEM_NUM { get; set; }
        public string ITEM_NAME { get; set; }
        public string REQ_NUM { get; set; }
        public int ACTUAL_QTY { get; set; }
        public int REQ_QTY { get; set; }
    }
}