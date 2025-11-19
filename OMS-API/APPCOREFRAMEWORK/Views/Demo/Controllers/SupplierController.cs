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
    public class SupplierController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateSupplier")]
        public IHttpActionResult UpdSupplier([FromBody]SupplierInfo sInfo)
        {
            SupplierDAO sDAO = new SupplierDAO();
            int i = 0;
            i = sDAO.UpdateSupplier(sInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteSupplier")]
        public IHttpActionResult DelSupplier([FromBody]SupplierInfo sInfo)
        {
            SupplierDAO sDAO = new SupplierDAO();
            int i = 0;
            i = sDAO.DeleteSupplier(sInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertSupplier")]
        public IHttpActionResult InsSupplier([FromBody]SupplierInfo sInfo)
        {
            SupplierDAO sDAO = new SupplierDAO();
            int i = 0;
            i = sDAO.InsertSupplier(sInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListSupplierNopagingByCriteria")]
        public IHttpActionResult ListSuppNopagingByCriteria([FromBody]SupplierInfo sInfo)
        {
            SupplierListReturn supplierList = new SupplierListReturn();
            List<SupplierListReturn> listSupplier = new List<SupplierListReturn>();
            SupplierDAO sDAO = new SupplierDAO();

            listSupplier = sDAO.ListSupplierNopagingByCriteria(sInfo);

            return Ok<List<SupplierListReturn>>(listSupplier);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListSupplierByCriteria")]
        public IHttpActionResult ListSuppByCriteria([FromBody]SupplierInfo sInfo)
        {
            SupplierListReturn supplierList = new SupplierListReturn();
            List<SupplierListReturn> listSupplier = new List<SupplierListReturn>();
            SupplierDAO sDAO = new SupplierDAO();

            listSupplier = sDAO.ListSupplierByCriteria(sInfo);

            return Ok<List<SupplierListReturn>>(listSupplier);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountSupplierListByCriteria")]
        public IHttpActionResult CountSuppListByCriteria([FromBody]SupplierInfo sInfo)
        {
            SupplierDAO sDAO = new SupplierDAO();
            int? i = 0;
            i = sDAO.CountSupplierListByCriteria(sInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/SupplierCodeValidate")]
        public IHttpActionResult SupplierCodeValid([FromBody]SupplierInfo sInfo)
        {
            SupplierListReturn supplier = new SupplierListReturn();
            List<SupplierListReturn> listsupplier = new List<SupplierListReturn>();
            SupplierDAO eDAO = new SupplierDAO();

            listsupplier = eDAO.SupplierCodeValidate(sInfo);

            return Ok<List<SupplierListReturn>>(listsupplier);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPOMapSupplier")]
        public IHttpActionResult ListPOMapSupp([FromBody]SupplierInfo sInfo)
        {
            SupplierListReturn supplierList = new SupplierListReturn();
            List<SupplierListReturn> listSupplier = new List<SupplierListReturn>();
            SupplierDAO sDAO = new SupplierDAO();

            listSupplier = sDAO.ListPOMapSupplier(sInfo);

            return Ok<List<SupplierListReturn>>(listSupplier);
        }
    }
}
