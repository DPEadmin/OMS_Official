using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using APPCOREMODEL.OMSDAO;
using APPCOREMODEL.Datas.OMSDTO;

namespace APPCOREVIEW.Views.Demo.Controllers
{
    public class CallInformationController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCallinformationInfoByCriteria")]
        public IHttpActionResult ListCallinformationInfoByCriteria([FromBody]CallInformationInfo cInfo)
        {
            List<CallInformationListReturn> listCallInformation = new List<CallInformationListReturn>();
            CallInformationDAO cDAO = new CallInformationDAO();

            listCallInformation = cDAO.ListCallinformationInfoByCriteria(cInfo);

            return Ok<List<CallInformationListReturn>>(listCallInformation);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListNewCallinformationInfoByCriteria")]
        public IHttpActionResult ListNewCallinformationInfoByCriteria([FromBody] CallInformationInfo cInfo)
        {
            List<CallInformationListReturn> listCallInformation = new List<CallInformationListReturn>();
            CallInformationDAO cDAO = new CallInformationDAO();

            listCallInformation = cDAO.ListNewCallinformationInfoByCriteria(cInfo);

            return Ok<List<CallInformationListReturn>>(listCallInformation);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertCallInformation")]
        public IHttpActionResult InsCustomer([FromBody]CallInformationInfo cInfo)
        {
            //CustomerListReturn customerList = new CustomerListReturn();
            //List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            CallInformationDAO cDAO = new CallInformationDAO();
            int i = 0;
            i = cDAO.InsertCallInformation(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCustomerCode")]
        public IHttpActionResult UpdateCustomerCode([FromBody]CallInformationInfo cInfo)
        {
            CallInformationDAO cDAO = new CallInformationDAO();
            int? i = 0;
            i = cDAO.UpdateCustomerCode(cInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCONTACTSTATUS")]
        public IHttpActionResult UpdateCONTACTSTATUS([FromBody] CallInformationInfo cInfo)
        {
            CallInformationDAO cDAO = new CallInformationDAO();
            int? i = 0;
            i = cDAO.UpdateCONTACTSTATUS(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCustomerCodeCallInValidate")]
        public IHttpActionResult ListCustomerCodeCallInValidate([FromBody]CallInformationInfo cInfo)
        {
            List<CallInformationListReturn> listCallInfo = new List<CallInformationListReturn>();
            CallInformationDAO iDAO = new CallInformationDAO();

            listCallInfo = iDAO.ListCustomerCodeCallInValidate(cInfo);

            return Ok<List<CallInformationListReturn>>(listCallInfo);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCallinformationInfoByCriteria_Manual")]
        public IHttpActionResult ListCallinformationInfoByCriteria_Manual([FromBody] CallInformationInfo cInfo)
        {
            List<CallInformationListReturn> listCallInformation = new List<CallInformationListReturn>();
            CallInformationDAO cDAO = new CallInformationDAO();

            listCallInformation = cDAO.ListCallinformationInfoByCriteria_Manual(cInfo);

            return Ok<List<CallInformationListReturn>>(listCallInformation);
        }
    }
}