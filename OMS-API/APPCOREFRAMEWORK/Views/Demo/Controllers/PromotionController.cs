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
    public class PromotionController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdatePromotion")]
        public IHttpActionResult UpdPromotion([FromBody]PromotionInfo pInfo)
        {

            PromotionDAO pDAO = new PromotionDAO();
            int i = 0;
            i = pDAO.UpdatePromotion(pInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateLazadaPromotion")]
        public IHttpActionResult UpdateLazadaProduct([FromBody] PromotionInfo pInfo)
        {
            PromotionDAO pDAO = new PromotionDAO();
            int i = 0;
            i = pDAO.UpdateLazadaPromotion(pInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeletePromotion")]
        public IHttpActionResult DelPromotion([FromBody]PromotionInfo pInfo)
        {

            PromotionDAO pDAO = new PromotionDAO();
            int i = 0;
            i = pDAO.DeletePromotionList(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertPromotion")]
        public IHttpActionResult InsPromotion([FromBody]PromotionInfo pInfo)
        {

            PromotionDAO pDAO = new PromotionDAO();
            int i = 0;  
            i = pDAO.InsertPromotion(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPromotionNoPagingByCriteria")]
        public IHttpActionResult ListPmNoPagingByCriteria([FromBody]PromotionInfo pInfo)
        {
            PromotionListReturn promotionList = new PromotionListReturn();
            List<PromotionListReturn> listPromotion = new List<PromotionListReturn>();
            PromotionDAO pDAO = new PromotionDAO();

            listPromotion = pDAO.ListPromotionNoPagingByCriteria(pInfo);

            return Ok<List<PromotionListReturn>>(listPromotion);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPromotionList")]
        public IHttpActionResult ListPromoList([FromBody]PromotionInfo pInfo)
        {
            PromotionListReturn promotionList = new PromotionListReturn();
            List<PromotionListReturn> listPromotion = new List<PromotionListReturn>();
            PromotionDAO pDAO = new PromotionDAO();

            listPromotion = pDAO.ListPromotionList(pInfo);

            return Ok<List<PromotionListReturn>>(listPromotion);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPromotionInCampaign")]
        public IHttpActionResult ListPromotionInCampaign([FromBody]PromotionInfo pInfo)
        {
            PromotionListReturn promotionList = new PromotionListReturn();
            List<PromotionListReturn> listPromotion = new List<PromotionListReturn>();
            PromotionDAO pDAO = new PromotionDAO();

            listPromotion = pDAO.ListPromotionInCampaign(pInfo);

            return Ok<List<PromotionListReturn>>(listPromotion);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountPromotionListByCriteria")]
        public IHttpActionResult CountPromoListByCriteria([FromBody]PromotionInfo pInfo)
        {
            PromotionDAO pDAO = new PromotionDAO();
            int? i = 0;
            i = pDAO.CountPromotionListByCriteria(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountPromotionInCampaignListByCriteria")]
        public IHttpActionResult CountPromotionInCampaignListByCriteria([FromBody]PromotionInfo pInfo)
        {
            PromotionDAO pDAO = new PromotionDAO();
            int? i = 0;
            i = pDAO.CountPromotionInCampaignListByCriteria(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListPromotion")]
        public IHttpActionResult CountListPromo([FromBody]PromotionInfo pInfo)
        {
            PromotionDAO pDAO = new PromotionDAO();
            int? i = 0;
            i = pDAO.CountListPromotion(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPromotionType")]
        public IHttpActionResult ListPromoType([FromBody]PromotionTypeInfo pInfo)
        {
            PromotionTypeListReturn promotiontypeList = new PromotionTypeListReturn();
            List<PromotionTypeListReturn> listPromotionType = new List<PromotionTypeListReturn>();
            PromotionDAO pDAO = new PromotionDAO();

            listPromotionType = pDAO.ListPromotionType(pInfo);

            return Ok<List<PromotionTypeListReturn>>(listPromotionType);
        }

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("api/support/PromotionCodeValidateInsert")]
        //public IHttpActionResult PromoCodeValidateInsert([FromBody]PromotionInfo pInfo)
        //{
        //    PromotionListReturn promotionList = new PromotionListReturn();
        //    List<PromotionListReturn> listPromotion = new List<PromotionListReturn>();
        //    PromotionDAO pDAO = new PromotionDAO();

        //    listPromotion = pDAO.PromotionCodeValidateInsert(pInfo);

        //    return Ok<List<PromotionListReturn>>(listPromotion);
        //}
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPromotionListEliminate")]
        public IHttpActionResult ListPromoListelimi([FromBody]PromotionInfo pInfo)
        {
            PromotionListReturn promotionList = new PromotionListReturn();
            List<PromotionListReturn> listPromotion = new List<PromotionListReturn>();
            PromotionDAO pDAO = new PromotionDAO();

            listPromotion = pDAO.ListPromotionListEliminate(pInfo);

            return Ok<List<PromotionListReturn>>(listPromotion);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertPromotionImport")]
        public IHttpActionResult InsPromotionImport([FromBody]L_promotiondata lpromotiondata)
        {
            PromotionDAO cDAO = new PromotionDAO();
            int sum = 0;

            sum = cDAO.InsertPromotionImport(lpromotiondata.PromotionInfo.ToList());

            return Ok<int>(sum);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPromotionbyPromotionTypeNoPagingByCriteria")]
        public IHttpActionResult ListPromotionbyPromotionTypeNoPaging([FromBody]PromotionInfo pInfo)
        {
            PromotionListReturn promotionList = new PromotionListReturn();
            List<PromotionListReturn> listPromotion = new List<PromotionListReturn>();
            PromotionDAO pDAO = new PromotionDAO();

            listPromotion = pDAO.ListPromotionbyPromotionTypeNoPagingByCriteria(pInfo);

            return Ok<List<PromotionListReturn>>(listPromotion);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/PromotionCodeValidateInsert")]
        public IHttpActionResult TwentyThreexBuilxd([FromBody]PromotionInfo pInfo)
        {
            PromotionListReturn productReturn = new PromotionListReturn();
            List<PromotionListReturn> listproductReturn = new List<PromotionListReturn>();
            PromotionDAO pDAO = new PromotionDAO();
            listproductReturn = pDAO.PromotionCodeValidateInsert(pInfo);

            return Ok<List<PromotionListReturn>>(listproductReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdatePromotionfromApproverWF")]
        public IHttpActionResult UpdatePromotionfromApproverWF([FromBody] PromotionInfo pInfo)
        {

            PromotionDAO pDAO = new PromotionDAO();
            int i = 0;
            i = pDAO.UpdatePromotionfromApproverWF(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListPromotionGreenSpotWF")]
        public IHttpActionResult CountListPromotionGreenSpotWF([FromBody] PromotionInfo pInfo)
        {
            PromotionDAO pDAO = new PromotionDAO();
            int? i = 0;
            i = pDAO.CountListPromotionGreenSpotWF(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPromotionListGreenSprtWF")]
        public IHttpActionResult ListPromotionListGreenSprtWF([FromBody] PromotionInfo pInfo)
        {
            PromotionListReturn promotionList = new PromotionListReturn();
            List<PromotionListReturn> listPromotion = new List<PromotionListReturn>();
            PromotionDAO pDAO = new PromotionDAO();

            listPromotion = pDAO.ListPromotionListGreenSprtWF(pInfo);

            return Ok<List<PromotionListReturn>>(listPromotion);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertPromotionMapPromotionTag")]
        public IHttpActionResult InsertPromotionMapPromotionTag([FromBody] PromotionInfo pInfo)
        {

            PromotionDAO pDAO = new PromotionDAO();
            int i = 0;
            i = pDAO.InsertPromotionMapPromotionTag(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPromotionMapPromotionTagNoPagingByCriteria")]
        public IHttpActionResult ListPromotionMapPromotionTagNoPagingByCriteria([FromBody] PromotionInfo pInfo)
        {
            PromotionListReturn promotionList = new PromotionListReturn();
            List<PromotionListReturn> listPromotion = new List<PromotionListReturn>();
            PromotionDAO pDAO = new PromotionDAO();

            listPromotion = pDAO.ListPromotionMapPromotionTagNoPagingByCriteria(pInfo);

            return Ok<List<PromotionListReturn>>(listPromotion);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateFlagDeletePromotionMapPromotionTag")]
        public IHttpActionResult UpdateFlagDeletePromotionMapPromotionTag([FromBody] PromotionInfo pInfo)
        {
            PromotionDAO pDAO = new PromotionDAO();
            int i = 0;
            i = pDAO.UpdateFlagDeletePromotionMapPromotionTag(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ApprovePromotion")]
        public IHttpActionResult ApprovePromotion([FromBody] PromotionInfo pInfo)
        {

            PromotionDAO pDAO = new PromotionDAO();
            int i = 0;
            i = pDAO.ApprovePromotionList(pInfo);

            return Ok<int>(i);
        }

    }
}
