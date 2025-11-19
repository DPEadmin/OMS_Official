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
    public class CustomerPhoneController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountCustomerPhoneListByCriteria")]
        public IHttpActionResult CountCusPhoneListByCriteria([FromBody]CustomerPhoneInfo cInfo)
        {

            CustomerPhoneDAO cDAO = new CustomerPhoneDAO();
            int? i = 0;
            i = cDAO.CountCustomerPhoneListByCriteria(cInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountCustomerPhoneCallListByCriteria")]
        public IHttpActionResult CountCusPhoneCallListByCriteria([FromBody]CustomerPhoneInfo cInfo)
        {

            CustomerPhoneDAO cDAO = new CustomerPhoneDAO();
            int? i = 0;
            i = cDAO.CountCustomerPhoneCallListByCriteria(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCustomerPhone")]
        public IHttpActionResult ListCusPhone([FromBody]CustomerPhoneInfo cInfo)
        {
            CustomerPhoneListReturn customerPhoneList = new CustomerPhoneListReturn();
            List<CustomerPhoneListReturn> listCustomerPhone = new List<CustomerPhoneListReturn>();
            CustomerPhoneDAO cDAO = new CustomerPhoneDAO();

            listCustomerPhone = cDAO.ListCustomerPhone(cInfo);

            return Ok<List<CustomerPhoneListReturn>>(listCustomerPhone);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCustomerPhoneCall")]
        public IHttpActionResult ListCusPhoneCall([FromBody]CustomerPhoneInfo cInfo)
        {
            CustomerPhoneListReturn customerPhoneList = new CustomerPhoneListReturn();
            List<CustomerPhoneListReturn> listCustomerPhone = new List<CustomerPhoneListReturn>();
            CustomerPhoneDAO cDAO = new CustomerPhoneDAO();

            listCustomerPhone = cDAO.ListCustomerPhoneCall(cInfo);

            return Ok<List<CustomerPhoneListReturn>>(listCustomerPhone);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteCustomerPhone")]
        public IHttpActionResult DelCustomerPhone([FromBody]CustomerPhoneInfo cInfo)
        {

            CustomerPhoneDAO cDAO = new CustomerPhoneDAO();
            int i = 0;
            i = cDAO.DeleteCustomerPhone(cInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertCustomerPhone")]
        public IHttpActionResult InsCustomerPhone([FromBody]CustomerPhoneInfo cInfo)
        {

            CustomerPhoneDAO cDAO = new CustomerPhoneDAO();
            int i = 0;
            i = cDAO.InsertCustomerPhone(cInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CustomerPhone")]
        public IHttpActionResult InsertCusPhone([FromBody]CustomerPhoneInfo cInfo)
        {

            CustomerPhoneDAO cDAO = new CustomerPhoneDAO();
            int i = 0;
            i = cDAO.InsertCustomerPhone(cInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCustomerPhone")]
        public IHttpActionResult UpdCustomerPhone([FromBody]CustomerPhoneInfo cInfo)
        {

            CustomerPhoneDAO cDAO = new CustomerPhoneDAO();
            int i = 0;
            i = cDAO.UpdateCustomerPhone(cInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCustomerPhoneTakeOrderMK")]
        public IHttpActionResult UpdateCusPhoneTakeOrderMK([FromBody]CustomerPhoneInfo cInfo)
        {

            CustomerPhoneDAO cDAO = new CustomerPhoneDAO();
            int i = 0;
            i = cDAO.UpdateCustomerPhoneTakeOrderMK(cInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ValidateCustomerPhone")]
        public IHttpActionResult ValidateCusPhone([FromBody]CustomerPhoneInfo cInfo)
        {
            CustomerPhoneListReturn customerPhoneList = new CustomerPhoneListReturn();
            List<CustomerPhoneListReturn> listCustomerPhone = new List<CustomerPhoneListReturn>();
            CustomerPhoneDAO cDAO = new CustomerPhoneDAO();

            listCustomerPhone = cDAO.ValidateCustomerPhone(cInfo);

            return Ok<List<CustomerPhoneListReturn>>(listCustomerPhone);
        }
    }
}
