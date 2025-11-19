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
    public class DepartmentController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountDepartmentByCriteria")]
        public IHttpActionResult CountDmByCriteria([FromBody]DepartmentInfo dInfo)
        {
            
            DepartmentDAO dDAO = new DepartmentDAO();
            int? i = 0;
            i = dDAO.CountDepartmentByCriteria(dInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDepartmentByCriteria")]
        public IHttpActionResult ListDmByCriteria([FromBody]DepartmentInfo dInfo)
        {
            DepartmentListReturn departmentList = new DepartmentListReturn();
            List<DepartmentListReturn> listDepartment = new List<DepartmentListReturn>();
            DepartmentDAO dDAO = new DepartmentDAO();

            listDepartment = dDAO.ListDepartmentByCriteria(dInfo);

            return Ok<List<DepartmentListReturn>>(listDepartment);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertDepartment")]
        public IHttpActionResult InsDepartment([FromBody]DepartmentInfo dInfo)
        {

            DepartmentDAO dDAO = new DepartmentDAO();
            int i = 0;
            i = dDAO.InsertDepartment(dInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteDepartment")]
        public IHttpActionResult DelDepartment([FromBody]DepartmentInfo dInfo)
        {

            DepartmentDAO dDAO = new DepartmentDAO();
            int i = 0;
            i = dDAO.DeleteDepartment(dInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateDepartment")]
        public IHttpActionResult UpdDepartment([FromBody]DepartmentInfo dInfo)
        {

            DepartmentDAO dDAO = new DepartmentDAO();
            int i = 0;
            i = dDAO.UpdateDepartment(dInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ddlDepartment")]
        public IHttpActionResult ddlDpm([FromBody]DepartmentInfo dInfo)
        {
            DepartmentListReturn departmentList = new DepartmentListReturn();
            List<DepartmentListReturn> listDepartment = new List<DepartmentListReturn>();
            DepartmentDAO dDAO = new DepartmentDAO();

            listDepartment = dDAO.ddlDepartment(dInfo);

            return Ok<List<DepartmentListReturn>>(listDepartment);
        }
    }
}
