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
    public class LandmarkController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteLandmark")]
        public IHttpActionResult DelLandmark([FromBody]LandmarkInfo cInfo)
        {

            LandmarkDAO cDAO = new LandmarkDAO();
            int i = 0;
            i = cDAO.DeleteLandmark(cInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateLandmark")]
        public IHttpActionResult UpdLandmark([FromBody]LandmarkInfo cInfo)
        {

            LandmarkDAO cDAO = new LandmarkDAO();
            int i = 0;
            i = cDAO.UpdateLandmark(cInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertLandmark")]
        public IHttpActionResult InsLandmark([FromBody]LandmarkInfo cInfo)
        {

            LandmarkDAO cDAO = new LandmarkDAO();
            int i = 0;
            i = cDAO.InsertLandmark(cInfo);

            return Ok<int>(i);
        }
    }
}
