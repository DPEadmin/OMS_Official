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
    public class VoucherController : ApiController
    {

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListVoucherNopagingByCriteria")]
        public IHttpActionResult ListVoucherNopagingByCriteria([FromBody]VoucherInfo bInfo)
        {
            List<VoucherListReturn> listVoucher = new List<VoucherListReturn>();
            VoucherDAO bDAO = new VoucherDAO();

            listVoucher = bDAO.ListVoucherNopagingByCriteria(bInfo);

            return Ok<List<VoucherListReturn>>(listVoucher);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListVoucherByCriteria")]
        public IHttpActionResult ListVoucherByCriteria([FromBody]VoucherInfo bInfo)
        {
            List<VoucherListReturn> listVoucher = new List<VoucherListReturn>();
            VoucherDAO bDAO = new VoucherDAO();

            listVoucher = bDAO.ListVoucherByCriteria(bInfo);

            return Ok<List<VoucherListReturn>>(listVoucher);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountVoucherListByCriteria")]
        public IHttpActionResult CountVoucherListByCriteria([FromBody]VoucherInfo rInfo)
        {

            VoucherDAO bDAO = new VoucherDAO();
            int? i = 0;
            i = bDAO.CountVoucherListByCriteria(rInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertVoucher")]
        public IHttpActionResult InsertVoucher([FromBody]VoucherInfo rInfo)
        {
            VoucherDAO rDAO = new VoucherDAO();
            int i = 0;
            i = rDAO.InsertVoucher(rInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateVoucher")]
        public IHttpActionResult UpdateVoucher([FromBody]VoucherInfo rInfo)
        {
            VoucherDAO rDAO = new VoucherDAO();
            int i = 0;
            i = rDAO.UpdateVoucher(rInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteVoucher")]
        public IHttpActionResult DeleteVoucher([FromBody]VoucherInfo rInfo)
        {
            VoucherDAO rDAO = new VoucherDAO();
            int i = 0;
            i = rDAO.DeleteVoucher(rInfo);

            return Ok<int>(i);
        }
    }
}
