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
    public class RoleController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountRoleByCriteria")]
        public IHttpActionResult CntRoleByCriteria([FromBody]RoleInfo rInfo)
        {

            RoleDAO rDAO = new RoleDAO();
            int? i = 0;
            i = rDAO.CountRoleByCriteria(rInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRoleByCriteria")]
        public IHttpActionResult LstRoleByCriteria([FromBody]RoleInfo rInfo)
        {
            RoleListReturn roleList = new RoleListReturn();
            List<RoleListReturn> listRole = new List<RoleListReturn>();
            RoleDAO rDAO = new RoleDAO();

            listRole = rDAO.ListRoleByCriteria(rInfo);

            return Ok<List<RoleListReturn>>(listRole);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRoleValidateInsert")]
        public IHttpActionResult ListRoleValidateInsert([FromBody]RoleInfo rInfo)
        {
            RoleListReturn roleList = new RoleListReturn();
            List<RoleListReturn> listRole = new List<RoleListReturn>();
            RoleDAO rDAO = new RoleDAO();

            listRole = rDAO.ListRoleValidateInsert(rInfo);

            return Ok<List<RoleListReturn>>(listRole);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertRole")]
        public IHttpActionResult InsRole([FromBody]RoleInfo rInfo)
        {
            RoleDAO rDAO = new RoleDAO();
            int i = 0;
            i = rDAO.InsertRole(rInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateRole")]
        public IHttpActionResult UpdRole([FromBody]RoleInfo rInfo)
        {
            RoleDAO rDAO = new RoleDAO();
            int i = 0;
            i = rDAO.UpdateRole(rInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteRole")]
        public IHttpActionResult DelRole([FromBody]RoleInfo rInfo)
        {
            RoleDAO rDAO = new RoleDAO();
            int i = 0;
            i = rDAO.DeleteRole(rInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteRoleList")]
        public IHttpActionResult DelRoleList([FromBody]RoleInfo rInfo)
        {
            RoleDAO rDAO = new RoleDAO();
            int i = 0;
            i = rDAO.DeleteRoleList(rInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ddlRole")]
        public IHttpActionResult DdlRole()
        {
            RoleListReturn roleList = new RoleListReturn();
            List<RoleListReturn> listRole = new List<RoleListReturn>();
            RoleDAO rDAO = new RoleDAO();

            listRole = rDAO.ddlRole();

            return Ok<List<RoleListReturn>>(listRole);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ddlRoleByDepartmentCode")]
        public IHttpActionResult ddlRoleByDpmCode([FromBody]RoleInfo rInfo)
        {
            RoleListReturn roleList = new RoleListReturn();
            List<RoleListReturn> listRole = new List<RoleListReturn>();
            RoleDAO rDAO = new RoleDAO();

            listRole = rDAO.ddlRoleByDepartmentCode(rInfo);

            return Ok<List<RoleListReturn>>(listRole);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRoleFillfillByCriteria")]
        public IHttpActionResult LstRolefullfillByCriteria([FromBody]RoleInfo rInfo)
        {
            RoleListReturn roleList = new RoleListReturn();
            List<RoleListReturn> listRole = new List<RoleListReturn>();
            RoleDAO rDAO = new RoleDAO();

            listRole = rDAO.ListRoleFullfillByCriteria(rInfo);

            return Ok<List<RoleListReturn>>(listRole);
        }

    }
}
