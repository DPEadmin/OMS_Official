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
using System.Web.Services;

namespace DOMS_TSR.src.InventoryManagement
{
    public partial class InventoryGoodsInTransit : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        string Codelist = "";
        string EditFlag = "";
        Boolean isdelete;
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;

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
                BindDdlInventory();
                BindddlProvince();
                BindddlDistrict();
                BindddlSubDistrict();
                LoadInventory();
                LoadInventoryTo();

            }
        }

        #region FUNCTION
        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"InventoryDetail.aspx?InventoryCode=" + strCode + "\">" + strCode + "</a>";
        }

        protected string GetLink2(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a class=\"button-activity\" href=\"InventoryDetail.aspx?InventoryCode=" + strCode + "\">" + "<span class=\"fa fa-plus\">" + "</a>";
        }

        protected void LoadInventory()
        {
            string a = "";
            List<InventoryDetailInfoNew> lInventoryDetailInfoNew = new List<InventoryDetailInfoNew>();

            

            int? totalRow = CountInventoryMasterList();

            SetPageBar(Convert.ToDouble(totalRow));

            lInventoryDetailInfoNew = GetInventoryMasterByCriteria();

            gvInventoryDetailStart.DataSource = lInventoryDetailInfoNew;

            gvInventoryDetailStart.DataBind();
        }
        protected void LoadInventoryTo()
        {
            string a = "";
            List<InventoryDetailInfoNew> lInventoryDetailInfoNew = new List<InventoryDetailInfoNew>();

            

            lInventoryDetailInfoNew = GetInventoryMasterByCriteriaTo();

            gvInventoryDetailTo.DataSource = lInventoryDetailInfoNew;

            gvInventoryDetailTo.DataBind();
        }
        public int? CountInventoryMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountInventoryListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["InventoryCodeTo"] = ddlinvTo.SelectedValue;
                data["InventoryCode"] = ddlinvStart.SelectedValue;
                data["MerchantCode"] = hidMerCode.Value;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }

        public List<InventoryDetailInfoNew> GetInventoryMasterByCriteria()
        {
            string respstr = "";

          
            APIpath = APIUrl + "/api/support/ListInventoryDetailInfoNoPagingByCriteriaMoveStock";
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = ddlinvStart.SelectedValue;
                data["MerchantCode"] = hidMerCode.Value;


                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryDetailInfoNew> lInventoryDetailInfoNew = JsonConvert.DeserializeObject<List<InventoryDetailInfoNew>>(respstr);

            return lInventoryDetailInfoNew;

        }
        public List<InventoryDetailInfoNew> GetInventoryMasterByCriteriaTo()
        {
            string respstr = "";

            
            APIpath = APIUrl + "/api/support/ListInventoryDetailInfoNoPagingByCriteriaMoveStock";
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = ddlinvTo.SelectedValue;
                data["MerchantCode"] = hidMerCode.Value;


                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryDetailInfoNew> lInventoryDetailInfoNew = JsonConvert.DeserializeObject<List<InventoryDetailInfoNew>>(respstr);

            return lInventoryDetailInfoNew;

        }
        protected List<InventoryDetailInfoNew> GetListMasterInventoryByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantCode"] = hidMerCode.Value;
                data["InventoryCode"] = "";
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryDetailInfoNew> lInventoryDetailInfoNew = JsonConvert.DeserializeObject<List<InventoryDetailInfoNew>>(respstr);
            return lInventoryDetailInfoNew;
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

            LoadInventory();
        }

        protected void ClearSearch()
        {
            ddlinvStart.ClearSelection();
            ddlinvTo.ClearSelection();
            
        }

        protected Boolean validateInsertUpdate()
        {
            Boolean flag = true;

            if (txtInventoryCode_Ins.Text == "")
            {
                flag = false;
                lblInventoryCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสคลังสินค้า";
            }
            else
            {
                if (hidFlagInsert.Value == "True")
                {
                    if (txtInventoryCode_Ins.Text != "")
                    {
                        Boolean isDuplicate = ValidateDuplicate();

                        if (isDuplicate)
                        {
                            flag = false;
                            lblInventoryCode_Ins.Text = MessageConst._DATA_NComplete;
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblInventoryCode_Ins.Text = "";
                        }
                    }
                }                
            }

            if (txtInventoryName_Ins.Text == "")
            {
                flag = false;
                lblInventoryName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อคลังสินค้า";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblInventoryName_Ins.Text = "";
            }

            if (txtAddress_Ins.Text == "")
            {
                flag = false;
                lblAddress_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ที่อยู่คลังสินค้า";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblAddress_Ins.Text = "";
            }

            if (ddlProvince_Ins.SelectedValue == "-99" || ddlProvince_Ins.SelectedValue == "")
            {
                flag = false;
                lblProvince_Ins.Text = MessageConst._MSG_PLEASEINSERT + " จังหวัด";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblProvince_Ins.Text = "";
            }

            if (ddlDistrict_Ins.SelectedValue == "-99" || ddlDistrict_Ins.SelectedValue == "")
            {
                flag = false;
                lblDistrict_Ins.Text = MessageConst._MSG_PLEASEINSERT + " อำเภอ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblDistrict_Ins.Text = "";
            }

            if (ddlSubDistrict_Ins.SelectedValue == "-99" || ddlSubDistrict_Ins.SelectedValue == "")
            {
                flag = false;
                lblSubDistrict_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ตำบล";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblSubDistrict_Ins.Text = "";
            }

            if (txtPostCode_Ins.Text.Length != 5 || txtPostCode_Ins.Text != null || txtPostCode_Ins.Text != "")
            {
                if (txtPostCode_Ins.Text != "")
                {
                    if (txtPostCode_Ins.Text.Length < 5)
                    {
                        lblPostCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ระบุเลขไม่ครบ 5 ditgit";
                        flag = false;
                    }
                    else
                    {
                        if (txtPostCode_Ins.Text.Length > 5)
                        {
                            lblPostCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ระบุเลขเกิน 5 ditgit";
                            flag = false;
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblPostCode_Ins.Text = "";
                        }
                    }
                }
                else
                {
                    lblPostCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ระบุเลขไม่ครบ 5 ditgit";
                    flag = false;
                }
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblPostCode_Ins.Text = "";
            }            

            if (txtContactTel_Ins.Text == "")
            {
                flag = false;
                lblContactTel_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เบอร์โทรศัพท์";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblContactTel_Ins.Text = "";
            }

            



            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-inventory').modal();", true);
            }

            return flag;
        }

        public bool ValidateDuplicate()
        {
            bool isDuplicate;
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryValidatePagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = txtInventoryCode_Ins.Text;
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryDetailInfoNew> lInventoryDetailInfoNew = JsonConvert.DeserializeObject<List<InventoryDetailInfoNew>>(respstr);

            if (lInventoryDetailInfoNew.Count > 0)
            {
                isDuplicate = true;
            }
            else
            {
                isDuplicate = false;
            }

            return isDuplicate;

        }

        protected Boolean DeleteInventory()
        {
            for (int i = 0; i < gvInventoryDetailStart.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvInventoryDetailStart.Rows[i].FindControl("chkInventory");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvInventoryDetailStart.Rows[i].FindControl("hidInventoryId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }
                }
            }

            if (Codelist != "")
            {
                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteInventory";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["InventoryCode"] = Codelist;

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

            hidIdList.Value = "";
            return true;

        }
        protected Boolean MoveInventory()
        {
            L_InventoryMove resulta = new L_InventoryMove();


            for (int i = 0; i < gvInventoryDetailStart.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvInventoryDetailStart.Rows[i].FindControl("chkInventory");

                if (checkbox.Checked == true)
                {
                    Codelist = "Y";
                       HiddenField hidCode = (HiddenField)gvInventoryDetailStart.Rows[i].FindControl("hidInventoryId");
                    HiddenField hidProductCode = (HiddenField)gvInventoryDetailStart.Rows[i].FindControl("hidProductCode");
                    TextBox TXTQTY_MOVE = (TextBox)gvInventoryDetailStart.Rows[i].FindControl("TXTQTY_MOVE");
                    try
                    {
                        resulta.L_InventoryDetailInfoNew.Add(new InventoryDetailInfoNew () { InventoryCode = ddlinvStart.SelectedValue, InventoryCodeTo = ddlinvTo.SelectedValue, ProductCode = hidProductCode.Value.ToString().Trim(), QTY = int.Parse(TXTQTY_MOVE.Text) });

                    }
                    catch (Exception ex) 
                    {
                    
                    }

                 
                }
            }
            if (Codelist != "")
            {
                APIpath = APIUrl + "/api/support/InventoryMove";
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jsonObj = JsonConvert.SerializeObject(new { resulta.L_InventoryDetailInfoNew });
                    var dataString = client.UploadString(APIpath, jsonObj);
                    return true;
                }
            }
            else
            {
                hidIdList.Value = "";
                return false;
            }        

        

        }
        protected void InsertInventoryDetailAllProduct(string inventorycode)
        {
            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];

            List<ProductInfo> lpInfo = new List<ProductInfo>();
            lpInfo = GetProductMaster();

            int? sum = 0;

            if (lpInfo.Count > 0)
            {
                foreach (var od in lpInfo.ToList())
                {
                    string respstr = "";

                    APIpath = APIUrl + "/api/support/InsertInventoryDetail";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["InventoryCode"] = inventorycode;
                        data["ProductCode"] = od.ProductCode;
                        data["QTY"] = "0";
                        data["Reserved"] = "0";
                        data["SafetyStock"] = "1";
                        data["Current"] = "0";
                        data["Balance"] = "0";
                        data["CreateBy"] = empInfo.EmpCode;
                        data["UpdateBy"] = empInfo.EmpCode;
                        data["FlagDelete"] = "N";

                        var response = wb.UploadValues(APIpath, "POST", data);
                        respstr = Encoding.UTF8.GetString(response);

                        sum = JsonConvert.DeserializeObject<int?>(respstr);
                    }
                }
            }
        }

        protected List<ProductInfo> GetProductMaster()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductMasterNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = "";
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);

            return lProductInfo;
        }
        #endregion FUNCTION

        #region BINDING
        protected void BindddlProvince()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProvinceNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProvinceCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProvinceInfo> lProvinceInfo = JsonConvert.DeserializeObject<List<ProvinceInfo>>(respstr);


            ddlProvince_Ins.DataSource = lProvinceInfo;

            ddlProvince_Ins.DataTextField = "ProvinceName";

            ddlProvince_Ins.DataValueField = "ProvinceCode";

            ddlProvince_Ins.DataBind();

            ddlProvince_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }

        protected void BindddlDistrict()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListDistrictNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProvinceCode"] = ddlProvince_Ins.SelectedValue;
                data["DistrictCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<DistrictInfo> lDistrictInfo = JsonConvert.DeserializeObject<List<DistrictInfo>>(respstr);


            ddlDistrict_Ins.DataSource = lDistrictInfo;

            ddlDistrict_Ins.DataTextField = "DistrictName";

            ddlDistrict_Ins.DataValueField = "DistrictCode";

            ddlDistrict_Ins.DataBind();

            ddlDistrict_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }

        protected void BindddlSubDistrict()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListSubDistrictNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["DistrictCode"] = ddlDistrict_Ins.SelectedValue;
                data["SubDistrictCode"] = "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<SubDistrictInfo> lSubDistrictInfo = JsonConvert.DeserializeObject<List<SubDistrictInfo>>(respstr);


            ddlSubDistrict_Ins.DataSource = lSubDistrictInfo;

            ddlSubDistrict_Ins.DataTextField = "SubDistrictName";

            ddlSubDistrict_Ins.DataValueField = "SubDistrictCode";

            ddlSubDistrict_Ins.DataBind();

            ddlSubDistrict_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }
        protected void BindDdlInventory()
        {
            List<InventoryDetailInfoNew> linvInfo = new List<InventoryDetailInfoNew>();
            linvInfo = GetListMasterInventoryByCriteria();

            ddlinvStart.DataSource = linvInfo;
            ddlinvStart.DataTextField = "InventoryName";
            ddlinvStart.DataValueField = "InventoryCode";
            ddlinvStart.DataBind();

            ddlinvStart.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

            ddlinvTo.DataSource = linvInfo;
            ddlinvTo.DataTextField = "InventoryName";
            ddlinvTo.DataValueField = "InventoryCode";
            ddlinvTo.DataBind();

            ddlinvTo.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }
        #endregion BINDING

        #region EVENT
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            LoadInventory();
            LoadInventoryTo();
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            ClearSearch();
        }
        protected void btnAddInventory_Click(object sender, EventArgs e)
        {
            hidFlagInsert.Value = "True";
            txtInventoryCode_Ins.Attributes.Remove("disabled");
            txtInventoryCode_Ins.Text = "";
            txtInventoryName_Ins.Text = "";
            txtAddress_Ins.Text = "";
            txtContactTel_Ins.Text = "";
            txtFax_Ins.Text = "";
            txtPostCode_Ins.Text = "";
            ddlProvince_Ins.SelectedValue = "-99";
            ddlDistrict_Ins.SelectedValue = "-99";
            ddlSubDistrict_Ins.SelectedValue = "-99";

            lblInventoryCode_Ins.Text = "";
            lblInventoryName_Ins.Text = "";
            lblAddress_Ins.Text = "";
            lblProvince_Ins.Text = "";
            lblDistrict_Ins.Text = "";
            lblSubDistrict_Ins.Text = "";
            lblPostCode_Ins.Text = "";
            lblContactTel_Ins.Text = "";
            lblFax_Ins.Text = "";

            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-inventory').modal();", true);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteInventory();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('ลบข้อมูลสำเร็จ');", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();
            MerchantInfo merchantInfo = new MerchantInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];
            merchantInfo = (MerchantInfo)Session["MerchantInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                if (hidFlagInsert.Value == "True") //Insert
                {
                    if (validateInsertUpdate())
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertInventory";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["InventoryCode"] = txtInventoryCode_Ins.Text;
                            data["InventoryName"] = txtInventoryName_Ins.Text;
                          
                            data["Address"] = txtAddress_Ins.Text;
                            data["Province"] = ddlProvince_Ins.SelectedValue;
                            data["District"] = ddlDistrict_Ins.SelectedValue;
                            data["SubDistrict"] = ddlSubDistrict_Ins.SelectedValue;
                            data["PostCode"] = txtPostCode_Ins.Text;
                            data["ContactTel"] = txtContactTel_Ins.Text;
                            data["Fax"] = txtFax_Ins.Text;
                            data["FlagDelete"] = "N";
                            data["CreateBy"] = empInfo.EmpCode;


                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);

                        if (sum > 0)
                        {
                            InsertInventoryDetailAllProduct(txtInventoryCode_Ins.Text);

                            btnCancel_Click(null, null);

                            LoadInventory();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-inventory').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                }
                else //Update
                {
                    if (validateInsertUpdate())
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateInventory";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["InventoryId"] = hidIdList.Value;
                            data["InventoryCode"] = txtInventoryCode_Ins.Text;
                            data["InventoryName"] = txtInventoryName_Ins.Text;
                            data["Address"] = txtAddress_Ins.Text;
                            data["Province"] = ddlProvince_Ins.SelectedValue;
                            data["District"] = ddlDistrict_Ins.SelectedValue;
                            data["SubDistrict"] = ddlSubDistrict_Ins.SelectedValue;
                            data["PostCode"] = txtPostCode_Ins.Text;
                            data["ContactTel"] = txtContactTel_Ins.Text;
                            data["Fax"] = txtFax_Ins.Text;
                            data["CreateBy"] = empInfo.EmpCode;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);

                        if (sum > 0)
                        {
                            btnCancel_Click(null, null);

                            LoadInventory();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-inventory').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                        }
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtInventoryCode_Ins.Text = "";
            txtInventoryName_Ins.Text = "";
            txtAddress_Ins.Text = "";
            ddlProvince_Ins.SelectedValue = "-99";
            ddlDistrict_Ins.SelectedValue = "-99";
            ddlSubDistrict_Ins.SelectedValue = "-99";
            txtPostCode_Ins.Text = "";
            txtContactTel_Ins.Text = "";
            txtFax_Ins.Text = "";

            lblInventoryCode_Ins.Text = "";
            lblInventoryName_Ins.Text = "";
            lblAddress_Ins.Text = "";
            lblProvince_Ins.Text = "";
            lblDistrict_Ins.Text = "";
            lblSubDistrict_Ins.Text = "";
            lblPostCode_Ins.Text = "";
            lblContactTel_Ins.Text = "";
            lblFax_Ins.Text = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-inventory').modal('hide');", true);

            
        }

        protected void chkInventoryAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvInventoryDetailStart.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvInventoryDetailStart.HeaderRow.FindControl("chkInventoryAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvInventoryDetailStart.Rows[i].FindControl("hidInventoryDetailID");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkInventory = (CheckBox)gvInventoryDetailStart.Rows[i].FindControl("chkInventory");

                    chkInventory.Checked = true;
                }
                else
                {
                    CheckBox chkInventory = (CheckBox)gvInventoryDetailStart.Rows[i].FindControl("chkInventory");

                    chkInventory.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        protected void gvInventoryDetailStart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvInventoryDetailStart.Rows[index];

            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidInventoryId = (HiddenField)row.FindControl("hidInventoryId");
            HiddenField hidInventoryCode = (HiddenField)row.FindControl("hidInventoryCode");
            HiddenField hidInventoryName = (HiddenField)row.FindControl("hidInventoryName");
            HiddenField hidMerchantCode = (HiddenField)row.FindControl("hidMerchantCode");
            HiddenField hidAddress = (HiddenField)row.FindControl("hidAddress");
            HiddenField hidProvince = (HiddenField)row.FindControl("hidProvince");
            HiddenField hidDistrict = (HiddenField)row.FindControl("hidDistrict");
            HiddenField hidSubDistrict = (HiddenField)row.FindControl("hidSubDistrict");
            HiddenField hidPostCode = (HiddenField)row.FindControl("hidPostCode");
            HiddenField hidContactTel = (HiddenField)row.FindControl("hidContactTel");
            HiddenField hidFax = (HiddenField)row.FindControl("hidFax");

            if (e.CommandName == "ShowInventory")
            {
                try
                {
                    txtInventoryCode_Ins.Attributes.Add("disabled", "true");
                    txtInventoryCode_Ins.Text = hidInventoryCode.Value;
                    hidInventoryCode_Ins.Value = hidInventoryCode.Value;
                    txtInventoryName_Ins.Text = hidInventoryName.Value;
                    txtAddress_Ins.Text = hidAddress.Value;
                    ddlProvince_Ins.SelectedValue = (hidProvince.Value == null || hidProvince.Value == "") ? hidProvince.Value = "-99" : hidProvince.Value;
                    BindddlDistrict();
                    ddlDistrict_Ins.SelectedValue = (hidDistrict.Value == null || hidDistrict.Value == "") ? hidDistrict.Value = "-99" : hidDistrict.Value;
                    BindddlSubDistrict();
                    ddlSubDistrict_Ins.SelectedValue = (hidSubDistrict.Value == null || hidSubDistrict.Value == "") ? hidSubDistrict.Value = "-99" : hidSubDistrict.Value;
                    txtPostCode_Ins.Text = hidPostCode.Value;
                    txtContactTel_Ins.Text = hidContactTel.Value;
                    txtFax_Ins.Text = hidFax.Value;
                    hidIdList.Value = hidInventoryId.Value;
                    hidFlagInsert.Value = "False";

                    lblInventoryCode_Ins.Text = "";
                    lblInventoryName_Ins.Text = "";
                    lblAddress_Ins.Text = "";
                    lblProvince_Ins.Text = "";
                    lblDistrict_Ins.Text = "";
                    lblSubDistrict_Ins.Text = "";
                    lblPostCode_Ins.Text = "";
                    lblContactTel_Ins.Text = "";
                    lblFax_Ins.Text = "";

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-inventory').modal();", true);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }

            }
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadInventory();
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlDistrict();
            BindddlSubDistrict();
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlSubDistrict();
        }
        protected void gvInventoryDetailStart_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblProductCode = (Label)e.Row.FindControl("lblProductCode");
                TextBox TXTQTY_MOVE = (TextBox)e.Row.FindControl("TXTQTY_MOVE");
                HiddenField hidProductImportDup = (HiddenField)e.Row.FindControl("hidProductImportDup");
                HiddenField hidProductCode = (HiddenField)e.Row.FindControl("hidProductCode");
                if (hidProductImportDup.Value == "" || hidProductImportDup.Value == null)
                {

                }
                else
                {
                    lblProductCode.ForeColor = System.Drawing.Color.Red;
                }

          
            }
        }
        protected void btnSubmitImport_Click(object sender, EventArgs e)
        {
            isdelete = MoveInventory();

            btnSearch_Click(null, null);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('บันทึกข้อมูลสำเร็จ');", true);
            
        }
        #endregion EVENT
    }
}