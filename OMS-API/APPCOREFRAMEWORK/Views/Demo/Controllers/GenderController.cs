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
    public class GenderController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListGenderNopagingByCriteria")]
        public IHttpActionResult ListGdNopagingByCriteria([FromBody]GenderInfo gInfo)
        {
            GenderListReturn genderList = new GenderListReturn();
            List<GenderListReturn> listGender = new List<GenderListReturn>();
            GenderDAO cDAO = new GenderDAO();

            listGender = cDAO.ListGenderNopagingByCriteria(gInfo);

            return Ok<List<GenderListReturn>>(listGender);
        }
    }
}
