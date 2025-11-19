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

namespace DOMS_TSR.src.UserControl
{
    public partial class UserControlStockOrderDetail : System.Web.UI.UserControl
    {
        protected static string APIUrl;

        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        L_OrderChangestatus result = new L_OrderChangestatus();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                    APIUrl = empInfo.ConnectionAPI;
                    List<EmpBranchInfo> lb = new List<EmpBranchInfo>();

                    
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                Button2.Visible = false;
                BindDdlInventory();
                LoadOrder();
                LoadOrderDetailMapProductwithInventory();
            }
        }
        #region Function

        protected void LoadOrder()
        {
            List<OrderInfo> lOrderInfo = new List<OrderInfo>();

            lOrderInfo = GetOrderListByCriteria();

            lblOrderCode.Text = lOrderInfo[0].OrderCode == "" ? lblOrderCode.Text = "-" : lblOrderCode.Text = lOrderInfo[0].OrderCode;

            lblCreateDate.Text = DateTime.Parse(lOrderInfo[0].CreateDate).ToString("dd-MM-yyyy") == "" ? lblCreateDate.Text = "-" : lblCreateDate.Text = DateTime.Parse(lOrderInfo[0].CreateDate).ToString("dd-MM-yyyy");

            

            lblDeliveryDate.Text = DateTime.Parse(lOrderInfo[0].DeliveryDate).ToString("dd-MM-yyyy") == "" ? lblDeliveryDate.Text = "-" : lblDeliveryDate.Text = DateTime.Parse(lOrderInfo[0].DeliveryDate).ToString("dd-MM-yyyy");

            lblOrderStatus.Text = lOrderInfo[0].OrderStatusName == "" ? lblOrderStatus.Text = "-" : lblOrderStatus.Text = lOrderInfo[0].OrderStatusName;

            


            lblBranchOrderID.Text = lOrderInfo[0].BranchOrderID == "" ? lblBranchOrderID.Text = "-" : lblBranchOrderID.Text = lOrderInfo[0].BranchOrderID;

            
        }
        public List<OrderInfo> GetOrderListByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderManagementNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection
                {
                    ["OrderCode"] = Request.QueryString["OrderCode"]
                };

                respstr = Encoding.UTF8.GetString(wb.UploadValues(APIpath, "POST", data));
            }

            List<OrderInfo> lOrderInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);

            return lOrderInfo;
        }
        protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

        protected void LoadOrderDetailMapProductwithInventory()
        {
            List<OrderDetailInfo> lProductInfo = new List<OrderDetailInfo>();

            lProductInfo = GetListOrderDetailMapProductNopagingByCriteria();

            if (lProductInfo.Count > 0)
            {              
                gvProduct.DataSource = lProductInfo;
                gvProduct.DataBind();

                hidInventorySelected.Value = lProductInfo[0].InventoryCode;
                ddlInventory.SelectedValue = hidInventorySelected.Value;
                BindgvProductInventoryFromOrder(hidInventorySelected.Value, lProductInfo);
            }
        }
        public List<OrderDetailInfo> GetListOrderDetailMapProductNopagingByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderDetailMapProductNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection
                {
                    ["OrderCode"] = Request.QueryString["OrderCode"]
                };

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderDetailInfo> lProductInfo = JsonConvert.DeserializeObject<List<OrderDetailInfo>>(respstr);

            return lProductInfo;

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            saveOrder();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtDetailbackOrder.Text = "";
        }
        protected void saveOrder()
        {
            result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Request.QueryString["OrderCode"].Trim(), orderstate = StaticField.OrderState_12,OrderNote=txtDetailbackOrder.Text });
          
            APIpath = APIUrl + "/api/support/OrderChangeStatusInfo";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var jsonObj = JsonConvert.SerializeObject(new { result.L_OrderChangestatusInfo });
                var dataString = client.UploadString(APIpath, jsonObj);

            }

            InventoryDetailInfoNew idInfo = new InventoryDetailInfoNew();
            List<InventoryDetailInfoNew> lidInfo = new List<InventoryDetailInfoNew>();

            if (ValidateInsert())
            {
                foreach (GridViewRow row in gvProductInventory.Rows)
                {
                    Label lblProductCode = (Label)row.FindControl("lblProductCode");
                    Label lblProductName = (Label)row.FindControl("lblProductName");
                    Label lblAmount = (Label)row.FindControl("lblAmount");
                    Label lblQTY = (Label)row.FindControl("lblQTY");
                    Label lblReserved = (Label)row.FindControl("lblReserved");
                    Label lblCurrent = (Label)row.FindControl("lblCurrent");
                    Label lblBalance = (Label)row.FindControl("lblBalance");

                    idInfo = new InventoryDetailInfoNew();

                    idInfo.ProductCode = lblProductCode.Text;
                    idInfo.ProductName = lblProductName.Text;
                    idInfo.Amount = (lblAmount.Text != null) ? Convert.ToInt32(lblAmount.Text) : 0;
                    idInfo.QTY = (lblQTY != null) ? Convert.ToInt32(lblQTY.Text) : 0;
                    idInfo.Reserved = (lblReserved.Text != null) ? Convert.ToInt32(lblReserved.Text) : 0;
                    idInfo.Current = (lblCurrent.Text != null) ? Convert.ToInt32(lblCurrent.Text) : 0;
                    idInfo.Balance = (lblBalance.Text != null) ? Convert.ToInt32(lblBalance.Text) : 0;

                    lidInfo.Add(idInfo);
                }

                var filteredList = lidInfo.GroupBy(e => e.ProductCode).Select(g =>
                {
                    var item = g.First();
                    return new InventoryDetailInfoNew
                    {
                        Amount = g.Sum(e => e.Amount),

                        ProductCode = item.ProductCode,
                        ProductName = item.ProductName,
                        QTY = item.QTY,
                        Reserved = item.Reserved,
                        Current = item.Current,
                        Balance = item.Balance
                    };
                }).ToList();

                int? sum = 0;

                foreach (var od in filteredList.ToList())
                {
                    InventoryInfo iInfo = new InventoryInfo();
                    List<InventoryInfo> liInfo = new List<InventoryInfo>();

                    liInfo = GetInventoryDetailInfo(hidInventorySelected.Value, od.ProductCode);

                    if (liInfo.Count > 0) // Update
                    {
                        string respstring = "";
                        APIpath = APIUrl + "/api/support/UpdateInventoryDetailfromTakeOrderRetail";

                        int? qty = od.QTY - od.Amount;
                        int? reserved = od.Reserved - od.Amount;
                        int? current = od.Current;
                        int? balance = od.Balance - od.Amount;

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["InventoryCode"] = hidInventorySelected.Value;
                            data["ProductCode"] = od.ProductCode;
                            data["QTY"] = qty.ToString();
                            data["Reserved"] = reserved.ToString();
                            data["Current"] = current.ToString();
                            data["Balance"] = balance.ToString();
                            data["UpdateBy"] = hidEmpCode.Value;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstring = Encoding.UTF8.GetString(response);
                        }
                        sum = JsonConvert.DeserializeObject<int?>(respstring);
                    }
                    else // Insert
                    {

                        string respstring = "";
                        APIpath = APIUrl + "/api/support/InsertInventoryDetailfromTakeOrderRetail";

                        int? qty = od.QTY - od.Amount;
                        int? reserved = od.Reserved - od.Amount;
                        int? current = od.Current;
                        int? balance = od.Balance - od.Amount;

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["InventoryCode"] = hidInventorySelected.Value;
                            data["ProductCode"] = od.ProductCode;
                            data["QTY"] = qty.ToString();
                            data["Reserved"] = reserved.ToString();
                            data["Current"] = current.ToString();
                            data["Balance"] = balance.ToString();
                            data["CreateBy"] = hidEmpCode.Value;
                            data["UpdateBy"] = hidEmpCode.Value;
                            data["FlagDelete"] = "N";

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstring = Encoding.UTF8.GetString(response);
                        }
                        sum = JsonConvert.DeserializeObject<int?>(respstring);
                    }
                }

                if (sum > 0)
                {
                    ddlInventory.Enabled = false;

                    List<OrderDetailInfo> lProductInfo = new List<OrderDetailInfo>();

                    lProductInfo = GetListOrderDetailMapProductNopagingByCriteria();

                    if (lProductInfo.Count > 0)
                    {
                        BindgvProductInventoryFromOrder(hidInventorySelected.Value, lProductInfo);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "สินค้าในคลังไม่พอส่งของ Order นี้"  + "');", true);
            }
        }
        protected List<InventoryInfo> GetListMasterInventoryByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = "";
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryInfo> linventoryInfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(respstr);
            return linventoryInfo;
        }
        protected List<InventoryInfo> GetInventoryDetailInfo(String inventorycode, String productcode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryDetailGetFromTakeOrderRetail";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = inventorycode;
                data["ProductCode"] = productcode;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryInfo> linventoryInfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(respstr);
            return linventoryInfo;
        }
        protected void BindDdlInventory()
        {
            List<InventoryInfo> linvInfo = new List<InventoryInfo>();
            linvInfo = GetListMasterInventoryByCriteria();

            ddlInventory.DataSource = linvInfo;
            ddlInventory.DataTextField = "InventoryName";
            ddlInventory.DataValueField = "InventoryCode";
            ddlInventory.DataBind();

            ddlInventory.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));            
        }
        protected void BindgvProductInventoryFromOrder(string inventorycode, List<OrderDetailInfo> ldInfo)
        {
            InventoryInfo invdInfo = new InventoryInfo();
            List<InventoryDetailInfo> linvdInfo = new List<InventoryDetailInfo>();

            invdInfo.InventoryCode = inventorycode;
            linvdInfo = GetInventoryBalance(invdInfo);

            InventoryDetailInfoNew ivd = new InventoryDetailInfoNew();
            List<InventoryDetailInfoNew> lidvInfo = new List<InventoryDetailInfoNew>();

            foreach (var od in ldInfo.ToList()) // ProductCode ของ Order ที่อยู่ใน InventoryDetail
            {
                if (ldInfo.Count > 0)
                {
                    foreach (var ov in linvdInfo)
                    {
                        if (od.ProductCode == ov.ProductCode)
                        {
                            ivd = new InventoryDetailInfoNew();

                            ivd.ProductCode = od.ProductCode;
                            ivd.ProductName = od.ProductName;
                            ivd.Amount = od.Amount; // จำนวน
                            ivd.QTY = ov.QTY;
                            ivd.Reserved = ov.Reserved;
                            ivd.Current = ov.Current;
                            ivd.Balance = ov.Balance;
                            
                            lidvInfo.Add(ivd);

                            ldInfo.RemoveAll(s => s.ProductCode == od.ProductCode && s.PromotionCode == od.PromotionCode);
                        }
                    }
                }
            }

            if (ldInfo.Count > 0)
            {
                foreach (var og in ldInfo)
                {
                    ivd = new InventoryDetailInfoNew();

                    ivd.ProductCode = og.ProductCode;
                    ivd.ProductName = og.ProductName;
                    ivd.Amount = og.Amount; // จำนวน
                    ivd.QTY = 0;
                    ivd.Reserved = 0;
                    ivd.Current = 0;
                    ivd.Balance = 0;
                    

                    lidvInfo.Add(ivd);
                }
            }            

            var ProductInvenList = lidvInfo.GroupBy(e => e.ProductCode).Select(g =>
            {
                var item = g.First();
                return new InventoryDetailInfoNew
                {
                    Amount = g.Sum(e => e.Amount),

                    ProductCode = item.ProductCode,
                    ProductName = item.ProductName,
                    QTY = item.QTY,
                    Reserved = item.Reserved,
                    Current = item.Current,
                    Balance = item.Balance
                };
            }).ToList();

            gvProductInventory.DataSource = ProductInvenList;
            gvProductInventory.DataBind();
        }
        protected List<InventoryDetailInfo> GetInventoryBalance(InventoryInfo invInfo)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryDetailInfoNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = invInfo.InventoryCode;
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryDetailInfo> linventorydetailInfo = JsonConvert.DeserializeObject<List<InventoryDetailInfo>>(respstr);
            return linventorydetailInfo;
        }
        protected void ddlInventory_SelectedChanged(object sender, EventArgs e)
        {
            hidInventorySelected.Value = ddlInventory.SelectedValue;

            List<OrderDetailInfo> lProductInfo = new List<OrderDetailInfo>();
            lProductInfo = GetListOrderDetailMapProductNopagingByCriteria();

            if (lProductInfo.Count > 0)
            {
                BindgvProductInventoryFromOrder(hidInventorySelected.Value, lProductInfo);
            }
        }
        protected void gvProductInventory_RowDatabound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblBalance = (Label)e.Row.FindControl("lblBalance");
                Label lblCurrent = (Label)e.Row.FindControl("lblCurrent");

                if (Convert.ToInt32(lblBalance.Text) <= 0)
                {
                    lblBalance.ForeColor = System.Drawing.Color.Red;
                }

                if (Convert.ToInt32(lblCurrent.Text) <= 0)
                {
                    lblCurrent.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        protected Boolean ValidateInsert()
        {
            Boolean flag = true;

            foreach (GridViewRow row in gvProductInventory.Rows)
            {
                Label lblProductCode = (Label)row.FindControl("lblProductCode");
                Label lblProductName = (Label)row.FindControl("lblProductName");
                Label lblAmount = (Label)row.FindControl("lblAmount");
                Label lblQTY = (Label)row.FindControl("lblQTY");
                Label lblReserved = (Label)row.FindControl("lblReserved");
                Label lblCurrent = (Label)row.FindControl("lblCurrent");
                Label lblBalance = (Label)row.FindControl("lblBalance");

                if (Convert.ToInt32(lblBalance.Text) - Convert.ToInt32(lblAmount.Text) >= 0)
                {
                    flag = (!flag) ? false : true;
                }
                else
                {
                    flag = false;
                }
            }

            return flag;
        }
        #endregion
    }
}