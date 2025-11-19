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
  
    public partial class Default_OMS : Page
    {
       
        protected static string APIUrl = ConfigurationManager.AppSettings["APILogin"];
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            txtUserName.Focus();
        }
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (txtPassword.Text == "" || txtPassword.Text == null) 
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "ไม่สามารถเข้าใช้งานระบบได้ กรุณา PASSWORD');", true);
                return;
            }

            if (txtUserName.Text == "" || txtUserName.Text == null)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "ไม่สามารถเข้าใช้งานระบบได้ กรุณา USERNAME');", true);
                return;
            }
            List<EmpLoginInfo> EmpObject = GetLogin();

            EmpLoginInfo emp = new EmpLoginInfo();


            if (EmpObject.Count > 0)
            {
                emp.Username = EmpObject[0].Username;

                emp.Password = EmpObject[0].Password;

                EmpDBControlInfo empDB = new EmpDBControlInfo();
                empDB.Databasename= EmpObject[0].Databasename;
                empDB.Severname = EmpObject[0].Severname;
                empDB.DBUsername = EmpObject[0].DBUsername;
                empDB.DBPassword = EmpObject[0].DBPassword;
                Session["EmpDBControlInfo"]  = empDB;
                Response.Redirect(EmpObject[0].CompanyUrl+"?Username="+ EmpObject[0].Username+"&Password="+ EmpObject[0].Password);





                Session["EmpInfo"] = emp;
                
             
            }
            else 
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "ไม่สามารถเข้าใช้งานระบบได้ กรุณาติดต่อเจ้าหน้าที่');", true);

            }
        }
        
        public List<EmpLoginInfo> GetLogin()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/GetLogin";

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