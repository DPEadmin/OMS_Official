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

namespace DOMS_TSR.src.RoutingManagement
{
    public partial class RoutingMappingDetail : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];

        protected static string ProductImgUrl = ConfigurationManager.AppSettings["ProductImageUrl"];

        string Codelist = "";
        string Codelist1 = "";
        string EditFlag = "";
        Boolean isdelete;
        protected static int currentPageNumber;
        protected static int currentPageNumber1;
        protected static int currentPageNumber2;
        protected static int currentPageNumber3;
        protected static int currentPageNumberRI; 
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;

                currentPageNumber1 = 1;

                currentPageNumber2 = 1;

                currentPageNumber3 = 1;
                currentPageNumberRI=1;
                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    
                    hidEmpCode.Value = empInfo.EmpCode;
                    ((DropDownList)Master.FindControl("ddlMerchant")).Enabled = false;
                }
                else
                {
                    Response.Redirect("..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                VehicleSection.Visible = true;
                DriverSection.Visible = false;
                InventorySelection.Visible = false;
                BindStatusActive();
                BindDdlInventory();
                BindRoleName();
                BindddlVehicleType();
                LoadRoutingVehicle();
                LoadRoutingDriver();
                LoadRoutingDetail();
                
                LoadRoutingDetailInventory();
                LoadVehicle();
                LoadDriver();

                

            }

        }


        #region Function



        protected void LoadRoutingVehicle()
        {
            List<RoutingVehicleInfo> lRoutingInfo = new List<RoutingVehicleInfo>();


            int? totalRow = CountRoutingVehicleMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lRoutingInfo = GetRoutingVehicleMasterByCriteria();

            gvRoutingVehicle.DataSource = lRoutingInfo;

            gvRoutingVehicle.DataBind();


            

        }


        protected void LoadRoutingDriver()
        {
            List<RoutingDriverInfo> lRoutingInfo = new List<RoutingDriverInfo>();


            int? totalRow = CountRoutingDriverMasterList();

            SetPageBar1(Convert.ToDouble(totalRow));


            lRoutingInfo = GetRoutingDriverMasterByCriteria();

            gvRoutingDriver.DataSource = lRoutingInfo;

            gvRoutingDriver.DataBind();


            

        }


        protected void LoadRoutingDetail()
        {
            List<RoutingInfo> lRoutingInfo = new List<RoutingInfo>();

            lRoutingInfo = LoadRoutingDetailNoPaging();

            lblRoutingCode.Text = lRoutingInfo[0].Routing_code;

            lblRoutingName.Text = lRoutingInfo[0].Routing_name;

            

        }


        protected void LoadVehicle()
        {
            List<VechicleInfo> lVehicleInfo = new List<VechicleInfo>();


            int? totalRow = CountVehicleMasterList();

            SetPageBar2(Convert.ToDouble(totalRow));


            lVehicleInfo = GetVehicleMasterByCriteria();

            gvVehicle.DataSource = lVehicleInfo;

            gvVehicle.DataBind();


            

        }



        protected void LoadDriver()
        {
            List<DriverInfo> lDriverInfo = new List<DriverInfo>();


            int? totalRow = CountDriverMasterList();

            SetPageBar3(Convert.ToDouble(totalRow));


            lDriverInfo = GetDriverMasterByCriteria();

            gvDriver.DataSource = lDriverInfo;

            gvDriver.DataBind();


            

        }
        protected void LoadRoutingDetailInventory()
        {
            List<RoutingMapInventoryDetailInfo> lrmiInfo = new List<RoutingMapInventoryDetailInfo>();


            int? totalRowInventory = CountRMI();

              SetPageBarRI(Convert.ToDouble(totalRowInventory));

           
            lrmiInfo = LoadRoutingInventory();
            hidcoutRI.Value =lrmiInfo.Count.ToString();
          
            gvInventory.DataSource = lrmiInfo;

            gvInventory.DataBind();


            

        }

        protected Boolean DeleteRoutingVehicle()
        {

            for (int i = 0; i < gvRoutingVehicle.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvRoutingVehicle.Rows[i].FindControl("chkRoutingVehicle");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvRoutingVehicle.Rows[i].FindControl("hidRoutingVehicleId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                }
            }

            if (Codelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteRoutingVehicle";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["Routing_code"] = Codelist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            }
            else
            {
                
                return false;
            }

           
            return true;

        }


        protected Boolean DeleteRoutingDriver()
        {

            for (int i = 0; i < gvRoutingDriver.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvRoutingDriver.Rows[i].FindControl("chkRoutingDriver");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvRoutingDriver.Rows[i].FindControl("hidRoutingDriverId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                }
            }

            if (Codelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteRoutingDriver";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["Routing_code"] = Codelist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            }
            else
            {

                return false;
            }


            return true;

        }

        protected Boolean DeleteRoutingInventory()
        {

            for (int i = 0; i < gvInventory.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvInventory.Rows[i].FindControl("chkInventory");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvInventory.Rows[i].FindControl("hidRoutinginventoryId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                }
            }

            if (Codelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteRoutingInventory";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["Routing_code"] = Codelist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);
                if(cou>0)
                {
                    LoadRoutingDetailInventory();
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('ลบข้อมูลเรียบร้อยแล้ว');", true); 
                }
              
            }
            else
            {

                return false;
            }


            return true;

        }
        //GV RoutingVehicle
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadRoutingVehicle();
        }

        protected void GetPageIndex(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber = 1;
                    break;

                case "Previous":
                    currentPageNumber = Int32.Parse(ddlPage.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber = Int32.Parse(ddlPage.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber = Int32.Parse(lblTotalPages.Text);
                    break;
            }


            LoadRoutingVehicle();
        }

        private void setDDl(DropDownList ddls, String val)
        {
            ListItem li;
            for (int i = 0; i < ddls.Items.Count; i++)
            {
                li = ddls.Items[i];
                if (val.Equals(li.Value))
                {
                    ddls.SelectedIndex = i;
                    break;
                }
            }

        }

        protected void SetPageBar(double totalRow)
        {

            lblTotalPages.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages.Text) + 1; i++)
            {
                ddlPage.Items.Add(new ListItem(i.ToString()));
            }
            setDDl(ddlPage, currentPageNumber.ToString());
            

            
            if ((currentPageNumber == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst.Enabled = false;
                lnkbtnPre.Enabled = false;
                lnkbtnNext.Enabled = true;
                lnkbtnLast.Enabled = true;
            }
            else if ((currentPageNumber.ToString() == lblTotalPages.Text) && (currentPageNumber == 1))
            {
                lnkbtnFirst.Enabled = false;
                lnkbtnPre.Enabled = false;
                lnkbtnNext.Enabled = false;
                lnkbtnLast.Enabled = false;
            }
            else if ((currentPageNumber.ToString() == lblTotalPages.Text) && (currentPageNumber > 1))
            {
                lnkbtnFirst.Enabled = true;
                lnkbtnPre.Enabled = true;
                lnkbtnNext.Enabled = false;
                lnkbtnLast.Enabled = false;
            }
            else
            {
                lnkbtnFirst.Enabled = true;
                lnkbtnPre.Enabled = true;
                lnkbtnNext.Enabled = true;
                lnkbtnLast.Enabled = true;
            }
            
        }


        //GV RoutingDriver
        protected void ddlPage_SelectedIndexChanged1(object sender, EventArgs e)
        {
            currentPageNumber1 = Int32.Parse(ddlPage1.SelectedValue);

            LoadRoutingDriver();
        }

        protected void GetPageIndex1(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber1 = 1;
                    break;

                case "Previous":
                    currentPageNumber1 = Int32.Parse(ddlPage1.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber1 = Int32.Parse(ddlPage1.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber1 = Int32.Parse(lblTotalPages1.Text);
                    break;
            }


            LoadRoutingDriver();
        }

        private void setDDl1(DropDownList ddls, String val)
        {
            ListItem li;
            for (int i = 0; i < ddls.Items.Count; i++)
            {
                li = ddls.Items[i];
                if (val.Equals(li.Value))
                {
                    ddls.SelectedIndex = i;
                    break;
                }
            }

        }

        protected void SetPageBar1(double totalRow)
        {

            lblTotalPages1.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage1.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages1.Text) + 1; i++)
            {
                ddlPage1.Items.Add(new ListItem(i.ToString()));
            }
            setDDl1(ddlPage1, currentPageNumber1.ToString());
            

            
            if ((currentPageNumber1 == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst1.Enabled = false;
                lnkbtnPre1.Enabled = false;
                lnkbtnNext1.Enabled = true;
                lnkbtnLast1.Enabled = true;
            }
            else if ((currentPageNumber1.ToString() == lblTotalPages1.Text) && (currentPageNumber1 == 1))
            {
                lnkbtnFirst1.Enabled = false;
                lnkbtnPre1.Enabled = false;
                lnkbtnNext1.Enabled = false;
                lnkbtnLast1.Enabled = false;
            }
            else if ((currentPageNumber1.ToString() == lblTotalPages1.Text) && (currentPageNumber1 > 1))
            {
                lnkbtnFirst1.Enabled = true;
                lnkbtnPre1.Enabled = true;
                lnkbtnNext1.Enabled = false;
                lnkbtnLast1.Enabled = false;
            }
            else
            {
                lnkbtnFirst1.Enabled = true;
                lnkbtnPre1.Enabled = true;
                lnkbtnNext1.Enabled = true;
                lnkbtnLast1.Enabled = true;
            }
            
        }


        //GV VehicleDriver
        protected void ddlPage_SelectedIndexChanged2(object sender, EventArgs e)
        {
            currentPageNumber2 = Int32.Parse(ddlPage2.SelectedValue);

            LoadVehicle();
        }

        protected void GetPageIndex2(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber2 = 1;
                    break;

                case "Previous":
                    currentPageNumber2 = Int32.Parse(ddlPage2.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber2 = Int32.Parse(ddlPage2.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber2 = Int32.Parse(lblTotalPages2.Text);
                    break;
            }


            LoadVehicle();
        }

        private void setDDl2(DropDownList ddls, String val)
        {
            ListItem li;
            for (int i = 0; i < ddls.Items.Count; i++)
            {
                li = ddls.Items[i];
                if (val.Equals(li.Value))
                {
                    ddls.SelectedIndex = i;
                    break;
                }
            }

        }

        protected void SetPageBar2(double totalRow)
        {

            lblTotalPages2.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage2.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages2.Text) + 1; i++)
            {
                ddlPage2.Items.Add(new ListItem(i.ToString()));
            }
            setDDl2(ddlPage2, currentPageNumber2.ToString());
            

            
            if ((currentPageNumber2 == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst2.Enabled = false;
                lnkbtnPre2.Enabled = false;
                lnkbtnNext2.Enabled = true;
                lnkbtnLast2.Enabled = true;
            }
            else if ((currentPageNumber2.ToString() == lblTotalPages2.Text) && (currentPageNumber2 == 1))
            {
                lnkbtnFirst2.Enabled = false;
                lnkbtnPre2.Enabled = false;
                lnkbtnNext2.Enabled = false;
                lnkbtnLast2.Enabled = false;
            }
            else if ((currentPageNumber2.ToString() == lblTotalPages2.Text) && (currentPageNumber2 > 1))
            {
                lnkbtnFirst2.Enabled = true;
                lnkbtnPre2.Enabled = true;
                lnkbtnNext2.Enabled = false;
                lnkbtnLast2.Enabled = false;
            }
            else
            {
                lnkbtnFirst2.Enabled = true;
                lnkbtnPre2.Enabled = true;
                lnkbtnNext2.Enabled = true;
                lnkbtnLast2.Enabled = true;
            }
            
        }



        //GV VehicleDriver
        protected void ddlPage_SelectedIndexChanged3(object sender, EventArgs e)
        {
            currentPageNumber3 = Int32.Parse(ddlPage3.SelectedValue);

            LoadDriver();
        }

        protected void GetPageIndex3(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber3 = 1;
                    break;

                case "Previous":
                    currentPageNumber3 = Int32.Parse(ddlPage3.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber3 = Int32.Parse(ddlPage3.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber3 = Int32.Parse(lblTotalPages3.Text);
                    break;
            }

            LoadDriver();
        }

        private void setDDl3(DropDownList ddls, String val)
        {
            ListItem li;
            for (int i = 0; i < ddls.Items.Count; i++)
            {
                li = ddls.Items[i];
                if (val.Equals(li.Value))
                {
                    ddls.SelectedIndex = i;
                    break;
                }
            }

        }

        protected void SetPageBar3(double totalRow)
        {

            lblTotalPages3.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage3.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages3.Text) + 1; i++)
            {
                ddlPage3.Items.Add(new ListItem(i.ToString()));
            }
            setDDl3(ddlPage3, currentPageNumber3.ToString());
            

            
            if ((currentPageNumber3 == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst3.Enabled = false;
                lnkbtnPre3.Enabled = false;
                lnkbtnNext3.Enabled = true;
                lnkbtnLast3.Enabled = true;
            }
            else if ((currentPageNumber3.ToString() == lblTotalPages3.Text) && (currentPageNumber3 == 1))
            {
                lnkbtnFirst3.Enabled = false;
                lnkbtnPre3.Enabled = false;
                lnkbtnNext3.Enabled = false;
                lnkbtnLast3.Enabled = false;
            }
            else if ((currentPageNumber3.ToString() == lblTotalPages3.Text) && (currentPageNumber3 > 1))
            {
                lnkbtnFirst3.Enabled = true;
                lnkbtnPre3.Enabled = true;
                lnkbtnNext3.Enabled = false;
                lnkbtnLast3.Enabled = false;
            }
            else
            {
                lnkbtnFirst3.Enabled = true;
                lnkbtnPre3.Enabled = true;
                lnkbtnNext3.Enabled = true;
                lnkbtnLast3.Enabled = true;
            }
            
        }
        protected void ddlRIPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumberRI = Int32.Parse(ddlPage.SelectedValue);

            LoadRoutingDetailInventory();
        }

        protected void GetPageRIIndex(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumberRI = 1;
                    break;

                case "Previous":
                    currentPageNumberRI = Int32.Parse(ddlRIPage.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumberRI = Int32.Parse(ddlRIPage.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumberRI = Int32.Parse(lblTotalPagesRI.Text);
                    break;
            }


            LoadRoutingDetailInventory();
        }

        private void setDDlRI(DropDownList ddls, String val)
        {
            ListItem li;
            for (int i = 0; i < ddls.Items.Count; i++)
            {
                li = ddls.Items[i];
                if (val.Equals(li.Value))
                {
                    ddls.SelectedIndex = i;
                    break;
                }
            }

        }

        protected void SetPageBarRI(double totalRowRI)
        {

            lblTotalPagesRI.Text = Math.Ceiling(totalRowRI / PAGE_SIZE).ToString(); 

            
            ddlRIPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPagesRI.Text) + 1; i++)
            {
                ddlRIPage.Items.Add(new ListItem(i.ToString()));
            }
            setDDlRI(ddlRIPage, currentPageNumberRI.ToString());
            

            
            if ((currentPageNumberRI == 1) && (Math.Ceiling(totalRowRI / PAGE_SIZE)) > 1)
            {
                lnkbtnFirstRI.Enabled = false;
                lnkbtnPreRI.Enabled = false;
                lnkbtnNextRI.Enabled = true;
                lnkbtnLastRI.Enabled = true;
            }
            else if ((currentPageNumberRI.ToString() == lblTotalPages.Text) && (currentPageNumberRI == 1))
            {
                lnkbtnFirstRI.Enabled = false;
                lnkbtnPreRI.Enabled = false;
                lnkbtnNextRI.Enabled = false;
                lnkbtnLastRI.Enabled = false;
            }
            else if ((currentPageNumberRI.ToString() == lblTotalPagesRI.Text) && (currentPageNumberRI > 1))
            {
                lnkbtnFirstRI.Enabled = true;
                lnkbtnPreRI.Enabled = true;
                lnkbtnNextRI.Enabled = false;
                lnkbtnLastRI.Enabled = false;
            }
            else
            {
                lnkbtnFirstRI.Enabled = true;
                lnkbtnPreRI.Enabled = true;
                lnkbtnNextRI.Enabled = true;
                lnkbtnLastRI.Enabled = true;
            }
            
        }


        protected void BindStatusActive()
        {
            List<ListItem> items = new List<ListItem>();
            items.Add(new ListItem("---- Please Select ----", "-99"));
            items.Add(new ListItem("Active", "Y"));
            items.Add(new ListItem("Inactive", "N"));
            
            ddlSearchlActive.Items.AddRange(items.ToArray());
            ddltabTransport_Active.Items.AddRange(items.ToArray());
        }

    

        protected void BindddlVehicleType()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "CAR_TYPE";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddltabTransport_typeCar.DataSource = lLookupInfo;
            ddltabTransport_typeCar.DataTextField = "LookupValue";
            ddltabTransport_typeCar.DataValueField = "LookupCode";
            ddltabTransport_typeCar.DataBind();
            ddltabTransport_typeCar.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));




            ddlSearchVehicleType.DataSource = lLookupInfo;
            ddlSearchVehicleType.DataTextField = "LookupValue";
            ddlSearchVehicleType.DataValueField = "LookupCode";
            ddlSearchVehicleType.DataBind();
            ddlSearchVehicleType.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));
        }
        protected Boolean validateInsertVehicle()
        {
            Boolean flag = true;
            int countchk = 0;
            for (int i = 0; i < gvVehicle.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvVehicle.Rows[i].FindControl("chkVehicle");

                if (checkbox.Checked == true)
                {
                    countchk++;
                }
            }


            //open modal show error
            if (countchk == 0)
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "alert('กรุณาเลือกพาหนะ')", true);
            }
            else
            {
                flag = true;
            }
           

            return flag;
        }

        protected void BindRoleName()
        {
            List<RoleInfo> linvInfo = new List<RoleInfo>();
            linvInfo = GetListRole();

            ddlSearchRole.DataSource = linvInfo;
            ddlSearchRole.DataTextField = "RoleName";
            ddlSearchRole.DataValueField = "RoleCode";
            ddlSearchRole.DataBind();

            ddlSearchRole.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));
            


            ddlInsRole.DataSource = linvInfo;
            ddlInsRole.DataTextField = "RoleName";
            ddlInsRole.DataValueField = "RoleCode";
            ddlInsRole.DataBind();

            ddlInsRole.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));
           
        }

        protected Boolean validateInsertDriver()
        {
            Boolean flag = true;

            int countchk = 0;
            for (int i = 0; i < gvRoutingDriver.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvRoutingVehicle.Rows[i].FindControl("chkRoutingVehicle");

                if (checkbox.Checked == true)
                {
                    countchk++;
                }
            }


            //open modal show error
            if (countchk == 0)
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "alert('กรุณาเลือกพาหนะ')", true);
            }
            else
            {
                flag = true;
            }


            return flag;
        }


        protected void BindDdlInventory()
        {
            List<InventoryInfo> linvInfo = new List<InventoryInfo>();
            linvInfo = GetListMasterInventoryByCriteria();

            ddlInventory.DataSource = linvInfo;
            ddlInventory.DataTextField = "InventoryName";
            ddlInventory.DataValueField = "InventoryCode";
            ddlInventory.DataBind();

            ddlInventory.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));
        }
        protected List<InventoryInfo> GetListMasterInventoryByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = "";
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryInfo> linventoryInfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(respstr);
            return linventoryInfo;
        }
        protected List<RoleInfo> GetListRole()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRoleFillfillByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["RoleCode"] ="";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RoleInfo> lRoleInfo = JsonConvert.DeserializeObject<List<RoleInfo>>(respstr);
            return lRoleInfo;
        }
        public List<RoutingMapInventoryDetailInfo> LoadRoutingInventory()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRoutingInventoryByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = Request.QueryString["RoutingCode"];
                data["Inventory_Code"] = txtSeachInventoryCode_Main.Text;
                data["Inventory_name"] = txtSeachInventoryName_Main.Text;
                data["Routing_name"] = txtSeachRoutingName_Main.Text;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RoutingMapInventoryDetailInfo> lInventoryInfo = JsonConvert.DeserializeObject<List<RoutingMapInventoryDetailInfo>>(respstr);
            
            return lInventoryInfo;


        }



        #endregion

        #region DAO Function

        public List<RoutingVehicleInfo> GetRoutingVehicleMasterNoPagingByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRoutingVehicleByCriteriaNoPaging";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = Request.QueryString["RoutingCode"];

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RoutingVehicleInfo> lRoutingInfo = JsonConvert.DeserializeObject<List<RoutingVehicleInfo>>(respstr);


            return lRoutingInfo;

        }

        public List<RoutingVehicleInfo> GetRoutingVehicleMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRoutingVehicleByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = Request.QueryString["RoutingCode"];

                data["Vechicle_No"] = TxtCar_No.Text.Trim();

                data["Vechicle_Band"] = txtSearchVehicleBrand_main.Text.Trim();

                data["Vechicle_Lookup"] = ddlSearchVehicleType.SelectedValue == "-99" ? data["Vechicle_Lookup"] = "" : data["Vechicle_Lookup"] = ddlSearchVehicleType.SelectedValue;

                data["Vechicle_Active"] = ddlSearchlActive.SelectedValue == "-99" ? data["Vechicle_Active"] = "" : data["Vechicle_Active"] = ddlSearchlActive.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RoutingVehicleInfo> lRoutingInfo = JsonConvert.DeserializeObject<List<RoutingVehicleInfo>>(respstr);


            return lRoutingInfo;

        }


        public List<RoutingDriverInfo> GetRoutingDriverMasterNoPagingByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRoutingDriverByCriteriaNoPaging";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = Request.QueryString["RoutingCode"];

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RoutingDriverInfo> lRoutingInfo = JsonConvert.DeserializeObject<List<RoutingDriverInfo>>(respstr);


            return lRoutingInfo;

        }


        public List<RoutingDriverInfo> GetRoutingDriverMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRoutingDriverByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();


                data["Routing_code"] = Request.QueryString["RoutingCode"];

                data["Driver_no"] = txtSearchEmpcode.Text.Trim();

                data["FName"] = txtSearchDriverFName.Text.Trim();
                data["LName"] = txtSearchDriverLName.Text.Trim();
                data["RoleCode"] = ddlSearchRole.SelectedValue;
       
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RoutingDriverInfo> lRoutingInfo = JsonConvert.DeserializeObject<List<RoutingDriverInfo>>(respstr);


            return lRoutingInfo;

        }


        public List<VechicleInfo> GetVehicleMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRoutingVechicleInsertByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = Request.QueryString["RoutingCode"];

                data["Vechicle_No"] = txtSearchLicenseplate.Text.Trim();

                data["Vechicle_Band"] = txtSearchVehiclebrand.Text.Trim();

                data["Vechicle_Lookup"] = ddltabTransport_typeCar.SelectedValue;

                data["Vechicle_Active"] = ddltabTransport_Active.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber2 - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<VechicleInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<VechicleInfo>>(respstr);


            return lVehicleInfo;

        }


        public List<DriverInfo> GetDriverMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRoutingDriverInsertByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = Request.QueryString["RoutingCode"];

                data["Driver_No"] = txtSearchDriverCodepopup.Text.Trim();

                data["FName"] = txtSearchFnamePopup.Text.Trim();

                data["LName"] = txtSearchLnamePopup.Text.Trim();

                data["RoleCode"] = ddlInsRole.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumber3 - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<DriverInfo> lDriverInfo = JsonConvert.DeserializeObject<List<DriverInfo>>(respstr);


            return lDriverInfo;

        }



        public List<RoutingInfo> LoadRoutingDetailNoPaging()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRoutingNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = Request.QueryString["RoutingCode"];


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RoutingInfo> lRoutingInfo = JsonConvert.DeserializeObject<List<RoutingInfo>>(respstr);


            return lRoutingInfo;

        }


        public void UpdateFlagRoutingVehicle()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/UpdateRoutingVehicle";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = Request.QueryString["RoutingCode"];

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
        }


        public List<ProvinceInfo> ListProvinceNoPaging()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProvinceNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProvinceId"] = null;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProvinceInfo> lProvinceInfo = JsonConvert.DeserializeObject<List<ProvinceInfo>>(respstr);


            return lProvinceInfo;

        }


        public List<DistrictInfo> ListDistrictNoPaging(string pCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListDistrictNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProvinceCode"] = pCode;
             

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<DistrictInfo> lDistrictInfo = JsonConvert.DeserializeObject<List<DistrictInfo>>(respstr);


            return lDistrictInfo;

        }


        public List<SubDistrictInfo> ListSubDistrictNoPaging(string dCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListSubDistrictNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["DistrictCode"] = dCode;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<SubDistrictInfo> lSubDistrictInfo = JsonConvert.DeserializeObject<List<SubDistrictInfo>>(respstr);


            return lSubDistrictInfo;

        }



        public bool ValidateDuplicate()
        {
            bool isDuplicate;
            string respstr = "";

            APIpath = APIUrl + "/api/support/RoutingDetailCheck";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = Request.QueryString["RoutingCode"];

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);

            if (lProductInfo.Count > 0)
            {
                isDuplicate = true;
            }
            else
            {
                isDuplicate = false;
            }

            return isDuplicate;

        }


        public int? CountRoutingVehicleMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountRoutingVehicleListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = Request.QueryString["RoutingCode"];

                data["Vechicle_No"] = ddlFilterVehicle.SelectedValue == "1" ? txtFilterVehicle.Text.Trim() : "";

                data["Vechicle_Band"] = ddlFilterVehicle.SelectedValue == "2" ? txtFilterVehicle.Text.Trim() : "";

                data["Vechicle_Lookup"] = ddlFilterVehicle.SelectedValue == "3" ? txtFilterVehicle.Text.Trim() : "";

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }


        public int? CountRoutingDriverMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountRoutingDriverListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = Request.QueryString["RoutingCode"];
                data["Driver_no"] = txtSearchEmpcode.Text.Trim();

                data["FName"] = txtSearchDriverFName.Text.Trim();
                data["LName"] = txtSearchDriverLName.Text.Trim();
                data["RoleCode"] = ddlSearchRole.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumber1 - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }


        public int? CountDriverMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountDriverInsertByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

           

                data["Routing_code"] = Request.QueryString["RoutingCode"];

                data["Driver_No"] = txtSearchDriverCodepopup.Text.Trim();

                data["FName"] = txtSearchFnamePopup.Text.Trim();

                data["LName"] = txtSearchLnamePopup.Text.Trim();

                data["RoleCode"] = ddlInsRole.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber3 - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }


        public int? CountVehicleMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountVehicleInsertByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = Request.QueryString["RoutingCode"];

                data["Vechicle_Band"] = "";

                data["Vechicle_Lookup"] = "";

                data["rowOFFSet"] = ((currentPageNumber2 - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }

           public int? CountRMI()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountRoutingInventorytByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = Request.QueryString["RoutingCode"];

           
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }
        #endregion

        #region Event 

        protected void gvRoutingVehicle_Change(object sender, GridViewPageEventArgs e)
        {
            gvRoutingVehicle.PageIndex = e.NewPageIndex;

            List<RoutingVehicleInfo> lRoutingInfo = new List<RoutingVehicleInfo>();

            lRoutingInfo = GetRoutingVehicleMasterByCriteria();

            gvRoutingVehicle.DataSource = lRoutingInfo;

            gvRoutingVehicle.DataBind();

        }

        protected void chkRoutingVehicleAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvRoutingVehicle.Rows.Count; i++)
            {
                CheckBox chkRoutingVehicle = (CheckBox)gvRoutingVehicle.Rows[i].FindControl("chkRoutingVehicle");
                CheckBox chkall = (CheckBox)gvRoutingVehicle.HeaderRow.FindControl("chkRoutingVehicleAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvRoutingVehicle.Rows[i].FindControl("hidRoutingVehicleId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }


                    chkRoutingVehicle.Checked = true;
                }
                else
                {

                    chkRoutingVehicle.Checked = false;
                }

            }

        }


        protected void chkRoutingDriverAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvRoutingDriver.Rows.Count; i++)
            {
                CheckBox chkRoutingDriver = (CheckBox)gvRoutingDriver.Rows[i].FindControl("chkRoutingDriver");
                CheckBox chkall = (CheckBox)gvRoutingDriver.HeaderRow.FindControl("chkRoutingDriverAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvRoutingDriver.Rows[i].FindControl("hidRoutingDriverId");

                    if (Codelist1 != "")
                    {
                        Codelist1 += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist1 += "'" + hidCode.Value + "'";
                    }


                    chkRoutingDriver.Checked = true;
                }
                else
                {

                    chkRoutingDriver.Checked = false;
                }

            }
           
        }

        protected void chkInventoryAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvInventory.Rows.Count; i++)
            {
                CheckBox chkInventory = (CheckBox)gvInventory.Rows[i].FindControl("chkInventory");
                CheckBox chkall = (CheckBox)gvInventory.HeaderRow.FindControl("chkInventoryAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvInventory.Rows[i].FindControl("hidRoutinginventoryId");

                    if (Codelist1 != "")
                    {
                        Codelist1 += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist1 += "'" + hidCode.Value + "'";
                    }


                    chkInventory.Checked = true;
                }
                else
                {

                    chkInventory.Checked = false;
                }

            }

        }
        protected void chkVehicleAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvVehicle.Rows.Count; i++)
            {
                CheckBox chkVehicle = (CheckBox)gvVehicle.Rows[i].FindControl("chkVehicle");
                CheckBox chkall = (CheckBox)gvVehicle.HeaderRow.FindControl("chkVehicleAll");

                if (chkall.Checked == true)
                {
       
                    chkVehicle.Checked = true;
                }
                else
                {

                    chkVehicle.Checked = false;
                }

            }

        }


        protected void chkDriverAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvVehicle.Rows.Count; i++)
            {
                CheckBox chkDriver = (CheckBox)gvVehicle.Rows[i].FindControl("chkDriver");
                CheckBox chkall = (CheckBox)gvVehicle.HeaderRow.FindControl("chkDriverAll");

                if (chkall.Checked == true)
                {

                    chkDriver.Checked = true;
                }
                else
                {

                    chkDriver.Checked = false;
                }

            }

        }

        protected void btntab1_Click(object sender, EventArgs e)
        {
            VehicleSection.Visible = true;
            DriverSection.Visible = false;
            InventorySelection.Visible = false;
        }

        protected void btntab2_Click(object sender, EventArgs e)
        {
            VehicleSection.Visible = false;
            DriverSection.Visible = true;
            InventorySelection.Visible = false;
        }


        protected void btntab3_Click(object sender, EventArgs e)
        {
            VehicleSection.Visible = false;
            DriverSection.Visible = false;
            InventorySelection.Visible = true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadRoutingVehicle();
        }
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            TxtCar_No.Text = "";
            txtSearchVehicleBrand_main.Text = "";
            ddlSearchVehicleType.ClearSelection();
            ddlSearchlActive.ClearSelection();
        }
        protected void btnSearchDirverMain_Click(object sender, EventArgs e)
        {
            LoadRoutingDriver();
        }

        protected void btnSearchDriver_Click(object sender, EventArgs e)
        {
            LoadDriver();
        }

        protected void btnSearchVehicle_Click(object sender, EventArgs e)
        {
            LoadVehicle();
        }
        protected void btnSearchInventoryMain_Click(object sender, EventArgs e)
        {
            LoadRoutingDetailInventory();
        
        }
        

        protected void btnSearchInventoryPopup_Click(object sender, EventArgs e)
        {
           
        }
        protected void btnSaveRoutInventory_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                string respstr = "";
                if (int.Parse(hidcoutRI.Value)==0)
                {
                    if (validateUpdateinventory())
                    {
                        APIpath = APIUrl + "/api/support/InsertRoutingInventory";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["Routing_code"] = Request.QueryString["RoutingCode"];
                            data["Inventory_Code"] = ddlInventory.SelectedValue;
                            data["FlagDelete"] = "N";
                            data["CreateBy"] = empInfo.EmpCode;


                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);
                        if (sum > 0)
                        {
                            clearinserinventory();
                            LoadRoutingDetailInventory();
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-Inventory').modal('hide');", true);
                        }
                    }
                  
                }
                else
                {
                    if (validateUpdateinventory())
                    {
                    APIpath = APIUrl + "/api/support/updateRoutingInventory";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["Routing_code"] = Request.QueryString["RoutingCode"];
                        data["Inventory_Code"] = ddlInventory.SelectedValue;
                      
                        data["UpdateBy"] = empInfo.EmpCode;


                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);
                    if (sum > 0)
                    {
                        clearinserinventory();
                        LoadRoutingDetailInventory();
                        
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-Inventory').modal('hide');", true);
                    }
                    }
                }
             

               
            }
        }
        protected void btnClearSearchInventoryPopup_Click(object sender, EventArgs e)
        {
             ddlInventory.ClearSelection();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Inventory').modal('hide');", true);
        }
        
        protected void btnDeleteVehicle_Click(object sender, EventArgs e)
        {
            isdelete = DeleteRoutingVehicle();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
        }

        protected void btnDeleteDriver_Click(object sender, EventArgs e)
        {
            isdelete = DeleteRoutingDriver();
            btnSearchDirverMain_Click(null, null);
        

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
        }

        protected void btnDeleteInventory_Click(object sender, EventArgs e)
        {
            isdelete = DeleteRoutingInventory();

            

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                if (hidFlagInsert.Value == "True") //Insert
                {
                    if (validateInsertVehicle())
                    {   
                      
                        //Insert Loop
                        int? sum = 0;
                        for (int i = 0; i < gvVehicle.Rows.Count; i++)
                        {
                            CheckBox chkVehicle = (CheckBox)gvVehicle.Rows[i].FindControl("chkVehicle");
                            HiddenField hidVehicleNo = (HiddenField)gvVehicle.Rows[i].FindControl("hidVehicleNo");

                            if (chkVehicle.Checked == true)
                            {
                                string respstr = "";

                                APIpath = APIUrl + "/api/support/InsertRoutingVehicle";

                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["Routing_code"] = Request.QueryString["RoutingCode"];
                                    data["Vechicle_No"] = hidVehicleNo.Value;
                                    data["FlagDelete"] = "N";
                                    data["CreateBy"] = empInfo.EmpCode;


                                    var response = wb.UploadValues(APIpath, "POST", data);

                                    respstr = Encoding.UTF8.GetString(response);
                                }

                                sum = JsonConvert.DeserializeObject<int?>(respstr);
                            }
                        }
   

                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            LoadRoutingVehicle();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-vehicle').modal('hide');", true);



                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }

                }
                else //Update
                {
                    
                    

                }

            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {


            

        }

        protected void btnClearSearchDriverMain_Click(object sender, EventArgs e)
        {

            txtSearchDriverFName.Text = "";
            txtSearchDriverLName.Text = "";
            txtSearchEmpcode.Text = "";
            ddlSearchRole.ClearSelection();
        }

        protected void btnClearSearch1_Click(object sender, EventArgs e)
        {


        }

        protected void btnClearSearchVehicle_Click(object sender, EventArgs e)
        {
            ddltabTransport_typeCar.ClearSelection();
            txtSearchVehiclebrand.Text = "";
         
            ddltabTransport_Active.ClearSelection();
            txtSearchLicenseplate.Text = "";
        }

        protected void btnClearSearchDriver_Click(object sender, EventArgs e)
        {
            txtSearchFnamePopup.Text = "";
            txtSearchLnamePopup.Text = "";
            txtSearchDriverCodepopup.Text = "";
            ddlInsRole.ClearSelection();
        }

        protected void btnClearSearchInventoryMain_Click(object sender, EventArgs e)
        {
            txtSeachInventoryCode_Main.Text = "";
            txtSeachInventoryName_Main.Text = "";
            txtSeachRoutingName_Main.Text = "";
        }
        protected void btnAddVehicle_Click(object sender, EventArgs e)
        {
            btnClearSearchVehicle_Click(null,null);
            LoadVehicle();
            
            hidFlagInsert.Value = "True";

            List<RoutingVehicleInfo> lrv = GetRoutingVehicleMasterNoPagingByCriteria();

            for (int i = 0; i < gvVehicle.Rows.Count; i++)
            {
                CheckBox chkVehicle = (CheckBox)gvVehicle.Rows[i].FindControl("chkVehicle");
                HiddenField hidVehicleNo = (HiddenField)gvVehicle.Rows[i].FindControl("hidVehicleNo");

                foreach(RoutingVehicleInfo rv in lrv)
                {
                    if(rv.Vechicle_No == hidVehicleNo.Value)
                    {
                        chkVehicle.Checked = true;
                    }
                }
            }

             ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-vehicle').modal();", true);
        }



        protected void btnAddDriver_Click(object sender, EventArgs e)
        {
            btnClearSearchDriver_Click(null, null);
            LoadDriver();
          
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-driver').modal();", true);
        }

        protected void btnAddInventory_Click(object sender, EventArgs e)
        {
            ddlInventory.ClearSelection();
            lblddlInventory_Ins.Visible = false;
            
            LoadRoutingDetailInventory();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Inventory').modal();", true);
           
        }
        protected void gvVehicle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvVehicle.Rows[index];
            int? sum = 0;

            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidVehicleNo = (HiddenField)row.FindControl("hidVehicleNo");

            if (e.CommandName == "AddVehicle")
            {
                hidFlagInsert.Value = "True";
                string respstr = "";

                APIpath = APIUrl + "/api/support/InsertRoutingVehicle";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["Routing_code"] = Request.QueryString["RoutingCode"];
                    data["Vechicle_No"] = hidVehicleNo.Value;
                    data["FlagDelete"] = "N";
                    data["CreateBy"] = empInfo.EmpCode;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                sum = JsonConvert.DeserializeObject<int?>(respstr);
                if (sum > 0)
                {
                    LoadRoutingVehicle();
                    LoadVehicle();
                    
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');", true);
                }
            }
        }


        protected void gvDriver_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvDriver.Rows[index];
            int? sum = 0;

            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidDriverNo = (HiddenField)row.FindControl("hidDriverNo");

            if (e.CommandName == "AddDriver")
            {
                hidFlagInsert.Value = "True";
                string respstr = "";

                APIpath = APIUrl + "/api/support/InsertRoutingDriver";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["Routing_code"] = Request.QueryString["RoutingCode"];
                    data["Driver_No"] = hidDriverNo.Value;
                    data["FlagDelete"] = "N";
                    data["CreateBy"] = empInfo.EmpCode;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                sum = JsonConvert.DeserializeObject<int?>(respstr);
                if (sum > 0)
                {
                    LoadRoutingDriver();
                    LoadDriver();
                    
                  
                          ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');", true);
                }
            }
        }
        
        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"RoutingDetail.aspx?RoutingCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        protected void BindddlFilterVehicle()
        {
            ddlFilterVehicle.Items.Insert(0, new ListItem("ประเภทรถ", "3"));
            ddlFilterVehicle.Items.Insert(0, new ListItem("ยี่ห้อรถ", "2"));
            ddlFilterVehicle.Items.Insert(0, new ListItem("ทะเบียน", "1"));

        }



        protected void BindddlFilterDriver()
        {
            txtSearchEmpcode.Text = "";
            txtSearchDriverFName.Text = "";
            txtSearchDriverLName.Text = "";
        }
        protected void clearinserinventory()
        {
            ddlInventory.ClearSelection();
        }

        protected Boolean validateUpdateinventory()
        {
            Boolean flag = true;

            if (ddlInventory.SelectedValue == "-99" || ddlInventory.SelectedValue=="")
            {
                flag = false;
                lblddlInventory_Ins.Text = MessageConst._MSG_PLEASEINSERT + " คลังสินค้า";
                lblddlInventory_Ins.Visible = true;
            }
            else
            {
                    flag = (flag == false) ? false : true;
                lblddlInventory_Ins.Text = "";
                lblddlInventory_Ins.Visible = true;
            }


            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Inventory').modal();", true);
            }

            return flag;
        }
        #endregion


    }
}