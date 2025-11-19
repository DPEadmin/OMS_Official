using Newtonsoft.Json;
using SALEORDER.Common;
using SALEORDER.DTO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DOMS_TSR.src.LeadManagement
{
    public partial class LeadAssignment : System.Web.UI.Page
    {
        protected static string APIUrl;
        string Codelist = "";
        string CodeEmpGroupList = "";
        string CodeEmpPercentList = "";
        string CodeEmpRoundRobinList = "";
        string Merchant_Code = "";
        string EditFlag = "";
        Boolean isdelete;
        protected static int currentPageNumber;
        protected static int currentPageNumbergvEmp;
        protected static int currentPageNumbergvEmpGroup;
        protected static int currentPageNumbergvEmpPercent;
        protected static int currentPageNumbergvEmpRoundRobin;
        protected static string APIUrlx = ConfigurationManager.AppSettings["apiurl"];
        protected static int PAGE_SIZE = Convert.ToInt32("30");
        string APIpath = "";
        protected List<EmpInfo> L_EmpGroupInfolist
        {
            get
            {
                if (Session["l_empgrouptlist"] == null)
                {
                    return new List<EmpInfo>();
                }
                else
                {
                    return (List<EmpInfo>)Session["l_empgrouptlist"];
                }
            }
            set
            {
                Session["l_empgrouptlist"] = value;
            }
        }
        protected List<EmpInfo> L_EmpPercentInfolist
        {
            get
            {
                if (Session["l_emppercentlist"] == null)
                {
                    return new List<EmpInfo>();
                }
                else
                {
                    return (List<EmpInfo>)Session["l_emppercentlist"];
                }
            }
            set
            {
                Session["l_emppercentlist"] = value;
            }
        }
        protected List<EmpInfo> L_EmpRoundRobinInfolist
        {
            get
            {
                if (Session["l_emproundrobinlist"] == null)
                {
                    return new List<EmpInfo>();
                }
                else
                {
                    return (List<EmpInfo>)Session["l_emproundrobinlist"];
                }
            }
            set
            {
                Session["l_emproundrobinlist"] = value;
            }
        }
        protected List<LeadManagementInfo> L_Leadchkboxstatus
        {
            get
            {
                if (Session["l_leadcheckboxstatus"] == null)
                {
                    return new List<LeadManagementInfo>();
                }
                else
                {
                    return (List<LeadManagementInfo>)Session["l_leadcheckboxstatus"];
                }
            }
            set
            {
                Session["l_leadcheckboxstatus"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;
                currentPageNumbergvEmp = 1;
                currentPageNumbergvEmpGroup = 1;
                currentPageNumbergvEmpPercent = 1;
                currentPageNumbergvEmpRoundRobin = 1;

                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

               

                if (empInfo != null)
                {
                    //  APIUrl = empInfo.ConnectionAPI;
                    APIUrl = APIUrlx;
                  
                    hidEmpCode.Value = empInfo.EmpCode;
                    MerchantInfo mInfo = (MerchantInfo)Session["MerchantInfo"];
                    if (mInfo!=null)
                    {
                        Merchant_Code = mInfo.MerchantCode;
                    }
                  
                   
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                L_EmpGroupInfolist = new List<EmpInfo>();
                L_EmpPercentInfolist = new List<EmpInfo>();
                L_EmpRoundRobinInfolist = new List<EmpInfo>();

                groupemp.Visible = false;
                emppercent.Visible = false;
                emproundrobin.Visible = false;

                BindddlBU();
                LoadgvLeadManagement();
                LoadgvEmp();
                LoadgvEmpGroup();
                LoadgvEmpPercent();
                LoadEmptygvEmpAssignGroup();
                LoadEmptygvEmpAssignGroupPercent();
                LoadEmptygvEmpAssignRoundRobin();
                setsummarylead();

            }
        }

        #region function
        protected void LoadgvLeadManagement()
        {
            List<LeadManagementInfo> lLeadManagementInfo = new List<LeadManagementInfo>();
            int? totalRow = CountLeadMasterList();
            SetPageBar(Convert.ToDouble(totalRow));
            lLeadManagementInfo = GetLeadManagement();

            gvLeadManagement.DataSource = lLeadManagementInfo;
            gvLeadManagement.DataBind();
        }
        public void setsummarylead()
        {
            int total_S_Up;
            int Cassign;
            total_S_Up = LoadCountSummaryLeadManagement("'UP'");
            Cassign = LoadCountSummaryLeadManagement("'CL','OP','PD'");
            lblsumtotal.Text = (total_S_Up + Cassign).ToString();
            lblsumassign.Text = (Cassign).ToString();
            lblsumremain.Text = (total_S_Up).ToString();

        }
        public int LoadCountSummaryLeadManagement(string leadstatus)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/LeadManagement/CountLeadManagement";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();



                data["Status"] = leadstatus;
                data["Merchant_code"] = Merchant_Code;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int cou = JsonConvert.DeserializeObject<int>(respstr);

            return cou;
        }
        public int? CountLeadMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountAssignLeadManagementListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();


                data["CustomerPhone"] = txtSearchCustomerPhone.Text.Trim();
                data["CustomerFName"] = txtSearchCustomerFName.Text.Trim();
                data["CustomerLName"] = txtSearchCustomerFName.Text.Trim();
                data["CreateDateFrom"] = txtSearchCreateDateFrom.Text;
                data["CreateDateTo"] = txtSearchCreateDateTo.Text;
                data["Status"] = "UP";
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }
        public List<LeadManagementInfo> GetLeadManagement()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListAssignLeadManagementPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();



                data["CustomerPhone"] = txtSearchCustomerPhone.Text.Trim();
                data["CustomerFName"] = txtSearchCustomerFName.Text.Trim();
                data["CustomerLName"] = txtSearchCustomerFName.Text.Trim();
                data["CreateDateFrom"] = txtSearchCreateDateFrom.Text;
                data["CreateDateTo"] = txtSearchCreateDateTo.Text;
                data["Status"] = "UP";
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LeadManagementInfo> lLeadManagementInfo = JsonConvert.DeserializeObject<List<LeadManagementInfo>>(respstr);

            return lLeadManagementInfo;
        }
        protected void LoadgvEmp()
        {
            List<EmpInfo> lEmpInfo = new List<EmpInfo>();
            int? totalRow = CountEmpList();
            SetPageBargvEmp(Convert.ToDouble(totalRow));
            lEmpInfo = GetEmpActive();

            gvEmp.DataSource = lEmpInfo;
            gvEmp.DataBind();
        }
        protected void LoadgvEmpGroup()
        {
            List<EmpInfo> lEmpGroupInfo = new List<EmpInfo>();
            int? totalRow = CountEmpGroupList();
            SetPageBargvEmpGroup(Convert.ToDouble(totalRow));
            lEmpGroupInfo = GetEmpGroupActive();

            if (L_EmpGroupInfolist.Count > 0)
            {
                foreach (var od in L_EmpGroupInfolist.ToList())
                {
                    lEmpGroupInfo.RemoveAll(x => x.EmpId == od.EmpId);
                }

                gvEmpGroup.DataSource = lEmpGroupInfo;
                gvEmpGroup.DataBind();
            }
            else
            {
                gvEmpGroup.DataSource = lEmpGroupInfo;
                gvEmpGroup.DataBind();
            }
        }
        protected void LoadgvEmpPercent()
        {
            List<EmpInfo> lEmpPercentInfo = new List<EmpInfo>();
            int? totalRow = CountEmpPercentList();
            SetPageBargvEmpPercent(Convert.ToDouble(totalRow));
            lEmpPercentInfo = GetEmpPercentActive();

            if (L_EmpPercentInfolist.Count > 0)
            {
                foreach (var od in L_EmpPercentInfolist.ToList())
                {
                    lEmpPercentInfo.RemoveAll(x => x.EmpId == od.EmpId);
                }

                gvEmpPercent.DataSource = lEmpPercentInfo;
                gvEmpPercent.DataBind();
            }
            else
            {
                gvEmpPercent.DataSource = lEmpPercentInfo;
                gvEmpPercent.DataBind();
            }
        }
        protected void LoadgvEmpRoundRobin()
        {
            List<EmpInfo> lEmpRoundRobinInfo = new List<EmpInfo>();
            int? totalRow = CountEmpRoundRobinList();
            SetPageBargvEmpRoundRobin(Convert.ToDouble(totalRow));
            lEmpRoundRobinInfo = GetEmpRoundRobinActive();

            if (L_EmpRoundRobinInfolist.Count > 0)
            {
                foreach (var od in L_EmpRoundRobinInfolist.ToList())
                {
                    lEmpRoundRobinInfo.RemoveAll(x => x.EmpId == od.EmpId);
                }

                gvEmpRoundRobin.DataSource = lEmpRoundRobinInfo;
                gvEmpRoundRobin.DataBind();
            }
            else
            {
                gvEmpRoundRobin.DataSource = lEmpRoundRobinInfo;
                gvEmpRoundRobin.DataBind();
            }
        }
        protected void LoadEmptygvEmpAssignGroup()
        {
            List<EmpInfo> leInfo = new List<EmpInfo>();
            txtSearchEmpCode.Text = "999";
            leInfo = GetEmpActive();

            gvEmpAssignGroup.DataSource = leInfo;
            gvEmpAssignGroup.DataBind();

            txtSearchEmpCode.Text = "";
        }
        protected void LoadEmptygvEmpAssignGroupPercent()
        {
            List<EmpInfo> leInfo = new List<EmpInfo>();
            txtSearchEmpCode.Text = "999";
            leInfo = GetEmpActive();

            gvEmpAssignPercent.DataSource = leInfo;
            gvEmpAssignPercent.DataBind();

            txtSearchEmpCode.Text = "";
        }
        protected void LoadEmptygvEmpAssignRoundRobin()
        {
            List<EmpInfo> leInfo = new List<EmpInfo>();
            txtSearchEmpCode.Text = "999";
            leInfo = GetEmpActive();

            gvEmpAssignRoundRobin.DataSource = leInfo;
            gvEmpAssignRoundRobin.DataBind();

            txtSearchEmpCode.Text = "";
        }
        public int? CountEmpList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountEmployeeListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = txtSearchEmpCode.Text.Trim();
                data["EmpFname_TH"] = txtSearchEmpFName_TH.Text.Trim();
                data["EmpLname_TH"] = txtSearchEmpLName_TH.Text.Trim();
                data["ActiveFlag"] = "Y";
                data["BUCode"] = ddlEmpBU.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumbergvEmp - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }
        public int? CountEmpGroupList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountEmployeeListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = txtSearchEmpGroup.Text.Trim();
                data["EmpFname_TH"] = txtSearchEmpFNameGroup.Text.Trim();
                data["EmpLname_TH"] = txtSearchEmpLNameGroup.Text.Trim();
                data["ActiveFlag"] = "Y";
                data["BUCode"] = ddlEmpBUGroup.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumbergvEmpGroup - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }
        public int? CountEmpPercentList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountEmployeeListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = txtSearchEmpPercent.Text.Trim();
                data["EmpFname_TH"] = txtSearchEmpFNamePercent.Text.Trim();
                data["EmpLname_TH"] = txtSearchEmpLNamePercent.Text.Trim();
                data["ActiveFlag"] = "Y";
                data["BUCode"] = ddlEmpBUPercent.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumbergvEmpPercent - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }
        public int? CountEmpRoundRobinList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountEmployeeListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = txtSearchEmpRoundRobin.Text.Trim();
                data["EmpFname_TH"] = txtSearchEmpFNameRoundRobin.Text.Trim();
                data["EmpLname_TH"] = txtSearchEmpLNameRoundRobin.Text.Trim();
                data["ActiveFlag"] = "Y";
                data["BUCode"] = ddlEmpBURoundRobin.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumbergvEmpRoundRobin - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }
        public List<EmpInfo> GetEmpActive()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = txtSearchEmpCode.Text.Trim();
                data["EmpFname_TH"] = txtSearchEmpFName_TH.Text.Trim();
                data["EmpLname_TH"] = txtSearchEmpLName_TH.Text.Trim();
                data["ActiveFlag"] = "Y";
                data["BUCode"] = ddlEmpBU.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumbergvEmp - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpInfo> lEmpInfo = JsonConvert.DeserializeObject<List<EmpInfo>>(respstr);

            return lEmpInfo;
        }
        public List<EmpInfo> GetEmpGroupActive()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = txtSearchEmpGroup.Text.Trim();
                data["EmpFname_TH"] = txtSearchEmpFNameGroup.Text.Trim();
                data["EmpLname_TH"] = txtSearchEmpLNameGroup.Text.Trim();
                data["ActiveFlag"] = "Y";
                data["BUCode"] = ddlEmpBUGroup.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumbergvEmpGroup - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpInfo> lEmpInfo = JsonConvert.DeserializeObject<List<EmpInfo>>(respstr);

            return lEmpInfo;
        }
        public List<EmpInfo> GetEmpPercentActive()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = txtSearchEmpPercent.Text.Trim();
                data["EmpFname_TH"] = txtSearchEmpFNamePercent.Text.Trim();
                data["EmpLname_TH"] = txtSearchEmpLNamePercent.Text.Trim();
                data["ActiveFlag"] = "Y";
                data["BUCode"] = ddlEmpBUPercent.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumbergvEmpPercent - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpInfo> lEmpInfo = JsonConvert.DeserializeObject<List<EmpInfo>>(respstr);

            return lEmpInfo;
        }
        public List<EmpInfo> GetEmpRoundRobinActive()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = txtSearchEmpRoundRobin.Text.Trim();
                data["EmpFname_TH"] = txtSearchEmpFNameRoundRobin.Text.Trim();
                data["EmpLname_TH"] = txtSearchEmpLNameRoundRobin.Text.Trim();
                data["ActiveFlag"] = "Y";
                data["BUCode"] = ddlEmpBURoundRobin.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumbergvEmpRoundRobin - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpInfo> lEmpInfo = JsonConvert.DeserializeObject<List<EmpInfo>>(respstr);

            return lEmpInfo;
        }
        protected Boolean ValidateUpdate()
        {
            Boolean flaggvLead = false;
            Boolean flag = true;
            string error = "";
            int counterr = 0;

            for (int i = 0; i < gvLeadManagement.Rows.Count; i++)
            {
                CheckBox chkLead = (CheckBox)gvLeadManagement.Rows[i].FindControl("chkLead");

                if (chkLead.Checked == true)
                {
                    flaggvLead = true;
                }
                else
                {
                    flag = (!flag) ? false : true;
                }
            }

            if (flaggvLead == false)
            {
                flag = false;
                error += "Please select Lead \\n";
                counterr++;
            }
            else
            {
                flag = (!flag) ? false : true;
            }

            if (hidEmpBUSelected.Value == "" || hidEmpBUSelected.Value == null || hidEmpBUSelected.Value == "-99")
            {
                flag = false;
                error += "Please select BU ของพนักงาน \\n";
                counterr++;
            }

            if (hidEmpCodeAssign.Value == "" || hidEmpCodeAssign.Value == null)
            {
                flag = false;
                error += "Please select employee you want Assign \\n";
                counterr++;
            }

            if (counterr > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + error + "');", true);
            }

            return flag;
        }
        protected Boolean ValidateAssignLeadEmpGroup()
        {
            Boolean flag = true;
            string error = "";
            int countgridtrue = 0;
            int countgriderr = 0;
            int counterr = 0;

            for (int i = 0; i < gvLeadManagement.Rows.Count; i++)
            {
                CheckBox chkLead = (CheckBox)gvLeadManagement.Rows[i].FindControl("chkLead");

                if (chkLead.Checked == true)
                {
                    flag = (!flag) ? false : true;
                    countgridtrue++;
                }
                else
                {
                    flag = false;
                    countgriderr++;
                }
            }

            if (flag == false)
            {
                error += "Please select order Lead ทุกรายการหรือเลือก Checkbox All อีกครั้ง เนื่องจากเป็นการ Assign แบบ Multi Emp Average \\n";
                counterr++;
            }

            int? countleadassign = 0;

            if (flag == true)
            {
                foreach (GridViewRow row in gvEmpAssignGroup.Rows)
                {
                    TextBox txtNumberLeadAssign = (TextBox)row.FindControl("txtNumberLeadAssign");

                    countleadassign += Convert.ToInt32(txtNumberLeadAssign.Text);
                }

                if (countgridtrue == countleadassign)
                {
                    flag = (!flag) ? false : true;
                }
                else
                {
                    flag = false;
                    error += "จำนวนทั้งหมด LeadAssign ให้กับพนักงานไม่ตรงกับจำนวน Lead = " + countgridtrue.ToString() + " \\n";
                    counterr++;
                }
            }

            if (gvEmpAssignGroup.Rows.Count > 0)
            {
                flag = (!flag) ? false : true;
            }
            else
            {
                flag = false;
                error += "Please select employee do you want Assign \\n";
                counterr++;
            }

            if (counterr > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + error + "');", true);
            }

            return flag;
        }
        protected Boolean ValidateAssignLeadEmpPercent()
        {
            Boolean flag = true;
            string error = "";
            int countgridtrue = 0;
            int countgriderr = 0;
            int counterr = 0;

            for (int i = 0; i < gvLeadManagement.Rows.Count; i++)
            {
                CheckBox chkLead = (CheckBox)gvLeadManagement.Rows[i].FindControl("chkLead");

                if (chkLead.Checked == true)
                {
                    flag = (!flag) ? false : true;
                    countgridtrue++;
                }
                else
                {
                    flag = false;
                    countgriderr++;
                }
            }

            if (flag == false)
            {
                error += "กรุณาเลือกรายการ Lead ทุกรายการหรือเลือก Checkbox All อีกครั้ง เนื่องจากเป็นการ Assign แบบ Multi Emp Percent \\n";
                counterr++;
            }

            if (gvEmpAssignPercent.Rows.Count > 0)
            {
                flag = (!flag) ? false : true;
            }
            else
            {
                flag = false;
                error += "Please select employee do you want Assign \\n";
                counterr++;
            }

            double? countpercentassign = 0;

            foreach (GridViewRow row in gvEmpAssignPercent.Rows)
            {
                TextBox txtPercentLeadAssign = (TextBox)row.FindControl("txtPercentLeadAssign");

                countpercentassign += (txtPercentLeadAssign.Text != "") ? Convert.ToDouble(txtPercentLeadAssign.Text) : 0;
            }

            if (countpercentassign == 100)
            {
                flag = (!flag) ? false : true;
            }
            else
            {
                flag = false;
                error += "กรุณาใส่เปอร์เซ็นต์ Assign ให้ได้ค่าเปอร์เซ็นต์รวมเท่ากับ 100 \\n";
                counterr++;
            }

            if (counterr > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + error + "');", true);
            }

            return flag;
        }
        protected Boolean ValidateAssignLeadEmpRoundRobin()
        {
            Boolean flag = true;
            string error = "";
            int countgridtrue = 0;
            int countgriderr = 0;
            int counterr = 0;

            for (int i = 0; i < gvLeadManagement.Rows.Count; i++)
            {
                CheckBox chkLead = (CheckBox)gvLeadManagement.Rows[i].FindControl("chkLead");

                if (chkLead.Checked == true)
                {
                    flag = (!flag) ? false : true;
                    countgridtrue++;
                }
                else
                {
                    flag = false;
                    countgriderr++;
                }
            }

            if (flag == false)
            {
                error += "กรุณาเลือกรายการ Lead ทุกรายการหรือเลือก Checkbox All อีกครั้ง เนื่องจากเป็นการ Assign แบบ Multi Emp Round Robin \\n";
                counterr++;
            }

            if (gvEmpAssignRoundRobin.Rows.Count > 0)
            {
                flag = (!flag) ? false : true;
            }
            else
            {
                flag = false;
                error += "Please select employee do you want Assign \\n";
                counterr++;
            }

            if (counterr > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + error + "');", true);
            }

            return flag;
        }
        protected int? UpdateLeadAssignment(LeadManagementInfo lInfo)
        {
            int? sum = 0;
            string respstr = "";

            APIpath = APIUrl + "/api/support/UpdateAssignEmpLeadManagement";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LeadID"] = lInfo.LeadID.ToString();
                data["AssignEmpCode"] = lInfo.AssignEmpCode;
                data["CallOutStatus"] = lInfo.CallOutStatus;
                data["UpdateBy"] = hidEmpCode.Value;
                data["Status"] = lInfo.Status;
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);

                sum = JsonConvert.DeserializeObject<int?>(respstr);
            }

            return sum;
        }
        protected void BindgvEmpGroupAssign()
        {
            List<EmpInfo> leInfo = new List<EmpInfo>();

            foreach (GridViewRow row in gvEmpGroup.Rows)
            {
                CheckBox chkEmpGroup = (CheckBox)row.FindControl("chkEmpGroup");

                if (chkEmpGroup.Checked == true)
                {
                    HiddenField hidEmpId = (HiddenField)row.FindControl("hidEmpId");
                    HiddenField hidEmpCode = (HiddenField)row.FindControl("hidEmpCode");
                    HiddenField hidEmpFname_TH = (HiddenField)row.FindControl("hidEmpFname_TH");
                    HiddenField hidEmpLname_TH = (HiddenField)row.FindControl("hidEmpLname_TH");

                    EmpInfo eInfo = new EmpInfo();

                    eInfo.EmpId = (hidEmpId.Value != "") ? Convert.ToInt32(hidEmpId.Value) : 0;
                    eInfo.EmpCode = hidEmpCode.Value;
                    eInfo.EmpFname_TH = hidEmpFname_TH.Value;
                    eInfo.EmpLname_TH = hidEmpLname_TH.Value;

                    if (L_EmpGroupInfolist.Count > 0)
                    {
                        int? max = L_EmpGroupInfolist.Max(r => r.runningNo);
                        max = (max == null) ? 0 : max;
                        eInfo.runningNo = max + 1;
                    }
                    else
                    {
                        int? max = leInfo.Max(r => r.runningNo);
                        max = (max == null) ? 0 : max;
                        eInfo.runningNo = max + 1;
                    }

                    eInfo.NumberLeadAssign = 1;

                    leInfo.Add(eInfo);
                }
            }

            if (leInfo.Count > 0)
            {
                if (L_EmpGroupInfolist.Count > 0)
                {
                    L_EmpGroupInfolist.AddRange(leInfo);
                }
                else
                {
                    L_EmpGroupInfolist = leInfo;
                }

                CalculateCountLeadAssign(L_EmpGroupInfolist);

                gvEmpAssignGroup.DataSource = L_EmpGroupInfolist;
                gvEmpAssignGroup.DataBind();
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-empgroup').modal('hide');", true);
        }
        protected void BindgvgvEmpAssignPercent()
        {
            List<EmpInfo> leInfo = new List<EmpInfo>();

            foreach (GridViewRow row in gvEmpPercent.Rows)
            {
                CheckBox chkEmpPercent = (CheckBox)row.FindControl("chkEmpPercent");

                if (chkEmpPercent.Checked == true)
                {
                    HiddenField hidEmpId = (HiddenField)row.FindControl("hidEmpId");
                    HiddenField hidEmpCode = (HiddenField)row.FindControl("hidEmpCode");
                    HiddenField hidEmpFname_TH = (HiddenField)row.FindControl("hidEmpFname_TH");
                    HiddenField hidEmpLname_TH = (HiddenField)row.FindControl("hidEmpLname_TH");

                    EmpInfo eInfo = new EmpInfo();

                    eInfo.EmpId = (hidEmpId.Value != "") ? Convert.ToInt32(hidEmpId.Value) : 0;
                    eInfo.EmpCode = hidEmpCode.Value;
                    eInfo.EmpFname_TH = hidEmpFname_TH.Value;
                    eInfo.EmpLname_TH = hidEmpLname_TH.Value;

                    if (L_EmpPercentInfolist.Count > 0)
                    {
                        int? max = L_EmpPercentInfolist.Max(r => r.runningNo);
                        max = (max == null) ? 0 : max;
                        eInfo.runningNo = max + 1;
                    }
                    else
                    {
                        int? max = leInfo.Max(r => r.runningNo);
                        max = (max == null) ? 0 : max;
                        eInfo.runningNo = max + 1;
                    }

                    string d = "0.00";
                    eInfo.PercentLeadAssign = string.Format("{0:n}", (d));

                    leInfo.Add(eInfo);
                }
            }

            if (leInfo.Count > 0)
            {
                if (L_EmpPercentInfolist.Count > 0)
                {
                    L_EmpPercentInfolist.AddRange(leInfo);
                }
                else
                {
                    L_EmpPercentInfolist = leInfo;
                }

                CalculateCountLeadAssignPercent(L_EmpPercentInfolist);

                gvEmpAssignPercent.DataSource = L_EmpPercentInfolist;
                gvEmpAssignPercent.DataBind();
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-emppercent').modal('hide');", true);
        }
        protected void BindgvgvEmpAssignRoundRobin()
        {
            List<EmpInfo> leInfo = new List<EmpInfo>();

            foreach (GridViewRow row in gvEmpRoundRobin.Rows)
            {
                CheckBox chkEmpRoundRobin = (CheckBox)row.FindControl("chkEmpRoundRobin");

                if (chkEmpRoundRobin.Checked == true)
                {
                    HiddenField hidEmpId = (HiddenField)row.FindControl("hidEmpId");
                    HiddenField hidEmpCode = (HiddenField)row.FindControl("hidEmpCode");
                    HiddenField hidEmpFname_TH = (HiddenField)row.FindControl("hidEmpFname_TH");
                    HiddenField hidEmpLname_TH = (HiddenField)row.FindControl("hidEmpLname_TH");

                    EmpInfo eInfo = new EmpInfo();

                    eInfo.EmpId = (hidEmpId.Value != "") ? Convert.ToInt32(hidEmpId.Value) : 0;
                    eInfo.EmpCode = hidEmpCode.Value;
                    eInfo.EmpFname_TH = hidEmpFname_TH.Value;
                    eInfo.EmpLname_TH = hidEmpLname_TH.Value;

                    if (L_EmpRoundRobinInfolist.Count > 0)
                    {
                        int? max = L_EmpRoundRobinInfolist.Max(r => r.runningNo);
                        max = (max == null) ? 0 : max;
                        eInfo.runningNo = max + 1;
                    }
                    else
                    {
                        int? max = leInfo.Max(r => r.runningNo);
                        max = (max == null) ? 0 : max;
                        eInfo.runningNo = max + 1;
                    }

                    eInfo.NumberLeadAssign = 1;

                    leInfo.Add(eInfo);
                }
            }

            if (leInfo.Count > 0)
            {
                if (L_EmpRoundRobinInfolist.Count > 0)
                {
                    L_EmpRoundRobinInfolist.AddRange(leInfo);
                }
                else
                {
                    L_EmpRoundRobinInfolist = leInfo;
                }



                gvEmpAssignRoundRobin.DataSource = L_EmpRoundRobinInfolist;
                gvEmpAssignRoundRobin.DataBind();
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-emproundrobin').modal('hide');", true);
        }
        protected List<EmpInfo> loadEmpGroupfromRemove(List<EmpInfo> LEmpInfo)
        {
            List<EmpInfo> leInfo = new List<EmpInfo>();
            int i = 1;

            foreach (var od in LEmpInfo.ToList())
            {
                EmpInfo eInfo = new EmpInfo();

                eInfo.runningNo = i;
                i++;

                eInfo.EmpId = od.EmpId;
                eInfo.EmpCode = od.EmpCode;
                eInfo.EmpFname_TH = od.EmpFname_TH;
                eInfo.EmpLname_TH = od.EmpLname_TH;
                eInfo.NumberLeadAssign = od.NumberLeadAssign;

                leInfo.Add(eInfo);
            }

            return leInfo;
        }
        protected List<EmpInfo> loadEmpPercentfromRemove(List<EmpInfo> LEmpInfo)
        {
            List<EmpInfo> leInfo = new List<EmpInfo>();
            int i = 1;

            foreach (var od in LEmpInfo.ToList())
            {
                EmpInfo eInfo = new EmpInfo();

                eInfo.runningNo = i;
                i++;

                eInfo.EmpId = od.EmpId;
                eInfo.EmpCode = od.EmpCode;
                eInfo.EmpFname_TH = od.EmpFname_TH;
                eInfo.EmpLname_TH = od.EmpLname_TH;
                eInfo.NumberLeadAssign = od.NumberLeadAssign;
                eInfo.PercentLeadAssign = od.PercentLeadAssign;

                leInfo.Add(eInfo);
            }

            return leInfo;
        }
        protected List<EmpInfo> loadEmpRoundRobinfromRemove(List<EmpInfo> LEmpInfo)
        {
            List<EmpInfo> leInfo = new List<EmpInfo>();
            int i = 1;

            foreach (var od in LEmpInfo.ToList())
            {
                EmpInfo eInfo = new EmpInfo();

                eInfo.runningNo = i;
                i++;

                eInfo.EmpId = od.EmpId;
                eInfo.EmpCode = od.EmpCode;
                eInfo.EmpFname_TH = od.EmpFname_TH;
                eInfo.EmpLname_TH = od.EmpLname_TH;
                eInfo.NumberLeadAssign = od.NumberLeadAssign;
                eInfo.PercentLeadAssign = od.PercentLeadAssign;

                leInfo.Add(eInfo);
            }

            return leInfo;
        }
        protected void CalculateCountLeadAssign(List<EmpInfo> LEmpInfo)
        {
            int? countlead = 0;

            for (int i = 0; i < gvLeadManagement.Rows.Count; i++)
            {
                CheckBox chkLead = (CheckBox)gvLeadManagement.Rows[i].FindControl("chkLead");

                if (chkLead.Checked == true)
                {
                    countlead++;
                }
            }

            int? countemp = LEmpInfo.Count;
            int? couleadresult = 0;
            int? leftcountlead = 0;

            if (countemp > 0)
            {
                couleadresult = countlead / countemp;
                leftcountlead = countlead % countemp;
            }

            foreach (var od in LEmpInfo.ToList())
            {
                od.NumberLeadAssign = couleadresult;

                if (leftcountlead > 0)
                {
                    od.NumberLeadAssign++;
                    leftcountlead--;
                }
            }
        }
        protected void CalculateCountLeadAssignPercent(List<EmpInfo> LEmpInfo)
        {
            int? countlead = 0;

            for (int i = 0; i < gvLeadManagement.Rows.Count; i++)
            {
                CheckBox chkLead = (CheckBox)gvLeadManagement.Rows[i].FindControl("chkLead");

                if (chkLead.Checked == true)
                {
                    countlead++;
                }
            }

            int? countemp = LEmpInfo.Count;
            double? leadpercent = 0;
            double? leftleadpercent = 0;
            double? leadpercentlastrow = 0;

            leadpercent = Math.Round((100 * Convert.ToDouble(countlead)) / Convert.ToDouble(countemp) / Convert.ToDouble(countlead), 2);
            leftleadpercent = Math.Round((Convert.ToDouble(100) - (Convert.ToDouble(countemp) * Convert.ToDouble(leadpercent))), 2);
            leadpercentlastrow = Math.Round((Convert.ToDouble(leadpercent) + Convert.ToDouble(leftleadpercent)), 2);

            for (int i = 0; i < LEmpInfo.Count; i++)
            {
                if (i == LEmpInfo.Count - 1)
                {
                    LEmpInfo[i].PercentLeadAssign = string.Format("{0:n}", (Convert.ToDouble(leadpercentlastrow)));
                }
                else
                {
                    LEmpInfo[i].PercentLeadAssign = string.Format("{0:n}", (Convert.ToDouble(leadpercent)));
                }
            }
        }
        protected List<LeadManagementInfo> GetLeadManagementNoPaging(int? leadId)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLeadManagementNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LeadID"] = leadId.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LeadManagementInfo> lleadmanagementInfo = JsonConvert.DeserializeObject<List<LeadManagementInfo>>(respstr);

            return lleadmanagementInfo;
        }
        #endregion

        #region binding
        protected void BindddlBU()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_BU;
                data["LookupCode"] = StaticField.LookupTypeBU_LookupCode_OUB;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlEmpBU.DataSource = lLookupInfo;
            ddlEmpBU.DataTextField = "LookupValue";
            ddlEmpBU.DataValueField = "LookupCode";
            ddlEmpBU.DataBind();
            ddlEmpBU.Items.Insert(0, new ListItem("Please select", "-99"));

            ddlEmpBUGroup.DataSource = lLookupInfo;
            ddlEmpBUGroup.DataTextField = "LookupValue";
            ddlEmpBUGroup.DataValueField = "LookupCode";
            ddlEmpBUGroup.DataBind();
            ddlEmpBUGroup.Items.Insert(0, new ListItem("Please select", "-99"));

            ddlEmpBUPercent.DataSource = lLookupInfo;
            ddlEmpBUPercent.DataTextField = "LookupValue";
            ddlEmpBUPercent.DataValueField = "LookupCode";
            ddlEmpBUPercent.DataBind();
            ddlEmpBUPercent.Items.Insert(0, new ListItem("Please select", "-99"));

            ddlEmpBURoundRobin.DataSource = lLookupInfo;
            ddlEmpBURoundRobin.DataTextField = "LookupValue";
            ddlEmpBURoundRobin.DataValueField = "LookupCode";
            ddlEmpBURoundRobin.DataBind();
            ddlEmpBURoundRobin.Items.Insert(0, new ListItem("Please select", "-99"));
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
        protected void SetPageBargvEmp(double totalRow)
        {
            lblgvEmpTotalPages.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString();


            ddlgvEmpPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblgvEmpTotalPages.Text) + 1; i++)
            {
                ddlgvEmpPage.Items.Add(new ListItem(i.ToString()));
            }
            setDDl(ddlgvEmpPage, currentPageNumbergvEmp.ToString());



            if ((currentPageNumbergvEmp == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtngvEmpFirst.Enabled = false;
                lnkbtngvEmpPre.Enabled = false;
                lnkbtngvEmpNext.Enabled = true;
                lnkbtngvEmpLast.Enabled = true;
            }
            else if ((currentPageNumbergvEmp.ToString() == lblTotalPages.Text) && (currentPageNumbergvEmp == 1))
            {
                lnkbtngvEmpFirst.Enabled = false;
                lnkbtngvEmpPre.Enabled = false;
                lnkbtngvEmpNext.Enabled = false;
                lnkbtngvEmpLast.Enabled = false;
            }
            else if ((currentPageNumbergvEmp.ToString() == lblTotalPages.Text) && (currentPageNumbergvEmp > 1))
            {
                lnkbtngvEmpFirst.Enabled = true;
                lnkbtngvEmpPre.Enabled = true;
                lnkbtngvEmpNext.Enabled = false;
                lnkbtngvEmpLast.Enabled = false;
            }
            else
            {
                lnkbtngvEmpFirst.Enabled = true;
                lnkbtngvEmpPre.Enabled = true;
                lnkbtngvEmpNext.Enabled = true;
                lnkbtngvEmpLast.Enabled = true;
            }

        }
        protected void SetPageBargvEmpGroup(double totalRow)
        {
            lblgvEmpGroupTotalPages.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString();


            ddlgvEmpGroupPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblgvEmpGroupTotalPages.Text) + 1; i++)
            {
                ddlgvEmpGroupPage.Items.Add(new ListItem(i.ToString()));
            }
            setDDl(ddlgvEmpGroupPage, currentPageNumbergvEmpGroup.ToString());



            if ((currentPageNumbergvEmpGroup == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtngvEmpGroupFirst.Enabled = false;
                lnkbtngvEmpGroupPrevious.Enabled = false;
                lnkbtngvEmpGroupNext.Enabled = true;
                lnkbtngvEmpGroupLast.Enabled = true;
            }
            else if ((currentPageNumbergvEmpGroup.ToString() == lblgvEmpGroupTotalPages.Text) && (currentPageNumbergvEmpGroup == 1))
            {
                lnkbtngvEmpGroupFirst.Enabled = false;
                lnkbtngvEmpGroupPrevious.Enabled = false;
                lnkbtngvEmpGroupNext.Enabled = false;
                lnkbtngvEmpGroupLast.Enabled = false;
            }
            else if ((currentPageNumbergvEmpGroup.ToString() == lblgvEmpGroupTotalPages.Text) && (currentPageNumbergvEmpGroup > 1))
            {
                lnkbtngvEmpFirst.Enabled = true;
                lnkbtngvEmpGroupPrevious.Enabled = true;
                lnkbtngvEmpGroupNext.Enabled = false;
                lnkbtngvEmpGroupLast.Enabled = false;
            }
            else
            {
                lnkbtngvEmpGroupFirst.Enabled = true;
                lnkbtngvEmpGroupPrevious.Enabled = true;
                lnkbtngvEmpGroupNext.Enabled = true;
                lnkbtngvEmpGroupLast.Enabled = true;
            }

        }
        protected void SetPageBargvEmpPercent(double totalRow)
        {
            lblgvEmpPercentTotalPages.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString();


            ddlgvEmpPercentPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblgvEmpPercentTotalPages.Text) + 1; i++)
            {
                ddlgvEmpPercentPage.Items.Add(new ListItem(i.ToString()));
            }
            setDDl(ddlgvEmpPercentPage, currentPageNumbergvEmpPercent.ToString());



            if ((currentPageNumbergvEmpPercent == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtngvEmpPercentFirst.Enabled = false;
                lnkbtngvEmpPercentPrevious.Enabled = false;
                lnkbtngvEmpPercentNext.Enabled = true;
                lnkbtngvEmpPercentLast.Enabled = true;
            }
            else if ((currentPageNumbergvEmpGroup.ToString() == lblgvEmpPercentTotalPages.Text) && (currentPageNumbergvEmpPercent == 1))
            {
                lnkbtngvEmpPercentFirst.Enabled = false;
                lnkbtngvEmpPercentPrevious.Enabled = false;
                lnkbtngvEmpPercentNext.Enabled = false;
                lnkbtngvEmpPercentLast.Enabled = false;
            }
            else if ((currentPageNumbergvEmpGroup.ToString() == lblgvEmpPercentTotalPages.Text) && (currentPageNumbergvEmpPercent > 1))
            {
                lnkbtngvEmpPercentFirst.Enabled = true;
                lnkbtngvEmpPercentPrevious.Enabled = true;
                lnkbtngvEmpPercentNext.Enabled = false;
                lnkbtngvEmpPercentLast.Enabled = false;
            }
            else
            {
                lnkbtngvEmpPercentFirst.Enabled = true;
                lnkbtngvEmpPercentPrevious.Enabled = true;
                lnkbtngvEmpPercentNext.Enabled = true;
                lnkbtngvEmpPercentLast.Enabled = true;
            }

        }
        protected void SetPageBargvEmpRoundRobin(double totalRow)
        {
            lblgvEmpRRTotalPages.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString();


            ddlgvEmpRRPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblgvEmpRRTotalPages.Text) + 1; i++)
            {
                ddlgvEmpRRPage.Items.Add(new ListItem(i.ToString()));
            }
            setDDl(ddlgvEmpRRPage, currentPageNumbergvEmpRoundRobin.ToString());



            if ((currentPageNumbergvEmpRoundRobin == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtngvEmpRRFirst.Enabled = false;
                lnkbtngvEmpRRPrevious.Enabled = false;
                lnkbtngvEmpRRPNext.Enabled = true;
                lnkbtngvEmpRRLast.Enabled = true;
            }
            else if ((currentPageNumbergvEmpRoundRobin.ToString() == lblgvEmpRRTotalPages.Text) && (currentPageNumbergvEmpRoundRobin == 1))
            {
                lnkbtngvEmpRRFirst.Enabled = false;
                lnkbtngvEmpRRPrevious.Enabled = false;
                lnkbtngvEmpRRPNext.Enabled = false;
                lnkbtngvEmpRRLast.Enabled = false;
            }
            else if ((currentPageNumbergvEmpRoundRobin.ToString() == lblgvEmpRRTotalPages.Text) && (currentPageNumbergvEmpRoundRobin > 1))
            {
                lnkbtngvEmpRRFirst.Enabled = true;
                lnkbtngvEmpRRPrevious.Enabled = true;
                lnkbtngvEmpRRPNext.Enabled = false;
                lnkbtngvEmpRRLast.Enabled = false;
            }
            else
            {
                lnkbtngvEmpRRFirst.Enabled = true;
                lnkbtngvEmpRRPrevious.Enabled = true;
                lnkbtngvEmpRRPNext.Enabled = true;
                lnkbtngvEmpRRLast.Enabled = true;
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

            LoadgvLeadManagement();

            if (radAssignType.SelectedValue == "1" || radAssignType.SelectedValue == "2" || radAssignType.SelectedValue == "3")
            {
                CheckBox chkLeadAll = (CheckBox)gvLeadManagement.HeaderRow.FindControl("chkLeadAll");
                chkLeadAll.Checked = true;

                foreach (GridViewRow row in gvLeadManagement.Rows)
                {
                    CheckBox chkLead = (CheckBox)row.FindControl("chkLead");

                    if (chkLead.Checked == false)
                    {
                        chkLead.Checked = true;
                    }
                }
            }
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadgvLeadManagement();
        }
        protected void GetPagegvEmpIndex(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "gvEmpFirst":
                    currentPageNumbergvEmp = 1;
                    break;

                case "gvEmpPrevious":
                    currentPageNumbergvEmp = Int32.Parse(ddlgvEmpPage.SelectedValue) - 1;
                    break;

                case "gvEmpNext":
                    currentPageNumbergvEmp = Int32.Parse(ddlgvEmpPage.SelectedValue) + 1;
                    break;

                case "gvEmpLast":
                    currentPageNumbergvEmp = Int32.Parse(lblgvEmpTotalPages.Text);
                    break;
            }

            LoadgvEmp();
        }
        protected void GetPagegvEmpGroupIndex(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "gvEmpGroupFirst":
                    currentPageNumbergvEmpGroup = 1;
                    break;

                case "gvEmpGroupPrevious":
                    currentPageNumbergvEmpGroup = Int32.Parse(ddlgvEmpGroupPage.SelectedValue) - 1;
                    break;

                case "gvEmpGroupNext":
                    currentPageNumbergvEmpGroup = Int32.Parse(ddlgvEmpGroupPage.SelectedValue) + 1;
                    break;

                case "gvEmpGroupLast":
                    currentPageNumbergvEmpGroup = Int32.Parse(lblgvEmpGroupTotalPages.Text);
                    break;
            }

            LoadgvEmpGroup();
        }
        protected void GetPagegvEmpPercentIndex(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "gvEmpPercentFirst":
                    currentPageNumbergvEmpPercent = 1;
                    break;

                case "gvEmpPercentPrevious":
                    currentPageNumbergvEmpPercent = Int32.Parse(ddlgvEmpPercentPage.SelectedValue) - 1;
                    break;

                case "gvEmpPercentNext":
                    currentPageNumbergvEmpPercent = Int32.Parse(ddlgvEmpPercentPage.SelectedValue) + 1;
                    break;

                case "gvEmpPercentLast":
                    currentPageNumbergvEmpPercent = Int32.Parse(lblgvEmpPercentTotalPages.Text);
                    break;
            }

            LoadgvEmpPercent();
        }
        protected void GetPagegvEmpRRIndex(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "lnkbtngvEmpRRFirst":
                    currentPageNumbergvEmpRoundRobin = 1;
                    break;

                case "lnkbtngvEmpRRPrevious":
                    currentPageNumbergvEmpRoundRobin = Int32.Parse(ddlgvEmpRRPage.SelectedValue) - 1;
                    break;

                case "lnkbtngvEmpRRPNext":
                    currentPageNumbergvEmpRoundRobin = Int32.Parse(ddlgvEmpRRPage.SelectedValue) + 1;
                    break;

                case "lnkbtngvEmpRRLast":
                    currentPageNumbergvEmpRoundRobin = Int32.Parse(lblgvEmpRRTotalPages.Text);
                    break;
            }

            LoadgvEmpRoundRobin();
        }
        protected void ddlgvEmpPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumbergvEmp = Int32.Parse(ddlgvEmpPage.SelectedValue);
            LoadgvEmp();
        }
        protected void ddlgvEmpGroupPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumbergvEmpGroup = Int32.Parse(ddlgvEmpPercentPage.SelectedValue);
            LoadgvEmpGroup();
        }
        protected void ddlgvEmpPercentPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumbergvEmpPercent = Int32.Parse(ddlgvEmpPercentPage.SelectedValue);
            LoadgvEmpPercent();
        }
        protected void ddlgvEmpRRPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumbergvEmpRoundRobin = Int32.Parse(ddlgvEmpRRPage.SelectedValue);
            LoadgvEmpRoundRobin();
        }
        #endregion

        #region event
        protected void btnSearch_Clicked(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            LoadgvLeadManagement();
        }
        protected void btnClearSearch_Clicked(object sender, EventArgs e)
        {
            txtSearchCustomerFName.Text = "";


            txtSearchCustomerPhone.Text = "";
            txtSearchCreateDateFrom.Text = "";
            txtSearchCreateDateTo.Text = "";
        }
        protected void btnClearSearchEmpGroup_Clicked(object sender, EventArgs e)
        {
            txtSearchEmpGroup.Text = "";
            txtSearchEmpFNameGroup.Text = "";
            txtSearchEmpLNameGroup.Text = "";
        }
        protected void btnClearSearchEmpPercent_Clicked(object sender, EventArgs e)
        {
            txtSearchEmpPercent.Text = "";
            txtSearchEmpFNamePercent.Text = "";
            txtSearchEmpLNamePercent.Text = "";
        }
        protected void btnSelectEmp_Clicked(object sender, EventArgs e)
        {
            if (ddlEmpBU.SelectedValue != "-99")
            {
                txtSearchEmpCode.Text = "";
                txtSearchEmpFName_TH.Text = "";
                txtSearchEmpLName_TH.Text = "";

                LoadgvEmp();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-emp').modal();", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "Please select BU" + "');", true);
            }
        }
        protected void ddlEmpBU_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidEmpBUSelected.Value = ddlEmpBU.SelectedValue;

            txtEmpAssigned.Text = "";
        }
        protected void ddlEmpBUGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidEmpGroupBUSelected.Value = ddlEmpBU.SelectedValue;

            L_EmpGroupInfolist = new List<EmpInfo>();

            gvEmpAssignGroup.DataSource = L_EmpGroupInfolist;
            gvEmpAssignGroup.DataBind();
        }
        protected void btnSearchEmp_Clicked(object sender, EventArgs e)
        {
            currentPageNumbergvEmp = 1;
            LoadgvEmp();
        }
        protected void btnSearchEmpGroup_Clicked(object sender, EventArgs e)
        {
            currentPageNumbergvEmpGroup = 1;
            LoadgvEmpGroup();
        }
        protected void btnSearchEmpPercent_Clicked(object sender, EventArgs e)
        {
            currentPageNumbergvEmpPercent = 1;
            LoadgvEmpPercent();
        }
        protected void btnClearSearchEmp_Clicked(object sender, EventArgs e)
        {
            txtSearchEmpCode.Text = "";
            txtSearchEmpFName_TH.Text = "";
            txtSearchEmpLName_TH.Text = "";
        }
        protected void gvEmp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvEmp.Rows[index];

            HiddenField hidEmpId = (HiddenField)row.FindControl("hidEmpId");
            HiddenField hidEmpCode = (HiddenField)row.FindControl("hidEmpCode");
            HiddenField hidEmpFname_TH = (HiddenField)row.FindControl("hidEmpFname_TH");
            HiddenField hidEmpLname_TH = (HiddenField)row.FindControl("hidEmpLname_TH");

            if (e.CommandName == "SelectEmp")
            {
                hidEmpCodeAssign.Value = hidEmpCode.Value;
                txtEmpAssigned.Text = hidEmpFname_TH.Value + "  " + hidEmpLname_TH.Value;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-emp').modal('hide');", true);
            }
        }
        protected void chkLeadAll_click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvLeadManagement.Rows.Count; i++)
            {
                CheckBox chkall = (CheckBox)gvLeadManagement.HeaderRow.FindControl("chkLeadAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidbCode = (HiddenField)gvLeadManagement.Rows[i].FindControl("hidLeadID");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidbCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidbCode.Value + "'";
                    }

                    CheckBox chkLead = (CheckBox)gvLeadManagement.Rows[i].FindControl("chkLead");
                    chkLead.Checked = true;
                }
                else
                {
                    CheckBox chkLead = (CheckBox)gvLeadManagement.Rows[i].FindControl("chkLead");
                    chkLead.Checked = false;
                }
            }
            hidbIdByList.Value = Codelist;

            List<LeadManagementInfo> llInfo = new List<LeadManagementInfo>();

            if (L_Leadchkboxstatus.Count > 0)
            {
                L_Leadchkboxstatus.RemoveAll(x => x.currentpagenumber == currentPageNumber);
            }

            for (int i = 0; i < gvLeadManagement.Rows.Count; i++)
            {
                CheckBox chkLead = (CheckBox)gvLeadManagement.Rows[i].FindControl("chkLead");
                HiddenField hidLeadID = (HiddenField)gvLeadManagement.Rows[i].FindControl("hidLeadID");
                Label lblCustomerCode = (Label)gvLeadManagement.Rows[i].FindControl("lblCustomerCode");
                Label lblCustomerPhone = (Label)gvLeadManagement.Rows[i].FindControl("lblCustomerPhone");
                Label lblCustomerName = (Label)gvLeadManagement.Rows[i].FindControl("lblCustomerName");
                Label lblCreateDate = (Label)gvLeadManagement.Rows[i].FindControl("lblCreateDate");

                CheckBox chkall = (CheckBox)gvLeadManagement.HeaderRow.FindControl("chkLeadAll");

                if (chkall.Checked == true)
                {
                    LeadManagementInfo lInfo = new LeadManagementInfo();

                    lInfo.checkboxallstatus = "true";

                    lInfo.CustomerCode = lblCustomerCode.Text;
                    lInfo.CustomerPhone = lblCustomerPhone.Text;
                    lInfo.CustomerName = lblCustomerName.Text;
                    lInfo.CreateDate = lblCreateDate.Text;
                    lInfo.chkboxstatus = "true";
                    lInfo.LeadID = Convert.ToInt32(hidLeadID.Value);
                    lInfo.currentpagenumber = currentPageNumber;

                    llInfo.Add(lInfo);
                }
                else
                {
                    LeadManagementInfo lInfo = new LeadManagementInfo();

                    lInfo.checkboxallstatus = "false";

                    lInfo.CustomerCode = lblCustomerCode.Text;
                    lInfo.CustomerPhone = lblCustomerPhone.Text;
                    lInfo.CustomerName = lblCustomerName.Text;
                    lInfo.CreateDate = lblCreateDate.Text;
                    lInfo.chkboxstatus = "false";
                    lInfo.LeadID = Convert.ToInt32(hidLeadID.Value);
                    lInfo.currentpagenumber = currentPageNumber;

                    llInfo.Add(lInfo);
                }
            }

            if (L_Leadchkboxstatus.Count > 0)
            {
                L_Leadchkboxstatus.AddRange(llInfo);
            }
            else
            {
                L_Leadchkboxstatus = llInfo;
            }
        }
        protected void chkEmpGroupAll_click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvEmpGroup.Rows.Count; i++)
            {
                CheckBox chkall = (CheckBox)gvEmpGroup.HeaderRow.FindControl("chkEmpGroupAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidbCode = (HiddenField)gvEmpGroup.Rows[i].FindControl("hidEmpId");

                    if (CodeEmpGroupList != "")
                    {
                        CodeEmpGroupList += ",'" + hidbCode.Value + "'";
                    }
                    else
                    {
                        CodeEmpGroupList += "'" + hidbCode.Value + "'";
                    }

                    CheckBox chkLead = (CheckBox)gvEmpGroup.Rows[i].FindControl("chkEmpGroup");
                    chkLead.Checked = true;
                }
                else
                {
                    CheckBox chkLead = (CheckBox)gvEmpGroup.Rows[i].FindControl("chkEmpGroup");
                    chkLead.Checked = false;
                }
            }
            hidbEmpGroupByList.Value = CodeEmpGroupList;
        }
        protected void chkEmpPercentAll_click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvEmpPercent.Rows.Count; i++)
            {
                CheckBox chkall = (CheckBox)gvEmpPercent.HeaderRow.FindControl("chkEmpPercentAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidbCode = (HiddenField)gvEmpPercent.Rows[i].FindControl("hidEmpId");

                    if (CodeEmpPercentList != "")
                    {
                        CodeEmpPercentList += ",'" + hidbCode.Value + "'";
                    }
                    else
                    {
                        CodeEmpPercentList += "'" + hidbCode.Value + "'";
                    }

                    CheckBox chkEmpPercent = (CheckBox)gvEmpPercent.Rows[i].FindControl("chkEmpPercent");
                    chkEmpPercent.Checked = true;
                }
                else
                {
                    CheckBox chkEmpPercent = (CheckBox)gvEmpPercent.Rows[i].FindControl("chkEmpPercent");
                    chkEmpPercent.Checked = false;
                }
            }
            hidEmpPercentByList.Value = CodeEmpPercentList;
        }
        protected void chkEmpRoundRobinAll_click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvEmpRoundRobin.Rows.Count; i++)
            {
                CheckBox chkall = (CheckBox)gvEmpRoundRobin.HeaderRow.FindControl("chkEmpRoundRobinAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidbCode = (HiddenField)gvEmpRoundRobin.Rows[i].FindControl("hidEmpId");

                    if (CodeEmpRoundRobinList != "")
                    {
                        CodeEmpRoundRobinList += ",'" + hidbCode.Value + "'";
                    }
                    else
                    {
                        CodeEmpRoundRobinList += "'" + hidbCode.Value + "'";
                    }

                    CheckBox chkEmpRoundRobin = (CheckBox)gvEmpRoundRobin.Rows[i].FindControl("chkEmpRoundRobin");
                    chkEmpRoundRobin.Checked = true;
                }
                else
                {
                    CheckBox chkEmpRoundRobin = (CheckBox)gvEmpRoundRobin.Rows[i].FindControl("chkEmpRoundRobin");
                    chkEmpRoundRobin.Checked = false;
                }
            }
            hidEmpRoundRobinList.Value = CodeEmpRoundRobinList;
        }
        protected void btnAssign_Clicked(object sender, EventArgs e)
        {
            if (ValidateUpdate())
            {
                List<LeadManagementInfo> llInfo = new List<LeadManagementInfo>();

                int? sum = 0;

                foreach (GridViewRow row in gvLeadManagement.Rows)
                {
                    CheckBox chkLead = (CheckBox)row.FindControl("chkLead");

                    if (chkLead.Checked == true)
                    {
                        HiddenField hidLeadID = (HiddenField)row.FindControl("hidLeadID");

                        LeadManagementInfo lInfo = new LeadManagementInfo();

                        lInfo.LeadID = (hidLeadID.Value != "") ? Convert.ToInt32(hidLeadID.Value) : 0;
                        lInfo.AssignEmpCode = hidEmpCodeAssign.Value;
                        lInfo.CallOutStatus = "0";
                        lInfo.Status = "OP";

                        llInfo.Add(lInfo);

                        sum = UpdateLeadAssignment(lInfo);
                    }
                }

                if (sum > 0)
                {

                    L_Leadchkboxstatus = new List<LeadManagementInfo>();
                    LoadgvLeadManagement();
                    setsummarylead();
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');", true);
                }
            }
        }
        protected void btnSelectEmpGroupAssign_Clicked(object sender, EventArgs e)
        {
            Boolean flag = true;
            string error = "";
            int countgridtrue = 0;
            int countgriderr = 0;
            int counterr = 0;

            for (int i = 0; i < gvLeadManagement.Rows.Count; i++)
            {
                CheckBox chkLead = (CheckBox)gvLeadManagement.Rows[i].FindControl("chkLead");

                if (chkLead.Checked == true)
                {
                    flag = (!flag) ? false : true;
                    countgridtrue++;
                }
                else
                {
                    flag = false;
                    countgriderr++;
                }
            }

            if (flag == false)
            {
                error += "กรุณาเลือกรายการ Lead ทุกรายการหรือเลือก Checkbox All อีกครั้ง เนื่องจากเป็นการ Assign แบบ Multi Emp Average \\n";
                counterr++;
            }

            if (ddlEmpBUGroup.SelectedValue != "-99")
            {
                flag = (!flag) ? false : true;
            }
            else
            {
                flag = false;
                error += "Please select BU \\n";
                counterr++;
            }

            if (counterr > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + error + "');", true);
            }
            else
            {
                txtSearchEmpGroup.Text = "";
                txtSearchEmpFNameGroup.Text = "";
                txtSearchEmpLNameGroup.Text = "";

                LoadgvEmpGroup();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-empgroup').modal();", true);
            }
        }
        protected void btnSelectEmpPercentAssign_Clicked(object sender, EventArgs e)
        {
            Boolean flag = true;
            string error = "";
            int countgridtrue = 0;
            int countgriderr = 0;
            int counterr = 0;

            for (int i = 0; i < gvLeadManagement.Rows.Count; i++)
            {
                CheckBox chkLead = (CheckBox)gvLeadManagement.Rows[i].FindControl("chkLead");

                if (chkLead.Checked == true)
                {
                    flag = (!flag) ? false : true;
                    countgridtrue++;
                }
                else
                {
                    flag = false;
                    countgriderr++;
                }
            }

            if (flag == false)
            {
                error += "Please select Lead ทุกรายการหรือเลือก Checkbox All อีกครั้ง เนื่องจากเป็นการ Assign แบบ Multi Emp Average \\n";
                counterr++;
            }

            if (ddlEmpBUPercent.SelectedValue != "-99")
            {
                flag = (!flag) ? false : true;
            }
            else
            {
                flag = false;
                error += "Please select BU \\n";
                counterr++;
            }

            if (counterr > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + error + "');", true);
            }
            else
            {
                txtSearchEmpPercent.Text = "";
                txtSearchEmpFNamePercent.Text = "";
                txtSearchEmpLNamePercent.Text = "";

                LoadgvEmpPercent();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-emppercent').modal();", true);
            }
        }
        protected void btnSelectEmpRoundRobinAssign_Clicked(object sender, EventArgs e)
        {
            Boolean flag = true;
            string error = "";
            int countgridtrue = 0;
            int countgriderr = 0;
            int counterr = 0;

            for (int i = 0; i < gvLeadManagement.Rows.Count; i++)
            {
                CheckBox chkLead = (CheckBox)gvLeadManagement.Rows[i].FindControl("chkLead");

                if (chkLead.Checked == true)
                {
                    flag = (!flag) ? false : true;
                    countgridtrue++;
                }
                else
                {
                    flag = false;
                    countgriderr++;
                }
            }

            if (flag == false)
            {
                error += "Please select Lead ทุกรายการหรือเลือก Checkbox All อีกครั้ง เนื่องจากเป็นการ Assign แบบ Multi Emp Average \\n";
                counterr++;
            }

            if (ddlEmpBURoundRobin.SelectedValue != "-99")
            {
                flag = (!flag) ? false : true;
            }
            else
            {
                flag = false;
                error += "Please select BU \\n";
                counterr++;
            }

            if (counterr > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + error + "');", true);
            }
            else
            {
                txtSearchEmpRoundRobin.Text = "";
                txtSearchEmpFNameRoundRobin.Text = "";
                txtSearchEmpLNameRoundRobin.Text = "";

                LoadgvEmpRoundRobin();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-emproundrobin').modal();", true);
            }
        }
        protected void btnSelectEmpGroup_Clicked(object sender, EventArgs e)
        {
            BindgvEmpGroupAssign();
        }
        protected void btnSelectEmpPercent_Clicked(object sender, EventArgs e)
        {
            BindgvgvEmpAssignPercent();
        }
        protected void btnSelectEmpRoundRobin_Clicked(object sender, EventArgs e)
        {
            BindgvgvEmpAssignRoundRobin();
        }
        protected void radAssignType_SelectedChanged(object sender, EventArgs e)
        {
            if (radAssignType.SelectedValue == "0")
            {
                singleemp.Visible = true;
                groupemp.Visible = false;
                emppercent.Visible = false;
                emproundrobin.Visible = false;
            }
            else if (radAssignType.SelectedValue == "1")
            {
                singleemp.Visible = false;
                groupemp.Visible = true;
                emppercent.Visible = false;
                emproundrobin.Visible = false;

                CheckBox chkLeadAll = (CheckBox)gvLeadManagement.HeaderRow.FindControl("chkLeadAll");
                chkLeadAll.Checked = true;

                foreach (GridViewRow row in gvLeadManagement.Rows)
                {
                    CheckBox chkLead = (CheckBox)row.FindControl("chkLead");

                    if (chkLead.Checked == false)
                    {
                        chkLead.Checked = true;
                    }
                }
            }
            else if (radAssignType.SelectedValue == "2")
            {
                singleemp.Visible = false;
                groupemp.Visible = false;
                emppercent.Visible = true;
                emproundrobin.Visible = false;

                CheckBox chkLeadAll = (CheckBox)gvLeadManagement.HeaderRow.FindControl("chkLeadAll");
                chkLeadAll.Checked = true;

                foreach (GridViewRow row in gvLeadManagement.Rows)
                {
                    CheckBox chkLead = (CheckBox)row.FindControl("chkLead");

                    if (chkLead.Checked == false)
                    {
                        chkLead.Checked = true;
                    }
                }
            }
            else if (radAssignType.SelectedValue == "3")
            {
                singleemp.Visible = false;
                groupemp.Visible = false;
                emppercent.Visible = false;
                emproundrobin.Visible = true;

                CheckBox chkLeadAll = (CheckBox)gvLeadManagement.HeaderRow.FindControl("chkLeadAll");
                chkLeadAll.Checked = true;

                foreach (GridViewRow row in gvLeadManagement.Rows)
                {
                    CheckBox chkLead = (CheckBox)row.FindControl("chkLead");

                    if (chkLead.Checked == false)
                    {
                        chkLead.Checked = true;
                    }
                }
            }
        }
        protected void btnCloseEmp_Click(object sender, EventArgs e)
        {
            List<EmpInfo> leInfo = new List<EmpInfo>();
            GridViewRow currentRow = (GridViewRow)((LinkButton)sender).Parent.Parent;

            HiddenField hidrunningNo = (HiddenField)currentRow.FindControl("hidrunningNo");
            HiddenField hidEmpId = (HiddenField)currentRow.FindControl("hidEmpId");

            L_EmpGroupInfolist.RemoveAll(x => x.EmpId == Convert.ToInt32(hidEmpId.Value));
            leInfo = loadEmpGroupfromRemove(L_EmpGroupInfolist);

            L_EmpGroupInfolist = leInfo;

            CalculateCountLeadAssign(L_EmpGroupInfolist);

            gvEmpAssignGroup.DataSource = L_EmpGroupInfolist;
            gvEmpAssignGroup.DataBind();
        }
        protected void btnCloseEmpAssignPercent_Click(object sender, EventArgs e)
        {
            List<EmpInfo> leInfo = new List<EmpInfo>();
            GridViewRow currentRow = (GridViewRow)((LinkButton)sender).Parent.Parent;

            HiddenField hidrunningNo = (HiddenField)currentRow.FindControl("hidrunningNo");
            HiddenField hidEmpId = (HiddenField)currentRow.FindControl("hidEmpId");

            L_EmpPercentInfolist.RemoveAll(x => x.EmpId == Convert.ToInt32(hidEmpId.Value));
            leInfo = loadEmpPercentfromRemove(L_EmpPercentInfolist);

            L_EmpPercentInfolist = leInfo;

            CalculateCountLeadAssignPercent(L_EmpPercentInfolist);

            gvEmpAssignPercent.DataSource = L_EmpPercentInfolist;
            gvEmpAssignPercent.DataBind();
        }
        protected void btnCloseEmpAssignRoundRobin_Clicked(object sender, EventArgs e)
        {
            List<EmpInfo> leInfo = new List<EmpInfo>();
            GridViewRow currentRow = (GridViewRow)((LinkButton)sender).Parent.Parent;

            HiddenField hidrunningNo = (HiddenField)currentRow.FindControl("hidrunningNo");
            HiddenField hidEmpId = (HiddenField)currentRow.FindControl("hidEmpId");

            L_EmpRoundRobinInfolist.RemoveAll(x => x.EmpId == Convert.ToInt32(hidEmpId.Value));
            leInfo = loadEmpRoundRobinfromRemove(L_EmpRoundRobinInfolist);

            L_EmpRoundRobinInfolist = leInfo;



            gvEmpAssignRoundRobin.DataSource = L_EmpRoundRobinInfolist;
            gvEmpAssignRoundRobin.DataBind();
        }
        protected void btnAssignLeadEmpGroup_Clicked(object sender, EventArgs e)
        {
            List<LeadManagementInfo> llInfo = new List<LeadManagementInfo>();

            if (ValidateAssignLeadEmpGroup())
            {
                // Bind List of Lead List
                foreach (GridViewRow row in gvLeadManagement.Rows)
                {
                    HiddenField hidLeadID = (HiddenField)row.FindControl("hidLeadID");
                    CheckBox chkLead = (CheckBox)row.FindControl("chkLead");

                    if (chkLead.Checked == true)
                    {
                        LeadManagementInfo lInfo = new LeadManagementInfo();

                        lInfo.LeadID = Convert.ToInt32(hidLeadID.Value);

                        llInfo.Add(lInfo);
                    }
                }

                // Bind List of Emp for Assign
                List<EmpInfo> leInfo = new List<EmpInfo>();

                foreach (GridViewRow row in gvEmpAssignGroup.Rows)
                {
                    HiddenField hidEmpCode = (HiddenField)row.FindControl("hidEmpCode");
                    TextBox txtNumberLeadAssign = (TextBox)row.FindControl("txtNumberLeadAssign");

                    EmpInfo eInfo = new EmpInfo();

                    eInfo.EmpCode = hidEmpCode.Value;
                    eInfo.NumberLeadAssign = Convert.ToInt32(txtNumberLeadAssign.Text);

                    leInfo.Add(eInfo);
                }

                // Assign Function
                int? sum = 0;

                foreach (var od in leInfo.ToList())
                {
                    foreach (var oe in llInfo.ToList())
                    {
                        oe.AssignEmpCode = od.EmpCode;
                        oe.CallOutStatus = "0";

                        if (od.NumberLeadAssign > 0)
                        {
                            sum = UpdateLeadAssignment(oe);

                            if (sum > 0)
                            {
                                od.NumberLeadAssign--;
                                llInfo.RemoveAll(x => x.LeadID == oe.LeadID);
                            }
                        }
                    }
                }

                if (sum > 0)
                {
                    L_Leadchkboxstatus = new List<LeadManagementInfo>();
                    L_EmpGroupInfolist = new List<EmpInfo>();

                    foreach (GridViewRow row in gvEmpGroup.Rows)
                    {
                        CheckBox chkEmpGroup = (CheckBox)row.FindControl("chkEmpGroup");

                        if (chkEmpGroup.Checked == true)
                        {
                            chkEmpGroup.Checked = false;
                        }
                    }

                    LoadgvLeadManagement();

                    CheckBox chkLeadAll = (CheckBox)gvLeadManagement.HeaderRow.FindControl("chkLeadAll");
                    chkLeadAll.Checked = true;

                    foreach (GridViewRow row in gvLeadManagement.Rows)
                    {
                        CheckBox chkLead = (CheckBox)row.FindControl("chkLead");

                        if (chkLead.Checked == false)
                        {
                            chkLead.Checked = true;
                        }
                    }

                    BindgvEmpGroupAssign();

                    List<EmpInfo> leInfo1 = new List<EmpInfo>();
                    txtSearchEmpCode.Text = "999";
                    leInfo1 = GetEmpActive();

                    gvEmpAssignGroup.DataSource = leInfo1;
                    gvEmpAssignGroup.DataBind();

                    txtSearchEmpCode.Text = "";

                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');", true);
                }
            }
        }
        protected void btnAssignLeadPercent_Clicked(object sender, EventArgs e)
        {
            List<LeadManagementInfo> llInfo = new List<LeadManagementInfo>();
            int? countlead = 0;

            if (ValidateAssignLeadEmpPercent())
            {
                // Bind List of Lead List
                foreach (GridViewRow row in gvLeadManagement.Rows)
                {
                    HiddenField hidLeadID = (HiddenField)row.FindControl("hidLeadID");
                    CheckBox chkLead = (CheckBox)row.FindControl("chkLead");

                    if (chkLead.Checked == true)
                    {
                        LeadManagementInfo lInfo = new LeadManagementInfo();

                        lInfo.LeadID = Convert.ToInt32(hidLeadID.Value);
                        countlead++;

                        llInfo.Add(lInfo);
                    }
                }

                // Bind List of Emp for Assign
                List<EmpInfo> leInfo = new List<EmpInfo>();
                int? counumlead = 0;

                foreach (GridViewRow row in gvEmpAssignPercent.Rows)
                {
                    HiddenField hidEmpCode = (HiddenField)row.FindControl("hidEmpCode");
                    TextBox txtPercentLeadAssign = (TextBox)row.FindControl("txtPercentLeadAssign");
                    HiddenField hidNumberLeadAssign = (HiddenField)row.FindControl("hidNumberLeadAssign");

                    EmpInfo eInfo = new EmpInfo();

                    eInfo.EmpCode = hidEmpCode.Value;
                    eInfo.NumberLeadAssign = Convert.ToInt32((Convert.ToDouble(txtPercentLeadAssign.Text) * Convert.ToDouble(countlead) / 100));
                    counumlead += eInfo.NumberLeadAssign;

                    leInfo.Add(eInfo);
                }

                // Rebinding NumberLeadAssign เมื่อจำนวน Lead ไม่เท่ากับ Lead ทั้งหมด (น้อยกว่าหรือน้อยกว่า)
                int? diffcou = countlead - counumlead;
                int? invertdiffcou = counumlead - countlead;

                if (diffcou > 0)
                {
                    foreach (var og in leInfo)
                    {
                        if (diffcou > 0)
                        {
                            og.NumberLeadAssign++;
                            diffcou--;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    foreach (var og in leInfo)
                    {
                        if (invertdiffcou > 0)
                        {
                            og.NumberLeadAssign--;
                            invertdiffcou--;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                // Assign Function
                int? sum = 0;

                foreach (var od in leInfo.ToList())
                {
                    foreach (var oe in llInfo.ToList())
                    {
                        oe.AssignEmpCode = od.EmpCode;
                        oe.CallOutStatus = "0";

                        if (od.NumberLeadAssign > 0)
                        {
                            sum = UpdateLeadAssignment(oe);

                            if (sum > 0)
                            {
                                od.NumberLeadAssign--;
                                llInfo.RemoveAll(x => x.LeadID == oe.LeadID);
                            }
                        }
                    }
                }

                if (sum > 0)
                {
                    L_Leadchkboxstatus = new List<LeadManagementInfo>();
                    L_EmpPercentInfolist = new List<EmpInfo>();

                    foreach (GridViewRow row in gvEmpPercent.Rows)
                    {
                        CheckBox chkEmpPercent = (CheckBox)row.FindControl("chkEmpPercent");

                        if (chkEmpPercent.Checked == true)
                        {
                            chkEmpPercent.Checked = false;
                        }
                    }

                    LoadgvLeadManagement();

                    CheckBox chkLeadAll = (CheckBox)gvLeadManagement.HeaderRow.FindControl("chkLeadAll");
                    chkLeadAll.Checked = true;

                    foreach (GridViewRow row in gvLeadManagement.Rows)
                    {
                        CheckBox chkLead = (CheckBox)row.FindControl("chkLead");

                        if (chkLead.Checked == false)
                        {
                            chkLead.Checked = true;
                        }
                    }

                    BindgvgvEmpAssignPercent();

                    List<EmpInfo> leInfo1 = new List<EmpInfo>();
                    txtSearchEmpCode.Text = "999";
                    leInfo1 = GetEmpActive();

                    gvEmpAssignPercent.DataSource = leInfo1;
                    gvEmpAssignPercent.DataBind();

                    txtSearchEmpCode.Text = "";

                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');", true);
                }
            }
        }
        protected void btnAssignLeadRoundRobin_Clicked(object sender, EventArgs e)
        {
            List<LeadManagementInfo> llInfo = new List<LeadManagementInfo>();
            int? countlead = 0;
            int? countemp = 0;

            if (ValidateAssignLeadEmpRoundRobin())
            {
                // Bind List of Lead List
                foreach (GridViewRow row in gvLeadManagement.Rows)
                {
                    HiddenField hidLeadID = (HiddenField)row.FindControl("hidLeadID");
                    CheckBox chkLead = (CheckBox)row.FindControl("chkLead");

                    if (chkLead.Checked == true)
                    {
                        LeadManagementInfo lInfo = new LeadManagementInfo();

                        lInfo.LeadID = Convert.ToInt32(hidLeadID.Value);

                        countlead++;
                        lInfo.runningNo = countlead;

                        llInfo.Add(lInfo);
                    }
                }

                // Bind List of Emp for Assign
                List<EmpInfo> leInfo = new List<EmpInfo>();

                foreach (GridViewRow row in gvEmpAssignRoundRobin.Rows)
                {
                    HiddenField hidEmpCode = (HiddenField)row.FindControl("hidEmpCode");
                    HiddenField hidrunningNo = (HiddenField)row.FindControl("hidrunningNo");
                    HiddenField hidNumberLeadAssign = (HiddenField)row.FindControl("hidNumberLeadAssign");

                    EmpInfo eInfo = new EmpInfo();

                    eInfo.EmpCode = hidEmpCode.Value;
                    eInfo.runningNo = (hidrunningNo.Value != "") ? Convert.ToInt32(hidrunningNo.Value) : 0;
                    countemp++;

                    leInfo.Add(eInfo);
                }

                int? numbermultiplelist = 0;
                int? leftassignlist = 0;

                if (countemp > 0)
                {
                    numbermultiplelist = countlead / countemp;
                    leftassignlist = countlead % countemp;
                }

                // Bind List of Emp for Round compare Lead List
                List<EmpInfo> leround = new List<EmpInfo>();
                int? countround = 0;

                for (int i = 0; i < numbermultiplelist; i++)
                {
                    foreach (var od in leInfo)
                    {
                        EmpInfo eInfo = new EmpInfo();

                        eInfo.EmpCode = od.EmpCode;

                        countround++;
                        eInfo.runningNo = countround;

                        leround.Add(eInfo);
                    }
                }

                foreach (var od in leInfo)
                {
                    if (leftassignlist > 0)
                    {
                        EmpInfo eInfo = new EmpInfo();

                        eInfo.EmpCode = od.EmpCode;

                        int? max = leround.Max(r => r.runningNo);
                        max = (max == null) ? 0 : max;
                        eInfo.runningNo = max + 1;
                        leftassignlist--;

                        leround.Add(eInfo);
                    }
                    else
                    {
                        break;
                    }
                }

                // Rounding Assign Function
                int? sum = 0;
                int? sum1 = 0;

                foreach (var og in leround.ToList())
                {
                    if (leround.Count > 0)
                    {
                        foreach (var ol in llInfo.ToList())
                        {
                            if (og.runningNo == ol.runningNo)
                            {
                                ol.AssignEmpCode = og.EmpCode;
                                ol.CallOutStatus = "0";

                                sum = UpdateLeadAssignment(ol);

                                if (sum > 0)
                                {
                                    llInfo.RemoveAll(x => x.runningNo == ol.runningNo);
                                    leround.RemoveAll(x => x.runningNo == og.runningNo);
                                }
                            }
                        }
                    }
                }

                if (sum > 0)
                {
                    L_Leadchkboxstatus = new List<LeadManagementInfo>();
                    L_EmpRoundRobinInfolist = new List<EmpInfo>();

                    foreach (GridViewRow row in gvEmpRoundRobin.Rows)
                    {
                        CheckBox chkEmpRoundRobin = (CheckBox)row.FindControl("chkEmpRoundRobin");

                        if (chkEmpRoundRobin.Checked == true)
                        {
                            chkEmpRoundRobin.Checked = false;
                        }
                    }

                    LoadgvLeadManagement();

                    CheckBox chkLeadAll = (CheckBox)gvLeadManagement.HeaderRow.FindControl("chkLeadAll");
                    chkLeadAll.Checked = true;

                    foreach (GridViewRow row in gvLeadManagement.Rows)
                    {
                        CheckBox chkLead = (CheckBox)row.FindControl("chkLead");

                        if (chkLead.Checked == false)
                        {
                            chkLead.Checked = true;
                        }
                    }

                    BindgvgvEmpAssignRoundRobin();

                    List<EmpInfo> leInfo1 = new List<EmpInfo>();
                    txtSearchEmpCode.Text = "999";
                    leInfo1 = GetEmpActive();

                    gvEmpAssignRoundRobin.DataSource = leInfo1;
                    gvEmpAssignRoundRobin.DataBind();

                    txtSearchEmpCode.Text = "";

                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');", true);
                }
            }
        }
        protected void chkLead_CheckedChanged(object sender, EventArgs e)
        {
            List<LeadManagementInfo> llInfo = new List<LeadManagementInfo>();

            if (L_Leadchkboxstatus.Count > 0)
            {
                L_Leadchkboxstatus.RemoveAll(x => x.currentpagenumber == currentPageNumber);
            }

            for (int i = 0; i < gvLeadManagement.Rows.Count; i++)
            {
                CheckBox chkLead = (CheckBox)gvLeadManagement.Rows[i].FindControl("chkLead");
                HiddenField hidLeadID = (HiddenField)gvLeadManagement.Rows[i].FindControl("hidLeadID");
                Label lblCustomerCode = (Label)gvLeadManagement.Rows[i].FindControl("lblCustomerCode");
                Label lblCustomerPhone = (Label)gvLeadManagement.Rows[i].FindControl("lblCustomerPhone");
                Label lblCustomerName = (Label)gvLeadManagement.Rows[i].FindControl("lblCustomerName");
                Label lblCreateDate = (Label)gvLeadManagement.Rows[i].FindControl("lblCreateDate");

                if (chkLead.Checked == true)
                {
                    LeadManagementInfo lInfo = new LeadManagementInfo();

                    lInfo.CustomerCode = lblCustomerCode.Text;
                    lInfo.CustomerPhone = lblCustomerPhone.Text;
                    lInfo.CustomerName = lblCustomerName.Text;
                    lInfo.CreateDate = lblCreateDate.Text;
                    lInfo.chkboxstatus = "true";
                    lInfo.LeadID = Convert.ToInt32(hidLeadID.Value);
                    lInfo.currentpagenumber = currentPageNumber;

                    llInfo.Add(lInfo);
                }
                else
                {
                    LeadManagementInfo lInfo = new LeadManagementInfo();

                    lInfo.CustomerCode = lblCustomerCode.Text;
                    lInfo.CustomerPhone = lblCustomerPhone.Text;
                    lInfo.CustomerName = lblCustomerName.Text;
                    lInfo.CreateDate = lblCreateDate.Text;
                    lInfo.chkboxstatus = "false";
                    lInfo.LeadID = Convert.ToInt32(hidLeadID.Value);
                    lInfo.currentpagenumber = currentPageNumber;

                    llInfo.Add(lInfo);
                }
            }

            if (L_Leadchkboxstatus.Count > 0)
            {
                L_Leadchkboxstatus.AddRange(llInfo);
            }
            else
            {
                L_Leadchkboxstatus = llInfo;
            }
        }
        protected void gvLeadManagement_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkall = (CheckBox)gvLeadManagement.HeaderRow.FindControl("chkLeadAll");
                CheckBox chkLead = (CheckBox)e.Row.FindControl("chkLead");
                HiddenField hidLeadID = (HiddenField)e.Row.FindControl("hidLeadID");

                string sta = "";

                for (int i = 0; i < L_Leadchkboxstatus.Count; i++)
                {
                    if (L_Leadchkboxstatus[i].currentpagenumber == currentPageNumber)
                    {
                        sta = L_Leadchkboxstatus[i].checkboxallstatus;
                    }
                }

                if (sta == "true")
                {
                    chkall.Checked = true;
                }
                else
                {
                    chkall.Checked = false;
                }

                foreach (var od in L_Leadchkboxstatus.ToList())
                {
                    if (od.currentpagenumber == currentPageNumber && od.LeadID == Convert.ToInt32(hidLeadID.Value))
                    {
                        if (od.chkboxstatus == "true")
                        {
                            chkLead.Checked = true;
                        }
                        else if (od.chkboxstatus == "false")
                        {
                            chkLead.Checked = false;
                        }
                    }
                }
            }
        }
        protected void txtNumberLeadAssign_Clicked(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvEmpAssignGroup.Rows)
            {
                TextBox txtNumberLeadAssign = (TextBox)row.FindControl("txtNumberLeadAssign");

                int? d = (txtNumberLeadAssign.Text != "") ? Convert.ToInt32(txtNumberLeadAssign.Text) : 0;
                txtNumberLeadAssign.Text = Convert.ToInt32(d).ToString();
            }
        }
        protected void txtPercentLeadAssign_TextChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvEmpAssignPercent.Rows)
            {
                TextBox txtPercentLeadAssign = (TextBox)row.FindControl("txtPercentLeadAssign");

                double? d = (txtPercentLeadAssign.Text != "") ? Convert.ToDouble(txtPercentLeadAssign.Text) : 0;
                txtPercentLeadAssign.Text = string.Format("{0:n}", (Convert.ToDouble(d)));
            }
        }
        #endregion

        #region CallOMSCore
        protected void CallOMSCore(List<LeadManagementInfo> ListLeadManagementInfo)
        {
            List<LeadManagementInfo> llInfo = new List<LeadManagementInfo>();
            List<LeadManagementInfo> lcallomscore = new List<LeadManagementInfo>();

            foreach (var od in ListLeadManagementInfo.ToList())
            {
                llInfo = GetLeadManagementNoPaging(od.LeadID);

                lcallomscore.AddRange(llInfo);
            }

            APIpath = "https://doublep.dlinkddns.com:8081/oms/api/customer/finishedorder";

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;

                var jsonObj = JsonConvert.SerializeObject(new
                {
                    lcallomscore
                });

                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var dataString = client.UploadString(APIpath, jsonObj);
            }
        }
        #endregion
    }
}