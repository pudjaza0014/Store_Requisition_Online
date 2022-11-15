using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frm_Report_Number : System.Web.UI.Page

{
    ClassDB db = new ClassDB();
    clsAuthenticate aAuthent = new clsAuthenticate();

    DataTable dtTempDetial;
    DataTable dt;

    DataTable DT;

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
        dt = new DataTable();

        if ((txtRequestNumFrom.Text.Trim().Length != 0) && (txtRequestNumTo.Text.Trim().Length != 0))
        {
            db.TSql = "SELECT MMT_STORE_INFO.* , MMT_STORE_ITEM.* , c.segment2 ITEM_CAT , i.DESCRIPTION, i.inventory_item_id From MMT_STORE_INFO ,MMT_STORE_ITEM ,WIp_def_cat_acc_classes_v w ,mtl_item_categories ic ,mtl_categories_b c,mtl_system_items_b i Where MMT_STORE_INFO.REQ_NUM = MMT_STORE_ITEM.REQ_NUM And w.organization_id(+) = 84 And C.STRUCTURE_ID = '101' And C.ENABLED_FLAG = 'Y' And i.organization_id = 84 And i.inventory_item_id = ic.inventory_item_id And ic.organization_id = 84  AND c.category_id = ic.category_id  And w.category_id(+) = ic.category_id And MMT_STORE_ITEM.PART_NUM = i.segment1 AND MMT_STORE_INFO.REQ_NUM BETWEEN '" + txtRequestNumFrom.Text + "' AND '" + txtRequestNumTo.Text + "' AND MMT_STORE_INFO.REQ_STATUS = '" + ddlStatus.SelectedItem.ToString() + "' AND MMT_STORE_INFO.REQ_LOCATION = '" + ddlLocation.SelectedItem.ToString() + "' ORDER BY MMT_STORE_ITEM.REQ_NUM DESC";
        }
        else
        {
            db.TSql = "SELECT MMT_STORE_INFO.* , MMT_STORE_ITEM.* , c.segment2 ITEM_CAT , i.DESCRIPTION, i.inventory_item_id From MMT_STORE_INFO ,MMT_STORE_ITEM ,WIp_def_cat_acc_classes_v w ,mtl_item_categories ic ,mtl_categories_b c,mtl_system_items_b i Where MMT_STORE_INFO.REQ_NUM = MMT_STORE_ITEM.REQ_NUM And w.organization_id(+) = 84 And C.STRUCTURE_ID = '101' And C.ENABLED_FLAG = 'Y' And i.organization_id = 84 And i.inventory_item_id = ic.inventory_item_id And ic.organization_id = 84  AND c.category_id = ic.category_id  And w.category_id(+) = ic.category_id And MMT_STORE_ITEM.PART_NUM = i.segment1 AND MMT_STORE_INFO.REQ_NUM = '" + txtRequestNumFrom.Text + "' ORDER BY MMT_STORE_ITEM.REQ_NUM DESC";
        }

        dt = db.GetDataOra();
        if (dt.Rows.Count != 0)
        {
            Div4.Visible = true;

            dgvMachine.DataSource = dt;
            dgvMachine.DataBind();
        }
        else
        {
            Div4.Visible = false;
        }

        //txtRequestNumFrom.Text = "";
        //txtRequestNumFrom.Focus();

        //txtRequestNumTo.Text = "";
        //ddlLocation.Text = "Please Select ...";
        //ddlStatus.Text = "Please Select ...";

        //txtRequestNumTo.Enabled = false;
        //ddlLocation.Enabled = false;
        //ddlStatus.Enabled = false;
        //btnSubmit.Enabled = false;
    }

    protected void txtRequestNumFrom_TextChanged(object sender, EventArgs e)
    {
        if (txtRequestNumFrom.Text.Length > 0)
        {
            txtRequestNumTo.Enabled = true;
            txtRequestNumTo.Focus();

            btnSubmit.Enabled = true;
        }
        else
        {
            txtRequestNumTo.Enabled = false;
            btnSubmit.Enabled = false;
        }
    }

    protected void txtRequestNumTo_TextChanged(object sender, EventArgs e)
    {
        if (txtRequestNumTo.Text.Length > 0)
        {
            ddlLocation.Enabled = true;
            ddlLocation.Focus();

            btnSubmit.Enabled = false;
        }
        else
        {
            btnSubmit.Enabled = false;
        }
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedItem.Value != "Please Select ...")
        {
            ddlStatus.Enabled = true;
            ddlStatus.Focus();
        }
        else
        {
            ddlStatus.Enabled = false;
        }
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedItem.Value != "Please Select ...")
        {
            btnSubmit.Enabled = true;
            btnSubmit.Focus();
        }
        else
        {
            btnSubmit.Enabled = false;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if ((txtRequestNumFrom.Text.Trim().Length != 0) && (txtRequestNumTo.Text.Trim().Length != 0))
        {
            db.TSql = "SELECT MMT_STORE_INFO.* , MMT_STORE_ITEM.* , c.segment2 ITEM_CAT , i.DESCRIPTION, i.inventory_item_id From MMT_STORE_INFO ,MMT_STORE_ITEM ,WIp_def_cat_acc_classes_v w ,mtl_item_categories ic ,mtl_categories_b c,mtl_system_items_b i Where MMT_STORE_INFO.REQ_NUM = MMT_STORE_ITEM.REQ_NUM And w.organization_id(+) = 84 And C.STRUCTURE_ID = '101' And C.ENABLED_FLAG = 'Y' And i.organization_id = 84 And i.inventory_item_id = ic.inventory_item_id And ic.organization_id = 84  AND c.category_id = ic.category_id  And w.category_id(+) = ic.category_id And MMT_STORE_ITEM.PART_NUM = i.segment1 AND MMT_STORE_INFO.REQ_NUM BETWEEN '" + txtRequestNumFrom.Text + "' AND '" + txtRequestNumTo.Text + "' AND MMT_STORE_INFO.REQ_STATUS = '" + ddlStatus.SelectedItem.ToString() + "' AND MMT_STORE_INFO.REQ_LOCATION = '" + ddlLocation.SelectedItem.ToString() + "' ORDER BY MMT_STORE_ITEM.REQ_NUM";
        }
        else
        {
            db.TSql = "SELECT MMT_STORE_INFO.* , MMT_STORE_ITEM.* , c.segment2 ITEM_CAT , i.DESCRIPTION, i.inventory_item_id From MMT_STORE_INFO ,MMT_STORE_ITEM ,WIp_def_cat_acc_classes_v w ,mtl_item_categories ic ,mtl_categories_b c,mtl_system_items_b i Where MMT_STORE_INFO.REQ_NUM = MMT_STORE_ITEM.REQ_NUM And w.organization_id(+) = 84 And C.STRUCTURE_ID = '101' And C.ENABLED_FLAG = 'Y' And i.organization_id = 84 And i.inventory_item_id = ic.inventory_item_id And ic.organization_id = 84  AND c.category_id = ic.category_id  And w.category_id(+) = ic.category_id And MMT_STORE_ITEM.PART_NUM = i.segment1 AND MMT_STORE_INFO.REQ_NUM = '" + txtRequestNumFrom.Text + "' ORDER BY MMT_STORE_ITEM.REQ_NUM";
        }

        DT = db.GetDataOra();

        ExportExcel(DT, "ExportToExcel_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "_Number.csv", string.Empty);

        txtRequestNumFrom.Text = "";
        txtRequestNumFrom.Focus();

        txtRequestNumTo.Text = "";
        ddlLocation.Text = "Please Select ...";
        ddlStatus.Text = "Please Select ...";

        txtRequestNumTo.Enabled = false;
        ddlLocation.Enabled = false;
        ddlStatus.Enabled = false;
        btnSubmit.Enabled = false;
    }

    public static void ExportExcel(DataTable ds, string filename, string sheetname)
    {
        StreamWriter sw = new StreamWriter("C:\\" + filename, false);
        //headers    
        for (int i = 0; i < ds.Columns.Count; i++)
        {
            sw.Write(ds.Columns[i]);
            if (i < ds.Columns.Count - 1)
            {
                sw.Write(",");
            }
        }
        sw.Write(sw.NewLine);
        foreach (DataRow dr in ds.Rows)
        {
            for (int i = 0; i < ds.Columns.Count; i++)
            {
                if (!Convert.IsDBNull(dr[i]))
                {
                    string value = dr[i].ToString();
                    if (value.Contains(","))
                    {
                        value = String.Format("\"{0}\"", value);
                        sw.Write(value);
                    }
                    else
                    {
                        sw.Write(dr[i].ToString());
                    }
                }
                if (i < ds.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
        }
        sw.Close();
    }
}
