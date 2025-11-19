using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using SALEORDER.DTO;
using Newtonsoft.Json;
using SALEORDER.Common;
using System.Text;
using System.Collections.Specialized;
using System.Net;


using System.ComponentModel;
using static System.Net.WebRequestMethods;
using System.IO.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DOMS_TSR.src.FullfillOrderlist
{
    public partial class InvoiceViewer : System.Web.UI.Page
    {
        protected static string APIUrl;

     
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];
                if (empInfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;

                  //  APIUrl = empInfo.ConnectionAPI;
                    APIUrl = "http://localhost:54545";
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                LoadDataPDF();
            }
               
        }
        private void LoadDataPDF()
        {
            

            
            List<InvoiceOrderInfo> lProductInfo = new List<InvoiceOrderInfo>();
            lProductInfo= LoadOrderDetailMapProduct();
            DataTable dt = new DataTable();
            dt = ConvertToDataTable(lProductInfo);
            try
            {
               
               
                if (dt.Rows.Count > 0)
                {
                    
                    if (dt.Rows.Count > 0)
                    {
                        // get address merchant
                        List<MerchantInfo> lmer = new List<MerchantInfo>();
                        lmer = GetListMerchantAddress(dt.Rows[0]["MerchantMapCode"].ToString());
                        string MerchantAddress = "";
                        string ParamMerchantName = "";
                        string ParamDateBefore = "-";

                        DateTime xdate = DateTime.Parse(dt.Rows[0]["Orderdate"].ToString());
                        xdate = xdate.AddDays(5);

                        if (lmer.Count > 0)
                        {
                            string address = lmer[0].Address==null?"-" : lmer[0].Address;
                            string provincename = lmer[0].ProvinceName == null ? "-" : lmer[0].ProvinceName;
                            string distictname = lmer[0].DistictName == null ? "-" : lmer[0].DistictName;
                            string subdistictname = lmer[0].SubDistictName == null ? "-" : lmer[0].SubDistictName;
                            string zipcode = lmer[0].ZipCode == null ? "-" : lmer[0].ZipCode;
                            string contactTel = lmer[0].ContactTel == null ? "-" : lmer[0].ContactTel;
                            ParamMerchantName = lmer[0].MerchantName == null ? "-" : lmer[0].MerchantName;
                            MerchantAddress = address + " " + subdistictname + " " + distictname + " " + provincename + " " + zipcode + "," + contactTel;
                            ParamDateBefore= xdate.ToString("dd MMM yyyy");
                        }
                        else 
                        {
                            ParamMerchantName = "-";
                            MerchantAddress = "-";
                        }
                      

                        //
                        DataTable dts = dt;

                        ReportDataSource datasource = new ReportDataSource("DataSet2", dts);

                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(datasource);
                        

                        ReportViewer1.LocalReport.ReportPath = @"src\Report\Report2.rdlc";
                        ReportViewer1.LocalReport.EnableExternalImages = true;
                        //string imagePath = new Uri(Server.MapPath("~/images/Mudassar.jpg")).AbsoluteUri;
                        ReportParameter parameter = new ReportParameter("ParamAddress", MerchantAddress);
                        ReportParameter parameter2 = new ReportParameter("ParamMerchantName", ParamMerchantName);
                        ReportParameter parameter3 = new ReportParameter("ParamDateBefore", ParamDateBefore);
                        

                        ReportViewer1.LocalReport.SetParameters(parameter);
                        ReportViewer1.LocalReport.SetParameters(parameter2);
                        ReportViewer1.LocalReport.SetParameters(parameter3);
                        ReportViewer1.LocalReport.Refresh();

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertxxx", "alert('ไม่มีข้อมูล')", true);
                        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);


                    }
                    string FlagView = Request.QueryString["FlagView"];
                    if (FlagView!="View") 
                    {
                        saveOrder();
                    }

                   
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "loadreport", "ShowMessage();", true);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Report 45 Error :" + ex.Message);
            }

        }
        private void ShowReports(ReportDataSource drsource)
        {

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(drsource);
            

            ReportViewer1.LocalReport.ReportPath = @"Report2.rdlc";


            
        }

        #region function 
        protected void LoadOrderMapCustomer()
        {
            List<OrderDetailInfo> lOrderDetailInfo = new List<OrderDetailInfo>();

            lOrderDetailInfo = GetOrderMapCustomerByCriteria();

        

        }

        public List<OrderDetailInfo> GetOrderMapCustomerByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderMapCustomerNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection
                {
                    ["OrderCode"] = Request.QueryString["OrderCode"]
                };

                respstr = Encoding.UTF8.GetString(wb.UploadValues(APIpath, "POST", data));
            }

            List<OrderDetailInfo> lOrderDetailInfo = JsonConvert.DeserializeObject<List<OrderDetailInfo>>(respstr);

            return lOrderDetailInfo;
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


        protected void LoadAddress()
        {
            List<CustomerAddressInfo> lOrderInfo = new List<CustomerAddressInfo>();

            lOrderInfo = GetAddressByCriteria("01");

          



            lOrderInfo = GetAddressByCriteria("02");

           


        }

        public List<CustomerAddressInfo> GetAddressByCriteria(string AddressType)
        {
            List<OrderInfo> lOrderInfo = new List<OrderInfo>();

            lOrderInfo = GetOrderListByCriteria();

            string respstr = "";

            
            APIpath = APIUrl + "/api/support/ListCustomerAddressDetailOrderByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection
                {
                    ["OrderCode"] = Request.QueryString["OrderCode"],
                    ["CustomerCode"] = lOrderInfo[0].CustomerCode,
                    ["AddressType"] = AddressType
                };

                respstr = Encoding.UTF8.GetString(wb.UploadValues(APIpath, "POST", data));
            }

            List<CustomerAddressInfo> lCustomerAddressInfo = JsonConvert.DeserializeObject<List<CustomerAddressInfo>>(respstr);

            return lCustomerAddressInfo;
        }

        protected List<InvoiceOrderInfo> LoadOrderDetailMapProduct()
        {
            List<InvoiceOrderInfo> lProductInfo = new List<InvoiceOrderInfo>();

            lProductInfo = GetListOrderDetailMapProductAndPronotionNopagingByCriteria();
            return lProductInfo;
       

        }
        public List<InvoiceOrderInfo> GetListOrderDetailMapProductAndPronotionNopagingByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/InvoiceOrder";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection
                {
                    ["OrderCode"] = Request.QueryString["OrderCode"]
                };

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InvoiceOrderInfo> lProductInfo = JsonConvert.DeserializeObject<List<InvoiceOrderInfo>>(respstr);

            return lProductInfo;

        }

        protected void saveOrder()
        {
            L_OrderChangestatus result = new L_OrderChangestatus();
            result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Request.QueryString["OrderCode"].Trim(), orderstatus = StaticField.OrderStatus_02, OrderNote =" Printed " }); 

            APIpath = APIUrl + "/api/support/OrderChangeStatusInfo";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var jsonObj = JsonConvert.SerializeObject(new { result.L_OrderChangestatusInfo });
                var dataString = client.UploadString(APIpath, jsonObj);

            }
        }
        #endregion
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }

        protected List<MerchantInfo> LoadMerchantAddress(string merCode)
        {
            List<MerchantInfo> lMerInfo = new List<MerchantInfo>();

            lMerInfo = GetListMerchantAddress(merCode);
            return lMerInfo;
        }
        public List<MerchantInfo> GetListMerchantAddress(string merCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/GetMerchantAddress";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection
                {
                    ["MerchantCode"] = merCode
                };

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<MerchantInfo> lProductInfo = JsonConvert.DeserializeObject<List<MerchantInfo>>(respstr);

            return lProductInfo;

        }
    }
}