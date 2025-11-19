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
    public class OccupationController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOccupationNopagingByCriteria")]
        public IHttpActionResult ListOccupNopagingByCriteria([FromBody]OccupationInfo oInfo)
        {
            OccupationListReturn occupationList = new OccupationListReturn();
            List<OccupationListReturn> listOccupation = new List<OccupationListReturn>();
            OccupationDAO oDAO = new OccupationDAO();

            listOccupation = oDAO.ListOccupationNopagingByCriteria(oInfo);

            return Ok<List<OccupationListReturn>>(listOccupation);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOccupationPagingByCriteria")]
        public IHttpActionResult ListOccupPagingByCriteria([FromBody]OccupationInfo oInfo)
        {
            OccupationListReturn occupationList = new OccupationListReturn();
            List<OccupationListReturn> listOccupation = new List<OccupationListReturn>();
            OccupationDAO oDAO = new OccupationDAO();

            listOccupation = oDAO.ListOccupationPagingByCriteria(oInfo);

            return Ok<List<OccupationListReturn>>(listOccupation);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountOccupationMasterListByCriteria")]
        public IHttpActionResult CountOccupationMasterbyCriteria([FromBody]OccupationInfo oInfo)
        {
            OccupationListReturn occupationListReturn = new OccupationListReturn();
            int? i = 0;
            OccupationDAO oDAO = new OccupationDAO();
            i = oDAO.CountOccupationMasterListByCriteria(oInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/OccupationCodeValidateInsert")]
        public IHttpActionResult ValidateOccupationCode([FromBody]OccupationInfo oInfo)
        {
            OccupationListReturn occupationReturn = new OccupationListReturn();
            List<OccupationListReturn> listoccupationReturn = new List<OccupationListReturn>();
            OccupationDAO oDAO = new OccupationDAO();
            listoccupationReturn = oDAO.OccupationCodeValidateInsert(oInfo);

            return Ok<List<OccupationListReturn>>(listoccupationReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertOccupation")]
        public IHttpActionResult InsertOccupationList([FromBody]OccupationInfo oInfo)
        {
            OccupationListReturn productReturn = new OccupationListReturn();
            int? i = 0;
            OccupationDAO pDAO = new OccupationDAO();
            i = pDAO.InsertOccupation(oInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateOccupation")]
        public IHttpActionResult UpdOccupation([FromBody]OccupationInfo oInfo)
        {
            //CompanyListReturn companyList = new CompanyListReturn();
            //List<CompanyListReturn> listCompany = new List<CompanyListReturn>();
            OccupationDAO oDAO = new OccupationDAO();
            int i = 0;
            i = oDAO.UpdateOccupation(oInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteOccupation")]
        public IHttpActionResult DelOccupation ([FromBody]OccupationInfo oInfo)
        {
            OccupationListReturn productReturn = new OccupationListReturn();
            int? i = 0;
            OccupationDAO oDAO = new OccupationDAO();
            i = oDAO.DeleteOccupation(oInfo);

            return Ok<int?>(i);
        }
    }
}
