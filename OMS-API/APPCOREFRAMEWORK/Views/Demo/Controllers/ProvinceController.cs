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
    public class ProvinceController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountProvinceListByCriteria")]
        public IHttpActionResult CountPvListByCriteria([FromBody]ProvinceInfo pInfo)
        {
            ProvinceDAO pDAO = new ProvinceDAO();
            int? i = 0;
            i = pDAO.CountProvinceListByCriteria(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertProvince")]
        public IHttpActionResult InsProvince([FromBody]ProvinceInfo pInfo)
        {
            ProvinceDAO pDAO = new ProvinceDAO();
            int i = 0;
            i = pDAO.InsertProvince(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateProvince")]
        public IHttpActionResult UpdProvince([FromBody]ProvinceInfo pInfo)
        {
            ProvinceDAO pDAO = new ProvinceDAO();
            int? i = 0;
            i = pDAO.UpdateProvince(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteProvince")]
        public IHttpActionResult DelProvince([FromBody]ProvinceInfo pInfo)
        {
            ProvinceDAO pDAO = new ProvinceDAO();
            int i = 0;
            i = pDAO.DeleteProvince(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProvinceNopagingByCriteria")]
        public IHttpActionResult ListPvNopagingByCriteria([FromBody]ProvinceInfo pInfo)
        {
            ProvinceListReturn provinceList = new ProvinceListReturn();
            List<ProvinceListReturn> listProvince = new List<ProvinceListReturn>();
            ProvinceDAO pDAO = new ProvinceDAO();

            listProvince = pDAO.ListProvinceNopagingByCriteria(pInfo);

            return Ok<List<ProvinceListReturn>>(listProvince);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProvinceByCriteria")]
        public IHttpActionResult ListPvByCriteria([FromBody]ProvinceInfo pInfo)
        {
            ProvinceListReturn provinceList = new ProvinceListReturn();
            List<ProvinceListReturn> listProvince = new List<ProvinceListReturn>();
            ProvinceDAO pDAO = new ProvinceDAO();

            listProvince = pDAO.ListProvinceByCriteria(pInfo);

            return Ok<List<ProvinceListReturn>>(listProvince);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMaxProvince")]
        public IHttpActionResult ListMaxPv([FromBody]ProvinceInfo pInfo)
        {
            ProvinceListReturn provinceList = new ProvinceListReturn();
            List<ProvinceListReturn> listProvince = new List<ProvinceListReturn>();
            ProvinceDAO pDAO = new ProvinceDAO();

            listProvince = pDAO.ListMaxProvince(pInfo);

            return Ok<List<ProvinceListReturn>>(listProvince);
        }
    }
}
