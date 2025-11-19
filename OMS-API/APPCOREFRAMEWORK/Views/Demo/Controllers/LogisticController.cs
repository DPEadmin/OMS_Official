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
    public class LogisticController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListLogisticNopagingByCriteria")]
        public IHttpActionResult ListLogisNobPagBuild([FromBody]LogisticInfo pInfo)
        {
            LogisticListReturn logisticList = new LogisticListReturn();
            List<LogisticListReturn> listLogistic = new List<LogisticListReturn>();
            LogisticDAO lDAO = new LogisticDAO();
            listLogistic = lDAO.ListLogisticNopagingByCriteria(pInfo);

            return Ok<List<LogisticListReturn>>(listLogistic);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/LogisticFeeCalculate")]
        public IHttpActionResult LogisticFeeCal([FromBody]LogisticInfo pInfo)
        {
            LogisticListReturn logisticList = new LogisticListReturn();
            List<LogisticListReturn> listLogistic = new List<LogisticListReturn>();
            LogisticDAO lDAO = new LogisticDAO();
            if (pInfo.Weight >= 20)
            {
                pInfo.LogisticCode = "01";
                pInfo.Weight = 0;
                listLogistic = lDAO.LogisticFeeCalculate(pInfo);
            }
            else
            {
                pInfo.LogisticCode = "02";
                pInfo.Weight = 0;
                listLogistic = lDAO.LogisticFeeCalculate(pInfo);
            }
            return Ok<List<LogisticListReturn>>(listLogistic);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateLogistic")]
        public IHttpActionResult UpdLogistic([FromBody]LogisticInfo cInfo)
        {
            //ChannelListReturn campaignList = new ChannelListReturn();
            //List<ChannelListReturn> listCampaign = new List<ChannelListReturn>();
            LogisticDAO cDAO = new LogisticDAO();
            int i = 0;
            i = cDAO.UpdateLogistic(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteLogistic")]
        public IHttpActionResult DelLogistic([FromBody]LogisticInfo cInfo)
        {
            //ChannelListReturn campaignList = new ChannelListReturn();
            //List<ChannelListReturn> listCampaign = new List<ChannelListReturn>();
            LogisticDAO cDAO = new LogisticDAO();
            int i = 0;
            i = cDAO.DeleteLogistic(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertLogistic")]
        public IHttpActionResult InsLogistic([FromBody]LogisticInfo cInfo)
        {

            LogisticDAO cDAO = new LogisticDAO();
            int i = 0;
            i = cDAO.InsertLogistic(cInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountLogisticListByCriteria")]
        public IHttpActionResult CountCpListByCriteria([FromBody]LogisticInfo cInfo)
        {
            //ChannelListReturn campaignList = new ChannelListReturn();
            //List<ChannelListReturn> listCampaign = new List<ChannelListReturn>();
            LogisticDAO cDAO = new LogisticDAO();
            int? i = 0;
            i = cDAO.CountLogisticListByCriteria(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListLogisticPagingByCriteria")]
        public IHttpActionResult CpListByCriteria([FromBody]LogisticInfo cInfo)
        {
            LogisticListReturn campaignList = new LogisticListReturn();
            List<LogisticListReturn> listCampaign = new List<LogisticListReturn>();
            LogisticDAO cDAO = new LogisticDAO();

            listCampaign = cDAO.ListLogisticPagingByCriteria(cInfo);

            return Ok<List<LogisticListReturn>>(listCampaign);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertLogisticDetail")]
        public IHttpActionResult InsLogisticDetail([FromBody]LogisticDetailInfo cInfo)
        {

            LogisticDAO cDAO = new LogisticDAO();
            int i = 0;
            i = cDAO.InsertLogisticDetail(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListLogisticDetailPagingByCriteria")]
        public IHttpActionResult ListLogisDetail([FromBody]LogisticDetailInfo pInfo)
        {
            LogisticListDetailReturn logisticList = new LogisticListDetailReturn();
            List<LogisticListDetailReturn> listLogistic = new List<LogisticListDetailReturn>();
            LogisticDAO lDAO = new LogisticDAO();
            listLogistic = lDAO.ListLogisticDetailPagingByCriteria(pInfo);

            return Ok<List<LogisticListDetailReturn>>(listLogistic);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateLogisticDetail")]
        public IHttpActionResult UpdLogisticDetailt2([FromBody]LogisticDetailInfo cInfo)
        {
            //ChannelListReturn campaignList = new ChannelListReturn();
            //List<ChannelListReturn> listCampaign = new List<ChannelListReturn>();
            LogisticDAO cDAO = new LogisticDAO();
            int i = 0;
            i = cDAO.UpdateLogisticDetail (cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteLogisticDetail")]
        public IHttpActionResult DelLogisticDetail([FromBody]LogisticDetailInfo cInfo)
        {
            //ChannelListReturn campaignList = new ChannelListReturn();
            //List<ChannelListReturn> listCampaign = new List<ChannelListReturn>();
            LogisticDAO cDAO = new LogisticDAO();
            int i = 0;
            i = cDAO.DeleteLogisticDetail(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListLogisticCodeValidateCriteria")]
        public IHttpActionResult ListLogisticCodeValidateCriteria([FromBody]LogisticInfo pInfo)
        {
            LogisticListReturn logisticList = new LogisticListReturn();
            List<LogisticListReturn> listLogistic = new List<LogisticListReturn>();
            LogisticDAO lDAO = new LogisticDAO();
            listLogistic = lDAO.ListLogisticCodeValidateCriteria(pInfo);

            return Ok<List<LogisticListReturn>>(listLogistic);
        }

    }
}
