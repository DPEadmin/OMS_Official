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
    public class DriverController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDriverByCriteriaNoPaging")]
        public IHttpActionResult ListDvNpByCriteria_showgv([FromBody]DriverInfo dInfo)
        {
            DriverListReturn driverList = new DriverListReturn();
            List<DriverListReturn> listDriver = new List<DriverListReturn>();
            DriverDAO dDAO = new DriverDAO();

            listDriver = dDAO.ListDriverByCriteriaNoPaging(dInfo);

            return Ok<List<DriverListReturn>>(listDriver);
        }




        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDriverByCriteria")]
        public IHttpActionResult ListDvByCriteria([FromBody]DriverInfo dInfo)
        {
            DriverListReturn driverList = new DriverListReturn();
            List<DriverListReturn> listDriver = new List<DriverListReturn>();
            DriverDAO dDAO = new DriverDAO();

            listDriver = dDAO.ListDriverByCriteria(dInfo);

            return Ok<List<DriverListReturn>>(listDriver);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDriverDetailByCriteria")]
        public IHttpActionResult ListDvDetailByCriteria([FromBody]CustomerPhoneInfo dInfo)
        {
            DriverListReturn driverList = new DriverListReturn();
            List<DriverListReturn> listDriver = new List<DriverListReturn>();
            DriverDAO dDAO = new DriverDAO();

            listDriver = dDAO.ListDriverDetailByCriteria(dInfo);

            return Ok<List<DriverListReturn>>(listDriver);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDriverByCriteria_showgv")]
        public IHttpActionResult ListDvByCriteria_showgv([FromBody]DriverInfo dInfo)
        {
            DriverListReturn driverList = new DriverListReturn();
            List<DriverListReturn> listDriver = new List<DriverListReturn>();
            DriverDAO dDAO = new DriverDAO();

            listDriver = dDAO.ListDriverByCriteria_showgv(dInfo);

            return Ok<List<DriverListReturn>>(listDriver);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountDriverListByCriteria")]
        public IHttpActionResult CountDvListByCriteria([FromBody]DriverInfo dInfo)
        {
            //CampaignListReturn campaignList = new CampaignListReturn();
            //List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            DriverDAO dDAO = new DriverDAO();
            int? i = 0;
            i = dDAO.CountDriverListByCriteria(dInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertDriver")]
        public IHttpActionResult InsDriver([FromBody]DriverInfo dInfo)
        {
            //CampaignListReturn campaignList = new CampaignListReturn();
            //List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            DriverDAO dDAO = new DriverDAO();
            int i = 0;
            i = dDAO.InsertDriver(dInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateDriver")]
        public IHttpActionResult UpdDriver([FromBody]DriverInfo dInfo)
        {
            //CampaignListReturn campaignList = new CampaignListReturn();
            //List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            DriverDAO dDAO = new DriverDAO();
            int? i = 0;
            i = dDAO.UpdateDriver(dInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteDriver")]
        public IHttpActionResult DelDriver([FromBody]DriverInfo dInfo)
        {
           
            DriverDAO dDAO = new DriverDAO();
            int i = 0;
            i = dDAO.DeleteDriver(dInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DriverCheck")]
        public IHttpActionResult DriverChk([FromBody]DriverInfo dInfo)
        {
            DriverListReturn driverList = new DriverListReturn();
            List<DriverListReturn> listDriver = new List<DriverListReturn>();
            DriverDAO dDAO = new DriverDAO();

            listDriver = dDAO.DriverCheck(dInfo);

            return Ok<List<DriverListReturn>>(listDriver);
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
     
    }
}
