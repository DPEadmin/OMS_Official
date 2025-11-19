using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Net;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using SALEORDER.DTO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Configuration;
namespace DOMS_TSR.src
{
    public partial class Web : System.Web.UI.MasterPage
    {
        protected static string AppVersion = ConfigurationManager.AppSettings["version"];
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string UrlWebLogin = ConfigurationManager.AppSettings["UrlWebLogin"];
        
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lbVerion.Text = AppVersion;
                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantinfo = new MerchantInfo();
                merchantinfo = (MerchantInfo)Session["MerchantInfo"];
                empInfo = (EmpInfo)Session["EmpInfo"];
                lblname.Text = "คุณ " + empInfo.EmpName_TH.ToString() + "    ";
                
                Bind_ddlMerchant(empInfo.EmpCode);
                ddlMerchant.SelectedValue = merchantinfo.MerchantCode;

                if (empInfo.EmpExpire <= 15)
                {
                    lblEmpExpire.Text = "เหลือเวลาใช้งานอีก "+ empInfo.EmpExpire.ToString() + " วัน";
                }
                



                LoadMenu(setFormMenu());

                
            }
        }

        #region Function
        protected void Bind_ddlMerchant(string Username)
        {
            if (!IsPostBack)
            {
                string respstr = "";
                APIpath = APIUrl + "/api/support/ListMerchantEnterprise";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["Username"] = Username;

                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                List<MerchantInfo> cMerchantInfo = JsonConvert.DeserializeObject<List<MerchantInfo>>(respstr);

                ddlMerchant.DataSource = cMerchantInfo;
                ddlMerchant.DataTextField = "MerchantName";
                ddlMerchant.DataValueField = "MerchantCode";
                ddlMerchant.DataBind();
               
            }
        }
        protected void ddlMerchant_SelectedIndexChanged(object sender, EventArgs e)
        {
            MerchantInfo merchant = new MerchantInfo();
            merchant.MerchantCode = ddlMerchant.SelectedValue;
            merchant.MerchantName = ddlMerchant.Text;
            Session["MerchantInfo"] = merchant;
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
 protected void LoadMenu(MenuInfo minfo)
{
    List<MenuInfo> lmenu = GetMenuList(minfo);
    string currentPageUrl = HttpContext.Current.Request.Url.AbsolutePath;
    
    if (lmenu.Count == 0)
    {
        lmenu = new List<MenuInfo>();
        lmenu = null;
    }
    else
    {
        foreach (var item in lmenu)
        {
            if (item.ParentId == 0)
            {
                bool hasSubMenu = lmenu.Any(subitem => subitem.ParentId == item.Id);
                bool isActive = item.MenuUrl == currentPageUrl || (hasSubMenu && lmenu.Any(subitem => subitem.MenuUrl == currentPageUrl && subitem.ParentId == item.Id));

                if ((item.MenuUrl != "NULL") && (item.MenuUrl != ""))
                {
                    if (isActive)
                    {
                        litMenu.Text += "  <li class=\"active\"><a href=\"" + item.MenuUrl + "\" class=\"nav-link dogerblue\">" +
                                        "<i class=\"" + item.Style + "\"></i>" +
                                        "<span>" + item.MenuName + "</span></a>";
                    }
                    else
                    {
                        litMenu.Text += "  <li><a href=\"" + item.MenuUrl + "\" class=\"nav-link\">" +
                                        "<i class=\"" + item.Style + "\"></i>" +
                                        "<span>" + item.MenuName + "</span></a>";
                    }
                }
                else
                {
                    if (isActive)
                    {
                        litMenu.Text += "  <li class=\"active nav-item dropdown\"><a href=\"#\" class=\"nav-link has-dropdown\">" +
                                        "<i class=\"" + item.Style + "\"></i>" +
                                        "<span>" + item.MenuName + "</span></a>";
                    }
                    else
                    {
                        litMenu.Text += "  <li class=\"nav-item dropdown\"><a href=\"#\" class=\"nav-link has-dropdown\">" +
                                        "<i class=\"" + item.Style + "\"></i>" +
                                        "<span>" + item.MenuName + "</span></a>";
                    }
                }
                
                if ((item.MenuUrl == null) || (item.MenuUrl == ""))
                {
                    int? parentid = item.Id;

                    List<MenuInfo> submenulist = (from MenuInfo dr in lmenu.Where(x => x.ParentId == parentid)
                                                  select new MenuInfo()
                                                  {
                                                      Id = dr.Id,
                                                      MenuName = dr.MenuName,
                                                      MenuUrl = dr.MenuUrl,
                                                      ParentId = dr.ParentId
                                                  }).ToList();

                    string submenuClass = isActive ? "dropdown-menu active" : "dropdown-menu";
                    litMenu.Text += " <ul class=\"" + submenuClass + "\" > ";

                    bool first = true;

                    if (submenulist.Count > 0)
                    {
                        foreach (var subitem in submenulist)
                        {
                            if (subitem.MenuUrl == currentPageUrl)
                            {
                                litMenu.Text += "    <li class=\"active\"><a class=\"nav-link dogerblue\" href=\"" + subitem.MenuUrl + "\">" +
                                                "" + subitem.MenuName + "</a></li>";
                            }
                            else
                            {
                                litMenu.Text += "    <li><a class=\"nav-link\" href=\"" + subitem.MenuUrl + "\">" +
                                                "" + subitem.MenuName + "</a></li>";
                            }
                        }
                    }

                    litMenu.Text += "     </ul>  </li>   ";

                }
                else
                {
                    litMenu.Text += "</li>   ";
                }
            }
        }
    }
}





        protected MenuInfo setFormMenu()
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            MenuInfo minfo = new MenuInfo();

            if (empInfo != null)
            {
                minfo.EmpCode = empInfo.EmpCode;
            }

            return minfo;
        }

        protected List<MenuInfo> GetMenuNull()
        {

            string respstr = "";

            APIpath = APIUrl + "/api/support/ListMenuNull";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["EmpCode"] = "";
               
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<MenuInfo> MenuInfo_list = JsonConvert.DeserializeObject<List<MenuInfo>>(respstr);


            return MenuInfo_list;

        }

        protected List<MenuInfo> GetMenuList(MenuInfo minfo)
        {
       
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListMenuByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["EmpCode"] = "";
                data["EmpCode"] = minfo.EmpCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<MenuInfo> MenuInfo_list = JsonConvert.DeserializeObject<List<MenuInfo>>(respstr);


            return MenuInfo_list;

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

        protected List<EmpBranchInfo> getEmpBranch()
        {

            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpBranchNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                
                data["EmpCode"] = empInfo.EmpCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpBranchInfo> empBranch_list = JsonConvert.DeserializeObject<List<EmpBranchInfo>>(respstr);


            return empBranch_list;
        }


        #endregion

        #region Binding
        #endregion

        #region Event
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("L_orderdata1");
            Session.Remove("L_orderdata2");
            Session.Remove("L_orderdata3");

            Session.Remove("L_transportdata1");
            Session.Remove("L_transportdata2");
            Session.Remove("L_transportdata3");

            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];
            if (empInfo.RoleCode == "ADMIN")
            {
                try {
                    empInfo = null;
                    Session.Remove("EmpInfo");
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                catch(Exception) 
                {
                    Response.Redirect(UrlWebLogin+"\\Default.aspx?flaglogin=_EMPCODENULL");
                }
              

            }
            else
            {
                try
                {
                    empInfo = null;
                    Session.Remove("EmpInfo");
                    Response.Redirect("..\\..\\Merchantlogin.aspx");
                }
                catch (Exception)
                {
                    Response.Redirect(UrlWebLogin + "\\Merchantlogin.aspx");
                }
            
            }


        }
        #endregion

    }
}