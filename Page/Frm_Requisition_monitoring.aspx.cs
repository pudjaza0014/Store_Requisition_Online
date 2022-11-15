using StoreRequisition.Class;
using StoreRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreRequisition.Page
{
    public partial class Frm_Requisition_monitoring : System.Web.UI.Page
    {

        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static Requisition_M MonitorGetData()
        {
            DataTable dt;
            clsSQLscript objRun = new clsSQLscript();
            Requisition_M  result_list = new Requisition_M();
            try
            {
                dt = new DataTable();
                dt = objRun.GetRequisitionListAll_monitor();

                List<Requisition_ListM> requisition_List = new List<Requisition_ListM>();
                List<ProcessGroup> processGroups = new List<ProcessGroup>();
                foreach (DataRow row in dt.Rows)
                {
                    requisition_List.Add(new Requisition_ListM()
                    {
                        REQ_NUM = Convert.ToInt32(row["REQ_NUM"]),
                        APPROVED_BY = row["APPROVE_BY"].ToString(),
                        SUB_INV = row["SUB_INV"].ToString(),
                        TRANSFER_TO = row["TRANSFER_TO"].ToString(),
                        STATUS = row["PROGRESS"].ToString(),
                        STATE_NEW = row["StateNew"].ToString(),
                        DELIVERY_STATE = row["DELIVERY_STATION"].ToString(),
                        PROCESS_COLORS = row["PROCESS_COLORS"].ToString(),
                        REQ_DATE = Convert.ToDateTime(row["APPROVED_DATE"]),
                        REQ_TIME = Convert.ToDateTime(row["APPROVED_DATE"]).ToString("HH:mm"),
                        LOCATION = row["REQ_LOCATION"].ToString()


                    }) ; 
                }

                

                dt = new DataTable();
                objRun.GetRequisitionGroupProcess(ref dt);

                foreach (DataRow row in dt.Rows)
                {
                    processGroups.Add(new ProcessGroup()
                    {
                        ProcessCode = row["PROCESS_CODE"].ToString(),
                        ProcessName = row["PROGRESS"].ToString(),
                        processColor = row["PROCESS_COLORS"].ToString(),
                        RequisitionAmount = Convert.ToInt32(row["CNT_REQ"])

                    });
                }


                result_list.ResultStatus = "OK";
                result_list.ErrorMsg = null;
                result_list.requisition_Lists = requisition_List;
                result_list.processGroups = processGroups;
            }
            catch (Exception ex)
            {
                result_list.ResultStatus = "ERROR";
                result_list.ErrorMsg = ex.Message.ToString(); 
            }


            return result_list;
        }
    }
}