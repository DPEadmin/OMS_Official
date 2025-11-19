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
using System.IO;
using OfficeOpenXml;
namespace DOMS_TSR.src.MediaPlanManagement
{
    public partial class ApproveMediaPlan : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string MediaPlanImgUrl = ConfigurationManager.AppSettings["MediaPlanImageUrl"];

        string Idlist = "";
        string Codelist = "";
        Boolean isdelete;
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        L_MediaPlan result = new L_MediaPlan();
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

                if (empInfo != null && merchantInfo != null)
                {
                    
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerCode.Value = merchantInfo.MerchantCode;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                
                BindddlSalechannel();
                BindddlCampaign();
                
                loadMediaPlan();
                BindddlSearchChannel();
                
            }
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            loadMediaPlan();
        }

        #region Function



        protected void loadMediaPlan()
        {
            List<MediaPlanInfo> lMediaPlanInfo = new List<MediaPlanInfo>();

            

            int? totalRow = CountMediaPlanMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lMediaPlanInfo = GetMediaPlanMasterByCriteria();

            gvlMediaPlan.DataSource = lMediaPlanInfo;

            gvlMediaPlan.DataBind();


            

        }

        public List<MediaPlanInfo> GetMediaPlanMasterByCriteria()
        {
            string respstr = "";
       
            APIpath = APIUrl + "/api/support/ListMediaPlanNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProgramName"] = txtProgramName.Text;
                data["MediaPlanDateStart"] = txtSearchStartDateFrom.Text;
                data["MediaPlanDateEnd"] = txtSearchStartDateTo.Text;
                data["MerchantCode"] = hidMerCode.Value;
                data["SALE_CHANNEL"] = ddlSearchMediaPlanChannel.SelectedValue;
                if (txthhstart.Text != "" && txtmmstart.Text != "")
                {
                    data["TIME_START"] = txthhstart.Text + ":" + txtmmstart.Text;
                }
                else
                {
                    data["TIME_START"] = "";
                }
                if (txthhEnd.Text != "" && txtmmEnd.Text != "")
                {
                    data["TIME_END"] = txthhEnd.Text + ":" + txtmmEnd.Text;
                }
                else
                {
                    data["TIME_END"] = "";
                }

                data["MediaPhone"] = txtMediaPhone.Text;
                data["Duration"] = txtDuration.Text;
                data["Active"] = ddlactive.SelectedValue;
                data["CampaignCode"] = ddlCamp.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<MediaPlanInfo> lMediaPlanInfo = JsonConvert.DeserializeObject<List<MediaPlanInfo>>(respstr);
            return lMediaPlanInfo;
        }

        public int? CountMediaPlanMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountListMediaPlan";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProgramName"] = txtProgramName.Text;
                data["MediaPlanDateStart"] = txtSearchStartDateFrom.Text;
                data["MediaPlanDateEnd"] = txtSearchStartDateTo.Text;
                data["SALE_CHANNEL"] = ddlSearchMediaPlanChannel.SelectedValue;
                if (txthhstart.Text != "" && txtmmstart.Text != "")
                {
                    data["TIME_START"] = txthhstart.Text + ":" + txtmmstart.Text;
                }
                else
                {
                    data["TIME_START"] = "";
                }
                if (txthhEnd.Text != "" && txtmmEnd.Text != "")
                {
                    data["TIME_END"] = txthhEnd.Text + ":" + txtmmEnd.Text;
                }
                else
                {
                    data["TIME_END"] = "";
                }

                data["MediaPhone"] = txtMediaPhone.Text;
                data["Duration"] = txtDuration.Text;
                data["Active"] = ddlactive.SelectedValue;
                data["CampaignCode"] = ddlCamp.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            int? cou = JsonConvert.DeserializeObject<int?>(respstr);
            
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
            loadMediaPlan();
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

        protected Boolean ApproveActiveMediaPlan()
        {
            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];
            for (int i = 0; i < gvlMediaPlan.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvlMediaPlan.Rows[i].FindControl("chkMediaPlan");

                if (checkbox.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvlMediaPlan.Rows[i].FindControl("hidMediaPlanId");


                    if (Idlist != "")
                    {
                        Idlist += "," + hidId.Value + "";
                    }

                    else
                    {
                        Idlist += "" + hidId.Value + "";

                    }
                }
            }

            if (Idlist != "")
            {
                string respstr = "";
              
                APIpath = APIUrl + "/api/support/ActiveMediaPlanList";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["UpdateBy"] = empInfo.EmpCode;
                    data["MediaPlanidList"] = Idlist;
                    
                    data["FlagApprove"] = "Y";
                    var response = wb.UploadValues(APIpath, "POST", data);
                    respstr = Encoding.UTF8.GetString(response);
                }
                int? cou = JsonConvert.DeserializeObject<int?>(respstr);
                if (cou > 0)
                {

                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('อนุมัติเรียบร้อยแล้ว');", true);
                }
            }
            hidIdList.Value = "";
            return true;
        }
        protected Boolean ApproveInactiveMediaPlan()
        {
            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];
            for (int i = 0; i < gvlMediaPlan.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvlMediaPlan.Rows[i].FindControl("chkMediaPlan");

                if (checkbox.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvlMediaPlan.Rows[i].FindControl("hidMediaPlanId");


                    if (Idlist != "")
                    {
                        Idlist += "," + hidId.Value + "";
                    }

                    else
                    {
                        Idlist += "" + hidId.Value + "";

                    }
                }
            }

            if (Idlist != "")
            {
                string respstr = "";
                APIpath = APIUrl + "/api/support/ActiveMediaPlanList";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["UpdateBy"] = empInfo.EmpCode;
                    data["MediaPlanidList"] = Idlist;
                    
                    data["FlagApprove"] = "N";
                    var response = wb.UploadValues(APIpath, "POST", data);
                    respstr = Encoding.UTF8.GetString(response);
                }
                int? cou = JsonConvert.DeserializeObject<int?>(respstr);
                if (cou>0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('ยกเลิกอนุมัติเรียบร้อยแล้ว');", true);
                }
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

        


        #endregion

        #region Event 

        protected void gvlMediaPlan_Change(object sender, GridViewPageEventArgs e)
        {
            gvlMediaPlan.PageIndex = e.NewPageIndex;
            List<MediaPlanInfo> lMediaPlanInfo = new List<MediaPlanInfo>();
            lMediaPlanInfo = GetMediaPlanMasterByCriteria();
            gvlMediaPlan.DataSource = lMediaPlanInfo;
            gvlMediaPlan.DataBind();
        }

        protected void chkMediaPlanAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvlMediaPlan.Rows.Count; i++)
            {
                CheckBox chkall = (CheckBox)gvlMediaPlan.HeaderRow.FindControl("chkMediaPlanAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvlMediaPlan.Rows[i].FindControl("hidMediaPlanid");
                    

                    if (Idlist != "")
                    {
                        Idlist += ",'" + hidId.Value + "'";
                    }
                    
                    else
                    {
                        Idlist += "'" + hidId.Value + "'";
                        
                    }

                    CheckBox chkMediaPlan = (CheckBox)gvlMediaPlan.Rows[i].FindControl("chkMediaPlan");
                    chkMediaPlan.Checked = true;
                }
                else
                {
                    CheckBox chkMediaPlan = (CheckBox)gvlMediaPlan.Rows[i].FindControl("chkMediaPlan");
                    chkMediaPlan.Checked = false;
                }
            }
            hidIdList.Value = Idlist;
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            loadMediaPlan();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = ApproveActiveMediaPlan();
            btnSearch_Click(null, null);
            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการอนุมัติ');", true);
            }
            loadMediaPlan();
        }
        protected void btnCancel_MediaPlan_Click(object sender, EventArgs e)
        {
            isdelete = ApproveInactiveMediaPlan();
            btnSearch_Click(null, null);
            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการอนุมัติ');", true);
            }
            loadMediaPlan();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();
            POInfo pInfo = new POInfo();
            MerchantInfo merchantInfo = new MerchantInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];
            merchantInfo = (MerchantInfo)Session["MerchantCode"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
            }
            else
            {
                if (validateInsert())
                {
                    if (hidFlagInsert.Value == "True") //Insert
                    {
                        string respstr = "";
                        APIpath = APIUrl + "/api/support/InsertMediaPlan";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["MediaPlanDate"] = txtMediaPlanDate_Ins.Text;
                            data["MediaPlanTime"] = txtMediaPlanTimeHH_Ins.Text+":"+ txtMediaPlanTimeMM_Ins.Text;
                            data["ProgramName"] = txtProgramName_Ins.Text;
                            data["Channel"] = ddlChannel_Ins.SelectedValue;
                            data["Duration"] = txtDuration_Ins.Text;
                            data["MediaPhone"] = txtMediaPhone_Ins.Text;
                            data["CampaignCode"] = ddlCamp_Ins.SelectedValue;
                            data["MerchantCode"] = 

                            data["CreateBy"] = empInfo.EmpCode;
                            data["UpdateBy"] = empInfo.EmpCode;
                            data["FlagDelete"] = "N";

                            var response = wb.UploadValues(APIpath, "POST", data);
                            respstr = Encoding.UTF8.GetString(response);
                        }
                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);

                        if (sum > 0)
                        {
                            btnCancel_Click(null, null);
                            loadMediaPlan();
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-MediaPlan').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                    else //Update
                    {
                        string respstr = "";
                        APIpath = APIUrl + "/api/support/UpdateMediaPlan";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();
                            data["MediaPlanId"] = hidIdList.Value;
                            data["MediaPlanDate"] = txtMediaPlanDate_Ins.Text;
                            data["MediaPlanTime"] = txtMediaPlanTimeHH_Ins.Text + ":" + txtMediaPlanTimeMM_Ins.Text;
                            data["ProgramName"] = txtProgramName_Ins.Text;
                            data["ChannelCode"] = ddlChannel_Ins.SelectedValue;
                            data["Duration"] = txtDuration_Ins.Text;
                            data["MediaPhone"] = txtMediaPhone_Ins.Text;

                            data["CreateBy"] = empInfo.EmpCode;
                            data["UpdateBy"] = empInfo.EmpCode;

                            var response = wb.UploadValues(APIpath, "POST", data);
                            respstr = Encoding.UTF8.GetString(response);
                        }
                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);

                        if (sum > 0)
                        {
                            btnCancel_Click(null, null);
                            loadMediaPlan();
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-MediaPlan').modal('hide');", true);
                        }

                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
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
            ddlCamp_Ins.SelectedValue = "-99";
            txtMediaPlanDate_Ins.Text = "";
            txtMediaPlanTimeHH_Ins.Text = "";
            txtMediaPlanTimeMM_Ins.Text = "";
            ddlChannel_Ins.ClearSelection();
            txtDuration_Ins.Text = "";
            txtMediaPhone_Ins.Text = "";
            txtProgramName_Ins.Text = "";
            

            

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaPlan').modal('hide');", true);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            ddlCamp.ClearSelection();
            txtProgramName.Text = "";
            txtSearchStartDateFrom.Text = "";
            txtSearchStartDateTo.Text = "";
            ddlSearchMediaPlanChannel.ClearSelection();
            txthhstart.Text = "";
            txtmmstart.Text = "";
            txthhEnd.Text = "";
            txtmmEnd.Text = "";
            txtMediaPhone.Text = "";
            txtDuration.Text = "";
            ddlactive.ClearSelection();
        }
        protected void gvlMediaPlan_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                

            }
        }
        protected void gvlMediaPlan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvlMediaPlan.Rows[index];

            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField HidMediaPlanid = (HiddenField)row.FindControl("HidMediaPlanid");
            HiddenField HidProgramName = (HiddenField)row.FindControl("HidProgramName");
            HiddenField HidMediaPlanDate = (HiddenField)row.FindControl("HidMediaPlanDate");
          
            HiddenField HidMediaPlanTimeHH = (HiddenField)row.FindControl("HidMediaPlanTimeHH");
            
            HiddenField HidDuration = (HiddenField)row.FindControl("HidDuration");
            HiddenField HidMediaPhone = (HiddenField)row.FindControl("HidMediaPhone");

            HiddenField hidChannelCode = (HiddenField)row.FindControl("hidChannelCode");
            HiddenField hidStatusActive = (HiddenField)row.FindControl("hidStatusActive");
            HiddenField HidMediaPlanCode = (HiddenField)row.FindControl("HidMediaPlanCode");
            HiddenField HidMediaPlanName = (HiddenField)row.FindControl("HidMediaPlanName");
            HiddenField HidMediaPlanDuration = (HiddenField)row.FindControl("HidMediaPlanDuration");
            HiddenField HidCampaignCode = (HiddenField)row.FindControl("HidCampaignCode");
            HiddenField HidPromotionCode = (HiddenField)row.FindControl("HidPromotionCode");
            HiddenField HidSaleChannelCode = (HiddenField)row.FindControl("HidSaleChannelCode");
            HiddenField HidMerCode = (HiddenField)row.FindControl("HidMerCode");

            if (e.CommandName == "ShowMediaPlan")
            {
                hidIdList.Value = HidMediaPlanid.Value;
                txtMediaPlanDate_Ins.Text = HidMediaPlanDate.Value;
            

                txtProgramName_Ins.Text = HidProgramName.Value;
                txtDuration_Ins.Text = HidDuration.Value;
                txtMediaPhone_Ins.Text = HidMediaPhone.Value;
                

                string[] Hourtime = HidMediaPlanTimeHH.Value.Split(':');
                txtMediaPlanTimeHH_Ins.Text = Hourtime[0].ToString();
                txtMediaPlanTimeMM_Ins.Text = Hourtime[1].ToString();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaPlan').modal();", true);
            }
            else if (e.CommandName == "Download")
            {

            }
        }

        protected void btnAddMediaPlan_Click(object sender, EventArgs e)
        {
            hidFlagInsert.Value = "True";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaPlan').modal();", true);
        }

        protected void ddlMediaPlanTypeIns_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCombosetFlag_Ins_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void ShowComboSection(string flag)
        {


        }
        protected void btnShowImportFile_Click(object sender, EventArgs e)
        {
            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-ImportMedia').modal();", true);
        }
  
    

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"MediaPlanDetail.aspx?MediaPlanCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        

        protected void BindddlSearchChannel()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListChannelNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["ChannelCode"] = null;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<ChannelInfo> lLookupInfo = JsonConvert.DeserializeObject<List<ChannelInfo>>(respstr);

            ddlSearchMediaPlanChannel.DataSource = lLookupInfo;
            ddlSearchMediaPlanChannel.DataTextField = "ChannelName";
            ddlSearchMediaPlanChannel.DataValueField = "ChannelCode";
            ddlSearchMediaPlanChannel.DataBind();
            ddlSearchMediaPlanChannel.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
            ddlChannel_Ins.DataSource = lLookupInfo;
            ddlChannel_Ins.DataTextField = "ChannelName";
            ddlChannel_Ins.DataValueField = "ChannelCode";
            ddlChannel_Ins.DataBind();
            ddlChannel_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlCampaign()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListCampaignNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["CampaignCode"] = null;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<CampaignInfo> lLookupInfo = JsonConvert.DeserializeObject<List<CampaignInfo>>(respstr);

            ddlCamp.DataSource = lLookupInfo;
            ddlCamp.DataTextField = "CampaignCode";
            ddlCamp.DataValueField = "CampaignCode";
            ddlCamp.DataBind();
            ddlCamp.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

            ddlCamp_Ins.DataSource = lLookupInfo;
            ddlCamp_Ins.DataTextField = "CampaignName";
            ddlCamp_Ins.DataValueField = "CampaignCode";
            ddlCamp_Ins.DataBind();
            ddlCamp_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }
        protected void BindddlSalechannel()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListSaleChannelNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["SaleChannelCode"] = null;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<SaleChannelInfo> lLookupInfo = JsonConvert.DeserializeObject<List<SaleChannelInfo>>(respstr);

            ddlChannel_Ins.DataSource = lLookupInfo;
            ddlChannel_Ins.DataTextField = "SaleChannelName";
            ddlChannel_Ins.DataValueField = "SaleChannelCode";
            ddlChannel_Ins.DataBind();
            ddlChannel_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

            ddlSearchMediaPlanChannel.DataSource = lLookupInfo;
            ddlSearchMediaPlanChannel.DataTextField = "SaleChannelName";
            ddlSearchMediaPlanChannel.DataValueField = "SaleChannelCode";
            ddlSearchMediaPlanChannel.DataBind();
            ddlSearchMediaPlanChannel.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }
        

        protected Boolean validateInsert()
        {
            Boolean flag = true;
            if (txtMediaPlanDate_Ins.Text == "")
            {
                flag = false;
                lblMediaPlanDate_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Media Plan Date";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblMediaPlanDate_Ins.Text = "";
            }

            if (ddlChannel_Ins.SelectedValue == "-99" || ddlChannel_Ins.SelectedValue == "")
            {
                flag = false;
                lblChannel_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Channel";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblChannel_Ins.Text = "";
            }
            
            if (txtMediaPlanTimeHH_Ins.Text == "")
            {
                flag = false;
                lblMediaPlanTimeHH_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Plan Time";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblMediaPlanTimeHH_Ins.Text = "";
            }
            if (txtMediaPlanTimeMM_Ins.Text == "")
            {
                flag = false;
                lblMediaPlanTimeMM_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Plan Time";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblMediaPlanTimeMM_Ins.Text = "";
            }

            if (ddlChannel_Ins.SelectedValue == "-99" || ddlChannel_Ins.SelectedValue == "")
            {
                flag = false;
                lblChannel_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Channel";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblChannel_Ins.Text = "";
            }

            if (ddlCamp_Ins.SelectedValue == "-99" || ddlCamp_Ins.SelectedValue == "")
            {
                flag = false;
                lblddlCamp_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสแคมเปญ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblddlCamp_Ins.Text = "";
            }

            

            if (txtDuration_Ins.Text == "")
            {
                flag = false;
                lblDuration_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ระยะเวลา";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblDuration_Ins.Text = "";
            }

            

            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaPlan').modal();", true);
            }

            return flag;
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

        private DataTable ConvertToDataTable(ExcelWorksheet worksheet)
        {
            int totalRows = worksheet.Dimension.End.Row;
            DataTable dt = new DataTable(worksheet.Name);
            DataRow dr = null;

            //*** Column **/
            dt.Columns.Add("LINE_NO");
            dt.Columns.Add("DATE");
            dt.Columns.Add("TIME");
            dt.Columns.Add("PROGRAM_NAME");
            dt.Columns.Add("CHANNEL");
            dt.Columns.Add("DURATION");
            dt.Columns.Add("MEDIA_PHONE");
            dt.Columns.Add("CAMPAIGN_NAME");

            try
            {
                int j;
                for (int i = 2; i <= totalRows; i++)
                {
                    //*** Rows ***//
                    dr = dt.NewRow();
                    j = 1;

                    dr["LINE_NO"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["DATE"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["TIME"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["PROGRAM_NAME"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["CHANNEL"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["DURATION"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["MEDIA_PHONE"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["CAMPAIGN_NAME"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
        #endregion
    }
}