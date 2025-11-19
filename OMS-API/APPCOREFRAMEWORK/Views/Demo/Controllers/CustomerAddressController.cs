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
    public class CustomerAddressController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteCustomerAddress")]
        public IHttpActionResult DelCustomerAddress([FromBody]CustomerAddressInfo cInfo)
        {
            //CustomerAddressListReturn customerAddressList = new CustomerAddressListReturn();
            //List<CustomerAddressListReturn> listCustomerAddress = new List<CustomerAddressListReturn>();
            CustomerAddressDAO cDAO = new CustomerAddressDAO();
            int i = 0;
            i = cDAO.DeleteCustomerAddress(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListAddressValidate")]
        public IHttpActionResult ListAdValidate([FromBody]CustomerAddressInfo cInfo)
        {
            CustomerAddressListReturn customerAddressList = new CustomerAddressListReturn();
            List<CustomerAddressListReturn> listCustomerAddress = new List<CustomerAddressListReturn>();
            CustomerAddressDAO cDAO = new CustomerAddressDAO();
            int i = 0;
            listCustomerAddress = cDAO.ListAddressValidate(cInfo);

            return Ok<List<CustomerAddressListReturn>>(listCustomerAddress);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertCustomerAddress")]
        public IHttpActionResult InsCustomerAddress([FromBody]CustomerAddressInfo cInfo)
        {
            //CustomerAddressListReturn customerAddressList = new CustomerAddressListReturn();
            //List<CustomerAddressListReturn> listCustomerAddress = new List<CustomerAddressListReturn>();
            CustomerAddressDAO cDAO = new CustomerAddressDAO();
            int i = 0;
            i = cDAO.InsertCustomerAddress(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CustomerAddressAPI")]
        public IHttpActionResult InsertCusAddress([FromBody]CustomerAddressInfo cInfo)
        {
            //CustomerAddressListReturn customerAddressList = new CustomerAddressListReturn();
            //List<CustomerAddressListReturn> listCustomerAddress = new List<CustomerAddressListReturn>();
            CustomerAddressDAO cDAO = new CustomerAddressDAO();
            int i = 0;
            i = cDAO.InsertCustomerAddress(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCustomerAddress")]
        public IHttpActionResult UpdCustomerAddress([FromBody]CustomerAddressInfo cInfo)
        {
            //CustomerAddressListReturn customerAddressList = new CustomerAddressListReturn();
            //List<CustomerAddressListReturn> listCustomerAddress = new List<CustomerAddressListReturn>();
            CustomerAddressDAO cDAO = new CustomerAddressDAO();
            int i = 0;
            i = cDAO.UpdateCustomerAddress(cInfo);

            return Ok<int>(i);
        }
     
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCustomerAddressLatLng")]
        public IHttpActionResult UpdCustomerAddressLatLng([FromBody]CustomerAddressInfo cInfo)
        {
            CustomerAddressDAO cDAO = new CustomerAddressDAO();
            int i = 0;
            i = cDAO.UpdateCustomerAddressLatLng(cInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateFlagDefaultCustomerAddress")]
        public IHttpActionResult UpdateFlagDefaultCustomerAddress([FromBody] CustomerAddressInfo cInfo)
        {
            CustomerAddressDAO cDAO = new CustomerAddressDAO();
            int i = 0;
            i = cDAO.UpdateFlagDefaultCustomerAddress(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountCustomerAddressListByCriteria")]
        public IHttpActionResult CountCusAddrListByCriteria([FromBody]CustomerAddressInfo cInfo)
        {
            //CustomerAddressListReturn customerAddressList = new CustomerAddressListReturn();
            //List<CustomerAddressListReturn> listCustomerAddress = new List<CustomerAddressListReturn>();
            CustomerAddressDAO cDAO = new CustomerAddressDAO();
            int? i = 0;
            i = cDAO.CountCustomerAddressListByCriteria(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCustomerAddressListByCriteria")]
        public IHttpActionResult ListCusAddrListByCriteria([FromBody]CustomerAddressInfo cInfo)
        {
            CustomerAddressListReturn customerAddressList = new CustomerAddressListReturn();
            List<CustomerAddressListReturn> listCustomerAddress = new List<CustomerAddressListReturn>();
            CustomerAddressDAO cDAO = new CustomerAddressDAO();
            int i = 0;
            listCustomerAddress = cDAO.ListCustomerAddressByCriteria(cInfo);

            return Ok<List<CustomerAddressListReturn>>(listCustomerAddress);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateDateCustomerAddress")]
        public IHttpActionResult UpdDateCustomerAddress([FromBody]CustomerAddressInfo cInfo)
        {
            CustomerAddressDAO cDAO = new CustomerAddressDAO();
            int? i = 0;
            i = cDAO.UpdateDateCustomerAddress(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetLatestUpdatedCustomerAddress")]
        public IHttpActionResult GetLatestUpdatedCustomerAddr([FromBody]CustomerAddressInfo cInfo)
        {
            CustomerAddressListReturn customerAddressList = new CustomerAddressListReturn();
            List<CustomerAddressListReturn> listCustomerAddress = new List<CustomerAddressListReturn>();
            CustomerAddressDAO cDAO = new CustomerAddressDAO();
            int i = 0;
            listCustomerAddress = cDAO.GetLatestUpdatedCustomerAddress(cInfo);

            return Ok<List<CustomerAddressListReturn>>(listCustomerAddress);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListAllCustomerAddressListByCriteria")]
        public IHttpActionResult ListAllCusAddrListByCriteria([FromBody]CustomerAddressInfo cInfo)
        {
            CustomerAddressListReturn customerAddressList = new CustomerAddressListReturn();
            List<CustomerAddressListReturn> listCustomerAddress = new List<CustomerAddressListReturn>();
            CustomerAddressDAO cDAO = new CustomerAddressDAO();
            int i = 0;
            listCustomerAddress = cDAO.ListAllCustomerAddressListByCriteria(cInfo);

            return Ok<List<CustomerAddressListReturn>>(listCustomerAddress);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCustomerAddressByEcommerce")]
        public IHttpActionResult UpdateCustomerAddressByEcommerce([FromBody] CustomerAddressInfo cInfo)
        {
            //CustomerAddressListReturn customerAddressList = new CustomerAddressListReturn();
            //List<CustomerAddressListReturn> listCustomerAddress = new List<CustomerAddressListReturn>();
            CustomerAddressDAO cDAO = new CustomerAddressDAO();
            int i = 0;
            i = cDAO.UpdateCustomerAddressByEcommerce(cInfo);

            return Ok<int>(i);
        }
       

    }
}
