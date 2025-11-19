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
using System.Data;

namespace APPCOREVIEW.Views.Demo.Controllers
{
    public class OrderChangeStatusController : ApiController
    {
       

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/OrderChangeStatusInfo")]
        public IHttpActionResult UpdateOdInfo([FromBody]L_OrderChangestatus oInfo)
        {

            OrderChangestatusDAO oDAO = new OrderChangestatusDAO();
            int i = 0;
            foreach (var ordercode in oInfo.L_OrderChangestatusInfo.ToList())
            {
                i = oDAO.UpdateOrderChangeStatusInfo(ordercode);
            }
            return Ok<int>(i);
        }


    }
}
