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
    public class DiscountBillController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateDiscountBill")]
        public IHttpActionResult UpdDiscountBill([FromBody]DiscountBillInfo pInfo)
        {

            DiscountBillDAO pDAO = new DiscountBillDAO();
            int i = 0;
            i = pDAO.UpdateDiscountBill(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteDiscountBill")]
        public IHttpActionResult DelDiscountBill([FromBody]DiscountBillInfo pInfo)
        {

            DiscountBillDAO pDAO = new DiscountBillDAO();
            int i = 0;
            i = pDAO.DeleteDiscountBillList(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertDiscountBill")]
        public IHttpActionResult InsDiscountBill([FromBody]DiscountBillInfo pInfo)
        {

            DiscountBillDAO pDAO = new DiscountBillDAO();
            int i = 0;  
            i = pDAO.InsertDiscountBill(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDiscountBillNoPagingByCriteria")]
        public IHttpActionResult ListPmNoPagingByCriteria([FromBody]DiscountBillInfo pInfo)
        {
            DiscountBillListReturn DiscountBillList = new DiscountBillListReturn();
            List<DiscountBillListReturn> listDiscountBill = new List<DiscountBillListReturn>();
            DiscountBillDAO pDAO = new DiscountBillDAO();

            listDiscountBill = pDAO.ListDiscountBillNoPagingByCriteria(pInfo);

            return Ok<List<DiscountBillListReturn>>(listDiscountBill);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDiscountBillList")]
        public IHttpActionResult ListPromoList([FromBody]DiscountBillInfo pInfo)
        {
            DiscountBillListReturn DiscountBillList = new DiscountBillListReturn();
            List<DiscountBillListReturn> listDiscountBill = new List<DiscountBillListReturn>();
            DiscountBillDAO pDAO = new DiscountBillDAO();

            listDiscountBill = pDAO.ListDiscountBillList(pInfo);

            return Ok<List<DiscountBillListReturn>>(listDiscountBill);
        }

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("api/support/ListDiscountBillInCampaign")]
        //public IHttpActionResult ListDiscountBillInCampaign([FromBody]DiscountBillInfo pInfo)
        //{
        //    DiscountBillListReturn DiscountBillList = new DiscountBillListReturn();
        //    List<DiscountBillListReturn> listDiscountBill = new List<DiscountBillListReturn>();
        //    DiscountBillDAO pDAO = new DiscountBillDAO();

        //    listDiscountBill = pDAO.ListDiscountBillInCampaign(pInfo);

        //    return Ok<List<DiscountBillListReturn>>(listDiscountBill);
        //}

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountDiscountBillListByCriteria")]
        public IHttpActionResult CountPromoListByCriteria([FromBody]DiscountBillInfo pInfo)
        {
            DiscountBillDAO pDAO = new DiscountBillDAO();
            int? i = 0;
            i = pDAO.CountDiscountBillListByCriteria(pInfo);

            return Ok<int?>(i);
        }

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("api/support/CountDiscountBillInCampaignListByCriteria")]
        //public IHttpActionResult CountDiscountBillInCampaignListByCriteria([FromBody]DiscountBillInfo pInfo)
        //{
        //    DiscountBillDAO pDAO = new DiscountBillDAO();
        //    int? i = 0;
        //    i = pDAO.CountDiscountBillInCampaignListByCriteria(pInfo);

        //    return Ok<int?>(i);
        //}

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListDiscountBill")]
        public IHttpActionResult CountListPromo([FromBody]DiscountBillInfo pInfo)
        {
            DiscountBillDAO pDAO = new DiscountBillDAO();
            int? i = 0;
            i = pDAO.CountListDiscountBill(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDiscountBillType")]
        public IHttpActionResult ListPromoType([FromBody]DiscountBillTypeInfo pInfo)
        {
            DiscountBillTypeListReturn DiscountBilltypeList = new DiscountBillTypeListReturn();
            List<DiscountBillTypeListReturn> listDiscountBillType = new List<DiscountBillTypeListReturn>();
            DiscountBillDAO pDAO = new DiscountBillDAO();

            listDiscountBillType = pDAO.ListDiscountBillType();

            return Ok<List<DiscountBillTypeListReturn>>(listDiscountBillType);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DiscountBillCodeValidateInsert")]
        public IHttpActionResult PromoCodeValidateInsert([FromBody]DiscountBillInfo pInfo)
        {
            DiscountBillListReturn DiscountBillList = new DiscountBillListReturn();
            List<DiscountBillListReturn> listDiscountBill = new List<DiscountBillListReturn>();
            DiscountBillDAO pDAO = new DiscountBillDAO();

            listDiscountBill = pDAO.DiscountBillCodeValidateInsert(pInfo);

            return Ok<List<DiscountBillListReturn>>(listDiscountBill);
        }

    }
}
