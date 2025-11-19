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
    public class CampaignPromotionController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountCampaignPromotionListByCriteria")]
        public IHttpActionResult CountCpPromoListByCriteria([FromBody]PromotionInfo cInfo)
        {

            CampaignPromotionDAO cDAO = new CampaignPromotionDAO();
            int? i = 0;
            i = cDAO.CountCampaignPromotionListByCriteria(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertCampaignPromotion")]
        public IHttpActionResult InsCampaignPromotion([FromBody]PromotionInfo cInfo)
        {

            CampaignPromotionDAO cDAO = new CampaignPromotionDAO();
            int i = 0;
            i = cDAO.InsertCampaignPromotion(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCampaignPromotion")]
        public IHttpActionResult UpdCampaignPromotion([FromBody]PromotionInfo cInfo)
        {

            CampaignPromotionDAO cDAO = new CampaignPromotionDAO();
            int i = 0;
            i = cDAO.UpdateCampaignPromotion(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteCampaignPromotion")]
        public IHttpActionResult DelCampaignPromotion([FromBody]PromotionInfo cInfo)
        {

            CampaignPromotionDAO cDAO = new CampaignPromotionDAO();
            int i = 0;
            i = cDAO.DeleteCampaignPromotion(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCampaignPromotionByCriteria")]
        public IHttpActionResult ListCpPromoByCriteria([FromBody]PromotionInfo cInfo)
        {
            PromotionListReturn PromotionList = new PromotionListReturn();
            List<PromotionListReturn> listPromotion = new List<PromotionListReturn>();
            CampaignPromotionDAO cDAO = new CampaignPromotionDAO();

            listPromotion = cDAO.ListCampaignPromotionByCriteria(cInfo);

            return Ok<List<PromotionListReturn>>(listPromotion);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCampaignPromotionNopagingByCriteria")]
        public IHttpActionResult ListCpPromoNopagingByCriteria([FromBody]PromotionInfo cInfo)
        {
            PromotionListReturn PromotionList = new PromotionListReturn();
            List<PromotionListReturn> listPromotion = new List<PromotionListReturn>();
            CampaignPromotionDAO cDAO = new CampaignPromotionDAO();

            listPromotion = cDAO.ListCampaignPromotionNopagingByCriteria(cInfo);

            return Ok<List<PromotionListReturn>>(listPromotion);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListWFCampaignPromotionByCriteria")]
        public IHttpActionResult ListWFCpPromoByCriteria([FromBody]PromotionInfo cInfo)
        {
            PromotionListReturn PromotionList = new PromotionListReturn();
            List<PromotionListReturn> listPromotion = new List<PromotionListReturn>();
            CampaignPromotionDAO cDAO = new CampaignPromotionDAO();

            listPromotion = cDAO.ListWFCampaignPromotionByCriteria(cInfo);

            return Ok<List<PromotionListReturn>>(listPromotion);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDeatilCampaignPromotionByCriteria")]
        public IHttpActionResult ListDeatilCpPromoByCriteria([FromBody]PromotionInfo cInfo)
        {
            PromotionListReturn PromotionList = new PromotionListReturn();
            List<PromotionListReturn> listPromotion = new List<PromotionListReturn>();
            CampaignPromotionDAO cDAO = new CampaignPromotionDAO();

            listPromotion = cDAO.ListDeatilCampaignPromotionByCriteria(cInfo);

            return Ok<List<PromotionListReturn>>(listPromotion);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountWFCampaignPromotionListByCriteria")]
        public IHttpActionResult CountWFCpPromoListByCriteria([FromBody]PromotionInfo cInfo)
        {

            CampaignPromotionDAO cDAO = new CampaignPromotionDAO();
            int? i = 0;
            i = cDAO.CountWFCampaignPromotionListByCriteria(cInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertCampProVer02")]
        public IHttpActionResult InsertCampProAPI([FromBody]PromotionInfo cInfo)
        {

            CampaignPromotionDAO cDAO = new CampaignPromotionDAO();
            int? i = 0;
            i = cDAO.InsertCampProVer02(cInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListNopagingDistinctCampaignPromotion")]
        public IHttpActionResult ListNopagingDistinctCampaignPromotionAPI([FromBody]PromotionInfo cInfo)
        {
            PromotionListReturn PromotionList = new PromotionListReturn();
            List<PromotionListReturn> listPromotion = new List<PromotionListReturn>();
            CampaignPromotionDAO cDAO = new CampaignPromotionDAO();

            listPromotion = cDAO.ListNopagingDistinctCampaignPromotion(cInfo);

            return Ok<List<PromotionListReturn>>(listPromotion);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPromotionTypeByCampaignNopagingByCriteria")]
        public IHttpActionResult ListPromotionTypeByCampaignNoPgByCriteria([FromBody]PromotionInfo cInfo)
        {
            PromotionListReturn PromotionList = new PromotionListReturn();
            List<PromotionListReturn> listPromotion = new List<PromotionListReturn>();
            CampaignPromotionDAO cDAO = new CampaignPromotionDAO();

            listPromotion = cDAO.ListPromotionTypeByCampaignNopagingByCriteria(cInfo);

            return Ok<List<PromotionListReturn>>(listPromotion);
        }
    }
}
