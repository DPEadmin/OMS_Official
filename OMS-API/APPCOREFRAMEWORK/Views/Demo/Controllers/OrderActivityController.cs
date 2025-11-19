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
    public class OrderActivityController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderActivityNopagingByCriteria")]
        public IHttpActionResult ListOrderActivityNopagingByCriteria([FromBody]OrderActivityInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderActivityInfoReturn> listOrder = new List<OrderActivityInfoReturn>();
            OrderActivityDAO oDAO = new OrderActivityDAO();

            listOrder = oDAO.ListOrderActivityNopagingByCriteria(oInfo);

            return Ok<List<OrderActivityInfoReturn>>(listOrder);
        }
    }
}