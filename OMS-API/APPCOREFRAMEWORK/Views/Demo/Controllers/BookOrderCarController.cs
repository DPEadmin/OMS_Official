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
    public class BookOrderCarController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertBookOrderCar")]
        public IHttpActionResult InsBookOrderCar([FromBody]BookOrderCarInfo cInfo)
        {
            //BookOrderCarListReturn bookOrderCarList = new BookOrderCarListReturn();
            //List<BookOrderCarListReturn> listBookOrderCar = new List<BookOrderCarListReturn>();
            BookOrderCarDAO cDAO = new BookOrderCarDAO();
            int? i = 0;
            i = cDAO.InsertBookOrderCar(cInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCustomerBookOrderCar")]
        public IHttpActionResult UpdCustomerBookOrderCar([FromBody]BookOrderCarInfo cInfo)
        {
            //BookOrderCarListReturn bookOrderCarList = new BookOrderCarListReturn();
            //List<BookOrderCarListReturn> listBookOrderCar = new List<BookOrderCarListReturn>();
            BookOrderCarDAO cDAO = new BookOrderCarDAO();
            int? i = 0;
            i = cDAO.UpdateCustomerBookOrderCar(cInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteCustomerBookOrderCar")]
        public IHttpActionResult DelCustomerBookOrderCar([FromBody]BookOrderCarInfo cInfo)
        {
            //BookOrderCarListReturn bookOrderCarList = new BookOrderCarListReturn();
            //List<BookOrderCarListReturn> listBookOrderCar = new List<BookOrderCarListReturn>();
            BookOrderCarDAO cDAO = new BookOrderCarDAO();
            int? i = 0;
            i = cDAO.DeleteCustomerBookOrderCar(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderBookingByCriteria_showgv")]
        public IHttpActionResult ListOBDByCriteriaBookOrder([FromBody]OrderInfo oInfo)
        {
            OrderListReturn driverList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            BookOrderCarDAO dDAO = new BookOrderCarDAO();

            listOrder = dDAO.ListOrderByCriteriaBookOrder(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountOrderBookOrderCarByCriteria")]
        public IHttpActionResult CountOBDListByCriteriaBookOrder([FromBody]OrderInfo oInfo)
        {
          
            BookOrderCarDAO dDAO = new BookOrderCarDAO();
            int? i = 0;
            i = dDAO.CountOrderListByCriteriaBookOrderCar(oInfo);

            return Ok<int?>(i);
        }

    }
}
