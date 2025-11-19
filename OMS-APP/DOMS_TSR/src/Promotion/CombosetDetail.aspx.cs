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
    public partial class CombosetDetail : System.Web.UI.Page
    {
        protected static string APIUrl;

        string Codelist = "";
        string EditFlag = "";
        Boolean isdelete;
        Boolean isinsert;
        protected static int currentPageNumber;
        protected static int currentPageNumber_MainModal;
        protected static int currentPageNumber_SubPd;
        protected static int currentPageNumber_SubPdModal;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;
                currentPageNumber_MainModal = 1;
                currentPageNumber_SubPd = 1;
                currentPageNumber_SubPdModal = 1;

                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                    APIUrl = empInfo.ConnectionAPI;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }


                LoadComboset();
                LoadMainExchangProductDetail();
                LoadSubExchangProductDetail();
                LoadPromotionImages();
                LoadMainProduct();
                LoadSubProduct();
            }

        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadMainExchangProductDetail();
        }

        protected void ddlSubGvPdPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber_SubPd = Int32.Parse(ddlSubGvPdPage.SelectedValue);

            LoadSubExchangProductDetail();
        }

        #region Function



        protected void LoadComboset()
        {
            List<CombosetInfo> lPromotionInfo = new List<CombosetInfo>();

            lPromotionInfo = GetCombosetMasterByCriteria();
            if (lPromotionInfo.Count > 0)
            {
                txtCombosetCode.Text = lPromotionInfo[0].CombosetCode;

                txtPromotionCode.Text = lPromotionInfo[0].PromotionCode;

                txtPromotionName.Text = lPromotionInfo[0].PromotionName;

                txtCombosetName.Text = lPromotionInfo[0].CombosetName;

       

                txtCombosetPrice.Text = lPromotionInfo[0].CombosetPrice.ToString() + " บาท";

                txtPromotionDetailId.Text = Request.QueryString["CombosetId"];
            }
          

            

        }



        protected void LoadPromotionImages()
        {
            


        }


        public List<CombosetInfo> GetCombosetMasterByCriteria()
        {
            string respstr = "";
            
                  
            APIpath = APIUrl + "/api/support/ListCombosetNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CombosetCode"] = Request.QueryString["CombosetCode"];
                data["PromotionCode"] = Request.QueryString["PromotionCode"];

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CombosetInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<CombosetInfo>>(respstr);


            return lPromotionInfo;

        }


        public List<PromotionImageInfo> GetPromotionImage()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/GetPromotionImageUrl";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = txtPromotionCode.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionImageInfo> lProductInfo = JsonConvert.DeserializeObject<List<PromotionImageInfo>>(respstr);


            return lProductInfo;

        }

        protected void LoadMainExchangProductDetail() {

            List<SubMainPromotionDetailInfo> lPromotionInfo = new List<SubMainPromotionDetailInfo>();

            



            lPromotionInfo = GetMainExchangeProductMasterByCriteria();
            
            int? totalRow = CountPromodetailMasterList();

            SetPageBar(Convert.ToDouble(totalRow));

            gvPromotion.DataSource = lPromotionInfo;

            gvPromotion.DataBind();
        }
        protected void LoadSubExchangProductDetail()
        {

            List<SubProductPromotionDetailInfo> lPromotionInfo = new List<SubProductPromotionDetailInfo>();

            

            int? totalRow = CountSubPromodetailMasterList();

            SetPageBarSubProduct(Convert.ToDouble(totalRow));


            lPromotionInfo = GetSubExchangProductMasterByCriteria();

            GridMainSubexchang.DataSource = lPromotionInfo;

            GridMainSubexchang.DataBind();
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

            HiddenField hidProductId = (HiddenField)row.FindControl("hidProductId");
            HiddenField hidProductCode = (HiddenField)row.FindControl("hidProductCode");
            HiddenField hidProductName = (HiddenField)row.FindControl("hidProductName");
            HiddenField hidPrice = (HiddenField)row.FindControl("hidProductPrice");
          
            HiddenField hidDefaultAmount = (HiddenField)row.FindControl("hidDefaultAmount");
         
            HiddenField hidProductPrice = (HiddenField)row.FindControl("hidProductPrice");


            if (e.CommandName == "AddPromotionDetail")
            {


               

                hidFlagInsert.Value = "True";
                string respstr = "";

                APIpath = APIUrl + "/api/support/InsertSubMainPromotionDetail";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["CombosetCode"] = Request.QueryString["CombosetCode"];
                    
                    data["ProductCode"] = hidProductCode.Value.ToString();
                    data["PromotionDetailID"] = Request.QueryString["CombosetId"];;
                    data["Amount"] = txtQty_Ins.Text;
                   
                    data["FlagSubPromotionDetailMain"] = "MainProduct";
                    data["FlagDelete"] = "N";
                    data["CreateBy"] = hidEmpCode.Value;                
                   




                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                cou = JsonConvert.DeserializeObject<int?>(respstr);
                LoadMainExchangProductDetail();
                LoadSubExchangProductDetail();
                LoadMainProduct();

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-product').modal('hide');", true);

            }

        }
        protected void gvPromotion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvPromotion.Rows[index];
            int? cou = 0;


            

            HiddenField HidSubMainId = (HiddenField)row.FindControl("HidSubMainId");
            
            if (e.CommandName == "addSub")
            {
                GVSubExchang.Visible = true;
                LbMainId.Text = HidSubMainId.Value.ToString();
                LoadSubExchangProductDetail();
            }

        }
        protected void gvSubProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = GridSubProduct.Rows[index];
            int? cou = 0;

            Label lblmsg = (Label)row.FindControl("lblmsg");

            TextBox txtPrice_Ins = (TextBox)row.FindControl("txtPrice_Ins");
            TextBox txtQty_Ins = (TextBox)row.FindControl("txtQty_Ins");

            HiddenField hidSubProductId = (HiddenField)row.FindControl("hidSubProductId");
            HiddenField hidSubProductCode = (HiddenField)row.FindControl("hidSubProductCode");
           
           

            if (e.CommandName == "AddPromotionDetail")
            {




                hidFlagInsert.Value = "True";
                string respstr = "";

                APIpath = APIUrl + "/api/support/InsertSubExchangePromotionDetail";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["CombosetCode"] = Request.QueryString["CombosetCode"];
                    
                    data["ProductCode"] = hidSubProductCode.Value.ToString();
                    data["PromotionDetailID"] = Request.QueryString["CombosetId"];;
                    data["Amount"] = txtQty_Ins.Text;
                    
                    data["FlagSubPromotionDetailExchange"] = "ExchangeProduct";
                    data["FlagDelete"] = "N";
                    data["CreateBy"] = hidEmpCode.Value;
                    data["SubMainExchangeID"] = LbMainId.Text;





                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                cou = JsonConvert.DeserializeObject<int?>(respstr);

                LoadMainExchangProductDetail();
                LoadSubExchangProductDetail();
                LoadSubProduct();

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-product').modal('hide');", true);

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

        public int? CountPromodetailMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountMainExchangProductbyCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CombosetCode"] = Request.QueryString["CombosetCode"];

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }

        public int? CountProductMasterList()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/CountProductMasterListByCriteria";
            

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = txtPromotionCode.Text;

                data["ProductCode"] = txtSearchProductCode.Text;

                data["ProductName"] = txtSearchProductName.Text;

                data["MerchantCode"] = txtSearchMerchantCode.Text;

                data["MerchantName"] = txtSearchMerchantName.Text;

                data["ProductBrandCode"] = txtProductBrandCode.Text;

                data["rowOFFSet"] = ((currentPageNumber_MainModal - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }

        public int? CountSubProductMasterList()
        {
            string respstr = "";

            
            APIpath = APIUrl + "/api/support/CountProductMasterListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CombosetCode"] = Request.QueryString["CombosetCode"];

                data["ProductCode"] = txtSearchProductCode.Text;

                data["ProductName"] = txtSearchProductName.Text;

                data["MerchantCode"] = txtSearchMerchantCode.Text;

                data["MerchantName"] = txtSearchMerchantName.Text;

                data["ProductBrandCode"] = txtProductBrandCode.Text;

                data["rowOFFSet"] = ((currentPageNumber_SubPdModal - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }

        public int? CountSubPromodetailMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountSubExchangeProductDetailbyCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CombosetCode"] = Request.QueryString["CombosetCode"];
                data["SubMainExchangeID"] = LbMainId.Text;

                data["rowOFFSet"] = ((currentPageNumber_SubPd - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }

        public int? CountMainComboDetailMaster()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountProductListInPromotion";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = txtPromotionCode.Text;

                data["ProductCode"] = txtSearchProductCode.Text;

                data["ProductName"] = txtSearchProductName.Text;

                data["MerchantCode"] = txtSearchMerchantCode.Text;

                data["MerchantName"] = txtSearchMerchantName.Text;

                data["ProductBrandCode"] = txtProductBrandCode.Text;
                
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
                CheckBox checkbox = (CheckBox)gvPromotion.Rows[i].FindControl("chkMainProduct");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvPromotion.Rows[i].FindControl("hidPromotionDetailId");

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

                APIpath = APIUrl + "/api/support/DeleteMainPromotionDetail";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["SubMainIdDelete"] = Codelist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);




            }
            else
            {
                hidIdList.Value = "";
                return false;
            }
            LoadMainExchangProductDetail();
            hidIdList.Value = "";
            return true;

        }
        protected Boolean DeleteSubExchangDetail()
        {

            for (int i = 0; i < GridMainSubexchang.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)GridMainSubexchang.Rows[i].FindControl("chkSubProduct");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)GridMainSubexchang.Rows[i].FindControl("hidSubExchngPromotionDetailId");

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

                APIpath = APIUrl + "/api/support/DeleteSubExchangePromotionDetail";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["SubMainIdDelete"] = Codelist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);




            }
            else
            {
                hidIdList.Value = "";
                return false;
            }

            GetSubExchangProductMasterByCriteria();
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

                    APIpath = APIUrl + "/api/support/InsertSubMainPromotionDetail";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["PromotionCode"] = txtPromotionCode.Text;
                        data["ProductCode"] = Request.QueryString["ProductCode"];
                        data["CombosetCode"] = txtCombosetCode.Text;
                        data["PromotionDetailID"] = Request.QueryString["CombosetId"];;
                        data["Amount"] = txtQty_Ins.Text;
                        data["Price"] = txtPrice_Ins.Text;
                        data["FlagSubPromotionDetailMain"] = "";                        
                        data["FlagDelete"] = "N";         
                        data["CreateBy"] = hidEmpCode.Value;

                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    cou = JsonConvert.DeserializeObject<int?>(respstr);

                }
            }

            if (cou != 0)
            {
                LoadMainExchangProductDetail();
              

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-product').modal('hide');", true);
                


                hidIdList.Value = "";
                return true;

            }
            else
            {
                hidIdList.Value = "";
                return false;
            }

           

        }

        

        protected void SetPageBarSubProductSel(double totalRow)
        {

            lblTotalPages_SubPdModal.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlSubPdModalPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages_SubPdModal.Text) + 1; i++)
            {
                ddlSubPdModalPage.Items.Add(new ListItem(i.ToString()));
            }
            setDDl(ddlSubPdModalPage, currentPageNumber_SubPdModal.ToString());
            

            
            if ((currentPageNumber_SubPdModal == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                btnSubPdSelFirst.Enabled = false;
                btnSubPdSelPre.Enabled = false;
                btnSubPdSelNext.Enabled = true;
                btnSubPdSelLast.Enabled = true;
            }
            else if ((currentPageNumber_SubPdModal.ToString() == lblTotalPages_SubPdModal.Text) && (currentPageNumber_SubPdModal == 1))
            {
                btnSubPdSelFirst.Enabled = false;
                btnSubPdSelPre.Enabled = false;
                btnSubPdSelNext.Enabled = false;
                btnSubPdSelLast.Enabled = false;
            }
            else if ((currentPageNumber_SubPdModal.ToString() == lblTotalPages_SubPdModal.Text) && (currentPageNumber_SubPdModal > 1))
            {
                btnSubPdSelFirst.Enabled = true;
                btnSubPdSelPre.Enabled = true;
                btnSubPdSelNext.Enabled = false;
                btnSubPdSelLast.Enabled = false;
            }
            else
            {
                btnSubPdSelFirst.Enabled = true;
                btnSubPdSelPre.Enabled = true;
                btnSubPdSelNext.Enabled = true;
                btnSubPdSelLast.Enabled = true;
            }
            
        }

        protected void ddlSubPdSelPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber_SubPdModal = Int32.Parse(ddlSubPdModalPage.SelectedValue);

            LoadSubProduct();
        }

        protected void SetPageBarMainProductSel(double totalRow)
        {

            lblTotalPages_MainPdModal.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlMainPdModalPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages_MainPdModal.Text) + 1; i++)
            {
                ddlMainPdModalPage.Items.Add(new ListItem(i.ToString()));
            }
            setDDl(ddlMainPdModalPage, currentPageNumber_MainModal.ToString());
            

            
            if ((currentPageNumber_MainModal == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                btnMainPdSelFirst.Enabled = false;
                btnMainPdSelPre.Enabled = false;
                btnMainPdSelNext.Enabled = true;
                btnMainPdSelLast.Enabled = true;
            }
            else if ((currentPageNumber_MainModal.ToString() == lblTotalPages_MainPdModal.Text) && (currentPageNumber_MainModal == 1))
            {
                btnMainPdSelFirst.Enabled = false;
                btnMainPdSelPre.Enabled = false;
                btnMainPdSelNext.Enabled = false;
                btnMainPdSelLast.Enabled = false;
            }
            else if ((currentPageNumber_MainModal.ToString() == lblTotalPages_MainPdModal.Text) && (currentPageNumber_MainModal > 1))
            {
                btnMainPdSelFirst.Enabled = true;
                btnMainPdSelPre.Enabled = true;
                btnMainPdSelNext.Enabled = false;
                btnMainPdSelLast.Enabled = false;
            }
            else
            {
                btnMainPdSelFirst.Enabled = true;
                btnMainPdSelPre.Enabled = true;
                btnMainPdSelNext.Enabled = true;
                btnMainPdSelLast.Enabled = true;
            }
            
        }

        protected void ddlMainPdModalPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber_MainModal = Int32.Parse(ddlMainPdModalPage.SelectedValue);

            LoadMainProduct();
        }

        protected void SetPageBarSubProduct(double totalRow)
        {

            lblTotalPages_GvSubPd.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlSubGvPdPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages_GvSubPd.Text) + 1; i++)
            {
                ddlSubGvPdPage.Items.Add(new ListItem(i.ToString()));
            }
            setDDl(ddlSubGvPdPage, currentPageNumber_SubPd.ToString());
            

            
            if ((currentPageNumber_SubPd == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                btnSubGvPdFirst.Enabled = false;
                btnSubGvPdPre.Enabled = false;
                btnSubGvPdNext.Enabled = true;
                btnSubGvPdLast.Enabled = true;
            }
            else if ((lblTotalPages_GvSubPd.ToString() == lblTotalPages_GvSubPd.Text) && (currentPageNumber_SubPd == 1))
            {
                btnSubGvPdFirst.Enabled = false;
                btnSubGvPdPre.Enabled = false;
                btnSubGvPdNext.Enabled = false;
                btnSubGvPdLast.Enabled = false;
            }
            else if ((currentPageNumber_SubPd.ToString() == lblTotalPages_GvSubPd.Text) && (currentPageNumber_SubPd > 1))
            {
                btnSubGvPdFirst.Enabled = true;
                btnSubGvPdPre.Enabled = true;
                btnSubGvPdNext.Enabled = false;
                btnSubGvPdLast.Enabled = false;
            }
            else
            {
                btnSubGvPdFirst.Enabled = true;
                btnSubGvPdPre.Enabled = true;
                btnSubGvPdNext.Enabled = true;
                btnSubGvPdLast.Enabled = true;
            }
            
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

            APIpath = APIUrl + "/api/support/ListSubExchangePromotionDetailbyCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CombosetCode"] = Request.QueryString["CombosetCode"];

                data["PromotionCode"] = txtPromotionCode.Text;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionDetailInfo> lPromotionDetailInfo = JsonConvert.DeserializeObject<List<PromotionDetailInfo>>(respstr);


            return lPromotionDetailInfo;

        }

        protected void LoadMainProduct()
        {

            List<ProductInfo> lProductInfo = new List<ProductInfo>();

            

            int? totalRow = CountProductMasterList();

            SetPageBarMainProductSel(Convert.ToDouble(totalRow));  
         
            lProductInfo = GetProductMasterByCriteria();

            gvProduct.DataSource = lProductInfo;
           
            gvProduct.DataBind();

            
           

        }
        protected void LoadSubProduct()
        {

            List<ProductInfo> lProductInfo = new List<ProductInfo>();

            

            int? totalRow = CountSubProductMasterList();

            SetPageBarSubProductSel(Convert.ToDouble(totalRow));


            lProductInfo = GetSubProductMasterByCriteria();

            GridSubProduct.DataSource = lProductInfo;

            GridSubProduct.DataBind();
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

                data["PromotionCode"] = txtPromotionCode.Text;

                data["ProductCode"] = txtSearchProductCode.Text;

                data["ProductName"] = txtSearchProductName.Text;

                data["MerchantCode"] = txtSearchMerchantCode.Text;

                data["MerchantName"] = txtSearchMerchantName.Text;

                data["ProductBrandCode"] = txtProductBrandCode.Text;

                data["rowOFFSet"] = ((currentPageNumber_MainModal - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);


            return lProductInfo;

        }

        public List<ProductInfo> GetSubProductMasterByCriteria()
        {
            string respstr = "";

            

            APIpath = APIUrl + "/api/support/ListProductMasterByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = txtPromotionCode.Text;

                data["ProductCode"] = txtSearchProductCode.Text;

                data["ProductName"] = txtSearchProductName.Text;

                data["MerchantCode"] = txtSearchMerchantCode.Text;

                data["MerchantName"] = txtSearchMerchantName.Text;

                data["ProductBrandCode"] = txtProductBrandCode.Text;

                data["rowOFFSet"] = ((currentPageNumber_SubPdModal - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);


            return lProductInfo;

        }

        public List<SubMainPromotionDetailInfo> GetMainExchangeProductMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListMainExchangProductPagingbyCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CombosetCode"] = Request.QueryString["CombosetCode"];

                

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<SubMainPromotionDetailInfo> lProductInfo = JsonConvert.DeserializeObject<List<SubMainPromotionDetailInfo>>(respstr);


            return lProductInfo;

        }
        public List<SubProductPromotionDetailInfo> GetSubExchangProductMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListSubExchangeProductDetailPagingbyCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CombosetCode"] = Request.QueryString["CombosetCode"];

                
                data["SubMainExchangeID"] = LbMainId.Text;

                data["rowOFFSet"] = ((currentPageNumber_SubPd - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<SubProductPromotionDetailInfo> lProductInfo = JsonConvert.DeserializeObject<List<SubProductPromotionDetailInfo>>(respstr);


            return lProductInfo;

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (LbMainId.Text != "")
            {
                LoadMainExchangProductDetail();
                LoadSubExchangProductDetail();
            }
            else
            {
                LoadSubExchangProductDetail();
            }
         

        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddPromotion_Click(object sender, EventArgs e)
        {
            hidFlagInsert.Value = "True";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Product').modal();", true);

        }
        protected void btnAddSubPromotion_Click(object sender, EventArgs e)
        {
            hidFlagInsert.Value = "True";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-SubProduct').modal();", true);

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
        protected void btnDeleteSub_Click(object sender, EventArgs e)
        {
            isdelete = DeleteSubExchangDetail();

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
                  LoadMainExchangProductDetail();
                LoadSubExchangProductDetail();
            if (!isinsert)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการเพิ่ม');", true);
            }

        }

        protected void chkMainProductAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvPromotion.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvPromotion.HeaderRow.FindControl("chkMainProductAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvPromotion.Rows[i].FindControl("hidProductCode");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkPromotion = (CheckBox)gvPromotion.Rows[i].FindControl("chkMainProduct");

                    chkPromotion.Checked = true;
                }
                else
                {

                    CheckBox chkPromotion = (CheckBox)gvPromotion.Rows[i].FindControl("chkMainProduct");

                    chkPromotion.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }
        protected void chkSubProductAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < GridMainSubexchang.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)GridMainSubexchang.HeaderRow.FindControl("chkSubProductAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidSubExchngProductCode = (HiddenField)GridMainSubexchang.Rows[i].FindControl("hidSubExchngProductCode");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidSubExchngProductCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidSubExchngProductCode.Value + "'";
                    }

                    CheckBox chkSubProduct = (CheckBox)GridMainSubexchang.Rows[i].FindControl("chkSubProduct");

                    chkSubProduct.Checked = true;
                }
                else
                {

                    CheckBox chkSubProduct = (CheckBox)GridMainSubexchang.Rows[i].FindControl("chkSubProduct");

                    chkSubProduct.Checked = false;
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


            LoadComboset();
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

        protected void GetPageIndex_SubPdModal(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber_SubPdModal = 1;
                    break;

                case "Previous":
                    currentPageNumber_SubPdModal = Int32.Parse(ddlSubPdModalPage.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber_SubPdModal = Int32.Parse(ddlSubPdModalPage.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber_SubPdModal = Int32.Parse(lblTotalPages_SubPdModal.Text);
                    break;
            }


            LoadSubProduct();
        }

        protected void GetPageIndex_MainPdModal(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber_MainModal = 1;
                    break;

                case "Previous":
                    currentPageNumber_MainModal = Int32.Parse(ddlMainPdModalPage.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber_MainModal = Int32.Parse(ddlMainPdModalPage.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber_MainModal = Int32.Parse(lblTotalPages_MainPdModal.Text);
                    break;
            }


            LoadMainProduct();
        }

        protected void GetPageIndex_GvSubPd(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber_SubPd = 1;
                    break;

                case "Previous":
                    currentPageNumber_SubPd = Int32.Parse(ddlSubGvPdPage.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber_SubPd = Int32.Parse(ddlSubGvPdPage.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber_SubPd = Int32.Parse(lblTotalPages_GvSubPd.Text);
                    break;
            }


            LoadMainProduct();
        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"CombosetDetail.aspx?PromotionCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Comboset.aspx");
        }




        #endregion

        
    }
}