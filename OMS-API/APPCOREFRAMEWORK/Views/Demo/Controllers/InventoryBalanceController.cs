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
using System.Data;

namespace APPCOREVIEW.Views.Demo.Controllers
{
    public class InventoryBalanceController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventoryBalanceNopagingByCriteria")]
        public IHttpActionResult ListInvenBalanceNopagingByCriteria([FromBody]InventoryBalanceInfo ibInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<InventoryBalanceListReturn> listOrder = new List<InventoryBalanceListReturn>();
            InventoryBalanceDAO oDAO = new InventoryBalanceDAO();

            listOrder = oDAO.ListInventoryBalanceNopagingByCriteria(ibInfo);

            return Ok<List<InventoryBalanceListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventoryBalanceByCriteria")]
        public IHttpActionResult ListInvenBalanceByCriteria([FromBody]InventoryBalanceInfo ibInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<InventoryBalanceListReturn> listOrder = new List<InventoryBalanceListReturn>();
            InventoryBalanceDAO oDAO = new InventoryBalanceDAO();

            listOrder = oDAO.ListInventoryBalanceByCriteria(ibInfo);

            return Ok<List<InventoryBalanceListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountInventoryBalanceByCriteria")]
        public IHttpActionResult CountInvenBalanceByCriteria([FromBody]InventoryBalanceInfo ibInfo)
        {
           // OrderListReturn orderList = new OrderListReturn();
          //  List<InventoryBalanceListReturn> listOrder = new List<InventoryBalanceListReturn>();
            int? i = 0;
            InventoryBalanceDAO oDAO = new InventoryBalanceDAO();

            i = oDAO.CountInventoryBalanceByCriteria(ibInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertInventoryBalance")]
        public IHttpActionResult InsertInvenBalance([FromBody]InventoryBalanceInfo ibInfo)
        {
            // OrderListReturn orderList = new OrderListReturn();
            //  List<InventoryBalanceListReturn> listOrder = new List<InventoryBalanceListReturn>();
            int? i = 0;
            InventoryBalanceDAO oDAO = new InventoryBalanceDAO();

            i = oDAO.InsertInventoryBalance(ibInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteInventoryBalance")]
        public IHttpActionResult DeleteInvenBalance([FromBody]InventoryBalanceInfo ibInfo)
        {
            // OrderListReturn orderList = new OrderListReturn();
            //  List<InventoryBalanceListReturn> listOrder = new List<InventoryBalanceListReturn>();
            int? i = 0;
            InventoryBalanceDAO oDAO = new InventoryBalanceDAO();

            i = oDAO.DeleteInventoryBalance(ibInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateInventoryBalance")]
        public IHttpActionResult UpdateInvenBalance([FromBody]InventoryBalanceInfo ibInfo)
        {
            // OrderListReturn orderList = new OrderListReturn();
            //  List<InventoryBalanceListReturn> listOrder = new List<InventoryBalanceListReturn>();
            int? i = 0;
            InventoryBalanceDAO oDAO = new InventoryBalanceDAO();

            i = oDAO.UpdateInventoryBalance(ibInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteInventoryBalanceFromInventoryCode")]
        public IHttpActionResult DeleteInvenBalanceFromInventoryCode([FromBody]InventoryBalanceInfo ibInfo)
        {
            // OrderListReturn orderList = new OrderListReturn();
            //  List<InventoryBalanceListReturn> listOrder = new List<InventoryBalanceListReturn>();
            int? i = 0;
            InventoryBalanceDAO oDAO = new InventoryBalanceDAO();

            i = oDAO.DeleteInventoryBalanceFromInventoryCode(ibInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInvenBalanceExactNoPagingByCriteria")] // Controller from Select All Product in InventoryDetail
        public IHttpActionResult ListInventoryBalanceExactNoPagingByCriteria([FromBody]InventoryDetailInfo cInfo)
        {
            InventoryBalanceListReturn inventoryList = new InventoryBalanceListReturn();
            List<InventoryBalanceListReturn> listInventory = new List<InventoryBalanceListReturn>();
            InventoryDetailDAO cDAO = new InventoryDetailDAO();

            listInventory = cDAO.ListInvenBalanceExactNoPagingByCriteria(cInfo);

            return Ok<List<InventoryBalanceListReturn>>(listInventory);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInvenBalanceProductNoPagingByCriteria")] // Controller from Select ProductList in Select Product of Take Order
        public IHttpActionResult ListInventoryBalanceProdNoPagingByCriteria([FromBody]InventoryDetailInfo cInfo)
        {
            InventoryBalanceListReturn inventoryList = new InventoryBalanceListReturn();
            List<InventoryBalanceListReturn> listInventory = new List<InventoryBalanceListReturn>();
            InventoryDetailDAO cDAO = new InventoryDetailDAO();

            listInventory = cDAO.ListInvenBalanceProductNoPagingByCriteria(cInfo);

            return Ok<List<InventoryBalanceListReturn>>(listInventory);
        }
    }
}
