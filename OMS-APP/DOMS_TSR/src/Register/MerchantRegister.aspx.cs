using SALEORDER.Common;
using SALEORDER.DTO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Drawing;
using System.Security.AccessControl;
using System.Drawing.Imaging;

namespace DOMS_TSR.src.Register
{
    public partial class MerchantRegister : System.Web.UI.Page
    {
        protected static string Account_Name = ConfigurationManager.AppSettings["Account_Name"];
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string PictureLocation ="";
        string APIpath = "";
        string fileName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    string empcode = empInfo.EmpCode;
                }

            }

        }

        #region Events
        public void btnNextToStore_Click(object sender, EventArgs e)
        {
                /*if (validateStep1() == true)
                

                    
                }*/

            createStep1.Visible = false;
            createStep2.Visible = true;
        }
        public void btnBackToSeller_Click(object sender, EventArgs e)
        {
            createStep1.Visible = true;
            createStep2.Visible = false;
            createStep3.Visible = false;
        }
        protected void btnNextToBank_Click(object sender, EventArgs e)
        {
            HttpPostedFile postedFile = storeImageUpload.PostedFile;

            if (postedFile != null && postedFile.ContentLength > 0)
            {
                 fileName = Path.GetFileName(postedFile.FileName);
                string targetFolder = "ECOM002";
                string targetLocation = Path.Combine(Server.MapPath("~/Uploadfile/StoreImage/" + targetFolder), fileName);
           //     PictureLocation = targetLocation;
                if (!Directory.Exists(Server.MapPath("~/Uploadfile/StoreImage/" + targetFolder)))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Uploadfile/StoreImage/" + targetFolder));
                }

                string[] validFileTypes = { ".jpg", ".jpeg", ".png", ".gif" };
                postedFile.SaveAs(targetLocation);
                Session["targetLocation"] = targetLocation;
                string ext = Path.GetExtension(fileName);

                if (!validFileTypes.Contains(ext.ToLower()))
                {
                    // Handle invalid file type
                    return;
                }

                
            }


            createStep1.Visible = false;
            createStep2.Visible = false;
            createStep3.Visible = true;
        }
        public void btnBackToStore_Click(object sender, EventArgs e)
        {
            createStep1.Visible = false;
            createStep2.Visible = true;
            createStep3.Visible = false;
        }
        public void btnNextToSubmit_Click(object sender, EventArgs e)
        {
            InsertEmployee();
            InsertUserLogin();
            InsertMerchant();
            InsertMerchantImage();
            InsertBankInfo();
            InsertMerchantMapping();
            createStep3.Visible = false;
            createStepSubmit.Visible = true;

        }
        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/src/Main.aspx");
        }
        protected void storePhoneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox storePhoneCheckBox = (CheckBox)sender;

            if (storePhoneCheckBox.Checked)
            {
                txtStorePhone.Enabled = false;
                txtStorePhone.Text = txtMobilePhone.Text; 
            }
            else
            {
                txtStorePhone.Enabled = true;
                txtStorePhone.Text = "" ;
            }
        }
        #endregion


        #region Function

        protected void InsertEmployee()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/InsertEmployee";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = "ECOM002";
                data["EmpFname_TH"] = txtFirstName.Text;
                data["EmpLname_TH"] = txtLastName.Text;
                data["CreateBy"] = "E-Commerce";
                data["UpdateBy"] = "E-Commerce";
                data["Mail"] = txtEMail.Text;
                data["Mobile"] = "N";
                data["ActiveFlag"] = StaticField.ActiveFlag_Y;



                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
        }
        protected void InsertUserLogin()
        {
            

            string respstr = "";
            
            APIpath = APIUrl + "/api/support/InsertUserLogin";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Username"] = txtUserName.Text;
                data["Password"] = convertToSha256(txtPassword.Text);
                data["EmpCode"] = "ECOM002";
                data["CreateBy"] = "E-Commerce";
                data["UpdateBy"] = "E-Commerce";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
        }
        protected void InsertMerchant()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/InsertMerchant";

            string rbBusinessGroup = Request.Form["businessGroup"];

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantCode"] = "ECOM002";
                data["MerchantName"] = txtSellerName.Text;
                data["MerchantType"] = rbBusinessGroup;
                data["TaxId"] = txtStoreTaxCode.Text;
                data["Address"] = txtStoreAddress.InnerText;
                data["ContactTel"] = txtStorePhone.Text;
                data["Email"] = txtEMail.Text;
                data["FlagDelete"] = "N";
                data["ActiveFlag"] = StaticField.ActiveFlag_Y;
                data["CreateBy"] = "E-commerce";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
        }
        public void InsertMerchantImage()
        {
            string targetLocation = "";

            targetLocation = HttpContext.Current.Session["targetLocation"].ToString();
            string respstr = "";

            APIpath = APIUrl + "/api/support/InsertMerchantImage";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantCode"] = "ECOM002";
                data["MerchantImageUrl"] = targetLocation;
                data["FlagDelete"] = "N";
                data["Active"] = StaticField.ActiveFlag_Y;
                data["CreateBy"] = "E-commerce";
                data["UpdateBy"] = "E-commerce";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
        }
        protected void InsertMerchantMapping()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/InsertMermap";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Username"] = txtUserName.Text;
                data["MerchantCode"] = "ECOM002";
                data["CreateBy"] = "E-commerce";
                data["UpdateBy"] = "E-commerce";
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
        }
        protected void InsertBankInfo()
        {

            string respstr = "";

            APIpath = APIUrl + "/api/support/InsertBankInfo";

            string rbBankTypeGroup = Request.Form["bankTypeGroup"];
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();


                data["BankName"] = txtBankName.Text;
                data["BankAccountNumber"] = txtBankAccNum.Text;
                data["BankAccountType"] = rbBankTypeGroup;
                data["AccountName"] = txtAccName.Text;
                data["BranchName"] = txtBankAccBranch.Text;
                data["MerchantCode"] = "ECOM002";
                data["FlagDelete"] = "N";
                data["ActiveFlag"] = StaticField.ActiveFlag_Y;
                data["CreateBy"] = "E-commerce";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

        }
        protected Boolean validateStep1()
        {
            Boolean flag = false;

            if ((txtFirstName.Text == null) || (txtFirstName.Text == ""))
            {
                flag = false;
                lblFirstName.Text = "Please enter your First Name";
                txtFirstName.Focus();
            }
            else
            {
                flag = true;
                lblFirstName.Text = "";
            }
            if ((txtLastName.Text == null) || (txtLastName.Text == ""))
            {
                flag = false;
                lblLastName.Text = "Please enter your Last Name";
                txtLastName.Focus();
            }
            else
            {
                flag = true;
                lblLastName.Text = "";
            }
            if ((txtCitizenId.Text == null) || (txtCitizenId.Text == ""))
            {
                flag = false;
                lblCitizenId.Text = "Please enter your Citizen ID";
                txtCitizenId.Focus();
            }
            else
            {
                flag = true;
                lblCitizenId.Text = "";
            }
            if ((txtEMail.Text == null) || (txtEMail.Text == ""))
            {
                flag = false;
                lblEMail.Text = "Please enter your Email";
                txtEMail.Focus();
            }
            else
            {
                flag = true;
                lblEMail.Text = "";
            }
            if ((txtMobilePhone.Text == null) || (txtMobilePhone.Text == ""))
            {
                flag = false;
                lblMobilePhone.Text = "Please enter your Mobile Phone number";
                txtMobilePhone.Focus();
            }
            else
            {
                flag = true;
                lblMobilePhone.Text = "";
            }
            if ((txtUserName.Text == null) || (txtUserName.Text == ""))
            {
                flag = false;
                lblUserName.Text = "Please enter your Username";
                txtUserName.Focus();
            }
            else
            {
                flag = true;
                lblUserName.Text = "";
            }
            if ((txtPassword.Text == null) || (txtPassword.Text == ""))
            {
                flag = false;
                lblPassword.Text = "Please enter your Password";
                txtPassword.Focus();
            }
            else
            {
                flag = true;
                lblPassword.Text = "";
            }
            if ((txtConfirmPass.Text == null) || (txtConfirmPass.Text == "") || (txtConfirmPass.Text != txtPassword.Text))
            {
                flag = false;
                lblConfirmPass.Text = "Password must be the same";
                txtConfirmPass.Focus();
            }
            else
            {
                flag = true;
                lblConfirmPass.Text = "";
            }

            return flag;

        }
        protected string convertToSha256(string inputPw)
        {
            
            byte[] passwordBytes = Encoding.UTF8.GetBytes(inputPw);

            SHA256 sha256 = SHA256.Create();
            byte[] pwHashBytes = sha256.ComputeHash(passwordBytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in pwHashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        #endregion

        
    }
}