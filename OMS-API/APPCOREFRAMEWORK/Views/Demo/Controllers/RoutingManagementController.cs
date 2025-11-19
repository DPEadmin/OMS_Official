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
    public class RoutingManagementController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRoutingNoPagingByCriteria")]
        public IHttpActionResult ListRtByCriteriaNoPaging([FromBody]RoutingInfo dInfo)
        {
            RoutingListReturn RoutingList = new RoutingListReturn();
            List<RoutingListReturn> listRouting = new List<RoutingListReturn>();
            RoutingDAO dDAO = new RoutingDAO();

            listRouting = dDAO.ListRoutingNoPagingByCriteria(dInfo);

            return Ok<List<RoutingListReturn>>(listRouting);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRoutingByCriteria_showgv")]
        public IHttpActionResult ListRtByCriteria_showgv([FromBody]RoutingInfo dInfo)
        {
            RoutingListReturn RoutingList = new RoutingListReturn();
            List<RoutingListReturn> listRouting = new List<RoutingListReturn>();
            RoutingDAO dDAO = new RoutingDAO();

            listRouting = dDAO.ListRoutingByCriteria_showgv(dInfo);

            return Ok<List<RoutingListReturn>>(listRouting);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountRoutingListByCriteria")]
        public IHttpActionResult CountDvListByCriteria([FromBody]RoutingInfo dInfo)
        {
            RoutingDAO dDAO = new RoutingDAO();
            int? i = 0;
            i = dDAO.CountRoutingListByCriteria(dInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertRouting")]
        public IHttpActionResult InsRouting([FromBody]RoutingInfo dInfo)
        {
            //CampaignListReturn campaignList = new CampaignListReturn();
            //List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            RoutingDAO dDAO = new RoutingDAO();
            int i = 0;
            i = dDAO.InsertRouting(dInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateRouting")]
        public IHttpActionResult UpdRouting([FromBody]RoutingInfo dInfo)
        {

            RoutingDAO dDAO = new RoutingDAO();
            int? i = 0;
            i = dDAO.UpdateRouting(dInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteRouting")]
        public IHttpActionResult DelDriver([FromBody]RoutingInfo dInfo)
        {
           
            RoutingDAO dDAO = new RoutingDAO();
            int i = 0;
            i = dDAO.DeleteRouting(dInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/RoutingCheck")]
        public IHttpActionResult RoutingChk([FromBody]RoutingInfo dInfo)
        {
            RoutingListReturn RoutingList = new RoutingListReturn();
            List<RoutingListReturn> listRouting = new List<RoutingListReturn>();
            RoutingDAO dDAO = new RoutingDAO();

            listRouting = dDAO.RoutingCheck(dInfo);

            return Ok<List<RoutingListReturn>>(listRouting);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDriverByCriteria_dll")]
        public IHttpActionResult ListDvByCriteria_dll([FromBody]DriverInfo dInfo)
        {
            DriverListReturn driverList = new DriverListReturn();
            List<DriverListReturn> listDriver = new List<DriverListReturn>();
            DriverDAO dDAO = new DriverDAO();

            listDriver = dDAO.ListDriverByCriteria_dll(dInfo);

            return Ok<List<DriverListReturn>>(listDriver);
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRoutingDetailByCriteria_showgv")]
        public IHttpActionResult ListRtdByCriteria_showgv([FromBody]RoutingDetailInfo dInfo)
        {
            RoutingDetailListReturn RoutingList = new RoutingDetailListReturn();
            List<RoutingDetailListReturn> listRouting = new List<RoutingDetailListReturn>();
            RoutingDAO dDAO = new RoutingDAO();

            listRouting = dDAO.ListRoutingDetailByCriteria_showgv(dInfo);

            return Ok<List<RoutingDetailListReturn>>(listRouting);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountRoutingDetailListByCriteria")]
        public IHttpActionResult CountRtdListByCriteria([FromBody]RoutingDetailInfo dInfo)
        {
            RoutingDAO dDAO = new RoutingDAO();
            int? i = 0;
            i = dDAO.CountRoutingDetailListByCriteria(dInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertRoutingDetail")]
        public IHttpActionResult InsRoutingDetail([FromBody]RoutingDetailInfo dInfo)
        {
          
            RoutingDAO dDAO = new RoutingDAO();
            int i = 0;
            i = dDAO.InsertRoutingDetail(dInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateRoutingDetail")]
        public IHttpActionResult UpdRoutingDetail([FromBody]RoutingDetailInfo dInfo)
        {

            RoutingDAO dDAO = new RoutingDAO();
            int? i = 0;
            i = dDAO.UpdateRoutingDetail(dInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteRoutingDetail")]
        public IHttpActionResult DelDriverDetail([FromBody]RoutingDetailInfo dInfo)
        {

            RoutingDAO dDAO = new RoutingDAO();
            int i = 0;
            i = dDAO.DeleteRoutingDetail(dInfo);

            return Ok<int>(i);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/RoutingDetailCheck")]
        public IHttpActionResult RoutingDetailChk([FromBody]RoutingDetailInfo dInfo)
        {
            RoutingDetailListReturn RoutingList = new RoutingDetailListReturn();
            List<RoutingDetailListReturn> listRouting = new List<RoutingDetailListReturn>();
            RoutingDAO dDAO = new RoutingDAO();

            listRouting = dDAO.RoutingDetailCheck(dInfo);

            return Ok<List<RoutingDetailListReturn>>(listRouting);
        }


    }
}
