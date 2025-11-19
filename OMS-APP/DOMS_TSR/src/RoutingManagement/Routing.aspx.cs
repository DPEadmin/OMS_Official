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
using System.Text.RegularExpressions;

namespace DOMS_TSR.src.RoutingManagement
{
    public partial class Routing : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        
        protected static string routingImgUrl = ConfigurationManager.AppSettings["ProductImageUrl"];

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

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                    
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }


                LoadRouting();
       
               

            }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadRouting();
        }

        #region Function

      

        protected void LoadRouting()
        {
            List<RoutingInfo> lroutingInfo = new List<RoutingInfo>();

            

            int? totalRow = CountRoutingMasterList();

            SetPageBar(Convert.ToDouble(totalRow));

            
            lroutingInfo = GetRoutingMasterByCriteria();

            gvRouting.DataSource = lroutingInfo;

            gvRouting.DataBind();


            

        }
        
        public List<RoutingInfo> GetRoutingMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRoutingByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = txtSearchRoutingCode.Text;

                data["Routing_name"] = txtSearchRoutingName.Text;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RoutingInfo> lRoutingInfo = JsonConvert.DeserializeObject<List<RoutingInfo>>(respstr);


            return lRoutingInfo;

        }


        public bool ValidateDuplicate()
        {
            bool isDuplicate;
            string respstr = "";

            APIpath = APIUrl + "/api/support/RoutingCheck";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = txtRoutingCode_Ins.Text;
             
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RoutingInfo> lroutingInfo = JsonConvert.DeserializeObject<List<RoutingInfo>>(respstr);

            if (lroutingInfo.Count > 0)
            {
                isDuplicate = true;
            }
            else
            {
                isDuplicate = false;
            }

            return isDuplicate;

        }

      
        public int? CountRoutingMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountRoutingListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = txtSearchRoutingCode.Text;

                data["Routing_name"] = txtSearchRoutingName.Text;

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


            LoadRouting();
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

        protected Boolean DeleteRouting()
        {

            for (int i = 0; i < gvRouting.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvRouting.Rows[i].FindControl("chkRouting");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvRouting.Rows[i].FindControl("hidRoutingId");

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

                APIpath = APIUrl + "/api/support/DeleteRouting";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["Routing_code"] = Codelist;


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

        protected Boolean validateInsert()
        {
            Boolean flag = true;

            if (txtRoutingCode_Ins.Text == "")
            {
                flag = false;
                lblRoutingCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสสายส่ง";
            }
            else if (CheckSymbol(txtRoutingCode_Ins.Text) == true)
            {
                flag = false;
                lblRoutingCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสสายส่งต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                if (txtRoutingCode_Ins.Text != "")
                {


                    Boolean isDuplicate = ValidateDuplicate();


                    if (isDuplicate)
                    {
                        flag = false;
                        lblRoutingCode_Ins.Text = MessageConst._DATA_NComplete;

                    }
                    else
                    {
                        flag = (flag == false) ? false : true;
                        lblRoutingCode_Ins.Text = "";

                    }
                }
            }


            if (txtRoutingName_Ins.Text == "")
            {
                flag = false;
                lblRoutingName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อสายส่ง";
            }
            else if (CheckSymbol(txtRoutingName_Ins.Text) == true)
            {
                flag = false;
                lblRoutingName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อสายส่งต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblRoutingName_Ins.Text = "";
            }


            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-routing').modal();", true);
            }

            return flag;
        }

        protected Boolean validateUpdate()
        {
            Boolean flag = true;

            if (txtRoutingCode_Ins.Text == "")
            {
                flag = false;
                lblRoutingCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสสายส่ง";
            }
            else if (CheckSymbol(txtRoutingCode_Ins.Text) == true)
            {
                flag = false;
                lblRoutingCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสสายส่งต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                if (txtRoutingCode_Ins.Text != "")
                {
                        flag = (flag == false) ? false : true;
                        lblRoutingCode_Ins.Text = "";
                }
            }


            if (txtRoutingName_Ins.Text == "")
            {
                flag = false;
                lblRoutingName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อสายส่ง";
            }
            else if (CheckSymbol(txtRoutingName_Ins.Text) == true)
            {
                flag = false;
                lblRoutingName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อสายส่งต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblRoutingName_Ins.Text = "";
            }


            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-routing').modal();", true);
            }

            return flag;
        }

        #endregion

        #region Event 

        protected void gvRouting_Change(object sender, GridViewPageEventArgs e)
        {
            gvRouting.PageIndex = e.NewPageIndex;

            List<RoutingInfo> lRoutingInfo = new List<RoutingInfo>();

            lRoutingInfo = GetRoutingMasterByCriteria();

            gvRouting.DataSource = lRoutingInfo;

            gvRouting.DataBind();

        }

        protected void chkRoutingAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvRouting.Rows.Count; i++)
            {
                CheckBox chkRouting = (CheckBox)gvRouting.Rows[i].FindControl("chkRouting");
                CheckBox chkall = (CheckBox)gvRouting.HeaderRow.FindControl("chkRoutingAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvRouting.Rows[i].FindControl("hidroutingId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                   
                    chkRouting.Checked = true;
                }
                else
                {

                    chkRouting.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }
    
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (validateSearch())
            {
            LoadRouting();
            }

            
        }
  


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteRouting();

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
                    if (validateInsert())
                    {
                       
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertRouting";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["Routing_code"] = txtRoutingCode_Ins.Text;
                            data["Routing_name"] = txtRoutingName_Ins.Text;
                            data["FlagDelete"] = "N";
                            data["CreateBy"] = empInfo.EmpCode;


                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            LoadRouting();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-routing').modal('hide');", true);



                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }

                }
                else //Update
                {
                    if (validateUpdate())
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateRouting";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["RoutingId"] = hidIdList.Value;
                            data["Routing_code"] = txtRoutingCode_Ins.Text;
                            data["Routing_name"] = txtRoutingName_Ins.Text;
                            data["FlagDelete"] = "N";
                            data["CreateBy"] = empInfo.EmpCode;


                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            LoadRouting();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-routing').modal('hide');", true);



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
            txtRoutingCode_Ins.Text = "";
            txtRoutingName_Ins.Text = "";
            lblRoutingCode_Ins.Text = "";
            lblRoutingName_Ins.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-routing').modal('hide');", true);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchRoutingCode.Text = "";
            txtSearchRoutingName.Text = "";
       

        }

        protected void gvRouting_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvRouting.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidRoutingId = (HiddenField)row.FindControl("hidRoutingId");
            HiddenField hidRoutingCode = (HiddenField)row.FindControl("hidRoutingCode");
            HiddenField hidRoutingName = (HiddenField)row.FindControl("hidRoutingName");
           
     


            if (e.CommandName == "ShowRouting")
            {

                txtRoutingCode_Ins.Text = hidRoutingCode.Value;
                txtRoutingName_Ins.Text = hidRoutingName.Value;
                txtRoutingCode_Ins.Enabled = false;
                hidRoutingCode_Ins.Value = hidRoutingCode.Value;

                lblRoutingCode_Ins.Text = "";
                lblRoutingName_Ins.Text = "";

                hidIdList.Value = hidRoutingId.Value;
                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-routing').modal();", true);

            }

        }

        protected void btnAddrouting_Click(object sender, EventArgs e)
        {
            txtRoutingCode_Ins.Enabled = true;
            hidFlagInsert.Value = "True";

            txtRoutingCode_Ins.Text = "";
            txtRoutingName_Ins.Text = "";

            lblRoutingCode_Ins.Text = "";
            lblRoutingName_Ins.Text = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-routing').modal();", true);
        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"RoutingDetail.aspx?RoutingCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        #endregion

         [WebMethod]
        public static MAPS[] BindMapMarker()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<MAPS> lstMarkers = new List<MAPS>();
            try
            {

                dt.Columns.Add("LocationName");
                dt.Columns.Add("Latitude");
                dt.Columns.Add("Longitude");

                dt.Rows.Add("RCA 1", "13.7481327", "100.57879700000001");
                dt.Rows.Add("LocationName 2", "23.715667", "90.384295");
                dt.Rows.Add("LocationName 3", "23.723928", "90.405924");
                dt.Rows.Add("LocationName 4", "23.716426", "90.395794");
                dt.Rows.Add("LocationName 5", "23.721985", "90.399379");

                foreach (DataRow dtrow in dt.Rows)
                {
                    MAPS objMAPS = new MAPS();
                    objMAPS.LocationName = dtrow[0].ToString();
                    objMAPS.Latitude = dtrow[1].ToString();
                    objMAPS.Longitude = dtrow[2].ToString();
                    lstMarkers.Add(objMAPS);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return lstMarkers.ToArray();
        }



        public class MAPS
        {
            public string LocationName;
            public string Latitude;
            public string Longitude;
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

            if (regexItem.IsMatch(txtSearchRoutingCode.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchRoutingCode.Text = "";
            }
            else
            {
                flag = false;
                lblSearchRoutingCode.Text = MessageConst._MSG_PLEASEINSERT + " รหัสสายส่งต้องไม่มีอักขระพิเศษ";
            }
            if (regexItem.IsMatch(txtSearchRoutingName.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchRoutingName.Text = "";
            }
            else
            {
                flag = false;
                lblSearchRoutingName.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อสายส่งต้องไม่มีอักขระพิเศษ";
            }
            return flag;
        }
    }
}