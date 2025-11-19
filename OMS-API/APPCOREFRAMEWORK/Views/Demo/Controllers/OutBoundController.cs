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
    public class OutBoundController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListClickToCallNoPagingByCriteria")]
        public IHttpActionResult ListCiNoPagingByCriteria([FromBody]LeadManagementInfo cInfo)
        {
            LeadManagementInfo cInList = new LeadManagementInfo();
            List<LeadManagementInfo> listCIn = new List<LeadManagementInfo>();
            OutBoundDAO callDAO = new OutBoundDAO();

            listCIn = callDAO.ListCallInNoPagingByCriteria(cInfo);

            return Ok<List<LeadManagementInfo>>(listCIn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListClickToCallNoPagingByCriteria")]
        public IHttpActionResult ListCountClicktocallNoPagingByCriteria([FromBody] LeadManagementInfo cInfo)
        {
            OutBoundDAO oDAO = new OutBoundDAO();
            int? i = 0;
            i = oDAO.CountListCallInNoPagingByCriteria(cInfo);

            return Ok<int?>(i);
        }
    }
}
