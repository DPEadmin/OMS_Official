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
    public class InventoryController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountInventoryListByCriteria")]
        public IHttpActionResult CountInventoryListByCriteria([FromBody]InventoryInfo iInfo)
        {
            InventoryDAO iDAO = new InventoryDAO();
            int? i = 0;
            i = iDAO.CountInventoryListByCriteria(iInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventoryPagingByCriteria")]
        public IHttpActionResult ListInvenPagingByCriteria([FromBody]InventoryInfo iInfo)
        {
            InventoryListReturn inventoryList = new InventoryListReturn();
            List<InventoryListReturn> listInventory = new List<InventoryListReturn>();
            InventoryDAO iDAO = new InventoryDAO();

            listInventory = iDAO.ListInventoryPagingByCriteria(iInfo);

            return Ok<List<InventoryListReturn>>(listInventory);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventoryNoPagingByCriteria")]
        public IHttpActionResult ListInvenNoPagingByCriteria([FromBody]InventoryInfo iInfo)
        {
            InventoryListReturn inventoryList = new InventoryListReturn();
            List<InventoryListReturn> listInventory = new List<InventoryListReturn>();
            InventoryDAO iDAO = new InventoryDAO();

            listInventory = iDAO.ListInventoryNoPagingByCriteria(iInfo);

            return Ok<List<InventoryListReturn>>(listInventory);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventorybyEmpNoPagingCriteria")]
        public IHttpActionResult ListInvenbyEmpNoPagingCriteria([FromBody]InventoryInfo iInfo)
        {
            InventoryListReturn inventoryList = new InventoryListReturn();
            List<InventoryListReturn> listInventory = new List<InventoryListReturn>();
            InventoryDAO iDAO = new InventoryDAO();

            listInventory = iDAO.ListInventorybyEmpNoPagingCriteria(iInfo);

            return Ok<List<InventoryListReturn>>(listInventory);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventoryInfoPagingByCriteria")]
        public IHttpActionResult ListInvenInfoPagingByCriteria([FromBody]InventoryInfo iInfo)
        {
            InventoryListReturn inventoryList = new InventoryListReturn();
            List<InventoryListReturn> listInventory = new List<InventoryListReturn>();
            InventoryDAO iDAO = new InventoryDAO();

            listInventory = iDAO.ListInventoryInfoPagingByCriteria(iInfo);

            return Ok<List<InventoryListReturn>>(listInventory);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertInventory")]
        public IHttpActionResult InsertInven([FromBody]InventoryInfo iInfo)
        {
            InventoryDAO iDAO = new InventoryDAO();
            int i = 0;
            i = iDAO.InsertInventory(iInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteInventory")]
        public IHttpActionResult DeleteInven([FromBody]InventoryInfo iInfo)
        {
            InventoryDAO iDAO = new InventoryDAO();
            int i = 0;
            i = iDAO.DeleteInventoryList(iInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateInventory")]
        public IHttpActionResult UpdateInven([FromBody]InventoryInfo iInfo)
        {
            InventoryDAO iDAO = new InventoryDAO();
            int i = 0;
            i = iDAO.UpdateInventory(iInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateInventoryPolygon")]
        public IHttpActionResult UpdateInvenPolygon([FromBody] InventoryInfo iInfo)
        {
            InventoryDAO iDAO = new InventoryDAO();
            int i = 0;
            i = iDAO.UpdateInventoryPolygon(iInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventoryValidatePagingByCriteria")]
        public IHttpActionResult ListInvenValidatePagingByCriteria([FromBody]InventoryInfo iInfo)
        {
            InventoryListReturn inventoryList = new InventoryListReturn();
            List<InventoryListReturn> listInventory = new List<InventoryListReturn>();
            InventoryDAO iDAO = new InventoryDAO();

            listInventory = iDAO.ListInventoryValidatePagingByCriteria(iInfo);

            return Ok<List<InventoryListReturn>>(listInventory);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetListInventorybyCateriaNoPaging")]
        public IHttpActionResult GetListInvenbyCateriaNoPaging([FromBody]InventoryInfo iInfo)
        {
            InventoryListReturn inventoryList = new InventoryListReturn();
            List<InventoryListReturn> listInventory = new List<InventoryListReturn>();
            InventoryDAO iDAO = new InventoryDAO();

            listInventory = iDAO.GetListInventorybyCateriaNoPaging(iInfo);

            return Ok<List<InventoryListReturn>>(listInventory);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/POMapInventoryNoPaging")]
        public IHttpActionResult POMapInvNoPging([FromBody]InventoryInfo iInfo)
        {
            InventoryListReturn inventoryList = new InventoryListReturn();
            List<InventoryListReturn> listInventory = new List<InventoryListReturn>();
            InventoryDAO iDAO = new InventoryDAO();

            listInventory = iDAO.POMapInventoryNoPaging(iInfo);

            return Ok<List<InventoryListReturn>>(listInventory);
        }
    }
}
