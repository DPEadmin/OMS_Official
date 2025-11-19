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

namespace DOMS_TSR.src.Point
{
    public partial class PromotionPointDetail : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        string Codelist = "";
        string EditFlag = "";
        Boolean isdelete;
        Boolean isinsert;
        string currentPromotionType;
        protected static int currentPageNumber;
        protected static int currentPdPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE_MEDIUM"]);
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
                    
                    hidMerchantCode.Value = merchantinfo.MerchantCode;
                    
                    ((DropDownList)Master.FindControl("ddlMerchant")).Enabled = false;

                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }


                LoadPromotion();
                LoadPromotionDetail();
                LoadPromotionImages();
                loadPromotionCombo();
                LoadProduct();
                
                CheckComboFlag();
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
        protected void ddlProcomPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPdPageNumber = Int32.Parse(ddlProcom.SelectedValue);

            LoadProduct();
        }

        #region Function



        protected void LoadPromotion()
        {
            List<PromotionInfo> lPromotionInfo = new List<PromotionInfo>();

            lPromotionInfo = GetPromotionMasterByCriteria();

            if (lPromotionInfo.Count > 0)
            {
                txtPromotionCode.Text = lPromotionInfo[0].PromotionCode;

                txtPromotionName.Text = lPromotionInfo[0].PromotionName;

                txtPromotionTypeName.Text = lPromotionInfo[0].PromotionTypeName;

                txtFreeShippingFlag.Text = lPromotionInfo[0].FreeShippingName;

                hidPromotionTypeCode.Text = lPromotionInfo[0].PromotionTypeCode;
                if (lPromotionInfo[0].PromotionTypeCode == StaticField.PromotionTypeCode14) 
                {
                    btnAddCombo.Visible = true;
                }
                else
                {
                    btnAddCombo.Visible = false;
                }
                currentPromotionType = lPromotionInfo[0].PromotionTypeCode;
                hidPromotionType.Value = lPromotionInfo[0].PromotionTypeCode;
                txtPromotionStatusName.Text = lPromotionInfo[0].PromotionStatusName;

                txtDescription.InnerText = lPromotionInfo[0].PromotionDesc;

                hidDiscountAmount.Value = lPromotionInfo[0].DiscountAmount.ToString();

                hidDiscountPercent.Value = lPromotionInfo[0].DiscountPercent.ToString();
                hidProductDiscountAmount.Value = lPromotionInfo[0].ProductDiscountAmount.ToString();
                hidProductDiscountPercent.Value = lPromotionInfo[0].ProductDiscountPercent.ToString();
                

                // muti brand
                
                hidProductBrandCode.Value = "";
                if (lPromotionInfo[0].CompanyCode == "" || lPromotionInfo[0].CompanyCode == null)
                {

                }
                hidProductCompanyCode.Value = lPromotionInfo[0].CompanyCode.ToString();

                txtCompanyNameEN.Text = lPromotionInfo[0].CompanyNameEN.ToString();
                hidFlagComboset.Text = lPromotionInfo[0].CombosetFlag.Trim();

                txtLockCheckbox.Text = lPromotionInfo[0].LockCheckbox.Trim() == "Y" ? "ใช่" : "ไม่ใช่";

                txtLockAmountFlag.Text = lPromotionInfo[0].LockAmountFlag.Trim() == "Y" ? "แก้ไขได้" : "แก้ไขไม่ได้";
                if (lPromotionInfo[0].StartDate != null || lPromotionInfo[0].EndDate != null)
                {
                    DateTime sDate = DateTime.Parse(lPromotionInfo[0].StartDate).Date;
                    txtStartDate.Text = sDate.ToString("dd/MM/yyyy");
                    DateTime eDate = DateTime.Parse(lPromotionInfo[0].EndDate).Date;
                    txtEndDate.Text = eDate.ToString("dd/MM/yyyy");
                }
                else
                {
                    txtStartDate.Text = "";
                    txtEndDate.Text = "";
                }






                if (lPromotionInfo[0].DiscountAmount.ToString() == "0" && lPromotionInfo[0].ProductDiscountAmount.ToString() == "0" &&
                    lPromotionInfo[0].ProductDiscountPercent.ToString() == "0" && lPromotionInfo[0].DiscountPercent.ToString() != "0")
                {
                    txtDiscount.Text = lPromotionInfo[0].DiscountPercent.ToString() + "%";
                }
                else if (lPromotionInfo[0].DiscountPercent.ToString() == "0" && lPromotionInfo[0].ProductDiscountAmount.ToString() == "0" &&
                    lPromotionInfo[0].ProductDiscountPercent.ToString() == "0" && lPromotionInfo[0].DiscountAmount.ToString() != "0")
                {
                    txtDiscount.Text = lPromotionInfo[0].DiscountAmount.ToString() + " บาท";
                }
                else if (lPromotionInfo[0].DiscountPercent.ToString() == "0" && lPromotionInfo[0].ProductDiscountAmount.ToString() == "0" &&
                   lPromotionInfo[0].DiscountAmount.ToString() == "0" && lPromotionInfo[0].ProductDiscountPercent.ToString() != "0")
                {
                    txtDiscount.Text = lPromotionInfo[0].ProductDiscountPercent + "%";
                }
                else if (lPromotionInfo[0].ProductDiscountPercent.ToString() == "0" && lPromotionInfo[0].DiscountPercent.ToString() == "0" &&
                   lPromotionInfo[0].DiscountAmount.ToString() == "0" && lPromotionInfo[0].ProductDiscountAmount.ToString() != "0")
                {
                    txtDiscount.Text = lPromotionInfo[0].ProductDiscountAmount.ToString() + " บาท";
                }
                else if (lPromotionInfo[0].PromotionTypeCode.ToString() == StaticField.PromotionTypeCode02) 
                {
                    txtDiscount.Text = "ส่วนลดตามรายการสินค้า";
                }
                else
                {
                    txtDiscount.Text = "ไม่มีส่วนลด";
                }
            }

        }
        protected void loadPromotionCombo()
        {
            List<CombosetInfo> lPromotionInfo = new List<CombosetInfo>();

            


            lPromotionInfo = GetPromotionComboMasterByCriteria();

            GvProCombo.DataSource = lPromotionInfo;

            GvProCombo.DataBind();


            

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

                data["PromotionCode"] = txtPromotionCode.Text;

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

        public List<CombosetInfo> GetPromotionComboMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCombosetByCriteriaMaster";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = "";


                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CombosetInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<CombosetInfo>>(respstr);


            return lPromotionInfo;

        }
        public List<PromotionInfo> GetPromotionMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = Request.QueryString["PromotionCode"];

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);

            if (lPromotionInfo.Count > 0)
            {
                foreach (var lpro in lPromotionInfo.ToList())
                {
                    hidPromotionSet.Value = lpro.LockCheckbox;
                }
            }

            return lPromotionInfo;

        }


        public List<PromotionImageInfo> GetPromotionImage()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/GetPromotionImageUrl";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = Request.QueryString["PromotionCode"];

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
            Label lblprice = (Label)row.FindControl("lblPrice");
            TextBox txtPrice_Ins = (TextBox)row.FindControl("txtPrice_Ins");
            TextBox txtQty_Ins = (TextBox)row.FindControl("txtQty_Ins");
            

            HiddenField hidProductId = (HiddenField)row.FindControl("hidPromotionId");
            HiddenField hidProductCode = (HiddenField)row.FindControl("hidProductCode");
            HiddenField hidProductName = (HiddenField)row.FindControl("hidProductName");
            HiddenField hidPrice = (HiddenField)row.FindControl("hidPrice");
            
            HiddenField hidDefaultAmount = (HiddenField)row.FindControl("hidDefaultAmount");
            
            HiddenField hidProductPrice = (HiddenField)row.FindControl("hidProductPrice");
            HiddenField detailId = (HiddenField)row.FindControl("hidPromotionDetailId");
            if (hidPromotionType.Value == "02")
            {
                hidDiscountAmount.Value = txtPrice_Ins.Text;
            }

            if (e.CommandName == "AddPromotionDetail")
            {
                if (Convert.ToInt32(txtQty_Ins.Text) > 0)
                {
                    hidFlagInsert.Value = "True";
                    string respstr = "";

                    APIpath = APIUrl + "/api/support/InsertPromotionDetail";

                    if (hidPromotionSet.Value.Trim() != "Y") // Promotion Product Insert
                    {
                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["PromotionCode"] = txtPromotionCode.Text;
                            data["ProductCode"] = hidProductCode.Value;
                            data["DefaultAmount"] = txtQty_Ins.Text;
                            data["Price"] = hidProductPrice.Value;
                            data["FlagDelete"] = "N";
                            data["DiscountPercent"] = hidDiscountPercent.Value;
                            data["DiscountAmount"] = hidDiscountAmount.Value;
                            data["ComplementaryAmount"] = "0";
                            data["CreateBy"] = hidEmpCode.Value;
                            




                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }
                    }
                    else // Promotion Set Insert
                    {
                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["PromotionCode"] = txtPromotionCode.Text;
                            data["ProductCode"] = hidProductCode.Value;
                            data["DefaultAmount"] = txtQty_Ins.Text;
                            data["Price"] = hidProductPrice.Value;
                            data["FlagDelete"] = "N";
                            data["DiscountPercent"] = "0";
                            data["DiscountAmount"] = "0";
                            data["ComplementaryAmount"] = "0";
                            data["CreateBy"] = hidEmpCode.Value;
                            




                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }
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

                    if (hidPromotionSet.Value.Trim() != "Y") // Promotion Product Insert
                    {
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
                    }
                    else // Promotion Set Insert
                    {
                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["PromotionCode"] = txtPromotionCode.Text;
                            data["ProductCode"] = hidProductCode.Value;
                            data["DefaultAmount"] = txtQty_Ins.Text;
                            data["Price"] = txtPrice_Ins.Text;
                            data["FlagDelete"] = "N";
                            data["DiscountPercent"] = "0";
                            data["DiscountAmount"] = "0";
                            data["ComplementaryAmount"] = "0";
                            data["CreateBy"] = hidEmpCode.Value;
                            
                            data["PromotionDeailInfoId"] = detailId.Value;




                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }
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

            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาระบุ Quantity มากกว่า 0');", true);
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

            APIpath = APIUrl + "/api/support/CountPromotionDetailListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = txtPromotionCode.Text;

                data["ProductCode"] = txtSearchProductCode.Text;

                data["ProductName"] = txtSearchProductName.Text;

                

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
                    Label lblid = (Label)gvPromotion.Rows[i].FindControl("LBLid");
                    HiddenField hidPromotionDetailInfoId = (HiddenField)gvPromotion.Rows[i].FindControl("hidPromotionDetailInfoId");
                    if (Codelist != "")
                    {
                        if (hidFlagComboset.Text == "Y")
                        {
                            Codelist += "," + lblid.Text + "";
                        }
                        else
                        {
                            Codelist += "," + hidPromotionDetailInfoId.Value + "";
                        }

                    }
                    else
                    {
                        if (hidFlagComboset.Text == "Y")
                        {
                            Codelist += "" + lblid.Text + "";
                        }
                        else
                        {
                            Codelist += "" + hidPromotionDetailInfoId.Value + "";
                        }

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

                data["CompanyCode"] = hidProductCompanyCode.Value;

                data["ProductCode"] = txtSearchProductCode.Text;
                data["FlagPointCoupon"] = "Y";
                data["ProductName"] = txtSearchProductName.Text;
                data["MerchantCode"] = hidMerchantCode.Value;

                

                data["ProductNotInPromotionCode"] = txtPromotionCode.Text;

                

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

                data["PromotionCode"] = txtPromotionCode.Text;

                data["ProductCode"] = txtSearchProductCode.Text;

                data["ProductName"] = txtSearchProductName.Text;

                

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

                data["CompanyCode"] = hidProductCompanyCode.Value;

                data["ProductCode"] = txtSearchModalProductCode.Text;
                data["FlagPointCoupon"] = "Y";
                data["ProductName"] = txtSearchModalProductName.Text;
                data["MerchantCode"] = hidMerchantCode.Value;

                

                data["ProductNotInPromotionCode"] = txtPromotionCode.Text;

                

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
            LoadPromotionDetail();

        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            
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
        protected void btnAddSetItemPromotion_Click(object sender, EventArgs e)
        {


            for (int i = 0; i < gvPromotion.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvPromotion.Rows[i].FindControl("chkItemType");

                if (checkbox.Checked == true)
                {
                    Label lblid = (Label)gvPromotion.Rows[i].FindControl("LBLid");
                    HiddenField hidPromotionDetailInfoId = (HiddenField)gvPromotion.Rows[i].FindControl("hidPromotionDetailInfoId");

                    if (Codelist != "")
                    {
                        if (hidFlagComboset.Text == "Y")
                        {
                            Codelist += "," + lblid.Text + "";
                        }
                        else
                        {
                            Codelist += "," + lblid.Text + "";
                        }

                    }
                    else
                    {
                        if (hidFlagComboset.Text == "Y")
                        {
                            Codelist += "" + lblid.Text + "";
                        }
                        else
                        {
                            Codelist += "" + lblid.Text + "";
                        }

                    }

                }
            }

            if (Codelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/UpdateItemTypePromotionDetailInfoByIdString";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["PromotionCode"] = Request.QueryString["PromotionCode"]; ;

                    data["PromotionDetailIDList"] = Codelist;
                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);
                if (cou > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('เซ๊ตรายการสินค้าหลักสำเร็จ');", true);
                }

            }
        }
        protected void chkPromotionDetailAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvPromotion.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvPromotion.HeaderRow.FindControl("chkPromotionDetailAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvPromotion.Rows[i].FindControl("hidPromotionDetailInfoId");

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

            LoadPromotionDetail();
            
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
            string ProdisPer = hidProductDiscountPercent.Value;
            string ProdisAmont = hidProductDiscountAmount.Value;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField itemCode = (HiddenField)e.Row.FindControl("hidProductCode");
                HiddenField hidProductPrice = (HiddenField)e.Row.FindControl("hidProductPrice");

                var avail = promotionDetails.Find(x => x.ProductCode == itemCode.Value);
                if (avail != null)
                {
                    HiddenField detailId = (HiddenField)e.Row.FindControl("hidPromotionDetailId");
                    TextBox txtqty = (TextBox)e.Row.FindControl("txtQty_Ins");
                    LinkButton addBtn = (LinkButton)e.Row.FindControl("btnEdit");

                    detailId.Value = avail.PromotionDetailInfoId.ToString();
                    txtqty.Text = avail.DefaultAmount.ToString();


                    addBtn.CommandName = "UpdateDetail";

                }
                else
                {
                    TextBox txtqty = (TextBox)e.Row.FindControl("txtQty_Ins");

                    txtqty.Text = "1";
                    if(currentPromotionType == StaticField.PromotionTypeCode00) 
                    {
                        txtqty.Enabled = false;
                    }
                }


                TextBox txtPrice_Ins = (TextBox)e.Row.FindControl("txtPrice_Ins");

                if (hidPromotionTypeCode.Text == StaticField.PromotionTypeCode02 || hidPromotionTypeCode.Text == StaticField.PromotionTypeCode11) 
                {
                    txtPrice_Ins.Text = hidProductPrice.Value.ToString();
                    txtPrice_Ins.TextMode = TextBoxMode.Number;

                    txtPrice_Ins.Visible = true;
                }
                else if (hidPromotionTypeCode.Text == StaticField.PromotionTypeCode03 || hidPromotionTypeCode.Text == StaticField.PromotionTypeCode08) //คำนวนอัตโนมัติ
                {
                    if (ProdisPer.ToString() != "0")
                    {
                        
                        txtPrice_Ins.Text = (decimal.Parse(hidProductPrice.Value.ToString()) - ((decimal.Parse(ProdisPer.ToString())) * (decimal.Parse(hidProductPrice.Value.ToString())) / 100)).ToString();
                    }
                    else if (ProdisAmont.ToString() != "0")
                    {
                        txtPrice_Ins.Text = ((decimal.Parse(hidProductPrice.Value.ToString()) - decimal.Parse(ProdisAmont.ToString()))).ToString();
                    }
                    else
                    {
                        txtPrice_Ins.Text = hidProductPrice.Value.ToString();
                    }
                    if (decimal.Parse(txtPrice_Ins.Text) < 0)
                    {
                        txtPrice_Ins.Text = "0";
                    }
                    txtPrice_Ins.Enabled = false;
                }
                else if
                    (hidPromotionTypeCode.Text == StaticField.PromotionTypeCode01 || hidPromotionTypeCode.Text == StaticField.PromotionTypeCode15 || hidPromotionTypeCode.Text == StaticField.PromotionTypeCode06 || hidPromotionTypeCode.Text == StaticField.PromotionTypeCode07
                    || hidPromotionTypeCode.Text == StaticField.PromotionTypeCode09) //ปกติ
                {
                    txtPrice_Ins.Text = hidProductPrice.Value.ToString();
                    if (decimal.Parse(txtPrice_Ins.Text) < 0)
                    {
                        txtPrice_Ins.Text = "0";
                    }
                    txtPrice_Ins.Enabled = false;
                    txtPrice_Ins.Visible = true;
                }

            }




        }

        protected void gvProduct_RowCreated(object sender, GridViewRowEventArgs e)
        {
            Label priceLabel = (Label)e.Row.FindControl("lblHeaderPrice");

            if (hidPromotionTypeCode.Text == StaticField.PromotionTypeCode02 || hidPromotionTypeCode.Text == StaticField.PromotionTypeCode11 || hidPromotionTypeCode.Text == StaticField.PromotionTypeCode04 || hidPromotionTypeCode.Text == StaticField.PromotionTypeCode03
                || hidPromotionTypeCode.Text == StaticField.PromotionTypeCode12
                || hidPromotionTypeCode.Text == StaticField.PromotionTypeCode10
                || hidPromotionTypeCode.Text == StaticField.PromotionTypeCode17 || hidPromotionTypeCode.Text == StaticField.PromotionTypeCode16 || hidPromotionTypeCode.Text == StaticField.PromotionTypeCode08) 
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
            if (e.CommandName == "AddPromotionComboDetail")
            {

                hidFlagInsert.Value = "True";
                string respstr = "";

                APIpath = APIUrl + "/api/support/InsertPromotionDetailCombo";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CombosetCode"] = hidCombosetCode.Value;

                    data["PromotionDetailName"] = hidCombosetName.Value;
                    data["PromotionCode"] = Request.QueryString["PromotionCode"];
                    data["Price"] = lblPrice.Text;
                    data["FlagDelete"] = "N";


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                cou = JsonConvert.DeserializeObject<int?>(respstr);
                if (cou > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('เพิ่มรายการสำเร็จ');", true);
                    LoadPromotion();
                    loadPromotionCombo();
                    LoadPromotionDetail();
                    

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
                gvPromotion.Columns[8].Visible = false;
                
                if (hidPromotionTypeCode.Text == "06")
                {

                    gvPromotion.Columns[10].Visible = true;
                }
                else
                {
                    gvPromotion.Columns[10].Visible = false;
                }
               
            }
          
        }

        protected void btnAddCombo_Click(object sender, EventArgs e)
        {

            hidFlagInsertCombo.Value = "True";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Comboset').modal();", true);
        }

        protected void btnCreatePromotionWF_Click(object sender, EventArgs e)
        {
            String promotioncode = Request.QueryString["PromotionCode"];

            StartWorkFlow(promotioncode, "1", "1", "Submit");
        }

        #endregion

        #region Binding

        

        protected string GetLinktoCombo(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"CombosetDetail.aspx?CombosetCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            String UrlShowAddComplementary = Request.QueryString["ComplementaryFlag"];

            if (UrlShowAddComplementary == "Y")
            {
                Response.Redirect("PromotionComplementaryManagement.aspx");
            }
            else
            {
                Response.Redirect("PromotionPointManagement.aspx");
            }
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


            

        }





        #endregion

        #region start k2 work flow
        public void StartWorkFlow(string promotioncode, string OStatus, string ostate, string eventsubmit)
        {
            EmpInfo empInfo = new EmpInfo();
            int? promotionid = 0;
            string promotiondesc = "";

            List<PromotionInfo> lpromoInfo = new List<PromotionInfo>();
            lpromoInfo = GetPromotionIDByCritreria(promotioncode);

            if (lpromoInfo.Count > 0)
            {
                promotionid = lpromoInfo[0].PromotionId;
                promotiondesc = lpromoInfo[0].PromotionDesc;
            }

            empInfo = (EmpInfo)Session["EmpInfo"];
            APIpath = ConfigurationManager.AppSettings["K2API"];
            string userName = ConfigurationManager.AppSettings["K2User"];
            string passWord = ConfigurationManager.AppSettings["K2Password"];
            using (var client = new WebClient())
            {

                System.Net.ServicePointManager.ServerCertificateValidationCallback += (send, certificate, chain, sslPolicyErrors) => { return true; };

                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(userName + ":" + passWord));
                client.Headers[HttpRequestHeader.Authorization] = string.Format(
                    "Basic {0}", credentials);

                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var olist = new k2Info();
                olist.folio = promotioncode;
                olist.expectedDuration = "86400";
                olist.priority = "1";
                
                olist.dataFields.PromotionID = promotionid;
                olist.dataFields.Event = "Submit";
                olist.dataFields.Actor = hidEmpCode.Value;
                olist.dataFields.Remark = promotiondesc;
                var jsonObj = JsonConvert.SerializeObject(olist);
                var dataString = client.UploadString(APIpath, jsonObj);

                

                if (dataString != null || dataString != "")
                {
                    

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('สร้าง Promotion สำเร็จ');window.location ='../WorkList/PromotionWorkList.aspx';", true);
                }
            }

        }

        public void InsertWorkFlow(string promotioncode, string OStatus, string ostate, string eventsubmit)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];
            APIpath = APIUrl + "/api/support/InsertK2Workflow";

            using (var client = new WebClient())
            {

                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var K2_OrderWF = new K2_OrderWFInfo();
                
                K2_OrderWF.CreateBy = empInfo.EmpCode;
                var jsonObj = JsonConvert.SerializeObject(K2_OrderWF);
                var dataString = client.UploadString(APIpath, jsonObj);
            }
        }

        protected List<PromotionInfo> GetPromotionIDByCritreria(string promotioncode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = promotioncode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lpromotionInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);

            return lpromotionInfo;
        }
        #endregion
    }
}