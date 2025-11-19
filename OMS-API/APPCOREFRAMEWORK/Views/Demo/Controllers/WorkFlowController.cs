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
    public class WorkflowController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListWorkflowTaskDetailByCriteria")]
        public IHttpActionResult ListWfTaskDetailByCriteria([FromBody]WorkflowInfo wInfo)
        {
            WorkflowListReturn workflowList = new WorkflowListReturn();
            List<WorkflowListReturn> listWorkflow = new List<WorkflowListReturn>();
            WorkflowDAO wDAO = new WorkflowDAO();

            listWorkflow = wDAO.ListWorkflowTaskDetailByCriteria(wInfo);

            return Ok<List<WorkflowListReturn>>(listWorkflow);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertWFTaskList")]
        public IHttpActionResult InsertWorkFlowTaskList([FromBody]WorkflowInfo infos)
        {
            WorkflowDAO cDAO = new WorkflowDAO();
            int i = 0;
            i = cDAO.InsertWFTaskList(infos);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateWFTaskList")]
        public IHttpActionResult UpdateWorkFlowTaskList([FromBody]WorkflowInfo wInfo)
        {
            WorkflowDAO cDAO = new WorkflowDAO();
            int i = 0;
            i = cDAO.UpdateWFTaskList(wInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListWorkflowStatusNoPagingByCriteria")]
        public IHttpActionResult ListWorkflowStatusNoPagingByCrit([FromBody]WorkflowInfo wInfo)
        {
            WorkflowListReturn workflowReturn = new WorkflowListReturn();
            List<WorkflowListReturn> listworkflowReturn = new List<WorkflowListReturn>();
            WorkflowDAO sDAO = new WorkflowDAO();

            listworkflowReturn = sDAO.ListWorkflowStatusNoPagingByCriteria(wInfo);

            return Ok<List<WorkflowListReturn>>(listworkflowReturn);
        }
    }
}
