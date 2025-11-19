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
    public class IntegrationEmpController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/IntegrationEmp")]
        public IHttpActionResult updatejobTechnicianRecieveItem([FromBody]IntegrationEmpInfo form)
        {
          
            ModelJobadssignMessageReturn resultjobMessage = new ModelJobadssignMessageReturn();
            List<JobMessageReturn> lJobMessage = new List<JobMessageReturn>();
            //JobAssignDAO jobDAO = new JobAssignDAO();
            //int sum = 0;
            //foreach (var JobV in form.JobAssign.ToList())
            //{
            //    //   form.JobAssignA.ToList;
            //    if (JobV.success == "01")//Recieve
            //    {
            //        sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), "02", "05", "", JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(), JobV.JOBBotRejectCode, "03", usercode);

            //    }
            //    else if (JobV.success == "02")//Return ของไม่ครบ
            //    {
            //        //sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), JobV.jobstatus.ToString(), JobV.jobitemstatus.ToString(), JobV.jobproblemstatus.ToString(), JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(), JobV.JOBBotRejectCode, JobV.checkitemrecieve);
            //        sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), "04", "07", JobV.jobproblemstatus.ToString(), JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(), JobV.JOBBotRejectCode, "03", usercode);
            //    }
            //    else
            //    {
            //        sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), "04", "07", "", JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(), JobV.JOBBotRejectCode, "03", usercode);


            //    }

            //    if (sum > 0)
            //    {
            //        resultjobMessage.JobAssignMessageReturn.Add(new JobMessageReturn() { JOBID = JobV.jobid.ToString(), StatusSuccess = "Y", StatusMessage = "บันทึกข้อมูลเรียบร้อย" });
            //    }
            //    else
            //    {
            //        resultjobMessage.JobAssignMessageReturn.Add(new JobMessageReturn() { JOBID = JobV.jobid.ToString(), StatusSuccess = "N", StatusMessage = "ไม่สามารถบันทึกข้อมูลได้" });
            //    }
            //}

            return Ok<ModelJobadssignMessageReturn>(resultjobMessage);
        }

    }
}