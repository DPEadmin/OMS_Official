using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Net;
using System.Configuration;
using SALEORDER.DTO;
using Newtonsoft.Json;
using SALEORDER.Common;

using System.ComponentModel;
namespace DOMS_TSR.src.FullfillmentOrderManagement
{
    public partial class FFOrderManagementDetail : System.Web.UI.Page
    {
        protected static string APIUrl;
        protected static string PromotionImgUrl = ConfigurationManager.AppSettings["PromotionImageUrl"];
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
            
          
        }
 
    }
}