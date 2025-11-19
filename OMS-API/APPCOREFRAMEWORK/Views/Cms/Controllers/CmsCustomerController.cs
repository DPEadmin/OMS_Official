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

namespace APPCOREVIEW.Views.Cms.Controllers
{
    public class CmsCustomerController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/AuthenCustomerByCriteria")]
        public IHttpActionResult AuthenCustomerByCriteria([FromBody]CustomerInfo cInfo)
        {
            CustomerListReturn customerList = new CustomerListReturn();
            List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CustomerDAO cDAO = new CustomerDAO();

            listCustomer = cDAO.AuthenCustomerByCriteria(cInfo);

            return Ok<List<CustomerListReturn>>(listCustomer);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertCustomer")]
        public IHttpActionResult InsCustomer([FromBody]CustomerInfo cInfo)
        {
            
            CustomerDAO cDAO = new CustomerDAO();
            int i = 0;
            i = cDAO.InsertCustomer(cInfo);

            return Ok<int>(i);
        }

    }
}
