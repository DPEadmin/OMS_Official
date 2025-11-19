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
    public class SubDistrictController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountSubDistrictListByCriteria")]
        public IHttpActionResult CountSdListByCriteria([FromBody]SubDistrictInfo sInfo)
        {
         
            SubDistrictDAO sDAO = new SubDistrictDAO();
            int? i = 0;
            i = sDAO.CountSubDistrictListByCriteria(sInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateSubDistrict")]
        public IHttpActionResult UpdSubDistrict([FromBody]SubDistrictInfo sInfo)
        {
         
            SubDistrictDAO sDAO = new SubDistrictDAO();
            int? i = 0;
            i = sDAO.UpdateSubDistrict(sInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteSubDistrict")]
        public IHttpActionResult DelSubDistrict([FromBody]SubDistrictInfo sInfo)
        {

            SubDistrictDAO sDAO = new SubDistrictDAO();
            int i = 0;
            i = sDAO.DeleteSubDistrict(sInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertSubDistrict")]
        public IHttpActionResult InsSubDistrict([FromBody]SubDistrictInfo sInfo)
        {

            SubDistrictDAO sDAO = new SubDistrictDAO();
            int? i = 0;
            i = sDAO.InsertSubDistrict(sInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListSubDistrictByCriteria")]
        public IHttpActionResult ListSdByCriteria([FromBody]SubDistrictInfo sInfo)
        {
            SubDistrictListReturn subDistrictList = new SubDistrictListReturn();
            List<SubDistrictListReturn> listSubDistrict = new List<SubDistrictListReturn>();
            SubDistrictDAO cDAO = new SubDistrictDAO();

            listSubDistrict = cDAO.ListSubDistrictByCriteria(sInfo);

            return Ok<List<SubDistrictListReturn>>(listSubDistrict);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMaxSubDistrict")]
        public IHttpActionResult ListMaxSd([FromBody]SubDistrictInfo sInfo)
        {
            SubDistrictListReturn subDistrictList = new SubDistrictListReturn();
            List<SubDistrictListReturn> listSubDistrict = new List<SubDistrictListReturn>();
            SubDistrictDAO cDAO = new SubDistrictDAO();

            listSubDistrict = cDAO.ListMaxSubDistrict(sInfo);

            return Ok<List<SubDistrictListReturn>>(listSubDistrict);
        }

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("api/support/ListSubDistrictNopagingByCriteria")]
        //public IHttpActionResult ListSdNopagingByCriteria([FromBody]SubDistrictInfo sInfo)
        //{
        //    SubDistrictListReturn subDistrictList = new SubDistrictListReturn();
        //    List<SubDistrictListReturn> listSubDistrict = new List<SubDistrictListReturn>();
        //    SubDistrictDAO cDAO = new SubDistrictDAO();

        //    listSubDistrict = cDAO.ListSubDistrictNopagingByCriteria(sInfo);

        //    return Ok<List<SubDistrictListReturn>>(listSubDistrict);
        //}

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListTumbonNopagingByCriteria")]
        public IHttpActionResult ListTumBonNopagingByCriteria([FromBody]SubDistrictInfo sInfo)
        {
            SubDistrictListReturn subDistrictList = new SubDistrictListReturn();
            List<SubDistrictListReturn> listSubDistrict = new List<SubDistrictListReturn>();
            SubDistrictDAO cDAO = new SubDistrictDAO();

            listSubDistrict = cDAO.ListTumbonNopagingByCriteria(sInfo);

            return Ok<List<SubDistrictListReturn>>(listSubDistrict);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListSubDistrictNoPagingByCriteria")]
        public IHttpActionResult ListSubDisNopagingByCriteria([FromBody]SubDistrictInfo sInfo)
        {
            SubDistrictListReturn subDistrictList = new SubDistrictListReturn();
            List<SubDistrictListReturn> listSubDistrict = new List<SubDistrictListReturn>();
            SubDistrictDAO cDAO = new SubDistrictDAO();

            listSubDistrict = cDAO.ListSubDistrictNoPagingByCriteria(sInfo);

            return Ok<List<SubDistrictListReturn>>(listSubDistrict);
        }
    }
}
