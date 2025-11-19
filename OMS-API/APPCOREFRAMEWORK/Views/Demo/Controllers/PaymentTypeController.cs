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
    public class PaymentTypeController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPaymentTypeNopagingByCriteria")]
        public IHttpActionResult ListPayTypeNopagingByCriteria([FromBody]PaymentTypeInfo cInfo)
        {
            PaymentTypeListReturn paymentTypeList = new PaymentTypeListReturn();
            List<PaymentTypeListReturn> listPaymentType = new List<PaymentTypeListReturn>();
            PaymentTypeDAO ptDAO = new PaymentTypeDAO();

            listPaymentType = ptDAO.ListPaymentTypeNopagingByCriteria(cInfo);

            return Ok<List<PaymentTypeListReturn>>(listPaymentType);
        }
    }
}
