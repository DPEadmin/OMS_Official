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
    public class SubPromotionDetailController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListSubMainPromotionDetailbyCriteria")]
        public IHttpActionResult ListsubmainpromodetailByCriteria([FromBody]SubPromotionDetailInfo cInfo)
        {
            SubPromotionDetailListReturn subpromotiondetail = new SubPromotionDetailListReturn();
            List<SubPromotionDetailListReturn> listSubPromotionDetailListReturn = new List<SubPromotionDetailListReturn>();
            SubPromotonDetailInfoDAO sDAO = new SubPromotonDetailInfoDAO();

            listSubPromotionDetailListReturn = sDAO.ListSubMainPromotionDetailNoPagingbyCriteria(cInfo);

            return Ok<List<SubPromotionDetailListReturn>>(listSubPromotionDetailListReturn);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListSubExchangePromotionDetailbyCriteria")]
        public IHttpActionResult ListsubexchangepromodetailByCriteria([FromBody]SubPromotionDetailInfo cInfo)
        {
            SubPromotionDetailListReturn subpromotiondetail = new SubPromotionDetailListReturn();
            List<SubPromotionDetailListReturn> listSubPromotionDetailListReturn = new List<SubPromotionDetailListReturn>();
            SubPromotonDetailInfoDAO sDAO = new SubPromotonDetailInfoDAO();

            listSubPromotionDetailListReturn = sDAO.ListSubExchangePromotionDetailNoPagingbyCriteria(cInfo);

            return Ok<List<SubPromotionDetailListReturn>>(listSubPromotionDetailListReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertSubMainPromotionDetail")]
        public IHttpActionResult InsSubMainPromotionDetail([FromBody]SubMainPromotionDetailInfo cInfo)
        {

            SubMainPromotonDetailIDAO cDAO = new SubMainPromotonDetailIDAO();
            int i = 0;
            i = cDAO.InsertSubMainPromotonDetail(cInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertSubExchangePromotionDetail")]
        public IHttpActionResult InsSubExchangePromotionDetail([FromBody]SubExchangePromotionDetailInfo cInfo)
        {

            SubExchangePromotionDetailDAO cDAO = new SubExchangePromotionDetailDAO();
            int i = 0;
            i = cDAO.InsertSubExchangePromotionDetail(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMainExchangProductbyCriteria")]
        public IHttpActionResult ListMainExchangProductbyCriteria([FromBody]SubMainPromotionDetailListReturn cInfo)
        {
            SubMainPromotionDetailListReturn subpromotiondetail = new SubMainPromotionDetailListReturn();
            List<SubMainPromotionDetailListReturn> listSubPromotionDetailListReturn = new List<SubMainPromotionDetailListReturn>();
            SubMainPromotonDetailIDAO sDAO = new SubMainPromotonDetailIDAO();

            listSubPromotionDetailListReturn = sDAO.ListMainExchangePromotionDetailNoPagingbyCriteria(cInfo);

            return Ok<List<SubMainPromotionDetailListReturn>>(listSubPromotionDetailListReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMainExchangProductPagingbyCriteria")]
        public IHttpActionResult ListMainExchangProductPagingbyCriteria([FromBody]SubMainPromotionDetailListReturn cInfo)
        {
            SubMainPromotionDetailListReturn subpromotiondetail = new SubMainPromotionDetailListReturn();
            List<SubMainPromotionDetailListReturn> listSubPromotionDetailListReturn = new List<SubMainPromotionDetailListReturn>();
            SubMainPromotonDetailIDAO sDAO = new SubMainPromotonDetailIDAO();

            listSubPromotionDetailListReturn = sDAO.ListMainExchangePromotionDetailbyCriteria(cInfo);

            return Ok<List<SubMainPromotionDetailListReturn>>(listSubPromotionDetailListReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountMainExchangProductbyCriteria")]
        public IHttpActionResult CountMainExchangProductbyCriteria([FromBody]SubMainPromotionDetailListReturn cInfo)
        {
            SubMainPromotionDetailListReturn subpromotiondetail = new SubMainPromotionDetailListReturn();
            List<SubMainPromotionDetailListReturn> listSubPromotionDetailListReturn = new List<SubMainPromotionDetailListReturn>();
            int? count = 0;
            SubMainPromotonDetailIDAO sDAO = new SubMainPromotonDetailIDAO();

            count = sDAO.CountMainExchangePromotionDetailbyCriteria(cInfo);

            return Ok<int?>(count);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListSubExchangeProductDetailbyCriteria")]
        public IHttpActionResult ListSubExchangeProductDetailbyCriteria([FromBody]SubExchangePromotionDetailInfoListReturn cInfo)
        {
            SubExchangePromotionDetailInfoListReturn subpromotiondetail = new SubExchangePromotionDetailInfoListReturn();
            List<SubExchangePromotionDetailInfoListReturn> listSubPromotionDetailListReturn = new List<SubExchangePromotionDetailInfoListReturn>();
            SubMainPromotonDetailIDAO sDAO = new SubMainPromotonDetailIDAO();

            listSubPromotionDetailListReturn = sDAO.ListSubExchangePromotionDetailNoPagingbyCriteria(cInfo);

            return Ok<List<SubExchangePromotionDetailInfoListReturn>>(listSubPromotionDetailListReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListSubExchangeProductDetailPagingbyCriteria")]
        public IHttpActionResult ListSubExchangeProductDetailPagingbyCriteria([FromBody]SubExchangePromotionDetailInfoListReturn cInfo)
        {
            SubExchangePromotionDetailInfoListReturn subpromotiondetail = new SubExchangePromotionDetailInfoListReturn();
            List<SubExchangePromotionDetailInfoListReturn> listSubPromotionDetailListReturn = new List<SubExchangePromotionDetailInfoListReturn>();
            SubMainPromotonDetailIDAO sDAO = new SubMainPromotonDetailIDAO();

            listSubPromotionDetailListReturn = sDAO.ListSubExchangePromotionDetailbyCriteria(cInfo);

            return Ok<List<SubExchangePromotionDetailInfoListReturn>>(listSubPromotionDetailListReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountSubExchangeProductDetailbyCriteria")]
        public IHttpActionResult CountSubExchangeProductDetailbyCriteria([FromBody]SubExchangePromotionDetailInfoListReturn cInfo)
        {
            SubExchangePromotionDetailInfoListReturn subpromotiondetail = new SubExchangePromotionDetailInfoListReturn();
            List<SubExchangePromotionDetailInfoListReturn> listSubPromotionDetailListReturn = new List<SubExchangePromotionDetailInfoListReturn>();
            int? count = 0;
            SubMainPromotonDetailIDAO sDAO = new SubMainPromotonDetailIDAO();

            count = sDAO.CountSubExchangePromotionDetailbyCriteria(cInfo);

            return Ok<int?>(count);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteMainPromotionDetail")]
        public IHttpActionResult DeleteSubMainPromotionDetail([FromBody]SubMainPromotionDetailInfo cInfo)
        {

            SubMainPromotonDetailIDAO cDAO = new SubMainPromotonDetailIDAO();
            int i = 0;
            i = cDAO.DeleteSubMainPromotonDetail(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteMainPromotionDetailByCode")]
        public IHttpActionResult DeleteSubMainPromotionDetailByCode([FromBody]SubMainPromotionDetailInfo cInfo)
        {

            SubMainPromotonDetailIDAO cDAO = new SubMainPromotonDetailIDAO();
            int i = 0;
            i = cDAO.DeleteSubMainPromotonDetailByCode(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteSubExchangePromotionDetail")]
        public IHttpActionResult DeleteSubExchangePromotionDetail([FromBody]SubExchangePromotionDetailInfo cInfo)
        {

            SubExchangePromotionDetailDAO cDAO = new SubExchangePromotionDetailDAO();
            int i = 0;
            i = cDAO.DeleteSubExchangePromotonDetail(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteSubExchangePromotionDetailByCode")]
        public IHttpActionResult DeleteSubExchangePromotionDetailByCode([FromBody]SubExchangePromotionDetailInfo cInfo)
        {

            SubExchangePromotionDetailDAO cDAO = new SubExchangePromotionDetailDAO();
            int i = 0;
            i = cDAO.DeleteSubExchangePromotonDetailByCode(cInfo);

            return Ok<int>(i);
        }

    }
}
