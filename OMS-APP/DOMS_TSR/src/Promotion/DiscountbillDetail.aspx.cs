using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using SALEORDER.DTO;
using Newtonsoft.Json;
using SALEORDER.Common;
using AjaxControlToolkit;
using System.IO;

namespace DOMS_TSR.src.Promotion
{
    public partial class DiscountbillDetail : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];

        string Codelist = "";
        string EditFlag = "";
        Boolean isdelete;
        Boolean isinsert;
        string currentDiscountBillType;
        protected static int currentPageNumber;
        protected static int currentPdPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;

                currentPdPageNumber = 1;

                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantinfo = new MerchantInfo();
                merchantinfo = (MerchantInfo)Session["MerchantInfo"];
                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerchantCode.Value = merchantinfo.MerchantCode;
                    
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

              
                LoadDiscountBill();
                LoadDiscountBillDetail();
                
                LoadProduct();
                
            }

        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadDiscountBillDetail();
        }

        protected void ddlProductPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPdPageNumber = Int32.Parse(ddlPdPage.SelectedValue);

            LoadProduct();
        }
        protected void ddlProcomPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPdPageNumber = Int32.Parse(ddlProcom.SelectedValue);

            LoadProduct();
        }

        #region Function



        protected void LoadDiscountBill()
        {
            List<DiscountBillInfo> lDiscountBillInfo = new List<DiscountBillInfo>();

            lDiscountBillInfo = GetDiscountBillMasterByCriteria();

            if (lDiscountBillInfo.Count > 0)
            {
                txtDiscountBillCode.Text = lDiscountBillInfo[0].DiscountBillCode;

                txtDiscountBillName.Text = lDiscountBillInfo[0].DiscountBillName;

                txtDiscountBillTypeName.Text = lDiscountBillInfo[0].DiscountBillTypeName;

                txtFreeShippingFlag.Text = lDiscountBillInfo[0].FreeShipping;

                hidDiscountBillTypeCode.Text = lDiscountBillInfo[0].DiscountBillTypeCode;
                if (lDiscountBillInfo[0].DiscountBillTypeCode == StaticField.DiscountBillTypeCode_14) 
                {
                    btnAddCombo.Visible = true;
                }
                else
                {
                    btnAddCombo.Visible = false;
                }
                currentDiscountBillType = lDiscountBillInfo[0].DiscountBillTypeCode;



                hidDiscountAmount.Value = lDiscountBillInfo[0].DiscountAmount.ToString();

                hidDiscountPercent.Value = lDiscountBillInfo[0].DiscountPercent.ToString();
              
                

                hidProductBrandCode.Value = lDiscountBillInfo[0].BrandCode;

                DateTime sDate = DateTime.Parse(lDiscountBillInfo[0].StartDate).Date;
                txtStartDate.Text = sDate.ToString("dd/MM/yyyy");

                DateTime eDate = DateTime.Parse(lDiscountBillInfo[0].EndDate).Date;
                txtEndDate.Text = eDate.ToString("dd/MM/yyyy");



                if (lDiscountBillInfo[0].DiscountAmount.ToString() == "0" && lDiscountBillInfo[0].DiscountPercent.ToString() != "0")
                {
                    txtDiscount.Text = lDiscountBillInfo[0].DiscountPercent.ToString() + "%";
                }
                else if (lDiscountBillInfo[0].DiscountPercent.ToString() == "0" && lDiscountBillInfo[0].DiscountAmount.ToString() != "0")
                {
                    txtDiscount.Text = lDiscountBillInfo[0].DiscountAmount.ToString() + " บาท";
                }
                else
                {
                    txtDiscount.Text = "ไม่มีส่วนลด";
                }
            }

        }
        protected void loadDiscountBillCombo()
        {
            List<CombosetInfo> lDiscountBillInfo = new List<CombosetInfo>();

            


            lDiscountBillInfo = GetDiscountBillComboMasterByCriteria();

            GvProCombo.DataSource = lDiscountBillInfo;

            GvProCombo.DataBind();


            

        }
        protected void CheckComboFlag()
        {
            if (hidFlagComboset.Text == "Y")
            {
                btnAddCombo.Visible = true;
                btnAddDiscountBill.Visible = false;
            }
            else
            {
                btnAddCombo.Visible = false;
                btnAddDiscountBill.Visible = true;
            }
        }
        public List<CombosetInfo> GetDiscountBillComboMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCombosetByCriteriaMaster";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["DiscountBillCode"] = "";


                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CombosetInfo> lDiscountBillInfo = JsonConvert.DeserializeObject<List<CombosetInfo>>(respstr);


            return lDiscountBillInfo;

        }

        public List<DiscountBillInfo> GetDiscountBillMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListDiscountBillNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["DiscountBillCode"] = Request.QueryString["DiscountBillCode"];

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<DiscountBillInfo> lDiscountBillInfo = JsonConvert.DeserializeObject<List<DiscountBillInfo>>(respstr);


            return lDiscountBillInfo;

        }


        protected void LoadDiscountBillDetail() {

            List<DiscountBillDetailInfo> lDiscountBillInfo = new List<DiscountBillDetailInfo>();

            

            int? totalRow = CountPromodetailMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lDiscountBillInfo = GetDiscountBillDetailMasterByCriteria();

            gvDiscountBill.DataSource = lDiscountBillInfo;

            gvDiscountBill.DataBind();
        }
        protected List<DiscountBillDetailInfo> LoadDiscountBillDetailNopaging()
        {


            string respstr = "";

            APIpath = APIUrl + "/api/support/ListDiscountBillProducttionDetailNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["DiscountBillCode"] = txtDiscountBillCode.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<DiscountBillDetailInfo> list = JsonConvert.DeserializeObject<List<DiscountBillDetailInfo>>(respstr);


            return list;


        }
        protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvProduct.Rows[index];
            int? cou = 0;

            Label lblmsg = (Label)row.FindControl("lblmsg");

            TextBox txtPrice_Ins = (TextBox)row.FindControl("txtPrice_Ins");
            TextBox txtQty_Ins = (TextBox)row.FindControl("txtQty_Ins");
            

            HiddenField hidProductId = (HiddenField)row.FindControl("hidDiscountBillId");
            HiddenField hidProductCode = (HiddenField)row.FindControl("hidProductCode");
            HiddenField hidProductName = (HiddenField)row.FindControl("hidProductName");
            HiddenField hidPrice = (HiddenField)row.FindControl("hidPrice");
            
            HiddenField hidDefaultAmount = (HiddenField)row.FindControl("hidDefaultAmount");
            
            HiddenField hidProductPrice = (HiddenField)row.FindControl("hidProductPrice");
            HiddenField detailId = (HiddenField)row.FindControl("hidDiscountBillDetailId");
 

            if (e.CommandName == "AddDiscountBillDetail")
            {
               
                hidFlagInsert.Value = "True";
                string respstr = "";

                APIpath = APIUrl + "/api/support/InsertDiscountBillDetail";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["DiscountBillCode"] = txtDiscountBillCode.Text;
                    data["ProductCode"] = hidProductCode.Value;
                    data["DefaultAmount"] = txtQty_Ins.Text;
                    data["Price"] = txtPrice_Ins.Text;
                    data["FlagDelete"] = "N";
                    data["DiscountPercent"] = "0";
                    data["DiscountAmount"] = "0";
                    data["ComplementaryAmount"] = "0";
                    data["CreateBy"] = hidEmpCode.Value;
                  




                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                cou = JsonConvert.DeserializeObject<int?>(respstr);
                if (cou > 0) {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('เพิ่มรายการสำเร็จ');", true);
                    LoadDiscountBillDetail();
                    LoadProduct();

                }

                

            }
            else if (e.CommandName == "UpdateDetail")
            {
                string respstr = "";

                APIpath = APIUrl + "/api/support/UpdateDiscountBillDetailInfo";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["DiscountBillCode"] = txtDiscountBillCode.Text;
                    data["ProductCode"] = hidProductCode.Value;
                    data["DefaultAmount"] = txtQty_Ins.Text;
                    data["Price"] = txtPrice_Ins.Text;
                    data["FlagDelete"] = "N";
                    data["DiscountPercent"] = hidDiscountPercent.Value;
                    data["DiscountAmount"] = hidDiscountAmount.Value;
                    data["ComplementaryAmount"] = "0";
                    data["CreateBy"] = hidEmpCode.Value;
                  
                    data["DiscountBillDeailInfoId"] = detailId.Value;




                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                cou = JsonConvert.DeserializeObject<int?>(respstr);

                if (cou > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('แก้ไขข้อมูลสำเร็จ');", true);
                    LoadDiscountBillDetail();
                    LoadProduct();

                }
            }

        }

        protected void SetPageBar(double totalRow)
        {

            lblTotalPages.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages.Text) + 1; i++)
            {
                ddlPage.Items.Add(new ListItem(i.ToString()));
            }
            setDDl(ddlPage, currentPageNumber.ToString());
            

            
            if ((currentPageNumber == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst.Enabled = false;
                lnkbtnPre.Enabled = false;
                lnkbtnNext.Enabled = true;
                lnkbtnLast.Enabled = true;
            }
            else if ((currentPageNumber.ToString() == lblTotalPages.Text) && (currentPageNumber == 1))
            {
                lnkbtnFirst.Enabled = false;
                lnkbtnPre.Enabled = false;
                lnkbtnNext.Enabled = false;
                lnkbtnLast.Enabled = false;
            }
            else if ((currentPageNumber.ToString() == lblTotalPages.Text) && (currentPageNumber > 1))
            {
                lnkbtnFirst.Enabled = true;
                lnkbtnPre.Enabled = true;
                lnkbtnNext.Enabled = false;
                lnkbtnLast.Enabled = false;
            }
            else
            {
                lnkbtnFirst.Enabled = true;
                lnkbtnPre.Enabled = true;
                lnkbtnNext.Enabled = true;
                lnkbtnLast.Enabled = true;
            }
            
        }

        private void setDDl(DropDownList ddls, String val)
        {
            ListItem li;
            for (int i = 0; i < ddls.Items.Count; i++)
            {
                li = ddls.Items[i];
                if (val.Equals(li.Value))
                {
                    ddls.SelectedIndex = i;
                    break;
                }
            }

        }

        protected void SetPageBar_Product(double totalRow)
        {

            lblTotalPdPages.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPdPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPdPages.Text) + 1; i++)
            {
                ddlPdPage.Items.Add(new ListItem(i.ToString()));
            }
            setDDl_Product(ddlPdPage, currentPdPageNumber.ToString());
            

            
            if ((currentPdPageNumber == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                btnPdFirst.Enabled = false;
                btnPdPre.Enabled = false;
                btnPdNext.Enabled = true;
                btnPdLast.Enabled = true;
            }
            else if ((currentPdPageNumber.ToString() == lblTotalPdPages.Text) && (currentPdPageNumber == 1))
            {
                btnPdFirst.Enabled = false;
                btnPdPre.Enabled = false;
                btnPdNext.Enabled = false;
                btnPdLast.Enabled = false;
            }
            else if ((currentPdPageNumber.ToString() == lblTotalPdPages.Text) && (currentPdPageNumber > 1))
            {
                btnPdFirst.Enabled = true;
                btnPdPre.Enabled = true;
                btnPdNext.Enabled = false;
                btnPdLast.Enabled = false;
            }
            else
            {
                btnPdFirst.Enabled = true;
                btnPdPre.Enabled = true;
                btnPdNext.Enabled = true;
                btnPdLast.Enabled = true;
            }
            
        }
        protected void SetPageBar_Procom(double totalRow)
        {

            lblTotalPdPages.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPdPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPdPages.Text) + 1; i++)
            {
                ddlPdPage.Items.Add(new ListItem(i.ToString()));
            }
            setDDl_Product(ddlPdPage, currentPdPageNumber.ToString());
            

            
            if ((currentPdPageNumber == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                btnPdFirst.Enabled = false;
                btnPdPre.Enabled = false;
                btnPdNext.Enabled = true;
                btnPdLast.Enabled = true;
            }
            else if ((currentPdPageNumber.ToString() == lblTotalPdPages.Text) && (currentPdPageNumber == 1))
            {
                btnPdFirst.Enabled = false;
                btnPdPre.Enabled = false;
                btnPdNext.Enabled = false;
                btnPdLast.Enabled = false;
            }
            else if ((currentPdPageNumber.ToString() == lblTotalPdPages.Text) && (currentPdPageNumber > 1))
            {
                btnPdFirst.Enabled = true;
                btnPdPre.Enabled = true;
                btnPdNext.Enabled = false;
                btnPdLast.Enabled = false;
            }
            else
            {
                btnPdFirst.Enabled = true;
                btnPdPre.Enabled = true;
                btnPdNext.Enabled = true;
                btnPdLast.Enabled = true;
            }
            
        }
        private void setDDl_Procom(DropDownList ddls, String val)
        {
            ListItem li;
            for (int i = 0; i < ddls.Items.Count; i++)
            {
                li = ddls.Items[i];
                if (val.Equals(li.Value))
                {
                    ddls.SelectedIndex = i;
                    break;
                }
            }

        }
        private void setDDl_Product(DropDownList ddls, String val)
        {
            ListItem li;
            for (int i = 0; i < ddls.Items.Count; i++)
            {
                li = ddls.Items[i];
                if (val.Equals(li.Value))
                {
                    ddls.SelectedIndex = i;
                    break;
                }
            }

        }

        public int? CountPromodetailMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountDiscountBillDetailListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["DiscountBillCode"] = txtDiscountBillCode.Text;

                data["ProductCode"] = txtSearchProductCode.Text;

                data["ProductName"] = txtSearchProductName.Text;

                data["MerchantCode"] = ddlSearchChannelCode.SelectedValue;

                

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }

        protected Boolean DeleteDiscountBillDetail()
        {

            for (int i = 0; i < gvDiscountBill.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvDiscountBill.Rows[i].FindControl("chkDiscountBill");

                if (checkbox.Checked == true)
                {
                    Label lblid = (Label)gvDiscountBill.Rows[i].FindControl("LBLid");
                    HiddenField hidDiscountBillDetailInfoId = (HiddenField)gvDiscountBill.Rows[i].FindControl("hidDiscountBillDetailInfoId");

                    if (Codelist != "")
                    {
                            Codelist += "," + lblid.Text + "";
                    }
                    else
                    {
                            Codelist += "" + lblid.Text + "";
                    }

                }
            }

            if (Codelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteDiscountBillDetailInfoByIdString";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["DiscountBillCode"] = Codelist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                


            }
            else
            {
                hidIdList.Value = "";
                return false;
            }

            hidIdList.Value = "";
            return true;

        }

        protected Boolean AddDiscountBillDetail()
        {
            int? cou = 0;

            for (int i = 0; i < gvProduct.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvProduct.Rows[i].FindControl("chkProduct");

                if (checkbox.Checked == true)
                {
                    HiddenField hidProductCode = (HiddenField)gvProduct.Rows[i].FindControl("hidProductCode");
                    TextBox txtQty_Ins = (TextBox)gvProduct.Rows[i].FindControl("txtQty_Ins");
                    TextBox txtPrice_Ins = (TextBox)gvProduct.Rows[i].FindControl("txtPrice_Ins");


                    string respstr = "";

                    APIpath = APIUrl + "/api/support/InsertDiscountBillDetail";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["DiscountBillCode"] = txtDiscountBillCode.Text;
                        data["ProductCode"] = hidProductCode.Value;
                        data["DefaultAmount"] = txtQty_Ins.Text;
                        data["Price"] = txtPrice_Ins.Text;
                        data["FlagDelete"] = "N";
                        data["FlagDelete"] = "N";
                        data["DiscountPercent"] = "0";
                        data["DiscountAmount"] = "0";
                        data["ComplementaryAmount"] = "0";
                        data["CreateBy"] = hidEmpCode.Value;

                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    cou = JsonConvert.DeserializeObject<int?>(respstr);

                }
            }

            if (cou != 0)
            {

                


                hidIdList.Value = "";
                return true;

            }
            else
            {
                hidIdList.Value = "";
                return false;
            }



        }

        

        public int? CountProductMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountProductMasterListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = hidProductBrandCode.Value;

                data["ProductCode"] = txtSearchProductCode.Text;

                data["ProductName"] = txtSearchProductName.Text;

                data["ChannelCode"] = ddlSearchChannelCode.SelectedValue;

                
                data["MerchantCode"] = hidMerchantCode.Value;

                data["rowOFFSet"] = ((currentPdPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }

        #endregion




        #region Event 

        protected void gvDiscountBillDetail_Change(object sender, GridViewPageEventArgs e)
        {
            gvDiscountBill.PageIndex = e.NewPageIndex;

            List<DiscountBillDetailInfo> lDiscountBillDetailInfo = new List<DiscountBillDetailInfo>();

            lDiscountBillDetailInfo = GetDiscountBillDetailMasterByCriteria();

            gvDiscountBill.DataSource = lDiscountBillDetailInfo;

            gvDiscountBill.DataBind();

        }

        public List<DiscountBillDetailInfo> GetDiscountBillDetailMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListDiscountBillProducttionDetailByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["DiscountBillCode"] = txtDiscountBillCode.Text;

                data["ProductCode"] = txtSearchProductCode.Text;

                data["ProductName"] = txtSearchProductName.Text;

                data["ChannelCode"] = ddlSearchChannelCode.SelectedValue;

                

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<DiscountBillDetailInfo> lDiscountBillDetailInfo = JsonConvert.DeserializeObject<List<DiscountBillDetailInfo>>(respstr);


            return lDiscountBillDetailInfo;

        }

        protected void LoadProduct()
        {

            List<ProductInfo> lProductInfo = new List<ProductInfo>();

            

            int? totalRow = CountProductMasterList();

            SetPageBar_Product(Convert.ToDouble(totalRow));


            lProductInfo = GetProductMasterByCriteria();

            gvProduct.DataSource = lProductInfo;

            gvProduct.DataBind();
        }

        protected void gvProduct_Change(object sender, GridViewPageEventArgs e)
        {
            gvProduct.PageIndex = e.NewPageIndex;

            List<ProductInfo> lProductInfo = new List<ProductInfo>();

            lProductInfo = GetProductMasterByCriteria();

            gvProduct.DataSource = lProductInfo;

            gvProduct.DataBind();

        }

        public List<ProductInfo> GetProductMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductMasterByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = hidProductBrandCode.Value;

                data["ProductCode"] = txtSearchModalProductCode.Text;

                data["ProductName"] = txtSearchModalProductName.Text;

                data["ChannelCode"] = ddlSearchModalChannel.SelectedValue;

                data["MerchantCode"] = hidMerchantCode.Value;
                

                data["rowOFFSet"] = ((currentPdPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);


            return lProductInfo;

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            LoadDiscountBillDetail();
          
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            ddlSearchChannelCode.SelectedValue = "-99";
            
            txtSearchProductCode.Text = "";
            txtSearchProductName.Text = "";
        }

        protected void btnAddDiscountBill_Click(object sender, EventArgs e)
        {
            hidFlagInsert.Value = "True";


            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Product').modal();", true);

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteDiscountBillDetail();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            isinsert = AddDiscountBillDetail();

            btnSearch_Click(null, null);

            if (!isinsert)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการเพิ่ม');", true);
            }

        }

        protected void chkDiscountBillDetailAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvDiscountBill.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvDiscountBill.HeaderRow.FindControl("chkDiscountBillAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvDiscountBill.Rows[i].FindControl("hidDiscountBillId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkDiscountBill = (CheckBox)gvDiscountBill.Rows[i].FindControl("chkDiscountBill");

                    chkDiscountBill.Checked = true;
                }
                else
                {

                    CheckBox chkDiscountBill = (CheckBox)gvDiscountBill.Rows[i].FindControl("chkDiscountBill");

                    chkDiscountBill.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        protected void GetPageIndex(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber = 1;
                    break;

                case "Previous":
                    currentPageNumber = Int32.Parse(ddlPage.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber = Int32.Parse(ddlPage.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber = Int32.Parse(lblTotalPages.Text);
                    break;
            }


            LoadDiscountBill();
        }

        protected void chkProductAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvProduct.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvProduct.HeaderRow.FindControl("chkProductAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvProduct.Rows[i].FindControl("hidProductId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkProduct = (CheckBox)gvProduct.Rows[i].FindControl("chkProduct");

                    chkProduct.Checked = true;
                }
                else
                {

                    CheckBox chkProduct = (CheckBox)gvProduct.Rows[i].FindControl("chkProduct");

                    chkProduct.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            List<DiscountBillDetailInfo> DiscountBillDetails = LoadDiscountBillDetailNopaging();
            string ProdisPer = hidProductDiscountPercent.Value;
            string ProdisAmont = hidProductDiscountAmount.Value;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField itemCode = (HiddenField)e.Row.FindControl("hidProductCode");
                HiddenField hidProductPrice = (HiddenField)e.Row.FindControl("hidProductPrice");

                var avail = DiscountBillDetails.Find(x => x.ProductCode == itemCode.Value);
                if (avail != null)
                {
                    HiddenField detailId = (HiddenField)e.Row.FindControl("hidDiscountBillDetailId");
                    TextBox txtqty = (TextBox)e.Row.FindControl("txtQty_Ins");
                    LinkButton addBtn = (LinkButton)e.Row.FindControl("btnEdit");

                    detailId.Value = avail.DiscountBillDetailId.ToString();
                    txtqty.Text = avail.DefaultAmount.ToString();


                    addBtn.CommandName = "UpdateDetail";

                }
                else
                {
                    TextBox txtqty = (TextBox)e.Row.FindControl("txtQty_Ins");

                    txtqty.Text = "1";
                }

               
                TextBox txtPrice_Ins = (TextBox)e.Row.FindControl("txtPrice_Ins");
                txtPrice_Ins.Text = "0";
                

            }




        }

        protected void gvProduct_RowCreated(object sender, GridViewRowEventArgs e)
        {
            Label priceLabel = (Label)e.Row.FindControl("lblHeaderPrice");

            if (hidDiscountBillTypeCode.Text == "02" )
            {
                gvProduct.Columns[3].Visible = true;
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Label lbl = (Label)e.Row.FindControl("lblHeaderPrice");
                    lbl.Text = "แลกซื้อ";

                }
            }
       
            else
            {
                gvProduct.Columns[3].Visible = false;
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Label lbl = (Label)e.Row.FindControl("lblHeaderPrice");
                    lbl.Text = "Price";

                }
            }
        }

        protected void GetProductPageIndex(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPdPageNumber = 1;
                    break;

                case "Previous":
                    currentPdPageNumber = Int32.Parse(ddlPdPage.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPdPageNumber = Int32.Parse(ddlPdPage.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPdPageNumber = Int32.Parse(lblTotalPdPages.Text);
                    break;
            }


            LoadProduct();
        }

        protected void GvProCombo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void GvProCombo_RowCreated(object sender, GridViewRowEventArgs e)
        {
            
        }
        protected void GvProCombo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = GvProCombo.Rows[index];
            int? cou = 0;

            Label lblmsg = (Label)row.FindControl("lblmsg");
            Label lblCombosetCode = (Label)row.FindControl("lblCombosetCode");
            Label lblPrice = (Label)row.FindControl("lblPrice");
            HiddenField hidCombosetId = (HiddenField)row.FindControl("hidCombosetId");
            HiddenField hidCombosetCode = (HiddenField)row.FindControl("hidCombosetCode");
            HiddenField hidCombosetName = (HiddenField)row.FindControl("hidCombosetName");

            string aaa, bbb;
            aaa = lblCombosetCode.Text;
            bbb = hidCombosetCode.Value;
            if (e.CommandName == "AddDiscountBillComboDetail")
            {

                hidFlagInsert.Value = "True";
                string respstr = "";

                APIpath = APIUrl + "/api/support/InsertDiscountBillDetailCombo";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CombosetCode"] = hidCombosetCode.Value;

                    data["DiscountBillDetailName"] = hidCombosetName.Value;
                    data["DiscountBillCode"] = Request.QueryString["DiscountBillCode"];
                    data["Price"] ="0";
                    data["FlagDelete"] = "N";


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                cou = JsonConvert.DeserializeObject<int?>(respstr);
                if (cou > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('เพิ่มรายการสำเร็จ');", true);
                    LoadDiscountBill();
                    loadDiscountBillCombo();
                    LoadDiscountBillDetail();
                    

                }
            }

        }
        protected void GetProComboPageIndex(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPdPageNumber = 1;
                    break;

                case "Previous":
                    currentPdPageNumber = Int32.Parse(ddlProcom.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPdPageNumber = Int32.Parse(ddlProcom.SelectedValue) + 1;
                    break;

                case "Last":
                    
                    break;
            }


            LoadProduct();
        }

        protected void btnModalSearch_Click(object sender, EventArgs e)
        {
            LoadProduct();
        }

        protected void btnModalClear_Click(object sender, EventArgs e)
        {
            txtSearchModalProductCode.Text = "";
            txtSearchModalProductName.Text = "";
            ddlSearchModalChannel.SelectedValue = "-99";
        }

        protected void btnSubmitCombo_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            POInfo pInfo = new POInfo();

            string pCode = hidProductBrandCode.Value == StaticField.ProductBrandCode_MK0001 ? StaticField.ProductBrandCode_MK000035 : StaticField.ProductBrandCode_YAYOI000002; 

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                if (hidFlagInsertCombo.Value == "True") //Insert
                {


                    string respstr = "";

                    APIpath = APIUrl + "/api/support/InsertDiscountBillDetailCombo";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["CombosetCode"] = txtCombosetCode_Ins.Text;
                        data["ProductCode"] = pCode;
                        data["DiscountBillDetailName"] = txtCombosetName_Ins.Text;
                        data["DiscountBillCode"] = txtDiscountBillCode.Text;
                        data["Price"] = txtCombosetPrice_Ins.Text;
                        data["FlagDelete"] = "N";



                        data["CreateBy"] = empInfo.EmpCode;


                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                    if (sum > 0)
                    {


                        btnCancelCombo_Click(null, null);

                        LoadDiscountBillDetail();

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-Comboset').modal('hide');", true);



                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                    }

                }
                else //Update
                {


                    string respstr = "";

                    APIpath = APIUrl + "/api/support/UpdateDiscountBillDetailCombo";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["CombosetId"] = hidIdList.Value;

                        data["CombosetCode"] = txtCombosetCode_Ins.Text;
                        data["DiscountBillDetailName"] = txtCombosetName_Ins.Text;
                        data["DiscountBillCode"] = txtDiscountBillCode.Text;
                        data["Price"] = txtCombosetPrice_Ins.Text;

                        data["FlagDelete"] = "N";


                        data["UpdateBy"] = empInfo.EmpCode;


                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                    if (sum > 0)
                    {


                        btnCancelCombo_Click(null, null);

                        LoadDiscountBillDetail();

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-Comboset').modal('hide');", true);



                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                    }

                }

            }
        }

        protected void btnCancelCombo_Click(object sender, EventArgs e)
        {
            txtCombosetCode_Ins.Text = "";
            txtCombosetName_Ins.Text = "";
            txtCombosetPrice_Ins.Text = "";          

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Comboset').modal('hide');", true);
        }

        protected void gvDiscountBill_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (hidFlagComboset.Text == "Y" )
            {
                gvDiscountBill.Columns[1].Visible = true;
                gvDiscountBill.Columns[2].Visible = true;
                gvDiscountBill.Columns[3].Visible = false;
                gvDiscountBill.Columns[4].Visible = false;
                gvDiscountBill.Columns[5].Visible = false;
                gvDiscountBill.Columns[7].Visible = false;
                gvDiscountBill.Columns[8].Visible = false;
               
            }
            else
            {
                gvDiscountBill.Columns[1].Visible = false;
                gvDiscountBill.Columns[2].Visible = false;
                gvDiscountBill.Columns[3].Visible = true;
                gvDiscountBill.Columns[4].Visible = true;
                gvDiscountBill.Columns[5].Visible = true;
                gvDiscountBill.Columns[7].Visible = true;
                gvDiscountBill.Columns[8].Visible = true;
              
            }
        }

        protected void btnAddCombo_Click(object sender, EventArgs e)
        {

            hidFlagInsertCombo.Value = "True";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Comboset').modal();", true);
        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"DiscountBillDetail.aspx?DiscountBillCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        protected string GetLinktoCombo(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"CombosetDetail.aspx?CombosetCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("DiscountBill.aspx");
        }

        protected void BindddlSearchDetailChannelCode()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListChannelNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCategoryCode"] = null;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ChannelInfo> lLookupInfo = JsonConvert.DeserializeObject<List<ChannelInfo>>(respstr);


            ddlSearchChannelCode.DataSource = lLookupInfo;

            ddlSearchChannelCode.DataTextField = "ChannelName";

            ddlSearchChannelCode.DataValueField = "ChannelCode";

            ddlSearchChannelCode.DataBind();

            ddlSearchChannelCode.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }

        protected void BindddlSearchProductChannelCode()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListChannelNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCategoryCode"] = null;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ChannelInfo> lLookupInfo = JsonConvert.DeserializeObject<List<ChannelInfo>>(respstr);


            ddlSearchModalChannel.DataSource = lLookupInfo;

            ddlSearchModalChannel.DataTextField = "ChannelName";

            ddlSearchModalChannel.DataValueField = "ChannelCode";

            ddlSearchModalChannel.DataBind();

            ddlSearchModalChannel.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }





        #endregion

       
    }
}