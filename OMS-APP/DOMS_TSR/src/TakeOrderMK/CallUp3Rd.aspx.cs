using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using SALEORDER.DTO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Configuration;
namespace DOMS_TSR.src.TakeOrderMK
{
    public partial class CallUp3Rd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();
            string a;
            if (!Page.IsPostBack)
            {
                empInfo = (EmpInfo)Session["EmpInfo"];
                a= empInfo.EmpCode;
            }
            
        }
    }
}