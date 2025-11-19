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

namespace DOMS_TSR.src.MasterLead
{
    public partial class ImportMasterLead : System.Web.UI.Page
    {
        protected static string APIUrl;
        protected static string PlannerImgUrl = ConfigurationManager.AppSettings["PlannerImageUrl"];

        string Idlist = "";
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
                    APIUrl = empInfo.ConnectionAPI;
                    hidEmpCode.Value = empInfo.EmpCode;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                
           
            }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            loadPlanner();
        }

        #region Function



        protected void loadPlanner()
        {
            List<PlannerInfo> lPlannerInfo = new List<PlannerInfo>();

            

            int? totalRow = CountPlannerMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lPlannerInfo = GetPlannerMasterByCriteria();

            gvPlanner.DataSource = lPlannerInfo;

            gvPlanner.DataBind();


             

        }

        public List<PlannerInfo> GetPlannerMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPlannerNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PlannerInfo> lPlannerInfo = JsonConvert.DeserializeObject<List<PlannerInfo>>(respstr);


            return lPlannerInfo;

        }

        public int? CountPlannerMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountListPlanner";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                
                data["SaleChannelCode"] = "";


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


            loadPlanner();
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

        protected Boolean DeletePlanner()
        {

            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];
            for (int i = 0; i < gvPlanner.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvPlanner.Rows[i].FindControl("chkPlanner");

                if (checkbox.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvPlanner.Rows[i].FindControl("hidPlannerid");
             

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

                APIpath = APIUrl + "/api/support/DeletePlanner";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["UpdateBy"] = empInfo.EmpCode;
                    data["PlanneridList"] = Idlist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);




            }
            
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

        protected void gvPlanner_Change(object sender, GridViewPageEventArgs e)
        {
            gvPlanner.PageIndex = e.NewPageIndex;

            List<PlannerInfo> lPlannerInfo = new List<PlannerInfo>();

            lPlannerInfo = GetPlannerMasterByCriteria();

            gvPlanner.DataSource = lPlannerInfo;

            gvPlanner.DataBind();

        }

        protected void chkPlannerAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvPlanner.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvPlanner.HeaderRow.FindControl("chkPlannerAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvPlanner.Rows[i].FindControl("hidPlannerid");
                    HiddenField hidCode = (HiddenField)gvPlanner.Rows[i].FindControl("hidPlannerCode");

                    if (Idlist != "")
                    {
                        Idlist += ",'" + hidId.Value + "'";
                    }
                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Idlist += "'" + hidId.Value + "'";
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkPlanner = (CheckBox)gvPlanner.Rows[i].FindControl("chkPlanner");

                    chkPlanner.Checked = true;
                }
                else
                {

                    CheckBox chkPlanner = (CheckBox)gvPlanner.Rows[i].FindControl("chkPlanner");

                    chkPlanner.Checked = false;
                }

            }
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            loadPlanner();


        }



        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeletePlanner();

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
                
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtPlannerCode_ins.Text = "";
            txtPlannername_ins.Text = "";
            ddlChannel_Ins.ClearSelection();
            ddlcamp_ins.ClearSelection();
            ddlpromotion_ins.ClearSelection();
            txtProgramname_ins.Text = "";
            txtDuration_Ins.Text = "";
            txtdateplanner_ins.Text = "";
            ddltimeplanner_ins.ClearSelection();
            ddlHourplanner_ins.ClearSelection();
            
          

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Planner').modal('hide');", true);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            
          
        }
        protected void gvPlanner_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              
                    
                
            }
        }
        protected void gvPlanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvPlanner.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField HidPlannerid = (HiddenField)row.FindControl("HidPlannerid");
            HiddenField hidChannelCode = (HiddenField)row.FindControl("hidChannelCode");
            HiddenField hidStatusActive = (HiddenField)row.FindControl("hidStatusActive");
            HiddenField HidPlannerCode = (HiddenField)row.FindControl("HidPlannerCode");
            HiddenField HidPlannerName = (HiddenField)row.FindControl("HidPlannerName");
            HiddenField HidPlannerProgramName = (HiddenField)row.FindControl("HidPlannerProgramName");
            HiddenField HidPlannerDate = (HiddenField)row.FindControl("HidPlannerDate");
            HiddenField HidPlannerTime = (HiddenField)row.FindControl("HidPlannerTime");
            HiddenField HidPlannerDuration = (HiddenField)row.FindControl("HidPlannerDuration");
            HiddenField HidCampaignCode = (HiddenField)row.FindControl("HidCampaignCode");
            HiddenField HidPromotionCode = (HiddenField)row.FindControl("HidPromotionCode");
            HiddenField HidSaleChannelCode = (HiddenField)row.FindControl("HidSaleChannelCode");

            if (e.CommandName == "ShowPlanner")
            {
                txtPlannerCode_ins.Text = HidPlannerCode.Value;
                txtPlannername_ins.Text = HidPlannerName.Value;
                ddlChannel_Ins.SelectedValue = hidChannelCode.Value;
                ddlcamp_ins.SelectedValue = HidCampaignCode.Value;
                ddlpromotion_ins.SelectedValue = HidPromotionCode.Value;
                ddlStatusActive_Ins.SelectedValue = hidStatusActive.Value;
                txtProgramname_ins.Text = HidPlannerProgramName.Value;
                
                txtPlannername_ins.Text = HidPlannerName.Value;
                txtDuration_Ins.Text = HidPlannerDuration.Value;
                txtdateplanner_ins.Text = HidPlannerDate.Value;

                string[] Hourtime = HidPlannerTime.Value.Split(':');
                ddlHourplanner_ins.SelectedValue = Hourtime[0].ToString();
                ddltimeplanner_ins.SelectedValue = Hourtime[1].ToString();

                ddlSaleChannel_Ins.SelectedValue = HidSaleChannelCode.Value;
                

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Planner').modal();", true);

            }
            else if (e.CommandName == "Download") 
            {

            }

        }

        protected void btnAddPlanner_Click(object sender, EventArgs e)
        {

            

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Planner').modal();", true);
        }

        protected void ddlPlannerTypeIns_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        protected void ddlCombosetFlag_Ins_SelectedIndexChanged(object sender, EventArgs e)
        {
           

        }

        protected void ShowComboSection(string flag)
        {
          

        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"PlannerDetail.aspx?PlannerCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        protected void BindStatus()
        {
            List<ListItem> items = new List<ListItem>();
            items.Add(new ListItem("---- กรุณาเลือก ----", ""));
            items.Add(new ListItem("Active", "Y"));
            items.Add(new ListItem("Inactive", "N"));
           
            ddlSearchPlannerActive.Items.AddRange(items.ToArray());
            ddlStatusActive_Ins.Items.AddRange(items.ToArray());
        }
        protected void BindHour()
        {
            List<ListItem> items = new List<ListItem>();
            items.Add(new ListItem("ชั่วโมง", "-99"));
            items.Add(new ListItem("1", "1"));
            items.Add(new ListItem("2", "2"));
            items.Add(new ListItem("3", "3"));
            items.Add(new ListItem("4", "4"));
            items.Add(new ListItem("5", "5"));
            items.Add(new ListItem("6", "6"));
            items.Add(new ListItem("7", "7"));
            items.Add(new ListItem("8", "8"));
            items.Add(new ListItem("9", "9"));
            items.Add(new ListItem("10", "10"));
            items.Add(new ListItem("11", "11"));
            items.Add(new ListItem("12", "12"));
            ddlHourplanner_ins.Items.AddRange(items.ToArray());
         
        }
        protected void Bindtime()
        {
            List<ListItem> items = new List<ListItem>();
            items.Add(new ListItem("เวลา", "-99"));
            items.Add(new ListItem("00", "00"));
            items.Add(new ListItem("15", "15"));
            items.Add(new ListItem("30", "30"));
            items.Add(new ListItem("45", "45"));
            items.Add(new ListItem("50", "50"));
         
            ddltimeplanner_ins.Items.AddRange(items.ToArray());
          
        }
        

        protected Boolean validateInsert()
        {
            Boolean flag = true;

            if (ddlHourplanner_ins.SelectedValue == "-99" || ddlHourplanner_ins.SelectedValue == "")
            {
                flag = false;
                lbltimeplanner_ins.Text = MessageConst._MSG_PLEASEINSERT + " ชั่วโมง";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lbltimeplanner_ins.Text = "";
            }
            if (ddltimeplanner_ins.SelectedValue == "-99" || ddltimeplanner_ins.SelectedValue == "")
            {
                flag = false;
                lbltimeplanner_ins.Text = MessageConst._MSG_PLEASEINSERT + " เวลา";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lbltimeplanner_ins.Text = "";
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
            if (txtPlannerCode_ins.Text == "")
            {
                flag = false;
                lbPlannerCode_ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัส Planner";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lbPlannerCode_ins.Text = "";
            }
            if (txtPlannername_ins.Text == "")
            {
                flag = false;
                lblPlannername_ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อ Planner";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblPlannername_ins.Text = "";
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
            if (ddlcamp_ins.SelectedValue == "-99" || ddlcamp_ins.SelectedValue == "")
            {
                flag = false;
                lblddlcamp_ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสแคมเปญ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblddlcamp_ins.Text = "";
            }
            if (ddlpromotion_ins.SelectedValue == "-99" || ddlpromotion_ins.SelectedValue == "")
            {
                flag = false;
                lblddlpromotion_ins.Text = MessageConst._MSG_PLEASEINSERT + " โปรโมชั่";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblddlpromotion_ins.Text = "";
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

            if (txtdateplanner_ins.Text == "")
            {
                flag = false;
                lbldateplanner_ins.Text = MessageConst._MSG_PLEASEINSERT + " วัน";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lbldateplanner_ins.Text = "";
            }
      
          
            if (ddlStatusActive_Ins.SelectedValue == "-99" || ddlStatusActive_Ins.SelectedValue == "")
            {
                flag = false;
                lbStatusActive_Ins.Text = MessageConst._MSG_PLEASEINSERT + " สถานะ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lbStatusActive_Ins.Text = "";
            }
            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Planner').modal();", true);
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
        #endregion


    }
}