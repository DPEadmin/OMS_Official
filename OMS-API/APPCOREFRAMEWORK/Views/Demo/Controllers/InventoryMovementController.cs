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
    public class InventoryMovementController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountInventoryMovementByCriteria")]
        public IHttpActionResult CountInvenMovementByCriteria([FromBody]InventoryMovementInfo imInfo)
        {
            InventoryMovementDAO imDAO = new InventoryMovementDAO();
            int? i = 0;
            i = imDAO.CountInventoryMovementByCriteria(imInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMaxSeqIdByCriteria")]
        public IHttpActionResult ListMxSeqIdByCriteria([FromBody]InventoryMovementInfo imInfo)
        {
            InventoryMovementDAO imDAO = new InventoryMovementDAO();
            int i = 0;
            i = imDAO.ListMaxSeqIdByCriteria(imInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMaxSeqManualIdByCriteria")]
        public IHttpActionResult ListMaxSeqManualIdByCriteria([FromBody]InventoryMovementInfo imInfo)
        {
            InventoryMovementDAO imDAO = new InventoryMovementDAO();
            int i = 0;
            i = imDAO.ListMaxSeqManualIdByCriteria(imInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventoryMovementInfoPagingByCriteria")]
        public IHttpActionResult ListInvenMovementInfoPagingByCriteria([FromBody]InventoryMovementInfo imInfo)
        {
            InventoryMovementListReturn inventoryMovementList = new InventoryMovementListReturn();
            List<InventoryMovementListReturn> listInventoryMovement = new List<InventoryMovementListReturn>();
            InventoryMovementDAO imDAO = new InventoryMovementDAO();
            listInventoryMovement = imDAO.ListInventoryMovementInfoPagingByCriteria(imInfo);

            return Ok<List<InventoryMovementListReturn>>(listInventoryMovement);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertInventoryMovement")]
        public IHttpActionResult InsertInvenMovement([FromBody]InventoryMovementInfo imInfo)
        {
            InventoryMovementDAO imDAO = new InventoryMovementDAO();
            int i = 0;
            i = imDAO.InsertInventoryMovement(imInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteInventoryMovement")]
        public IHttpActionResult DeleteInvenMovement([FromBody]InventoryMovementInfo imInfo)
        {
            InventoryMovementDAO imDAO = new InventoryMovementDAO();
            int i = 0;
            i = imDAO.DeleteInventoryMovement(imInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateInventoryMovement")]
        public IHttpActionResult UpdateInvenMovement([FromBody]InventoryMovementInfo imInfo)
        {
            InventoryMovementDAO imDAO = new InventoryMovementDAO();
            int i = 0;
            i = imDAO.UpdateInventoryMovement(imInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventoryMovementNoPagingSelectedFormTakeOrderRetail")]
        public IHttpActionResult ListInventoryMovementNoPagingSelectedFormTakeOrderRetail([FromBody]InventoryMovementInfo imInfo)
        {
            InventoryMovementListReturn inventoryMovementList = new InventoryMovementListReturn();
            List<InventoryMovementListReturn> listInventoryMovement = new List<InventoryMovementListReturn>();
            InventoryMovementDAO imDAO = new InventoryMovementDAO();
            listInventoryMovement = imDAO.ListInventoryMovementNoPagingSelectedFormTakeOrderRetail(imInfo);

            return Ok<List<InventoryMovementListReturn>>(listInventoryMovement);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateInventoryMovementfromTakeOrderRetail")]
        public IHttpActionResult UpdateInventoryMovementfromTakeOrderRetail([FromBody]InventoryMovementInfo imInfo)
        {
            InventoryMovementDAO imDAO = new InventoryMovementDAO();
            int i = 0;
            i = imDAO.UpdateInventoryMovementfromTakeOrderRetail(imInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventoryMovementNoPagingByCriteria")]
        public IHttpActionResult ListInventoryMovementNoPagingByCriteria([FromBody]InventoryMovementInfo imInfo)
        {
            InventoryMovementListReturn inventoryMovementList = new InventoryMovementListReturn();
            List<InventoryMovementListReturn> listInventoryMovement = new List<InventoryMovementListReturn>();
            InventoryMovementDAO imDAO = new InventoryMovementDAO();
            listInventoryMovement = imDAO.ListInventoryMovementNoPagingByCriteria(imInfo);

            return Ok<List<InventoryMovementListReturn>>(listInventoryMovement);
        }
    }
}
