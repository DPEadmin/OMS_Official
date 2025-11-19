using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing;
using System.IO;
using OfficeOpenXml;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SALEORDER.DTO;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net;
using SALEORDER.Common;


namespace DOMS_TSR.src.OrderManagement
{
    public partial class UploadChangStatusOrder : System.Web.UI.Page
    {
        L_OrderChangestatus result = new L_OrderChangestatus();

        string APIpath = "";
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

              

                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantInfo = new MerchantInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];
                merchantInfo = (MerchantInfo)Session["MerchantInfo"];

                if (empInfo != null)
                {
                    
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerCode.Value = merchantInfo.MerchantCode;

                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
            }
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            string ExcelPath = "";
            string FileName = "";

            try
            {
                //*** Check HasFile And Type of File is equals excel /**
                if (fiUpload.HasFile && (fiUpload.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))
                {
                    int fileSize = fiUpload.PostedFile.ContentLength;
                    if (fileSize > 5600000)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('ขนาดไฟล์เกิน 5 MB');", true);
                        return;
                    }
                    else
                    {
                        string newFileName = "Upload_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                        fiUpload.SaveAs(Server.MapPath("~/Uploadfile/Xls/" + newFileName));
                     

                        FileInfo excel = new FileInfo(Server.MapPath(@"~/Uploadfile/Xls/" + newFileName));
                        ExcelPath = excel.ToString();
                        FileName = newFileName;
                        ViewState["UpLoadFileName"] = fiUpload.FileName.ToString();
                        ViewState["FileName"] = newFileName;
                        ViewState["ExcelPath"] = excel.ToString();

                        using (var package = new ExcelPackage(excel))
                        {
                            var workbook = package.Workbook;

                            var worksheet = workbook.Worksheets[1];
                            //เช็คว่าไฟล์มีข้อมูล
                            if (worksheet.Cells[2, 2].Text.ToString().Trim() == "")
                            {
                                File.Delete(ExcelPath);                            }
                            else
                            {
                                DataTable dt = ConvertToDataTable(worksheet);                              
                                dt = dt.DefaultView.ToTable();
                                gvOrder.DataSource = dt;
                                gvOrder.DataBind();
                                divGrid.Visible = true;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

        }
        private DataTable ConvertToDataTable(ExcelWorksheet worksheet)
        {
            int totalRows = worksheet.Dimension.End.Row;
            DataTable dt = new DataTable(worksheet.Name);
            DataRow dr = null;

            //*** Column **/
            dt.Columns.Add("Ordercode");
            dt.Columns.Add("PostNumber");
            dt.Columns.Add("OrderStatus");
            dt.Columns.Add("OrderState");
            dt.Columns.Add("MerchantName");
           

            try
            {
                for (int i = 2; i <= totalRows; i++)
                {
                    //*** Rows ***//
                    dr = dt.NewRow();
                    
                    dr["Ordercode"] = worksheet.Cells[i, 3].Text.ToString().Trim();
                    dr["PostNumber"] = worksheet.Cells[i, 20].Text.ToString().Trim();
                    dr["OrderStatus"] = worksheet.Cells[i, 19].Text.ToString().Trim();//สถานะจัดส่ง
                    dr["OrderState"] = worksheet.Cells[i, 21].Text.ToString().Trim();//สถานะออเดอร์
                    dr["MerchantName"] = worksheet.Cells[i, 24].Text.ToString();

                    if(dr["Ordercode"] != "")
                    {
                        
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        protected void btnSaveStatusOrder_Click(object sender, EventArgs e)
        {
            List<MerchantInfo> merchantInfos = new List<MerchantInfo>();
            for (int i = 0; i < gvOrder.Rows.Count; i++)
            {
                string StatusCode = "";
                string StateCode = "";
                string Merchant = "";
                HiddenField HidOrdercode = (HiddenField)gvOrder.Rows[i].FindControl("HidOrdercode");
                HiddenField HidPostNumber = (HiddenField)gvOrder.Rows[i].FindControl("HidPostNumber");
                HiddenField HidOrderStatus = (HiddenField)gvOrder.Rows[i].FindControl("HidOrderStatus");
                HiddenField HidOrderState = (HiddenField)gvOrder.Rows[i].FindControl("HidOrderState");
                HiddenField HidMerchantName = (HiddenField)gvOrder.Rows[i].FindControl("HidMerchantName");
                StatusCode = OrderststusCode(HidOrderStatus.Value.Trim());
                if (StatusCode != "-99")
                {
                    StatusCode = OrderststusCode(HidOrderStatus.Value.Trim());                  
                }
                else 
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('สถานะออเดอร์ผิด Line ที่ " + i + "');", true);
                    return;
                }
                StateCode = OrderstateCode(HidOrderState.Value.Trim());
                if (StateCode != "-99")
                {
                    StateCode = OrderstateCode(HidOrderState.Value.Trim());
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('สถานะขนส่งผิด Line ที่ " + i + "');", true);
                    return;
                }
                merchantInfos = GetMerValidateInsert(HidMerchantName.Value);
                if (merchantInfos.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Mercahnt Name ไม่ตรงกับ Merchant ที่เลือก Line ที่ " + i + "');", true);
                    return;
                }
                
                if (StatusCode == StaticField.OrderStatus_01 && StateCode == StaticField.OrderState_01) 
                {
                    StatusCode = OrderststusCode(HidOrderStatus.Value.Trim());
                    StateCode = OrderstateCode(HidOrderState.Value.Trim());
                }
                else if (StatusCode == StaticField.OrderStatus_01 && StateCode == StaticField.OrderState_16) 
                {
                    StatusCode = OrderststusCode(HidOrderStatus.Value.Trim());
                    StateCode = OrderstateCode(HidOrderState.Value.Trim());
                }
                else if (StatusCode == StaticField.OrderStatus_01 && StateCode == StaticField.OrderState_14) 
                {
                    StatusCode = OrderststusCode(HidOrderStatus.Value.Trim());
                    StateCode = OrderstateCode(HidOrderState.Value.Trim());
                }
                else if (StatusCode == StaticField.OrderStatus_10 && StateCode == StaticField.OrderState_13) 
                {
                    StatusCode = OrderststusCode(HidOrderStatus.Value.Trim());
                    StateCode = OrderstateCode(HidOrderState.Value.Trim());
                }
                else if (StatusCode == StaticField.OrderStatus_02 && StateCode == StaticField.OrderState_13) 
                {
                    StatusCode = OrderststusCode(HidOrderStatus.Value.Trim());
                    StateCode = OrderstateCode(HidOrderState.Value.Trim());
                }
                else if (StatusCode == StaticField.OrderStatus_03 && StateCode == StaticField.OrderState_13) 
                {
                    StatusCode = OrderststusCode(HidOrderStatus.Value.Trim());
                    StateCode = OrderstateCode(HidOrderState.Value.Trim());
                }
                else if (StatusCode == StaticField.OrderStatus_04 && StateCode == StaticField.OrderState_13) 
                {
                    StatusCode = OrderststusCode(HidOrderStatus.Value.Trim());
                    StateCode = OrderstateCode(HidOrderState.Value.Trim());
                }
                else if (StatusCode == StaticField.OrderStatus_05 && StateCode == StaticField.OrderState_13) 
                {
                    StatusCode = OrderststusCode(HidOrderStatus.Value.Trim());
                    StateCode = OrderstateCode(HidOrderState.Value.Trim());
                }
                else if (StatusCode == StaticField.OrderStatus_06 && StateCode == StaticField.OrderState_04) 
                {
                    StatusCode = OrderststusCode(HidOrderStatus.Value.Trim());
                    StateCode = OrderstateCode(HidOrderState.Value.Trim());
                }
                else 
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('สถานะขนส่ง และ สถานะออเดอร์ไม่เชื่อมโยงกันกรุณาตรวจสอบใหม่');", true);
                    return;
                }


                result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), 
                    ordercode = HidOrdercode.Value.ToString().Trim(),
                    orderstatus = StatusCode.Trim(),
                    orderstate= StateCode.Trim(),
                    OrderTracking = HidPostNumber.Value.Trim(),
                    MerchantMapCode = hidMerCode.Value,
                });

            }


            APIpath = APIUrl + "/api/support/OrderChangeStatusInfo";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var jsonObj = JsonConvert.SerializeObject(new { result.L_OrderChangestatusInfo });
                var dataString = client.UploadString(APIpath, jsonObj);
              
            }
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('นำเข้าข้อมูลสำเร็จ');", true);
            divGrid.Visible = false;
        }

        public string OrderststusCode(string NameStatusCode)
        {
            string OrderststusCode = "";
            List<LookupInfo> lLookupInfo = new List<LookupInfo>();
            try 
            {
                string respstr = "";

                APIpath = APIUrl + "/api/support/ListStatusCode";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["LookupType"] = StaticField.LookupType_ORDERSTATUS; 
                    data["LookupValue"] = NameStatusCode;

                    data["FlagDelete"] = "N";

                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }
                lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);
                

            }
            catch (Exception ex) 

            {
              
                
            }
            if (lLookupInfo.Count > 0)
            {
                OrderststusCode= lLookupInfo[0].LookupCode.ToString();
            }
            else 
            
            {
                OrderststusCode = "-99";
             
               
            }
            return OrderststusCode;
        }
        public string OrderstateCode(string NameStateCode)
        {
            String OrderstateCode = "";
            List<LookupInfo> lLookupInfo = new List<LookupInfo>();
            try
            {
                string respstr = "";

                APIpath = APIUrl + "/api/support/ListStatusCode";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["LookupType"] = StaticField.LookupType_ORDERSTATE; 
                    data["LookupValue"] = NameStateCode;

                    data["FlagDelete"] = "N";

                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }
               lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

               
            } catch (Exception ex) 
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('สถานะ Orderstate');", true);
               
            }
            if (lLookupInfo.Count > 0)
            {
                OrderstateCode = lLookupInfo[0].LookupCode.ToString();
            }
            else

            {
                OrderstateCode="-99";
              
               
            }
            return OrderstateCode;
        }
        protected List<MerchantInfo> GetMerValidateInsert(string code)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListMerchantForValByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["MerchantName"] = code;
                data["MerchantCode"] = hidMerCode.Value;
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<MerchantInfo> MerchantInfo = JsonConvert.DeserializeObject<List<MerchantInfo>>(respstr);

            return MerchantInfo;
        }

        

        protected void btnCancelSaveStatusOrder_Click(object sender, EventArgs e)
        {
            gvOrder.DataSource = new DataTable();
            gvOrder.DataBind();
            divGrid.Visible = false;
        }
    }
}