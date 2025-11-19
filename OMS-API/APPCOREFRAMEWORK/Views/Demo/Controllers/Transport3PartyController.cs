using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using APPCOREMODEL.OMSDAO;
using APPCOREMODEL.Datas.OMSDTO;

namespace APPCOREVIEW.Views.Demo.Controllers
{
    public class Transport3PartyController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/OrderTrackingNo")]
        public IHttpActionResult OrderTrackingNo([FromBody] ThaiPostInfo vInfo)
        {
            string i;
           
            Transport3PartyDAO vDAO = new Transport3PartyDAO();

            i = vDAO.GenThaipost(vInfo);

            return Ok<string>(i);
        }

    }
}