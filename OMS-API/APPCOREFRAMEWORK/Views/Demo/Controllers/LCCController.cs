using APPCOREMODEL.Datas.OMSDTO;
using APPCOREMODEL.OMSDAO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace APPCOREVIEW.Views.Demo.Controllers
{
    public class LCCController : ApiController
    {
        protected static string appUrl = ConfigurationManager.AppSettings["appUrl"];

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetLCC")]
        public IHttpActionResult ListCusByCriteria([FromBody] LCCInfo lccInfo)
        {
            List<LCCInfo> listLCC = new List<LCCInfo>();
            LCCDAO lccDAO = new LCCDAO();

            listLCC = lccDAO.GetLCC(lccInfo);

            return Ok<List<LCCInfo>>(listLCC);
        }
    }
}