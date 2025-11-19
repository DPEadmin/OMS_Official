using APPCOREMODEL.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APPCOREMODEL.OMSDAO;
using APPCOREMODEL.Datas.OMSDTO;
using System.Security.Claims;
using System.Configuration;
using System.Web.Helpers;
using System.IO;


namespace APPCOREVIEW.Views.Demo.Controllers
{
    public class TemplateController : ApiController
    {
        protected static string appUrl = ConfigurationManager.AppSettings["appUrl"];

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListTemplateListPagingByCriteria")]
        public IHttpActionResult ListTemplateListPagingByCriteria([FromBody] TemplateInfo tInfo)
        {
           
            List<TemplateInfo> listTemplate = new List<TemplateInfo>();
            TemplateDAO tDAO = new TemplateDAO();

            listTemplate = tDAO.ListTemplateListPagingByCriteria(tInfo);

            return Ok<List<TemplateInfo>>(listTemplate);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountTemplateListPagingByCriteria")]
        public IHttpActionResult CountTemplateListPagingByCriteria([FromBody] TemplateInfo tInfo)
        {
            TemplateDAO tDAO = new TemplateDAO();
            int? i = 0;
            i = tDAO.CountTemplateListPagingByCriteria(tInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListTemplateParamNoPagingByCriteria")]
        public IHttpActionResult ListTemplateParamNoPagingByCriteria([FromBody] TemplateParamInfo tInfo)
        {
            List<TemplateParamInfo> listTemplate = new List<TemplateParamInfo>();
            TemplateDAO tDAO = new TemplateDAO();

            listTemplate = tDAO.ListTemplateParamNoPagingByCriteria(tInfo);

            return Ok<List<TemplateParamInfo>>(listTemplate);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDatabyTemplateParam")]
        public IHttpActionResult ListDatabyTemplateParam([FromBody] TemplateParamInfo tInfo)
        {
            List<TemplateParamInfo> listTemplate = new List<TemplateParamInfo>();
            TemplateDAO tDAO = new TemplateDAO();

            listTemplate = tDAO.ListDatabyTemplateParam(tInfo);

            return Ok<List<TemplateParamInfo>>(listTemplate);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertTemplate")]
        public IHttpActionResult InsertTemplate([FromBody] TemplateInfo cInfo)
        {
            TemplateDAO tDAO = new TemplateDAO();
            int? i = 0;
            i = tDAO.InsertTemplate(cInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertPromotionTemplate")]
        public IHttpActionResult InsertPromotionTemplate([FromBody] PromotionTemplateInfo cInfo)
        {
            TemplateDAO tDAO = new TemplateDAO();
            int? i = 0;
            i = tDAO.InsertPromotionTemplate(cInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertTemplatePlatform")]
        public IHttpActionResult InsertTemplatePlatform([FromBody] TemplatePlatformInfo cInfo)
        {
            TemplateDAO tDAO = new TemplateDAO();
            int? i = 0;
            i = tDAO.InsertTemplatePlatform(cInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertTemplateField")]
        public IHttpActionResult InsertTemplateField([FromBody] TemplateFieldInfo cInfo)
        {
            TemplateDAO tDAO = new TemplateDAO();
            int? i = 0;
            i = tDAO.InsertTemplateField(cInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListTemplatePlaformNoPagingByCriteria")]
        public IHttpActionResult ListTemplatePlaformNoPagingByCriteria([FromBody] TemplatePlatformInfo tInfo)
        {
            List<TemplatePlatformInfo> listTemplate = new List<TemplatePlatformInfo>();
            TemplateDAO tDAO = new TemplateDAO();

            listTemplate = tDAO.ListTemplatePlaformNoPagingByCriteria(tInfo);

            return Ok<List<TemplatePlatformInfo>>(listTemplate);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListTemplateFieldNoPagingByCriteria")]
        public IHttpActionResult ListTemplateFieldNoPagingByCriteria([FromBody] TemplateFieldInfo tInfo)
        {
            List<TemplateFieldInfo> listTemplate = new List<TemplateFieldInfo>();
            TemplateDAO tDAO = new TemplateDAO();

            listTemplate = tDAO.ListTemplateFieldNoPagingByCriteria(tInfo);

            return Ok<List<TemplateFieldInfo>>(listTemplate);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListTemplatePlatformNoPagingByCriteria")]
        public IHttpActionResult ListTemplatePlatformNoPagingByCriteria([FromBody] TemplatePlatformInfo tInfo)
        {
            List<TemplatePlatformInfo> listTemplate = new List<TemplatePlatformInfo>();
            TemplateDAO tDAO = new TemplateDAO();

            listTemplate = tDAO.ListTemplatePlatformNoPagingByCriteria(tInfo);

            return Ok<List<TemplatePlatformInfo>>(listTemplate);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPromotionTemplatePagingByCriteria")]
        public IHttpActionResult ListPromotionTemplatePagingByCriteria([FromBody] PromotionTemplateInfo tInfo)
        {
            List<PromotionTemplateInfo> listTemplate = new List<PromotionTemplateInfo>();
            TemplateDAO tDAO = new TemplateDAO();

            listTemplate = tDAO.ListPromotionTemplatePagingByCriteria(tInfo);

            return Ok<List<PromotionTemplateInfo>>(listTemplate);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountPromotionTemplatePagingByCriteria")]
        public IHttpActionResult CountPromotionTemplatePagingByCriteria([FromBody] PromotionTemplateInfo tInfo)
        {
            TemplateDAO tDAO = new TemplateDAO();
            int? i = 0;
            i = tDAO.CountPromotionTemplatePagingByCriteria(tInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/SaveTempPicfromjsonstring64/")]
        public IHttpActionResult TwentyfourBuild([FromBody] TemplateImageInfo pInfo)
        {
         

            String mediaPath = ConfigurationManager.AppSettings["UploadPicTemplatePath"];
            string filename = "";

            String adate = DateTime.Now.ToString("ddMMyyyyHHmmss");
            filename = "S" + adate + ".jpg";
            string filePath = System.Web.HttpContext.Current.Server.MapPath(mediaPath + "\\" + pInfo.TemplateImageName);

            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(pInfo.TemplateImageBase64);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(filePath);
            }
            return Ok();
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteTemplate")]
        public IHttpActionResult DelTemplate([FromBody] TemplateInfo cInfo)
        {

            TemplateDAO cDAO = new TemplateDAO();
            int? i = 0;
            i = cDAO.DeleteTemplate(cInfo);

            return Ok<int?>(i);
        }

    }
}
