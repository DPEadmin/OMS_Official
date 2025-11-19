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
    public class PlannerController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdatePlanner")]
        public IHttpActionResult UpdPlanner([FromBody]PlannerInfo pInfo)
        {

            PlannerDAO pDAO = new PlannerDAO();
            int i = 0;
            i = pDAO.UpdatePlanner(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeletePlanner")]
        public IHttpActionResult DelPlanner([FromBody]PlannerInfo pInfo)
        {

            PlannerDAO pDAO = new PlannerDAO();
            int i = 0;
            i = pDAO.DeletePlannerList(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertPlanner")]
        public IHttpActionResult InsPlanner([FromBody]PlannerInfo pInfo)
        {

            PlannerDAO pDAO = new PlannerDAO();
            int i = 0;  
            i = pDAO.InsertPlanner(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPlannerNoPagingByCriteria")]
        public IHttpActionResult ListPmNoPagingByCriteria([FromBody]PlannerInfo pInfo)
        {
            PlannerListReturn PlannerList = new PlannerListReturn();
            List<PlannerListReturn> listPlanner = new List<PlannerListReturn>();
            PlannerDAO pDAO = new PlannerDAO();

            listPlanner = pDAO.ListPlannerNoPagingByCriteria(pInfo);

            return Ok<List<PlannerListReturn>>(listPlanner);
        }

        

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountPlannerListByCriteria")]
        public IHttpActionResult CountPromoListByCriteria([FromBody]PlannerInfo pInfo)
        {
            PlannerDAO pDAO = new PlannerDAO();
            int? i = 0;
            i = pDAO.CountPlannerListByCriteria(pInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListPlanner")]
        public IHttpActionResult CountListPlanner([FromBody]PlannerInfo pInfo)
        {
            PlannerDAO pDAO = new PlannerDAO();
            int? i = 0;
            i = pDAO.CountListPlanner(pInfo);

            return Ok<int?>(i);
        }

    }
}
