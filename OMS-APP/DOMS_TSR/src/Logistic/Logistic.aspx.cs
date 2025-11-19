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
using System.Text.RegularExpressions;

namespace DOMS_TSR.src.Logistic
{
    public partial class Logistic : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];

        string Codelist = "";
   
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

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    
                    hidEmpCode.Value = empInfo.EmpCode;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                BindddlLogisticConditoin();
                BindddlLogisticTyper();
                ViewState["RowNumber"] = 1;
            
                LoadLogistic();
              
              }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadLogistic();
        }

        protected void BindddlLogisticTyper()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "LOGISTICTYPE";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlLogistype_Ins.DataSource = lLookupInfo;
            ddlLogistype_Ins.DataTextField = "LookupValue";
            ddlLogistype_Ins.DataValueField = "LookupCode";
            ddlLogistype_Ins.DataBind();
            ddlLogistype_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", ""));


            ddlsearchLogictype.DataSource = lLookupInfo;
            ddlsearchLogictype.DataTextField = "LookupValue";
            ddlsearchLogictype.DataValueField = "LookupCode";
            ddlsearchLogictype.DataBind();
            ddlsearchLogictype.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", ""));
        }
        protected void BindddlLogisticConditoin()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "LOGISTICCONDITON";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);
            ClCondition_Ins.DataSource = lLookupInfo;
            ClCondition_Ins.DataTextField = "LookupValue";
            ClCondition_Ins.DataValueField = "LookupCode";
            ClCondition_Ins.DataBind();
             
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Checking whether the Row is Data Row
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }
        }

        
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            
        }
        //A method that returns a string which calls the connection string from the web.config
        private string GetConnectionString()
        {
            //"DBConnection" is the name of the Connection String
            //that was set up from the web.config file
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        }
        //A method that Inserts the records to the database
        private void InsertRecords(StringCollection sc)
        {
            
            StringBuilder sb = new StringBuilder(string.Empty);
            string[] splitItems = null;
            foreach (string item in sc)
            {

                const string sqlStatement = "INSERT INTO CreatePR_Detail (Product_code,Description,Quantity,Estimate_price,Unit,CreatePR_No) VALUES";
                if (item.Contains(","))
                {
                    splitItems = item.Split(",".ToCharArray());
                    sb.AppendFormat("{0}('{1}','{2}','{3}','{4}','{5}','{6}'); ", sqlStatement, splitItems[0], splitItems[1], splitItems[2], splitItems[3], splitItems[4], splitItems[5]);
                }

            }

            try
            {
                

                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Records Successfuly Saved!');", true);

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
                throw new Exception(msg);

            }
            finally
            {
                
            }
        }
        // Hide the Remove Button at the last row of the GridView
        protected void Gridview1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton1");
                    if (lb != null)
                    {
                        if (dt.Rows.Count > 1)
                        {
                            if (e.Row.RowIndex == dt.Rows.Count - 1)
                            {
                                lb.Visible = false;
                            }
                        }
                        else
                        {
                            lb.Visible = false;
                        }
                    }
                }
              
            }
        }
    

        #region Function

        protected void clearAdddata()
        {
            txtLogisticCode_Ins.Text = "";
          txtLogisticName_Ins.Text = "";
            txtLogisticCode_Ins.Text = "";
            ddlActive_Ins.SelectedValue = "";
            txtEstimatedTime_Ins.Text = "";
            ddlActive_Ins.ClearSelection();
            ddlLogistype_Ins.ClearSelection();
            ClCondition_Ins.ClearSelection();
        }

        protected void LoadLogistic()
        {
            List<LogisticInfo> lProductInfo = new List<LogisticInfo>();

            

            int? totalRow = CountProductMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lProductInfo = GetProductMasterByCriteria();

            gvLogistic.DataSource = lProductInfo;

            gvLogistic.DataBind();


          

        }

        public List<LogisticInfo> GetProductMasterByCriteria()
        {
            string respstr = "";
         
            APIpath = APIUrl + "/api/support/ListLogisticPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LogisticCode"] = txtSearchLogisticCode.Text;

                data["LogisticName"] = txtSearchLogisticName.Text;

                data["LogisticType"] = ddlsearchLogictype.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LogisticInfo> lProductInfo = JsonConvert.DeserializeObject<List<LogisticInfo>>(respstr);


            return lProductInfo;

        }

        public int? CountProductMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountLogisticListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LogisticCode"] = txtSearchLogisticCode.Text;

                data["LogisticName"] = txtSearchLogisticName.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }
        public List<LogisticDetailSelectInfo> GetLogisticDetailByCriteria(string code)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLogisticDetailPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LogisticCodeDetail"] = code;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LogisticDetailSelectInfo> lProductInfo = JsonConvert.DeserializeObject<List<LogisticDetailSelectInfo>>(respstr);


            return lProductInfo;

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


            LoadLogistic();
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

        protected Boolean DeleteProduct()
        {

            for (int i = 0; i < gvLogistic.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvLogistic.Rows[i].FindControl("chkProduct");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvLogistic.Rows[i].FindControl("hidLogisticId");

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

                APIpath = APIUrl + "/api/support/DeleteLogistic";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["LogisticIdDelete"] = Codelist;


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
        protected Boolean ValidateInsertUpdate()
        {
            Boolean flag = true;

            if (txtLogisticCode_Ins.Text == "")
            {
                flag = false;
                lblLogisticCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสขนส่งสินค้า";
            }
            else if (CheckSymbol(txtLogisticCode_Ins.Text) == true)
            {
                flag = false;
                lblLogisticCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + "รหัสขนส่งสินค้าต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                if (hidFlagInsert.Value == "True")
                {
                    if (txtLogisticCode_Ins.Text != "")
                    {
                        Boolean isDuplicate = ValidateDuplicate();

                        if (isDuplicate)
                        {
                            flag = false;
                            lblLogisticCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสขนส่งสินค้าซ้ำในระบบ";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblLogisticCode_Ins.Text = "";
                        }
                    }
                }
            }
            if (txtLogisticName_Ins.Text == "")
            {
                flag = false;
                lblLogisticName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อการขนส่งสินค้า";
            }
            else if (CheckSymbol(txtLogisticName_Ins.Text) == true)
            {
                flag = false;
                lblLogisticName_Ins.Text = MessageConst._MSG_PLEASEINSERT + "ชื่อการขนส่งสินค้าต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblLogisticName_Ins.Text = "";
            }

            if (ddlLogistype_Ins.SelectedValue == "-99" || ddlLogistype_Ins.SelectedValue == "")
            {
                flag = false;
                lblogistype_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชนิดการขนส่งสินค้า";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblogistype_Ins.Text = "";
            }

            if (txtEstimatedTime_Ins.Text == "")
            {
                flag = false;
                lbEstimatedTime_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ระยะเวลาการขนส่ง";
            }
            else if (CheckSymbol(txtEstimatedTime_Ins.Text) == true)
            {
                flag = false;
                lbEstimatedTime_Ins.Text = MessageConst._MSG_PLEASEINSERT + "ระยะเวลาการขนส่งต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lbEstimatedTime_Ins.Text = "";
            }

            if (ddlActive_Ins.SelectedValue == "-99" || ddlActive_Ins.SelectedValue == "")
            {
                flag = false;
                lblActive_Ins.Text = MessageConst._MSG_PLEASEINSERT + " สถานะการขนส่งสินค้า";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblActive_Ins.Text = "";
            }

            if (txtFee_Ins.Text == "")
            {
                flag = false;
                lblFee_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ค่าธรรมเนียมการขนส่งสินค้า";
            }else if (CheckSymbol(txtFee_Ins.Text) == true)
            {
                flag = false;
                lblFee_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ค่าธรรมเนียมการขนส่งสินค้าต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblFee_Ins.Text = "";
            }

            if (flag == false)
            {
                if (flag == false)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Logistic').modal();", true);
                }
            }

            return flag;
        }
        public bool ValidateDuplicate()
        {
            bool isDuplicate;
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLogisticCodeValidateCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LogisticCode"] = txtLogisticCode_Ins.Text;
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryInfo> lInventoryInfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(respstr);

            if (lInventoryInfo.Count > 0)
            {
                isDuplicate = true;
            }
            else
            {
                isDuplicate = false;
            }

            return isDuplicate;

        }

        #endregion

        #region Event 

        protected void gvProduct_Change(object sender, GridViewPageEventArgs e)
        {
            gvLogistic.PageIndex = e.NewPageIndex;

            List<LogisticInfo> lProductInfo = new List<LogisticInfo>();

            lProductInfo = GetProductMasterByCriteria();

            gvLogistic.DataSource = lProductInfo;

            gvLogistic.DataBind();

        }

        protected void chkProductAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvLogistic.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvLogistic.HeaderRow.FindControl("chkProductAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvLogistic.Rows[i].FindControl("hidLogisticId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkProduct = (CheckBox)gvLogistic.Rows[i].FindControl("chkProduct");

                    chkProduct.Checked = true;
                }
                else
                {

                    CheckBox chkProduct = (CheckBox)gvLogistic.Rows[i].FindControl("chkProduct");

                    chkProduct.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (validateSearch())
            {
                currentPageNumber = 1;
                LoadLogistic();
            }
          

        }



        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteProduct();

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

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                if (hidFlagInsert.Value == "True") //Insert
                {
                    if (ValidateInsertUpdate())
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertLogistic";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["LogisticCode"] = txtLogisticCode_Ins.Text;
                            data["LogisticName"] = txtLogisticName_Ins.Text;
                            data["LogisticType"] = ddlLogistype_Ins.SelectedValue;
                            data["Status"] = ddlActive_Ins.SelectedValue;
                            data["EstimatedTime"] = txtEstimatedTime_Ins.Text;
                            data["Fee"] = txtFee_Ins.Text;
                            data["CreateBy"] = empInfo.EmpCode;
                            if (ClCondition_Ins.Items[0].Selected)
                            {
                                data["TypeCalWeight"] = "Y";
                            }
                            else
                            {
                                data["TypeCalWeight"] = "N";
                            }

                            if (ClCondition_Ins.Items[1].Selected)
                            {
                                data["TypeCalSize"] = "Y";
                            }
                            else
                            {
                                data["TypeCalSize"] = "N";
                            }

                            if (ClCondition_Ins.Items[2].Selected)
                            {
                                data["TypeCalWeightSize"] = "Y";
                            }
                            else
                            {
                                data["TypeCalWeightSize"] = "N";
                            }



                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);

                        if (sum > 0)
                        {
                            btnCancel_Click(null, null);

                            LoadLogistic();
                            clearAdddata();
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-Logistic').modal('hide');", true);
                        }
                    }
                    else
                    {
                        
                    }

                }
                else //Update
                {
                    if (ValidateInsertUpdate())
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateLogistic";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["LogisticId"] = hidIdList.Value;

                            data["LogisticCode"] = txtLogisticCode_Ins.Text;
                            data["LogisticName"] = txtLogisticName_Ins.Text;
                            data["LogisticType"] = ddlLogistype_Ins.SelectedValue;
                            data["Fee"] = txtFee_Ins.Text;
                            data["UpdateBy"] = empInfo.EmpCode;
                            data["status"] = ddlActive_Ins.SelectedValue;
                            data["EstimatedTime"] = txtEstimatedTime_Ins.Text;

                            for (int i = 0; i < ClCondition_Ins.Items.Count; i++)
                            {
                                if (ClCondition_Ins.Items[0].Selected)
                                {
                                    data["TypeCalWeight"] = "Y";
                                }
                                else
                                {
                                    data["TypeCalWeight"] = "N";
                                }
                                if (ClCondition_Ins.Items[1].Selected)
                                {
                                    data["TypeCalSize"] = "Y";
                                }
                                else
                                {
                                    data["TypeCalSize"] = "N";
                                }
                                if (ClCondition_Ins.Items[2].Selected)
                                {
                                    data["TypeCalWeightSize"] = "Y";
                                }
                                else
                                {
                                    data["TypeCalWeightSize"] = "N";
                                }
                            }

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {
                            btnCancel_Click(null, null);

                            LoadLogistic();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-Logistic').modal('hide');", true);

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
            txtLogisticCode_Ins.Text = "";
            txtLogisticName_Ins.Text = "";
           
            HttpFileCollection uploadFiles = Request.Files;
            for (int i = 0; i < uploadFiles.Count; i++)
            {
                HttpPostedFile postedFile = uploadFiles[i];
                string x = postedFile.FileName;
                int y = postedFile.ContentLength;

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Logistic').modal('hide');", true);
        }
        protected void btnEditSubmit_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            POInfo pInfo = new POInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                if (hidFlagInsert.Value == "True") //Insert
                {
                    string respstr = "";
                    string respstrDetail = "";
                    APIpath = APIUrl + "/api/support/InsertLogistic";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["LogisticCode"] = txtLogisticCode_Ins.Text;
                        data["LogisticName"] = txtLogisticName_Ins.Text;

                        data["CreateBy"] = empInfo.EmpCode;


                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);
                }
                else //Update
                {
                    string respstr = "";

                    APIpath = APIUrl + "/api/support/UpdateLogistic";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["LogisticId"] = hidIdList.Value;

                        data["LogisticCode"] = txtEditLogisCode.Text;
                        data["LogisticName"] = txtEditLogisName.Text;
                        data["UpdateBy"] = empInfo.EmpCode;



                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                    if (sum > 0)
                    {
                        btnCancel_Click(null, null);

                        LoadLogistic();

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-Logistic').modal('hide');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                    }

                }

            }

        }
        protected void btnEditCancel_Click(object sender, EventArgs e)
        {
            txtLogisticCode_Ins.Text = "";
            txtLogisticName_Ins.Text = "";

            HttpFileCollection uploadFiles = Request.Files;
            for (int i = 0; i < uploadFiles.Count; i++)
            {
                HttpPostedFile postedFile = uploadFiles[i];
                string x = postedFile.FileName;
                int y = postedFile.ContentLength;

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-EditLogistic').modal('hide');", true);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {

        }

        protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvLogistic.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidLogisticId = (HiddenField)row.FindControl("hidLogisticId");
            HiddenField hidLogisticCode = (HiddenField)row.FindControl("hidLogisticCode");
            HiddenField hidLogisticName = (HiddenField)row.FindControl("hidLogisticName");
            HiddenField hidEstimatedTime = (HiddenField)row.FindControl("hidEstimatedTime");
            HiddenField hidLogisticType = (HiddenField)row.FindControl("hidLogisticType");
            HiddenField hidLogisticstatus = (HiddenField)row.FindControl("hidLogisticstatus");
            HiddenField hidTypeCalWeight = (HiddenField)row.FindControl("hidTypeCalWeight");
            HiddenField hidTypeCalSize = (HiddenField)row.FindControl("hidTypeCalSize");
            HiddenField hidTypeCalWeightSize = (HiddenField)row.FindControl("hidTypeCalWeightSize");
            HiddenField hidFee = (HiddenField)row.FindControl("hidFee");

            if (e.CommandName == "ShowProduct")
            {
              
                txtLogisticCode_Ins.Text = hidLogisticCode.Value;
                txtLogisticName_Ins.Text = hidLogisticName.Value;
                ddlLogistype_Ins.SelectedValue = hidLogisticType.Value;
                txtEditLogisCode.Text = hidLogisticCode.Value;
                txtEditLogisName.Text = hidLogisticName.Value;
                txtEstimatedTime_Ins.Text = hidEstimatedTime.Value;
                ddlActive_Ins.SelectedValue = hidLogisticstatus.Value;
                txtFee_Ins.Text = hidFee.Value;

                

                if (hidTypeCalWeight.Value == "Y")
                {
                    ClCondition_Ins.Items[0].Selected = true;
                }
                else
                {
                    ClCondition_Ins.Items[0].Selected = false;
                }

                if (hidTypeCalSize.Value == "Y")
                {
                    ClCondition_Ins.Items[1].Selected = true;
                }
                else
                {
                    ClCondition_Ins.Items[1].Selected = false;
                }

                if (hidTypeCalWeightSize.Value == "Y")
                {
                    ClCondition_Ins.Items[2].Selected = true;
                }
                else
                {
                    ClCondition_Ins.Items[2].Selected = false;
                }

                hidIdList.Value = hidLogisticId.Value;             
                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Logistic').modal();", true);

            }

        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {

            hidFlagInsert.Value = "True";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Logistic').modal();", true);
        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"LogisticDetail.aspx?LogisticCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }
        private bool CheckSymbol(string value)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (value.Contains(item)) return true;
            }

            return false;
        }
        protected Boolean validateSearch()
        {
            Boolean flag = true;

            var regexItem = new Regex("^[ก-๙a-zA-Z0-9/ ]*$");

            if (regexItem.IsMatch(txtSearchLogisticCode.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchLogisticCode.Text = "";
            }
            else
            {
                flag = false;
                lblSearchLogisticCode.Text = MessageConst._MSG_PLEASEINSERT + " รหัสการขนส่งสินค้าต้องไม่มีอักขระพิเศษ";
            }
            if (regexItem.IsMatch(txtSearchLogisticName.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchLogisticName.Text = "";
            }
            else
            {
                flag = false;
                lblSearchLogisticName.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อการขนส่งสินค้าต้องไม่มีอักขระพิเศษ";
            }
            return flag;
        }

        #endregion


    }
}