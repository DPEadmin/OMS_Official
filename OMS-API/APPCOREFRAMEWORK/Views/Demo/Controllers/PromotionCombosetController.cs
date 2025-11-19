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
    public class PromotionCombosetController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPromotionCombosetList")]
        public IHttpActionResult ListPromoList([FromBody]PromotionCombosetInfo pInfo)
        {
            PromotionCombosetListReturn PromotionCombosetList = new PromotionCombosetListReturn();
            List<PromotionCombosetListReturn> listPromotionComboset = new List<PromotionCombosetListReturn>();
            PromotionCombosetDAO pDAO = new PromotionCombosetDAO();

            listPromotionComboset = pDAO.ListPromotionCombosetList(pInfo);

            return Ok<List<PromotionCombosetListReturn>>(listPromotionComboset);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListPromotionComboset")]
        public IHttpActionResult CountListPromo([FromBody]PromotionCombosetInfo pInfo)
        {
            PromotionCombosetDAO pDAO = new PromotionCombosetDAO();
            int? i = 0;
            i = pDAO.CountListPromotionComboset(pInfo);

            return Ok<int?>(i);
        }
    }
}