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
    public class ContactHistoryController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListContactByCriteria")]
        public IHttpActionResult ListCpNoPagingByCriteria([FromBody]ContactHistoryInfo cInfo)
        {
            ContactHistoryListReturn campaignList = new ContactHistoryListReturn();
            List<ContactHistoryListReturn> listCampaign = new List<ContactHistoryListReturn>();
            ContactHistoryDAO cDAO = new ContactHistoryDAO();

            listCampaign = cDAO.ListContactByCriteria(cInfo);

            return Ok<List<ContactHistoryListReturn>>(listCampaign);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListContactPagingByCriteria")]
        public IHttpActionResult ListContactPagingByCriteria([FromBody]ContactHistoryInfo cInfo)
        {
            ContactHistoryListReturn campaignList = new ContactHistoryListReturn();
            List<ContactHistoryListReturn> listCampaign = new List<ContactHistoryListReturn>();
            ContactHistoryDAO cDAO = new ContactHistoryDAO();

            listCampaign = cDAO.ListContactPagingByCriteria(cInfo);

            return Ok<List<ContactHistoryListReturn>>(listCampaign);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListContactPagingByCriteria")]
        public IHttpActionResult CountListContactPagingByCriteria([FromBody]ContactHistoryInfo cInfo)
        {
            ContactHistoryDAO cDAO = new ContactHistoryDAO();
            int? i = 0;
            i = cDAO.CountListContactPagingByCriteria(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertContactHis")]
        public IHttpActionResult InsCampaign([FromBody]ContactHistoryInfo cInfo)
        {
            //ContactHistoryListReturn campaignList = new ContactHistoryListReturn();
            //List<ContactHistoryListReturn> listCampaign = new List<ContactHistoryListReturn>();
            ContactHistoryDAO cDAO = new ContactHistoryDAO();
            int i = 0;
            i = cDAO.InsertContactHis(cInfo);

            return Ok<int>(i);
        }
    }
}
