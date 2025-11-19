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
    public class POController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdatePO")]
        public IHttpActionResult UpdPO([FromBody]POInfo poInfo)
        {
            PODAO poDAO = new PODAO();
            int i = 0;
            i = poDAO.UpdatePO(poInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeletePO")]
        public IHttpActionResult DelPO([FromBody]POInfo poInfo)
        {
            PODAO poDAO = new PODAO();
            int i = 0;
            i = poDAO.DeletePO(poInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/Insertpo")]
        public IHttpActionResult Inspo([FromBody]POInfo poInfo)
        {
            PODAO poDAO = new PODAO();
            int i = 0;
            i = poDAO.Insertpo(poInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPONopagingByCriteria")]
        public IHttpActionResult ListProductOrderNopagingByCriteria([FromBody]POInfo poInfo)
        {
            POListReturn pOList = new POListReturn();
            List<POListReturn> listPO = new List<POListReturn>();
            PODAO poDAO = new PODAO();

            listPO = poDAO.ListPONopagingByCriteria(poInfo);

            return Ok<List<POListReturn>>(listPO);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPOByCriteria")]
        public IHttpActionResult ListProductOrderByCriteria([FromBody]POInfo poInfo)
        {
            POListReturn pOList = new POListReturn();
            List<POListReturn> listPO = new List<POListReturn>();
            PODAO poDAO = new PODAO();

            listPO = poDAO.ListPOByCriteria(poInfo);

            return Ok<List<POListReturn>>(listPO);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountPOSumpriceWorkListByCriteria")]
        public IHttpActionResult CountPOSumpriceWorklstByCriteria([FromBody]POInfo poInfo)
        {
            int? i = 0;
            PODAO poDAO = new PODAO();

            i = poDAO.CountPOSumpriceWorkListByCriteria(poInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPOSumPriceWorklistByCriteria")]
        public IHttpActionResult ListPOSumPriceWorklstByCriteria([FromBody]POInfo poInfo)
        {
            POListReturn pOList = new POListReturn();
            List<POListReturn> listPO = new List<POListReturn>();
            PODAO poDAO = new PODAO();

            listPO = poDAO.ListPOSumPriceWorklistByCriteria(poInfo);

            return Ok<List<POListReturn>>(listPO);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountPOSumpriceListByCriteria")]
        public IHttpActionResult CountPOSumpriceLstByCriteria([FromBody]POInfo poInfo)
        {
            int? i = 0;
            PODAO poDAO = new PODAO();

            i = poDAO.CountPOSumpriceListByCriteria(poInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPOSumPriceByCriteria")]
        public IHttpActionResult LstPOSumPriceByCriteria([FromBody]POInfo poInfo)
        {
            POListReturn pOList = new POListReturn();
            List<POListReturn> listPO = new List<POListReturn>();
            PODAO poDAO = new PODAO();

            listPO = poDAO.ListPOSumPriceByCriteria(poInfo);

            return Ok<List<POListReturn>>(listPO);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountPOListByCriteria")]
        public IHttpActionResult CountProductOrderListByCriteria([FromBody]POInfo poInfo)
        {
            int? i = 0;
            PODAO poDAO = new PODAO();

            i = poDAO.CountPOListByCriteria(poInfo);

            return Ok<int?>(i);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdatePOItem")]
        public IHttpActionResult UpdPOItem([FromBody]POItemInfo poInfo)
        {
            PODAO poDAO = new PODAO();
            int i = 0;
            i = poDAO.UpdatePOItem(poInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeletePOItem")]
        public IHttpActionResult DelPOItem([FromBody]POItemInfo poInfo)
        {
            PODAO poDAO = new PODAO();
            int i = 0;
            i = poDAO.DeletePOItem(poInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertpoItem")]
        public IHttpActionResult InspoItem([FromBody]POItemInfo poInfo)
        {
            PODAO poDAO = new PODAO();
            int i = 0;
            i = poDAO.InsertpoItem(poInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPOItemNopagingByCriteria")]
        public IHttpActionResult ListPOItNopagingByCriteria([FromBody]POItemInfo poInfo)
        {
            POItemListReturn pOList = new POItemListReturn();
            List<POItemListReturn> listPO = new List<POItemListReturn>();
            PODAO poDAO = new PODAO();

            listPO = poDAO.ListPOItemNopagingByCriteria(poInfo);

            return Ok<List<POItemListReturn>>(listPO);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPOItemByCriteria")]
        public IHttpActionResult ListPOItByCriteria([FromBody]POItemInfo poInfo)
        {
            POItemListReturn pOList = new POItemListReturn();
            List<POItemListReturn> listPO = new List<POItemListReturn>();
            PODAO poDAO = new PODAO();

            listPO = poDAO.ListPOItemByCriteria(poInfo);

            return Ok<List<POItemListReturn>>(listPO);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountPOItemListByCriteria")]
        public IHttpActionResult CountPOItListByCriteria([FromBody]POItemInfo poInfo)
        {
            int? i = 0;
            PODAO poDAO = new PODAO();

            i = poDAO.CountPOItemListByCriteria(poInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertfromImportPOItem")]
        public IHttpActionResult InsertfromImpPOItem([FromBody]POItemInfo poInfo)
        {
            int? i = 0;
            PODAO poDAO = new PODAO();

            i = poDAO.InsertfromImportPOItem(poInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPOFromInvenDetail")]
        public IHttpActionResult LstPOFromInvenDetail([FromBody]POInfo poInfo)
        {
            POItemListReturn pOList = new POItemListReturn();
            List<POListReturn> listPO = new List<POListReturn>();
            PODAO poDAO = new PODAO();

            listPO = poDAO.ListPOFromInvenDetail(poInfo);

            return Ok<List<POListReturn>>(listPO);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPOmapInventoryDetailByCriteria")]
        public IHttpActionResult ListPOinModalByCriteria([FromBody]POInfo poInfo)
        {
            POListReturn pOList = new POListReturn();
            List<POListReturn> listPO = new List<POListReturn>();
            PODAO poDAO = new PODAO();

            listPO = poDAO.ListPOmapInventoryDetailByCriteria(poInfo);

            return Ok<List<POListReturn>>(listPO);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPOMOdalmapInventoryDetailByCriteria")]
        public IHttpActionResult ListPOMOdalmapInventoryDetailByCriteria([FromBody]POInfo poInfo)
        {
            POListReturn pOList = new POListReturn();
            List<POListReturn> listPO = new List<POListReturn>();
            PODAO poDAO = new PODAO();

            listPO = poDAO.ListPOMOdalmapInventoryDetailByCriteria(poInfo);

            return Ok<List<POListReturn>>(listPO);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPOItemMapProductByCriteria")]
        public IHttpActionResult ListPOItemMapProductByCriteria([FromBody]POInfo poInfo)
        {
            POListReturn pOList = new POListReturn();
            List<POListReturn> listPO = new List<POListReturn>();
            PODAO poDAO = new PODAO();

            listPO = poDAO.ListPOItemMapProductByCriteria(poInfo);

            return Ok<List<POListReturn>>(listPO);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountPOCodeRunningNumber")]
        public IHttpActionResult CountPORunningNo([FromBody]POInfo poInfo)
        {
            POListReturn poReturn = new POListReturn();
            int? i = 0;
            PODAO pDAO = new PODAO();
            i = pDAO.CountPOCodeRunningNumber(poInfo);

            return Ok<int?>(i);
        }
    }
}
