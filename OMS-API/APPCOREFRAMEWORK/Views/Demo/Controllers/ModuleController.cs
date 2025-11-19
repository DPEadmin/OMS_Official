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
    public class ModuleController : ApiController
    {

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateModule")]
        public IHttpActionResult UpdModule([FromBody]ModuleInfo mInfo)
        {
           
            ModuleDAO mDAO = new ModuleDAO();
            int i = 0;
            i = mDAO.UpdateModule(mInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListModuleNopagingByCriteria")]
        public IHttpActionResult ListMdNopagingByCriteria([FromBody]ModuleInfo mInfo)
        {
            ModuleListReturn moduleList = new ModuleListReturn();
            List<ModuleListReturn> listModule = new List<ModuleListReturn>();
            ModuleDAO mDao = new ModuleDAO();

            listModule = mDao.ListModuleNopagingByCriteria(mInfo);

            return Ok<List<ModuleListReturn>>(listModule);
        }
    }
}
