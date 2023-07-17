using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreRequisition.Models
{
    public class Material
    {
        public int NO { get; set; }
        public string LotNo { get; set; }    
        public int Qty { get; set; }    
        public string Expired { get; set; }
        public string SubInventory { get; set; }
        public string Locator { get; set; }
        public string LocatorID { get; set; }
        public string ScanStatus { get; set; }
    }

    public class Storage_Suggestion
    {
        public string invoiceNo { get; set; }
        public string itemCode { get; set; }
        public string Total { get; set; }
        public string ShelfPackCount { get; set; }
        public string ShelfStatus { get; set; }
        public string shelfStatus_Color { get; set; }
        public string ShelfName { get; set; }
        public string ShelfPackCAP { get; set; }
        public string TotalCountPack { get; set; } 
        public string Subinventory { get; set; }
        public string locator { get; set; }
        public string locatorID { get; set; }
        public string ScanToSTTemp { get; set; }
        public List<Material> materials { get; set; } 
    }

    public class TransferItemlist
    {
        public string UserID { get; set; }
        public string PP_FROM_ORG_ID { get; set; }
        public string PP_FROM_SUBINVENTORY { get; set; }
        public string PP_FROM_LOCATOR_ID { get; set; }
        public string PP_TO_ORG_ID { get; set; }
        public string PP_TO_SUBINVENTORY { get; set; }
        public string PP_TO_LOCATOR_ID { get; set; }
        public string PP_LOT_LIST { get; set; }
    }
}