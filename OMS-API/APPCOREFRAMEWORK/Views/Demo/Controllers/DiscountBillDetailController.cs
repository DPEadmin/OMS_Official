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
    public class DiscountBillDetailController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateDiscountBillDetailInfo")]
        public IHttpActionResult UpdatePromoDetailInfo([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();
            int i = 0;
            i = pdDAO.UpdateDiscountBillDetailInfo(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertDiscountBillDetail")]
        public IHttpActionResult InsertDiscountBillDetail([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();
            int i = 0;
            i = pdDAO.InsertDiscountBillDetail(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteDiscountBillDetailInfo")]
        public IHttpActionResult DeletePromoDetailInfo([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();
            int i = 0;
            i = pdDAO.DeleteDiscountBillDetailInfo(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDiscountBillDetailByCampaign")]
        public IHttpActionResult ListDiscountBillDetailByCampaign([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailListReturn DiscountBillDetailList = new DiscountBillDetailListReturn();
            List<DiscountBillDetailListReturn> listProductionDetail = new List<DiscountBillDetailListReturn>();
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();

           // listProductionDetail = pdDAO.ListDiscountBillDetailByCampaign(pdInfo);

            return Ok<List<DiscountBillDetailListReturn>>(listProductionDetail);
        }


        
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDiscountBillProducttionDetailByCriteria")]
        public IHttpActionResult ListDiscountBillProducttionDetailByCriteria([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailListReturn DiscountBillDetailList = new DiscountBillDetailListReturn();
            List<DiscountBillDetailListReturn> listProductionDetail = new List<DiscountBillDetailListReturn>();
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();

            listProductionDetail = pdDAO.ListProducttionDetailByCriteria(pdInfo);

            return Ok<List<DiscountBillDetailListReturn>>(listProductionDetail);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDiscountBillProducttionDetailNoPagingByCriteria")]
        public IHttpActionResult ListDiscountBillProducttionDetailNPGByCriteria([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailListReturn DiscountBillDetailList = new DiscountBillDetailListReturn();
            List<DiscountBillDetailListReturn> listProductionDetail = new List<DiscountBillDetailListReturn>();
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();

            listProductionDetail = pdDAO.ListProducttionDetailNoPagingByCriteria(pdInfo);

            return Ok<List<DiscountBillDetailListReturn>>(listProductionDetail);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountDiscountBillDetailListByCriteria")]
        public IHttpActionResult CountPromoDetailListByCriteria([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();
            int? i = 0;
            i = pdDAO.CountDiscountBillDetailListByCriteria(pdInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetDiscountBilldetailDropDownList")]
        public IHttpActionResult GetPromodetailDropDownList([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailListReturn DiscountBillDetailList = new DiscountBillDetailListReturn();
            List<DiscountBillDetailListReturn> listProductionDetail = new List<DiscountBillDetailListReturn>();
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();

            listProductionDetail = pdDAO.GetDiscountBilldetailDropDownList(pdInfo);

            return Ok<List<DiscountBillDetailListReturn>>(listProductionDetail);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetDiscountBillCodeDistinctdetailList")]
        public IHttpActionResult GetPromoCodeDistinctdetailList([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailListReturn DiscountBillDetailList = new DiscountBillDetailListReturn();
            List<DiscountBillDetailListReturn> listProductionDetail = new List<DiscountBillDetailListReturn>();
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();

            listProductionDetail = pdDAO.GetDiscountBillCodeDistinctdetailList(pdInfo);

            return Ok<List<DiscountBillDetailListReturn>>(listProductionDetail);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateDiscountBillDetailAmount")]
        public IHttpActionResult UpdatePromoDetailAmount([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();
            int i = 0;
            i = pdDAO.UpdateDiscountBillDetailAmount(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteDiscountBillDetailInfoByCode")]
        public IHttpActionResult DeletePromoDetailInfoByCode([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();
            int i = 0;
            i = pdDAO.DeleteDiscountBillDetailInfoByCode(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDiscountBillDetailforTakeOrder")]
        public IHttpActionResult EleventhBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            //listproductReturn = pDAO.ListDiscountBillDetailforTakeOrder(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountDiscountBillDetailTakeOrderByCriteria")]
        public IHttpActionResult TwelthBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            //i = pDAO.CountDiscountBillDetailTakeOrderByCriteria(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateComplementaryFlag")]
        public IHttpActionResult ThirteenBuild([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();
            int i = 0;
            i = pdDAO.UpdateComplementaryFlag(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateDiscountBillDetailDiscount")]
        public IHttpActionResult UpdDiscountBillDetailDiscount([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();
            int i = 0;
            i = pdDAO.UpdateDiscountBillDetailDiscount(pdInfo);

            return Ok<int>(i);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateDiscountBillDetailName")]
        public IHttpActionResult UpdDiscountBillDetailName([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();
            int i = 0;
            i = pdDAO.UpdateDiscountBillDetailName(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteDiscountBillDetailInfoByIdString")]
        public IHttpActionResult DelDiscountBillDetailInfoByIdString([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();
            int i = 0;
            i = pdDAO.DeleteDiscountBillDetailInfoByIdString(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDiscountBillDetailRecipeinCampaignProductTakeOrderCriteria")]
        public IHttpActionResult ListProductRecipeinCampaignProductTakeOrderCriteriaNoPaging([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailListReturn DiscountBillDetailList = new DiscountBillDetailListReturn();
            List<DiscountBillDetailListReturn> listProductionDetail = new List<DiscountBillDetailListReturn>();
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();

            listProductionDetail = pdDAO.ListDiscountBillDetailRecipeinCampaignProductTakeOrderCriteria(pdInfo);

            return Ok<List<DiscountBillDetailListReturn>>(listProductionDetail);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDiscountBillDetailbyProductinRecipeCampaignProductTakeOrderCriteria")]
        public IHttpActionResult ListDiscountBillDetailbyProductinRecipeCampaignProductTakeOrder([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailListReturn DiscountBillDetailList = new DiscountBillDetailListReturn();
            List<DiscountBillDetailListReturn> listProductionDetail = new List<DiscountBillDetailListReturn>();
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();

            listProductionDetail = pdDAO.ListDiscountBillDetailbyProductinRecipeCampaignProductTakeOrderCriteria(pdInfo);

            return Ok<List<DiscountBillDetailListReturn>>(listProductionDetail);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertDiscountBillDetailCombo")]
        public IHttpActionResult InsDiscountBillDetailCombo([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();
            int i = 0;
            i = pdDAO.InsertDiscountBillDetailCombo(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateDiscountBillDetailCombo")]
        public IHttpActionResult UpdatePromoDetailCombo([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();
            int i = 0;
            i = pdDAO.UpdateDiscountBillDetailCombo(pdInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DiscountBillDetailfromProductTop5")]
        public IHttpActionResult ItemDiscountBillDetailfromProductTop5([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailListReturn DiscountBillDetailList = new DiscountBillDetailListReturn();
            List<DiscountBillDetailListReturn> listProductionDetail = new List<DiscountBillDetailListReturn>();
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();

            listProductionDetail = pdDAO.DiscountBillDetailfromProductTop5(pdInfo);

            return Ok<List<DiscountBillDetailListReturn>>(listProductionDetail);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DiscountBillDetailfromLatestOrderbyCustomer")]
        public IHttpActionResult PromoDetailfromLatestOrderbyCustomer([FromBody]DiscountBillDetailInfo pdInfo)
        {
            DiscountBillDetailListReturn DiscountBillDetailList = new DiscountBillDetailListReturn();
            List<DiscountBillDetailListReturn> listProductionDetail = new List<DiscountBillDetailListReturn>();
            DiscountBillDetailDAO pdDAO = new DiscountBillDetailDAO();

            listProductionDetail = pdDAO.DiscountBillDetailfromLatestOrderbyCustomer(pdInfo);

            return Ok<List<DiscountBillDetailListReturn>>(listProductionDetail);
        }
    }
}
