using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using SALEORDER.Common;
using SALEORDER.DTO;



namespace DOMS_TSR.src.UserControl
{
    public class LatLng
    {

        public double lat { get; set; }
        public double lng { get; set; }
    }

    public partial class SelectBranch : System.Web.UI.UserControl
    {
        protected static string APIUrl;
        protected static string APIUrlx = ConfigurationManager.AppSettings["apiurl"];
        string APIpath = "";

        protected List<transportdataInfo> L_transportdata1
        {
            get
            {
                if (Session["L_transportdata1"] == null)
                {
                    return new List<transportdataInfo>();
                }
                else
                {
                    return (List<transportdataInfo>)Session["L_transportdata1"];
                }
            }
            set
            {
                Session["L_transportdata1"] = value;
            }
        }

        protected List<transportdataInfo> L_transportdata2
        {
            get
            {
                if (Session["L_transportdata2"] == null)
                {
                    return new List<transportdataInfo>();
                }
                else
                {
                    return (List<transportdataInfo>)Session["L_transportdata2"];
                }
            }
            set
            {
                Session["L_transportdata2"] = value;
            }
        }

        protected List<transportdataInfo> L_transportdata3
        {
            get
            {
                if (Session["L_transportdata3"] == null)
                {
                    return new List<transportdataInfo>();
                }
                else
                {
                    return (List<transportdataInfo>)Session["L_transportdata3"];
                }
            }
            set
            {
                Session["L_transportdata3"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    APIUrl = empInfo.ConnectionAPI;
                    if(APIUrl == null)
                    {
                        APIUrl = ConfigurationManager.AppSettings["APIUrl"];

                    }
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                BindddlProvince();
                LoadCustomerReceiptAddress();
                LoadCustomerDeliveryAddress();


                InsertSection.Visible = false;
                SelectAddressSection.Visible = false;
                SaveAddrSection.Visible = true;

                
            }

        }

        #region Function

       
        public static List<InventoryInfo> LoadInventory()
        {
            string respstr = "";

            string APIpath1 = APIUrl + "/api/support/ListInventoryNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["InventoryCode"] = StaticField.CompanyCode_MK;

                var response = wb.UploadValues(APIpath1, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryInfo> lInventoryInfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(respstr);

            return lInventoryInfo;
        }


        protected void LoadCustomerAddress()
        {
            List<CustomerAddressInfo> lCustomerInfo = new List<CustomerAddressInfo>();

            lCustomerInfo = LoadCustomerAddressList();

            gvAddress.DataSource = lCustomerInfo;

            gvAddress.DataBind();


        }


        public List<CustomerAddressInfo> LoadCustomerAddressList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCustomerAddressListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                data["AddressType"] = hidAddressType.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CustomerAddressInfo> lCustomerDeliveryAddress = JsonConvert.DeserializeObject<List<CustomerAddressInfo>>(respstr);

            return lCustomerDeliveryAddress;

        }

        public void LoadCustomerDeliveryAddress()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/GetLatestUpdatedCustomerAddress";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                if(currentDeliveryAddressId.Value != "")
                {
                    data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                    data["AddressType"] = StaticField.AddressTypeCode01;
                    data["CustomerAddressId"] = currentDeliveryAddressId.Value;

                }
                else
                {
                    data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                    data["AddressType"] = StaticField.AddressTypeCode01;
                }
       

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CustomerAddressInfo> lCustomerDeliveryAddress = JsonConvert.DeserializeObject<List<CustomerAddressInfo>>(respstr);

            if (lCustomerDeliveryAddress.Count > 0 && lCustomerDeliveryAddress[0].Lat != "")
            {
                hidCustomerLat.Value = lCustomerDeliveryAddress[0].Lat;
                hidCustomerLng.Value = lCustomerDeliveryAddress[0].Long;

                hidFlagCusLatLng.Value = "TRUE";

                currentDeliveryAddress.Value = lCustomerDeliveryAddress[0].Address;
                currentDeliverySubDistrict.Value = lCustomerDeliveryAddress[0].Subdistrict;
                currentDeliverySubDistrictName.Value = lCustomerDeliveryAddress[0].SubdistrictName;
                currentDeliveryDistrict.Value = lCustomerDeliveryAddress[0].District;
                currentDeliveryDistrictName.Value = lCustomerDeliveryAddress[0].DistrictName;
                currentDeliveryProvince.Value = lCustomerDeliveryAddress[0].Province;
                currentDeliveryProvinceName.Value = lCustomerDeliveryAddress[0].ProvinceName;
                currentDeliveryZipCode.Value = lCustomerDeliveryAddress[0].ZipCode;

                lblFullAddress.Text = lCustomerDeliveryAddress[0].Address + " " + lCustomerDeliveryAddress[0].SubdistrictName + " " + lCustomerDeliveryAddress[0].DistrictName +
                " " + lCustomerDeliveryAddress[0].ProvinceName + " " + lCustomerDeliveryAddress[0].ZipCode;

                currentDeliveryAddressId.Value = lCustomerDeliveryAddress[0].CustomerAddressId.ToString();
            }
            else
            {
                hidCustomerLat.Value = "";
                hidCustomerLng.Value = "";

                hidFlagCusLatLng.Value = "FALSE";

                if (lCustomerDeliveryAddress.Count > 0)
                {
                    currentDeliveryAddress.Value = lCustomerDeliveryAddress[0].Address;
                    currentDeliverySubDistrict.Value = lCustomerDeliveryAddress[0].Subdistrict;
                    currentDeliverySubDistrictName.Value = lCustomerDeliveryAddress[0].SubdistrictName;
                    currentDeliveryDistrict.Value = lCustomerDeliveryAddress[0].District;
                    currentDeliveryDistrictName.Value = lCustomerDeliveryAddress[0].DistrictName;
                    currentDeliveryProvince.Value = lCustomerDeliveryAddress[0].Province;
                    currentDeliveryProvinceName.Value = lCustomerDeliveryAddress[0].ProvinceName;
                    currentDeliveryZipCode.Value = lCustomerDeliveryAddress[0].ZipCode;

                    lblFullAddress.Text = lCustomerDeliveryAddress[0].Address + " " + lCustomerDeliveryAddress[0].SubdistrictName + " " + lCustomerDeliveryAddress[0].DistrictName +
                    " " + lCustomerDeliveryAddress[0].ProvinceName + " " + lCustomerDeliveryAddress[0].ZipCode;

                    currentDeliveryAddressId.Value = lCustomerDeliveryAddress[0].CustomerAddressId.ToString();
                }
            }


        }



        public void LoadCustomerReceiptAddress()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/GetLatestUpdatedCustomerAddress";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                if (currentReceiptAddressId.Value != "")
                {
                    data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                    data["AddressType"] = StaticField.AddressTypeCode02;
                    data["CustomerAddressId"] = currentReceiptAddressId.Value;

                }
                else
                {
                    data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                    data["AddressType"] = StaticField.AddressTypeCode02;
                }

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CustomerAddressInfo> lCustomerDeliveryAddress = JsonConvert.DeserializeObject<List<CustomerAddressInfo>>(respstr);

            if (lCustomerDeliveryAddress.Count > 0)
            {
                currentReceiptAddress.Value = lCustomerDeliveryAddress[0].Address;
                currentReceiptSubDistrict.Value = lCustomerDeliveryAddress[0].Subdistrict;
                currentReceiptSubDistrictName.Value = lCustomerDeliveryAddress[0].SubdistrictName;
                currentReceiptDistrict.Value = lCustomerDeliveryAddress[0].District;
                currentReceiptDistrictName.Value = lCustomerDeliveryAddress[0].DistrictName;
                currentReceiptProvince.Value = lCustomerDeliveryAddress[0].Province;
                currentReceiptProvinceName.Value = lCustomerDeliveryAddress[0].ProvinceName;
                currentReceiptZipCode.Value = lCustomerDeliveryAddress[0].ZipCode;
                
                lblFullAddress1.Text = lCustomerDeliveryAddress[0].Address + " " + lCustomerDeliveryAddress[0].SubdistrictName + " " + lCustomerDeliveryAddress[0].DistrictName +
                " " + lCustomerDeliveryAddress[0].ProvinceName + " " + lCustomerDeliveryAddress[0].ZipCode;

                currentReceiptAddressId.Value = lCustomerDeliveryAddress[0].CustomerAddressId.ToString();

            }
        }

        public List<LookupInfo> ListAddressTypeInfo()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_ADDRESSTYPE;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);


            return lLookupInfo;

        }



        public List<ProvinceInfo> ListProvinceMasterInfo()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProvinceNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProvinceInfo> lProvinceInfo = JsonConvert.DeserializeObject<List<ProvinceInfo>>(respstr);


            return lProvinceInfo;

        }


        public List<DistrictInfo> ListDistrictMasterInfo(string ProvinceCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListDistrictNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();


                data["ProvinceCode"] = ProvinceCode;

                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<DistrictInfo> lDistrictInfo = JsonConvert.DeserializeObject<List<DistrictInfo>>(respstr);


            return lDistrictInfo;

        }


        public List<SubDistrictInfo> ListSubDistrictMasterInfo(string DistrictCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListSubDistrictNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();


                data["DistrictCode"] = DistrictCode;

                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<SubDistrictInfo> lSubDistrictInfo = JsonConvert.DeserializeObject<List<SubDistrictInfo>>(respstr);


            return lSubDistrictInfo;

        }


        protected Boolean validateInsert()
        {
            Boolean flag = true;


            if (txtAddress_Ins.Text == "")
            {
                flag = false;
                lblAddress.Text = MessageConst._MSG_PLEASEINSERT + " ที่อยู่";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblAddress.Text = "";
            }

            

            if (ddlDistrict.SelectedValue == "-99" || ddlDistrict.SelectedValue == "")
            {
                flag = false;
                lblDistrict_Ins.Text = MessageConst._MSG_PLEASEINSERT + " อำเภอ/เขต";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblDistrict_Ins.Text = "";
            }


            if (ddlProvince.SelectedValue == "-99" || ddlProvince.SelectedValue == "")
            {
                flag = false;
                lblProvince_Ins.Text = MessageConst._MSG_PLEASEINSERT + " จังหวัด";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblProvince_Ins.Text = "";
            }


            if (txtPostcode_Ins.Text == "")
            {
                flag = false;
                lblPostCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสไปรษณีย์";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblPostCode_Ins.Text = "";
            }


           

            return flag;
        }



        #endregion

        #region control        

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string xlat = hidNearLat.Value;
            string xlong = hidNearLng.Value;
            if (hidFlagInsert.Value == "TRUE")
            {
                if (validateInsert())
                {
                    string respstr = "";

                    //APIpath = APIUrl + "/api/support/InsertCustomerAddress";
                    APIpath = APIUrlx + "/api/support/InsertCustomerAddress";
                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                        data["AddressType"] = hidAddressType.Value;
                        data["Address"] = txtAddress_Ins.Text;
                        data["Province"] = ddlProvince.SelectedValue;
                        data["District"] = ddlDistrict.SelectedValue;
                        data["Subdistrict"] = ddlSubDistrict.SelectedValue;
                        data["ZipCode"] = txtPostcode_Ins.Text;
                        data["CreateBy"] = hidEmpCode.Value;
                        data["UpdateBy"] = hidEmpCode.Value;
                        data["Lat"] = hidNearLat.Value;
                        data["Long"] = hidNearLng.Value;
                        data["FlagActive"] = StaticField.ActiveFlag_Y;



                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                    if (sum > 0)
                    {
                        if (hidAddressType.Value == StaticField.AddressTypeCode01)
                        {
                            currentDeliveryAddressId.Value = "";
                            LoadCustomerDeliveryAddress();
                        }
                        else
                        {
                            currentReceiptAddressId.Value = "";
                            LoadCustomerReceiptAddress();
                        }


                        btnCancel_Click(null, null);
                 
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');initialize();", true);

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                    }
                }
            }
            else
            {
                if (validateInsert())
                {
                    string respstr = "";
                    
                    //APIpath = APIUrl + "/api/support/UpdateCustomerAddress";
                    APIpath = APIUrlx + "/api/support/UpdateCustomerAddress";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                        data["CustomerAddressId"] = hidEditCustomerAddressId.Value;
                        data["AddressType"] = hidAddressType.Value;
                        data["Address"] = txtAddress_Ins.Text;
                        data["Province"] = ddlProvince.SelectedValue;
                        data["District"] = ddlDistrict.SelectedValue;
                        data["Subdistrict"] = ddlSubDistrict.SelectedValue;
                        data["ZipCode"] = txtPostcode_Ins.Text;
                        data["UpdateBy"] = hidEmpCode.Value;
                        data["FlagActive"] = StaticField.ActiveFlag_Y;
                        data["Lat"] = hidNearLat.Value;
                        data["Long"] = hidNearLng.Value;


                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                    if (sum > 0)
                    {
                     
                            LoadCustomerDeliveryAddress();
                            LoadCustomerReceiptAddress();

                        btnCancel_Click(null, null);

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                    }
                }
            }
          
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtAddress_Ins.Text = "";
            txtPostcode_Ins.Text = "";
            ddlProvince.ClearSelection();
            ddlDistrict.ClearSelection();
            ddlSubDistrict.ClearSelection();

            InsertSection.Visible = false;
            SaveAddrSection.Visible = true;


            

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            

            BtnClick(sender, e);

            
        }

        public event EventHandler BtnClick;

        protected void OnbtnClick(object sender, EventArgs e)
        {
            // raise the Save event
            EventHandler btnClick = BtnClick;
            if (btnClick != null)
            {
                btnClick(sender, e);
            }
        }

        protected void btnCancelSave_Click(object sender, EventArgs e)
        {
            hidCustomerLat.Value = "";
            hidCustomerLng.Value = "";
            hidNearLat.Value = "";
            hidNearLng.Value = "";
            currentDeliveryAddressId.Value = "";
            currentReceiptAddressId.Value = "";


            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "$('#modal-address').modal('hide');", true);
        }

        protected void gvAddress_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvAddress.Rows[index];


            HiddenField hidCustomerAddressId = (HiddenField)row.FindControl("hidCustomerAddressId");


            if (e.CommandName == "SelectAddress")
            {
                if(hidAddressType.Value == StaticField.AddressTypeCode01)
                {
                    currentDeliveryAddressId.Value = hidCustomerAddressId.Value;
                    LoadCustomerDeliveryAddress();
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert","initialize();", true);
                }
                else
                {
                    currentReceiptAddressId.Value = hidCustomerAddressId.Value;
                    LoadCustomerReceiptAddress();
                }

                SelectAddressSection.Visible = false;
                SaveAddrSection.Visible = true;

            }

        }

        protected void btnAddAddress_Click(object sender, EventArgs e)
        {
            hidAddressType.Value = StaticField.AddressTypeCode01;
            hidFlagInsert.Value = "TRUE";
            lblAddressText.Text = "เพิ่มข้อมูลที่อยู่";

            InsertSection.Visible = true;
            SelectAddressSection.Visible = false;
            SaveAddrSection.Visible = false;
        }

        protected void btnSelectAddress_Click(object sender, EventArgs e)
        {
            hidAddressType.Value = StaticField.AddressTypeCode01;
            LoadCustomerAddress();

            InsertSection.Visible = false;
            SelectAddressSection.Visible = true;
            SaveAddrSection.Visible = false;
        }

        protected void btnEditAddress_Click(object sender, EventArgs e)
        {
            hidAddressType.Value = StaticField.AddressTypeCode01;
            hidFlagInsert.Value = "False";
            lblAddressText.Text = "แก้ไขข้อมูลที่อยู่";

            txtAddress_Ins.Text = currentDeliveryAddress.Value;
            ddlProvince.SelectedValue = currentDeliveryProvince.Value;
            BindddlDistrict(currentDeliveryProvince.Value, ddlDistrict);
            ddlDistrict.SelectedValue = currentDeliveryDistrict.Value;
            BindddlSubDistrict(currentDeliveryDistrict.Value);
            ddlSubDistrict.SelectedValue = currentDeliverySubDistrict.Value;
            txtPostcode_Ins.Text = currentDeliveryZipCode.Value;

            if (currentDeliveryAddressId.Value == "" || currentDeliveryAddressId.Value == null)
            {                
                hidEditCustomerAddressId.Value = L_transportdata1[0].CustomerAddressId.ToString();
            }
            else
            {
                hidEditCustomerAddressId.Value = currentDeliveryAddressId.Value;
            }
            
            InsertSection.Visible = true;
            SelectAddressSection.Visible = false;
            SaveAddrSection.Visible = false;
        }

        protected void btnAddAddress1_Click(object sender, EventArgs e)
        {
            hidAddressType.Value = StaticField.AddressTypeCode02;
            hidFlagInsert.Value = "TRUE";
            lblAddressText.Text = "เพิ่มข้อมูลที่อยู่";

            InsertSection.Visible = true;
            SelectAddressSection.Visible = false;
            SaveAddrSection.Visible = false;
        }

        protected void btnSelectAddress1_Click(object sender, EventArgs e)
        {
            hidAddressType.Value = StaticField.AddressTypeCode02;
            LoadCustomerAddress();

            InsertSection.Visible = false;
            SelectAddressSection.Visible = true;
            SaveAddrSection.Visible = false;
        }

        protected void btnEditAddress1_Click(object sender, EventArgs e)
        {
            hidAddressType.Value = StaticField.AddressTypeCode02;
            hidFlagInsert.Value = "False";
            lblAddressText.Text = "แก้ไขข้อมูลที่อยู่";

            txtAddress_Ins.Text = currentReceiptAddress.Value;
            ddlProvince.SelectedValue = currentReceiptProvince.Value;
            BindddlDistrict(currentReceiptProvince.Value, ddlDistrict);
            ddlDistrict.SelectedValue = currentReceiptDistrict.Value;
            BindddlSubDistrict(currentReceiptDistrict.Value);
            ddlSubDistrict.SelectedValue = currentReceiptSubDistrict.Value;
            txtPostcode_Ins.Text = currentReceiptZipCode.Value;

            hidEditCustomerAddressId.Value = currentReceiptAddressId.Value;
      

            InsertSection.Visible = true;
            SelectAddressSection.Visible = false;
            SaveAddrSection.Visible = false;
        }


        protected void chkAddress_Change(object sender, EventArgs e)
        {
       
        }


        


        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlDistrict(ddlProvince.SelectedValue, ddlDistrict);
            ddlSubDistrict.SelectedIndex = 0;

        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlSubDistrict(ddlDistrict.SelectedValue);
        
        }

        #endregion

        #region Binding

        

        protected void BindddlProvince()
        {
            List<ProvinceInfo> lProvinceInfo = new List<ProvinceInfo>();

            lProvinceInfo = ListProvinceMasterInfo();

            ddlProvince.DataSource = lProvinceInfo;
            ddlProvince.DataValueField = "ProvinceCode";
            ddlProvince.DataTextField = "ProvinceName";
            ddlProvince.DataBind();

            ddlProvince.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

           
        }

        protected void BindddlDistrict(string ProvinceCode, DropDownList ddl)
        {
            List<DistrictInfo> lDistrictInfo = new List<DistrictInfo>();

            lDistrictInfo = ListDistrictMasterInfo(ProvinceCode);

            ddl.DataSource = lDistrictInfo;
            ddl.DataValueField = "DistrictCode";
            ddl.DataTextField = "DistrictName";
            ddl.DataBind();


            ddl.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }


        protected void BindddlSubDistrict(string DistrictCode)
        {
            List<SubDistrictInfo> lSubDistrictInfo = new List<SubDistrictInfo>();

            lSubDistrictInfo = ListSubDistrictMasterInfo(DistrictCode);

            ddlSubDistrict.DataSource = lSubDistrictInfo;
            ddlSubDistrict.DataValueField = "SubDistrictCode";
            ddlSubDistrict.DataTextField = "SubDistrictName";
            ddlSubDistrict.DataBind();


            ddlSubDistrict.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected string GetPath()
        {
            var ls = new List<LatLng>();

            ls.Add(new LatLng { lat = 13.8217128, lng = 100.6549701 });
            ls.Add(new LatLng { lat = 13.8254003, lng = 100.65536 });
            ls.Add(new LatLng { lat = 13.8342476, lng = 100.6642673 });
            ls.Add(new LatLng { lat = 13.8212006, lng = 100.6666772 });

            string path = "";

           foreach(LatLng x in ls)
            {
                path += "{lat:" + x.lat + ",lng:" + x.lng + " },";
            }

            return path;
        }

        [WebMethod]
        public static List<InventoryInfo> BindMapMarker()
        {
            List<InventoryInfo> lstInventories = LoadInventory();
            List<InventoryInfo> lstMarkers = new List<InventoryInfo>();

            try
            {
                foreach (InventoryInfo inventoryInfo in lstInventories)
                {
                    InventoryInfo objMAPS = new InventoryInfo();
                    objMAPS.InventoryName = inventoryInfo.InventoryName;
                    objMAPS.Lat = inventoryInfo.Lat;
                    objMAPS.Long = inventoryInfo.Long; 
                    lstMarkers.Add(objMAPS);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstMarkers;
        }



        #endregion
    }
}