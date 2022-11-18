using StoreRequisition.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace StoreRequisition.Class
{
    public class clsSQLscript
    {
        private DataTable dt;
        private ClassDB db = new ClassDB();
        private String strSQL;

        public DataTable GetUsersData(string En)
        {
            dt = new DataTable();
            try
            {
                //strSQL = "SELECT * FROM (SELECT * FROM APPS.MMT_HR_EMPLOYEE_V UNION SELECT * FROM MMT.MMT_STORE_REQ_CENTER_USER) WHERE NVL(DATE_END,SYSDATE) >= SYSDATE AND EMP_NO = '" + En + "'";

                strSQL = "SELECT * FROM APPS.MMT_HR_EMPLOYEE_V WHERE NVL(DATE_END,SYSDATE) >= SYSDATE AND  EMP_NO = '" + En + "'";

                // strSQL = "SELECT * FROM MMT_HR_EMPLOYEE_V WHERE NVL(DATE_END,SYSDATE) >= SYSDATE AND POSITION LIKE '%Supervisor%' and EMP_NO = '" + En + "'";
                db.TSql = strSQL;
                dt = db.GetDataOra();
                return dt;
            }
            catch (Exception )
            { 
                throw;
            }
        }


        public DataTable getuserGuest(string Username, string password)
        {
            try
            {
                strSQL = "SELECT * FROM MMT.MMT_STORE_GUEST_USER WHERE ID = '" + Username + "' AND PASSWORD ='" + password + "'";
                db.TSql = strSQL;
                dt = db.GetDataOra();
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }



        //public Employee GetUserData(string EN)
        //{
        //    Employee UserDatas = new Employee();
        //  //  clsSQLscript ObjRun = new clsSQLscript();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = GetUsersData(EN);
        //        if (dt.Rows.Count > 0)
        //        {
        //            UserDatas = new Employee
        //            {
        //                EMP_NO = dt.Rows[0]["EMP_NO"].ToString(),
        //                EMP_NAME_ENG = dt.Rows[0]["EMP_NAME_ENG"].ToString(),
        //                EXT = dt.Rows[0]["EXT"].ToString(),
        //                EMP_EMAIL = dt.Rows[0]["EMP_EMAIL"].ToString(),
        //                POSITION = dt.Rows[0]["POSITION"].ToString(),
        //                GL_CODE = dt.Rows[0]["GL_CODE"].ToString()
        //            };
        //        }

        //        return UserDatas;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        internal void GetRequisitionGroupProcess(ref DataTable dt)
        {
            try
            {


                //strSQL = "SELECT   PC.PROCESS_CODE,CASE WHEN INF.REQ_STATUS = 'SUBMIT' THEN 'CONFIRM PACK' ELSE INF.REQ_STATUS END AS PROGRESS  ,PC.PROCESS_COLORS , COUNT(REQ_NUM)AS CNT_REQ FROM MMT.MMT_STORE_INFO INF left join MMT.MMT_STORE_REQ_PROCESS PC ON INF.REQ_STATUS = PC.PROCESS_NAME LEFT JOIN (SELECT * FROM MMT.MMT_STORE_REQ_PROCESS_FLOW WHERE FLOW_ID  = 'REQ010') PF ON PC.PROCESS_CODE = PF.PROCESS_CODE   WHERE INF.REQ_NUM IS NOT NULL AND REQ_DATE < SYSDATE  ";

                strSQL = "SELECT   PC.PROCESS_CODE, INF.REQ_STATUS AS PROGRESS  ,PC.PROCESS_COLORS , COUNT(REQ_NUM)AS CNT_REQ FROM MMT.MMT_STORE_INFO INF left join MMT.MMT_STORE_REQ_PROCESS PC ON INF.REQ_STATUS = PC.PROCESS_NAME LEFT JOIN (SELECT * FROM MMT.MMT_STORE_REQ_PROCESS_FLOW WHERE FLOW_ID  = 'REQ010') PF ON PC.PROCESS_CODE = PF.PROCESS_CODE   WHERE INF.REQ_NUM IS NOT NULL AND REQ_DATE < SYSDATE AND PC.PROCESS_CODE not like '%09%'   ";


                int hTime = Convert.ToInt32(DateTime.Now.ToString("HH"));

                if (hTime >= 6 && hTime < 18)
                {
                    strSQL += " and  (req_date >= (trunc(SYSDATE) + interval '6' hour))";
                }
                else
                {
                    if (hTime > 18)
                    {
                        strSQL += " and  (req_date >= (trunc(SYSDATE) + interval '18' hour))";
                    }
                    else
                    {
                        strSQL += " and (req_date > (trunc(to_date(SYSDATE-1)) + interval '18' hour))";

                    }
                }
                strSQL += "GROUP BY PC.PROCESS_CODE,INF.REQ_STATUS ,PC.PROCESS_COLORS,PF.SEQ_NO ORDER BY PF.SEQ_NO";

            db.TSql = strSQL;
            dt = db.GetDataOra();

            }
            catch (Exception )
            {

                throw;
            }

        }

        internal DataTable getDataProgress(string req_num)
        {
            // throw new NotImplementedException();

            dt = new DataTable(); 
            strSQL = "";
            try
            {
                strSQL = "SELECT  PG.REQ_NUM,PG.PROCESS_CODE, PC.PROCESS_NAME, PG.FLAG_PICK, PG.SEQ_NO,PG.APPROVED_DATE,PC.PROCESS_COLORS,PC.PROCESS_ICONS FROM MMT.MMT_STORE_REQ_PROGRESS PG INNER JOIN MMT.MMT_STORE_REQ_PROCESS PC ON PG.PROCESS_CODE = PC.PROCESS_CODE WHERE PG.REQ_NUM = '" + req_num + "'  AND PG.PROCESS_CODE !='REQ001' order by PG.SEQ_NO ASC";
                db.TSql = strSQL;
                dt = db.GetDataOra();
                return dt;

            }
            catch (Exception ex)
            {

                throw ex;
            } 
        }

        public DataTable GetRequisitionListAll()
        {
            dt = new DataTable();
            try
            {
                //strSQL = "SELECT * FROM MMT.MMT_STORE_INFO_TEST WHERE ROWNUM <=50";  
                //strSQL = "SELECT REQ_NUM ,req_date,  REQ_BY,REQ_STATUS,SUB_INV,DELIVERY_STATION ,case when sysdate - req_date< 0.001333 then 'new'else '' end as StateNew  FROM mmt.MMT_STORE_INFO_TEST  WHERE REQ_DATE < sysdate AND REQ_DATE > sysdate -3 order by req_date desc";
                // strSQL = "SELECT * FROM  MMT.MMT_STORE_INFO_TEST  WHERE REQ_DATE < sysdate AND REQ_DATE > sysdate -3  order by REQ_DATE desc";

                //strSQL = " SELECT INF.REQ_NUM AS REQ_NUM, INF.REQ_BY AS REQ_BY, PF.APPROVED_DATE AS ACTION_DATE,  PF.SEQ_NO||' : '||PF.PROCESS_NAME AS PROGRESS ,case when sysdate - req_date< 0.001333 then 'new'else '' end as StateNew , INF.REQ_APPROVE_BY FROM  MMT.MMT_STORE_INFO_TEST INF,( SELECT * FROM MMT.MMT_STORE_REQ_PROGRESS  WHERE   FLAG_PICK = 1 ORDER BY SEQ_NO DESC) PF  WHERE INF.REQ_NUM = PF.REQ_NUM  ";

                //strSQL = "SELECT INF.REQ_NUM AS REQ_NUM,REQ_STATUS , INF.REQ_APPROVE_BY , INF.REQ_BY AS REQ_BY ,NVL(SUB_INV,TRANS_TYPE)as TRANSFER_TO,DELIVERY_STATION, PF.APPROVED_DATE AS ACTION_DATE, PF.PROCESS_NAME AS PROGRESS ,case when sysdate - req_date < 0.001333 then 'new'else '' end as StateNew, INF.SUB_INV, PF.ACTIVITY_EN FROM  MMT.MMT_STORE_INFO_TEST INF,( SELECT * FROM MMT.MMT_STORE_REQ_PROGRESS  WHERE   FLAG_PICK = 1 ORDER BY SEQ_NO DESC) PF  WHERE INF.REQ_NUM = PF.REQ_NUM ";

                //strSQL = "SELECT  REQ_NUM,REQ_STATUS,REQ_APPROVE_BY_NAME APPROVE_BY ,NVL(SUB_INV,TRANS_TYPE) TRANSFER_TO,DELIVERY_STATION ,case when sysdate - req_date < 0.001333 then 'new'else '' end as StateNew FROM  MMT.MMT_STORE_INFO";

                strSQL = " SELECT INF.REQ_NUM AS REQ_NUM ,INF.REQ_DATE AS REQ_DATE, INF.REQ_BY AS REQ_BY, sysdate AS ACTION_DATE, INF.REQ_STATUS AS PROGRESS , REQ_APPROVE_BY_NAME APPROVE_BY,  NVL(SUB_INV,TRANS_TYPE) TRANSFER_TO,DELIVERY_STATION , case when sysdate - req_date< 0.001333 then 'new'else '' end as StateNew , INF.REQ_APPROVE_BY , INF.SUB_INV  , INF.CUR_ACTIVITY ACTIVITY_EN , PC.PROCESS_COLORS ,INF.REQ_LOCATION  FROM MMT.MMT_STORE_INFO INF LEFT JOIN MMT.MMT_STORE_REQ_PROCESS PC ON INF.REQ_STATUS = PC.PROCESS_NAME  WHERE INF.REQ_NUM IS NOT NULL AND REQ_DATE < SYSDATE   ";
                //  strSQL += " ORDER BY REQ_NUM  desc ";

                int hTime = Convert.ToInt32(DateTime.Now.ToString("HH"));

                if (hTime >= 6 && hTime < 18)
                {
                    strSQL += " and  (req_date >= (trunc(SYSDATE) + interval '6' hour))";
                }
                else
                {
                    if (hTime > 18)
                    {
                        strSQL += " and  (req_date >= (trunc(SYSDATE) + interval '18' hour))";
                    }
                    else
                    {
                        strSQL += " and (req_date > (trunc(to_date(SYSDATE-1)) + interval '18' hour))";
                    }
                }
                strSQL += " ORDER BY REQ_NUM  desc ";
                db.TSql = strSQL;
                dt = db.GetDataOra();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRequisitionListAll(string JobType , UserAD ENOwner)
        {
            dt = new DataTable();
            try
            {
                // string InitName = (string)HttpContext.Current.Session["UserAD"];
                UserAD userAD = ENOwner;



                // var employee = (Employee)HttpContext.Current.Session["UserData"];

                //strSQL = "SELECT * FROM MMT.MMT_STORE_INFO_TEST WHERE ROWNUM <=50";  
                //strSQL = "SELECT REQ_NUM ,req_date,  REQ_BY,REQ_STATUS,SUB_INV,DELIVERY_STATION ,case when sysdate - req_date< 0.001333 then 'new'else '' end as StateNew  FROM mmt.MMT_STORE_INFO_TEST  WHERE REQ_DATE < sysdate AND REQ_DATE > sysdate -3 order by req_date desc";
                // strSQL = "SELECT * FROM  MMT.MMT_STORE_INFO_TEST  WHERE REQ_DATE < sysdate AND REQ_DATE > sysdate -3  order by REQ_DATE desc";

                //strSQL = " SELECT INF.REQ_NUM AS REQ_NUM, INF.REQ_BY AS REQ_BY, PF.APPROVED_DATE AS ACTION_DATE,  PF.SEQ_NO||' : '||PF.PROCESS_NAME AS PROGRESS ,case when sysdate - req_date< 0.001333 then 'new'else '' end as StateNew , INF.REQ_APPROVE_BY , INF.SUB_INV, PF.ACTIVITY_EN FROM  MMT.MMT_STORE_INFO_TEST INF,( SELECT * FROM MMT.MMT_STORE_REQ_PROGRESS  WHERE   FLAG_PICK = 1 ORDER BY SEQ_NO DESC) PF  WHERE INF.REQ_NUM = PF.REQ_NUM  ";

                //strSQL = "select * from( SELECT INF.REQ_NUM AS REQ_NUM ,INF.REQ_DATE AS REQ_DATE, INF.REQ_BY AS REQ_BY, PF.APPROVED_DATE AS ACTION_DATE, INF.REQ_STATUS AS PROGRESS , REQ_APPROVE_BY_NAME APPROVE_BY,  NVL(SUB_INV,TRANS_TYPE) TRANSFER_TO,DELIVERY_STATION ,   case when sysdate - req_date< 0.001333 then 'new'else '' end as StateNew , INF.REQ_APPROVE_BY , INF.SUB_INV, PF.ACTIVITY_EN     FROM  MMT.MMT_STORE_INFO INF Left join( SELECT * FROM MMT.MMT_STORE_REQ_PROGRESS  WHERE   FLAG_PICK = 1 ORDER BY SEQ_NO DESC) PF ON INF.REQ_NUM = PF.REQ_NUM  WHERE INF.REQ_NUM IS NOT NULL AND REQ_DATE < SYSDATE  ";

                strSQL = "SELECT INF.REQ_NUM AS REQ_NUM ,INF.REQ_DATE AS REQ_DATE, INF.REQ_BY AS REQ_BY, sysdate AS ACTION_DATE, INF.REQ_STATUS AS PROGRESS , REQ_APPROVE_BY_NAME APPROVE_BY,  NVL(SUB_INV,TRANS_TYPE) TRANSFER_TO,DELIVERY_STATION ,   case when sysdate - req_date< 0.001333 then 'new'else '' end as StateNew , INF.REQ_APPROVE_BY , INF.SUB_INV,  INF.CUR_ACTIVITY ACTIVITY_EN    FROM MMT.MMT_STORE_INFO INF  WHERE INF.REQ_NUM IS NOT NULL AND REQ_DATE < SYSDATE  ";

                switch (JobType)
                {
                    case "ActOwner":

                     //   strSQL += " AND ACTIVITY_EN = '" + (userAD.Departments.ToLower() == "packing" || userAD.Departments.ToLower() == "store"? userAD.Departments : userAD.InitName) + "' ";

                        if (userAD.Departments == null)
                        {
                            strSQL += " AND  INF.CUR_ACTIVITY = '" + userAD.InitName + "' ";
                        }
                        else
                        {
                            if (userAD.Departments.ToLower() == "packing" || userAD.Departments.ToLower() == "store")
                            {
                                strSQL += " AND  INF.CUR_ACTIVITY = '" + userAD.Departments.ToLower() + "' ";
                            }
                            else
                            {
                                strSQL += " AND  INF.CUR_ACTIVITY = '" + userAD.InitName + "' ";
                            }
                        }

                        //switch (userAD.Departments.ToLower())
                        //{
                        //    case "packing" || "store":
                        //        strSQL += " AND ACTIVITY_EN = '" + userAD.Departments + "' ";
                        //        break;
                        //    //case "store":
                        //    //    strSQL += "AND INF.REQ_APPROVE_BY = '" + ENOwner + "' ";
                        //    //    break;
                        //    default:
                        //        strSQL += "AND ACTIVITY_EN = '" + userAD.InitName + "' ";
                        //        break;
                        //}
                        break;
                    case "jobOwner": 

                        strSQL += "AND INF.REQ_BY = '" + userAD.EN + "' ";
                        break;
                }

             //   strSQL += " ORDER BY REQ_NUM  desc ) where to_date(req_date) >= to_date(SYSDATE-1) and to_date(req_date) <= to_date(SYSDATE)";

               

                int hTime = Convert.ToInt32( DateTime.Now.ToString("HH"));

                if(hTime >=6 && hTime < 18)
                {
                    strSQL += " and  (req_date >= (trunc(SYSDATE) + interval '6' hour))";
                }
                else
                {
                    if (hTime > 18)
                    {
                        strSQL += " and  (req_date >= (trunc(SYSDATE) + interval '18' hour))";
                    }
                    else
                    {
                        strSQL += " and (req_date < (trunc(to_date(SYSDATE-1)) + interval '18' hour))";

                    }
                } 

                strSQL += " ORDER BY REQ_NUM  desc ";

                db.TSql = strSQL;
                dt = db.GetDataOra();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRequisitionListAll(string JobType ,string strLocation,string strdays, UserAD ENOwner)
        {
            dt = new DataTable();
            try
            { 
                UserAD userAD = ENOwner; 
                
                strSQL = "SELECT APPROVED_DATE, INF.REQ_NUM AS REQ_NUM ,INF.REQ_DATE AS REQ_DATE, INF.REQ_BY AS REQ_BY, sysdate AS ACTION_DATE, INF.REQ_STATUS AS PROGRESS , REQ_APPROVE_BY_NAME APPROVE_BY,  NVL(SUB_INV,TRANS_TYPE) TRANSFER_TO,DELIVERY_STATION ,   case when sysdate - req_date< 0.001333 then 'new'else '' end as StateNew , INF.REQ_APPROVE_BY , INF.SUB_INV,  INF.CUR_ACTIVITY ACTIVITY_EN       FROM  MMT.MMT_STORE_INFO INF INNER JOIN ( SELECT REQ_NUM ,MAX(SEQ_NO)  AS SEQ_NO, MAX(APPROVED_DATE)  AS APPROVED_DATE FROM MMT.MMT_STORE_REQ_PROGRESS   WHERE APPROVED_DATE IS NOT NULL  GROUP BY REQ_NUM ) PF  ON INF.REQ_NUM = PF.REQ_NUM   LEFT JOIN MMT.MMT_STORE_REQ_PROCESS PC ON INF.REQ_STATUS = PC.PROCESS_NAME  WHERE INF.REQ_NUM IS NOT NULL AND REQ_DATE < SYSDATE     ";
                
                


                if (strLocation != "")
                {
                    strSQL += " AND REQ_LOCATION ='" + strLocation + "' ";
                } 

                switch (JobType)
                {
                    case "ActOwner":

                        //   strSQL += " AND ACTIVITY_EN = '" + (userAD.Departments.ToLower() == "packing" || userAD.Departments.ToLower() == "store"? userAD.Departments : userAD.InitName) + "' ";

                        if (userAD.Departments == null)
                        {
                            strSQL += " AND  INF.CUR_ACTIVITY = '" + userAD.InitName + "' ";
                        }
                        else
                        {
                            if (userAD.Departments.ToLower() == "packing")
                            {
                                strSQL += " AND  INF.CUR_ACTIVITY = '" + userAD.Departments.ToLower() + "' ";
                            }
                            else if (userAD.Departments.ToLower() == "store") {
                                strSQL += " AND  (INF.CUR_ACTIVITY = '" + userAD.Departments.ToLower() + "' OR  INF.CUR_ACTIVITY = '" + userAD.InitName.ToLower() + "') ";
                            }
                            else
                            {
                                strSQL += " AND  INF.CUR_ACTIVITY = '" + userAD.InitName + "' ";
                            }
                        }
                         
                        break;
                    case "jobOwner":

                        strSQL += "AND INF.REQ_BY_ID = '" + userAD.InitName + "' ";
                        break;
                    case "ReversePicking":
                        strSQL += " AND INF.REQ_STATUS = 'ON PROCESS PICKING'";
                        break;

                }
                //  strSQL += " ORDER BY REQ_NUM  desc ) where to_date(req_date) >= to_date(SYSDATE-1) and to_date(req_date) <= to_date(SYSDATE)";



                int hTime = Convert.ToInt32(DateTime.Now.ToString("HH"));

                if (strdays !="")
                {
                    strSQL += " and (req_date > (trunc(to_date(SYSDATE-"+ strdays+")) + interval '7' hour))";
                }
                else
                {
                    if (hTime >= 6 && hTime < 18)
                    {
                        strSQL += " and  (req_date >= (trunc(SYSDATE) + interval '6' hour))";
                    }
                    else
                    {
                        if (hTime > 18)
                        {
                            strSQL += " and  (req_date >= (trunc(SYSDATE) + interval '18' hour))";
                        }
                        else
                        {
                            strSQL += " and (req_date > (trunc(to_date(SYSDATE-1)) + interval '18' hour))";

                        }
                    }
                }


               




               // strSQL += " and (req_date > (trunc(to_date(SYSDATE-1)) + interval '18' hour))";


                strSQL += " ORDER BY APPROVED_DATE  asc ";
                db.TSql = strSQL;
                dt = db.GetDataOra();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRequisitionListAll_monitor()
        {
            dt = new DataTable();
            try
            {

                //strSQL = " SELECT INF.REQ_NUM AS REQ_NUM ,INF.REQ_DATE AS REQ_DATE, INF.REQ_BY AS REQ_BY, sysdate AS ACTION_DATE, CASE WHEN INF.REQ_STATUS ='SUBMIT' THEN 'CONFIRM PACK' ELSE INF.REQ_STATUS END AS PROGRESS , REQ_APPROVE_BY_NAME APPROVE_BY,  NVL(SUB_INV,TRANS_TYPE) TRANSFER_TO,DELIVERY_STATION , case when sysdate - req_date< 0.001333 then 'new'else '' end as StateNew , INF.REQ_APPROVE_BY , INF.SUB_INV  , INF.CUR_ACTIVITY ACTIVITY_EN , PC.PROCESS_COLORS ,INF.REQ_LOCATION  FROM MMT.MMT_STORE_INFO INF LEFT JOIN MMT.MMT_STORE_REQ_PROCESS PC ON INF.REQ_STATUS = PC.PROCESS_NAME  WHERE INF.REQ_NUM IS NOT NULL AND REQ_DATE < SYSDATE   ";
                //  strSQL += " ORDER BY REQ_NUM  desc ";


                //strSQL = " SELECT INF.REQ_NUM AS REQ_NUM ,INF.REQ_DATE AS REQ_DATE, INF.REQ_BY AS REQ_BY,SYSDATE AS ACTION_DATE, CASE WHEN INF.REQ_STATUS ='SUBMIT'   THEN 'CONFIRM PICK' ELSE INF.REQ_STATUS END AS PROGRESS , REQ_APPROVE_BY_NAME APPROVE_BY,  NVL(SUB_INV,TRANS_TYPE) TRANSFER_TO,DELIVERY_STATION ,  CASE WHEN SYSDATE - req_date< 0.001333 THEN 'new'ELSE '' END AS StateNew , INF.REQ_APPROVE_BY , INF.SUB_INV  , INF.CUR_ACTIVITY ACTIVITY_EN , PC.PROCESS_COLORS ,INF.REQ_LOCATION  ,PF.APPROVED_DATE  FROM  MMT.MMT_STORE_INFO INF INNER JOIN ( SELECT REQ_NUM , MAX(APPROVED_DATE)  AS APPROVED_DATE FROM MMT.MMT_STORE_REQ_PROGRESS   GROUP BY REQ_NUM ) PF  ON INF.REQ_NUM = PF.REQ_NUM   LEFT JOIN MMT.MMT_STORE_REQ_PROCESS PC ON INF.REQ_STATUS = PC.PROCESS_NAME    WHERE INF.REQ_NUM IS NOT NULL AND REQ_DATE < SYSDATE   AND PC.PROCESS_CODE IN ('REQ003','REQ004','REQ100','REQ005') ";

                strSQL = " SELECT INF.REQ_NUM AS REQ_NUM ,INF.REQ_DATE AS REQ_DATE, INF.REQ_BY AS REQ_BY,SYSDATE AS ACTION_DATE, INF.REQ_STATUS AS PROGRESS , REQ_APPROVE_BY_NAME APPROVE_BY,  NVL(SUB_INV,TRANS_TYPE) TRANSFER_TO,DELIVERY_STATION ,  CASE WHEN SYSDATE - req_date< 0.001333 THEN 'new'ELSE '' END AS StateNew , INF.REQ_APPROVE_BY , INF.SUB_INV  , INF.CUR_ACTIVITY ACTIVITY_EN , PC.PROCESS_COLORS ,INF.REQ_LOCATION  ,PF.APPROVED_DATE  FROM  MMT.MMT_STORE_INFO INF INNER JOIN ( SELECT REQ_NUM ,MAX(SEQ_NO)  AS SEQ_NO, MAX(APPROVED_DATE)  AS APPROVED_DATE FROM MMT.MMT_STORE_REQ_PROGRESS   WHERE APPROVED_DATE IS NOT NULL  GROUP BY REQ_NUM ) PF  ON INF.REQ_NUM = PF.REQ_NUM   LEFT JOIN MMT.MMT_STORE_REQ_PROCESS PC ON INF.REQ_STATUS = PC.PROCESS_NAME    WHERE INF.REQ_NUM IS NOT NULL AND REQ_DATE < SYSDATE   AND PC.PROCESS_CODE IN ('REQ003','REQ004','REQ100') ";

                int hTime = Convert.ToInt32(DateTime.Now.ToString("HH"));

                if (hTime >= 6 && hTime < 18)
                {
                    strSQL += " and  (req_date >= (trunc(SYSDATE) + interval '6' hour))";
                }
                else
                {
                    if (hTime > 18)
                    {
                        strSQL += " and  (req_date >= (trunc(SYSDATE) + interval '18' hour))";
                    }
                    else
                    {
                        strSQL += " and (req_date > (trunc(to_date(SYSDATE-1)) + interval '18' hour))";

                    }
                }

                strSQL += " ORDER BY   PF.APPROVED_DATE ASC";
                db.TSql = strSQL;
                dt = db.GetDataOra();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRequisitionListLF(string JobType, string strLocation, string strdays,string strTransferType, UserAD ENOwner)
        {
            dt = new DataTable();
            try
            {
                UserAD userAD = ENOwner;

                strSQL = " SELECT  inf.REQ_NUM , inf.REQ_LOCATION,  inf.REQ_DATE , NVL(inf.SUB_INV,inf.TRANS_TYPE) TRANSFER_TO ,inf.REQ_STATUS PROGRESS ,inf.DELIVERY_STATION , inf.REQ_BY , inf.REQ_BY_TEL  FROM MMT.MMT_STORE_INFO inf where inf.req_num not in (select req_num from   mmt.MMT_STORE_REQ_ROUND_DT )   AND req_location = 'LF' AND INF.REQ_NUM IS NOT NULL AND REQ_DATE < SYSDATE ";




                if (strLocation != "")
                {
                    strSQL += " AND REQ_LOCATION ='" + strLocation + "' ";
                }

                switch (JobType)
                {
                    case "ActOwner":

                        //   strSQL += " AND ACTIVITY_EN = '" + (userAD.Departments.ToLower() == "packing" || userAD.Departments.ToLower() == "store"? userAD.Departments : userAD.InitName) + "' ";

                        if (userAD.Departments == null)
                        {
                            strSQL += " AND  INF.CUR_ACTIVITY = '" + userAD.InitName + "' ";
                        }
                        else
                        {
                            if (userAD.Departments.ToLower() == "packing")
                            {
                                strSQL += " AND  INF.CUR_ACTIVITY = '" + userAD.Departments.ToLower() + "' ";
                            }
                            else if (userAD.Departments.ToLower() == "store")
                            {
                                strSQL += " AND  (INF.CUR_ACTIVITY = '" + userAD.Departments.ToLower() + "' OR  INF.CUR_ACTIVITY = '" + userAD.InitName.ToLower() + "') ";
                            }
                            else
                            {
                                strSQL += " AND  INF.CUR_ACTIVITY = '" + userAD.InitName + "' ";
                            }
                        }

                        break;
                    case "jobOwner":

                        strSQL += "AND INF.REQ_BY_ID = '" + userAD.InitName + "' ";
                        break;
                    case "ReversePicking":
                        strSQL += " AND INF.REQ_STATUS = 'ON PROCESS PICKING'";
                        break;

                } 

                switch (strTransferType)
                {
                    case "RM temp 0 - 5c": 
                        strSQL += " AND SUBSTR(NVL(inf.SUB_INV,inf.TRANS_TYPE),0,5) != 'OS IS'";

                        break;
                    case "Chemical":
                        strSQL += " AND SUBSTR(NVL(inf.SUB_INV,inf.TRANS_TYPE),0,5) = 'OS IS'";
                        break;
                }

                strSQL += " AND inf.REQ_STATUS = 'APPROVED'";



                int hTime = Convert.ToInt32(DateTime.Now.ToString("HH"));

                if (strdays != "")
                {
                    strSQL += " and (req_date > (trunc(to_date(SYSDATE-" + strdays + ")) + interval '7' hour))";
                }
                else
                {
                    if (hTime >= 6 && hTime < 18)
                    {
                        strSQL += " and  (req_date >= (trunc(SYSDATE) + interval '6' hour))";
                    }
                    else
                    {
                        if (hTime > 18)
                        {
                            strSQL += " and  (req_date >= (trunc(SYSDATE) + interval '18' hour))";
                        }
                        else
                        {
                            strSQL += " and (req_date > (trunc(to_date(SYSDATE-1)) + interval '18' hour))";

                        }
                    }
                }







                // strSQL += " and (req_date > (trunc(to_date(SYSDATE-1)) + interval '18' hour))";


                strSQL += "  GROUP BY inf.REQ_NUM , inf.REQ_LOCATION,  inf.REQ_DATE , NVL(inf.SUB_INV,inf.TRANS_TYPE)  ,inf.REQ_STATUS  ,inf.DELIVERY_STATION , inf.REQ_BY , inf.REQ_BY_TEL  ";
                db.TSql = strSQL;
                dt = db.GetDataOra();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetRequisitionListActiveOwner(string ENOnwer)
        {
            dt = new DataTable();
            try
            {

                strSQL = " SELECT INF.REQ_NUM AS REQ_NUM, INF.REQ_BY AS REQ_BY, PF.APPROVED_DATE AS ACTION_DATE,  PF.SEQ_NO||' : '||PF.PROCESS_NAME AS PROGRESS ,case when sysdate - req_date< 0.001333 then 'new'else '' end as StateNew , INF.REQ_APPROVE_BY FROM  MMT.MMT_STORE_INFO INF,( SELECT * FROM MMT.MMT_STORE_REQ_PROGRESS  WHERE   FLAG_PICK = 1 ORDER BY SEQ_NO DESC) PF  WHERE INF.REQ_NUM = PF.REQ_NUM  AND INF.REQ_APPROVE_BY = '"+ ENOnwer+"' ";
                db.TSql = strSQL;
                dt = db.GetDataOra();

                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable GetStore_Req_Info(string Req_Num)
        {
            dt = new DataTable();
            try
            {
                //strSQL = "SELECT inf.REQ_LOCATION,inf.DELIVERY_STATION,inf.REQ_BY,inf.REQ_BY_TEL, TO_CHAR(inf.REQ_DATE, 'YYYY/MM/DD') AS REQ_DATE, TO_CHAR(inf.REQ_DATE, 'hh:mm:ss') AS REQ_TIME ,inf.ISSUE_TYPE,inf.SUB_INV,inf.REQ_REMARK ,inf.REQ_APPROVE_BY  ,emp.emp_name_eng as REQ_APPROVE_NAME_BY FROM MMT.MMT_STORE_INFO_TEST inf left join  MMT_HR_EMPLOYEE_V emp ON inf.req_Approve_by = emp.emp_no WHERE inf.REQ_NUM ='" + Req_Num + "'";

                //strSQL = "SELECT inf.REQ_LOCATION, inf.DELIVERY_STATION, inf.REQ_BY,inf.REQ_BY_TEL,  TO_CHAR(inf.REQ_DATE, 'YYYY/MM/DD') AS REQ_DATE, TO_CHAR(inf.REQ_DATE, 'hh:mm:ss') AS REQ_TIME, inf.ISSUE_TYPE, inf.SUB_INV, inf.REQ_REMARK , inf.REQ_APPROVE_BY  , emp.emp_name_eng as REQ_APPROVE_NAME_BY , PG.SEQ_NO || ' : ' || PG.PROCESS_NAME AS PROCESS_NAME FROM MMT.MMT_STORE_INFO_TEST inf left join  MMT_HR_EMPLOYEE_V emp ON inf.req_Approve_by = emp.emp_no left join(SELECT* FROM MMT.MMT_STORE_REQ_PROGRESS WHERE   FLAG_PICK = 1 ORDER BY SEQ_NO DESC) PG ON  PG.REQ_NUM = INF.REQ_NUM where REQ_Approve_By is not null and  inf.REQ_NUM ='" + Req_Num + "'"; 
                strSQL = "SELECT  inf.REQ_LOCATION, inf.DELIVERY_STATION, inf.REQ_BY,inf.REQ_BY_TEL,  TO_CHAR(inf.REQ_DATE, 'YYYY/MM/DD') AS REQ_DATE, TO_CHAR(inf.REQ_DATE, 'hh:mm:ss') AS REQ_TIME, inf.ISSUE_TYPE, NVL(inf.SUB_INV,inf.TRANS_TYPE) SUB_INV, inf.REQ_REMARK , inf.REQ_APPROVE_BY  ,inf.INF_HEADER_ID , inf.INF_DATE , CASE WHEN APD.CREATE_BY IS NULL THEN emp.emp_name_eng ||' <b class=text-danger >(PENDING)</b>'ELSE emp.emp_name_eng END AS REQ_APPROVE_NAME_BY , PG.SEQ_NO SEQ_NO,PG.PROCESS_NAME AS PROCESS_NAME FROM MMT.MMT_STORE_INFO inf left join  MMT_HR_EMPLOYEE_V emp ON inf.req_Approve_by = emp.emp_no left join(SELECT* FROM MMT.MMT_STORE_REQ_PROGRESS WHERE   FLAG_PICK = 1 ORDER BY SEQ_NO DESC) PG ON  PG.REQ_NUM = INF.REQ_NUM LEFT JOIN MMT.MMT_STORE_REQ_APPROVED APD  ON  APD.REQ_NUM = INF.REQ_NUM where inf.REQ_NUM ='" + Req_Num + "'";
                db.TSql = strSQL;
                dt = db.GetDataOra();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public DataTable GetStore_Req_Info(string Req_Num, string Approve_by)
        //{
        //    dt = new DataTable();
        //    try
        //    {
                
        //        strSQL = "SELECT  inf.REQ_LOCATION, inf.DELIVERY_STATION, inf.REQ_BY,inf.REQ_BY_TEL,  TO_CHAR(inf.REQ_DATE, 'YYYY/MM/DD') AS REQ_DATE, TO_CHAR(inf.REQ_DATE, 'hh:mm:ss') AS REQ_TIME, inf.ISSUE_TYPE, NVL(inf.SUB_INV,inf.TRANS_TYPE) SUB_INV, inf.REQ_REMARK , inf.REQ_APPROVE_BY  , CASE WHEN APD.CREATE_BY IS NULL THEN emp.emp_name_eng ||' <b class=text-danger >(PENDING)</b>'ELSE emp.emp_name_eng END AS REQ_APPROVE_NAME_BY , PG.SEQ_NO || ' : ' || PG.PROCESS_NAME AS PROCESS_NAME FROM MMT.MMT_STORE_INFO inf left join  MMT_HR_EMPLOYEE_V emp ON inf.req_Approve_by = emp.emp_no left join(SELECT* FROM MMT.MMT_STORE_REQ_PROGRESS WHERE   FLAG_PICK = 1 ORDER BY SEQ_NO DESC) PG ON  PG.REQ_NUM = INF.REQ_NUM LEFT JOIN MMT.MMT_STORE_REQ_APPROVED APD  ON  APD.REQ_NUM = INF.REQ_NUM where REQ_Approve_By is not null and  inf.REQ_NUM ='" + Req_Num + "' And  inf.REQ_APPROVE_BY ='"+Approve_by+"'";
        //        db.TSql = strSQL;
        //        dt = db.GetDataOra();

        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public DataTable GetStore_req_issue_items(string REQ_NUM)
        {
            dt = new DataTable();
            string INF_HEADER_ID ="";
            try
            {
                strSQL = "SELECT INF_HEADER_ID FROM MMT.MMT_STORE_INFO WHERE REQ_NUM = '" + REQ_NUM + "'";
                db.TSql = strSQL;
                dt = db.GetDataOra();

                if (dt.Rows.Count > 0) 
                {
                    INF_HEADER_ID =  dt.Rows[0]["INF_HEADER_ID"].ToString();
                }

                dt = new DataTable();




                strSQL = "select MTLT.TRANSACTION_SET_ID, MTLT.WAYBILL_AIRBILL STORE_SLIP_NO, MTLI.SEGMENT1, MTLT.INVENTORY_ITEM_ID, MTLI.DESCRIPTION, MTLT.TRANSFER_SUBINVENTORY SUB_TO, MTLT.SUBINVENTORY_CODE SUB_FROM, (MTLT.TRANSACTION_QUANTITY*-1) TRANS_QTY, MTLT.TRANSACTION_UOM TRANS_UOM, MTLT.PRIMARY_QUANTITY PRIMARY_QTY, MTLI.PRIMARY_UOM_CODE, MTLT.ATTRIBUTE1 DOC_NO, TRUNC(MTLT.TRANSACTION_DATE) TRANSACTION_DATE, TO_CHAR(MTLT.TRANSACTION_DATE,'HH:MI:SS AM') TRANSACTION_TIME , (MTLN.TRANSACTION_QUANTITY * -1)  LOT_TRANSACTION_QTY, MTLN.PRIMARY_QUANTITY LOT_PRIMARY_QTY, MTLN.LOT_NUMBER LOT_NUMBER, MTLT.TRANSACTION_ID, MLB.INVOICE_NUMBER LOT_INVOCE_NO,  MLB.SUPPLIER_LOT_NUMBER LOT_SUPPLIER_LOT ,MTLT.ATTRIBUTE2 REQUESTOR_BY, MTLT.ATTRIBUTE3 ISSUE_BY from   (select * from MTL_MATERIAL_TRANSACTIONS MTLT  where MTLT.TRANSACTION_SET_ID = '" + INF_HEADER_ID + "'  AND MTLT.ORGANIZATION_ID = 84  and MTLT.TRANSACTION_QUANTITY < 0 ) MTLT, (select * from MTL_SYSTEM_ITEMS MTLI where  MTLI.ORGANIZATION_ID = 84  ) MTLI, MTL_TRANSACTION_LOT_NUMBERS MTLN, mmt_receiving_labels MLB where MTLN.TRANSACTION_ID = MTLT.TRANSACTION_ID AND MTLI.INVENTORY_ITEM_ID = MTLT.INVENTORY_ITEM_ID AND MTLN.INVENTORY_ITEM_ID = MLB.INVENTORY_ITEM_ID AND MTLN.LOT_NUMBER = MLB.LOT_NUMBER  order by MTLI.SEGMENT1 asc";
                db.TSql = strSQL;
                dt = db.GetDataOra();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetStore_req_item(string Req_Num)
        {
            dt = new DataTable();
            try
            {
                strSQL = "select itm.ITEM_NUM,itm.PART_NUM || ' : ' ||MSI.DESCRIPTION as PART_NUM,itm.UOM,itm.QTY_REQ,itm.ACTUAL,itm.PENDING,itm.SHELF ,itm.EXPIRE  FROM MMT.MMT_STORE_ITEM  itm inner join ( SELECT MSI.SEGMENT1, MSI.DESCRIPTION FROM Mtl_system_items MSI, MTL_ITEM_CATEGORIES a,MTL_CATEGORIES b  WHERE msi.Organization_id = 84  AND a.organization_id = 84  AND msi.Inventory_item_status_code = 'Active'  AND msi.Inventory_item_ID = a.Inventory_ITEM_ID  AND a.category_ID = b.category_ID ) MSI on MSI.SEGMENT1 = itm.PART_NUM WHERE itm.REQ_NUM ='" + Req_Num + "'";

                // strSQL = "SELECT ITEM_NUM,PART_NUM,UOM,QTY_REQ,ACTUAL,PENDING,SHELF ,EXPIRE FROM MMT.MMT_STORE_ITEM WHERE REQ_NUM = '" + Req_Num + "'";
                db.TSql = strSQL;
                dt = db.GetDataOra();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getStore_info_approved(string Req_Num , string ENApprove)
        {
            dt = new DataTable();
            try
            {
                strSQL = "SELECT  * FROM  MMT.MMT_STORE_INFO  WHERE  REQ_NUM ='"+ Req_Num+"' and REQ_APPROVE_BY = '"+ ENApprove+"'";
                db.TSql = strSQL;
                dt = db.GetDataOra();

                if(dt.Rows.Count > 0)
                {
                    dt = new DataTable();
                    strSQL = "SELECT  * FROM  MMT.MMT_STORE_REQ_PROGRESS  WHERE  REQ_NUM ='" + Req_Num + "' AND FLAG_PICK = 1 and ACTIVITY_EN = '" + ENApprove + "'";
                    db.TSql = strSQL;
                    dt = db.GetDataOra();

                    if(dt.Rows.Count == 0)
                    {
                        return dt = new DataTable();
                    }
                }
                else
                {
                    dt = new DataTable();
                    strSQL = "SELECT  * FROM  MMT.MMT_STORE_REQ_PROGRESS  WHERE  REQ_NUM ='" + Req_Num + "' AND FLAG_PICK = 1 and ACTIVITY_EN = '" + ENApprove + "'";
                    db.TSql = strSQL;
                    dt = db.GetDataOra();

                    if (dt.Rows.Count == 0)
                    {
                        return dt = new DataTable();
                    }
                }



                return dt;
            }
            catch (Exception)
            {
                throw;
            } 
        }

        public DataTable getRequisitionInfo(string Req_Num)
        {
            DataTable dt = new DataTable();
            try
            {
                strSQL = "SELECT * FROM  MMT.MMT_STORE_INFO WHERE REQ_NUM ='" + Req_Num + "'";
                db.TSql = strSQL;
                dt = db.GetDataOra();

                if (dt.Rows.Count == 0)
                {
                    return dt = new DataTable();
                }

                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}