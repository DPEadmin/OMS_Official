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
using RawPrint;
using System.Xml.Linq;
using PdfSharp;
using PdfSharp.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace DOMS_TSR.src.TestCode
{
    public partial class TestPrint : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];

        string Codelist = "";
        string EditFlag = "";
        Boolean isdelete;
        Boolean isinsert;
        string currentPromotionType;
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

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                }
                else
                {
                    Response.Redirect("..\\Default.aspx?flaglogin=_EMPCODENULL");
                }


                
            }

        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadPromotionDetail();
        }

        protected void ddlProductPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPdPageNumber = Int32.Parse(ddlPdPage.SelectedValue);

            LoadProduct();
        }

        #region Function



        protected void LoadPromotion()
        {
            List<PromotionInfo> lPromotionInfo = new List<PromotionInfo>();

            lPromotionInfo = GetPromotionMasterByCriteria();

            txtPromotionCode.Text = lPromotionInfo[0].PromotionCode;

            txtPromotionName.Text = lPromotionInfo[0].PromotionName;

            txtPromotionTypeName.Text = lPromotionInfo[0].PromotionTypeName;

            txtFreeShippingFlag.Text = lPromotionInfo[0].FreeShippingName;

            hidPromotionTypeCode.Text = lPromotionInfo[0].PromotionTypeCode;

            currentPromotionType = lPromotionInfo[0].PromotionTypeCode;

            txtPromotionStatusName.Text = lPromotionInfo[0].PromotionStatusName;

            txtDescription.InnerText = lPromotionInfo[0].PromotionDesc;

            hidDiscountAmount.Value = lPromotionInfo[0].ProductDiscountAmount.ToString();

            hidDiscountPercent.Value = lPromotionInfo[0].ProductDiscountPercent.ToString();

            

            hidProductBrandCode.Value = lPromotionInfo[0].ProductBrandCode;

            hidFlagComboset.Text = lPromotionInfo[0].CombosetFlag.Trim();

            txtLockCheckbox.Text = lPromotionInfo[0].LockCheckbox.Trim() == "Y" ? "ใช่" : "ไม่ใช่";

            txtLockAmountFlag.Text = lPromotionInfo[0].LockAmountFlag.Trim() == "Y" ? "แก้ไขได้" : "แก้ไขไม่ได้";
            DateTime sDate = DateTime.Parse(lPromotionInfo[0].StartDate).Date;
            txtStartDate.Text = sDate.ToString("dd/MM/yyyy");

            DateTime eDate = DateTime.Parse(lPromotionInfo[0].EndDate).Date;
            txtEndDate.Text = eDate.ToString("dd/MM/yyyy");



            if (lPromotionInfo[0].DiscountAmount == 0 && lPromotionInfo[0].ProductDiscountAmount == 0 &&
                lPromotionInfo[0].ProductDiscountPercent == 0 && lPromotionInfo[0].DiscountPercent != 0)
            {
                txtDiscount.Text = lPromotionInfo[0].DiscountPercent + "%";
            }
            else if (lPromotionInfo[0].DiscountPercent == 0 && lPromotionInfo[0].ProductDiscountAmount == 0 &&
                lPromotionInfo[0].ProductDiscountPercent == 0 && lPromotionInfo[0].DiscountAmount != 0)
            {
                txtDiscount.Text = lPromotionInfo[0].DiscountAmount + " บาท";
            }
            else if (lPromotionInfo[0].DiscountPercent == 0 && lPromotionInfo[0].ProductDiscountAmount == 0 &&
               lPromotionInfo[0].DiscountAmount == 0 && lPromotionInfo[0].ProductDiscountPercent != 0)
            {
                txtDiscount.Text = lPromotionInfo[0].ProductDiscountPercent + "%";
            }
            else if (lPromotionInfo[0].ProductDiscountPercent == 0 && lPromotionInfo[0].DiscountPercent == 0 &&
               lPromotionInfo[0].DiscountAmount == 0 && lPromotionInfo[0].ProductDiscountAmount != 0)
            {
                txtDiscount.Text = lPromotionInfo[0].ProductDiscountAmount + " บาท";
            }
            else
            {
                txtDiscount.Text = "ไม่มีส่วนลด";
            }

        }

        protected void CheckComboFlag()
        {
            if (hidFlagComboset.Text == "Y")
            {
                btnAddCombo.Visible = true;
                btnAddPromotion.Visible = false;
            }
            else
            {
                btnAddCombo.Visible = false;
                btnAddPromotion.Visible = true;
            }
        }
        protected List<PromotionDetailInfo> LoadPromotionDetailNopaging()
        {


            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProducttionDetailNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = "FullCombo";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionDetailInfo> list = JsonConvert.DeserializeObject<List<PromotionDetailInfo>>(respstr);


            return list;


        }



        protected void LoadPromotionImages()
        {
            List<PromotionImageInfo> lPromotionImgInfo = new List<PromotionImageInfo>();

            lPromotionImgInfo = GetPromotionImage();

            ProductImg.Src = lPromotionImgInfo.Count > 0 ? APIUrl + lPromotionImgInfo[0].PromotionImageUrl : "";


        }


        public List<PromotionInfo> GetPromotionMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = "FullCombo";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);


            return lPromotionInfo;

        }


        public List<PromotionImageInfo> GetPromotionImage()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/GetPromotionImageUrl";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = "FullCombo";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionImageInfo> lProductInfo = JsonConvert.DeserializeObject<List<PromotionImageInfo>>(respstr);


            return lProductInfo;

        }

        protected void LoadPromotionDetail()
        {

            List<PromotionDetailInfo> lPromotionInfo = new List<PromotionDetailInfo>();

            

            int? totalRow = CountPromodetailMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lPromotionInfo = GetPromotionDetailMasterByCriteria();

            gvPromotion.DataSource = lPromotionInfo;

            gvPromotion.DataBind();
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

            HiddenField hidProductId = (HiddenField)row.FindControl("hidPromotionId");
            HiddenField hidProductCode = (HiddenField)row.FindControl("hidProductCode");
            HiddenField hidProductName = (HiddenField)row.FindControl("hidProductName");
            HiddenField hidPrice = (HiddenField)row.FindControl("hidPrice");
            
            HiddenField hidDefaultAmount = (HiddenField)row.FindControl("hidDefaultAmount");
            
            HiddenField hidProductPrice = (HiddenField)row.FindControl("hidProductPrice");
            HiddenField detailId = (HiddenField)row.FindControl("hidPromotionDetailId");


            if (e.CommandName == "AddPromotionDetail")
            {




                hidFlagInsert.Value = "True";
                string respstr = "";

                APIpath = APIUrl + "/api/support/InsertPromotionDetail";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["PromotionCode"] = txtPromotionCode.Text;
                    data["ProductCode"] = hidProductCode.Value;
                    data["DefaultAmount"] = txtQty_Ins.Text;
                    data["Price"] = txtPrice_Ins.Text;
                    data["FlagDelete"] = "N";
                    data["DiscountPercent"] = hidDiscountPercent.Value;
                    data["DiscountAmount"] = hidDiscountAmount.Value;
                    data["ComplementaryAmount"] = "0";
                    data["CreateBy"] = hidEmpCode.Value;
                    




                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                cou = JsonConvert.DeserializeObject<int?>(respstr);
                if (cou > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('เพิ่มรายการสำเร็จ');", true);
                    LoadPromotionDetail();
                    LoadProduct();

                }



            }
            else if (e.CommandName == "UpdateDetail")
            {
                string respstr = "";

                APIpath = APIUrl + "/api/support/UpdatePromotionDetailInfo";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["PromotionCode"] = txtPromotionCode.Text;
                    data["ProductCode"] = hidProductCode.Value;
                    data["DefaultAmount"] = txtQty_Ins.Text;
                    data["Price"] = txtPrice_Ins.Text;
                    data["FlagDelete"] = "N";
                    data["DiscountPercent"] = hidDiscountPercent.Value;
                    data["DiscountAmount"] = hidDiscountAmount.Value;
                    data["ComplementaryAmount"] = "0";
                    data["CreateBy"] = hidEmpCode.Value;
                    
                    data["PromotionDeailInfoId"] = detailId.Value;




                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                cou = JsonConvert.DeserializeObject<int?>(respstr);

                if (cou > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('แก้ไขข้อมูลสำเร็จ');", true);
                    LoadPromotionDetail();
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

            APIpath = APIUrl + "/api/support/CountPromotionDetailListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = "FullCombo";

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

        protected Boolean DeletePromotionDetail()
        {

            for (int i = 0; i < gvPromotion.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvPromotion.Rows[i].FindControl("chkPromotion");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvPromotion.Rows[i].FindControl("hidPromotionDetailInfoId");

                    if (Codelist != "")
                    {
                        Codelist += "," + hidCode.Value + "";
                    }
                    else
                    {
                        Codelist += "" + hidCode.Value + "";
                    }

                }
            }

            if (Codelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeletePromtoionDetailInfoByIdString";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["PromotionCode"] = Codelist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);

                int? cou2 = 0;
                int? cou3 = 0;
                string respstr2 = "";

                string APIpath2 = APIUrl + "/api/support/DeleteMainPromotionDetailByCode";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["SubMainIdDelete"] = Codelist;


                    var response2 = wb.UploadValues(APIpath2, "POST", data);

                    respstr2 = Encoding.UTF8.GetString(response2);
                }
                cou2 = JsonConvert.DeserializeObject<int?>(respstr2);

                string respstr3 = "";

                string APIpath3 = APIUrl + "/api/support/DeleteSubExchangePromotionDetailByCode";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["SubMainIdDelete"] = Codelist;


                    var response3 = wb.UploadValues(APIpath2, "POST", data);

                    respstr3 = Encoding.UTF8.GetString(response3);
                }
                cou3 = JsonConvert.DeserializeObject<int?>(respstr3);


            }
            else
            {
                hidIdList.Value = "";
                return false;
            }

            hidIdList.Value = "";
            return true;

        }

        protected Boolean AddPromotionDetail()
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

                    APIpath = APIUrl + "/api/support/InsertPromotionDetail";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["PromotionCode"] = txtPromotionCode.Text;
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

        protected void gvPromotionDetail_Change(object sender, GridViewPageEventArgs e)
        {
            gvPromotion.PageIndex = e.NewPageIndex;

            List<PromotionDetailInfo> lPromotionDetailInfo = new List<PromotionDetailInfo>();

            lPromotionDetailInfo = GetPromotionDetailMasterByCriteria();

            gvPromotion.DataSource = lPromotionDetailInfo;

            gvPromotion.DataBind();

        }

        public List<PromotionDetailInfo> GetPromotionDetailMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProducttionDetailByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = "FullCombo";

                data["ProductCode"] = txtSearchProductCode.Text;

                data["ProductName"] = txtSearchProductName.Text;

                data["ChannelCode"] = ddlSearchChannelCode.SelectedValue;

                

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionDetailInfo> lPromotionDetailInfo = JsonConvert.DeserializeObject<List<PromotionDetailInfo>>(respstr);


            return lPromotionDetailInfo;

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
            LoadPromotionDetail();

        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            ddlSearchChannelCode.SelectedValue = "-99";
            
            txtSearchProductCode.Text = "";
            txtSearchProductName.Text = "";
        }

        protected void btnAddPromotion_Click(object sender, EventArgs e)
        {
            hidFlagInsert.Value = "True";


            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Product').modal();", true);

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeletePromotionDetail();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            isinsert = AddPromotionDetail();

            btnSearch_Click(null, null);

            if (!isinsert)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการเพิ่ม');", true);
            }

        }

        protected void chkPromotionDetailAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvPromotion.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvPromotion.HeaderRow.FindControl("chkPromotionAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvPromotion.Rows[i].FindControl("hidPromotionId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkPromotion = (CheckBox)gvPromotion.Rows[i].FindControl("chkPromotion");

                    chkPromotion.Checked = true;
                }
                else
                {

                    CheckBox chkPromotion = (CheckBox)gvPromotion.Rows[i].FindControl("chkPromotion");

                    chkPromotion.Checked = false;
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


            LoadPromotion();
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
            List<PromotionDetailInfo> promotionDetails = LoadPromotionDetailNopaging();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                HiddenField itemCode = (HiddenField)e.Row.FindControl("hidProductCode");

                var avail = promotionDetails.Find(x => x.ProductCode == itemCode.Value);
                if (avail != null)
                {

                    HiddenField detailId = (HiddenField)e.Row.FindControl("hidPromotionDetailId");
                    TextBox txtprice = (TextBox)e.Row.FindControl("txtPrice_Ins");
                    TextBox txtqty = (TextBox)e.Row.FindControl("txtQty_Ins");
                    LinkButton addBtn = (LinkButton)e.Row.FindControl("btnEdit");

                    txtprice.Text = avail.Price.ToString();
                    detailId.Value = avail.PromotionDetailInfoId.ToString();
                    txtqty.Text = avail.DefaultAmount.ToString();


                    addBtn.CommandName = "UpdateDetail";

                }
                else
                {
                    TextBox txtqty = (TextBox)e.Row.FindControl("txtQty_Ins");

                    txtqty.Text = "1";
                }


            }




        }

        protected void gvProduct_RowCreated(object sender, GridViewRowEventArgs e)
        {
            Label priceLabel = (Label)e.Row.FindControl("lblHeaderPrice");

            if (hidPromotionTypeCode.Text == "02" || hidPromotionTypeCode.Text == "11")
            {
                gvProduct.Columns[3].Visible = true;
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Label lbl = (Label)e.Row.FindControl("lblHeaderPrice");
                    lbl.Text = "Discount Price";

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

            string pCode = hidProductBrandCode.Value == "MK0001" ? "MK000035" : "YAYOI000002";

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

                    APIpath = APIUrl + "/api/support/InsertPromotionDetailCombo";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["CombosetCode"] = txtCombosetCode_Ins.Text;
                        data["ProductCode"] = pCode;
                        data["PromotionDetailName"] = txtCombosetName_Ins.Text;
                        data["PromotionCode"] = txtPromotionCode.Text;
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

                        LoadPromotionDetail();

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

                    APIpath = APIUrl + "/api/support/UpdatePromotionDetailCombo";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["CombosetId"] = hidIdList.Value;

                        data["CombosetCode"] = txtCombosetCode_Ins.Text;
                        data["PromotionDetailName"] = txtCombosetName_Ins.Text;
                        data["PromotionCode"] = txtPromotionCode.Text;
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

                        LoadPromotionDetail();

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

        protected void gvPromotion_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (hidFlagComboset.Text == "Y")
            {
                gvPromotion.Columns[1].Visible = true;
                gvPromotion.Columns[2].Visible = true;
                gvPromotion.Columns[3].Visible = false;
                gvPromotion.Columns[4].Visible = false;
                gvPromotion.Columns[5].Visible = false;
                gvPromotion.Columns[7].Visible = false;
                gvPromotion.Columns[8].Visible = false;

                
            }
            else
            {
                gvPromotion.Columns[1].Visible = false;
                gvPromotion.Columns[2].Visible = false;
                gvPromotion.Columns[3].Visible = true;
                gvPromotion.Columns[4].Visible = true;
                gvPromotion.Columns[5].Visible = true;
                gvPromotion.Columns[7].Visible = true;
                gvPromotion.Columns[8].Visible = true;
                
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
            return "<a href=\"PromotionDetail.aspx?PromotionCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        protected string GetLinktoCombo(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"CombosetDetail.aspx?CombosetCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Promotion.aspx");
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            
            printPDF();
        }

        public void printPDF()
        {
            

            // Absolute path to your PDF to print (with filename)
            
            string Filepath = @"C:\Users\DoubleP\Documents\testpdf.pdf";
            // The name of the PDF that will be printed (just to be shown in the print queue)
            string Filename = "testpdf";
            // The name of the printer that you want to use
            // Note: Check step 1 from the B alternative to see how to list
            // the names of all the available printers with C#
            //string PrinterName = "Brother MFC-9140CDN Printer";
            string PrinterName = "Brother MFC-J200 Printer";


            // Create an instance of the Printer
            IPrinter printer = new Printer();

            // Print the file
            printer.PrintRawFile(PrinterName, Filepath);
        }

        public void savePDF()
        {
            
            //set innerHTML for save to pdf
            StringWriter sw = new StringWriter();
            HtmlTextWriter w = new HtmlTextWriter(sw);
            receiptOrder.RenderControl(w);
            string s = sw.GetStringBuilder().ToString();

           //set path
            string Filepath = @"C:\TempTest\";

            PdfDocument pdf = PdfGenerator.GeneratePdf(s, PageSize.Statement);
            pdf.Save(Filepath + "document.pdf");
        }

        protected void btnSavePDF_Click(object sender, EventArgs e)
        {
            savePDF();
        }
    }
}