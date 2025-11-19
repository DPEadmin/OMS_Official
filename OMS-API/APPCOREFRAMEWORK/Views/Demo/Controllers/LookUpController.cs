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
    public class LookupController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListLookupNopagingByCriteria")]
        public IHttpActionResult ListLookUpNopagingByCriteria([FromBody]LookupInfo lInfo)
        {
            LookupListReturn lookUpList = new LookupListReturn();
            List<LookupListReturn> listLookUp = new List<LookupListReturn>();
            LookupDAO cDAO = new LookupDAO();

            listLookUp = cDAO.ListLookupNopagingByCriteria(lInfo);

            return Ok<List<LookupListReturn>>(listLookUp);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetBranch")]
        public IHttpActionResult GETBranch([FromBody]LookupInfo lInfo)
        {
            LookupListReturn lookUpList = new LookupListReturn();
            List<LookupListReturn> listLookUp = new List<LookupListReturn>();
            LookupDAO cDAO = new LookupDAO();

            listLookUp = cDAO.GetBranch(lInfo);

            return Ok<List<LookupListReturn>>(listLookUp);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetAccountType")]
        public IHttpActionResult GETAccountType([FromBody]LookupInfo lInfo)
        {
            LookupListReturn lookUpList = new LookupListReturn();
            List<LookupListReturn> listLookUp = new List<LookupListReturn>();
            LookupDAO cDAO = new LookupDAO();

            listLookUp = cDAO.GetAccountType(lInfo);

            return Ok<List<LookupListReturn>>(listLookUp);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetAccountNumber")]
        public IHttpActionResult GETAccountNumber([FromBody]LookupInfo lInfo)
        {
            LookupListReturn lookUpList = new LookupListReturn();
            List<LookupListReturn> listLookUp = new List<LookupListReturn>();
            LookupDAO cDAO = new LookupDAO();

            listLookUp = cDAO.GetAccountNumber(lInfo);

            return Ok<List<LookupListReturn>>(listLookUp);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetLockAmountFlag")]
        public IHttpActionResult GETLockAmountFlag()
        {
            LookupListReturn lookUpList = new LookupListReturn();
            List<LookupListReturn> listLookUp = new List<LookupListReturn>();
            LookupDAO cDAO = new LookupDAO();

            listLookUp = cDAO.GetLockAmountFlag();

            return Ok<List<LookupListReturn>>(listLookUp);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListStatusCode")]
        public IHttpActionResult ListStatusCode([FromBody]LookupInfo lInfo)
        {
            LookupListReturn lookUpList = new LookupListReturn();
            List<LookupListReturn> listLookUp = new List<LookupListReturn>();
            LookupDAO cDAO = new LookupDAO();

            listLookUp = cDAO.ListLookupNopagingByCriteria(lInfo);

            return Ok<List<LookupListReturn>>(listLookUp);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListLookupContactStatusMapOrderSituation")]
        public IHttpActionResult ListLookupContactStatusMapOrderSituation([FromBody] LookupInfo lInfo)
        {
            LookupListReturn lookUpList = new LookupListReturn();
            List<LookupListReturn> listLookUp = new List<LookupListReturn>();
            LookupDAO cDAO = new LookupDAO();

            listLookUp = cDAO.ListLookupContactStatusMapOrderSituation(lInfo);

            return Ok<List<LookupListReturn>>(listLookUp);
        }
    }
}
