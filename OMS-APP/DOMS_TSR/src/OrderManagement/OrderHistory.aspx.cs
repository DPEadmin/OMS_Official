using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.Configuration;
using SALEORDER.DTO;
using Newtonsoft.Json;
using SALEORDER.Common;
using System.Globalization;
using System.IO;

namespace DOMS_TSR.src.OrderManagement
{
    public partial class OrderHistory : System.Web.UI.Page
    {
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string APIUrl;
        string APIpath = "";
        string Codelist = "";
        Boolean isdelete;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;

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
                String CustomerCode = Request.QueryString["CustomerCode"];
                loadOrderHistory(CustomerCode);
                bindddlOrderStatusCode_Search();
                bindddlOrderTypeCode_Search();
            }
        }
        #region event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            String CustomerCode = Request.QueryString["CustomerCode"];
            currentPageNumber = 1;
            loadOrderHistorySearch(CustomerCode);
        }
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtOrderCode_Search.Text = "";
            ddlOrderStatusCode_Search.SelectedValue = "";
            ddlOrderTypeCode_Search.SelectedValue = "";
            txtOrderDateFrom_Search.Text = "";
            txtOrderDateTo_Search.Text = "";
            txtDeliveryDateFrom_Search.Text = "";
            txtDeliveryDateTo_Search.Text = "";
            txtDateReceivedFrom_Search.Text = "";
            txtDateReceivedTo_Search.Text = "";
        }
        protected void gvOrderHistory_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvOrderHistory.Rows[index];

            HiddenField hidOrderCode = (HiddenField)row.FindControl("hidOrderCode");

            if (e.CommandName == "ShowOrderDetail")
            {
                hidOrderCodeHistory.Value = hidOrderCode.Value;

                
                loadOrderHistoryDetail(hidOrderCodeHistory.Value);                
            }            
        }
        protected void btnOrderCode_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            HiddenField hidOrderCode = (HiddenField)row.FindControl("hidOrderCode");
            lblHeadOrderCode.Text = hidOrderCode.Value;
            loadOrderHistoryDetail(hidOrderCode.Value);
        }
        #endregion

        #region function
        protected void loadOrderHistory(String customercode)
        {
            List<OrderInfo> lorderInfo = new List<OrderInfo>();
            int? totalRow = CountOrderHistoryMasterList();
            SetPageBar(Convert.ToDouble(totalRow));
            lorderInfo = GetOrderHistoryMasterByCriteria();
            Double? d = 0.00;

            foreach(var lV in lorderInfo)
            {
                d = Convert.ToDouble(lV.TotalPrice);
                lV.TotalPrice = string.Format("{0:n}", d);
            }
            
            gvOrderHistory.DataSource = lorderInfo;
            gvOrderHistory.DataBind();
        }
        protected void loadOrderHistorySearch(String customercode)
        {
            List<OrderInfo> lorderInfo = new List<OrderInfo>();
            int? totalRow = CountOrderHistoryMasterList();
            SetPageBar(Convert.ToDouble(totalRow));
            lorderInfo = GetOrderHistoryMasterforSearchByCriteria();
            Double? d = 0.00;
            foreach (var lorder in lorderInfo)
            {
                lorder.CreateDate = string.Format("{0:MM/dd/yyyy}", lorder.CreateDate);
                lorder.DeliveryDate = string.Format("{0:MM/dd/yyyy}", lorder.DeliveryDate);
                lorder.ReceivedDate = string.Format("{0:MM/dd/yyyy}", lorder.ReceivedDate);

                d = Convert.ToDouble(lorder.TotalPrice);
                lorder.TotalPrice = string.Format("{0:n}", d);
            }

            gvOrderHistory.DataSource = lorderInfo;
            gvOrderHistory.DataBind();
        }
        protected int? CountOrderHistoryMasterList()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/CountOrderListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtOrderCode_Search.Text;
                data["OrderStatusCode"] = ddlOrderStatusCode_Search.SelectedValue;
                data["OrderTypeCode"] = ddlOrderTypeCode_Search.SelectedValue;
                data["CreateDateFrom"] = txtOrderDateFrom_Search.Text;
                data["CreateDateTo"] = txtOrderDateTo_Search.Text;
                data["DeliveryDateFrom"] = txtDeliveryDateFrom_Search.Text;
                data["DeliveryDateTo"] = txtDeliveryDateTo_Search.Text;
                data["ReceivedDateFrom"] = txtDateReceivedFrom_Search.Text;
                data["ReceivedDateTo"] = txtDateReceivedTo_Search.Text;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);
            return cou;
        }
        protected List<OrderInfo> GetOrderHistoryMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtOrderCode_Search.Text;
                data["CustomerCode"] = Request.QueryString["CustomerCode"];
                data["OrderStatusCode"] = ddlOrderStatusCode_Search.SelectedValue;
                data["OrderTypeCode"] = ddlOrderTypeCode_Search.SelectedValue;
                data["CreateDateFrom"] = txtOrderDateFrom_Search.Text;
                data["CreateDateTo"] = txtOrderDateTo_Search.Text;
                data["DeliveryDateFrom"] = txtDeliveryDateFrom_Search.Text;
                data["DeliveryDateTo"] = txtDeliveryDateTo_Search.Text;
                data["ReceivedDateFrom"] = txtDateReceivedFrom_Search.Text;
                data["ReceivedDateTo"] = txtDateReceivedTo_Search.Text;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lOrderInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);
            return lOrderInfo;
        }
        protected List<OrderInfo> GetOrderHistoryMasterforSearchByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtOrderCode_Search.Text;
                data["CustomerCode"] = Request.QueryString["CustomerCode"];
                data["OrderStatusCode"] = ddlOrderStatusCode_Search.SelectedValue;
                data["OrderTypeCode"] = ddlOrderTypeCode_Search.SelectedValue;
                data["CreateDateFrom"] = txtOrderDateFrom_Search.Text;
                data["CreateDateTo"] = txtOrderDateTo_Search.Text;
                data["DeliveryDateFrom"] = txtDeliveryDateFrom_Search.Text;
                data["DeliveryDateTo"] = txtDeliveryDateTo_Search.Text;
                data["ReceivedDateFrom"] = txtDateReceivedFrom_Search.Text;
                data["ReceivedDateTo"] = txtDateReceivedTo_Search.Text;
                data["rowOFFSet"] = "0";
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lOrderInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);
            return lOrderInfo;
        }
        protected void loadOrderHistoryDetail(String ordercode)
        {
            lblHeadOrderCode.Text = ordercode;

            OrderDetailInfo odInfo = new OrderDetailInfo();
            List<OrderDetailInfo> lodInfo = new List<OrderDetailInfo>();
            lodInfo = GetOrderHistoryDetailMasterByCriteria(ordercode);

            if (ordercode != "")
            {
                gvOrderHistoryDetail.DataSource = lodInfo;
                gvOrderHistoryDetail.DataBind();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-orderhistorydetail').modal();", true);
            }
            else
            {
                gvOrderHistoryDetail.DataSource = lodInfo;
                gvOrderHistoryDetail.DataBind();

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "myModal", "$('#modal-orderhistorydetail').modal('hide');", true);
            }

            

            
        }
        protected List<OrderDetailInfo> GetOrderHistoryDetailMasterByCriteria(String ordercode)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/GetOrderHistoryatSalesOrderPage";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = ordercode;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<OrderDetailInfo> lOrderDetailInfo = JsonConvert.DeserializeObject<List<OrderDetailInfo>>(respstr);

            return lOrderDetailInfo;
        }
        protected List<LookupInfo> OrderStatusMasterInfo()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["LookupType"] = StaticField.LookupType_ORDERSTATUS; 
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            return lLookupInfo;
        }
        protected List<LookupInfo> OrderTypeMasterInfo()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["LookupType"] = StaticField.LookupType_ORDERTYPE; 
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            return lLookupInfo;
        }
        #endregion

        #region binding
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
            String CustomerCode = Request.QueryString["CustomerCode"];
            loadOrderHistory(CustomerCode);
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            String CustomerCode = Request.QueryString["CustomerCode"];
            loadOrderHistory(CustomerCode);
        }
        protected void bindddlOrderStatusCode_Search()
        {
            List<LookupInfo> lProvinceInfo = new List<LookupInfo>();
            lProvinceInfo = OrderStatusMasterInfo();

            ddlOrderStatusCode_Search.DataSource = lProvinceInfo;
            ddlOrderStatusCode_Search.DataValueField = "LookupCode";
            ddlOrderStatusCode_Search.DataTextField = "LookupValue";
            ddlOrderStatusCode_Search.DataBind();

            ddlOrderStatusCode_Search.Items.Insert(0, new ListItem("กรุณาเลือกสถานะใบสั่งขาย", ""));
        }
        protected void bindddlOrderTypeCode_Search()
        {
            List<LookupInfo> lProvinceInfo = new List<LookupInfo>();
            lProvinceInfo = OrderTypeMasterInfo();

            ddlOrderTypeCode_Search.DataSource = lProvinceInfo;
            ddlOrderTypeCode_Search.DataValueField = "LookupCode";
            ddlOrderTypeCode_Search.DataTextField = "LookupValue";
            ddlOrderTypeCode_Search.DataBind();

            ddlOrderTypeCode_Search.Items.Insert(0, new ListItem("กรุณาเลือกชนิดใบสั่งขาย", ""));
        }
        #endregion
    }
}