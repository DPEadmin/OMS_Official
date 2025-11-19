using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Net;
using System.Configuration;
using SALEORDER.DTO;
using Newtonsoft.Json;
using SALEORDER.Common;

using System.ComponentModel;
namespace DOMS_TSR.src.FullfillOrderlist
{
    public partial class AppointmentOrderManagementDetail : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string PromotionImgUrl = ConfigurationManager.AppSettings["PromotionImageUrl"];
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        L_OrderChangestatus result = new L_OrderChangestatus();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantinfo = new MerchantInfo();
                merchantinfo = (MerchantInfo)Session["MerchantInfo"];
                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    APIUrl = empInfo.ConnectionAPI;
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerchantMapcode.Value = merchantinfo.MerchantCode;
                    hidMerchantMapName.Value = merchantinfo.MerchantName;
                    ((DropDownList)Master.FindControl("ddlMerchant")).Enabled = false;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                BindddlSearchOrderStatus();
                BindddlSearchOrderState();
            }
               
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (validateInsert())
            {
                saveOrder();
                Response.Redirect("AppointmentOrderManagement.aspx");
            }
        
            else 
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('ไม่สามารถบันทึกข้อมูลได้');", true);
            }
        }
        protected void btnEditOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("../TakeOrderRetail/TakeOrderEdit.aspx?CustomerCode="+ Request.QueryString["CustomerCode"].ToString() + "&OrderCode="+ Request.QueryString["OrderCode"].ToString() + "&MerchantSession="+hidMerchantMapcode.Value + "&MerchantSessionName=" + hidMerchantMapName.Value);
        }
        protected void BindddlSearchOrderStatus()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "ORDERSTATUS";
                data["GroupLookupCode"] = StaticField.OrderStatus_01; 

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> cLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlOrderstatus.DataSource = cLookupInfo;
            ddlOrderstatus.DataTextField = "LookupValue";
            ddlOrderstatus.DataValueField = "LookupCode";
            ddlOrderstatus.DataBind();
            ddlOrderstatus.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }
        protected void BindddlSearchOrderState()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "ORDERSTATE";
                data["GroupLookupCode"] = StaticField.OrderState_13 + "," + StaticField.OrderState_12 + "," + StaticField.OrderState_14; 

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> cLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlOrderstate.DataSource = cLookupInfo;
            ddlOrderstate.DataTextField = "LookupValue";
            ddlOrderstate.DataValueField = "LookupCode";
            ddlOrderstate.DataBind();
            ddlOrderstate.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("AppointmentOrderManagement.aspx");
        }
        protected void saveOrder()
        {
            if (ddlOrderstate.SelectedValue == StaticField.OrderState_13) 
            {
                string ordercode = Request.QueryString["OrderCode"].Trim();

                UpdateInventoryDetailbyStock(ordercode);
            }

                result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Request.QueryString["OrderCode"].Trim(), orderstate = ddlOrderstate.SelectedValue, orderstatus = ddlOrderstatus.SelectedValue, OrderNote = txtDetailbackOrder.Text });

                APIpath = APIUrl + "/api/support/OrderChangeStatusInfo";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var jsonObj = JsonConvert.SerializeObject(new { result.L_OrderChangestatusInfo });
                var dataString = client.UploadString(APIpath, jsonObj);
            }
        
        }

        protected Boolean validateInsert()
        {
            Boolean flag = true;

         

            if (ddlOrderstate.SelectedValue == "-99" || ddlOrderstate.SelectedValue == "")
            {
                flag = false;
                Lbstate.Text = MessageConst._MSG_PLEASEINSERT + " เลือกสถานะ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                Lbstate.Text = "";
            }

            if (ddlOrderstatus.SelectedValue == "-99" || ddlOrderstatus.SelectedValue == "")
            {
                flag = false;
                Lbstatus.Text = MessageConst._MSG_PLEASEINSERT + " เลือกขั้นตอน";
            }
            else
            {
                flag = (flag == false) ? false : true;
                Lbstatus.Text = "";
            }
            return flag;
        }

        #region Function
        protected void UpdateInventoryDetailbyStock(string ordercode)
        {
            string respstr1 = "";
            int? amount = 0;
            string productcode = "";
            string inventory = "";

            APIpath = APIUrl + "/api/support/ListOrderDetailNopagingbyOrderCode";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = ordercode;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr1 = Encoding.UTF8.GetString(response);
            }

            List<OrderDetailInfo> lorderdataInfo = JsonConvert.DeserializeObject<List<OrderDetailInfo>>(respstr1);

            if (lorderdataInfo.Count > 0)
            {
                foreach (var od in lorderdataInfo)
                {
                    amount = od.Amount;
                    productcode = od.ProductCode;
                    inventory = od.InventoryCode;

                    string respstr2 = "";

                    APIpath = APIUrl + "/api/support/ListInventoryDetailInfoNoPagingByCriteria";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["InventoryCode"] = od.InventoryCode;
                        data["ProductCode"] = od.ProductCode;

                        var response = wb.UploadValues(APIpath, "POST", data);
                        respstr2 = Encoding.UTF8.GetString(response);
                    }

                    List<OrderDetailInfo> linventroydetailinfo = JsonConvert.DeserializeObject<List<OrderDetailInfo>>(respstr2);
                    int? QTY = 0;
                    int? reserved = 0;
                    int? balance = 0;

                    try
                    {
                        if (linventroydetailinfo.Count > 0)
                        {
                            foreach (var oe in linventroydetailinfo)
                            {
                                QTY = oe.QTY - amount;
                                reserved = oe.Reserved - amount;
                                balance = oe.Balance - amount;

                                string respstr = "";
                                int? sum = 0;
                                APIpath = APIUrl + "/api/support/UpdateInvenDetailfromOrderChangeStatus";

                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["InventoryCode"] = inventory;
                                    data["ProductCode"] = productcode;
                                    data["QTY"] = QTY.ToString();
                                    data["Reserved"] = reserved.ToString();
                                    data["Balance"] = balance.ToString();
                                    data["UpdateBy"] = hidEmpCode.Value;

                                    var response = wb.UploadValues(APIpath, "POST", data);

                                    respstr = Encoding.UTF8.GetString(response);

                                    sum = JsonConvert.DeserializeObject<int?>(respstr);
                                }
                            }
                        }
                    }
                    catch (Exception ex) { }
                }
            }
        }
        #endregion
    }

}