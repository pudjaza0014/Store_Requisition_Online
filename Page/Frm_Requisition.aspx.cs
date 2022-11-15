using StoreRequisition.Class;
using StoreRequisition.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frm_Priority_Report : System.Web.UI.Page

{
    ClassDB db = new ClassDB();
    clsAuthenticate aAuthent = new clsAuthenticate();
    clsSQLscript ObjRun = new clsSQLscript();
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
            db.TSql = "SELECT gen_head,gen_no FROM mmt_store_genno WHERE gen_head = 'REQ_NUM'";
            dt = db.GetDataOra();
            if (dt.Rows.Count > 0)
            {
                REQ_STATUS = "NEW";
                REQ_NUM = (Convert.ToUInt32(dt.Rows[0]["gen_no"].ToString()) + 1).ToString();
                txtRequestNum.Text = REQ_NUM;

                db.TSql = "UPDATE mmt_store_genno SET gen_no = " + REQ_NUM + ",update_date = sysdate WHERE gen_head = 'REQ_NUM'";
                db.InsertDataOra();
            }
            getApprover();
            txtRequestDate.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        }
    }

    protected void txtPartNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ITEM_NUM = txtPartNo.Text.Trim();
            dt = new DataTable();
            db.TSql = "SELECT * FROM Mtl_system_items MSI, MTL_ITEM_CATEGORIES a,MTL_CATEGORIES b WHERE msi.Organization_id = 84 AND a.organization_id = 84 AND msi.Inventory_item_status_code = 'Active' AND msi.Inventory_item_ID = a.Inventory_ITEM_ID AND a.category_ID = b.category_ID AND msi.segment1 = '" + ITEM_NUM + "'";
            dt = db.GetDataOra();
            if (dt.Rows.Count > 0)
            {
                txtPartName.Text = dt.Rows[0]["DESCRIPTION"].ToString();
                txtUom.Text = dt.Rows[0]["PRIMARY_UNIT_OF_MEASURE"].ToString();
                INVENTORY_ITEM_ID = Convert.ToInt32(dt.Rows[0]["INVENTORY_ITEM_ID"]);
                LOCATION = ddlLocation.SelectedItem.Value.ToString();
                db.TSql = "SELECT MMT_STORE_RECEIVING_SHELF('" + ITEM_NUM + "') AS MMCTSHELF FROM DUAL";
                dt = db.GetDataOra();
                if (dt.Rows.Count > 0)
                {
                    txtShelf.Text = dt.Rows[0]["MMCTSHELF"].ToString();
                }

                //db.TSql = "SELECT SUM(nvl(GOOD_QTY,moq.transaction_quantity)) ONHAND,CASE mitem.shelf_life_code WHEN 1 THEN MIN(MMT_MEKTEC_WORK_WEEK(NVL(mlabel.RECEIVED_DATE, moq.date_received))) ELSE TO_CHAR(MIN(mln.expiration_date) , 'RRRRMMDD') END FIFO ,CASE mitem.shelf_life_code WHEN 1 THEN SUBSTR(MIN(MMT_MEKTEC_WORK_WEEK(NVL(mlabel.RECEIVED_DATE, moq.date_received))) , 5, 2) ELSE TO_CHAR(MIN(mln.expiration_date) , 'DD-MON-RRRR') END DISPLAY_FIFO FROM mtl_lot_numbers mln, mtl_onhand_quantities_detail moq, mtl_secondary_inventories mse, MTL_SECONDARY_INVENTORIES_FK_V sub, mtl_system_items mitem, mmt_receiving_labels mlabel WHERE mln.organization_id   = 84 AND mln.inventory_item_id = '" + INVENTORY_ITEM_ID + "' AND mln.inventory_item_id = moq.inventory_item_id AND mln.organization_id   = moq.organization_id AND mln.lot_number        = moq.lot_number AND mln.ORGANIZATION_ID   = mlabel.ORGANIZATION_ID(+) AND mln.lot_number        = mlabel.MMT_RECEIVING_CODE(+) AND moq.subinventory_code = mse.secondary_inventory_name AND moq.SUBINVENTORY_CODE = sub.SECONDARY_INVENTORY_NAME AND moq.ORGANIZATION_ID   = sub.ORGANIZATION_ID AND moq.ORGANIZATION_ID   = mitem.ORGANIZATION_ID AND moq.INVENTORY_ITEM_ID = mitem.INVENTORY_ITEM_ID AND moq.organization_id   = mse.organization_id AND sub.ATTRIBUTE5 IN ('RM','OS','OTHER','SP') AND (sub.ATTRIBUTE2 = 'Y' OR SUB.ATTRIBUTE5 = 'SP') AND (mse.attribute2 = 'Y'OR MSE.ATTRIBUTE5 = 'SP') AND( CASE  WHEN '" + LOCATION + "' = 'MMCT' AND SUB.SECONDARY_INVENTORY_NAME NOT LIKE 'ST-MPCT%' AND SUB.SECONDARY_INVENTORY_NAME  LIKE 'ST%' THEN 'MMCT' WHEN '" + LOCATION + "' = 'MPCT' AND SUB.SECONDARY_INVENTORY_NAME  LIKE 'ST-MPCT%' THEN 'MPCT' ELSE 'OOO' END) = '" + LOCATION + "'  GROUP BY mitem.shelf_life_code,MMT_MEKTEC_WORK_WEEK(NVL(mlabel.RECEIVED_DATE, moq.date_received)) ORDER BY 2 asc";
                db.TSql = "SELECT SUM(nvl(GOOD_QTY,moq.transaction_quantity)) ONHAND,CASE mitem.shelf_life_code WHEN 1 THEN MIN(MMT_MEKTEC_WORK_WEEK(NVL(mlabel.RECEIVED_DATE, moq.date_received))) ELSE TO_CHAR(MIN(mln.expiration_date) , 'RRRRMMDD') END FIFO ,CASE mitem.shelf_life_code WHEN 1 THEN SUBSTR(MIN(MMT_MEKTEC_WORK_WEEK(NVL(mlabel.RECEIVED_DATE, moq.date_received))) , 5, 2) ELSE TO_CHAR(MIN(mln.expiration_date) , 'DD-MON-RRRR') END DISPLAY_FIFO FROM mtl_lot_numbers mln, mtl_onhand_quantities_detail moq, mtl_secondary_inventories mse, MTL_SECONDARY_INVENTORIES_FK_V sub, mtl_system_items mitem, mmt_receiving_labels mlabel WHERE mln.organization_id   = 84 AND mln.inventory_item_id = '" + INVENTORY_ITEM_ID + "' AND mln.inventory_item_id = moq.inventory_item_id AND mln.organization_id   = moq.organization_id AND mln.lot_number        = moq.lot_number AND mln.ORGANIZATION_ID   = mlabel.ORGANIZATION_ID(+) AND mln.lot_number        = mlabel.MMT_RECEIVING_CODE(+) AND moq.subinventory_code = mse.secondary_inventory_name AND moq.SUBINVENTORY_CODE = sub.SECONDARY_INVENTORY_NAME AND moq.ORGANIZATION_ID   = sub.ORGANIZATION_ID AND moq.ORGANIZATION_ID   = mitem.ORGANIZATION_ID AND moq.INVENTORY_ITEM_ID = mitem.INVENTORY_ITEM_ID AND moq.organization_id   = mse.organization_id AND sub.ATTRIBUTE5 IN ('RM','OS','OTHER','SP') AND (sub.ATTRIBUTE2 = 'Y' OR SUB.ATTRIBUTE5 = 'SP') AND (mse.attribute2 = 'Y'OR MSE.ATTRIBUTE5 = 'SP') AND( CASE  WHEN '" + LOCATION + "' = 'MMCT' AND SUB.SECONDARY_INVENTORY_NAME NOT LIKE 'ST-MPCT%' AND SUB.SECONDARY_INVENTORY_NAME  LIKE 'ST%' AND INSTR(SUB.SECONDARY_INVENTORY_NAME, 'TEMP') = 0 THEN 'MMCT' WHEN '" + LOCATION + "' = 'LF' AND SUB.SECONDARY_INVENTORY_NAME NOT LIKE 'ST-MPCT%' AND SUB.SECONDARY_INVENTORY_NAME  LIKE 'ST%' AND INSTR(SUB.SECONDARY_INVENTORY_NAME, 'TEMP') = 0 THEN 'LF' WHEN '" + LOCATION + "' = 'MPCT' AND SUB.SECONDARY_INVENTORY_NAME  LIKE 'ST-MPCT%' THEN 'MPCT' ELSE 'OOO' END) = '" + LOCATION + "'  GROUP BY mitem.shelf_life_code,MMT_MEKTEC_WORK_WEEK(NVL(mlabel.RECEIVED_DATE, moq.date_received)) ORDER BY 2 asc";
                dt = db.GetDataOra();
                if (dt.Rows.Count > 0)
                {
                    txtExpire.Text = "";
                    ViewState["Tmp_DT"] = dt;
                    TmpC_Onhand = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        txtExpire.Text = txtExpire.Text + dt.Rows[i]["DISPLAY_FIFO"].ToString() + ",";
                        txtOnHand.Text = txtOnHand.Text + dt.Rows[i]["ONHAND"].ToString() + ",";
                        TmpC_Onhand = TmpC_Onhand + Convert.ToDouble(dt.Rows[i]["ONHAND"].ToString());
                    }

                    txtExpire.Text = txtExpire.Text.Remove(txtExpire.Text.Length - 1);
                    txtOnHand.Text = txtOnHand.Text.Remove(txtOnHand.Text.Length - 1);

                    txtOnHand.Text = Convert.ToDouble(TmpC_Onhand).ToString("#,##0.00");
                }
                else
                {
                    ViewState["Tmp_DT"] = null;
                    txtExpire.Text = "Short";
                    txtQty0.Text = "Short";
                    txtOnHand.Text = "0";
                }

                txtQty.Enabled = true;
                txtQty.Focus();
            }
        }
        catch (Exception)
        {
            txtQty.Enabled = false;
        }
    }

    protected void btnPartNo_Click(object sender, EventArgs e)
    {
        ITEM_NUM = txtPartNo.Text.Trim();
        dt = new DataTable();
        db.TSql = "SELECT * FROM Mtl_system_items MSI, MTL_ITEM_CATEGORIES a,MTL_CATEGORIES b WHERE msi.Organization_id = 84 AND a.organization_id = 84 AND msi.Inventory_item_status_code = 'Active' AND msi.Inventory_item_ID = a.Inventory_ITEM_ID AND a.category_ID = b.category_ID AND msi.segment1 = '" + ITEM_NUM + "'";
        dt = db.GetDataOra();
        if (dt.Rows.Count > 0)
        {
            txtPartName.Text = dt.Rows[0]["DESCRIPTION"].ToString();
            txtUom.Text = dt.Rows[0]["PRIMARY_UNIT_OF_MEASURE"].ToString();
            INVENTORY_ITEM_ID = Convert.ToInt32(dt.Rows[0]["INVENTORY_ITEM_ID"]);
            LOCATION = ddlLocation.SelectedItem.Value;
            db.TSql = "SELECT MMT_STORE_RECEIVING_SHELF('" + ITEM_NUM + "') AS MMCTSHELF FROM DUAL";
            dt = db.GetDataOra();
            if (dt.Rows.Count > 0)
            {
                txtShelf.Text = dt.Rows[0]["MMCTSHELF"].ToString();
            }

            //db.TSql = "SELECT SUM(GOOD_QTY) ONHAND,CASE mitem.shelf_life_code WHEN 1 THEN SUBSTR(MIN(MMT_MEKTEC_WORK_WEEK(NVL(mlabel.RECEIVED_DATE, moq.date_received))) , 5, 2) ELSE TO_CHAR(MIN(mln.expiration_date) , 'DD-MON-RRRR') END FIFO FROM mtl_lot_numbers mln, mtl_onhand_quantities_detail moq, mtl_secondary_inventories mse, MTL_SECONDARY_INVENTORIES_FK_V sub, mtl_system_items mitem, mmt_receiving_labels mlabel WHERE mln.organization_id   = 84 AND mln.inventory_item_id = " + INVENTORY_ITEM_ID + " AND mln.inventory_item_id = moq.inventory_item_id AND mln.organization_id   = moq.organization_id AND mln.lot_number        = moq.lot_number AND mln.ORGANIZATION_ID   = mlabel.ORGANIZATION_ID(+) AND mln.lot_number        = mlabel.MMT_RECEIVING_CODE(+) AND moq.subinventory_code = mse.secondary_inventory_name AND moq.SUBINVENTORY_CODE = sub.SECONDARY_INVENTORY_NAME AND moq.ORGANIZATION_ID   = sub.ORGANIZATION_ID AND moq.ORGANIZATION_ID   = mitem.ORGANIZATION_ID AND moq.INVENTORY_ITEM_ID = mitem.INVENTORY_ITEM_ID AND moq.organization_id   = mse.organization_id AND sub.ATTRIBUTE5 IN ('RM','OS') AND sub.ATTRIBUTE2 = 'Y' AND mse.attribute2 = 'Y' AND( CASE  WHEN '" + LOCATION + "' = 'MMCT' AND SUB.SECONDARY_INVENTORY_NAME NOT LIKE 'ST-MPCT%' AND SUB.SECONDARY_INVENTORY_NAME  LIKE 'ST%' THEN 'MMCT' WHEN '" + LOCATION + "' = 'MPCT' AND SUB.SECONDARY_INVENTORY_NAME  LIKE 'ST-MPCT%' THEN 'MPCT' ELSE 'OOO' END) = '" + LOCATION + "' GROUP BY mitem.shelf_life_code,MMT_MEKTEC_WORK_WEEK(NVL(mlabel.RECEIVED_DATE, moq.date_received)) ORDER BY MMT_MEKTEC_WORK_WEEK(NVL(mlabel.RECEIVED_DATE, moq.date_received))";
            db.TSql = "SELECT SUM(GOOD_QTY) ONHAND,CASE mitem.shelf_life_code WHEN 1 THEN SUBSTR(MIN(MMT_MEKTEC_WORK_WEEK(NVL(mlabel.RECEIVED_DATE, moq.date_received))) , 5, 2) ELSE TO_CHAR(MIN(mln.expiration_date) , 'DD-MON-RRRR') END FIFO FROM mtl_lot_numbers mln, mtl_onhand_quantities_detail moq, mtl_secondary_inventories mse, MTL_SECONDARY_INVENTORIES_FK_V sub, mtl_system_items mitem, mmt_receiving_labels mlabel WHERE mln.organization_id   = 84 AND mln.inventory_item_id = " + INVENTORY_ITEM_ID + " AND mln.inventory_item_id = moq.inventory_item_id AND mln.organization_id   = moq.organization_id AND mln.lot_number        = moq.lot_number AND mln.ORGANIZATION_ID   = mlabel.ORGANIZATION_ID(+) AND mln.lot_number        = mlabel.MMT_RECEIVING_CODE(+) AND moq.subinventory_code = mse.secondary_inventory_name AND moq.SUBINVENTORY_CODE = sub.SECONDARY_INVENTORY_NAME AND moq.ORGANIZATION_ID   = sub.ORGANIZATION_ID AND moq.ORGANIZATION_ID   = mitem.ORGANIZATION_ID AND moq.INVENTORY_ITEM_ID = mitem.INVENTORY_ITEM_ID AND moq.organization_id   = mse.organization_id AND sub.ATTRIBUTE5 IN ('RM','OS') AND sub.ATTRIBUTE2 = 'Y' AND mse.attribute2 = 'Y' AND( CASE  WHEN '" + LOCATION + "' = 'MMCT' AND SUB.SECONDARY_INVENTORY_NAME NOT LIKE 'ST-MPCT%' AND SUB.SECONDARY_INVENTORY_NAME  LIKE 'ST%' AND INSTR(SUB.SECONDARY_INVENTORY_NAME,'TEMP') = 0 THEN 'MMCT' WHEN '" + LOCATION + "' = 'MPCT' AND SUB.SECONDARY_INVENTORY_NAME  LIKE 'ST-MPCT%' THEN 'MPCT' ELSE 'OOO' END) = '" + LOCATION + "' GROUP BY mitem.shelf_life_code,MMT_MEKTEC_WORK_WEEK(NVL(mlabel.RECEIVED_DATE, moq.date_received)) ORDER BY MMT_MEKTEC_WORK_WEEK(NVL(mlabel.RECEIVED_DATE, moq.date_received))";
            dt = db.GetDataOra();
            if (dt.Rows.Count > 0)
            {
                txtExpire.Text = "";
                ViewState["Tmp_DT"] = dt;
                TmpC_Onhand = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    txtExpire.Text = txtExpire.Text + dt.Rows[i]["FIFO"].ToString() + ",";
                    txtOnHand.Text = txtOnHand.Text + dt.Rows[i]["ONHAND"].ToString() + ",";
                    TmpC_Onhand = TmpC_Onhand + Convert.ToDouble(dt.Rows[i]["ONHAND"].ToString());
                }

                txtExpire.Text = txtExpire.Text.Remove(txtExpire.Text.Length - 1);
                txtOnHand.Text = txtOnHand.Text.Remove(txtOnHand.Text.Length - 1);

                txtOnHand.Text = Convert.ToDouble(TmpC_Onhand).ToString("#,##0.00");
            }
            else
            {
                ViewState["Tmp_DT"] = null;
                txtExpire.Text = "Short";
                txtQty0.Text = "Short";
                txtOnHand.Text = "0";
            }

            txtQty.Enabled = true;
        }
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedItem.Value != "Please Select ...")
        {
            txtDeliveryStation.Enabled = true;
            txtDeliveryStation.Focus();
        }
        else
        {
            txtDeliveryStation.Enabled = false;
        }
    }

    protected void dtgHeader_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {

            dt = new DataTable();
            dt = (DataTable)ViewState["DataT"];

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Item"].ToString() == e.Item.Cells[0].Text)
                {
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
            dtgHeader.DataSource = (DataTable)ViewState["DataT"];
            dtgHeader.DataBind();
        }
    }
    protected void getApprover()
    {
        dt = new DataTable();
        //  db.TSql = "SELECT * FROM MMT_HR_EMPLOYEE_V WHERE NVL(DATE_END,SYSDATE) >= SYSDATE AND POSITION LIKE '%Supervisor%' or  emp_no = '1A019338' or  emp_no = '1A005808' order by EMP_NAME_ENG ASC ";
        db.TSql = "SELECT * FROM MMT_HR_EMPLOYEE_V WHERE POSITION LIKE '%Manag%' or POSITION like '%Supervi%' or  emp_no = '1A019338' or  emp_no = '1A005808' order by EMP_NAME_ENG ASC ";
        dt = db.GetDataOra();
        if (dt.Rows.Count > 0)
        {
            ddlApprovedby.DataSource = dt;
            ddlApprovedby.DataValueField = "EMP_NO";
            ddlApprovedby.DataTextField = "EMP_NAME_ENG";
            ddlApprovedby.DataBind();
            ddlApprovedby.Items.Insert(0, "Please Select Data...");
        }
        lbIssue.Text = "Sub Inventory";
        ddlApprovedby.Enabled = true;
        ddlApprovedby.Focus();
    }

  





    protected void dtgHeader_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        btnAdd.Text = "Edit";
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        btnAdd.Text = "Cancel";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ViewState["DataT"] != null)
        {
            dt = (DataTable)ViewState["DataT"];
            DataRow rd = dt.NewRow();
            rd["Item"] = (dt.Rows.Count + 1).ToString();

            if (txtPartName.Text.Length > 50)
            {
                rd["PN"] = txtPartNo.Text + ":" + txtPartName.Text.Remove(50);
            }
            else
            {
                rd["PN"] = txtPartNo.Text + ":" + txtPartName.Text;
            }

            rd["Shelf"] = txtShelf.Text;
            rd["EXPIRE"] = txtQty0.Text;
            rd["UOM"] = txtUom.Text;
            rd["QTY"] = Convert.ToDouble(txtQty.Text).ToString("#,#00.00");
            rd["Actual"] = "";
            rd["Pending"] = "";
            rd["PNS"] = txtPartNo.Text;
            dt.Rows.Add(rd);

            dt.DefaultView.Sort = "Item";

            ViewState["DataT"] = dt;
            dtgHeader.DataSource = (DataTable)ViewState["DataT"];
            dtgHeader.DataBind();

            dtTmp = dt;

            txtPartNo.Text = "";
            txtPartName.Text = "";
            txtShelf.Text = "";
            txtExpire.Text = "";
            txtUom.Text = "";
            txtOnHand.Text = "";
            txtQty.Text = "";
            txtExpire0.Text = "";
            txtOnHand0.Text = "";
            txtQty0.Text = "";
            txtPartNo.Focus();

            btnAdd.Enabled = false;
            btnSubmit.Enabled = true;
        }
    }
    private DataTable TempTable()
    {
        dtTempDetial = new DataTable();
        dtTempDetial.Columns.Add("Item", typeof(string));
        dtTempDetial.Columns.Add("PN", typeof(string));
        dtTempDetial.Columns.Add("Shelf", typeof(string));
        dtTempDetial.Columns.Add("EXPIRE", typeof(string));
        dtTempDetial.Columns.Add("UOM", typeof(string));
        dtTempDetial.Columns.Add("QTY", typeof(string));
        dtTempDetial.Columns.Add("Actual", typeof(string));
        dtTempDetial.Columns.Add("Pending", typeof(string));
        dtTempDetial.Columns.Add("PNS", typeof(string));
        return dtTempDetial;
    }

    

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        DataTable Tmp_Dt = (DataTable)ViewState["Tmp_DT"];

        if (txtQty0.Text != "Short")
        {
            try
            {
                if (Convert.ToDouble(txtQty.Text) > 0)
                {
                    TmpC_Onhand = 0;
                    txtQty0.Text = "";
                    for (int i = 0; i < Tmp_Dt.Rows.Count; i++)
                    {
                        if (TmpC_Onhand <= Convert.ToDouble(txtQty.Text))
                        {
                            TmpC_Onhand = TmpC_Onhand + Convert.ToDouble(Tmp_Dt.Rows[i]["ONHAND"].ToString());
                            txtQty0.Text = txtQty0.Text + Tmp_Dt.Rows[i]["FIFO"].ToString() + ":" + Convert.ToDouble(Tmp_Dt.Rows[i]["ONHAND"].ToString()).ToString("#,#00.00") + "/";
                        }
                        else
                        {
                            txtQty0.Text = txtQty0.Text.Remove(txtQty0.Text.Length - 1);
                            break;
                        }
                    }
                    btnAdd.Enabled = true;
                }
                else
                {
                    btnAdd.Enabled = false;
                }
            }
            catch (Exception)
            {
                btnAdd.Enabled = false;
            }
        }
        else
        {
            if (Convert.ToDouble(txtQty.Text) > 0)
            {
                btnAdd.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = false;
            }
        }
    }

    protected void txtDeliveryStation_TextChanged(object sender, EventArgs e)
    {
        if (txtDeliveryStation.Text.Length > 0)
        {
            txtRequestBy.Enabled = true;
            txtRequestBy.Focus();
        }
        else
        {
            txtRequestBy.Enabled = false;
        }
    }

    

    protected Boolean getApprover(string EN_Requester)
    {
        dt = new DataTable();

        //db.TSql = "SELECT * FROM MMT_HR_EMPLOYEE_V WHERE POSITION LIKE '%Manag%' or POSITION like '%Supervi%' or  emp_no = '1A019338' or  emp_no = '1A005808' order by EMP_NAME_ENG ASC ";
        db.TSql = "SELECT * FROM MMT_HR_EMPLOYEE_V WHERE EMP_NO ='" + EN_Requester + "'";
        dt = db.GetDataOra();
        if (dt.Rows.Count > 0)
        {
            var Sub_EN = dt.Rows[0]["EMP_SUP"].ToString();
            var GL_Code = dt.Rows[0]["GL_CODE"].ToString();

            db.TSql = "SELECT EMP_SUP,SUP_NAMEMPE FROM mmt_hr_employee_v WHERE  gl_code = '"+ GL_Code + "' and date_end is null group BY EMP_SUP , SUP_NAMEMPE order by EMP_SUP";

            dt = new DataTable();

            dt = db.GetDataOra();
            if (dt.Rows.Count > 0)
            {
                ddlApprovedby.DataSource = dt;
                ddlApprovedby.DataValueField = "EMP_SUP";
                ddlApprovedby.DataTextField = "SUP_NAMEMPE";
                ddlApprovedby.DataBind();
                ddlApprovedby.Items.Insert(0, "PLEASE CHOOSE SUPERVISOR...");


                ddlApprovedby.Items.Insert(0, new ListItem( "MR.theerapat", "1A019338"));

            }
            ddlApprovedby.SelectedValue = Sub_EN;


        }
        lbIssue.Text = "Sub Inventory";
        ddlApprovedby.Enabled = true;
        ddlApprovedby.Focus();

        return (dt.Rows.Count > 0 ? true : false);
    }

    protected void txtRequesTel_TextChanged(object sender, EventArgs e)
    {
        if (txtRequesTel.Text.Length > 0)
        {
            ddlIssueType.Enabled = true;
            ddlIssueType.Focus();
        }
        else
        {
            ddlIssueType.Enabled = false;
        }
    }

    protected void ddlIssueType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIssueType.SelectedItem.Value == "Material Transfer")
        {
            dt = new DataTable();
            db.TSql = "SELECT S.SECONDARY_INVENTORY_NAME FROM MTL_SECONDARY_INVENTORIES_FK_V S WHERE NVL(TO_CHAR(S.DISABLE_DATE,'YYYYMMDD'),TO_CHAR(SYSDATE,'YYYYMMDD')) >= TO_CHAR(SYSDATE,'YYYYMMDD') ORDER BY S.SECONDARY_INVENTORY_NAME";
            dt = db.GetDataOra();
            if (dt.Rows.Count > 0)
            {
                ddlIssue.DataSource = dt;
                ddlIssue.DataValueField = "SECONDARY_INVENTORY_NAME";
                ddlIssue.DataTextField = "SECONDARY_INVENTORY_NAME";
                ddlIssue.DataBind();
                ddlIssue.Items.Insert(0, "Please Select Data...");
            }
            lbIssue.Text = "Sub Inventory";
            ddlIssue.Enabled = true;
            ddlIssue.Focus();
        }
        else if (ddlIssueType.SelectedItem.Value == "OS Issue")
        {
            dt = new DataTable();
            db.TSql = "SELECT AA.FLEX_VALUE FROM FND_FLEX_VALUES AA,FND_FLEX_VALUE_SETS BB,FND_FLEX_VALUES_TL cc ,MTL_TRANSACTION_TYPES DD WHERE AA.FLEX_VALUE_SET_ID = BB.FLEX_VALUE_SET_ID AND  aa.FLEX_VALUE = dd.TRANSACTION_TYPE_NAME  AND AA.FLEX_VALUE_ID = cc.FLEX_VALUE_ID AND BB.FLEX_VALUE_SET_NAME = 'MMT_MISC_TRANS_TYPE_A' AND AA.PARENT_FLEX_VALUE_LOW = 'STORE-ISSUE' AND NVL(TO_CHAR(DD.DISABLE_DATE,'YYYYMMDD'),TO_CHAR(SYSDATE,'YYYYMMDD')) >= TO_CHAR(SYSDATE,'YYYYMMDD') AND AA.ENABLED_FLAG = 'Y' ORDER BY AA.FLEX_VALUE";
            dt = db.GetDataOra();
            if (dt.Rows.Count > 0)
            {
                ddlIssue.DataSource = dt;
                ddlIssue.DataValueField = "FLEX_VALUE";
                ddlIssue.DataTextField = "FLEX_VALUE";
                ddlIssue.DataBind();
                ddlIssue.Items.Insert(0, "Please Select Data...");
            }
            lbIssue.Text = "Transection Type";
            ddlIssue.Enabled = true;
            ddlIssue.Focus();
        }
        else
        {
            ddlIssue.Enabled = false;
        }
    }

    protected void ddlIssue_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIssue.SelectedItem.Value != "Please Select ...")
        {
            ddlReason.Text = "Please Select ...";
            txtMachineName.Text = "";
            txtProcessName.Text = "";
            txtArea.Text = "";
            txtRemark.Text = "";

            ddlReason.Enabled = false;
            txtMachineName.Enabled = false;
            txtProcessName.Enabled = false;
            txtArea.Enabled = false;

            if (ddlIssue.SelectedItem.Value.Contains("SP ISSUE TO"))
            {
                ddlReason.Enabled = true;
                ddlReason.Focus();

                txtRemark.Enabled = false;
                txtPartNo.Enabled = false;
            }
            else
            {
                txtRemark.Enabled = true;
                txtPartNo.Enabled = true;
                txtPartNo.Focus();
            }
        }
        else
        {
            txtRemark.Enabled = false;
            txtPartNo.Enabled = false;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {

    }

    protected void ddlReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReason.SelectedItem.Value != "Please Select ...")
        {
            txtMachineName.Enabled = true;
            txtMachineName.Focus();
        }
        else
        {
            txtMachineName.Enabled = true;
        }
    }

    protected void txtMachineName_TextChanged(object sender, EventArgs e)
    {
        if (txtMachineName.Text.Length > 0)
        {
            txtProcessName.Enabled = true;
            txtProcessName.Focus();
        }
        else
        {
            txtProcessName.Enabled = false;
        }
    }

    protected void txtProcessName_TextChanged(object sender, EventArgs e)
    {
        if (txtProcessName.Text.Length > 0)
        {
            txtArea.Enabled = true;
            txtArea.Focus();
        }
        else
        {
            txtArea.Enabled = false;
        }
    }

    protected void txtArea_TextChanged(object sender, EventArgs e)
    {
        if (txtDeliveryStation.Text.Length > 0)
        {
            txtRemark.Enabled = true;
            txtPartNo.Enabled = true;
            txtPartNo.Focus();
        }
        else
        {
            txtRemark.Enabled = false;
            txtPartNo.Enabled = false;
        }
    }

    protected void txtRequestBy_TextChanged(object sender, EventArgs e)
    {
        if (txtRequestBy.Text.Length > 0)
        {
            if (txtRequestBy.Text.Length >= 8)
            {
                int errorCounter = Regex.Matches(txtRequestBy.Text, @"[a-zA-Z]").Count;
                if (errorCounter == 1)
                {
                    if (getApprover(txtRequestBy.Text.Trim()))
                    {
                        txtRequesTel.Enabled = true;
                        txtRequesTel.Focus();
                        Label45.Text = "Ex : 1A016950";
                    }
                    else
                    {
                        Label45.Text = "Ex : 1A016950" + " *ไม่พบข้อมูล Supervisor ของคุณ";
                    };
                }
                else
                {
                    Label45.Text = "Ex : 1A016950" + " *กรุุณากรอก EN ให้ถูกต้องเพื่อทำการQuery Supervisor List ให้Approve ในขั้นตอนถัดไป";
                }
            }
            else
            {
                Label45.Text = "Ex : 1A016950" + " *กรุุณากรอก EN ให้ถูกต้องเพื่อทำการQuery Supervisor List ให้Approve ในขั้นตอนถัดไป";
                txtRequesTel.Enabled = false;
                txtRequestBy.Focus();
            }





            //txtRequesTel.Enabled = true;
            //txtRequesTel.Focus();
        }
        else
        {
            txtRequesTel.Enabled = false;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            btnSubmit.Enabled = false;
            if (TmpC_FIFO == 0)
            {
                TmpC_FIFO += 1;


                if (ddlApprovedby.SelectedValue.ToString() == "")
                {
                    lblMSGs.Text = "Approve by is not choose. Please choose your supervisor for approve a requisition <br> ผู้Approve ไม่ถูกเลือก กรุณาเลือกSupervisor ของคุณเพื่อทำการ Approve ในขั้นตอนถัดไป";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Approved warning", "alert('Approve by is not choose. Please choose your supervisor for approve a requisition \r\nผู้Approve ไม่ถูกเลือก กรุณาเลือกSupervisor ของคุณเพื่อทำการ Approve ในขั้นตอนถัดไป')", true);

                    return;
                }


                string EnApproved = ddlApprovedby.SelectedValue.ToString();
                DirectoryEntry entry1 = new DirectoryEntry("LDAP://mmct_domain");
                object obj1 = entry1.NativeObject;
                DirectorySearcher search1 = new DirectorySearcher(entry1);
                search1.Filter = "(employeeid=" + EnApproved + ")";
                search1.Sort.Direction = System.DirectoryServices.SortDirection.Descending;
                search1.Sort.PropertyName = "whenCreated";
                search1.PropertiesToLoad.Add("EmployeeId");
                search1.PropertiesToLoad.Add("cn");
                search1.PropertiesToLoad.Add("userAccountControl");
                search1.PropertiesToLoad.Add("whenCreated");

                SearchResult result1 = search1.FindOne();
 
                if (result1 == null)
                {
                    lblMSGs.Text = "Approved by is not have Windows Login <br> ผู้Approved ที่คุณเลือกไม่มี Username Windows Login ไม่สามารถ Approve ให้ได้";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Approved warning", "alert('Approved by is not have Windows Login \r\nคนApproved ที่คุณเลือกไม่มี Username Windows Login ไม่สามารถ Approve ให้ได้')", true);
                     
                    return;
                }
             

                var strDepartments = result1.Properties["employeeid"];
                var strDepartments2 = result1.Properties["cn"][0];
                string ApproveName = "";
                dt = new DataTable();
                UserAD user = (UserAD)Session["UserAD"];
                dt = ObjRun.GetUsersData((string)result1.Properties["employeeid"][0]);
                if (dt.Rows.Count > 0)
                {
                    ApproveName = dt.Rows[0]["emp_name_eng"].ToString();
                }

                if (ddlIssueType.SelectedItem.Value == "Material Transfer")
                {
                    db.TSql = "INSERT INTO MMT.MMT_STORE_INFO (REQ_NUM, REQ_BY, REQ_DATE, SUB_INV, REQ_STATUS, REQ_LOCATION, DELIVERY_STATION,ISSUE_TYPE ,REQ_APPROVE_BY_NAME ,REQ_BY_TEL ,REQ_REMARK , REQ_BY_ID) VALUES ( '" + txtRequestNum.Text.Trim() + "','" + txtRequestBy.Text.Trim() + "',(SELECT TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY HH24:MI:SS'),'DD/MM/YYYY HH24:MI:SS') FROM DUAL),'" + ddlIssue.SelectedItem.Value + "','DRAFT','" + ddlLocation.SelectedItem.Value + "','" + txtDeliveryStation.Text.Trim() + "','" + ddlIssueType.SelectedItem.Value + "','" + ApproveName + "','" + txtRequesTel.Text + "','" + txtRemark.Text + "','" + user.InitName + "' )";
                }
                else if (ddlIssueType.SelectedItem.Value == "OS Issue")
                {
                    db.TSql = "INSERT INTO MMT.MMT_STORE_INFO (REQ_NUM, REQ_BY, REQ_DATE, REQ_STATUS, TRANS_TYPE, REQ_LOCATION, DELIVERY_STATION,ISSUE_TYPE,REASON,MACHINENAME,PROCESSNAME,AREA ,REQ_APPROVE_BY_NAME ,REQ_BY_TEL ,REQ_REMARK , REQ_BY_ID) VALUES ( '" + txtRequestNum.Text.Trim() + "','" + txtRequestBy.Text.Trim() + "',(SELECT TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY HH24:MI:SS'),'DD/MM/YYYY HH24:MI:SS') FROM DUAL),'DRAFT','" + ddlIssue.SelectedItem.Value + "','" + ddlLocation.SelectedItem.Value + "','" + txtDeliveryStation.Text.Trim() + "','" + ddlIssueType.SelectedItem.Value + "','" + ddlReason.SelectedItem.Value + "','" + txtMachineName.Text + "','" + txtProcessName.Text + "','" + txtArea.Text + "','" + ApproveName + "','" + txtRequesTel.Text + "','" + txtRemark.Text + "' ,'" + user.InitName + "' )";
                }

                if (db.InsertDataOra() == 1)
                {
                    dt = (DataTable)ViewState["DataT"];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        db.TSql = "INSERT INTO MMT.MMT_STORE_ITEM (REQ_NUM, ITEM_NUM, PART_NUM, UOM, QTY_REQ,SHELF,EXPIRE)VALUES ('" + txtRequestNum.Text + "','" + dt.Rows[i][0].ToString().Trim() + "','" + dt.Rows[i][8].ToString().Trim() + "','" + dt.Rows[i][4].ToString().Trim() + "','" + dt.Rows[i][5].ToString().Replace(",", "").Trim() + "','" + dt.Rows[i][2].ToString().Replace(",", "").Trim() + "','" + dt.Rows[i][3].ToString().Replace(",", "").Trim() + "')";

                        if (db.InsertDataOra() == 1)
                        {
                            db.TSql = "APPS.MMT_STORE_PROGRESS_MAKING";
                        };
                    }
                    db.Requisition_Progress_Making(Convert.ToInt32(txtRequestNum.Text), (string)strDepartments2, (UserAD)Session["UserAD"], "", "", "", "");

                    Session["RequisitionNumber"] = txtRequestNum.Text;
                    Session["Location"] = ddlLocation.SelectedItem.ToString();
                    Session["DeliveryStation"] = txtDeliveryStation.Text;
                    Session["RequestBy"] = txtRequestBy.Text;
                    Session["RequestTel"] = txtRequesTel.Text;
                    Session["RequestDate"] = txtRequestDate.Text;
                    Session["IssueType"] = ddlIssueType.SelectedItem.ToString();
                    Session["SubInventory"] = ddlIssue.SelectedItem.ToString();
                    Session["Remark"] = txtRemark.Text;
                    Session["ListItem"] = ViewState["DataT"];


                    Response.Redirect("~/Page/Frm_Report_R.aspx");
                    btnSubmit.Enabled = true;
                    TmpC_FIFO += 1;
                }
            }
        }
        catch (Exception ex)
        {
            // lblMSGs.Text = ex.Message.ToString();

            Response.Redirect("~/Page/Frm_Report_R.aspx");
            return;
            //  throw;
        }

    }
}
