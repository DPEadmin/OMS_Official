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
    public class BankController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertBankInfo")]
        public IHttpActionResult InsertBankInfo([FromBody] BankInfo bInfo)
        {
            int i = 0;

            BankInfoDAO oDAO = new BankInfoDAO();

            i = oDAO.InsertBankInfo(bInfo);
            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateBankInfo")]
        public IHttpActionResult UpdateBankInfo([FromBody] BankInfo bInfo)
        {
            int i = 0;

            BankInfoDAO oDAO = new BankInfoDAO();

            i = oDAO.InsertBankInfo(bInfo);
            return Ok<int>(i);
        }
    }
}

