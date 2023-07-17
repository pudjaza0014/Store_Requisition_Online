
using StoreRequisition.Models;
using System.Collections.Generic;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Remoting;
using StoreRequisition.Class;
using Org.BouncyCastle.Utilities;
using System.Web.DynamicData;
using iTextSharp.text;

namespace StoreRequisition.Page
{
    public partial class Frm_Storage_Suggestion : System.Web.UI.Page
    {
        public string jobType { get; set; }
        public List<MyData> dataList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Remove("item");
            Session.Remove("itemTransfer");
            Session.Remove("mat_");
            if (!IsPostBack)
            {
                Session.Remove("item");
                Session.Remove("itemTransfer");
                Session.Remove("mat_");
                dataList = new List<MyData>();

                if (Session["UserAD"] == null)
                {
                    Response.Redirect("~/Page/Frm_Login_R.aspx");
                }
                string assemblyVersion = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
                lblpageName.Text = assemblyVersion;
            }
        }

        public string getAuthority()
        { 
            string strResults = "Other";
            try
            {
                UserAD userAD = (UserAD)HttpContext.Current.Session["UserAD"];
                if (userAD != null)
                { 
                    strResults = userAD.Departments.ToLower(); 
                } 
                return strResults;
            }
            catch (Exception)
            {
                return "Other";
            }
        }

        [WebMethod]
        public static Storage_Suggestion GetData(String param1)
        {
            string strLotNo = param1.Replace("\t", "").Replace("\r\n", "").ToUpper(); 
            List<Material> Materials = new List<Material>();
            Storage_Suggestion mat_ = new Storage_Suggestion();
            mat_ = getDataMaterialDetail(strLotNo);
            return mat_;
        }
        [WebMethod]
        protected static Storage_Suggestion getDataMaterialDetail(string strLotNo)
        {
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            List<Material> Materials = new List<Material>();
            Storage_Suggestion mat_ = new Storage_Suggestion();
            try
            {
                clsSQLscript Objrun = new clsSQLscript();
                dt = Objrun.getMaterialInvoice(strLotNo.Replace("\t", "").Replace("\r\n", "").ToUpper());
                if (dt.Rows.Count <= 0)
                {
                    throw new Exception("This Lot number is not found. please checking and try again.");
                }

                string strinv = dt.Rows[0]["INVOICE_NUMBER"].ToString();
                string stritemCode = dt.Rows[0]["ITEM_CODE"].ToString();

                dt2 = Objrun.getMaterialListInvoice(strinv, stritemCode);
                if (dt2.Rows.Count <= 0)
                {
                    throw new Exception("This Lot number Invoice is not found. please checking and try again.");
                }


                mat_.itemCode = stritemCode;
                mat_.invoiceNo = strinv;
                mat_.Total = dt2.Rows.Count.ToString();
                int i = 1;
                foreach (DataRow item in dt2.Rows)
                {
                    Materials.Add(new Material()
                    {
                        NO = i++,
                        LotNo = item["LOT_NUMBER"].ToString(),
                        Qty = Convert.ToInt32(item["TRANSACTION_QUANTITY"]),
                        Expired = item["ORIGINAL_EXPIRATION_DATE"].ToString()
                    });
                }
                mat_.materials = Materials;
                dt3 = Objrun.getShelfPackCAP(stritemCode);
                if (dt3.Rows.Count <= 0)
                {
                    throw new Exception("This This item Code shelf pack cap is not found. please checking and try again.");
                }


                mat_.ShelfPackCAP = dt3.Rows[0]["SHELF_PACK_CAP"].ToString();
                mat_.ShelfName = dt3.Rows[0]["SHELF_NAME"].ToString();
                mat_.TotalCountPack = dt3.Rows[0]["TOTAL_COUNT_PACK"].ToString();
                mat_.Subinventory = dt3.Rows[0]["RECEIVE_SUBINVENTORY"].ToString();
                mat_.locator = dt3.Rows[0]["RECEIVE_LOCATOR"].ToString();
                mat_.ShelfPackCount = (Convert.ToInt32(mat_.TotalCountPack) - Convert.ToInt32(mat_.Total)).ToString();


                int V_Diff = 0,
                    Scan_to_Temp = 0,
                    Shelf_Status_Avail_Qty = 0,
                    total = Convert.ToInt32(mat_.Total),
                    ShelfPackCAP = Convert.ToInt32(mat_.ShelfPackCAP),
                    TotalCountPack = Convert.ToInt32(mat_.TotalCountPack);
                string Shelf_Status = string.Empty,
                    Shelf_Status_color = string.Empty;

                V_Diff = ShelfPackCAP - TotalCountPack;

                if (V_Diff < 0)
                {
                    Scan_to_Temp = Math.Abs(V_Diff) < total ? V_Diff : total;
                }

                //Shelf_Status_Avail_Qty = V_Diff;
                Shelf_Status_Avail_Qty = V_Diff + total > ShelfPackCAP ? ShelfPackCAP : V_Diff + total;

                if (Convert.ToInt32(mat_.ShelfPackCAP) == 0)
                {
                    Shelf_Status = "No Space";
                    Shelf_Status_color = "bg-secondary text-white";
                }
                else
                {
                    if (Shelf_Status_Avail_Qty == total)
                    {
                        Shelf_Status = "Full Space";
                        Shelf_Status_color = "bg-warning text-white";

                    }
                    else if (Shelf_Status_Avail_Qty > total)
                    {
                        Shelf_Status = "Free Space";
                        Shelf_Status_color = "bg-success text-white";


                    }
                    else if (Shelf_Status_Avail_Qty < total)
                    {
                        Shelf_Status = "Over Space";
                        Shelf_Status_color = "bg-danger text-white";
                    }
                }
                mat_.ShelfStatus = Shelf_Status + " : " + Shelf_Status_Avail_Qty;
                mat_.shelfStatus_Color = Shelf_Status_color;
                mat_.ScanToSTTemp = Scan_to_Temp.ToString();
                HttpContext.Current.Session["mat_"] = mat_;

                return mat_;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static IEnumerable<string> GenInventory(String param1)
        {
            DataTable dt = new DataTable();
            try
            {
                clsSQLscript Objrun = new clsSQLscript();
                dt = Objrun.getSubInventory();


                List<string> list = new List<string>();
                foreach (DataRow item in dt.Rows)
                {
                    list.Add(item["SECONDARY_INVENTORY_NAME"].ToString());
                }

                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static IEnumerable<string> GenLocator(String param1)
        {
            DataTable dt = new DataTable();
            try
            {
                string strSubInventory = param1.Trim();
                clsSQLscript Objrun = new clsSQLscript();
                dt = Objrun.getLocator(strSubInventory);


                List<string> list = new List<string>();
                foreach (DataRow item in dt.Rows)
                {
                    list.Add(item["ST_TEMP_LOCATOR"].ToString());
                }

                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static Material GetLotToTransfer(String param1, String param2, String param3, String param4, String param5)
        {
            Material lotDetail = new Material();
            List<Material> Materials_trans = new List<Material>();
            string LotNo = param1.Trim().ToUpper(),

                   SubinvFrom = param2.Trim(),
                   LocatorFrom = param3.Trim(),

                   SubinvTo = param4.Trim(),
                   LocatorTo = param5.Trim();
            try
            {
               


                DataTable dt = new DataTable();
                clsSQLscript Objrun = new clsSQLscript();
                string strLotNo = param1;
                List<Material> Materials = new List<Material>();
               
                Storage_Suggestion mat_;
                mat_ = (Storage_Suggestion)HttpContext.Current.Session["mat_"];
               

                Materials_trans = (List<Material>)HttpContext.Current.Session["itemTransfer"] == null ? new List<Material>() : (List<Material>)HttpContext.Current.Session["itemTransfer"];
                if (mat_ != null)
                {
                    Materials = mat_.materials;

                    lotDetail = Materials.Where(x => x.LotNo.Contains(LotNo)).FirstOrDefault();
                    if (lotDetail == null)
                    {
                        throw new Exception($"This Lot number :\"{LotNo}\":is nothing in invoice. please checking and try again.");
                    }
                }

                lotDetail = new Material();
                lotDetail = Materials_trans.Where(x => x.LotNo.Contains(LotNo)).FirstOrDefault();
                if (lotDetail != null)
                {
                    throw new Exception($"This Lot number :\"{LotNo}\":duplicated in List. please checking and try again.");
                   
                }

                dt = LocatorFrom==""? Objrun.getLotNo(LotNo, SubinvFrom): Objrun.getLotNo(LotNo, SubinvFrom, LocatorFrom);
                if (dt.Rows.Count <= 0)
                {
                    throw new Exception($"This Lot number :\"{LotNo}\" ไม่พบใน OnHand ใน SubInventory \"{SubinvFrom}\" , Locator \"{LocatorFrom}\"  . please checking and try again.");
                }

                if ((Materials_trans.Count + 1) > 1000)
                {
                    throw new Exception($"The System Limit 1000Lot to Transfer. please checking and try again.");
                }

                if (dt.Rows.Count > 0)
                {
                    lotDetail = new Material()
                    {
                        NO = (Materials_trans.Count + 1),
                        LotNo = dt.Rows[0]["LOT_NUMBER"].ToString(),
                        Qty = Convert.ToInt32(dt.Rows[0]["ONHAND_QTY"]),
                        Expired = dt.Rows[0]["ORIGINAL_EXPIRATION_DATE"].ToString(),
                        SubInventory = dt.Rows[0]["SUBINVENTORY_CODE"].ToString(),
                        Locator = dt.Rows[0]["LOCATOR"].ToString(),
                        LocatorID = dt.Rows[0]["INVENTORY_LOCATION_ID"].ToString(),
                        ScanStatus = "OK"
                    };
                }

                if (lotDetail.LotNo != null)
                {
                    Materials_trans.Add(lotDetail);
                    HttpContext.Current.Session["itemTransfer"] = Materials_trans;
                    return lotDetail;
                }
                throw new Exception("This Lot number is not found. please checking and try again.");
            }
            catch (Exception ex)
            {
                Materials_trans = (List<Material>)HttpContext.Current.Session["itemTransfer"] == null ? new List<Material>() : (List<Material>)HttpContext.Current.Session["itemTransfer"];
                lotDetail = new Material()
                {
                    NO = (Materials_trans.Count + 1),
                    LotNo = LotNo,
                    Qty = 0,
                    Expired = ex.Message,
                    SubInventory = SubinvFrom,
                    Locator = LocatorFrom,
                    LocatorID = "",
                    ScanStatus = "ERROR"
                };


                if (lotDetail.LotNo != null)
                {
                    Materials_trans.Add(lotDetail);
                    HttpContext.Current.Session["itemTransfer"] = Materials_trans;
                    return lotDetail;
                }
                return lotDetail;
               // throw;
            }
        }
        [WebMethod]
        public static List<Material> RemoveScan(string param1)
        {
            try
            {
                string No = param1.ToUpper();

                DataTable dt = new DataTable();
                clsSQLscript Objrun = new clsSQLscript();
                string strLotNo = param1;
                List<Material> Materials = new List<Material>();
                List<Material> Materials_trans = new List<Material>();
                Storage_Suggestion mat_;
                mat_ = (Storage_Suggestion)HttpContext.Current.Session["mat_"];
                Material lotDetail = new Material();

                Materials_trans = (List<Material>)HttpContext.Current.Session["itemTransfer"] == null ? new List<Material>() : (List<Material>)HttpContext.Current.Session["itemTransfer"];
                if (Materials_trans.Count != 0)
                {
                    Materials = Materials_trans.Where(x => !x.NO.Equals(Convert.ToInt32(No))).ToList();

                    Materials_trans = new List<Material>();
                    int i = 1;
                    foreach (var item in Materials)
                    {
                        Materials_trans.Add(new Material()
                        {
                            NO = i++,
                            LotNo = item.LotNo,
                            Qty = item.Qty,
                            Expired = item.Expired,
                            SubInventory = item.SubInventory,
                            Locator = item.Locator,
                            LocatorID = item.LocatorID,
                            ScanStatus = item.ScanStatus

                            
                        });;
                    }


                    HttpContext.Current.Session["itemTransfer"] = Materials_trans;
                    //Materials_trans = Materials.ToList();
                }

                return Materials_trans.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [WebMethod]
        public static List<Material> RemoveErrorRow()
        {
             
                try
                {
                   

                    DataTable dt = new DataTable();
                    clsSQLscript Objrun = new clsSQLscript();
                   
                    List<Material> Materials = new List<Material>();
                    List<Material> Materials_trans = new List<Material>();
                    Storage_Suggestion mat_;
                    mat_ = (Storage_Suggestion)HttpContext.Current.Session["mat_"];
                    Material lotDetail = new Material();

                    Materials_trans = (List<Material>)HttpContext.Current.Session["itemTransfer"] == null ? new List<Material>() : (List<Material>)HttpContext.Current.Session["itemTransfer"];
                    if (Materials_trans.Count != 0)
                    {
                        Materials = Materials_trans.Where(x => !x.ScanStatus.Equals("ERROR")).ToList();

                        Materials_trans = new List<Material>();
                        int i = 1;
                        foreach (var item in Materials)
                        {
                            Materials_trans.Add(new Material()
                            {
                                NO = i++,
                                LotNo = item.LotNo,
                                Qty = item.Qty,
                                Expired = item.Expired,
                                SubInventory = item.SubInventory,
                                Locator = item.Locator,
                                LocatorID = item.LocatorID,
                                ScanStatus = item.ScanStatus
                            });
                        }


                        HttpContext.Current.Session["itemTransfer"] = Materials_trans;
                        //Materials_trans = Materials.ToList();
                    }

                    return Materials_trans.ToList();
                }
            catch (Exception)
            {

                throw;
            }
        }


        [WebMethod]
        public static void transferData(string param1, string param2, string param3, string param4)
        {
            try
            {
                string SubInventoryFrom = param1
                    , LocatorFrom = param2
                    , SubInventoryTo = param3
                    , LocatorTo = param4

                    , LocatorTo_ID = string.Empty
                    , strConcat = string.Empty;


                List<Material> Materials_trans = new List<Material>();
                Materials_trans = (List<Material>)HttpContext.Current.Session["itemTransfer"];
                if (Materials_trans == null || Materials_trans.Count == 0)
                {
                    throw new Exception("ไม่พบข้อมูลที่ต้องการ Transfer กรุณาตรวจสอบแล้วลองอีกครั้ง");
                }

                Storage_Suggestion mat_;
                DataTable dt = new DataTable();
                clsSQLscript Objrun = new clsSQLscript();
                ClassDB objClassDB = new ClassDB();
                UserAD user_AD = (UserAD)HttpContext.Current.Session["UserAD"];

                dt = Objrun.getLocator(SubInventoryTo, LocatorTo);
                if (dt.Rows.Count == 0)
                {
                    throw new Exception($"พบสิ่งผิดปกติเกี่ยวกับการค้นหา Locator_ID จากข้อมูล[SubInventoryTo : {SubInventoryTo}][LocatorTo:{LocatorTo}] กรุณาถ่ายรูปหน้าจอแล้วส่งให้ทาง ISC ตรวจสอบ");
                }

                LocatorTo_ID = dt.Rows[0]["ST_TEMP_LOCATOR_ID"].ToString();                
                var locatorFrom_ID =  Materials_trans.FirstOrDefault();
                mat_ = (Storage_Suggestion)HttpContext.Current.Session["mat_"];

                List< TransferItemlist > TrnList= new List< TransferItemlist >();

                int ListSize = 100;

                for (int PageNumber = 1; PageNumber <= Math.Ceiling((double)Materials_trans.Count / ListSize); PageNumber++)
                {

                    IEnumerable<string> values = Materials_trans.Skip((PageNumber - 1) * ListSize).Take(ListSize).AsEnumerable().Select(x => x.LotNo);
                    strConcat = string.Join("|", values);
                    TransferItemlist strList = new TransferItemlist()
                    {
                        UserID = "2589",
                        PP_FROM_ORG_ID = "84", //PP_FROM_ORG_ID NUMBER
                        PP_FROM_SUBINVENTORY = SubInventoryFrom, //PP_FROM_SUBINVENTORY VARCHAR2
                        PP_FROM_LOCATOR_ID = string.IsNullOrEmpty(LocatorFrom)?null:locatorFrom_ID.LocatorID, //PP_FROM_LOCATOR_ID VARCHAR2  --12214 E
                        PP_TO_ORG_ID = "84", //PP_FROM_ORG_ID NUMBER
                        PP_TO_SUBINVENTORY = SubInventoryTo,//PP_TO_SUBINVENTORY VARCHAR2
                        PP_TO_LOCATOR_ID = LocatorTo_ID,//PP_TO_LOCATOR_ID VARCHAR2  --113479 A010404 
                        PP_LOT_LIST = strConcat //PP_LOT_LIST VARCHAR2
                    };

                    objClassDB.TransferData(strList);
                    TrnList.Add(strList);

                }


                var TrnLisff = TrnList.ToArray();
                 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        protected void err(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "alert('" + msg + "');", true);
        }
    }
}