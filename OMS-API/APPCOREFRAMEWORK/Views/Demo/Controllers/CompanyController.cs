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
    public class CompanyController : ApiController
    {

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateCompany")]
        public IHttpActionResult UpdCompany([FromBody]CompanyInfo cInfo)
        {
            //CompanyListReturn companyList = new CompanyListReturn();
            //List<CompanyListReturn> listCompany = new List<CompanyListReturn>();
            CompanyDAO cDAO = new CompanyDAO();
            int i = 0;
            i = cDAO.UpdateCompany(cInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteCompany")]
        public IHttpActionResult DelCompany([FromBody]CompanyInfo cInfo)
        {
            //CompanyListReturn companyList = new CompanyListReturn();
            //List<CompanyListReturn> listCompany = new List<CompanyListReturn>();
            CompanyDAO cDAO = new CompanyDAO();
            int i = 0;
            i = cDAO.DeleteCompany(cInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertCompany")]
        public IHttpActionResult InsCompany([FromBody]CompanyInfo cInfo)
        {
            //CompanyListReturn companyList = new CompanyListReturn();
            //List<CompanyListReturn> listCompany = new List<CompanyListReturn>();
            CompanyDAO cDAO = new CompanyDAO();
            int i = 0;
            i = cDAO.InsertCompany(cInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CompanyListNoPaging")]
        public IHttpActionResult CompanyLNoPaging([FromBody]CompanyInfo cInfo)
        {
            CompanyListReturn companyList = new CompanyListReturn();
            List<CompanyListReturn> listCompany = new List<CompanyListReturn>();
            CompanyDAO cDAO = new CompanyDAO();

            listCompany = cDAO.CompanyListNoPaging(cInfo);

            return Ok<List<CompanyListReturn>>(listCompany);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCompanyPagingByCriteria")]
        public IHttpActionResult ListCompanyPagingByCriteria([FromBody] CompanyInfo cInfo)
        {
            CompanyListReturn companyList = new CompanyListReturn();
            List<CompanyListReturn> listCompany = new List<CompanyListReturn>();
            CompanyDAO cDAO = new CompanyDAO();

            listCompany = cDAO.ListCompanyPagingByCriteria(cInfo);

            return Ok<List<CompanyListReturn>>(listCompany);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountCompanyListPagingByCriteria")]
        public IHttpActionResult CountCompanyListPagingByCriteria([FromBody] CompanyInfo cInfo)
        {
            CompanyDAO cDAO = new CompanyDAO();
            int? i = 0;
            i = cDAO.CountCompanyListPagingByCriteria(cInfo);

            return Ok<int?>(i);
        }
    }
}
