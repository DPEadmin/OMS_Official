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
    public class ComplementaryController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertComplementary")]
        public IHttpActionResult InsComplementary([FromBody]ComplementaryInfo cpmInfo)
        {
            //ComplementaryListReturn complementaryList = new ComplementaryListReturn();
            //List<ComplementaryListReturn> listComplementary = new List<ComplementaryListReturn>();
            ComplementaryDAO cDAO = new ComplementaryDAO();
            int i = 0;
            i = cDAO.InsertComplementary(cpmInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListComplementaryByCriteria")]
        public IHttpActionResult ListCpmByCriteria([FromBody]ComplementaryInfo cpmInfo)
        {
            ComplementaryListReturn complementaryList = new ComplementaryListReturn();
            List<ComplementaryListReturn> listComplementary = new List<ComplementaryListReturn>();
            ComplementaryDAO cDAO = new ComplementaryDAO();
            listComplementary = cDAO.ListComplementaryByCriteria(cpmInfo);

            return Ok<List<ComplementaryListReturn>>(listComplementary);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListComplementarynopagingByCriteria")]
        public IHttpActionResult ListCpmNoPagingByCriteria([FromBody]ComplementaryInfo cpmInfo)
        {
            ComplementaryListReturn complementaryList = new ComplementaryListReturn();
            List<ComplementaryListReturn> listComplementary = new List<ComplementaryListReturn>();
            ComplementaryDAO cDAO = new ComplementaryDAO();
            listComplementary = cDAO.ListComplementarynopagingByCriteria(cpmInfo);

            return Ok<List<ComplementaryListReturn>>(listComplementary);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountComplementaryListByCriteria")]
        public IHttpActionResult CountCpmListByCriteria([FromBody]ComplementaryInfo cpmInfo)
        {
            //ComplementaryListReturn complementaryList = new ComplementaryListReturn();
            //List<ComplementaryListReturn> listComplementary = new List<ComplementaryListReturn>();
            ComplementaryDAO cDAO = new ComplementaryDAO();
            int? i = 0;
            i = cDAO.CountComplementaryListByCriteria(cpmInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateComplementary")]
        public IHttpActionResult UpdComplementary([FromBody]ComplementaryInfo cpmInfo)
        {
            //ComplementaryListReturn complementaryList = new ComplementaryListReturn();
            //List<ComplementaryListReturn> listComplementary = new List<ComplementaryListReturn>();
            ComplementaryDAO cDAO = new ComplementaryDAO();
            int i = 0;
            i = cDAO.UpdateComplementary(cpmInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteComplementary")]
        public IHttpActionResult DelComplementary([FromBody]ComplementaryInfo cpmInfo)
        {
            //ComplementaryListReturn complementaryList = new ComplementaryListReturn();
            //List<ComplementaryListReturn> listComplementary = new List<ComplementaryListReturn>();
            ComplementaryDAO cDAO = new ComplementaryDAO();
            int i = 0;
            i = cDAO.DeleteComplementary(cpmInfo);

            return Ok<int>(i);
        }
    }
}
