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
    public class PromotionImageController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetPromotionImageUrl")]
        public IHttpActionResult GetPromoImageUrl([FromBody]PromotionImageInfo pInfo)
        {
            PromotionListReturn PromotionReturn = new PromotionListReturn();
            List<PromotionImageListReturn> listPromotionReturn = new List<PromotionImageListReturn>();
            PromotionImageDAO pDAO = new PromotionImageDAO();
            listPromotionReturn = pDAO.GetPromotionImageUrl(pInfo);

            return Ok<List<PromotionImageListReturn>>(listPromotionReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPromotionImageValidate")]
        public IHttpActionResult ListPromoImageValidate([FromBody]PromotionImageInfo pInfo)
        {
            PromotionImageListReturn PromotionReturn = new PromotionImageListReturn();
            List<PromotionImageListReturn> listPromotionReturn = new List<PromotionImageListReturn>();
            PromotionImageDAO pDAO = new PromotionImageDAO();
            listPromotionReturn = pDAO.ListPromotionImageValidate(pInfo);

            return Ok<List<PromotionImageListReturn>>(listPromotionReturn);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertPromotionImage")]
        public IHttpActionResult InsertPromoImage([FromBody]PromotionImageInfo pInfo)
        {
       
            int? i = 0;
            PromotionImageDAO pDAO = new PromotionImageDAO();
            i = pDAO.InsertPromotionImage(pInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdatePromotionImage")]
        public IHttpActionResult UpdatePromoImage([FromBody]PromotionImageInfo pInfo)
        {
      
            int? i = 0;
            PromotionImageDAO pDAO = new PromotionImageDAO();
            i = pDAO.UpdatePromotionImage(pInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/SavePromoPicfromjsonstring64/")]
        public IHttpActionResult TwentyfourBuild([FromBody]PromotionImageInfo pInfo)
        {
            PromotionListReturn promoReturn = new PromotionListReturn();
            List<PromotionListReturn> listpromoReturn = new List<PromotionListReturn>();
            ProductDAO pDAO = new ProductDAO();

            String mediaPath = ConfigurationManager.AppSettings["UploadPicPromotionPath"];
            string filename = "";

            //string path = @"~\Images\";
            String adate = DateTime.Now.ToString("ddMMyyyyHHmmss");
            filename = "S" + adate + ".jpg";
            // string filePath = System.Web.HttpContext.Current.Server.MapPath(mediaPath + "\\" + filename);
            string filePath = System.Web.HttpContext.Current.Server.MapPath(mediaPath + "\\" + pInfo.PromotionImageName);

            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(pInfo.PromotionImageBase64);
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
