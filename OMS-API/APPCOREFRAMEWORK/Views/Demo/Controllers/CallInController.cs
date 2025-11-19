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
    public class CallInController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCallInNoPagingByCriteria")]
        public IHttpActionResult ListCiNoPagingByCriteria([FromBody]CallInInfo cInfo)
        {
            CallInListReturn callInList = new CallInListReturn();
            List<CallInListReturn> listCallIn = new List<CallInListReturn>();
            CallInDAO callDAO = new CallInDAO();

            listCallIn = callDAO.ListCallInNoPagingByCriteria(cInfo);

            return Ok<List<CallInListReturn>>(listCallIn);
        }
    }
}
