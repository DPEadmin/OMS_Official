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
using System.Data;


namespace APPCOREVIEW.Views.Demo.Controllers
{
    public class LeadManagementController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateLeadManagement")]
        public IHttpActionResult UpdCampaign([FromBody] LeadManagementInfo cInfo)
        {
            //LeadManagementInfo campaignList = new LeadManagementInfo();
            //List<LeadManagementInfo> listCampaign = new List<LeadManagementInfo>();
            LeadManagementDAO cDAO = new LeadManagementDAO();
            int i = 0;
            i = cDAO.UpdateLeadManagement(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteLeadManagement")]
        public IHttpActionResult DelCampaign([FromBody]LeadManagementInfo cInfo)
        {
            //LeadManagementInfo campaignList = new LeadManagementInfo();
            //List<LeadManagementInfo> listCampaign = new List<LeadManagementInfo>();
            LeadManagementDAO cDAO = new LeadManagementDAO();
            int i = 0;
            i = cDAO.DeleteLeadManagement(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertLeadManagement")]
        public IHttpActionResult InsCampaign([FromBody]LeadManagementInfo cInfo)
        {
        
            LeadManagementDAO cDAO = new LeadManagementDAO();
            int i = 0;
            i = cDAO.InsertLeadManagement(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListLeadManagementNoPagingByCriteria")]
        public IHttpActionResult ListCpNoPagingByCriteria([FromBody]LeadManagementInfo cInfo)
        {
            LeadManagementInfo campaignList = new LeadManagementInfo();
            List<LeadManagementInfo> listCampaign = new List<LeadManagementInfo>();
            LeadManagementDAO cDAO = new LeadManagementDAO();

            listCampaign = cDAO.ListLeadManagementNoPagingByCriteria(cInfo);

            return Ok<List<LeadManagementInfo>>(listCampaign);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountLeadManagementListByCriteria")]
        public IHttpActionResult CountCpListByCriteria([FromBody]LeadManagementInfo cInfo)
        {
            //LeadManagementInfo campaignList = new LeadManagementInfo();
            //List<LeadManagementInfo> listCampaign = new List<LeadManagementInfo>();
            LeadManagementDAO cDAO = new LeadManagementDAO();
            int? i = 0;
            i = cDAO.countLeadListByCriteria(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListLeadManagementPagingByCriteria")]
        public IHttpActionResult CpListByCriteria([FromBody]LeadManagementInfo cInfo)
        {
            LeadManagementInfo campaignList = new LeadManagementInfo();
            List<LeadManagementInfo> listCampaign = new List<LeadManagementInfo>();
            LeadManagementDAO cDAO = new LeadManagementDAO();

            listCampaign = cDAO.ListLeadManagementPagingByCriteria(cInfo);

            return Ok<List<LeadManagementInfo>>(listCampaign);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/LeadManagementListNoPagingCriteria")]
        public IHttpActionResult channalListNoPaging([FromBody]LeadManagementInfo cInfo)
        {
            LeadManagementInfo channalReturn = new LeadManagementInfo();
            List<LeadManagementInfo> lChannalListReturn = new List<LeadManagementInfo>();
            LeadManagementDAO cDAO = new LeadManagementDAO();

            lChannalListReturn = cDAO.LeadManagementListNoPagingCriteria(cInfo);

            return Ok<List<LeadManagementInfo>>(lChannalListReturn);
        }

 
        
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertLeadList")]
        public IHttpActionResult InsertLeadManagementList([FromBody] L_LeadManagementInfo mInfo)
        {
            LeadManagementDAO mDAO = new LeadManagementDAO();
            int i = 0;
            foreach (var LeadManagementList in mInfo.L_LeadManagement.ToList())
            {
                i = mDAO.InsertLeadManagement(LeadManagementList);
            }
            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountAssignLeadManagementListByCriteria")]
        public IHttpActionResult CountAssignLeadManagementListByCriteria([FromBody]LeadManagementInfo cInfo)
        {
            //LeadManagementInfo campaignList = new LeadManagementInfo();
            //List<LeadManagementInfo> listCampaign = new List<LeadManagementInfo>();
            LeadManagementDAO cDAO = new LeadManagementDAO();
            int? i = 0;
            i = cDAO.CountAssignLeadManagementListByCriteria(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListAssignLeadManagementPagingByCriteria")]
        public IHttpActionResult ListAssignLeadManagementPagingByCriteria([FromBody]LeadManagementInfo cInfo)
        {
            LeadManagementInfo campaignList = new LeadManagementInfo();
            List<LeadManagementInfo> listCampaign = new List<LeadManagementInfo>();
            LeadManagementDAO cDAO = new LeadManagementDAO();

            listCampaign = cDAO.ListAssignLeadManagementPagingByCriteria(cInfo);

            return Ok<List<LeadManagementInfo>>(listCampaign);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateAssignEmpLeadManagement")]
        public IHttpActionResult UpdateAssignEmpLeadManagement([FromBody] LeadManagementInfo cInfo)
        {
            LeadManagementDAO cDAO = new LeadManagementDAO();
            int i = 0;
            i = cDAO.UpdateAssignEmpLeadManagement(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertLeadfromEcommerceOrder")]
        public IHttpActionResult InsertLeadfromEcommerceOrder([FromBody] LeadManagementInfo cInfo)
        {
            string date = cInfo.RecontactbackDate;

            LeadManagementDAO cDAO = new LeadManagementDAO();
            int i = 0;
            i = cDAO.InsertLeadfromEcommerceOrder(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateStatusTIBLeadfromTakeOrderRetail")]
        public IHttpActionResult UpdateStatusTIBLeadfromTakeOrderRetail([FromBody] LeadManagementInfo cInfo)
        {
            LeadManagementDAO cDAO = new LeadManagementDAO();
            int i = 0;
            i = cDAO.UpdateStatusTIBLeadfromTakeOrderRetail(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateTIBLeadfromClickToCall")]
        public IHttpActionResult UpdateTIBLeadfromClickToCall([FromBody] LeadManagementInfo cInfo)
        {
            LeadManagementDAO cDAO = new LeadManagementDAO();
            int i = 0;
            i = cDAO.UpdateTIBLeadfromClickToCall(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/LeadManagement/CountLeadManagement")]
        public IHttpActionResult CountLeadManagement([FromBody] LeadManagementInfo cInfo)
        {
           
            LeadManagementDAO cDAO = new LeadManagementDAO();
            int i = 0;
            i = cDAO.CountLeadManagement(cInfo);

            return Ok<int>(i);
        }
    }
}
