using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using StoreRequisition.Class;
using StoreRequisition.Models;
using System;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls; 

public partial class Frm_Send_Requisition : System.Web.UI.Page

{
    ClassDB db = new ClassDB();
    clsAuthenticate aAuthent = new clsAuthenticate();

    DataTable dtTempDetial;
    DataTable dt;

    DataTable dtTmp;

    String REQ_STATUS = "";
    String REQ_NUM = "";
    String ITEM_NUM = "";
    String LOCATION = "";
    Int32 INVENTORY_ITEM_ID;
    Int32 TmpC_FIFO = 0;
    String TmpS_FiFp = "";
    Double TmpC_Onhand = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserAD"] == null)
        {
            //Response.Redirect("~/Page/Frm_Login.aspx");
            Response.Redirect("~/Page/Frm_Login_R.aspx");
        }

        if (!IsPostBack)
        {
            ViewState["DataT"] = TempTable();

            dt = new DataTable();
            db.TSql = "select 'RD'|| to_char(sysdate,'RRMMDD')||  substr(   to_char( NVL( max(substr(round_no,9,3))+1001,'1001')),2,3) GEN_ROUND from MMT_STORE_REQ_ROUND where substr(round_no,3,6) = to_char(sysdate,'RRMMDD')";
            dt = db.GetDataOra();
            if (dt.Rows.Count > 0)
            {
                REQ_STATUS = "NEW";
                REQ_NUM = dt.Rows[0]["GEN_ROUND"].ToString();
                txtRoundNum.Text = REQ_NUM;

                db.TSql = "insert into mmt.mmt_store_req_round (round_no,req_status,req_location) values ('" + txtRoundNum.Text + "','NEW','" + ddlLocation.SelectedItem.ToString() + "')";
                db.InsertDataOra();
            }
            
        }
       
    }


    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedItem.Value != "Please Select ...")
        {
            ddlType.Enabled = true;
            ddlType.Focus();
        }
        else
        {
            ddlType.Enabled = false;
        }
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedItem.Value != "Please Select ...")
        {
            ddlRoundTime.Enabled = true;
            ddlRoundTime.Focus();
        }
        else
        {
            ddlRoundTime.Enabled = false;
        }
    }

    protected void ddlRoundTime_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRoundTime.SelectedItem.Value != "Please Select ...")
        {
           
            txtRequestNum.Enabled = true;
            GetDataReqisitionList();
            txtRequestNum.Focus();
        }
        else
        {
            txtRequestNum.Enabled = false;
        }
    }

    private DataTable TempTable()
    {
        dtTempDetial = new DataTable();
        dtTempDetial.Columns.Add("Item", typeof(string));
        dtTempDetial.Columns.Add("PN", typeof(string));
        dtTempDetial.Columns.Add("Type", typeof(string));
        return dtTempDetial;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        db.TSql = "UPDATE mmt_store_req_round SET req_status = 'CREATE FILE',req_date = sysdate,req_location = '" + ddlLocation.SelectedItem.ToString() + "' WHERE round_no = '" + txtRoundNum.Text + "'";
        db.InsertDataOra();

        for (int r = 0; r < dgvMachine.Rows.Count; r++)
        {
            string RN = dgvMachine.Rows[r].Cells[1].Text.ToString();

            db.TSql = "INSERT INTO MMT_STORE_REQ_ROUND_DT (round_no,req_num,update_date) VALUES('" + txtRoundNum.Text + "','" + RN + "',sysdate)";
            db.InsertDataOra();

            dt = new DataTable();
            db.TSql = " SELECT MMT_STORE_INFO.* , MMT_STORE_ITEM.* , c.segment2 ITEM_CAT , i.DESCRIPTION, i.inventory_item_id From MMT_STORE_INFO ,MMT_STORE_ITEM ,WIp_def_cat_acc_classes_v w ,mtl_item_categories ic ,mtl_categories_b c,mtl_system_items_b i Where MMT_STORE_INFO.REQ_NUM = MMT_STORE_ITEM.REQ_NUM And w.organization_id(+) = 84 And C.STRUCTURE_ID = '101' And C.ENABLED_FLAG = 'Y' And i.organization_id = 84 And i.inventory_item_id = ic.inventory_item_id And ic.organization_id = 84  AND c.category_id = ic.category_id  And w.category_id(+) = ic.category_id And MMT_STORE_ITEM.PART_NUM = i.segment1 And MMT_STORE_INFO.REQ_NUM in ('" + RN + "') ORDER BY MMT_STORE_ITEM.REQ_NUM";
            dt = db.GetDataOra();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                Paragraph paragraph = new Paragraph("Store's Material Requisition");
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Font.Size = 10;
                document.Add(paragraph);

                paragraph = new Paragraph("Requisition Number");
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Font.Size = 8;
                document.Add(paragraph);

                paragraph = new Paragraph(dt.Rows[0]["REQ_NUM"].ToString());
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Font.Size = 8;
                document.Add(paragraph);

                paragraph = new Paragraph("   ");
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Font.Size = 8;
                document.Add(paragraph);

                string tmpHTML = "<table style='width: 100 %;  font-size : 8px;'>";
                tmpHTML = tmpHTML + "<tr>";
                tmpHTML = tmpHTML + "<td width='5%'><div align='center'>Item</div></td>";
                tmpHTML = tmpHTML + "<td><div align='center' style =' font-size : 8px'>P/N</div></td>";
                tmpHTML = tmpHTML + "<td width='5%'><div align='center'>Shelf</div></td>";
                tmpHTML = tmpHTML + "<td width='5%'><div align='center'>FI-FO / Expire *</div></td>";
                tmpHTML = tmpHTML + "<td width='5%'><div align='center'>Uom</div></td>";
                tmpHTML = tmpHTML + "<td width='5%'><div align='center'>Qty</div></td>";
                tmpHTML = tmpHTML + "<td width='5%'><div align='center'>Actual</div></td>";
                tmpHTML = tmpHTML + "<td width='5%'><div align='center'>Pending</div></td>";
                tmpHTML = tmpHTML + "</tr>";

                double S_Qty = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tmpHTML = tmpHTML + "<tr>";
                    tmpHTML = tmpHTML + "<td width='5%'><div align='center'>" + dt.Rows[i]["ITEM_NUM"].ToString() + "</div></td>";
                    tmpHTML = tmpHTML + "<td><div align='center'>" + dt.Rows[i]["PART_NUM"].ToString() + " : " + dt.Rows[0]["DESCRIPTION"].ToString() + "</div></td>";
                    tmpHTML = tmpHTML + "<td width='5%'><div align='center'>" + getSHELF(dt.Rows[i]["PART_NUM"].ToString()) + "</div></td>";
                    tmpHTML = tmpHTML + "<td width='5%'><div align='center'>" + getFIFO(dt.Rows[i]["inventory_item_id"].ToString()) + "</div></td>";
                    tmpHTML = tmpHTML + "<td width='5%'><div align='center'>" + dt.Rows[i]["UOM"].ToString() + "</div></td>";
                    tmpHTML = tmpHTML + "<td width='5%'><div align='center'>" + dt.Rows[i]["QTY_REQ"].ToString() + "</div></td>";
                    tmpHTML = tmpHTML + "<td width='5%'><div align='center'>" + dt.Rows[i]["ACTUAL"].ToString() + "</div></td>";
                    tmpHTML = tmpHTML + "<td width='5%'><div align='center'>" + dt.Rows[i]["PENDING"].ToString() + "</div></td>";
                    tmpHTML = tmpHTML + "</tr>";

                    S_Qty = S_Qty + Convert.ToDouble(dt.Rows[i]["QTY_REQ"].ToString());
                }

                tmpHTML = tmpHTML + "</table>";

                StringReader srs = new StringReader(tmpHTML.ToString());
                HTMLWorker htmlparsers = new HTMLWorker(document);
                htmlparsers.Parse(srs);

                paragraph = new Paragraph("   ");
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Font.Size = 8;
                document.Add(paragraph);

                paragraph = new Paragraph("Total by Category : " + S_Qty.ToString() + "");
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font.Size = 8;
                document.Add(paragraph);

                paragraph = new Paragraph("Total by Requisition Number : " + S_Qty.ToString() + "");
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font.Size = 8;
                document.Add(paragraph);

                string tmpHTMLs = " <table style='width: 100%;  font-size : 8px;'>";
                tmpHTMLs = tmpHTMLs + "<tr>";
                tmpHTMLs = tmpHTMLs + "<td>Delivey Status :</td>";
                tmpHTMLs = tmpHTMLs + "<td>" + dt.Rows[0]["DELIVERY_STATION"].ToString() + "</td>";
                tmpHTMLs = tmpHTMLs + "<td>&nbsp;</td>";
                tmpHTMLs = tmpHTMLs + "<td>&nbsp;</td>";
                tmpHTMLs = tmpHTMLs + "<td>&nbsp;</td>";
                tmpHTMLs = tmpHTMLs + "<td>&nbsp;</td>";
                tmpHTMLs = tmpHTMLs + "</tr>";
                tmpHTMLs = tmpHTMLs + "<tr>";
                tmpHTMLs = tmpHTMLs + "<td>Request By :</td>";
                tmpHTMLs = tmpHTMLs + "<td>" + dt.Rows[0]["REQ_BY"].ToString().ToUpper() + "</td>";
                tmpHTMLs = tmpHTMLs + "<td>Date :</td>";
                tmpHTMLs = tmpHTMLs + "<td>" + Convert.ToDateTime(dt.Rows[0]["REQ_DATE"]).ToString("dd/MM/yyyy") + "</td>";
                tmpHTMLs = tmpHTMLs + "<td>Time :</td>";
                tmpHTMLs = tmpHTMLs + "<td>" + Convert.ToDateTime(dt.Rows[0]["REQ_DATE"]).ToString("HH:mm:ss") + "</td>";
                tmpHTMLs = tmpHTMLs + "</tr>";
                tmpHTMLs = tmpHTMLs + "<tr>";
                tmpHTMLs = tmpHTMLs + "<td>Sub Inventory :</td>";
                tmpHTMLs = tmpHTMLs + "<td> " + dt.Rows[0]["SUB_INV"].ToString() + " </td>";
                tmpHTMLs = tmpHTMLs + "<td colspan='4'><img style=' - webkit - user - select: none; margin: auto; cursor: zoom -in; background - color: hsl(0, 0 %, 90 %); transition: background - color 300ms; ' src='http://intranet2.mektec.co.th/mmctlabel/LabelHandler.ashx?text=" + dt.Rows[0]["SUB_INV"].ToString() + "' width='250' height='20'></td>";
                tmpHTMLs = tmpHTMLs + "</tr>";
                tmpHTMLs = tmpHTMLs + "<tr>";
                tmpHTMLs = tmpHTMLs + "<td>Transection Type :</td>";
                tmpHTMLs = tmpHTMLs + "<td> " + dt.Rows[0]["TRANS_TYPE"].ToString() + " </td>";
                tmpHTMLs = tmpHTMLs + "<td colspan='4'><img style=' - webkit - user - select: none; margin: auto; cursor: zoom -in; background - color: hsl(0, 0 %, 90 %); transition: background - color 300ms; ' src='http://intranet2.mektec.co.th/mmctlabel/LabelHandler.ashx?text=" + dt.Rows[0]["TRANS_TYPE"].ToString() + "' width='250' height='20'></td>";
                tmpHTMLs = tmpHTMLs + "</tr>";
                tmpHTMLs = tmpHTMLs + "<tr>";
                tmpHTMLs = tmpHTMLs + "<td>Load start :</td>";
                tmpHTMLs = tmpHTMLs + "<td>" + dt.Rows[0]["LOAD_START"].ToString() + "</td>";
                tmpHTMLs = tmpHTMLs + "<td>Load Finish :</td>";
                tmpHTMLs = tmpHTMLs + "<td>" + dt.Rows[0]["LOAD_FINISH"].ToString() + "</td>";
                tmpHTMLs = tmpHTMLs + "<td>Load By :</td>";
                tmpHTMLs = tmpHTMLs + "<td>" + dt.Rows[0]["LOAD_BY"].ToString() + "</td>";
                tmpHTMLs = tmpHTMLs + "</tr>";
                tmpHTMLs = tmpHTMLs + "<tr>";
                tmpHTMLs = tmpHTMLs + "<td>Delivery Start :</td>";
                tmpHTMLs = tmpHTMLs + "<td>" + dt.Rows[0]["DELI_START"].ToString() + "</td>";
                tmpHTMLs = tmpHTMLs + "<td>Delivery Finish :</td>";
                tmpHTMLs = tmpHTMLs + "<td>" + dt.Rows[0]["DELI_FINISH"].ToString() + "</td>";
                tmpHTMLs = tmpHTMLs + "<td>Delivery By :</td>";
                tmpHTMLs = tmpHTMLs + "<td>" + dt.Rows[0]["DELI_BY"].ToString() + "</td>";
                tmpHTMLs = tmpHTMLs + "</tr>";
                tmpHTMLs = tmpHTMLs + "</table>";

                StringReader srss = new StringReader(tmpHTMLs.ToString());
                HTMLWorker htmlparserss = new HTMLWorker(document);
                htmlparserss.Parse(srss);

                document.Close();

                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                MailMessage mm = new MailMessage("Store_Center@mektec.co.th", "LFLTHLMEKTECDG@LFLogistics.com");
                mm.To.Add("LFLTHLMEKTECRM@LFLogistics.com");
                mm.To.Add("NattayaKunthol@LFLogistics.com");
                mm.To.Add("KedsarinPangawtong@LFLogistics.com");

                mm.CC.Add("Store_Center@mektec.co.th");
                mm.CC.Add("noppadolj@mektec.co.th");
                mm.CC.Add("pichetk@mektec.co.th");
                //mm.CC.Add("anurakw@mektec.co.th");

                //MailMessage mm = new MailMessage("Store_Center@mektec.co.th", "anurakw@mektec.co.th");
                ////mm.To.Add("LFLTHLMEKTECDG@LFLogistics.com");
                ////mm.To.Add("LFLTHLMEKTECRM@LFLogistics.com");
                ////mm.To.Add("NattayaKunthol@LFLogistics.com");
                ////mm.To.Add("KedsarinPangawtong@LFLogistics.com");

                //mm.CC.Add("Store_Center@mektec.co.th");
                ////mm.CC.Add("noppadolj@mektec.co.th");
                ////mm.CC.Add("pichetk@mektec.co.th");

                mm.Subject = "Store Request raw material " + DateTime.Now.ToString("dd-MMM-yyyy") + " [" + ddlType.SelectedItem.ToString() + "] " + " (รอบ " + ddlRoundTime.SelectedItem.ToString() + " น.) [Auto]";
                mm.Attachments.Add(new Attachment(new MemoryStream(bytes), RN + ".pdf"));
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("192.168.236.22");
                smtp.Credentials = new System.Net.NetworkCredential("Store_Center@mektec.co.th", "storec");
                smtp.Port = 25;
                smtp.Send(mm);
            }
        }

        string script = "alert(\"" + "บันทึกข้อมูลเรียบร้อยแล้ว" + "\");";
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        Div4.Visible = false;
        txtRequestNum.Text = "";
        btnSubmit.Enabled = false;
        ViewState["DataT"] = TempTable();
        dgvMachine.DataSource = (DataTable)ViewState["DataT"];
        dgvMachine.DataBind();
        GetDataReqisitionList();
    }


    protected void GetDataReqisitionList()
    {
        clsSQLscript objRun = new clsSQLscript();

        DataTable dt = new DataTable();
        UserAD userAD = new UserAD();
        userAD = (UserAD)Session["UserAD"];

        string strTransferType = ddlType.Text;


        dt = objRun.GetRequisitionListLF("", "LF", "", strTransferType, userAD);
        
        if (dt.Rows.Count > 0)
        {
            string[] colomn = new[] { "REQ_NUM", "PROGRESS", "REQ_BY", "REQ_BY_TEL", "REQ_LOCATION", "TRANSFER_TO", "DELIVERY_STATION" };            
            DataTable dt1 = new DataView(dt).ToTable(false, colomn);            
            dgvLFRequisitionList.DataSource = dt1;
            dgvLFRequisitionList.DataBind();
        }

    }

    protected void GetDatatoItemList(string strReqNum)
    {

      //  string strReqNum = 
        dt = new DataTable();
        db.TSql = "SELECT * FROM MMT_STORE_REQ_ROUND_DT WHERE req_num = '" + strReqNum + "'";
        dt = db.GetDataOra();
        if (dt.Rows.Count == 0)
        {
            db.TSql = "SELECT MMT_STORE_INFO.* , MMT_STORE_ITEM.* , c.segment2 ITEM_CAT , i.DESCRIPTION, i.inventory_item_id From MMT_STORE_INFO ,MMT_STORE_ITEM ,WIp_def_cat_acc_classes_v w ,mtl_item_categories ic ,mtl_categories_b c,mtl_system_items_b i Where MMT_STORE_INFO.REQ_NUM = MMT_STORE_ITEM.REQ_NUM And w.organization_id(+) = 84 And C.STRUCTURE_ID = '101' And C.ENABLED_FLAG = 'Y' And i.organization_id = 84 And i.inventory_item_id = ic.inventory_item_id And ic.organization_id = 84  AND c.category_id = ic.category_id  And w.category_id(+) = ic.category_id And MMT_STORE_ITEM.PART_NUM = i.segment1 And MMT_STORE_INFO.REQ_NUM in ('" + strReqNum + "') ORDER BY MMT_STORE_ITEM.REQ_NUM";
            dt = db.GetDataOra();
            if (dt.Rows.Count != 0)
            {
                if (ViewState["DataT"] != null)
                {
                    dt = (DataTable)ViewState["DataT"];
                    if (dt.Select("PN='" + strReqNum + "'").Length == 0)
                    {
                        DataRow rd = dt.NewRow();
                        rd["Item"] = (dt.Rows.Count + 1).ToString();
                        rd["PN"] = strReqNum;
                        rd["Type"] = ddlType.SelectedItem.ToString();
                        dt.Rows.Add(rd);
                        dt.DefaultView.Sort = "Item";
                        ViewState["DataT"] = dt;
                        dgvMachine.DataSource = (DataTable)ViewState["DataT"];
                        dgvMachine.DataBind();
                        dtTmp = dt;
                        Div4.Visible = true;
                        btnSubmit.Enabled = true;

                        txtRequestNum.Text = "";
                        txtRequestNum.Focus();
                    }
                    else
                    {
                        txtRequestNum.Text = "";
                        txtRequestNum.Focus();

                        string script = "alert(\"" + "พบข้อมูลในระบบแล้ว" + "\");";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                    }
                }
            }
            else
            {
                string script = "alert(\"" + "ไม่พบข้อมูลนี้ในระบบ" + "\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                Div4.Visible = false;
                btnSubmit.Enabled = false;
            }
        }
        else
        {
            string script = "alert(\"" + "พบข้อมูลนี้ในระบบแล้ว" + "\");";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }
    }


    protected void txtRequestNum_TextChanged(object sender, EventArgs e)
    {
        dt = new DataTable();
        db.TSql = "SELECT * FROM MMT_STORE_REQ_ROUND_DT WHERE req_num = '" + txtRequestNum.Text + "'";
        dt = db.GetDataOra();
        if (dt.Rows.Count == 0)
        {
            db.TSql = "SELECT MMT_STORE_INFO.* , MMT_STORE_ITEM.* , c.segment2 ITEM_CAT , i.DESCRIPTION, i.inventory_item_id From MMT_STORE_INFO ,MMT_STORE_ITEM ,WIp_def_cat_acc_classes_v w ,mtl_item_categories ic ,mtl_categories_b c,mtl_system_items_b i Where MMT_STORE_INFO.REQ_NUM = MMT_STORE_ITEM.REQ_NUM And w.organization_id(+) = 84 And C.STRUCTURE_ID = '101' And C.ENABLED_FLAG = 'Y' And i.organization_id = 84 And i.inventory_item_id = ic.inventory_item_id And ic.organization_id = 84  AND c.category_id = ic.category_id  And w.category_id(+) = ic.category_id And MMT_STORE_ITEM.PART_NUM = i.segment1 And MMT_STORE_INFO.REQ_NUM in ('" + txtRequestNum.Text + "') ORDER BY MMT_STORE_ITEM.REQ_NUM";
            dt = db.GetDataOra();
            if (dt.Rows.Count != 0)
            {
                if (ViewState["DataT"] != null)
                {
                    dt = (DataTable)ViewState["DataT"];
                    if (dt.Select("PN='" + txtRequestNum.Text + "'").Length == 0)
                    {
                        DataRow rd = dt.NewRow();
                        rd["Item"] = (dt.Rows.Count + 1).ToString();
                        rd["PN"] = txtRequestNum.Text;
                        rd["Type"] = ddlType.SelectedItem.ToString();

                        dt.Rows.Add(rd);

                        dt.DefaultView.Sort = "Item";

                        ViewState["DataT"] = dt;

                        dgvMachine.DataSource = (DataTable)ViewState["DataT"];
                        dgvMachine.DataBind();

                        dtTmp = dt;

                        Div4.Visible = true;
                        btnSubmit.Enabled = true;

                        txtRequestNum.Text = "";
                        txtRequestNum.Focus();
                    }
                    else
                    {
                        txtRequestNum.Text = "";
                        txtRequestNum.Focus();

                        string script = "alert(\"" + "พบข้อมูลในระบบแล้ว" + "\");";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                    }
                }
            }
            else
            {
                string script = "alert(\"" + "ไม่พบข้อมูลนี้ในระบบ" + "\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                Div4.Visible = false;
                btnSubmit.Enabled = false;
            }
        }
        else
        {
            string script = "alert(\"" + "พบข้อมูลนี้ในระบบแล้ว" + "\");";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }
    }

    public string getSHELF(string ITEM_NUM)
    {
        string shelf = "";

        DataTable DT = new DataTable();
        db.TSql = "SELECT MMT_STORE_RECEIVING_SHELF('" + ITEM_NUM + "') AS MMCTSHELF FROM DUAL ";
        DT = db.GetDataOra();

        if (DT.Rows.Count != 0)
        {
            shelf = DT.Rows[0]["MMCTSHELF"].ToString();
        }
        else
        {
            shelf = "";
        }

        return shelf;
    }

    public string getFIFO(string ITEM_NUM)
    {
        string fifo = "";

        DataTable DT = new DataTable();

        db.TSql = "";
        db.TSql = db.TSql + "SELECT CASE mitem.shelf_life_code ";
        db.TSql = db.TSql + "    WHEN 1 THEN SUBSTR( MIN(MMT_MEKTEC_WORK_WEEK(NVL(mlabel.RECEIVED_DATE ,moq.date_received))) ,5 ,2) ";
        db.TSql = db.TSql + "    ELSE TO_CHAR(MIN(mln.expiration_date) ,'DD-MON-RRRR') ";
        db.TSql = db.TSql + "    END FIFO ";

        db.TSql = db.TSql + "FROM mtl_lot_numbers mln                , ";
        db.TSql = db.TSql + "    mtl_onhand_quantities_detail moq   , ";
        db.TSql = db.TSql + "    mtl_secondary_inventories mse      , ";
        db.TSql = db.TSql + "    MTL_SECONDARY_INVENTORIES_FK_V sub , ";
        db.TSql = db.TSql + "    mtl_system_items mitem             , ";
        db.TSql = db.TSql + "    mmt_receiving_labels mlabel ";
        db.TSql = db.TSql + "WHERE mln.organization_id   = 84 ";
        db.TSql = db.TSql + "    AND mln.inventory_item_id = " + ITEM_NUM;
        db.TSql = db.TSql + "    AND mln.inventory_item_id = moq.inventory_item_id ";
        db.TSql = db.TSql + "    AND mln.organization_id   = moq.organization_id ";
        db.TSql = db.TSql + "    AND mln.lot_number        = moq.lot_number ";
        db.TSql = db.TSql + "    AND mln.ORGANIZATION_ID   = mlabel.ORGANIZATION_ID(+) ";
        db.TSql = db.TSql + "    AND mln.lot_number        = mlabel.MMT_RECEIVING_CODE(+) ";
        db.TSql = db.TSql + "    AND moq.subinventory_code =mse.secondary_inventory_name ";
        db.TSql = db.TSql + "    AND moq.SUBINVENTORY_CODE = sub.SECONDARY_INVENTORY_NAME ";
        db.TSql = db.TSql + "    AND moq.ORGANIZATION_ID   = sub.ORGANIZATION_ID ";
        db.TSql = db.TSql + "    AND moq.ORGANIZATION_ID   = mitem.ORGANIZATION_ID ";
        db.TSql = db.TSql + "    AND moq.INVENTORY_ITEM_ID = mitem.INVENTORY_ITEM_ID ";
        db.TSql = db.TSql + "    AND moq.organization_id   =mse.organization_id ";
        db.TSql = db.TSql + "    AND sub.ATTRIBUTE5        IN ( 'RM' ,'OS') ";
        db.TSql = db.TSql + "    AND sub.ATTRIBUTE2        ='Y' ";
        db.TSql = db.TSql + "    AND mse.attribute2        ='Y' ";
        db.TSql = db.TSql + "    AND SUB.SECONDARY_INVENTORY_NAME LIKE 'ST%' ";
        db.TSql = db.TSql + "GROUP BY mitem.shelf_life_code";

        DT = db.GetDataOra();

        if (DT.Rows.Count != 0)
        {
            fifo = DT.Rows[0]["FIFO"].ToString();
        }
        else
        {
            fifo = "";
        }

        return fifo;
    }

    protected void dgvMachine_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DL")
        {
            dt = new DataTable();
            dt = (DataTable)ViewState["DataT"];

            foreach (DataRow dr in dt.Rows)
            {

                Int32 test1 = Convert.ToInt32(dr["Item"].ToString());
                Int32 test2 = Convert.ToInt32(e.CommandArgument.ToString()) +1;
               // if (dr["Item"].ToString() == e.CommandArgument.ToString()+1)


                if(test1 == test2)
                {
                  


                    for (int i = 0; i < dgvLFRequisitionList.Rows.Count; i++)
                    {
                        string req_num;

                        req_num = dgvLFRequisitionList.Rows[i].Cells[1].Text;


                        if (req_num == dr.ItemArray[1].ToString())
                        {
                            dgvLFRequisitionList.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFF7E7");
                        }

                    }


                    dr.Delete();
                    dt.AcceptChanges();

                    break;
                }
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Item"] = i + 1;
            }

            ViewState["DataT"] = dt;
            dgvMachine.DataSource = (DataTable)ViewState["DataT"];
            dgvMachine.DataBind();

            btnSubmit.Enabled = (dt.Rows.Count > 0 ? true : false);
        }
    }

    protected void dgvLFRequisitionList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName=="SL")
        {
            //txtRequestNum.Text = e.CommandArgument.ToString();
            //txtRequestNum.Text = dgvLFRequisitionList.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[1].Text ;
            string  strReq_num = dgvLFRequisitionList.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[1].Text;
            dgvLFRequisitionList.Rows[Convert.ToInt32(e.CommandArgument.ToString())].BackColor = System.Drawing.Color.Red;
            GetDatatoItemList(strReq_num);

        }
    }
}
