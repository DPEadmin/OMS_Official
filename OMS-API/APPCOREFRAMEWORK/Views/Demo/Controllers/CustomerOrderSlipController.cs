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
    public class CustomerOrderSlipController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertCustomerOrderSlip")]
        public IHttpActionResult InsertOrderFile([FromBody] CustomerOrderSlipInfo opfInfo)
        {
            int i = 0;

            CustomerOrderSlipDAO oDAO = new CustomerOrderSlipDAO();

            i = oDAO.InsertOrderFile(opfInfo);
            return Ok<int>(i);
        }
    }
}

