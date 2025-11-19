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
    public class BranchController : ApiController
    {

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListBranchByCriteria")]
        public IHttpActionResult ListAddrByCriteria([FromBody]BranchInfo bInfo)
        {
            List<BranchListReturn> listBranch = new List<BranchListReturn>();
            BranchDAO bDAO = new BranchDAO();

            listBranch = bDAO.ListAddressByCriteria(bInfo);

            return Ok<List<BranchListReturn>>(listBranch);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListAreaByCriteria")]
        public IHttpActionResult ListAreaByCriteria([FromBody]AreaInfo aInfo)
        {
            List<AreaListReturn> listArea = new List<AreaListReturn>();
            BranchDAO bDAO = new BranchDAO();

            listArea = bDAO.ListAreaByCriteria(aInfo);

            return Ok<List<AreaListReturn>>(listArea);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/BranchListNoPagingCriteria")]
        public IHttpActionResult BranchListNoPagingCriteria([FromBody]BranchInfo bInfo)
        {
            List<BranchListReturn> listBranch = new List<BranchListReturn>();
            BranchDAO bDAO = new BranchDAO();

            listBranch = bDAO.BranchListNoPagingCriteria(bInfo);

            return Ok<List<BranchListReturn>>(listBranch);
        }
    }
}
