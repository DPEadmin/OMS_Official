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

namespace DOMS_TSR.src.Organization
{
    public partial class UserDetail : System.Web.UI.Page
    {
        protected static string APIUrl;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static int currentPageNumber;
        protected static int currentPromotionPageNumber;
        protected static int currentPromotionDetailPageNumber;
        string APIpath = "";
        string Codelist = "";
        Boolean isdelete;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;
                currentPromotionPageNumber = 1;
                currentPromotionDetailPageNumber = 1;

                EmpInfo empInfo = new EmpInfo();
                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                    APIUrl = empInfo.ConnectionAPI;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                txtUserNameIns.ReadOnly = true;
                txtPasswordIns.ReadOnly = true;
                hidEmpCodeafterInsert.Value = "";

                loadEmpInformation();
                loadEmpUserLoginInformation();

                litLinkBack.Text = "<a href=\"UserAuthorization.aspx" + "\" class=\"font11link\"> << Back</a>";
            }
        }

        #region event
        protected void menuTabs_MenuItemClick(object sender, MenuEventArgs e)
        {
            multiTabs.ActiveViewIndex = Int32.Parse(menuTabs.SelectedValue);
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];

            UserLoginInfo uInfo = new UserLoginInfo();
            List<UserLoginInfo> luInfo = new List<UserLoginInfo>();
            String username = "";

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
            }
            else
            {
                if (hidFlagInsert.Value == "True") // Insert UserLogin
                {
                    if(ValidateInsertUpdate())
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertUserLogin";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["EmpCode"] = Request.QueryString["EmpCodeTemp"];
                            data["EmpCodeTemp"] = Request.QueryString["EmpCodeTemp"];
                            data["Username"] = txtUserNameIns.Text;
                            data["Password"] = txtPasswordIns.Text;
                            data["CreateBy"] = hidEmpCode.Value;
                            data["UpdateBy"] = hidEmpCode.Value;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }
                        int sum = JsonConvert.DeserializeObject<int>(respstr);
                        if (sum > 0)
                        {
                            txtUserNameIns.ReadOnly = true;
                            txtPasswordIns.ReadOnly = true;
                            btnSyncUser.Enabled = true;

                            //PostCreateEmptoOneAppApi(lblEmpCode.Text, username); ***Open this code for system need to post API to 3rd party for sync emp data (Can apply for another API)

                            
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                }
                else // Update UserLogin
                {
                    if(ValidateInsertUpdate())
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateUserLogin";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["UserLoginId"] = hidEmpId.Value;
                            if (hidEmpCodeafterInsert.Value == "")
                            {
                                data["EmpCode"] = Request.QueryString["EmpCodeTemp"];
                            }
                            else
                            {
                                data["EmpCode"] = hidEmpCodeafterInsert.Value;
                            }
                            data["Username"] = txtUserNameIns.Text;
                            data["Password"] = txtPasswordIns.Text;
                            data["UpdateBy"] = hidEmpCode.Value;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }
                        int sum = JsonConvert.DeserializeObject<int>(respstr);
                        if (sum > 0)
                        {
                            txtUserNameIns.ReadOnly = true;
                            txtPasswordIns.ReadOnly = true;
                            luInfo = GetUserNamePassword(lblEmpCode.Text);
                            if (luInfo.Count > 0)
                            {
                                username = luInfo[0].Username;
                            }
                            PostUpdateEmptoOneAppApi(lblEmpCode.Text, username);
                            if (hidEmpCodeafterInsert.Value == "")
                            {
                                loadEmpUserLoginInformation();
                            }
                            else
                            {
                                ReloadUserLoginInformation(hidEmpCodeafterInsert.Value);
                            }
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                        }
                    }
                }
            }
        }
        protected void btnSyncUser_Click(object sender, EventArgs e)
        {
            UserLoginInfo uInfo = new UserLoginInfo();
            List<UserLoginInfo> luInfo = new List<UserLoginInfo>();
            String username = "";
            luInfo = GetUserNamePassword(lblEmpCode.Text);
            if (luInfo.Count > 0)
            {
                username = luInfo[0].Username;
            }

            PostCreateEmptoOneAppApi(lblEmpCode.Text, username);
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            txtUserNameIns.ReadOnly = false;
            txtPasswordIns.ReadOnly = false;
        }
        protected void BacktoEmpMaster_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region function
        protected void loadEmpInformation()
        {
            EmpInfo eInfo = new EmpInfo();
            List<EmpInfo> leInfo = new List<EmpInfo>();
            String empCode = "";
            leInfo = GetEmpMasterByCriteria(empCode);
            if (leInfo.Count > 0)
            {
                lblEmpCode.Text = leInfo[0].EmpCodeTemp;
                lblrefCode.Text = leInfo[0].RefCode;
                lblEmpName.Text = leInfo[0].EmpName_TH;
                hidEmpId.Value = leInfo[0].EmpId.ToString();

                if (leInfo[0].ActiveFlag == "Y")
                {
                    lblEmpStatus.Text = "Active";
                }
                else
                {
                    lblEmpStatus.Text = "Inactive";
                }
                if(leInfo[0].RefCode == "" || leInfo[0].RefCode == null)
                {
                    btnSyncUser.Visible = true;
                    btnSyncUser.Enabled = false;
                    btnSyncUserSuccess.Visible = false;
                }
                else
                {
                    btnSyncUser.Visible = false;
                }
            }
        }
        protected List<EmpInfo> GetEmpMasterByCriteria(String empcode)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListEmpByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                if (empcode == "" || empcode == null)
                {
                    data["EmpCodeTemp"] = Request.QueryString["EmpCodeTemp"];
                }
                else
                {
                    data["EmpCode"] = empcode;
                }

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<EmpInfo> lEmpInfo = JsonConvert.DeserializeObject<List<EmpInfo>>(respstr);

            return lEmpInfo;
        }
        protected void loadEmpUserLoginInformation()
        {
            UserLoginInfo uInfo = new UserLoginInfo();
            List<UserLoginInfo> luInfo = new List<UserLoginInfo>();
            String empCode = "";
            luInfo = GetUserLoginMasterbyCriteria(empCode);
            if(luInfo.Count > 0)
            {
                txtUserNameIns.Text = luInfo[0].Username;
                txtPasswordIns.Text = luInfo[0].Password;
                hidEmpId.Value = luInfo[0].UserLoginId.ToString();
                hidFlagInsert.Value = "False";
            }
            else
            {
                hidFlagInsert.Value = "True";
                txtUserNameIns.Text = "";
                txtPasswordIns.Text = "";
            }
        }
        protected List<UserLoginInfo> GetUserLoginMasterbyCriteria(String empcode)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListUserLoginByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                if (empcode == "" || empcode == null)
                {
                    data["EmpCodeTemp"] = Request.QueryString["EmpCodeTemp"];
                }
                else
                {
                    data["EmpCode"] = empcode;
                }
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<UserLoginInfo> lUserLoginInfo = JsonConvert.DeserializeObject<List<UserLoginInfo>>(respstr);

            return lUserLoginInfo;
        }
        protected Boolean ValidateInsertUpdate()
        {
            Boolean flag = true;

            if (txtUserNameIns.Text == "")
            {
                flag = false;
                lblUserNameIns.Text = "Please Insert UserName";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblUserNameIns.Text = "";
            }
            if (txtPasswordIns.Text == "")
            {
                flag = false;
                lblPasswordIns.Text = "Please Insert Password";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblPasswordIns.Text = "";
            }

            return flag;
        }
        protected void PostCreateEmptoOneAppApi(String empCode, String uCode)
        {
            sendCreateempdatatoOneApp empdatasend = new sendCreateempdatatoOneApp();

            List<EmpInfo> leInfo = new List<EmpInfo>();
            leInfo = GetEmpListbyRefCode(empCode);

            foreach (var od in leInfo)
            {
                empdatasend.EmpCode = empCode;
                empdatasend.EmpFName = od.EmpFname_TH;
                empdatasend.EmpLName = od.EmpLname_TH;
                empdatasend.EmpStatus = od.ActiveFlag; // "Y" or "N"
                empdatasend.UserLevel = "1"; // default = "1"
                empdatasend.UserName = uCode;

            }

            APIpath = "http://doublep.three-rd.com:3230/api/1.0.0/oms/user?access_token=X2LCUDLQoWlqpZoNDhijsUvp9ytvVUAl1YIUSCjqm2BSTUVbGitzHBIJGBvdS8DS";
            List<msgreturnOneAPP> lresult = new List<msgreturnOneAPP>();
            String refcodefromOneApp = "";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var jsonObj = JsonConvert.SerializeObject(new { empdatasend.EmpCode, empdatasend.EmpFName, empdatasend.EmpLName, empdatasend.EmpStatus, empdatasend.UserLevel, empdatasend.UserName });
                var dataString = client.UploadString(APIpath, jsonObj);
                var data = JsonConvert.DeserializeObject<List<msgreturnOneAPP>>(dataString);
                lresult = data;
            }
            if (lresult.Count > 0)
            {
                foreach (var od in lresult)
                {
                    refcodefromOneApp = od.resultData.ref_code;
                }
            }
            hidRefCodefromOneApp.Value = refcodefromOneApp;
            int sum = UpdateRefCodefromOneApptoOMSEmp(refcodefromOneApp); // Go to Update RefCode and EmpCode = RefCode to OMS Table Emp
            int sum1 = 0;

            if(sum > 0)
            {
                sum1 = UpdateEmpCodefromRefCodetoOMSUserLogin(refcodefromOneApp);
            }
            if(sum1 > 0)
            {
                ReloadEmpInformation(refcodefromOneApp);
                ReloadUserLoginInformation(refcodefromOneApp);
                btnSyncUser.Visible = false;
                btnSyncUserSuccess.Visible = true;
            }

        }
        protected void PostUpdateEmptoOneAppApi(String empCode, String uCode)
        {
            sendUpdateempdatatoOneApp empupdatasend = new sendUpdateempdatatoOneApp();

            List<EmpInfo> leInfo = new List<EmpInfo>();
            leInfo = GetEmpListbyRefCode(empCode);

            foreach (var od in leInfo)
            {
                empupdatasend.EmpCode = od.EmpCode;
                empupdatasend.EmpFName = od.EmpFname_TH;
                empupdatasend.EmpLName = od.EmpLname_TH;
                empupdatasend.EmpStatus = od.ActiveFlag; // "Y" or "N"
                empupdatasend.UserLevel = "1"; // default = "1"
                empupdatasend.UserName = uCode;
                empupdatasend.RefCode = od.RefCode;
            }

            APIpath = "http://doublep.three-rd.com:3230/api/1.0.0/oms/user?access_token=X2LCUDLQoWlqpZoNDhijsUvp9ytvVUAl1YIUSCjqm2BSTUVbGitzHBIJGBvdS8DS";
            List<msgreturnOneAPP> lresult = new List<msgreturnOneAPP>();
            String refcodefromOneApp = "";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var jsonObj = JsonConvert.SerializeObject(new { empupdatasend.EmpCode, empupdatasend.EmpFName, empupdatasend.EmpLName, empupdatasend.EmpStatus, empupdatasend.UserLevel, empupdatasend.UserName, empupdatasend.RefCode });
                var dataString = client.UploadString(APIpath, jsonObj);
                var data = JsonConvert.DeserializeObject<List<msgreturnOneAPP>>(dataString);
                lresult = data;
            }
            if (lresult.Count > 0)
            {
                foreach (var od in lresult)
                {
                    refcodefromOneApp = od.resultData.ref_code;
                }
            }
        }
        protected List<EmpInfo> GetEmpListbyRefCode(String eCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCodeTemp"] = eCode;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpInfo> lEmpInfo = JsonConvert.DeserializeObject<List<EmpInfo>>(respstr);
            return lEmpInfo;
        }
        protected List<UserLoginInfo> GetUserNamePassword(String eCode)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListUserLoginByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["EmpCode"] = eCode;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<UserLoginInfo> lUserLoginInfo = JsonConvert.DeserializeObject<List<UserLoginInfo>>(respstr);

            return lUserLoginInfo;
        }
        protected int UpdateRefCodefromOneApptoOMSEmp(String refCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/UpdateCreateRefCodefromOneApp";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpId"] = hidEmpId.Value;
                
                data["RefCode"] = refCode;
                data["UpdateBy"] = hidEmpCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            int sum = JsonConvert.DeserializeObject<int>(respstr);

            return sum;
        }
        protected int UpdateEmpCodefromRefCodetoOMSUserLogin(String refCode)
        {
            List<UserLoginInfo> ulInfo = new List<UserLoginInfo>();
            ulInfo = GetUserNamePassword(lblEmpCode.Text);
            int? userloginId = 0;

            if (ulInfo.Count > 0)
            {
               foreach(var od in ulInfo)
                {
                    userloginId = od.UserLoginId;
                }
            }

            string respstr = "";

            APIpath = APIUrl + "/api/support/UpdateEmpCodefromRefCodefromOneApp";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["UserLoginId"] = userloginId.ToString();
                data["EmpCode"] = refCode;
                data["UpdateBy"] = hidEmpCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            int sum = JsonConvert.DeserializeObject<int>(respstr);

            return sum;
        }
        protected void ReloadEmpInformation(String refcode)
        {
            EmpInfo eInfo = new EmpInfo();
            List<EmpInfo> leInfo = new List<EmpInfo>();
            leInfo = GetEmpMasterByCriteria(refcode);
            if (leInfo.Count > 0)
            {
                lblEmpCode.Text = leInfo[0].EmpCodeTemp;
                lblrefCode.Text = leInfo[0].RefCode;
                lblEmpName.Text = leInfo[0].EmpName_TH;
                hidEmpId.Value = leInfo[0].EmpId.ToString();

                if (leInfo[0].ActiveFlag == "Y")
                {
                    lblEmpStatus.Text = "Active";
                }
                else
                {
                    lblEmpStatus.Text = "Inactive";
                }
            }
        }
        protected void ReloadUserLoginInformation(String empcode)
        {
            UserLoginInfo uInfo = new UserLoginInfo();
            List<UserLoginInfo> luInfo = new List<UserLoginInfo>();
            luInfo = GetUserLoginMasterbyCriteria(empcode);
            if (luInfo.Count > 0)
            {
                txtUserNameIns.Text = luInfo[0].Username;
                txtPasswordIns.Text = luInfo[0].Password;
                hidEmpId.Value = luInfo[0].UserLoginId.ToString();
                hidFlagInsert.Value = "False";
                hidEmpCodeafterInsert.Value = empcode;
            }
            else
            {
                hidFlagInsert.Value = "True";
                txtUserNameIns.Text = "";
                txtPasswordIns.Text = "";
                hidEmpCodeafterInsert.Value = "";
            }
        }
        #endregion
    }
}