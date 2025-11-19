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
    public class CustomerOrderController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountOrderListByCriteriaOrderlist")]
        public IHttpActionResult CountOlByCriteriaOrderlist([FromBody]CustomerOrder_StatusPageModel cInfo)
        {
            //CustomerOrderListReturn customerOrderList = new CustomerOrderListReturn();
            //List<CustomerOrderListReturn> listCustomerOrder = new List<CustomerOrderListReturn>();
            CustomerOrderDAO cDAO = new CustomerOrderDAO();
            int? i = 0;
            i = cDAO.CountOrderListByCriteriaOrderlist(cInfo.COrderInfo, cInfo.StatusPage);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderByCriteriaOrderlist")]
        public IHttpActionResult ListOlByCriteriaOrderlist([FromBody]CustomerOrder_StatusPageModel cInfo)
        {
            CustomerOrderListReturn customerOrderList = new CustomerOrderListReturn();
            List<CustomerOrderListReturn> listCustomerOrder = new List<CustomerOrderListReturn>();
            CustomerOrderDAO cDAO = new CustomerOrderDAO();
    
            listCustomerOrder = cDAO.ListOrderByCriteriaOrderlist(cInfo.COrderInfo, cInfo.StatusPage);

            return Ok<List<CustomerOrderListReturn>>(listCustomerOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCustomerOrderNoPagingbyCriteria")]
        public IHttpActionResult ListCusOrderNoPagingbyCriteria([FromBody]CustomerOrderInfo cInfo)
        {
            CustomerOrderListReturn customerOrderList = new CustomerOrderListReturn();
            List<CustomerOrderListReturn> listCustomerOrder = new List<CustomerOrderListReturn>();
            CustomerOrderDAO cDAO = new CustomerOrderDAO();

            listCustomerOrder = cDAO.ListCustomerOrderNoPagingbyCriteria(cInfo);

            return Ok<List<CustomerOrderListReturn>>(listCustomerOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderByCriteriaOrderlist_ReceiptReturn")]
        public IHttpActionResult ListOlByCriteriaOl_ReceiptReturn([FromBody]ReceiptReturnOrderInfo cInfo)
        {
            ReceiptReturnOrderListReturn customerOrderList = new ReceiptReturnOrderListReturn();
            List<ReceiptReturnOrderListReturn> listCustomerOrder = new List<ReceiptReturnOrderListReturn>();
            CustomerOrderDAO cDAO = new CustomerOrderDAO();
   
            listCustomerOrder = cDAO.ListOrderByCriteriaOrderlist_ReceiptReturn(cInfo);

            return Ok<List<ReceiptReturnOrderListReturn>>(listCustomerOrder);
        }
    }
}
