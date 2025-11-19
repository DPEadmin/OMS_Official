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
    public class PromotionDetailController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdatePromotionDetailInfo")]
        public IHttpActionResult UpdatePromoDetailInfo([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();
            int i = 0;
            i = pdDAO.UpdatePromotionDetailInfo(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertPromotionDetail")]
        public IHttpActionResult InsertPromoDetail([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();
            int i = 0;
            i = pdDAO.InsertPromotionDetail(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeletePromtoionDetailInfo")]
        public IHttpActionResult DeletePromoDetailInfo([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();
            int i = 0;
            i = pdDAO.DeletePromtoionDetailInfo(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPromotionDetailByCampaign")]
        public IHttpActionResult ListPromotionDetailByCampaign([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailListReturn PromotionDetailList = new PromotionDetailListReturn();
            List<PromotionDetailListReturn> listProductionDetail = new List<PromotionDetailListReturn>();
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();

            listProductionDetail = pdDAO.ListPromotionDetailByCampaign(pdInfo);

            return Ok<List<PromotionDetailListReturn>>(listProductionDetail);
        }


        
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProducttionDetailByCriteria")]
        public IHttpActionResult ListPromoDetailByCriteria([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailListReturn PromotionDetailList = new PromotionDetailListReturn();
            List<PromotionDetailListReturn> listProductionDetail = new List<PromotionDetailListReturn>();
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();

            listProductionDetail = pdDAO.ListProducttionDetailByCriteria(pdInfo);

            return Ok<List<PromotionDetailListReturn>>(listProductionDetail);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountPromotionDetailListByCriteria")]
        public IHttpActionResult CountPromoDetailListByCriteria([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();
            int? i = 0;
            i = pdDAO.CountPromotionDetailListByCriteria(pdInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetPromotiondetailDropDownList")]
        public IHttpActionResult GetPromodetailDropDownList([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailListReturn PromotionDetailList = new PromotionDetailListReturn();
            List<PromotionDetailListReturn> listProductionDetail = new List<PromotionDetailListReturn>();
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();

            listProductionDetail = pdDAO.GetPromotiondetailDropDownList(pdInfo);

            return Ok<List<PromotionDetailListReturn>>(listProductionDetail);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetPromotionCodeDistinctdetailList")]
        public IHttpActionResult GetPromoCodeDistinctdetailList([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailListReturn PromotionDetailList = new PromotionDetailListReturn();
            List<PromotionDetailListReturn> listProductionDetail = new List<PromotionDetailListReturn>();
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();

            listProductionDetail = pdDAO.GetPromotionCodeDistinctdetailList(pdInfo);

            return Ok<List<PromotionDetailListReturn>>(listProductionDetail);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdatePromotionDetailAmount")]
        public IHttpActionResult UpdatePromoDetailAmount([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();
            int i = 0;
            i = pdDAO.UpdatePromotionDetailAmount(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeletePromtoionDetailInfoByCode")]
        public IHttpActionResult DeletePromoDetailInfoByCode([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();
            int i = 0;
            i = pdDAO.DeletePromtoionDetailInfoByCode(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPromotionDetailforTakeOrder")]
        public IHttpActionResult EleventhBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ListPromotionDetailforTakeOrder(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountPromotionDetailTakeOrderByCriteria")]
        public IHttpActionResult TwelthBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.CountPromotionDetailTakeOrderByCriteria(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateComplementaryFlag")]
        public IHttpActionResult ThirteenBuild([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();
            int i = 0;
            i = pdDAO.UpdateComplementaryFlag(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdatePromotionDetailDiscount")]
        public IHttpActionResult UpdPromotionDetailDiscount([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();
            int i = 0;
            i = pdDAO.UpdatePromotionDetailDiscount(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProducttionDetailNoPagingByCriteria")]
        public IHttpActionResult ListPromoDetailNoPagingByCriteria([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailListReturn PromotionDetailList = new PromotionDetailListReturn();
            List<PromotionDetailListReturn> listProductionDetail = new List<PromotionDetailListReturn>();
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();

            listProductionDetail = pdDAO.ListProducttionDetailNoPagingByCriteria(pdInfo);

            return Ok<List<PromotionDetailListReturn>>(listProductionDetail);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdatePromotionDetailName")]
        public IHttpActionResult UpdPromotionDetailName([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();
            int i = 0;
            i = pdDAO.UpdatePromotionDetailName(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeletePromtoionDetailInfoByIdString")]
        public IHttpActionResult DelPromtoionDetailInfoByIdString([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();
            int i = 0;
            i = pdDAO.DeletePromtoionDetailInfoByIdString(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPromotionDetailRecipeinCampaignProductTakeOrderCriteria")]
        public IHttpActionResult ListProductRecipeinCampaignProductTakeOrderCriteriaNoPaging([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailListReturn PromotionDetailList = new PromotionDetailListReturn();
            List<PromotionDetailListReturn> listProductionDetail = new List<PromotionDetailListReturn>();
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();

            listProductionDetail = pdDAO.ListPromotionDetailRecipeinCampaignProductTakeOrderCriteria(pdInfo);

            return Ok<List<PromotionDetailListReturn>>(listProductionDetail);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPromotionDetailbyProductinRecipeCampaignProductTakeOrderCriteria")]
        public IHttpActionResult ListPromotionDetailbyProductinRecipeCampaignProductTakeOrder([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailListReturn PromotionDetailList = new PromotionDetailListReturn();
            List<PromotionDetailListReturn> listProductionDetail = new List<PromotionDetailListReturn>();
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();

            listProductionDetail = pdDAO.ListPromotionDetailbyProductinRecipeCampaignProductTakeOrderCriteria(pdInfo);

            return Ok<List<PromotionDetailListReturn>>(listProductionDetail);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertPromotionDetailCombo")]
        public IHttpActionResult InsPromotionDetailCombo([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();
            int i = 0;
            i = pdDAO.InsertPromotionDetailCombo(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdatePromotionDetailCombo")]
        public IHttpActionResult UpdatePromoDetailCombo([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();
            int i = 0;
            i = pdDAO.UpdatePromotionDetailCombo(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/PromotionDetailfromProductTop5")]
        public IHttpActionResult ItemPromotionDetailfromProductTop5([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailListReturn PromotionDetailList = new PromotionDetailListReturn();
            List<PromotionDetailListReturn> listProductionDetail = new List<PromotionDetailListReturn>();
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();

            listProductionDetail = pdDAO.PromotionDetailfromProductTop5(pdInfo);

            return Ok<List<PromotionDetailListReturn>>(listProductionDetail);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/PromotionDetailfromLatestOrderbyCustomer")]
        public IHttpActionResult PromoDetailfromLatestOrderbyCustomer([FromBody]PromotionDetailInfo pdInfo)
        {
            PromotionDetailListReturn PromotionDetailList = new PromotionDetailListReturn();
            List<PromotionDetailListReturn> listProductionDetail = new List<PromotionDetailListReturn>();
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();

            listProductionDetail = pdDAO.PromotionDetailfromLatestOrderbyCustomer(pdInfo);

            return Ok<List<PromotionDetailListReturn>>(listProductionDetail);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateItemTypePromotionDetailInfoByIdString")]
        public IHttpActionResult UpdateItemTypePromotionDetailInfoByIdString([FromBody] PromotionDetailInfo pdInfo)
        {
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();
            int i = 0;
            i = pdDAO.UpdateItemTypePromotionDetailInfoByIdString(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdatePromoDetailInfoByProductCode")]
        public IHttpActionResult UpdatePromoDetailInfoByProductCode([FromBody] PromotionDetailInfo pdInfo)
        {
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();
            int i = 0;
            i = pdDAO.UpdatePromoDetailInfoByProductCode(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CheckPromotionComplementaryTypebyPromotionDetailInfoID")]
        public IHttpActionResult CheckPromotionComplementaryTypebyPromotionDetailInfoID([FromBody] PromotionDetailInfo pdInfo)
        {
            PromotionDetailListReturn PromotionDetailList = new PromotionDetailListReturn();
            List<PromotionDetailListReturn> listProductionDetail = new List<PromotionDetailListReturn>();
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();

            listProductionDetail = pdDAO.CheckPromotionComplementaryTypebyPromotionDetailInfoID(pdInfo);

            return Ok<List<PromotionDetailListReturn>>(listProductionDetail);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetPromotiondetailInfoNoPagingbyCriteria")]
        public IHttpActionResult GetPromotiondetailInfoNoPagingbyCriteria([FromBody] PromotionDetailInfo pdInfo)
        {
            PromotionDetailListReturn PromotionDetailList = new PromotionDetailListReturn();
            List<PromotionDetailListReturn> listProductionDetail = new List<PromotionDetailListReturn>();
            PromotionDetailDAO pdDAO = new PromotionDetailDAO();

            listProductionDetail = pdDAO.GetPromotiondetailInfoNoPagingbyCriteria(pdInfo);

            return Ok<List<PromotionDetailListReturn>>(listProductionDetail);
        }
    }
}
