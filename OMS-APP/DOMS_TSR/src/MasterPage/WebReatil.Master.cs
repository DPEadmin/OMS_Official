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
    public partial class WebReatil : System.Web.UI.MasterPage
    {
        protected static string AppVersion = ConfigurationManager.AppSettings["version"];

        protected static string APIUrl;
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

                List<EmpInfo> lemp = new List<EmpInfo>();

                lbVerion.Text = AppVersion;
                lblname.Text = "คุณ " + empInfo.EmpName_TH.ToString()+"    ";
                APIUrl = empInfo.ConnectionAPI;
                
            }
        }

        #region Function

        protected void LoadMenu(MenuInfo minfo)
        {
            List<MenuInfo> lmenu = new List<MenuInfo>();

            lmenu = GetMenuList(minfo);

            

            if (lmenu.Count == 0)
            {

                lmenu = new List<MenuInfo>();

                lmenu = GetMenuNull();

            }

                                 
                                  
            foreach (var item in lmenu)
            {
                if (item.ParentId == 0)
                {
                    if ((item.MenuUrl != "NULL") && (item.MenuUrl != ""))
                    {
                        litMenu.Text += "  <li><a href=\""+ item.MenuUrl + "\" class=\"nav-link\">" +
                                        "<i class=\"" + item.Style + "\"></i>" +
                                        "<span>" + item.MenuName + "" +
                                        "</span></a>";
                    }
                    else
                    {
                        litMenu.Text += "  <li class=\"nav-item dropdown\"><a href=\"#\" class=\"nav-link has-dropdown\">" +
                                        "<i class=\"" + item.Style + "\"></i>" +
                                        "<span>" + item.MenuName + "" +
                                        "</span></a>";
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
                       
                                       
                        litMenu.Text += " <ul class=\"dropdown-menu\" > ";

                        bool first = true;

                        if (submenulist.Count > 0)
                        {
                            foreach (var subitem in submenulist)
                            {


                                
                                    litMenu.Text += "    <li ><a class=\"nav-link\" href=\"" + subitem.MenuUrl + "\" >" +
                                                    ""+subitem.MenuName+" "+"</a></li>";

                                   


                            


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
        protected void btnAddPromotion_Click(object sender, EventArgs e)
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
                empInfo = null;
                Session.Remove("EmpInfo");
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
            }
            else 
            {
                empInfo = null;
                Session.Remove("EmpInfo");
                Response.Redirect("..\\..\\Merchantlogin.aspx");
            }

         
        }
        #endregion

    }
}