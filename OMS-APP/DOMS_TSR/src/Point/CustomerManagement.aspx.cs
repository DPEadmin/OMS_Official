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

namespace DOMS_TSR.src.Point
{
    public partial class CustomerManagement : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string ProductImgUrl = ConfigurationManager.AppSettings["ProductImageUrl"];

        string Codelist = "";
        string Showcase_img11Upload_Name = "";
        string Showcase_img43Upload_Name = "";
        string SKUimg1Upload_Name = "";
       

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
                MerchantInfo merchantinfo = new MerchantInfo();
                merchantinfo = (MerchantInfo)Session["MerchantInfo"];
                empInfo = (EmpInfo)Session["EmpInfo"];
               

                if (empInfo != null && merchantinfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerchantCode.Value = merchantinfo.MerchantCode;
                    
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                LoadCustomer();
                
                BindddlPointRange();
            }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadCustomer();
        }


        #region Function
        protected int? loadcountCustomer()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountCustomerListByCriteriaMaster";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }

        protected void LoadCustomer()
        {
            List<CustomerInfo> lCustomerInfo = new List<CustomerInfo>();

            int? totalRow = CountCustomerMasterList();

            SetPageBar(Convert.ToDouble(totalRow));
            
            lCustomerInfo = GetCustomerMasterByCriteria();

            gvCustomer.DataSource = lCustomerInfo;

            gvCustomer.DataBind();


        }


        public List<CustomerInfo> GetCustomerMasterByCriteria()
        {
            string respstr = "";
            List<PointInfo> lPointInfo = new List<PointInfo>();
            lPointInfo = ListPointRangeNopaging();

            

            APIpath = APIUrl + "/api/support/ListCustomerListPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = txtSearchCustomerCode.Text;

                data["CustomerFName"] = txtSearchCustomerFName.Text;

                data["CustomerLName"] = txtSearchCustomerLName.Text;

                data["Mail"] = txtSearchCustomerEmail.Text;

                data["ContactTel"] = txtSearchCustomerTel.Text;

                data["MerchantCode"] = hidMerchantCode.Value;

                data["PointRangeCode"] = ddlPointRange_Search.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();


                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CustomerInfo> lCustomerInfo = JsonConvert.DeserializeObject<List<CustomerInfo>>(respstr);


            return lCustomerInfo;

        }

        public int? CountCustomerMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountCustomerListPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();


                data["CustomerCode"] = txtSearchCustomerCode.Text;

                data["CustomerFName"] = txtSearchCustomerFName.Text;

                data["CustomerLName"] = txtSearchCustomerLName.Text;

                data["Mail"] = txtSearchCustomerEmail.Text;

                data["ContactTel"] = txtSearchCustomerTel.Text;

                data["PointRangeCode"] = ddlPointRange_Search.SelectedValue;

                data["MerchantCode"] = hidMerchantCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int?  cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


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


            LoadCustomer();
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

        protected Boolean DeleteCustomer()
        {
            InventoryDetailInfoNew ind = new InventoryDetailInfoNew();
            List<InventoryDetailInfoNew> lind = new List<InventoryDetailInfoNew>(); 

            for (int i = 0; i < gvCustomer.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvCustomer.Rows[i].FindControl("chkCustomer");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvCustomer.Rows[i].FindControl("hidCustomerId");
                    HiddenField hidCustomerCode = (HiddenField)gvCustomer.Rows[i].FindControl("hidCustomerCode");

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

                APIpath = APIUrl + "/api/support/DeleteCustomer";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CusIdforDelete"] = Codelist;


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

        protected void DeleteProductById(string pId)
        {

            if (pId != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteCustomer";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CustomerId"] = pId;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);

                if(cou > 0)
                {

                }
            }


        }

        public List<InventoryInfo> GetInventoryMaster()
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

            List<InventoryInfo> lInventoryInfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(respstr);

            return lInventoryInfo;
        }
        public List<InventoryDetailInfo> GetInventoryDetailIDMaster(string inventorycode, string productcode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryDetailInfoNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = inventorycode;
                data["ProductCode"] = productcode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryDetailInfo> lInventoryDetailInfo = JsonConvert.DeserializeObject<List<InventoryDetailInfo>>(respstr);

            return lInventoryDetailInfo;
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

        protected Boolean validateInsertUpdate()
        {
            Boolean flag = true;
            int LastPointEnd = 0;
            string LastPointCode = "";
            List<PointInfo> lPointInfo = new List<PointInfo>();
            lPointInfo = ListPointRangeNopaging();



            if (txtCustomerFName_Ins.Text == "" && txtCustomerLName_Ins.Text == "")
            {
                flag = false;
                lblCustomerName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อ-นามสกุล สมาชิก ให้ครบถ้วน";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblCustomerName_Ins.Text = "";
            }
            if (txtCustomerTel_Ins.Text == "")
            {
                flag = false;
                lblCustomerTel_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เบอร์โทรศัพท์";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblCustomerTel_Ins.Text = "";
            }
            if (txtCustomerEmail_Ins.Text == "")
            {
                flag = false;
                lblCustomerEmail_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Email";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblCustomerEmail_Ins.Text = "";
            } 
            if (txtPointNum_Ins.Text == "")
            {
                txtPointNum_Ins.Text = "0";
            }

            foreach (var item in lPointInfo)
            {
                
                if (int.Parse(txtPointNum_Ins.Text) >= int.Parse(item.PointBegin.ToString()) && int.Parse(txtPointNum_Ins.Text) <= int.Parse(item.PointEnd.ToString()))
                {
                    hidPointRangeCode_Ins.Value = item.PointCode.ToString();
                }
                else
                {
                    hidPointRangeCode_Ins.Value = (hidPointRangeCode_Ins.Value == "") ? "" : hidPointRangeCode_Ins.Value;
                }
                LastPointEnd = int.Parse(item.PointEnd.ToString());
                LastPointCode = item.PointCode.ToString();
            }

            if (hidPointRangeCode_Ins.Value == "" && int.Parse(txtPointNum_Ins.Text) > LastPointEnd)
            {
                hidPointRangeCode_Ins.Value = LastPointCode;
            }
            



            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-customer').modal();", true);
            }

            return flag;
        }

        protected void UpdatePromotionDetail(string productcode, string productprice)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/UpdatePromoDetailInfoByProductCode";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = productcode;
                data["Price"] = productprice;
                data["FlagDelete"] = "N";
                data["UpdateBy"] = hidEmpCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
        }

            #endregion

            #region Event 

            protected void gvCustomer_Change(object sender, GridViewPageEventArgs e)
        {
            gvCustomer.PageIndex = e.NewPageIndex;

            List<CustomerInfo> lCustomerInfo = new List<CustomerInfo>();

            lCustomerInfo = GetCustomerMasterByCriteria();

            gvCustomer.DataSource = lCustomerInfo;

            gvCustomer.DataBind();

        }

        protected void chkCustomerAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvCustomer.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvCustomer.HeaderRow.FindControl("chkCustomerAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvCustomer.Rows[i].FindControl("hidCustomerId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkCustomer = (CheckBox)gvCustomer.Rows[i].FindControl("chkCustomer");

                    chkCustomer.Checked = true;
                }
                else
                {

                    CheckBox chkCustomer = (CheckBox)gvCustomer.Rows[i].FindControl("chkCustomer");

                    chkCustomer.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }
    
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            LoadCustomer();
        }
  


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteCustomer();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

          
            POInfo pInfo = new POInfo();
            MerchantInfo merchantinfo = new MerchantInfo();

            merchantinfo = (MerchantInfo)Session["MerchantInfo"];
            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                if (validateInsertUpdate()) 
                {
                    if (hidFlagInsert.Value == "True") //Insert
                    {
                        int? count = loadcountCustomer();
                        string CustomerCode = "C" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(5, '0');

                        string respstr = "";

                       APIpath = APIUrl + "/api/support/InsertCustomerofOMS"; //Insert 

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CustomerCode"] = CustomerCode;
                            data["CustomerFName"] = txtCustomerFName_Ins.Text;
                            data["CustomerLName"] = txtCustomerLName_Ins.Text;
                            data["MerchantCode"] = merchantinfo.MerchantCode;
                            data["PointNum"] = txtPointNum_Ins.Text;
                            data["PointRangeCode"] = hidPointRangeCode_Ins.Value;
                            data["Mail"] = txtCustomerEmail_Ins.Text;
                            data["ContactTel"] = txtCustomerTel_Ins.Text;
                            data["FlagDelete"] = "N";
                            data["CreateBy"] = empInfo.EmpCode;
                            data["UpdateBy"] = empInfo.EmpCode;


                            var response = wb.UploadValues(APIpath, "POST", data);
                       

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);
                        int? sum1 = 0;

                        if (sum > 0)
                        {
                            
                                btnCancel_Click(null, null);

                                LoadCustomer();

                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-customer').modal('hide');", true);
                            

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                    else //Update
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateCustomer";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CustomerId"] = hidIdList.Value;
                            data["CustomerCode"] = txtCustomerCode_Ins.Text;
                            data["CustomerFName"] = txtCustomerFName_Ins.Text;
                            data["CustomerLName"] = txtCustomerLName_Ins.Text;
                            data["MerchantCode"] = merchantinfo.MerchantCode;
                            data["PointNum"] = txtPointNum_Ins.Text;
                            data["PointRangeCode"] = hidPointRangeCode_Ins.Value;
                            data["Mail"] = txtCustomerEmail_Ins.Text;
                            data["ContactTel"] = txtCustomerTel_Ins.Text;
                            data["FlagDelete"] = "N";
                            data["UpdateBy"] = empInfo.EmpCode;


                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            LoadCustomer();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-customer').modal('hide');", true);



                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                        }

                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                }
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

            txtCustomerCode_Ins.Text = "";
            lblCustomerCode_Ins.Text = "";

            txtCustomerFName_Ins.Text = "";
            lblCustomerName_Ins.Text = "";

            txtCustomerLName_Ins.Text = "";

            txtCustomerTel_Ins.Text = "";
            lblCustomerTel_Ins.Text = "";

            txtCustomerEmail_Ins.Text = "";
            lblCustomerEmail_Ins.Text = "";

            txtPointNum_Ins.Text = "";
            lblPointNum_Ins.Text = "";

            
           
         
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchCustomerCode.Text = "";
            txtSearchCustomerFName.Text = "";
            txtSearchCustomerLName.Text = "";
            txtSearchCustomerTel.Text = "";
            txtSearchCustomerEmail.Text = "";
            ddlPointRange_Search.ClearSelection();

        }

        protected void gvCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvCustomer.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidCustomerId = (HiddenField)row.FindControl("hidCustomerId");
            HiddenField hidCustomerCode = (HiddenField)row.FindControl("hidCustomerCode");
           
            HiddenField hidCustomerFName = (HiddenField)row.FindControl("hidCustomerFName");
            HiddenField hidCustomerLName = (HiddenField)row.FindControl("hidCustomerLName");
            HiddenField hidCustomerTel = (HiddenField)row.FindControl("hidCustomerTel"); 
            HiddenField hidCustomerEmail = (HiddenField)row.FindControl("hidCustomerEmail");
            HiddenField hidCustomerPointNum = (HiddenField)row.FindControl("hidCustomerPointNum");



            if (e.CommandName == "ShowCustomer")
            {


                txtCustomerFName_Ins.Text = hidCustomerFName.Value;
                txtCustomerLName_Ins.Text = hidCustomerLName.Value;
                txtCustomerCode_Ins.Text = hidCustomerCode.Value;
                txtCustomerCode_Ins.Enabled = false;
                hidCustomerCode_Ins.Value = hidCustomerCode.Value;
                txtCustomerTel_Ins.Text = hidCustomerTel.Value;
                txtCustomerEmail_Ins.Text = hidCustomerEmail.Value;
                txtPointNum_Ins.Text = hidCustomerPointNum.Value;


                

                hidIdList.Value = hidCustomerId.Value;
                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-customer').modal();", true);


            }

            if (e.CommandName == "DeleteCustomer")
            {
                DeleteProductById(hidCustomerId.Value);
                btnSearch_Click(null, null);
            }

        }

        protected void btnAddCustomer_Click(object sender, EventArgs e)
        {
            txtCustomerCode_Ins.Enabled = false;
            hidFlagInsert.Value = "True";

            txtCustomerCode_Ins.Text = "";
            lblCustomerCode_Ins.Text = "";

            txtCustomerFName_Ins.Text = "";
            lblCustomerName_Ins.Text = "";

            txtCustomerLName_Ins.Text = "";

            txtCustomerTel_Ins.Text = "";
            lblCustomerTel_Ins.Text = "";

            txtCustomerEmail_Ins.Text = "";
            lblCustomerEmail_Ins.Text = "";

            txtPointNum_Ins.Text = "";
            lblPointNum_Ins.Text = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-customer').modal();", true);
        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"ReportCustomerPoint.aspx?CustomerCode=" + strCode + "\">" + strCode + "</a>";
        }

        protected List<PointInfo> ListPointRangeNopaging()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListPointRangePagingbyCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantMapCode"] = hidMerchantCode.Value;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PointInfo> lPointInfo = JsonConvert.DeserializeObject<List<PointInfo>>(respstr);
            return lPointInfo;
        }
        protected void BindddlPointRange()
        {
           

            List<PointInfo> lPointInfo = new List<PointInfo>();
            lPointInfo = ListPointRangeNopaging();


            ddlPointRange_Search.DataSource = lPointInfo;

            ddlPointRange_Search.DataTextField = "PointName";

            ddlPointRange_Search.DataValueField = "PointCode";

            ddlPointRange_Search.DataBind();

            ddlPointRange_Search.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }
      
        

       
        #endregion
    }
}