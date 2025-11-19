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
    public class ChannelController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateChannel")]
        public IHttpActionResult UpdCampaign([FromBody]ChannelInfo cInfo)
        {
            //ChannelListReturn campaignList = new ChannelListReturn();
            //List<ChannelListReturn> listCampaign = new List<ChannelListReturn>();
            ChannelDAO cDAO = new ChannelDAO();
            int i = 0;
            i = cDAO.UpdateChannel(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteChannel")]
        public IHttpActionResult DelCampaign([FromBody]ChannelInfo cInfo)
        {
            //ChannelListReturn campaignList = new ChannelListReturn();
            //List<ChannelListReturn> listCampaign = new List<ChannelListReturn>();
            ChannelDAO cDAO = new ChannelDAO();
            int i = 0;
            i = cDAO.DeleteChannel(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertChannel")]
        public IHttpActionResult InsCampaign([FromBody]ChannelInfo cInfo)
        {
        
            ChannelDAO cDAO = new ChannelDAO();
            int i = 0;
            i = cDAO.InsertChannel(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListChannelNoPagingByCriteria")]
        public IHttpActionResult ListCpNoPagingByCriteria([FromBody]ChannelInfo cInfo)
        {
            ChannelListReturn campaignList = new ChannelListReturn();
            List<ChannelListReturn> listCampaign = new List<ChannelListReturn>();
            ChannelDAO cDAO = new ChannelDAO();

            listCampaign = cDAO.ListChannelNoPagingByCriteria(cInfo);

            return Ok<List<ChannelListReturn>>(listCampaign);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountChannelListByCriteria")]
        public IHttpActionResult CountCpListByCriteria([FromBody]ChannelInfo cInfo)
        {
            //ChannelListReturn campaignList = new ChannelListReturn();
            //List<ChannelListReturn> listCampaign = new List<ChannelListReturn>();
            ChannelDAO cDAO = new ChannelDAO();
            int? i = 0;
            i = cDAO.CountChannelListByCriteria(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListChannelPagingByCriteria")]
        public IHttpActionResult CpListByCriteria([FromBody]ChannelInfo cInfo)
        {
            ChannelListReturn campaignList = new ChannelListReturn();
            List<ChannelListReturn> listCampaign = new List<ChannelListReturn>();
            ChannelDAO cDAO = new ChannelDAO();

            listCampaign = cDAO.ListChannelPagingByCriteria(cInfo);

            return Ok<List<ChannelListReturn>>(listCampaign);
        }
        //[AllowAnonymous]
        //[HttpPost]
        //[Route("api/support/InsertChannelImport")]
        //public IHttpActionResult InsertCampImport([FromBody]L_CampaignCategorydata lcampaigndata)
        //{
        //    ChannelDAO cDAO = new ChannelDAO();
        //    int sum = 0;

        //    sum = cDAO.InsertCampaignImport(lcampaigndata.ChannelInfo.ToList());

        //    return Ok<int>(sum);
        //}
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ChannelListNoPagingCriteria")]
        public IHttpActionResult channalListNoPaging([FromBody]ChannelInfo cInfo)
        {
            ChannelListReturn channalReturn = new ChannelListReturn();
            List<ChannelListReturn> lChannalListReturn = new List<ChannelListReturn>();
            ChannelDAO cDAO = new ChannelDAO();

            lChannalListReturn = cDAO.ChannelListNoPagingCriteria(cInfo);

            return Ok<List<ChannelListReturn>>(lChannalListReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/Swaggertest")]
        public IHttpActionResult channalListNoPaging()
        {
            swaggerInfo channalReturn = new swaggerInfo();
            List<swaggerInfo> lChannalListReturn = new List<swaggerInfo>();
            ChannelDAO cDAO = new ChannelDAO();

            lChannalListReturn = cDAO.ChannelListNoPagingCriteriaSwagger();

            return Ok<List<swaggerInfo>>(lChannalListReturn);
        }

        // GET api/values
        [AllowAnonymous]
        [HttpGet]
        [Route("api/support/SwaggertestGet")]
        public IHttpActionResult Get()
        {
            swaggerInfo channalReturn = new swaggerInfo();
            List<swaggerInfo> lChannalListReturn = new List<swaggerInfo>();
            ChannelDAO cDAO = new ChannelDAO();

            lChannalListReturn = cDAO.ChannelListNoPagingCriteriaSwagger();

            return Ok<List<swaggerInfo>>(lChannalListReturn);
        }

    }
}
