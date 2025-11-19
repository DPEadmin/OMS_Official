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
    public partial class WebMK : System.Web.UI.MasterPage
    {
        
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                EmpInfo empInfo = new EmpInfo();

                

                List<EmpInfo> lemp = new List<EmpInfo>();

       

                EmpInfo employeeInfo = new EmpInfo();

                List<EmpRole> EmpRoleObject = getRole();

                int RoleCount = 0;

              

                foreach (var EmpRole in EmpRoleObject)
                {

                    
                }

                



                LoadMenu(setFormMenu());

                
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
     
        
        #endregion

        #region Binding
        #endregion

        #region Event
        #endregion

    }
}