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
namespace DOMS_TSR
{
  
    public partial class _DefaultAston : Page
    {
       
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            List<EmpInfo> EmpObject = GetLogin();

            EmpInfo emp = new EmpInfo();

            try
            {
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


                    Session["EmpInfo"] = emp;
                    List<EmpRole> EmpRoleObject = getRole();
                    if (EmpRoleObject[0].RoleCode == "TELE") { Response.Redirect("./src/CallInfo/CallInfo.aspx"); } else { Response.Redirect("./src/Main.aspx"); }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "ไม่สามารถเข้าใช้งานระบบได้ กรุณาติดต่อเจ้าหน้าที่');", true);

                }
            }
            catch 
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "ไม่สามารถเข้าใช้งานระบบได้ กรุณาติดต่อเจ้าหน้าที่');", true);
            }
    
        }
        
        public List<EmpInfo> GetLogin()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/GetLogin";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["Username"] = txtUserName.Text;
             

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
    }
}