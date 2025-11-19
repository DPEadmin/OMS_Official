using Microsoft.Reporting.Map.WebForms;
using Newtonsoft.Json;
using SALEORDER.DTO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Text;
using System.Web.UI;

namespace DOMS_TSR.src.UserControl
{
    public partial class Drawpolygon : System.Web.UI.UserControl
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // ไม่จำเป็นต้องมีโค้ดในนี้ เนื่องจากจะถูกเรียกเมื่อหน้าโหลด
          
        }

        public void LoadPolygon(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "initMap", "initMap();", true);
        }
        // ในส่วนของ C#
        public void SetLatLong(string lat, string lng , string idinven ,string PolyLine)
        {
            // ทำอะไรบางอย่างด้วย lat และ lng ที่ส่งเข้ามา
            string script = $"UpdateLabels('{lat}', '{lng}','{idinven}','{PolyLine}');";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "UpdateLabels", script, true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SetLatLng", $"var strlat = '{lat}'; var strLong = '{lng}'; initMap();", true);
        }
        public static void UpdateCoordinates(string coordinates)
        {
            // ดำเนินการกับข้อมูล coordinates ที่ส่งมาจาก JavaScript
            // ตัวอย่างเช่น: บันทึกลงฐานข้อมูล, ประมวลผล, เป็นต้น

            // อ่านค่าจาก hidden field
            string coordinatesValue = coordinates;

            // ทำตามที่ต้องการ
        }
        protected void btnPopupMapSave_Click(object sender, EventArgs e)
        {
            string respstr = "";
            int? sum;
            APIpath = APIUrl + "/api/support/UpdateInventoryPolygon";
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = hidInvenIDPopupMap.Value;
                data["Polygon"] = coordinatesHiddenField.Value;
                data["UpdateBy"] = empInfo.EmpCode;


                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);

                sum = JsonConvert.DeserializeObject<int?>(respstr);

                if (sum > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-show-Map').modal();", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-show-Map').modal();", true);
                }
            }

        }
        protected void btnMapReset_Click(object sender, EventArgs e)
        {
            coordinatesHiddenField.Value = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-show-Map').modal();", true);
        }







    }

}
