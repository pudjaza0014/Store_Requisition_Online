using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Web;
using System.Net.NetworkInformation;
using StoreRequisition.Models;
using static iTextSharp.text.pdf.AcroFields;

public class ClassDB
{
    // public string hostNames =;
    // public string Param_Role = (HttpContext.Current.Server.MachineName.ToString() == "THEERAPATC-NB" ? "2":"1");
    public string[] UserinitName = { "srl", "pnp","pys","pcp" };
    public string Param_Role = "2";    
    public string ErrorMsg { get; set; }
    public string TSql { get; set; } 
    public string param_Approval { get; set; }
    public string param_status { get; set; }
    public string param_reason { get; set; }

    internal string getRole(string initName)
    {
        string strRole = "3";
        try
        {

            UserAD userAD = new UserAD();
            userAD = (UserAD)HttpContext.Current.Session["UserAD"];

            if (Array.IndexOf(UserinitName, initName) != -1) {
                strRole = "2";
            };

        
            return strRole;
        }
        catch (Exception ex)
        {

            throw;
        }
                   
    }
    internal List<string> StartPicking(Int32 P_REQ_NUM, String P_APPROVED_BY, UserAD P_ACTION_BY, String P_PARAM1, String P_PARAM2, String P_RESULT, String P_ERROR_MSG)
    {
        List<string> arrResult = new List<string>();
        try
        {
            
            using (OracleConnection conn = new OracleConnection())
            {
                conn.ConnectionString = "User Id=apps;Password=apps;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.234.40)(PORT=1530))(CONNECT_DATA=(SID=prod)));";
                using (OracleCommand cmd = new OracleCommand())
                {

                    
                    cmd.Connection = conn;
                    cmd.CommandText = "APPS.MMT_STORE_PROGRESS_MAKING";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_REQ_NUM", Direction = ParameterDirection.Input, Value = P_REQ_NUM });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_APPROVED_BY", Direction = ParameterDirection.Input, Value = P_APPROVED_BY });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_ROLE", Direction = ParameterDirection.Input, Value = getRole(P_APPROVED_BY) }); //2 picking , 1 approved
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_ACTION_BY", Direction = ParameterDirection.Input, Value = P_ACTION_BY.InitName });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_PARAM1", Direction = ParameterDirection.Input, Value = P_PARAM1 });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_PARAM2", Direction = ParameterDirection.Input, Value = P_PARAM2 });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_RESULT", Direction = ParameterDirection.Output, OracleType = OracleType.Char, Size = 100 });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_ERROR_MSG", Direction = ParameterDirection.Output, OracleType = OracleType.Char, Size = 100 });
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                     
                    //string strResult =(String)cmd.Parameters["P_RESULT"].Value.ToString().Trim();
                    //string strResultMsg = (String)cmd.Parameters["P_ERROR_MSG"].Value.ToString().Trim();
                    //string[] arrResult = { strResult , strResultMsg };
                     
                    arrResult.Add((String)cmd.Parameters["P_RESULT"].Value.ToString().Trim());
                    arrResult.Add((String)cmd.Parameters["P_ERROR_MSG"].Value.ToString().Trim());

                    return arrResult;
                }
            }

        }
        catch (Exception ex)
        {

            throw;
        }
    }


    public string param_Status { get; set; }

    public string param_SEGMENT1 { get; set; }


    public String INSERT_PRIORITY(Int32 P_LOCATION_ID,String P_ESTIMATE_DATE,Int32 P_ITEM_ID,Int32 P_QTY,String P_UOM,String P_INVOICE_NUMBER,String P_USER_REMARK,String P_USER_ID)
    {
        //DataTable Dt = new DataTable();
        String x;
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = "User Id=apps;Password=apps;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.234.40)(PORT=1530))(CONNECT_DATA=(SID=prod)));";
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = TSql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_LOCATION_ID", OracleType.Number)).Value = P_LOCATION_ID;
                cmd.Parameters.Add(new OracleParameter("P_ESTIMATE_DATE", OracleType.NVarChar)).Value = P_ESTIMATE_DATE;
                cmd.Parameters.Add(new OracleParameter("P_ITEM_ID", OracleType.Number)).Value = P_ITEM_ID;
                cmd.Parameters.Add(new OracleParameter("P_QTY", OracleType.Number)).Value = P_QTY;
                cmd.Parameters.Add(new OracleParameter("P_UOM", OracleType.NVarChar)).Value = P_UOM;
                cmd.Parameters.Add(new OracleParameter("P_INVOICE_NUMBER", OracleType.NVarChar)).Value = P_INVOICE_NUMBER;
                cmd.Parameters.Add(new OracleParameter("P_USER_REMARK", OracleType.NVarChar)).Value = P_USER_REMARK;
                cmd.Parameters.Add(new OracleParameter("P_USER_ID", OracleType.Number)).Value = P_USER_ID;
                cmd.Parameters.Add(new OracleParameter("P_PRIORITY_SEQ", OracleType.Number)).Direction = ParameterDirection.InputOutput;

                conn.Open();

                OracleDataReader rd;
                rd = cmd.ExecuteReader();

                //cmd.ExecuteNonQuery();
                x = cmd.Parameters["P_PRIORITY_SEQ"].Value.ToString();

                conn.Close();
                conn.Dispose();
            }
        }
        return x;
        //return Dt;
    }

    public void CANCEL_PRIORITY(Int32 P_LOCATION_ID, String P_ESTIMATE_DATE, String P_PRIORITY_SEQ, String P_USER_ID)
    {
        //DataTable Dt = new DataTable();
        //String x;
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = "User Id=apps;Password=apps;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.234.40)(PORT=1530))(CONNECT_DATA=(SID=prod)));";
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = TSql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_LOCATION_ID", OracleType.Number)).Value = P_LOCATION_ID;
                cmd.Parameters.Add(new OracleParameter("P_ESTIMATE_DATE", OracleType.NVarChar)).Value = P_ESTIMATE_DATE;
                cmd.Parameters.Add(new OracleParameter("P_USER_ID", OracleType.Number)).Value = P_USER_ID;
                cmd.Parameters.Add(new OracleParameter("P_PRIORITY_SEQ", OracleType.Number)).Value = P_PRIORITY_SEQ;

                conn.Open();

                //OracleDataReader rd;
                //rd = cmd.ExecuteReader();

                cmd.ExecuteNonQuery();
                //x = cmd.Parameters["P_PRIORITY_SEQ"].Value.ToString();

                conn.Close();
                conn.Dispose();
            }
        }
        //return x;
        //return Dt;
    }

    public void UPDATE_PRIORITY(Int32 P_LOCATION_ID, String P_ESTIMATE_DATE, String P_PRIORITY_SEQ, Int32 P_QTY, String P_INVOICE_NUMBER, String P_USER_REMARK, String P_USER_ID)
    {
        //DataTable Dt = new DataTable();
        //String x;
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = "User Id=apps;Password=apps;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.234.40)(PORT=1530))(CONNECT_DATA=(SID=prod)));";
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = TSql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_LOCATION_ID", OracleType.Number)).Value = P_LOCATION_ID;
                cmd.Parameters.Add(new OracleParameter("P_ESTIMATE_DATE", OracleType.NVarChar)).Value = P_ESTIMATE_DATE;
                cmd.Parameters.Add(new OracleParameter("P_QTY", OracleType.Number)).Value = P_QTY;
                cmd.Parameters.Add(new OracleParameter("P_INVOICE_NUMBER", OracleType.NVarChar)).Value = P_INVOICE_NUMBER;
                cmd.Parameters.Add(new OracleParameter("P_USER_REMARK", OracleType.NVarChar)).Value = P_USER_REMARK;
                cmd.Parameters.Add(new OracleParameter("P_USER_ID", OracleType.Number)).Value = P_USER_ID;
                cmd.Parameters.Add(new OracleParameter("P_PRIORITY_SEQ", OracleType.Number)).Value = P_PRIORITY_SEQ;

                conn.Open();

                //OracleDataReader rd;
                //rd = cmd.ExecuteReader();

                cmd.ExecuteNonQuery();
                //x = cmd.Parameters["P_PRIORITY_SEQ"].Value.ToString();

                conn.Close();
                conn.Dispose();
            }
        }
        //return x;
        //return Dt;
    }

    public DataTable PRIORITY_REPORT(Int32 P_LOCATION_ID,String P_ESTIMATE_DATE_FROM,String P_ESTIMATE_DATE_TO)
    {
        DataTable Dt = new DataTable();
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = "User Id=apps;Password=apps;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.234.40)(PORT=1530))(CONNECT_DATA=(SID=prod)));";
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = TSql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_LOCATION_ID", OracleType.Number)).Value = P_LOCATION_ID;
                cmd.Parameters.Add(new OracleParameter("P_ESTIMATE_DATE_TO", OracleType.NVarChar)).Value = P_ESTIMATE_DATE_TO;
                cmd.Parameters.Add(new OracleParameter("P_ESTIMATE_DATE_FROM", OracleType.NVarChar)).Value = P_ESTIMATE_DATE_FROM;
                cmd.Parameters.Add(new OracleParameter("O_RESULTSET", OracleType.Cursor)).Direction = ParameterDirection.Output;

                conn.Open();

                OracleDataReader rd;
                rd = cmd.ExecuteReader();

                Dt.Load(rd);

                conn.Close();
                conn.Dispose();
            }
        }
        return Dt;
    }

    public DataTable ValidatedUserId(String P_USERNAME, String P_PASSWORD)
    {
        DataTable Dt = new DataTable();
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = "User Id=apps;Password=apps;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.234.40)(PORT=1530))(CONNECT_DATA=(SID=prod)));";
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = TSql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_USERNAME", OracleType.Number)).Value = P_USERNAME;
                cmd.Parameters.Add(new OracleParameter("P_PASSWORD", OracleType.NVarChar)).Value = P_PASSWORD;
                cmd.Parameters.Add(new OracleParameter("P_USER_ID", OracleType.Cursor)).Direction = ParameterDirection.Output;

                conn.Open();

                OracleDataReader rd;
                rd = cmd.ExecuteReader();

                Dt.Load(rd);

                conn.Close();
                conn.Dispose();
            }

        }
        return Dt;
    }

    public DataTable GET_VALIDATE_ITEM(String P_ITEM_CODE)
    {
        DataTable Dt = new DataTable();
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = "User Id=apps;Password=apps;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.234.40)(PORT=1530))(CONNECT_DATA=(SID=prod)));";
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = TSql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_ORG_ID", OracleType.Number)).Value = 84;
                cmd.Parameters.Add(new OracleParameter("P_ITEM_CODE", OracleType.NVarChar)).Value = P_ITEM_CODE;
                cmd.Parameters.Add(new OracleParameter("O_RESULTSET", OracleType.Cursor)).Direction = ParameterDirection.Output;
                conn.Open();

                OracleDataReader rd;
                rd = cmd.ExecuteReader();

                Dt.Load(rd);

                conn.Close();
                conn.Dispose();
            }
        }
        return Dt;
    }

    public DataTable GET_LOCATION_LIST()
    {
        DataTable Dt = new DataTable();
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = "User Id=apps;Password=apps;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.234.40)(PORT=1530))(CONNECT_DATA=(SID=prod)));";
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = TSql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("O_RESULTSET", OracleType.Cursor)).Direction = ParameterDirection.Output;
                conn.Open();

                OracleDataReader rd;
                rd = cmd.ExecuteReader();

                Dt.Load(rd);

                conn.Close();
                conn.Dispose();
            }
        }
        return Dt;
    }

    public List<string> Requisition_Progress_Making(Int32 P_REQ_NUM, String P_APPROVED_BY, UserAD P_ACTION_BY,String P_PARAM1, String P_PARAM2, String P_RESULT,String P_ERROR_MSG)
    {
        //DataTable Dt = new DataTable();
        //String x;
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = "User Id=apps;Password=apps;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.234.40)(PORT=1530))(CONNECT_DATA=(SID=prod)));";
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = TSql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter { ParameterName = "P_REQ_NUM", Direction = ParameterDirection.Input, Value = P_REQ_NUM })  ;
                cmd.Parameters.Add(new OracleParameter{ParameterName = "P_APPROVED_BY", Direction = ParameterDirection.Input, Value = P_APPROVED_BY });
                cmd.Parameters.Add(new OracleParameter{ParameterName = "P_ROLE", Direction = ParameterDirection.Input, Value = getRole(P_APPROVED_BY) }); //1 approved , 2 picking , 3 old flow
                cmd.Parameters.Add(new OracleParameter{ParameterName = "P_ACTION_BY", Direction = ParameterDirection.Input, Value = P_ACTION_BY.InitName });
                cmd.Parameters.Add(new OracleParameter { ParameterName = "P_PARAM1", Direction = ParameterDirection.Input, Value = P_PARAM1 });
                cmd.Parameters.Add(new OracleParameter { ParameterName = "P_PARAM2", Direction = ParameterDirection.Input, Value = P_PARAM2 });
                cmd.Parameters.Add(new OracleParameter{ParameterName = "P_RESULT", Direction = ParameterDirection.Output, OracleType = OracleType.Char, Size = 100 });
                cmd.Parameters.Add(new OracleParameter{ParameterName = "P_ERROR_MSG", Direction = ParameterDirection.Output, OracleType = OracleType.Char, Size = 100 });       
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();




                //string strResult =(String)cmd.Parameters["P_RESULT"].Value.ToString().Trim();
                //string strResultMsg = (String)cmd.Parameters["P_ERROR_MSG"].Value.ToString().Trim();
                //string[] arrResult = { strResult , strResultMsg };

                List<string> arrResult = new List<string>();



                arrResult.Add((String)cmd.Parameters["P_RESULT"].Value.ToString().Trim());
                arrResult.Add((String)cmd.Parameters["P_ERROR_MSG"].Value.ToString().Trim());

                return arrResult;
            }
        }

    }
    public List<string> Requisition_Trading_Generate(string Due_date)
    {
        try
        {
            UserAD userAD = (UserAD)HttpContext.Current.Session["UserAD"];




            using (OracleConnection conn = new OracleConnection())
            {
                conn.ConnectionString = "User Id=apps;Password=apps;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.234.40)(PORT=1530))(CONNECT_DATA=(SID=prod)));";
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "MMT_STORE_REQ_TREDING";
                    cmd.CommandType = CommandType.StoredProcedure; 
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_DUE_DATE", Direction = ParameterDirection.Input, Value = Due_date });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_LOCATION", Direction = ParameterDirection.Input, Value = "MMCT" });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_DELIVERY_STATION", Direction = ParameterDirection.Input, Value = "PRODUCTION" });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_TEL", Direction = ParameterDirection.Input, Value = "4124" });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_REQUEST_BY", Direction = ParameterDirection.Input, Value = userAD.InitName });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_ISSUE_TYPE", Direction = ParameterDirection.Input, Value = "MATERIAL TRANSFER" });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_SUB_INV", Direction = ParameterDirection.Input, Value = "WH-TB" });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_REMARK", Direction = ParameterDirection.Input, Value = "TRADING" });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_APPROVEDNAME", Direction = ParameterDirection.Input, Value = "TRADING" });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_REQ_NUM", Direction = ParameterDirection.Output, OracleType = OracleType.Char, Size = 100 });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_APPROVE_BY", Direction = ParameterDirection.Output, OracleType = OracleType.Char, Size = 100 });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_ACTION_BY", Direction = ParameterDirection.Output, OracleType = OracleType.Char, Size = 100 });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_RESULT", Direction = ParameterDirection.Output, OracleType = OracleType.Char, Size = 100 });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "P_ERROR_MSG", Direction = ParameterDirection.Output, OracleType = OracleType.Char, Size = 1000 });
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();


                    List<string> arrResult = new List<string>();
                    arrResult.Add((String)cmd.Parameters["P_RESULT"].Value.ToString().Trim());
                    arrResult.Add((String)cmd.Parameters["P_ERROR_MSG"].Value.ToString().Trim());

                    return arrResult;
                }
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    public List<string> Requisition_cancel_picking(UserAD P_ACTION_BY, string req_num)
    {
        //DataTable Dt = new DataTable();
        //String x;
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = "User Id=apps;Password=apps;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.234.40)(PORT=1530))(CONNECT_DATA=(SID=prod)));";
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "APPS.MMT_STORE_REQ_CANCLE_PICKING";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter { ParameterName = "P_REQ_NUM", Direction = ParameterDirection.Input, Value = req_num });
                cmd.Parameters.Add(new OracleParameter { ParameterName = "P_APPROVED_BY", Direction = ParameterDirection.Input, Value = P_ACTION_BY.InitName });
                cmd.Parameters.Add(new OracleParameter { ParameterName = "P_ROLE", Direction = ParameterDirection.Input, Value = getRole(P_ACTION_BY.InitName)}); //2 picking , 1 approved
                cmd.Parameters.Add(new OracleParameter { ParameterName = "P_ACTION_BY", Direction = ParameterDirection.Input, Value = P_ACTION_BY.InitName });
                cmd.Parameters.Add(new OracleParameter { ParameterName = "P_PARAM1", Direction = ParameterDirection.Input, Value = "" });
                cmd.Parameters.Add(new OracleParameter { ParameterName = "P_PARAM2", Direction = ParameterDirection.Input, Value = "" });
                cmd.Parameters.Add(new OracleParameter { ParameterName = "P_RESULT", Direction = ParameterDirection.Output, OracleType = OracleType.Char, Size = 100 });
                cmd.Parameters.Add(new OracleParameter { ParameterName = "P_ERROR_MSG", Direction = ParameterDirection.Output, OracleType = OracleType.Char, Size = 100 });
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();




                //string strResult =(String)cmd.Parameters["P_RESULT"].Value.ToString().Trim();
                //string strResultMsg = (String)cmd.Parameters["P_ERROR_MSG"].Value.ToString().Trim();
                //string[] arrResult = { strResult , strResultMsg };

                List<string> arrResult = new List<string>();



                arrResult.Add((String)cmd.Parameters["P_RESULT"].Value.ToString().Trim());
                arrResult.Add((String)cmd.Parameters["P_ERROR_MSG"].Value.ToString().Trim());

                return arrResult;
            }
        }

    }

    // public List<string> RequisitionSaveItemsPicked(String P_APPROVED_BY, String P_ACTION_BY,List<PickingItems> pickingItems)
    public List<string> RequisitionSaveItemsPicked(UserAD P_ACTION_BY,List<PickingItems> pickingItems)
    {
        List<string> arrResult = new List<string>();
        try
        {
            foreach (var item in pickingItems)
            {
                string stritemName = item.ITEM_NAME;
                int indexString = stritemName.IndexOf(':');
                stritemName = stritemName.Substring(0, indexString).Trim();



                using (OracleConnection conn = new OracleConnection())
                {
                    conn.ConnectionString = "User Id=apps;Password=apps;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.234.40)(PORT=1530))(CONNECT_DATA=(SID=prod)));";
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = conn;
                        //cmd.CommandText = TSql;
                        cmd.CommandText = "APPS.MMT_STORE_SAVE_PICKED_ITEM";
                        cmd.CommandType = CommandType.StoredProcedure; 
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "P_REQ_NUM", Direction = ParameterDirection.Input, Value = item.REQ_NUM });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "P_ITEM_NUM", Direction = ParameterDirection.Input, Value = item.ITEM_NUM });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "P_PART_NUM", Direction = ParameterDirection.Input, Value = stritemName });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "P_REQ_QTY", Direction = ParameterDirection.Input, Value = item.REQ_QTY });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "P_ACTUAL_QTY", Direction = ParameterDirection.Input, Value = item.ACTUAL_QTY });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "P_RESULT", Direction = ParameterDirection.Output, OracleType = OracleType.Char, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "P_ERROR_MSG", Direction = ParameterDirection.Output, OracleType = OracleType.Char, Size = 100 });
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        conn.Dispose();
                        arrResult.Add((String)cmd.Parameters["P_RESULT"].Value.ToString().Trim());
                        arrResult.Add((String)cmd.Parameters["P_ERROR_MSG"].Value.ToString().Trim());
                    }
                }

            } 

            if (arrResult[0]=="OK")
            {

                //arrResult = new List<string>();
                //arrResult = Requisition_Progress_Making(pickingItems[0].REQ_NUM,)

                using (OracleConnection conn1 = new OracleConnection())
                {
                    conn1.ConnectionString = "User Id=apps;Password=apps;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.234.40)(PORT=1530))(CONNECT_DATA=(SID=prod)));";
                    using (OracleCommand cmd1 = new OracleCommand())
                    {
                        cmd1.Connection = conn1;
                        cmd1.CommandText = "APPS.MMT_STORE_PROGRESS_MAKING";
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new OracleParameter { ParameterName = "P_REQ_NUM", Direction = ParameterDirection.Input, Value = pickingItems[0].REQ_NUM });
                        cmd1.Parameters.Add(new OracleParameter { ParameterName = "P_APPROVED_BY", Direction = ParameterDirection.Input, Value = P_ACTION_BY.InitName });
                        cmd1.Parameters.Add(new OracleParameter { ParameterName = "P_ROLE", Direction = ParameterDirection.Input, Value = getRole(P_ACTION_BY.InitName) });
                        cmd1.Parameters.Add(new OracleParameter { ParameterName = "P_ACTION_BY", Direction = ParameterDirection.Input, Value = P_ACTION_BY.InitName });
                        cmd1.Parameters.Add(new OracleParameter { ParameterName = "P_PARAM1", Direction = ParameterDirection.Input, Value = "" });
                        cmd1.Parameters.Add(new OracleParameter { ParameterName = "P_PARAM2", Direction = ParameterDirection.Input, Value = "" });
                        cmd1.Parameters.Add(new OracleParameter { ParameterName = "P_RESULT", Direction = ParameterDirection.Output, OracleType = OracleType.Char, Size = 100 });
                        cmd1.Parameters.Add(new OracleParameter { ParameterName = "P_ERROR_MSG", Direction = ParameterDirection.Output, OracleType = OracleType.Char, Size = 100 });
                        conn1.Open();
                        cmd1.ExecuteNonQuery();
                        conn1.Close();
                        conn1.Dispose();




                        //string strResult =(String)cmd.Parameters["P_RESULT"].Value.ToString().Trim();
                        //string strResultMsg = (String)cmd.Parameters["P_ERROR_MSG"].Value.ToString().Trim();
                        //string[] arrResult = { strResult , strResultMsg };

                         



                        arrResult.Add((String)cmd1.Parameters["P_RESULT"].Value.ToString().Trim());
                        arrResult.Add((String)cmd1.Parameters["P_ERROR_MSG"].Value.ToString().Trim());

                        return arrResult;
                    }
                }
            } 
        }
        catch (Exception ex)
        {
            arrResult.Add("ERROR");
            arrResult.Add(ex.Message.ToString());
        }

        return arrResult;

    }

    public List<string> RequisitionSaveItem(UserAD P_ACTION_BY, List<PickingItems> pickingItems)
    {
        List<string> arrResult = new List<string>();
        try
        {
            foreach (var item in pickingItems)
            {

                string stritemName = item.ITEM_NAME;
                int indexString = stritemName.IndexOf(':');
                 stritemName = stritemName.Substring(0, indexString).Trim();


                using (OracleConnection conn = new OracleConnection())
                {
                    conn.ConnectionString = "User Id=apps;Password=apps;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.234.40)(PORT=1530))(CONNECT_DATA=(SID=prod)));";
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = conn;
                        //cmd.CommandText = TSql;
                        cmd.CommandText = "APPS.MMT_STORE_SAVE_PICKED_ITEM";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "P_REQ_NUM", Direction = ParameterDirection.Input, Value = item.REQ_NUM });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "P_ITEM_NUM", Direction = ParameterDirection.Input, Value = item.ITEM_NUM });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "P_PART_NUM", Direction = ParameterDirection.Input, Value = stritemName });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "P_REQ_QTY", Direction = ParameterDirection.Input, Value = item.REQ_QTY });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "P_ACTUAL_QTY", Direction = ParameterDirection.Input, Value = item.ACTUAL_QTY });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "P_RESULT", Direction = ParameterDirection.Output, OracleType = OracleType.Char, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "P_ERROR_MSG", Direction = ParameterDirection.Output, OracleType = OracleType.Char, Size = 100 });
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        conn.Dispose();
                        arrResult.Add((String)cmd.Parameters["P_RESULT"].Value.ToString().Trim());
                        arrResult.Add((String)cmd.Parameters["P_ERROR_MSG"].Value.ToString().Trim());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            arrResult.Add("ERROR");
            arrResult.Add(ex.Message.ToString());
        }

        return arrResult;

    }

    public bool TransferData(TransferItemlist item)
    {
        List<string> arrResult = new List<string>();
        try
        {
            using (OracleConnection conn = new OracleConnection())
            {
                conn.ConnectionString = "User Id=apps;Password=apps;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.234.40)(PORT=1530))(CONNECT_DATA=(SID=prod)));";
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "APPS.Mmt_Inv_Oracle_Interface.MOVE_TO_ST_TEMP";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "PP_USER_ID", Direction = ParameterDirection.Input, Value = item.UserID });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "PP_FROM_ORG_ID", Direction = ParameterDirection.Input, Value = item.PP_FROM_ORG_ID });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "PP_FROM_SUBINVENTORY", Direction = ParameterDirection.Input, Value = item.PP_FROM_SUBINVENTORY });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "PP_FROM_LOCATOR_ID", Direction = ParameterDirection.Input, Value =  (item.PP_FROM_LOCATOR_ID == null) ? DBNull.Value : (object)item.PP_FROM_LOCATOR_ID
                });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "PP_TO_ORG_ID", Direction = ParameterDirection.Input, Value = item.PP_TO_ORG_ID });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "PP_TO_SUBINVENTORY", Direction = ParameterDirection.Input, Value = item.PP_TO_SUBINVENTORY });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "PP_TO_LOCATOR_ID", Direction = ParameterDirection.Input, Value = item.PP_TO_LOCATOR_ID });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "PP_LOT_LIST", Direction = ParameterDirection.Input, Value = item.PP_LOT_LIST });
                    conn.Open();
                    OracleTransaction transaction = conn.BeginTransaction();
                    try
                    {          
                        cmd.Transaction = transaction;
                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                        arrResult.Add("OK");
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        } 
    }

    public bool BudgetControl_Operation(BudgetControl item)
    {
        List<string> arrResult = new List<string>();
        try
        {
            using (OracleConnection conn = new OracleConnection())
            {
                conn.ConnectionString = "User Id=apps;Password=apps;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.234.40)(PORT=1530))(CONNECT_DATA=(SID=prod)));";
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "MMTMRQ_BUDGET_CONTROL_OP";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "PE_OPERATION", Direction = ParameterDirection.Input, Value = item.Operation });

                    cmd.Parameters.Add(new OracleParameter { ParameterName = "PE_PERIODNAME", Direction = ParameterDirection.Input, Value = item.Period.PerioldName });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "PE_BUDGETNAME", Direction = ParameterDirection.Input, Value = item.Period.BudgetName });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "PE_BUDGETAMOUNT", Direction = ParameterDirection.Input, Value = item.Period.InitialBudgetAmount });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "PE_REMARK", Direction = ParameterDirection.Input, Value = item.Period.Remark != null ? (object)item.Period.Remark : DBNull.Value });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "DOC_TRANS_DATE", Direction = ParameterDirection.Input, Value = item.transection.DocumentTransDate });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "DOC_NO", Direction = ParameterDirection.Input, Value = item.transection.DocumentNumber });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "DOC_AMOUNT", Direction = ParameterDirection.Input, Value = item.transection.Amount });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "DOC_TYPE", Direction = ParameterDirection.Input, Value = item.transection.Document_Type });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "DOC_TRANSACTION_TYPE", Direction = ParameterDirection.Input, Value = item.transection.Transaction_Type != null ? (object)item.transection.Transaction_Type : DBNull.Value });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "DOC_ACTION", Direction = ParameterDirection.Input, Value = item.transection.Action });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "DOC_REFER_DOC_NO", Direction = ParameterDirection.Input, Value = item.transection.ReferDocumentNumber != null ? (object)item.transection.ReferDocumentNumber : DBNull.Value });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "DOC_REMARK", Direction = ParameterDirection.Input, Value = item.transection.Remark != null ? (object)item.transection.Remark : DBNull.Value });
                    cmd.Parameters.Add(new OracleParameter { ParameterName = "PE_OPERATOR_EN", Direction = ParameterDirection.Input, Value = item.EN });                    
                    conn.Open();
                    OracleTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = transaction;
                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                        arrResult.Add("OK");
                        return true;
                    }
                    catch (OracleException ex)
                    {
                        transaction.Rollback();                       
                        string errorMessage = ex.Message;
                        string[] array = ex.Message.Split(':');
                        string errMsg = array[1];
                        throw new Exception(errMsg);
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


     
    public DataTable GetDataOra()
    {
        DataTable Dt = new DataTable();

        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = "User Id=apps;Password=apps;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.234.40)(PORT=1530))(CONNECT_DATA=(SID=prod)));";
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = TSql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                conn.Open();
                using (OracleDataAdapter da = new OracleDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.Fill(Dt);
                }
                conn.Close();
                conn.Dispose();
            }
        }
        return Dt;
    }

    public DataTable GetDataSql()
    {
        DataTable Dt = new DataTable();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = StoreRequisition.Properties.Settings.Default.Con_PR;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = TSql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.Fill(Dt);
                }
                conn.Close();
                conn.Dispose();
            }
        }
        return Dt;
    }

    public int InsertDataSql()
    {
        int _return = 0;
        using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection())
        {
            conn.ConnectionString = StoreRequisition.Properties.Settings.Default.Con_PR;
            try
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.CommandText = TSql;
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    _return = 1;
                }
            }
            catch (SqlException ex)
            {
                ErrorMsg = ex.Message.ToString();
                _return = 0;
            }
        }
        return _return;
    }

    public int InsertDataOra()
    {
        int _return = 0;
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = "User Id=apps;Password=apps;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.234.40)(PORT=1530))(CONNECT_DATA=(SID=prod)));";
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.CommandText = TSql;
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    _return = 1;
                }
            }
            catch (SqlException ex)
            {
                ErrorMsg = ex.Message.ToString();
                _return = 0;
            }
        }
        return _return;
    }
}