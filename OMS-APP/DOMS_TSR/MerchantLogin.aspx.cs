using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using SALEORDER.DTO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using System.Configuration;
using System.IO.Packaging;

namespace DOMS_TSR
{
    public partial class MerchantLogin : System.Web.UI.Page
    {
        protected static string APIUrlLogin = ConfigurationManager.AppSettings["APILogin"];
        protected static string Account_Name = ConfigurationManager.AppSettings["Account_Name"];
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        string APIpath = ConfigurationManager.AppSettings["APIUrl"];
        int? EmpExpire = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
            //txtUserName.Text = "pan006";
            //txtPassword.Text = "1234";
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (txtPassword.Text == "" || txtPassword.Text == null)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "ไม่สามารถเข้าใช้งานระบบได้ กรุณา ระบุ PASSWORD');", true);
                return;
            }

            if (txtUserName.Text == "" || txtUserName.Text == null)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "ไม่สามารถเข้าใช้งานระบบได้ กรุณา ระบุ USERNAME');", true);
                return;
            }

            LCCInfo LCCInfo = new LCCInfo();
            LCCInfo.Account_Name = Account_Name;
            List<LCCInfo> list_LCCReturn = GetLCC(LCCInfo);

            if (list_LCCReturn.Count > 0)
            {
                if (!PurchaseCheck(list_LCCReturn))
                {
                    if (ExpiredCheck(list_LCCReturn))
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "License Expired กรุณาติดต่อเจ้าหน้าที่');", true);
                        return;
                    }
                }
            }

            List<EmpLoginInfo> EmpObjectAPIHost = GetConnectAPI();
            if (EmpObjectAPIHost.Count > 0)
            {                
                List <EmpInfo> EmpObject = GetLogin(EmpObjectAPIHost[0].Username.ToString(), EmpObjectAPIHost[0].Password.ToString());                

                MerchantInfo merchant = new MerchantInfo();
                EmpInfo emp = new EmpInfo();

                if (EmpObject.Count > 0)
                {                    
                    emp.Username = EmpObject[0].Username;

                    emp.Password = EmpObject[0].Password;

                    emp.ActiveFlag = "Y";

                    emp.EmpCode = EmpObject[0].EmpCode;

                    emp.EmpName_TH = EmpObject[0].EmpName_TH;

                    emp.PositionCode = EmpObject[0].PositionCode;

                    emp.EmpFname_TH = EmpObject[0].EmpFname_TH;

                    emp.EmpLname_TH = EmpObject[0].EmpLname_TH;

                    emp.ExtensionID = EmpObject[0].ExtensionID;

                    emp.Prefix = EmpObject[0].Prefix;

                    emp.ConnectionAPI = EmpObjectAPIHost[0].ConnectionAPI;
                    emp.CompanyCode = EmpObjectAPIHost[0].CompanyCode;
                    emp.EmpExpire = EmpExpire;
                    emp.BuCode = EmpObject[0].BuCode;
                    emp.RoleCode = EmpObject[0].RoleCode;

                    List<MerchantInfo> lmerchantInfo = GetMerchantFromUserLogin(EmpObject[0].EmpCode);

                    if (lmerchantInfo.Count > 0)
                    {
                        if (lmerchantInfo[0].MerchantCode == null || lmerchantInfo[0].MerchantCode == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "ไม่สามารถเข้าใช้งานระบบได้ กรุณาตั้งค่า Usernameนี้กับ Merchant');", true);
                            return;                            
                        }
                        else
                        {
                            merchant.MerchantCode = lmerchantInfo[0].MerchantCode;
                            merchant.MerchantName = lmerchantInfo[0].MerchantName;

                            Session["MerchantCode"] = lmerchantInfo[0].MerchantCode;
                            Session["MerchantInfo"] = merchant;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "ไม่สามารถเข้าใช้งานระบบได้ กรุณาตั้งค่า Usernameนี้กับ Merchant');", true);
                        return;
                    }

                    Session["EmpInfo"] = emp;                    

                    List<EmpRole> EmpRoleObject = getRole();

                    List<MerchantInfo> MerchantGetUsername = MerChantMappingUserName(EmpObject[0].EmpCode, merchant.MerchantCode);
                    if (MerchantGetUsername.Count < 1)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "ไม่สามารถเข้าใช้งานระบบได้ กรุณา เลือก Merchant ที่ถูกต้องกับ Username นี้');", true);
                        return;
                    }
                    if (EmpRoleObject[0].RoleCode == "TELE") { Response.Redirect("./src/CallInfo/CallInfo.aspx"); } else { Response.Redirect("./src/Main.aspx"); }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "ไม่สามารถเข้าใช้งานระบบได้ กรุณาติดต่อเจ้าหน้าที่');", true);

                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "ไม่สามารถเข้าใช้งานระบบได้ กรุณาติดต่อเจ้าหน้าที่');", true);

            }

        }
        public List<MerchantInfo> MerChantMappingUserName(string Empcode, string MerchantCode)
        {
            string respstr = "";
            APIUrl = ConfigurationManager.AppSettings["apiurl"];
            APIpath = APIUrl + "/api/support/ListMerchantMappingUserName";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Username"] = Empcode;
                data["MerchantCode"] = MerchantCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<MerchantInfo> cMerchantInfo = JsonConvert.DeserializeObject<List<MerchantInfo>>(respstr);
            return cMerchantInfo;
        }
        public List<EmpLoginInfo> GetConnectAPI()
        {
            string respstr = "";

            APIpath = APIUrlLogin + "/api/support/GetLogin";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["Username"] = txtUserName.Text;
                data["Password"] = txtPassword.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpLoginInfo> emp_list = JsonConvert.DeserializeObject<List<EmpLoginInfo>>(respstr);



            return emp_list;
        }

        public List<EmpInfo> GetLogin(string SUsername, string SPassword)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/GetLogin";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["Username"] = SUsername;
                data["Password"] = SPassword;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpInfo> emp_list = JsonConvert.DeserializeObject<List<EmpInfo>>(respstr);



            return emp_list;
        }
        protected List<EmpRole> getRole()
        {

            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpRoleNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = empInfo.EmpCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpRole> emprole_list = JsonConvert.DeserializeObject<List<EmpRole>>(respstr);


            return emprole_list;


        }

        public List<LCCInfo> GetLCC(LCCInfo lccInfo)
        {
            string respstr = "";
            APIUrl = ConfigurationManager.AppSettings["apiurl"];
            APIpath = APIUrl + "/api/support/GetLCC";
            List<LCCInfo> lccReturn = null;
            try
            {
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["LCCID"] = lccInfo.LCCID.ToString();
                    data["Account_Name"] = lccInfo.Account_Name;
                    data["Limit"] = lccInfo.Limit.ToString(); ;
                    data["CountRun"] = lccInfo.CountRun.ToString();

                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                lccReturn = JsonConvert.DeserializeObject<List<LCCInfo>>(respstr);
                return lccReturn;

            }
            catch (Exception ex)
            {
                return lccReturn;
            }
        }

        public bool PurchaseCheck(List<LCCInfo> listLCC)
        {
            bool purchased = false;
            if (listLCC[0].X == 0)
            {
                return purchased;
            }
            else if (listLCC[0].X == 1)
            {
                purchased = true;
                return purchased;
            }
            else
            {
                return purchased;
            }
        }

        public bool ExpiredCheck(List<LCCInfo> listLCC)
        {
            bool expired = true;
            if (listLCC[0].CountRun < listLCC[0].Limit)
            {
                expired = false;

                EmpExpire = listLCC[0].Expire;
                return expired;
            }

            else
            {

                return expired;
            }
        }

        protected List<MerchantInfo> GetMerchantFromUserLogin(string username)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListMerchantMappingUserName";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["Username"] = username;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<MerchantInfo> merchantlist = JsonConvert.DeserializeObject<List<MerchantInfo>>(respstr);

            return merchantlist;
        }
    }
}