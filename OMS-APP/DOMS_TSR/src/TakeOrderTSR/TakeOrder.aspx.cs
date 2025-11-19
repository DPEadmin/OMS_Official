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
using System.IO;

namespace DOMS_TSR.src.TakeOrderTSR
{
    public class OrderData
    {
        public string PromotionCode { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductOrderType { get; set; }
        public Double? Price { get; set; }
        public string Unit { get; set; }
        public int? Amount { get; set; }
        public int? DefaultAmount { get; set; }
        public int? PromotionDetailId { get; set; }
        public Double? SumPrice { get; set; }

        public int? DiscountAmount { get; set; }
        public int? DiscountPercent { get; set; }
        public string OrderCode { get; set; }
        public string CustomerCode { get; set; }
        public string OrderStatusCode { get; set; }
        public string OrderStateCode { get; set; }
        public string BUCode { get; set; }
        public Double? NetPrice { get; set; }
        public Double? Vat { get; set; }
        public string UpdateBy { get; set; }
        public int? runningNo { get; set; }
        public Double Transportprice { get; set; }

        public string CampaignCode { get; set; }

    }
    public class InventoryData
    {
        public string InventoryCode { get; set; }
        public string ProductCode { get; set; }
        public int? Qty { get; set; }
        public int? Reserved { get; set; }
        public int? Reserving { get; set; }
        public int? Balance { get; set; }
    }

    public partial class TakeOrder : System.Web.UI.Page
    {
       
        string Codelist = "";
        string EditFlag = "";
        protected static int currentPageNumber;
        protected static int currentPageNumberProduct;
        protected static int currentPageNumberProductPopup;
        
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        decimal? total = 0;
        string CustomerCode = "";
        string APIpath = "";
        protected List<OrderData> L_orderdata
        {
            get
            {
                if (Session["l_orderdata"] == null)
                {
                    return new List<OrderData>();
                }
                else
                {
                    return (List<OrderData>)Session["l_orderdata"];
                }
            }
            set
            {
                Session["l_orderdata"] = value;
            }
        }
        protected List<InventoryData> L_inventorydata
        {
            get
            {
                if (Session["L_inventorydata"] == null)
                {
                    return new List<InventoryData>();
                }
                else
                {
                    return (List<InventoryData>)Session["L_inventorydata"];
                }
            }
            set
            {
                Session["L_inventorydata"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CustomerCode = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
           
            Session["CustomerCode"] = CustomerCode;

            
            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;
                currentPageNumberProduct = 1;
                currentPageNumberProductPopup = 1;

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

               

                

                L_orderdata = new List<OrderData>();

                BindCampaign();
             

            }
        }

        #region Function

        public ProductInfo SetFormProductPopup()
        {
            ProductInfo pinfo = new ProductInfo();

            pinfo.PromotionCode = lbPromotion.SelectedValue;
            pinfo.CampaignCode = lbCampaign.SelectedValue;

            return pinfo;
        }
        public List<ProductInfo> ListProductByCriteria(ProductInfo pinfo)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = pinfo.PromotionCode;

                data["CampaignCode"] = pinfo.CampaignCode;

                data["rowOFFSet"] = ((currentPageNumberProductPopup - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);


            return lProductInfo;

        }

        public int? CountProductListByCriteria(ProductInfo pinfo)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountProductListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = pinfo.PromotionCode;

                data["CampaignCode"] = pinfo.CampaignCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


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

        protected void SetPageBarProductPopup(double totalRow)
        {

            lblTotalPagesProductPopup.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPageProductPopup.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPagesProductPopup.Text) + 1; i++)
            {
                ddlPageProductPopup.Items.Add(new ListItem(i.ToString()));
            }
            setDDl(ddlPageProductPopup, currentPageNumberProductPopup.ToString());
            

            
            if ((currentPageNumberProductPopup == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirstProductPopup.Enabled = false;
                lnkbtnPreProductPopup.Enabled = false;
                lnkbtnNextProductPopup.Enabled = true;
                lnkbtnLastProductPopup.Enabled = true;
            }
            else if ((currentPageNumberProductPopup.ToString() == lblTotalPagesProductPopup.Text) && (currentPageNumberProductPopup == 1))
            {
                lnkbtnFirstProductPopup.Enabled = false;
                lnkbtnPreProductPopup.Enabled = false;
                lnkbtnNextProductPopup.Enabled = false;
                lnkbtnLastProductPopup.Enabled = false;
            }
            else if ((currentPageNumberProductPopup.ToString() == lblTotalPagesProductPopup.Text) && (currentPageNumberProductPopup > 1))
            {
                lnkbtnFirstProductPopup.Enabled = true;
                lnkbtnPreProductPopup.Enabled = true;
                lnkbtnNextProductPopup.Enabled = false;
                lnkbtnLastProductPopup.Enabled = false;
            }
            else
            {
                lnkbtnFirstProductPopup.Enabled = true;
                lnkbtnPreProductPopup.Enabled = true;
                lnkbtnNextProductPopup.Enabled = true;
                lnkbtnLastProductPopup.Enabled = true;
            }
            
        }

        protected void LoadProductPopup(ProductInfo pinfo)
        {
            List<ProductInfo> lProductInfo = new List<ProductInfo>();
            
            int? totalRow = CountProductListByCriteria(pinfo);

            SetPageBarProductPopup(Convert.ToDouble(totalRow));

            lProductInfo = ListProductByCriteria(pinfo);


            

            gvProductPopup.DataSource = lProductInfo;
            gvProductPopup.DataBind();


        }


        #endregion

        #region Event 

        protected void btnProductPopupClose_Click(object sender, EventArgs e)
        {

        }

        protected void gvOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void chkProductPopupAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvProductPopup.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvProductPopup.HeaderRow.FindControl("chkProductPopupAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvProductPopup.Rows[i].FindControl("hidProductId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkProductPopup = (CheckBox)gvProductPopup.Rows[i].FindControl("chkProductPopup");

                    chkProductPopup.Checked = true;
                }
                else
                {

                    CheckBox chkProductPopup = (CheckBox)gvProductPopup.Rows[i].FindControl("chkProductPopup");

                    chkProductPopup.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        protected void ddlPageProductPopup_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumberProductPopup = Int32.Parse(ddlPageProductPopup.SelectedValue);

            LoadProductPopup(SetFormProductPopup());
        }

        protected void gvProductPopup_Change(object sender, GridViewPageEventArgs e)
        {
            gvProductPopup.PageIndex = e.NewPageIndex;

            List<ProductInfo> lProductInfo = new List<ProductInfo>();

            lProductInfo = ListProductByCriteria(SetFormProductPopup());

            gvProductPopup.DataSource = lProductInfo;

            gvProductPopup.DataBind();

        }

        protected void GetPageIndexProductPopup(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumberProductPopup = 1;
                    break;

                case "Previous":
                    currentPageNumberProductPopup = Int32.Parse(ddlPageProductPopup.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumberProductPopup = Int32.Parse(ddlPageProductPopup.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumberProductPopup = Int32.Parse(lblTotalPagesProductPopup.Text);
                    break;
            }


            LoadProductPopup(SetFormProductPopup());
        }


        protected void lbCampaign_SelectedIndexChanged(object sender, EventArgs e)
        {
            PromotionInfo pinfo = new PromotionInfo();

            pinfo.CampaignCode = lbCampaign.SelectedValue;

            hidCampaigncode.Value = lbCampaign.SelectedValue;

            BindPromotion(pinfo);
        }

        protected void lbPromotion_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidPromotioncode.Value = lbPromotion.SelectedValue;
            hidCampaigncode.Value = lbCampaign.SelectedValue;


            LoadProductPopup(SetFormProductPopup());
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-product').modal();", true);

        }

        protected void gvProductPopup_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvProductPopup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSumPrice = (Label)e.Row.FindControl("lblSumPrice");
                Label lblPrice = (Label)e.Row.FindControl("lblPrice");
                TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                HiddenField hidLockAmountFlag = (HiddenField)e.Row.FindControl("hidLockAmountFlag");
                HiddenField hidDefaultAmount = (HiddenField)e.Row.FindControl("hidDefaultAmount");


                txtAmount.Enabled = (hidLockAmountFlag.Value == "Y") ? false : true;

                txtAmount.Text = (hidDefaultAmount.Value != "") ? hidDefaultAmount.Value : "1";

               
                lblSumPrice.Text = (Convert.ToInt32(txtAmount.Text) * Convert.ToDecimal(lblPrice.Text)).ToString();
                

                TextBox textboxamount = (TextBox)e.Row.FindControl("txtAmount");
                textboxamount.Style["text-align"] = "right";
                textboxamount.Text = "1";

            }

        }

        protected void btnProductPopupSubmit_Click(object sender, EventArgs e)
        {
           
            List<OrderData> ldata = new List<OrderData>();

            ldata = L_orderdata;

            foreach (GridViewRow row in gvProductPopup.Rows)
            {
                Label lblProductCode = (Label)row.FindControl("lblProductCode");
                Label lblProductName = (Label)row.FindControl("lblProductName");
                Label lblPrice = (Label)row.FindControl("lblPrice");
                Label lblUNIT = (Label)row.FindControl("lblUNIT");
                Label lblTransportPrice = (Label)row.FindControl("lblTransportprice");
                HiddenField hidLockAmountFlag = (HiddenField)row.FindControl("hidLockAmountFlag");
                HiddenField hidDefaultAmount = (HiddenField)row.FindControl("hidDefaultAmount");
                HiddenField hidPromotionDetailId = (HiddenField)row.FindControl("hidPromotionDetailId");
                HiddenField hidDiscountAmount = (HiddenField)row.FindControl("hidDiscountAmount");
                HiddenField hidDiscountPercent = (HiddenField)row.FindControl("hidDiscountPercent");
                TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");



                if (chkSelect.Checked == true)
                {
                    if ((txtAmount.Text != "") && (txtAmount.Text != "0"))
                    {
                        OrderData odata = new OrderData();

                        odata.ProductCode = lblProductCode.Text;
                        odata.ProductName = lblProductName.Text;
                        odata.Price = (lblPrice.Text != "") ? Convert.ToDouble(lblPrice.Text) : -99;
                        odata.Amount = Convert.ToInt32(txtAmount.Text);
                        odata.DefaultAmount = (hidDefaultAmount.Value != "") ? Convert.ToInt32(hidDefaultAmount.Value) : -99;
                        odata.DiscountAmount = (hidDiscountAmount.Value != "") ? Convert.ToInt32(hidDiscountAmount.Value) : 0;
                        odata.DiscountPercent = (hidDiscountPercent.Value != "") ? Convert.ToInt32(hidDiscountPercent.Value) : 0;
                        odata.PromotionCode = hidPromotioncode.Value;
                        odata.Transportprice = (lblTransportPrice.Text != "") ? Convert.ToDouble(lblTransportPrice.Text) : -99;


                        odata.SumPrice = ((odata.Price - ((odata.Price * odata.DiscountPercent) / 100)) - odata.DiscountAmount) * odata.Amount;


                        odata.PromotionDetailId = (hidPromotionDetailId.Value != "") ? Convert.ToInt32(hidPromotionDetailId.Value) : -99;
                        odata.CampaignCode = hidCampaigncode.Value;

                        hidCampCode.Value = odata.CampaignCode;
                        ldata.Add(odata);
                        L_orderdata = ldata;
                        
                    }


                }


            }



          
        }
        #endregion

        #region Binding

        protected void BindCampaign()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCampaignNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CampaignInfo> lCampaignInfo = JsonConvert.DeserializeObject<List<CampaignInfo>>(respstr);


            lbCampaign.DataSource = lCampaignInfo;

            lbCampaign.DataTextField = "CampaignName";

            lbCampaign.DataValueField = "CampaignCode";

            lbCampaign.DataBind();

            lbCampaign.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }
        protected void BindPromotion(PromotionInfo pinfo)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCampaignPromotionNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCode"] = pinfo.CampaignCode;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);


            lbPromotion.DataSource = lPromotionInfo;

            lbPromotion.DataTextField = "PromotionName";

            lbPromotion.DataValueField = "PromotionCode";

            lbPromotion.DataBind();

            lbPromotion.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }



        #endregion

       
    }
}