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
    public class CampaignCategoryController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCampaignCategory")]
        public IHttpActionResult UpdCampaign([FromBody]CampaignCategoryInfo cInfo)
        {
            //CampaignCategoryReturn campaignList = new CampaignCategoryReturn();
            //List<CampaignCategoryReturn> listCampaign = new List<CampaignCategoryReturn>();
            CampaignCategoryDAO cDAO = new CampaignCategoryDAO();
            int i = 0;
            i = cDAO.UpdateCampaignCategory(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteCampaignCategory")]
        public IHttpActionResult DelCampaign([FromBody]CampaignCategoryInfo cInfo)
        {
            //CampaignCategoryReturn campaignList = new CampaignCategoryReturn();
            //List<CampaignCategoryReturn> listCampaign = new List<CampaignCategoryReturn>();
            CampaignCategoryDAO cDAO = new CampaignCategoryDAO();
            int i = 0;
            i = cDAO.DeleteCampaignCategory(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertCampaignCategory")]
        public IHttpActionResult InsCampaign([FromBody]CampaignCategoryInfo cInfo)
        {
        
            CampaignCategoryDAO cDAO = new CampaignCategoryDAO();
            int i = 0;
            i = cDAO.InsertCampaignCategory(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCampaignCategoryNoPagingByCriteria")]
        public IHttpActionResult ListCpNoPagingByCriteria([FromBody]CampaignCategoryInfo cInfo)
        {
            CampaignCategoryReturn campaignList = new CampaignCategoryReturn();
            List<CampaignCategoryReturn> listCampaign = new List<CampaignCategoryReturn>();
            CampaignCategoryDAO cDAO = new CampaignCategoryDAO();

            listCampaign = cDAO.ListCampaignCategoryNoPagingByCriteria(cInfo);

            return Ok<List<CampaignCategoryReturn>>(listCampaign);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountCampaignCategoryListByCriteria")]
        public IHttpActionResult CountCpListByCriteria([FromBody]CampaignCategoryInfo cInfo)
        {
            //CampaignCategoryReturn campaignList = new CampaignCategoryReturn();
            //List<CampaignCategoryReturn> listCampaign = new List<CampaignCategoryReturn>();
            CampaignCategoryDAO cDAO = new CampaignCategoryDAO();
            int? i = 0;
            i = cDAO.CountCampaignCategoryListByCriteria(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCampaignCategoryPagingByCriteria")]
        public IHttpActionResult CpListByCriteria([FromBody]CampaignCategoryInfo cInfo)
        {
            CampaignCategoryReturn campaignList = new CampaignCategoryReturn();
            List<CampaignCategoryReturn> listCampaign = new List<CampaignCategoryReturn>();
            CampaignCategoryDAO cDAO = new CampaignCategoryDAO();

            listCampaign = cDAO.ListCampaignCategoryPagingByCriteria(cInfo);

            return Ok<List<CampaignCategoryReturn>>(listCampaign);
        }
        //[AllowAnonymous]
        //[HttpPost]
        //[Route("api/support/InsertCampaignCategoryImport")]
        //public IHttpActionResult InsertCampImport([FromBody]L_CampaignCategorydata lcampaigndata)
        //{
        //    CampaignCategoryDAO cDAO = new CampaignCategoryDAO();
        //    int sum = 0;

        //    sum = cDAO.InsertCampaignImport(lcampaigndata.CampaignCategoryInfo.ToList());

        //    return Ok<int>(sum);
        //}
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/Savepicfromjsonstring64CampaignCategory")]
        public IHttpActionResult Savepicfromjsonstring64CampaignCategory([FromBody]CampaignCategoryImageListInfo pInfo)
        {
            CampaignCategoryImageListReturn productReturn = new CampaignCategoryImageListReturn();
            List<CampaignCategoryImageListReturn> listproductReturn = new List<CampaignCategoryImageListReturn>();
          

            String mediaPath = ConfigurationManager.AppSettings["UploadPicCampaignCategoryPath"];
            string filename = "";

            //string path = @"~\Images\";
            String adate = DateTime.Now.ToString("ddMMyyyyHHmmss");
            filename = "S" + adate + ".jpg";
            // string filePath = System.Web.HttpContext.Current.Server.MapPath(mediaPath + "\\" + filename);
            string filePath = System.Web.HttpContext.Current.Server.MapPath(mediaPath + "\\" + pInfo.CampaignCategoryImageName);

            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(pInfo.CampaignCategoryImageBase64);
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
        [Route("api/support/InsertCampaignCategoryImage")]
        public IHttpActionResult InsertCampaignCategoryImage([FromBody]CampaignCategoryImageListInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            CampaignCategoryDAO pDAO = new CampaignCategoryDAO();
            i = pDAO.InsertCampaignCategoryImage(pInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCampaignCategoryImage")]
        public IHttpActionResult TwentyfirstBuild([FromBody]CampaignCategoryImageListInfo pInfo)
        {
            CampaignCategoryImageListInfo productReturn = new CampaignCategoryImageListInfo();
            int? i = 0;
            CampaignCategoryDAO pDAO = new CampaignCategoryDAO();
            i = pDAO.UpdateCampaignCategoryImage(pInfo);

            return Ok<int?>(i);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CampaignCategoryCodeValidateInsert")]
        public IHttpActionResult TwentyThreexBuild([FromBody]CampaignCategoryInfo pInfo)
        {
            CampaignCategoryReturn productReturn = new CampaignCategoryReturn();
            List<CampaignCategoryReturn> listproductReturn = new List<CampaignCategoryReturn>();
            CampaignCategoryDAO pDAO = new CampaignCategoryDAO();
            listproductReturn = pDAO.CampaignCategoryCodeValidateInsert(pInfo);

            return Ok<List<CampaignCategoryReturn>>(listproductReturn);
        }
    }
}
