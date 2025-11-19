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
    public partial class CompanyManagement : System.Web.UI.Page
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

                LoadCompany();
                
            }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadCompany();
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

        protected void LoadCompany()
        {
            List<CompanyInfo> lCustomerInfo = new List<CompanyInfo>();

            int? totalRow = CountCompanyMasterList();

            SetPageBar(Convert.ToDouble(totalRow));

            lCustomerInfo = GetCompanyMasterByCriteria();

            gvCompany.DataSource = lCustomerInfo;

            gvCompany.DataBind();


        }


        public List<CompanyInfo> GetCompanyMasterByCriteria()
        {
            string respstr = "";
          
            APIpath = APIUrl + "/api/support/ListCompanyPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CompanyCode"] = txtSearchCompanyCode.Text;

                data["CompanyNameTH"] = txtSearchCompanyNameTH.Text;

                data["CompanyNameEN"] = txtSearchCompanyNameEN.Text;

                data["MerchantMapCode"] = hidMerchantCode.Value;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();


                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CompanyInfo> lCompanyInfo = JsonConvert.DeserializeObject<List<CompanyInfo>>(respstr);


            return lCompanyInfo;

        }
        public List<CompanyInfo> GetCompanyMasterByCriteria(string CompanyCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCompanyPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CompanyCode"] = CompanyCode;
                
                data["TechnicianCode"] = "Y";

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();


                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CompanyInfo> lCompanyInfo = JsonConvert.DeserializeObject<List<CompanyInfo>>(respstr);


            return lCompanyInfo;

        }

        public int? CountCompanyMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountCompanyListPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();


                data["CustomerCode"] = txtSearchCompanyCode.Text;

                data["CustomerFName"] = txtSearchCompanyNameTH.Text;

                data["CustomerLName"] = txtSearchCompanyNameEN.Text;

                data["MerchantMapCode"] = hidMerchantCode.Value;

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


            LoadCompany();
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

        protected Boolean DeleteCompany()
        {
          
            for (int i = 0; i < gvCompany.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvCompany.Rows[i].FindControl("chk");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvCompany.Rows[i].FindControl("hidCompanyId");
                    HiddenField hidCompanyCode = (HiddenField)gvCompany.Rows[i].FindControl("hidCompanyCode");

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

                APIpath = APIUrl + "/api/support/DeleteCompany";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CompanyId_str"] = Codelist;


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

        protected void DeleteCompanyById(string pId)
        {

            if (pId != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteCompany";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CompanyId"] = pId;


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
        protected void UpdateProductMapRecipe(string pCode)
        {

            if (pCode != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/UpdateClearProductMapRecipe";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["ProductCode"] = pCode;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            }
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
            List<CompanyInfo> lCompanyInfo = new List<CompanyInfo>();
            lCompanyInfo = GetCompanyMasterByCriteria(txtCompanyCode_Ins.Text);

            if (txtCompanyCode_Ins.Text == "")
            {
                flag = false;
                lblCompanyCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสร้านค้า ให้ครบถ้วน";
            }
            else
            {
                if(hidFlagInsert.Value == "True")
                {
                    if (lCompanyInfo.Count() > 0)
                    {
                        flag = false;
                        lblCompanyCode_Ins.Text = "รหัสร้านค้านี้ถูกใช้แล้ว";
                    }
                    else
                    {
                        flag = (flag == false) ? false : true;
                        lblCompanyCode_Ins.Text = "";
                    }
                }
                else
                {
                    if (hidComCode.Value != txtCompanyCode_Ins.Text)
                    {
                        List<CompanyInfo> lCompanyin = new List<CompanyInfo>();
                        lCompanyin = GetCompanyMasterByCriteria(txtCompanyCode_Ins.Text);
                        if (lCompanyin.Count() > 0)
                        {
                            flag = false;
                            lblCompanyCode_Ins.Text = "รหัสร้านค้านี้ถูกใช้แล้ว";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblCompanyCode_Ins.Text = "";
                        }
                    }
                    else
                    {
                        flag = (flag == false) ? false : true;
                        lblCompanyCode_Ins.Text = "";
                    }
                }

            }
            if (txtCompanyNameTH_Ins.Text == "")
            {
                flag = false;
                lblCompanyNameTH_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อร้านค้า(ไทย) ให้ครบถ้วน";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblCompanyNameTH_Ins.Text = "";
            }
            if (txtCompanyNameEN_Ins.Text == "")
            {
                flag = false;
                lblCompanyNameEN_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อร้านค้า(EN) ให้ครบถ้วน";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblCompanyNameEN_Ins.Text = "";
            }
          

            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-company').modal();", true);
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

            protected void gvCompany_Change(object sender, GridViewPageEventArgs e)
        {
            gvCompany.PageIndex = e.NewPageIndex;

            List<CompanyInfo> lCompanyInfo = new List<CompanyInfo>();

            lCompanyInfo = GetCompanyMasterByCriteria();

            gvCompany.DataSource = lCompanyInfo;

            gvCompany.DataBind();

        }

        protected void chkAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvCompany.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvCompany.HeaderRow.FindControl("chkAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvCompany.Rows[i].FindControl("hidCompanyId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chk = (CheckBox)gvCompany.Rows[i].FindControl("chk");

                    chk.Checked = true;
                }
                else
                {

                    CheckBox chk = (CheckBox)gvCompany.Rows[i].FindControl("chk");

                    chk.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }
    
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            LoadCompany();
        }
  


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteCompany();

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
                        string respstr = "";

                       APIpath = APIUrl + "/api/support/InsertCompany"; //Insert 

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CompanyCode"] = txtCompanyCode_Ins.Text;
                            data["CompanyNameTH"] = txtCompanyNameTH_Ins.Text;
                            data["CompanyNameEN"] = txtCompanyNameEN_Ins.Text;
                            data["MerchantMapCode"] = merchantinfo.MerchantCode;
                            data["FlagDelete"] = "N";
                            data["CreateBy"] = empInfo.EmpCode;
                            data["UpdateBy"] = empInfo.EmpCode;


                            var response = wb.UploadValues(APIpath, "POST", data);
                       

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);
                        

                        if (sum > 0)
                        {
                            
                                btnCancel_Click(null, null);

                            LoadCompany();

                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-company').modal('hide');", true);
                            

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                    else //Update
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateCompany";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CompanyId"] = hidIdList.Value;
                            data["CompanyCode"] = txtCompanyCode_Ins.Text;
                            data["CompanyNameTH"] = txtCompanyNameTH_Ins.Text;
                            data["CompanyNameEN"] = txtCompanyNameEN_Ins.Text;
                            data["FlagDelete"] = "N";
                            data["UpdateBy"] = empInfo.EmpCode;


                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            LoadCompany();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-company').modal('hide');", true);



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

            txtCompanyCode_Ins.Text = "";
            lblCompanyCode_Ins.Text = "";

            txtCompanyNameTH_Ins.Text = "";
            lblCompanyNameTH_Ins.Text = "";


            txtCompanyNameEN_Ins.Text = "";
            lblCompanyNameEN_Ins.Text = "";

  
         
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchCompanyCode.Text = "";
            txtSearchCompanyNameTH.Text = "";
            txtSearchCompanyNameEN.Text = "";
           

        }

        protected void gvCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvCompany.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidCompanyId = (HiddenField)row.FindControl("hidCompanyId");
            HiddenField hidCompanyCode = (HiddenField)row.FindControl("hidCompanyCode");
           
            HiddenField hidCompanyNameTH = (HiddenField)row.FindControl("hidCompanyNameTH");
            HiddenField hidCompanyNameEN = (HiddenField)row.FindControl("hidCompanyNameEN");
           



            if (e.CommandName == "ShowCompany")
            {


                txtCompanyCode_Ins.Text = hidCompanyCode.Value;
                txtCompanyNameTH_Ins.Text = hidCompanyNameTH.Value;
                txtCompanyNameEN_Ins.Text = hidCompanyNameEN.Value;




                
                hidComCode.Value = hidCompanyCode.Value;
                hidIdList.Value = hidCompanyId.Value;
                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-company').modal();", true);


            }

            if (e.CommandName == "DeleteCompany")
            {
                DeleteCompanyById(hidCompanyId.Value);
                btnSearch_Click(null, null);
            }

        }

        protected void btnAddCompany_Click(object sender, EventArgs e)
        {
            
            hidFlagInsert.Value = "True";

            txtCompanyCode_Ins.Text = "";
            lblCompanyCode_Ins.Text = "";

            txtCompanyNameTH_Ins.Text = "";
            lblCompanyNameTH_Ins.Text = "";


            txtCompanyNameEN_Ins.Text = "";
            lblCompanyNameEN_Ins.Text = "";


            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-company').modal();", true);
        }

        #endregion

        #region Binding

    
       
        #endregion
    }
}