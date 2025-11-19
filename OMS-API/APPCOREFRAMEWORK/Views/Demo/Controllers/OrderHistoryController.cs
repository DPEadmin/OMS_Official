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
    public class OrderHistoryController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetOrderHistory")]
        public IHttpActionResult GetOdHistory([FromBody]OrderHistoryInfo ohInfo)
        {
            OrderHistoryListReturn orderHistoryList = new OrderHistoryListReturn();
            List<OrderHistoryListReturn> listOrderHistory = new List<OrderHistoryListReturn>();
            OrderHistoryDAO ohDAO = new OrderHistoryDAO();

            listOrderHistory = ohDAO.GetOrderHistory(ohInfo);

            return Ok<List<OrderHistoryListReturn>>(listOrderHistory);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetOrderHistoryatSalesOrderPage")]
        public IHttpActionResult GetOrderHistoryatSalesOrderPage([FromBody]OrderDetailInfo ohInfo)
        {
            OrderDetailListReturn orderHistoryList = new OrderDetailListReturn();
            List<OrderDetailListReturn> listOrderHistory = new List<OrderDetailListReturn>();
            OrderHistoryDAO ohDAO = new OrderHistoryDAO();

            listOrderHistory = ohDAO.GetOrderHistoryatSalesOrderPage(ohInfo);

            return Ok<List<OrderDetailListReturn>>(listOrderHistory);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertOrderHistory")]
        public IHttpActionResult InsCampaign([FromBody]OrderHistoryInfo ohInfo)
        {

            OrderHistoryDAO ohDAO = new OrderHistoryDAO();
            int i = 0;
            i = ohDAO.InsertOrderHistory(ohInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetOrderHistoryWithoutProductList")]
        public IHttpActionResult GetOrderHistoryWithoutProductList([FromBody]OrderHistoryInfo ohInfo)
        {
            OrderHistoryListReturn orderHistoryList = new OrderHistoryListReturn();
            List<OrderHistoryListReturn> listOrderHistory = new List<OrderHistoryListReturn>();
            OrderHistoryDAO ohDAO = new OrderHistoryDAO();

            listOrderHistory = ohDAO.GetOrderHistoryWithoutProductList(ohInfo);

            return Ok<List<OrderHistoryListReturn>>(listOrderHistory);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetOrderHistoryWithoutProductListPaging")]
        public IHttpActionResult GetOrderHistoryWithoutProductListPaging([FromBody]OrderHistoryInfo ohInfo)
        {
            OrderHistoryListReturn orderHistoryList = new OrderHistoryListReturn();
            List<OrderHistoryListReturn> listOrderHistory = new List<OrderHistoryListReturn>();
            OrderHistoryDAO ohDAO = new OrderHistoryDAO();

            listOrderHistory = ohDAO.GetOrderHistoryWithoutProductListPaging(ohInfo);

            return Ok<List<OrderHistoryListReturn>>(listOrderHistory);
        }
    }
}
