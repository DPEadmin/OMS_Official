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
    public class DistrictController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDistrictNopagingByCriteria")]
        public IHttpActionResult ListDistNopagingByCriteria([FromBody]DistrictInfo cInfo)
        {
            DistrictListReturn districtList = new DistrictListReturn();
            List<DistrictListReturn> listDistrict = new List<DistrictListReturn>();
            DistrictDAO cDAO = new DistrictDAO();

            listDistrict = cDAO.ListDistrictNopagingByCriteria(cInfo);

            return Ok<List<DistrictListReturn>>(listDistrict);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountDistrictListByCriteria")]
        public IHttpActionResult CountDistListByCriteria([FromBody]DistrictInfo dInfo)
        {
            
            DistrictDAO dDAO = new DistrictDAO();
            int? i = 0;
            i = dDAO.CountDistrictListByCriteria(dInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertDistrict")]
        public IHttpActionResult InsertDistrict([FromBody]DistrictInfo dInfo)
        {
            
            DistrictDAO dDAO = new DistrictDAO();
            int i = 0;
            i = dDAO.InsertDistrict(dInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateDistrict")]
        public IHttpActionResult UpdDistrict([FromBody]DistrictInfo dInfo)
        {

            DistrictDAO dDAO = new DistrictDAO();
            int? i = 0;
            i = dDAO.UpdateDistrict(dInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteDistrict")]
        public IHttpActionResult DelDistrict([FromBody]DistrictInfo dInfo)
        {

            DistrictDAO dDAO = new DistrictDAO();
            int i = 0;
            i = dDAO.DeleteDistrict(dInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDistrictByCriteria")]
        public IHttpActionResult ListDtByCriteria([FromBody]DistrictInfo cInfo)
        {
            DistrictListReturn districtList = new DistrictListReturn();
            List<DistrictListReturn> listDistrict = new List<DistrictListReturn>();
            DistrictDAO cDAO = new DistrictDAO();

            listDistrict = cDAO.ListDistrictByCriteria(cInfo);

            return Ok<List<DistrictListReturn>>(listDistrict);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMaxDistrictByCriteria")]
        public IHttpActionResult ListMaxDtByCriteria([FromBody]DistrictInfo cInfo)
        {
            DistrictListReturn districtList = new DistrictListReturn();
            List<DistrictListReturn> listDistrict = new List<DistrictListReturn>();
            DistrictDAO cDAO = new DistrictDAO();

            listDistrict = cDAO.ListMaxDistrictByCriteria(cInfo);

            return Ok<List<DistrictListReturn>>(listDistrict);
        }
    }
}
