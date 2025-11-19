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
namespace DOMS_TSR.src.OrderManagement
{
    public partial class OrderIncompleteBackOrderDetail : System.Web.UI.Page
    {
        protected static string APIUrl;
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

                BindddlSearchOrderStatus();
                BindddlSearchOrderState();
            }
               
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            saveOrder();
            Response.Redirect("AppointmentOrderManagement.aspx");
        }
        protected void BindddlSearchOrderStatus()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_ORDERSTATUS; 
                data["GroupLookupCode"] = StaticField.OrderStatus_01; 

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> cLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchOrderstatus.DataSource = cLookupInfo;
            ddlSearchOrderstatus.DataTextField = "LookupValue";
            ddlSearchOrderstatus.DataValueField = "LookupCode";
            ddlSearchOrderstatus.DataBind();
            ddlSearchOrderstatus.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }
        protected void BindddlSearchOrderState()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_ORDERSTATE; 
                data["GroupLookupCode"] = StaticField.OrderState_13 + "," + StaticField.OrderState_12 + "," + StaticField.OrderState_14; 

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> cLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchOrderstate.DataSource = cLookupInfo;
            ddlSearchOrderstate.DataTextField = "LookupValue";
            ddlSearchOrderstate.DataValueField = "LookupCode";
            ddlSearchOrderstate.DataBind();
            ddlSearchOrderstate.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("AppointmentOrderManagement.aspx");
        }
        protected void saveOrder()
        {
            result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Request.QueryString["OrderCode"].Trim(), orderstate = ddlSearchOrderstate.SelectedValue,orderstatus=ddlSearchOrderstatus.SelectedValue, OrderNote = txtDetailbackOrder.Text });

            APIpath = APIUrl + "/api/support/OrderChangeStatusInfo";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var jsonObj = JsonConvert.SerializeObject(new { result.L_OrderChangestatusInfo });
                var dataString = client.UploadString(APIpath, jsonObj);

            }
        }
    }  
    
}