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
    public class CampaignController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCampaign")]
        public IHttpActionResult UpdCampaign([FromBody]CampaignInfo cInfo)
        {
            //CampaignListReturn campaignList = new CampaignListReturn();
            //List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            CampaignDAO cDAO = new CampaignDAO();
            int i = 0;
            i = cDAO.UpdateCampaign(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteCampaign")]
        public IHttpActionResult DelCampaign([FromBody]CampaignInfo cInfo)
        {
            //CampaignListReturn campaignList = new CampaignListReturn();
            //List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            CampaignDAO cDAO = new CampaignDAO();
            int i = 0;
            i = cDAO.DeleteCampaign(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertCampaign")]
        public IHttpActionResult InsCampaign([FromBody]CampaignInfo cInfo)
        {
            //CampaignListReturn campaignList = new CampaignListReturn();
            //List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            CampaignDAO cDAO = new CampaignDAO();
            int i = 0;
            i = cDAO.InsertCampaign(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCampaignNoPagingByCriteria")]
        public IHttpActionResult ListCpNoPagingByCriteria([FromBody]CampaignInfo cInfo)
        {
            CampaignListReturn campaignList = new CampaignListReturn();
            List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            CampaignDAO cDAO = new CampaignDAO();

            listCampaign = cDAO.ListCampaignNoPagingByCriteria(cInfo);

            return Ok<List<CampaignListReturn>>(listCampaign);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCampaignMediaNoPagingByCriteria")]
        public IHttpActionResult ListCpMediaNoPagingByCriteria([FromBody]CampaignInfo cInfo)
        {
            CampaignListReturn campaignList = new CampaignListReturn();
            List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            CampaignDAO cDAO = new CampaignDAO();

            listCampaign = cDAO.ListCampaignMediaNoPagingByCriteria(cInfo);

            return Ok<List<CampaignListReturn>>(listCampaign);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCampaignMediaNoPagingByCriteria2")]
        public IHttpActionResult ListCpMediaNoPagingByCriteria2([FromBody] CampaignInfo cInfo)
        {
            CampaignListReturn campaignList = new CampaignListReturn();
            List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            CampaignDAO cDAO = new CampaignDAO();

            listCampaign = cDAO.ListCampaignMediaNoPagingByCriteria2(cInfo);

            return Ok<List<CampaignListReturn>>(listCampaign);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountCampaignListByCriteria")]
        public IHttpActionResult CountCpListByCriteria([FromBody]CampaignInfo cInfo)
        {
            //CampaignListReturn campaignList = new CampaignListReturn();
            //List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            CampaignDAO cDAO = new CampaignDAO();
            int? i = 0;
            i = cDAO.CountCampaignListByCriteria(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertOrderCampaignMovement")]
        public IHttpActionResult InsOrderCampaignMovement([FromBody]CampaignInfo cInfo)
        {
            //CampaignListReturn campaignList = new CampaignListReturn();
            //List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            CampaignDAO cDAO = new CampaignDAO();
            int? i = 0;
            i = cDAO.InsertOrderCampaignMovement(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetTopFiveCampagin")]
        public IHttpActionResult GetTopFiveCp([FromBody]CampaignInfo cInfo)
        {
            CampaignListReturn campaignList = new CampaignListReturn();
            List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            CampaignDAO cDAO = new CampaignDAO();

            listCampaign = cDAO.GetTopFiveCampagin(cInfo);

            return Ok<List<CampaignListReturn>>(listCampaign);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CampaignCodeValidateInsert")]
        public IHttpActionResult CpCodeValidateInsert([FromBody]CampaignInfo cInfo)
        {
            CampaignListReturn campaignList = new CampaignListReturn();
            List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            CampaignDAO cDAO = new CampaignDAO();

            listCampaign = cDAO.CampaignCodeValidateInsert(cInfo);

            return Ok<List<CampaignListReturn>>(listCampaign);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCampaignPagingByCriteria")]
        public IHttpActionResult CpListByCriteria([FromBody]CampaignInfo cInfo)
        {
            CampaignListReturn campaignList = new CampaignListReturn();
            List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            CampaignDAO cDAO = new CampaignDAO();

            listCampaign = cDAO.ListCampaignPagingByCriteria(cInfo);

            return Ok<List<CampaignListReturn>>(listCampaign);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertCampaignImport")]
        public IHttpActionResult InsertCampImport([FromBody]L_campaigndata lcampaigndata)
        {
            CampaignDAO cDAO = new CampaignDAO();
            int sum = 0;

            sum = cDAO.InsertCampaignImport(lcampaigndata.CampaignInfo.ToList());

            return Ok<int>(sum);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/SaveCampaignpicfromjsonstring64/")]
        public IHttpActionResult SaveCampPicfromjsonstring64([FromBody]CampaignInfo cInfo)
        {
            CampaignListReturn productReturn = new CampaignListReturn();
            List<CampaignListReturn> listproductReturn = new List<CampaignListReturn>();
            CampaignDAO pDAO = new CampaignDAO();

            String mediaPath = ConfigurationManager.AppSettings["UploadPicCampaignPath"];
            string filename = "";

            //string path = @"~\Images\";
            String adate = DateTime.Now.ToString("ddMMyyyyHHmmss");
            filename = "S" + adate + ".jpg";
            // string filePath = System.Web.HttpContext.Current.Server.MapPath(mediaPath + "\\" + filename);
            string filePath = System.Web.HttpContext.Current.Server.MapPath(mediaPath + "\\" + cInfo.CampaignImageName);

            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(cInfo.CampaignImageBase64);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(filePath);
            }
            return Ok();
        }
    }
}
