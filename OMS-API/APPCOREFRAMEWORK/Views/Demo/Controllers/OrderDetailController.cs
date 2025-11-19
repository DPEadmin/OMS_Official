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
    public class OrderDetailController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderMapCustomerNopagingByCriteria")]
        public IHttpActionResult ListOrderMapCustomerNopagingByCriteria([FromBody]OrderDetailInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderDetailListReturn> listOrder = new List<OrderDetailListReturn>();
            OrderDetailDAO oDAO = new OrderDetailDAO();

            listOrder = oDAO.ListOrderMapCustomerNopagingByCriteria(oInfo);

            return Ok<List<OrderDetailListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderPaymentMapCustomerNopagingByCriteria")]
        public IHttpActionResult ListOrderPaymentMapCustomerNopagingByCriteria([FromBody] OrderDetailInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderDetailListReturn> listOrder = new List<OrderDetailListReturn>();
            OrderDetailDAO oDAO = new OrderDetailDAO();

            listOrder = oDAO.ListOrderPaymentMapCustomerNopagingByCriteria(oInfo);

            return Ok<List<OrderDetailListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListGetCustomerPhoneNopagingByCriteria")]
        public IHttpActionResult ListGetCustomerPhoneNopagingByCriteria([FromBody]OrderDetailInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderDetailListReturn> listOrder = new List<OrderDetailListReturn>();
            OrderDetailDAO oDAO = new OrderDetailDAO();

            listOrder = oDAO.ListGetCustomerPhoneNopagingByCriteria(oInfo);

            return Ok<List<OrderDetailListReturn>>(listOrder);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCustomerAddressDetailByCriteria")]
        public IHttpActionResult ListCustomerAddressDetailByCriteria([FromBody]CustomerAddressInfo oInfo)
        {
            CustomerAddressListReturn orderList = new CustomerAddressListReturn();
            List<CustomerAddressListReturn> listOrder = new List<CustomerAddressListReturn>();
            OrderDetailDAO oDAO = new OrderDetailDAO();

            listOrder = oDAO.ListCustomerAddressDetailByCriteria(oInfo);

            return Ok<List<CustomerAddressListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderDetailMapProductNopagingByCriteria")]
        public IHttpActionResult ListOrderDetailMapProductNopagingByCriteria([FromBody]OrderDetailInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderDetailListReturn> listOrder = new List<OrderDetailListReturn>();
            OrderDetailDAO oDAO = new OrderDetailDAO();

            listOrder = oDAO.ListOrderDetailMapProductNopagingByCriteria(oInfo);

            return Ok<List<OrderDetailListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountOrderDetailMapProductByCriteria")]
        public IHttpActionResult CountOrderDetailMapProductByCriteria([FromBody]OrderDetailInfo oInfo)
        {
            OrderDetailDAO oDAO = new OrderDetailDAO();
            int? i = 0;
            i = oDAO.CountOrderDetailMapProductByCriteria(oInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/sumOrderDetail")]
        public IHttpActionResult sumOrderDetail([FromBody]OrderDetailInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderDetailListReturn> listOrder = new List<OrderDetailListReturn>();
            OrderDetailDAO oDAO = new OrderDetailDAO();

            listOrder = oDAO.sumOrderDetail(oInfo);

            return Ok<List<OrderDetailListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCustomerAddressDetailOrderByCriteria")]
        public IHttpActionResult ListCustomerAddressDetailOrderByCriteria([FromBody]CustomerAddressInfo oInfo)
        {
            CustomerAddressListReturn orderList = new CustomerAddressListReturn();
            List<CustomerAddressListReturn> listOrder = new List<CustomerAddressListReturn>();
            OrderDetailDAO oDAO = new OrderDetailDAO();

            listOrder = oDAO.ListCustomerAddressDetailOrderByCriteria(oInfo);

            return Ok<List<CustomerAddressListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderDetailNopagingbyOrderCode")]
        public IHttpActionResult ListOrderDetailNopagingbyOrderCode([FromBody]orderdataInfo oInfo)
        {
            orderdataInfo orderdata = new orderdataInfo();
            List<orderdataInfo> listorderdata = new List<orderdataInfo>();
            OrderDetailDAO oDAO = new OrderDetailDAO();

            listorderdata = oDAO.ListOrderDetailNopagingbyOrderCode(oInfo);

            return Ok<List<orderdataInfo>>(listorderdata);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderDetailNopagingbyOrderCode2")]
        public IHttpActionResult ListOrderDetailNopagingbyOrderCode2([FromBody] orderdataInfo oInfo)
        {
            orderdataInfo orderdata = new orderdataInfo();
            List<orderdataInfo> listorderdata = new List<orderdataInfo>();
            OrderDetailDAO oDAO = new OrderDetailDAO();

            listorderdata = oDAO.ListOrderDetailNopagingbyOrderCode2(oInfo);

            return Ok<List<orderdataInfo>>(listorderdata);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderDetailMapProductAndPromotionNopagingByCriteria")]
        public IHttpActionResult ListOrderDetailMapProductAndPromotionNopagingByCriteria([FromBody]OrderDetailInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderDetailListReturn> listOrder = new List<OrderDetailListReturn>();
            OrderDetailDAO oDAO = new OrderDetailDAO();

            listOrder = oDAO.ListOrderDetailMapProductAndPromotionNopagingByCriteria(oInfo);

            return Ok<List<OrderDetailListReturn>>(listOrder);
        }
    }
}