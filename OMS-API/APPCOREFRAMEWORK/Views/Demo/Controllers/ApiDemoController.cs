using APPCOREMODEL.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APPCOREMODEL.DAO;
using APPCOREMODEL.Datas.Authens;
using System.Security.Claims;
using System.Configuration;
using System.Web.Helpers;
using System.IO;
namespace APPCOREVIEW.Views.Demo.Controllers
{
    public class ApiDemoController : ApiController
    {
       
        [Authorize]
        [HttpGet]
        [Route("api/support/demo01")]
        public IHttpActionResult demo01([FromUri]Object form)
        {
           
            return Ok("");
        }
        [Authorize]
        [HttpPost]
        [Route("api/support/demo03")]
        public IHttpActionResult demo03([FromBody]Object form)
        {
            return Ok("");
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("api/support/demo02")]
        public IHttpActionResult demo02([FromUri]Object form)
        {
            return Ok("");
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/demo4")]
        public IHttpActionResult demo04([FromBody]Object form)
        {
            return Ok<ModelDemo4>(null);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/MasterData")]
        public IHttpActionResult MasterData()
        {
            //string username = "";
            //string password = "";
            ModelDemo4 result = new ModelDemo4();
            EmpLogin lLogin = new EmpLogin();
      
            ClaimsIdentity identiy = (ClaimsIdentity)User.Identity;
            string usercode = identiy.FindFirst(ClaimTypes.NameIdentifier).Value;
            string vendercode = identiy.FindFirst(ClaimTypes.GivenName).Value;
            string EmpRole = identiy.FindFirst(ClaimTypes.Role).Value;
            lLogin.EmpCode = usercode;
            lLogin.VendorCode = vendercode;
            lLogin.Role = EmpRole;
            List<MenuMobile> lMenuMobile = new List<MenuMobile>();
            MenuMobileDAO Menumobile = new MenuMobileDAO();
            lMenuMobile = Menumobile.GetMenu(lLogin);
            foreach (var MenuV in lMenuMobile)
            {
                result.MenuVendor.Add(new MenuMobile() { MenuCode = MenuV.MenuCode, Vendorid = MenuV.Vendorid });
            }

            List<ConfigMaster> lConfig = new List<ConfigMaster>();
            ConfigMasterDAO ConficDAO = new ConfigMasterDAO();
            lConfig = ConficDAO.GetConfigMaster();
            foreach (var ConficV in lConfig)
            {
                result.ConfigMastersV.Add(new ConfigMaster() { NameTB = ConficV.NameTB, Version = ConficV.Version, Vendor = ConficV.Vendor, Role = ConficV.Role });
            }

            List<MasterLookup> lLookUp = new List<MasterLookup>();
            LookUpMobileDAO LookUpaa = new LookUpMobileDAO();
            lLookUp = LookUpaa.GetLookUp();
            foreach (var LookV in lLookUp)
            {
                result.masterlookupStatus.Add(new MasterLookup() { lookup_id = LookV.lookup_id, LookupCode = LookV.LookupCode, LookupType = LookV.LookupType, LookupValue = LookV.LookupValue, RefferenceJobstatus=LookV.RefferenceJobstatus });
            }

            List<LogoVendor> lLogo = new List<LogoVendor>();
            LogoMobileDAO LogoItem = new LogoMobileDAO();
            lLogo = LogoItem.GetLogo(lLogin);
            foreach (var LV in lLogo)
            {
                result.LogoV.Add(new LogoVendor() { id = LV.id, MediaBase64 = LV.MediaBase64, VendorCode = LV.VendorCode });
            }
            string PathUploadfile = ConfigurationManager.AppSettings["UploadPicJobPath"].ToString();
            result.PathUploadV.Add(new PathUpload() { Path = PathUploadfile });


            return Ok<ModelDemo4>(result);
        }

        [Authorize]
        [HttpPost]
        [Route("api/support/userprofile")]
        public IHttpActionResult Userprofile()
        {
             ClaimsIdentity identiy = (ClaimsIdentity)User.Identity;
             string usercode = identiy.FindFirst(ClaimTypes.NameIdentifier).Value;
             string vendercode = identiy.FindFirst(ClaimTypes.GivenName).Value;
            AuthenResult resultprofile = new AuthenResult();
            EmpLoginn lLogin = new EmpLoginn();

            //lLogin.username = "somchai";
            //lLogin.password = "somchai";
            List<Emp> lEMP = new List<Emp>();
            MenuMobileDAO Menumobile = new MenuMobileDAO();
            lEMP = Menumobile.GetEmp(usercode, vendercode);
            foreach (var EmpV in lEMP)
            {
                resultprofile.Empployee.Add(new Emp() { EmpUsername = EmpV.EmpUsername, EmpPassword = EmpV.EmpPassword, EmpVendor = EmpV.EmpVendor, EmpRole = EmpV.EmpRole, NameRole = EmpV.NameRole,EmpCode=EmpV.EmpCode });

            }
            return Ok<AuthenResult>(resultprofile);

        }
        [Authorize]
        [HttpPost]
        [Route("api/support/JobAssign")]
        public IHttpActionResult JobAssignCenter([FromBody]Object form)
        {
            ClaimsIdentity identiy = (ClaimsIdentity)User.Identity;
            string usercode = identiy.FindFirst(ClaimTypes.NameIdentifier).Value;
            string vendercode = identiy.FindFirst(ClaimTypes.GivenName).Value;
            string EmpRole = identiy.FindFirst(ClaimTypes.Role).Value;

            ModelJobadssign resultjob = new ModelJobadssign();
            List<JobAssignDetail> lJobAssign = new List<JobAssignDetail>();
            List<JobItem> lJobItem = new List<JobItem>();
            List<JobHistory> lJobHistory = new List<JobHistory>();
            JobAssignDAO JobAssignDAO = new JobAssignDAO();
            if (EmpRole == "3")
            {
                lJobAssign = JobAssignDAO.GetJobAssign("'01'", "'02'", vendercode, "");
              
            }
            else 
            {
                lJobAssign = JobAssignDAO.GetJobAssign("'02','06','10'", "'02','05'", vendercode, usercode);
            }
            

            foreach (var JobV in lJobAssign)
            {
                lJobItem = JobAssignDAO.GetJobItem(JobV.Gitem);
                lJobHistory = JobAssignDAO.GetJobHistory(JobV.JobID);
                resultjob.JobAssign.Add(new JobAssignDetail()
                {
                    //  JobID = JobV.JobID, AcqBank = JobV.AcqBank, TypeOfJob = JobV.TypeOfJob

                    JobID = JobV.JobID,
                    TypeOfJob = JobV.TypeOfJob,
                    AcqBank = JobV.AcqBank,
                    JobStatus = JobV.JobStatus,
                    JobStatusName = JobV.JobStatusName,

                    JOBITEMSTATUS = JobV.JOBITEMSTATUS,
                    JOBITEMSTATUS_Name = JobV.JOBITEMSTATUS_Name,
                    MerchanID = JobV.MerchanID,
                    MerchanName = JobV.MerchanName,
                    TerminalId = JobV.TerminalId,

                    TerminalAddress = JobV.TerminalAddress,
                    TerminalContract = JobV.TerminalContract,
                    Brand = JobV.Brand,
                    EdcSerialNo = JobV.EdcSerialNo,

                    jobclass = JobV.jobclass,
                    JobclassName = JobV.JobclassName,
                    RequestDate = JobV.RequestDate,
                    JOBPROBLEMSTATUS = JobV.JOBPROBLEMSTATUS,
                    JOBPROBLEMSTATUS_Name = JobV.JOBPROBLEMSTATUS_Name,
                    TrackingNo = JobV.TrackingNo,

                    TerminalPhone = JobV.TerminalPhone,
                    Model = JobV.Model,
                    jobNote = JobV.jobNote,
                    assignto = JobV.assignto,
                    Gitem = JobV.Gitem,
                    Item = lJobItem,
                    JobHistory = lJobHistory,
                    Vendor = JobV.Vendor,
                    checkitemrecieve =JobV.checkitemrecieve
                    ,ServiceDate = JobV.ServiceDate
                    ,SerialSim = JobV.SerialSim
                    
                });
            }

            return Ok<ModelJobadssign>(resultjob);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/JobAssignVendor")]
        public IHttpActionResult JobAssignVendorCenter([FromBody]Object form)
        {
            ClaimsIdentity identiy = (ClaimsIdentity)User.Identity;
            string usercode = identiy.FindFirst(ClaimTypes.NameIdentifier).Value;
            string vendercode = identiy.FindFirst(ClaimTypes.GivenName).Value;
            string EmpRole = identiy.FindFirst(ClaimTypes.Role).Value;

            ModelJobadssign resultjob = new ModelJobadssign();
            List<JobAssignDetail> lJobAssign = new List<JobAssignDetail>();
            List<JobItem> lJobItem = new List<JobItem>();
            List<JobHistory> lJobHistory = new List<JobHistory>();
            JobAssignDAO JobAssignDAO = new JobAssignDAO();

            lJobAssign = JobAssignDAO.GetJobAssign("'01'", "'02'", vendercode, usercode);

            foreach (var JobV in lJobAssign)
            {
                lJobItem = JobAssignDAO.GetJobItem(JobV.Gitem);
                lJobHistory = JobAssignDAO.GetJobHistory(JobV.JobID);
                resultjob.JobAssign.Add(new JobAssignDetail()
                {
                    //  JobID = JobV.JobID, AcqBank = JobV.AcqBank, TypeOfJob = JobV.TypeOfJob

                    JobID = JobV.JobID,
                    TypeOfJob = JobV.TypeOfJob,
                    AcqBank = JobV.AcqBank,
                    JobStatus = JobV.JobStatus,
                    JobStatusName = JobV.JobStatusName,

                    JOBITEMSTATUS = JobV.JOBITEMSTATUS,
                    JOBITEMSTATUS_Name = JobV.JOBITEMSTATUS_Name,
                    MerchanID = JobV.MerchanID,
                    MerchanName = JobV.MerchanName,
                    TerminalId = JobV.TerminalId,

                    TerminalAddress = JobV.TerminalAddress,
                    TerminalContract = JobV.TerminalContract,
                    Brand = JobV.Brand,
                    EdcSerialNo = JobV.EdcSerialNo,

                    jobclass = JobV.jobclass,
                    JobclassName = JobV.JobclassName,
                    RequestDate = JobV.RequestDate,
                    JOBPROBLEMSTATUS = JobV.JOBPROBLEMSTATUS,
                    JOBPROBLEMSTATUS_Name = JobV.JOBPROBLEMSTATUS_Name,
                    TrackingNo = JobV.TrackingNo,

                    TerminalPhone = JobV.TerminalPhone,
                    Model = JobV.Model,
                    jobNote = JobV.jobNote,
                    assignto = JobV.assignto,
                    Gitem = JobV.Gitem,
                    Item = lJobItem,
                    JobHistory = lJobHistory
                });
            }

            return Ok<ModelJobadssign>(resultjob);
        }
        [Authorize]
        [HttpPost]
        [Route("api/support/updatejob")]
        public IHttpActionResult updateJobID([FromBody]UpdateJobStatusID form)
        {
            ClaimsIdentity identiy = (ClaimsIdentity)User.Identity;
            string usercode = identiy.FindFirst(ClaimTypes.NameIdentifier).Value;
            string vendercode = identiy.FindFirst(ClaimTypes.GivenName).Value;
            string EmpRole = identiy.FindFirst(ClaimTypes.Role).Value;

            ModelJobadssignMessageReturn resultjobMessage = new ModelJobadssignMessageReturn();
            List<JobMessageReturn> lJobMessage = new List<JobMessageReturn>();
            JobAssignDAO jobDAO = new JobAssignDAO();
            int sum = 0;
            if (EmpRole == "3") //Vendor
            {
                foreach (var JobV in form.JobAssign.ToList())
                {
                    if (JobV.success == "01")//Recieve
                    {
                        sum = jobDAO.UpdateJob(JobV.jobid.ToString(), "01", "04", "", JobV.jobnote.ToString(), usercode, "01");
                    }
                    else if (JobV.success == "02")//Return ของไม่ครบ
                    {
                        sum = jobDAO.UpdateJob(JobV.jobid.ToString(), "01", "07", "", JobV.jobnote.ToString(), usercode, "01");
                    }
                    else
                    {
                        sum = jobDAO.UpdateJob(JobV.jobid.ToString(), "03", "06", "", JobV.jobnote.ToString(), usercode, "01");
                    }
                    if (sum > 0)
                    {
                        resultjobMessage.JobAssignMessageReturn.Add(new JobMessageReturn() { JOBID = JobV.jobid.ToString(), StatusSuccess = "Y", StatusMessage = "บันทึกข้อมูลเรียบร้อย" });
                    }
                    else
                    {
                        resultjobMessage.JobAssignMessageReturn.Add(new JobMessageReturn() { JOBID = JobV.jobid.ToString(), StatusSuccess = "N", StatusMessage = "ไม่สามารถบันทึกข้อมูลได้" });
                    }
                }
            }
            else
            {
                foreach (var JobV in form.JobAssign.ToList())//Technician
                {
                    if (JobV.success == "01")//Recieve
                    {
                        sum = jobDAO.UpdateJob(JobV.jobid.ToString(), "02", "05", "", JobV.jobnote.ToString(), usercode,"03");
                    }
                    else if (JobV.success == "02")//Return ของไม่ครบ
                    {
                        sum = jobDAO.UpdateJob(JobV.jobid.ToString(), "04", "07", "", JobV.jobnote.ToString(), usercode,"03");
                    }
                    else // Error
                    {
                        sum = jobDAO.UpdateJob(JobV.jobid.ToString(), "04", "06", "", JobV.jobnote.ToString(), usercode,"03");
                    }
                    if (sum > 0)
                    {
                        resultjobMessage.JobAssignMessageReturn.Add(new JobMessageReturn() { JOBID = JobV.jobid.ToString(), StatusSuccess = "Y", StatusMessage = "บันทึกข้อมูลเรียบร้อย" });
                    }
                    else
                    {
                        resultjobMessage.JobAssignMessageReturn.Add(new JobMessageReturn() { JOBID = JobV.jobid.ToString(), StatusSuccess = "N", StatusMessage = "ไม่สามารถบันทึกข้อมูลได้" });
                    }
                }
            }
        

            return Ok<ModelJobadssignMessageReturn>(resultjobMessage);
        }
 
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/updatejobRecieveItem")]
        public IHttpActionResult updatejobVendorRecieveItem([FromBody]UpdateJobStatusIDTecnicain form)
        {
            ClaimsIdentity identiy = (ClaimsIdentity)User.Identity;
            string usercode = identiy.FindFirst(ClaimTypes.NameIdentifier).Value;
            string vendercode = identiy.FindFirst(ClaimTypes.GivenName).Value;
            string EmpRole = identiy.FindFirst(ClaimTypes.Role).Value;

            ModelJobadssignMessageReturn resultjobMessage = new ModelJobadssignMessageReturn();
            List<JobMessageReturn> lJobMessage = new List<JobMessageReturn>();
            JobAssignDAO jobDAO = new JobAssignDAO();
            int sum = 0;
            if (EmpRole == "3")
            {
                foreach (var JobV in form.JobAssign.ToList())
                {
                    //   form.JobAssignA.ToList;
                    if (JobV.success == "01")//Recieve
                    {
                        sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), "01", "04", "", JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(),"", "01", usercode);

                    }
                    else if (JobV.success == "02")//Return ของไม่ครบส่งกลับ Loxbit
                    {
                        sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), "03","07", JobV.jobproblemstatus.ToString(), JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(),"", "01", usercode);

                    }
                    else if (JobV.success == "03")//Return  Loxbit
                    {
                        sum = jobDAO.UpdateJob(JobV.jobid.ToString(), "03", "06", "", JobV.jobnote.ToString(), usercode, "03");
                       // sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), "03", "06", JobV.jobproblemstatus.ToString(), JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(),"", "01", usercode);

                    }
                    else
                    {
                       // sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), "04", "07", "", JobV.jobnote.ToString(), JobV.ItemRecieve.ToList());

                    }
                    if (sum > 0)
                    {
                        resultjobMessage.JobAssignMessageReturn.Add(new JobMessageReturn() { JOBID = JobV.jobid.ToString(), StatusSuccess = "Y", StatusMessage = "บันทึกข้อมูลเรียบร้อย" });
                    }
                    else
                    {
                        resultjobMessage.JobAssignMessageReturn.Add(new JobMessageReturn() { JOBID = JobV.jobid.ToString(), StatusSuccess = "N", StatusMessage = "ไม่สามารถบันทึกข้อมูลได้" });
                    }
                }

            }
            else if (EmpRole == "6")
            {
                foreach (var JobV in form.JobAssign.ToList())
                {
                    //   form.JobAssignA.ToList;
                    if (JobV.success == "01")//Recieve
                    {
                        sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), "02", "05", "", JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(),"", "03", usercode);

                    }
                    else if (JobV.success == "02")//Return ของไม่ครบ
                    {
                        sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), "04", "07", JobV.jobproblemstatus.ToString(), JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(),JobV.JOBBotRejectCode, "03", usercode);

                    }
                    else if (JobV.success == "03")//Return  Vendor
                    {
                        sum = jobDAO.UpdateJob(JobV.jobid.ToString(), "03", "06", "", JobV.jobnote.ToString(), usercode, "03");
                      //  sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), "04", "06", JobV.jobproblemstatus.ToString(), JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(), JobV.JOBBotRejectCode, "03", usercode);

                    }
                    else
                    {
                        sum = jobDAO.UpdateJob(JobV.jobid.ToString(), "04", "06", "", JobV.jobnote.ToString(), usercode, "03");
                        //sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), "04", "07", "", JobV.jobnote.ToString(), JobV.ItemRecieve.ToList());
                    }

                    if (sum > 0)
                    {
                        resultjobMessage.JobAssignMessageReturn.Add(new JobMessageReturn() { JOBID = JobV.jobid.ToString(), StatusSuccess = "Y", StatusMessage = "บันทึกข้อมูลเรียบร้อย" });
                    }
                    else
                    {
                        resultjobMessage.JobAssignMessageReturn.Add(new JobMessageReturn() { JOBID = JobV.jobid.ToString(), StatusSuccess = "N", StatusMessage = "ไม่สามารถบันทึกข้อมูลได้" });
                    }
                }
            }
            else
            {
                foreach (var JobV in form.JobAssign.ToList())
                {
                    //   form.JobAssignA.ToList;
                    //if (JobV.success == "01")//Recieve
                    //{
                    //    sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), "02", "05", "", "Loxbit " + JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(), JobV.JOBBotRejectCode, "03");
                    //}
                    //else if (JobV.success == "02")//Return ของไม่ครบ
                    //{
                    //    //sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), JobV.jobstatus.ToString(), "Loxbit " + JobV.jobitemstatus.ToString(), JobV.jobproblemstatus.ToString(), JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(), JobV.JOBBotRejectCode, JobV.checkitemrecieve);
                    //    //sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), JobV.jobstatus.ToString(), "Loxbit " + JobV.jobitemstatus.ToString(), JobV.jobproblemstatus.ToString(), JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(), JobV.JOBBotRejectCode, JobV.checkitemrecieve);
                    //    sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), "04", "07", "", "Loxbit " + JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(), JobV.JOBBotRejectCode, "01");

                    //}
                    //else
                    //{
                    //    sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), "04", "07", "", "Loxbit " + JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(), JobV.JOBBotRejectCode, "01");
                    //}

                    //if (sum > 0)
                    //{
                    //    resultjobMessage.JobAssignMessageReturn.Add(new JobMessageReturn() { JOBID = JobV.jobid.ToString(), StatusSuccess = "Y", StatusMessage = "บันทึกข้อมูลเรียบร้อย" });
                    //}
                    //else
                    //{
                    //    resultjobMessage.JobAssignMessageReturn.Add(new JobMessageReturn() { JOBID = JobV.jobid.ToString(), StatusSuccess = "N", StatusMessage = "ไม่สามารถบันทึกข้อมูลได้" });
                    //}
                }
             }


            return Ok<ModelJobadssignMessageReturn>(resultjobMessage);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/updatejobTechnicianRecieveItem")]
        public IHttpActionResult updatejobTechnicianRecieveItem([FromBody]UpdateJobStatusIDTecnicain form)
        {
            ClaimsIdentity identiy = (ClaimsIdentity)User.Identity;
            string usercode = identiy.FindFirst(ClaimTypes.NameIdentifier).Value;
            string vendercode = identiy.FindFirst(ClaimTypes.GivenName).Value;
            string EmpRole = identiy.FindFirst(ClaimTypes.Role).Value;

            ModelJobadssignMessageReturn resultjobMessage = new ModelJobadssignMessageReturn();
            List<JobMessageReturn> lJobMessage = new List<JobMessageReturn>();
            JobAssignDAO jobDAO = new JobAssignDAO();
            int sum = 0;
            foreach (var JobV in form.JobAssign.ToList())
            {
                //   form.JobAssignA.ToList;
                if (JobV.success == "01")//Recieve
                {
                    sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), "02", "05", "", JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(), JobV.JOBBotRejectCode, "03", usercode);

                }
                else if (JobV.success == "02")//Return ของไม่ครบ
                {
                    //sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), JobV.jobstatus.ToString(), JobV.jobitemstatus.ToString(), JobV.jobproblemstatus.ToString(), JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(), JobV.JOBBotRejectCode, JobV.checkitemrecieve);
                    sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), "04", "07", JobV.jobproblemstatus.ToString(), JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(), JobV.JOBBotRejectCode, "03", usercode);
                }
                else
                {
                    sum = jobDAO.UpdateJobTechnician(JobV.jobid.ToString(), "04", "07", "", JobV.jobnote.ToString(), JobV.ItemRecieve.ToList(), JobV.JOBBotRejectCode, "03", usercode);


                }

                if (sum > 0)
                {
                    resultjobMessage.JobAssignMessageReturn.Add(new JobMessageReturn() { JOBID = JobV.jobid.ToString(), StatusSuccess = "Y", StatusMessage = "บันทึกข้อมูลเรียบร้อย" });
                }
                else
                {
                    resultjobMessage.JobAssignMessageReturn.Add(new JobMessageReturn() { JOBID = JobV.jobid.ToString(), StatusSuccess = "N", StatusMessage = "ไม่สามารถบันทึกข้อมูลได้" });
                }
            }

            return Ok<ModelJobadssignMessageReturn>(resultjobMessage);
        }

        [Authorize]
        [HttpPost]
        [Route("api/support/updatejobAppointmentCutomer")]
        public IHttpActionResult updatejobTechnicianMeetCutomer([FromBody]UpdateJobStatusIDTecnicainMeetCustomer form)
        {
            ClaimsIdentity identiy = (ClaimsIdentity)User.Identity;
            string usercode = identiy.FindFirst(ClaimTypes.NameIdentifier).Value;
            string vendercode = identiy.FindFirst(ClaimTypes.GivenName).Value;
            string EmpRole = identiy.FindFirst(ClaimTypes.Role).Value;

            ModelJobadssignMessageReturn resultjobMessage = new ModelJobadssignMessageReturn();
            List<JobMessageReturn> lJobMessage = new List<JobMessageReturn>();
            JobAssignDAO jobDAO = new JobAssignDAO();
            int sum = 0;
            foreach (var JobV in form.JobAssign.ToList())
            {
                //   form.JobAssignA.ToList;
                if (JobV.success == "01")//Recieve
                {
                    sum = jobDAO.UpdateJobTechnicianMeetCustomer(JobV.jobid.ToString(), "06", "05", "", JobV.jobnote.ToString(),JobV.DateMeet.ToString(),JobV.TimeMeet.ToString(), usercode);
                }
                else if (JobV.success == "02")//Return ของไม่ครบ
                {
                    sum = jobDAO.UpdateJobTechnicianMeetCustomer(JobV.jobid.ToString(), JobV.jobstatus.ToString(), JobV.jobitemstatus.ToString(), JobV.jobproblemstatus.ToString(), JobV.jobnote.ToString(), JobV.DateMeet.ToString(), JobV.TimeMeet.ToString(), usercode);
                }
             
                if (sum > 0)
                {
                    resultjobMessage.JobAssignMessageReturn.Add(new JobMessageReturn() { JOBID = JobV.jobid.ToString(), StatusSuccess = "Y", StatusMessage = "บันทึกข้อมูลเรียบร้อย" });
                }
                else
                {
                    resultjobMessage.JobAssignMessageReturn.Add(new JobMessageReturn() { JOBID = JobV.jobid.ToString(), StatusSuccess = "N", StatusMessage = "ไม่สามารถบันทึกข้อมูลได้" });
                }
            }

            return Ok<ModelJobadssignMessageReturn>(resultjobMessage);
        }


        [Authorize]
        [HttpPost]
        [Route("api/support/Jobkeepfile")]
        public IHttpActionResult updateJobKeepPicture([FromBody]InsertJobFile form)
        {
            ClaimsIdentity identiy = (ClaimsIdentity)User.Identity;
            string usercode = identiy.FindFirst(ClaimTypes.NameIdentifier).Value;
            string vendercode = identiy.FindFirst(ClaimTypes.GivenName).Value;
            string EmpRole = identiy.FindFirst(ClaimTypes.Role).Value;
           
            ModelJobkeepfile resultjobkeepfileMessage = new ModelJobkeepfile();
            List<JobMessageReturnFile> lJobMessage = new List<JobMessageReturnFile>();
            JobAssignDAO jobDAO = new JobAssignDAO();
            int sum = 0,sumupdateJob=0;
            int i = 1;
            foreach (var JobV in form.JOBASSIGNTechnicianSuccess.ToList())
            {
                sumupdateJob = jobDAO.UpdateJobTechnicianJobSuccess(JobV.jobid.ToString(), JobV.jobstatus.ToString(), JobV.jobproblemstatus.ToString(), JobV.jobnote.ToString(), JobV.JOBBoiRejectCode, JobV.JOBBoiNote, JobV.LatLong.ToString().Trim(),JobV.JobProblemNote.ToString(),JobV.ItemRecieve.ToList());

                if (sumupdateJob > 0)
                {
                    if (JobV.jobstatus.ToString() == "08")
                    {
                        foreach (var JobPicture in JobV.JobAssignKeepfile.ToList())
                        {
                            DateTime dt = DateTime.Now; // Or whatever
                            string sTime = dt.ToString("yyyyMMddHHmmss");
                            // new name file
                            string s = JobPicture.namefile.ToString();
                            string[] TypeFile = s.Split('.');
                            string path = ConfigurationManager.AppSettings["UploadPicJobPath"].ToString();
                            string PathUploadfile = System.Web.HttpContext.Current.Server.MapPath(path) + JobPicture.namefile.ToString();
                            string PathUploadfile2 = System.Web.HttpContext.Current.Server.MapPath(path) + JobV.jobid.ToString() + form.serial.ToString() + i.ToString() + sTime + "_" + JobPicture.Typefile.ToString() + "." + TypeFile[1].ToString();
                            string newfileName = JobV.jobid.ToString() + form.serial.ToString() + i.ToString() +sTime+ "_" + JobPicture.Typefile.ToString() + "." + TypeFile[1].ToString();
                            i++;
                            ///////////
                            if (File.Exists(PathUploadfile))
                            {

                                System.IO.File.Move(PathUploadfile, PathUploadfile2);
                                sum = jobDAO.inserJobKeepFile(JobV.jobid.ToString(), JobPicture.notefile.ToString(), newfileName, JobPicture.Typefile.ToString(), form.serial.ToString(), usercode);

                            }

                        }
                        resultjobkeepfileMessage.JobKeepMessageReturnfile.Add(new JobMessageReturnFile() { JOBID = JobV.jobid.ToString(), StatusSuccess = "Y", StatusMessage = "บันทึกข้อมูลเรียบร้อย" });

                    }
                    else
                    {
                        resultjobkeepfileMessage.JobKeepMessageReturnfile.Add(new JobMessageReturnFile() { JOBID = JobV.jobid.ToString(), StatusSuccess = "Y", StatusMessage = "บันทึกข้อมูลเรียบร้อย" });

                    }



                }
                else
                    {
                    resultjobkeepfileMessage.JobKeepMessageReturnfile.Add(new JobMessageReturnFile() { JOBID = JobV.jobid.ToString(), StatusSuccess = "N", StatusMessage = "ไม่สามารถบันทึกข้อมูลได้" });
                }
        }
            return Ok<ModelJobkeepfile>(resultjobkeepfileMessage);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/jobupload")]
        public IHttpActionResult uploadjobImage()
        {
    
            var _file = System.Web.HttpContext.Current.Request.Files["file"];
            string _Oldimage = Path.GetFileName(_file.FileName);

            WebImage img = new WebImage(_file.InputStream);
            if (img.Width > 500)
            {
                img.Resize(500, 450);
            }
            string _image = Guid.NewGuid().ToString() + "." + img.ImageFormat;
           
            //string PathUploadfile = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["UploadPicJobPath"].ToString()) + _image;
            string _current_path = System.Web.HttpContext.Current.Server.MapPath("~/Views/Contractors/Uploads/") + _image;
            //string PathUploadfile2 = ConfigurationManager.AppSettings["UploadPicJobPath"].ToString() + "/" + JobPicture.jobid.ToString() + form.serial.ToString() + i.ToString() + "_" + JobPicture.Typefile.ToString() + "." + TypeFile[1].ToString();

            img.Save(_current_path);
            return Ok<Jobupload>(new Jobupload() { namefile = _image, oldnamefile = _Oldimage.ToString()});
            //return Ok<Jobupload>(new Jobupload() { pathfile = _current_path });
        }

        [Authorize]
        [HttpPost]
        [Route("api/support/UpdatefileNotComplete")]
        public IHttpActionResult updateJobKeepPictureNotComplete([FromBody]UpdateJobFileNotComplete form)
        {
            ClaimsIdentity identiy = (ClaimsIdentity)User.Identity;
            string usercode = identiy.FindFirst(ClaimTypes.NameIdentifier).Value;
            string vendercode = identiy.FindFirst(ClaimTypes.GivenName).Value;
            string EmpRole = identiy.FindFirst(ClaimTypes.Role).Value;

            ModelJobkeepfile resultjobkeepfileMessage = new ModelJobkeepfile();
            List<JobMessageReturnFile> lJobMessage = new List<JobMessageReturnFile>();
            JobAssignDAO jobDAO = new JobAssignDAO();
            int sum = 0, sumupdateJob = 0;
            int i = 1;
            foreach (var JobV in form.JOBASSIGNTechnicianSuccess.ToList())
            {
                sumupdateJob = jobDAO.UpdateJobTechnicianJobSuccessNotComplete(JobV.jobid.ToString(),"08");

                if (sumupdateJob > 0)
                {
                    //if (JobV.jobstatus.ToString() == "08")
                    //{
                        foreach (var JobPicture in JobV.JobAssignKeepfile.ToList())
                        {
                            DateTime dt = DateTime.Now; // Or whatever
                            string sTime = dt.ToString("yyyyMMddHHmmss");
                            // new name file
                            string s = JobPicture.namefile.ToString();
                            string[] TypeFile = s.Split('.');
                            string path = ConfigurationManager.AppSettings["UploadPicJobPath"].ToString();
                            string PathUploadfile = System.Web.HttpContext.Current.Server.MapPath(path) + JobPicture.namefile.ToString();
                            string PathUploadfile2 = System.Web.HttpContext.Current.Server.MapPath(path) + JobV.jobid.ToString() + form.serial.ToString() + i.ToString() + sTime + "_" + JobPicture.Typefile.ToString() + "." + TypeFile[1].ToString();
                            string newfileName = JobV.jobid.ToString() + form.serial.ToString() + i.ToString() + sTime + "_" + JobPicture.Typefile.ToString() + "." + TypeFile[1].ToString();
                            i++;
                            ///////////
                            if (File.Exists(PathUploadfile))
                            {

                                System.IO.File.Move(PathUploadfile, PathUploadfile2);
                                sum = jobDAO.inserJobKeepFile(JobV.jobid.ToString(), JobPicture.notefile.ToString(), newfileName, JobPicture.Typefile.ToString(), form.serial.ToString(), usercode);

                            }

                        }
                        resultjobkeepfileMessage.JobKeepMessageReturnfile.Add(new JobMessageReturnFile() { JOBID = JobV.jobid.ToString(), StatusSuccess = "Y", StatusMessage = "บันทึกข้อมูลเรียบร้อย" });

                    //}
                    //else
                    //{
                    //    resultjobkeepfileMessage.JobKeepMessageReturnfile.Add(new JobMessageReturnFile() { JOBID = JobV.jobid.ToString(), StatusSuccess = "Y", StatusMessage = "บันทึกข้อมูลเรียบร้อย" });

                    //}



                }
                else
                {
                    resultjobkeepfileMessage.JobKeepMessageReturnfile.Add(new JobMessageReturnFile() { JOBID = JobV.jobid.ToString(), StatusSuccess = "N", StatusMessage = "ไม่สามารถบันทึกข้อมูลได้" });
                }
            }
            return Ok<ModelJobkeepfile>(resultjobkeepfileMessage);
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertTokenMobile")]
        public IHttpActionResult TokenMobile([FromBody]JobModelToken_Mobile form)
        {
            ClaimsIdentity identiy = (ClaimsIdentity)User.Identity;
            //string usercode = identiy.FindFirst(ClaimTypes.NameIdentifier).Value;
            //string vendercode = identiy.FindFirst(ClaimTypes.GivenName).Value;
            //string EmpRole = identiy.FindFirst(ClaimTypes.Role).Value;

            ModelTokenMobileMessage resultjobMessage = new ModelTokenMobileMessage();
            List<JobMessageTokenMobile> lJobMessage = new List<JobMessageTokenMobile>();
            JobAssignDAO jobDAO = new JobAssignDAO();
            int sum = 0; int summobile = 0;
            summobile = jobDAO.checkTokenmobile(form.Serial_Mobile.ToString());
            if (summobile > 0)
            {
                sum = jobDAO.UpdateTokenMobile(form.Token.ToString(), form.Serial_Mobile.ToString(), form.Phone_Mobile.ToString(), form.Vendor);

            }
            else
            {
                sum = jobDAO.insertTokenMobile(form.Token.ToString(), form.Serial_Mobile.ToString(), form.Phone_Mobile.ToString(), form.Vendor);

            }

            if (sum > 0)
            {
                resultjobMessage.Token_MobileMessageReturn.Add(new JobMessageTokenMobile() { Token = form.Token.ToString(), StatusSuccess = "Y", StatusMessage = "บันทึกข้อมูลเรียบร้อย" });
            }
            else
            {
                resultjobMessage.Token_MobileMessageReturn.Add(new JobMessageTokenMobile() { Token = form.Token.ToString(), StatusSuccess = "N", StatusMessage = "ไม่สามารถบันทึกข้อมูลได้" });
            }
            return Ok<ModelTokenMobileMessage>(resultjobMessage);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetTokenMobile")]
        public IHttpActionResult JobTokenMobile([FromBody]Object form)
        {
            ClaimsIdentity identiy = (ClaimsIdentity)User.Identity;
            string usercode = identiy.FindFirst(ClaimTypes.NameIdentifier).Value;
            string vendercode = identiy.FindFirst(ClaimTypes.GivenName).Value;
            string EmpRole = identiy.FindFirst(ClaimTypes.Role).Value;

            ModelJobTokenbile resultjob = new ModelJobTokenbile();
            List<JobTokenMobile> lJobAssign = new List<JobTokenMobile>();
           
            JobAssignDAO JobAssignDAO = new JobAssignDAO();
         
            lJobAssign = JobAssignDAO.GetTokenMobile();
            foreach (var TokenV in lJobAssign)
            {
                resultjob.JobTokenDetail.Add(new JobTokenMobile()
                {

                    Tokenid = TokenV.Tokenid,
                    Token = TokenV.Token,
                    Serial_Mobile = TokenV.Serial_Mobile,
                    Phone_Mobile = TokenV.Phone_Mobile,
                    active = TokenV.active,
                    Vendor = TokenV.Vendor,
                });
            }
            return Ok<ModelJobTokenbile>(resultjob);
        }
        /// <summary>
        /// Exten Project
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/jobuploadCream")]
        public IHttpActionResult uploadjobImageCream()
        {

            var _file = System.Web.HttpContext.Current.Request.Files["file"];
            string _Oldimage = Path.GetFileName(_file.FileName);

            WebImage img = new WebImage(_file.InputStream);
            if (img.Width > 500)
            {
                img.Resize(500, 450);
            }
            string _image = Guid.NewGuid().ToString() + "." + img.ImageFormat;

            string _current_path = System.Web.HttpContext.Current.Server.MapPath("~/Views/Contractors/CreamPic/") + _image;

            img.Save(_current_path);
            return Ok<Jobupload>(new Jobupload() { namefile = _image, oldnamefile = _Oldimage.ToString() });
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetTMSTerminal")]
        public IHttpActionResult PostMerchant([FromBody]TMSGet TmsGet)
        {
            ResultMerchant resultMerchant = new ResultMerchant();
            MenuMobileDAO mbDAO = new MenuMobileDAO();
            List<TMSMerchantResult> tmsmerchantResult = new List<TMSMerchantResult>();
            tmsmerchantResult = mbDAO.GetTMSMerchant(TmsGet);
            
            foreach(var merchantV in tmsmerchantResult)
            {
                resultMerchant.tmsmerchantResult.Add(new TMSMerchantResult() { TerminalID = merchantV.TerminalID, MerchantID = merchantV.MerchantID, MerchantName = merchantV.MerchantName });
            }

            return Ok<ResultMerchant>(resultMerchant);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/MerchantProfile")]
        public IHttpActionResult PostMerchantProfile([FromBody]TMSGet TmsGet)
        {
            ResultMerchantProfile resultMerchantProfile = new ResultMerchantProfile();
            List<MerchantProfile> merchantProfile = new List<MerchantProfile>();
            TMSTypeOfJob tmstypeofJob = new TMSTypeOfJob();
            List<TMSTypeOfJob> ltmstypeofJob = new List<TMSTypeOfJob>();
            MenuMobileDAO mbDAO = new MenuMobileDAO();
            ltmstypeofJob = mbDAO.CheckMerchantTypeOfJob(TmsGet);
            String CheckTypeOfJob = "";

            if (ltmstypeofJob.Count > 0)
            {
                CheckTypeOfJob = ltmstypeofJob[0].TypeOfJob;
            }

            if(CheckTypeOfJob == "Install" || CheckTypeOfJob == "Update")
            {
                merchantProfile = mbDAO.GetMerchantProfile(TmsGet);

                foreach(var itemV in merchantProfile)
                {
                    string PrefixLogoAppUrl = "http://203.154.117.212:93/FLoad/Logo/";
                    string PrefixLogoUrl = "http://203.154.117.212:93/FLoad/LogoSlip/";
                    itemV.LogoAppUrl = "";
                    itemV.LogoAppUrl = PrefixLogoAppUrl + itemV.LogoAppName;
                    itemV.LogoUrl = PrefixLogoUrl + itemV.LogoUrl;

                    itemV.SlipHeader = removeDoubleBackslashes(itemV.SlipHeader);
                }
                foreach(var MerchantProf in merchantProfile)
                {
                    resultMerchantProfile.merchantProfile.Add(new MerchantProfile() { TerminalID = MerchantProf.TerminalID, SerialNumber = MerchantProf.SerialNumber, AppVersion = MerchantProf.AppVersion, MerchantID = MerchantProf.MerchantID, MerchantName = MerchantProf.MerchantName, Brand = MerchantProf.Brand, TypeOfJob = MerchantProf.TypeOfJob, BankName = MerchantProf.BankName, MultiMerchant = MerchantProf.MultiMerchant, Address = MerchantProf.Address, ContactPhoneNo = MerchantProf.ContactPhoneNo, MerchantPassword = MerchantProf.MerchantPassword, SlipCode = MerchantProf.SlipCode, SlipName = MerchantProf.SlipName, SlipHeader = MerchantProf.SlipHeader, SlipFooter = MerchantProf.SlipFooter, LogoAppUrl = MerchantProf.LogoAppUrl, LogoUrl = MerchantProf.LogoUrl, LogoAppName = MerchantProf.LogoAppName, TaxNo = MerchantProf.TaxNo });
                }
            }
            return Ok<ResultMerchantProfile>(resultMerchantProfile);
        }
        public string removeDoubleBackslashes(string input)
        {
            char[] separator = new char[1] { '\\' };
            string result = "";
            string[] subResult = input.Split(separator);
            for (int i = 0; i <= subResult.Length - 1; i++)
            {
                if (i < subResult.Length - 1)
                {
                    // subResult = subResult[i] + "/";
                    result += subResult[i] + "/";
                }
                if (i == subResult.Length - 1)
                {
                    result += subResult[i];
                }
            }
            return result;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CheckInstall")]
        public IHttpActionResult CheckInstall([FromBody]TMSGet tmsGet)
        {
            ResultCheckInstall resultcheckInstall = new ResultCheckInstall();
            MessageReturnInstall msgreturn = new MessageReturnInstall();
            List<MessageReturnInstall> lmsgreturn = new List<MessageReturnInstall>();
            TMSCheckInstall tmschekInstall = new TMSCheckInstall();
            List<TMSCheckInstall> listtmschekInstall = new List<TMSCheckInstall>();
            MenuMobileDAO MenuTMSDao = new MenuMobileDAO();
            listtmschekInstall = MenuTMSDao.CheckMerchantInstall(tmsGet);

            string a = "";
            Boolean FlagStatus = true;

            for (int i = 0; i < listtmschekInstall.Count; i++)
            {

                if ((listtmschekInstall[i].MerchantName == "") || (listtmschekInstall[i].MerchantName == null))
                {
                    FlagStatus = false;
                    a = (a == "") ? "MerchantName, " : "MerchantName, ";
                }

                if (listtmschekInstall[i].MerchantName == "N")
                {
                    FlagStatus = false;
                    a = (a == "") ? "MerchantName, " : "MerchantName, ";
                }
                if (listtmschekInstall[i].MerchantPassword == "" || listtmschekInstall[i].MerchantPassword == null)
                {
                    FlagStatus = false;
                    a += (a == "") ? "MerchantPassword, " : "MerchantPassword, ";
                }
                if (listtmschekInstall[i].MerchantPassword == "N")
                {
                    FlagStatus = false;
                    a += (a == "") ? "MerchantPassword, " : "MerchantPassword, ";
                }

                if (listtmschekInstall[i].MerchantID == "" || listtmschekInstall[i].MerchantID == null)
                {
                    FlagStatus = false;
                    a += (a == "") ? "MerchantID, " : "MerchantID, ";
                }

                if (listtmschekInstall[i].MerchantID == "N")
                {
                    FlagStatus = false;
                    a += (a == "") ? "MerchantID, " : "MerchantID, ";
                }

                if (listtmschekInstall[i].TerminalID == "" || listtmschekInstall[i].TerminalID == null)
                {
                    FlagStatus = false;
                    a += (a == "") ? "TerminalID, " : "TerminalID, ";
                }
                if (listtmschekInstall[i].TerminalID == "N")
                {
                    FlagStatus = false;
                    a += (a == "") ? "TerminalID, " : "TerminalID, ";
                }

                if (listtmschekInstall[i].Brand == "" || listtmschekInstall[i].Brand == null)
                {
                    FlagStatus = false;
                    a += (a == "") ? "Brand, " : "Brand, ";
                }
                if (listtmschekInstall[i].Brand == "N")
                {
                    FlagStatus = false;
                    a += (a == "") ? "Brand, " : "Brand, ";
                }

                if (listtmschekInstall[i].AppVersion == "" || listtmschekInstall[i].AppVersion == null)
                {
                    FlagStatus = false;
                    a += (a == "") ? "AppVersion, " : "AppVersion, ";
                }
                if (listtmschekInstall[i].AppVersion == "N")
                {
                    FlagStatus = false;
                    a += (a == "") ? "AppVersion, " : "AppVersion, ";
                }

                if (listtmschekInstall[i].EDCMenu == "" || listtmschekInstall[i].EDCMenu == null)
                {
                    FlagStatus = false;
                    a += (a == "") ? "EDCMenu, " : "EDCMenu, ";
                }
                if (listtmschekInstall[i].EDCMenu == "N")
                {
                    FlagStatus = false;
                    a += (a == "") ? "EDCMenu, " : "EDCMenu, ";
                }

                if (listtmschekInstall[i].HostMenu == "" || listtmschekInstall[i].HostMenu == null)
                {
                    FlagStatus = false;
                    a += (a == "") ? "HostMenu, " : "HostMenu, ";
                }
                if (listtmschekInstall[i].HostMenu == "N")
                {
                    FlagStatus = false;
                    a += (a == "") ? "HostMenu, " : "HostMenu, ";
                }

                if (listtmschekInstall[i].MultiMerchant == "" || listtmschekInstall[i].MultiMerchant == null)
                {
                    FlagStatus = false;
                    a += (a == "") ? "MultiMerchant, " : "MultiMerchant, ";
                }
                //if (listtmschekInstall[i].MultiMerchant == "N")
                //{
                //    FlagStatus = false;
                //    a += (a == "") ? "MultiMerchant, " : "MultiMerchant, ";
                //}

                if (listtmschekInstall[i].LogoMerchant == "" || listtmschekInstall[i].LogoMerchant == null)
                {
                    FlagStatus = false;
                    a += (a == "") ? "LogoMerchant, " : "LogoMerchant, ";
                }
                if (listtmschekInstall[i].LogoMerchant == "N")
                {
                    FlagStatus = false;
                    a += (a == "") ? "LogoMerchant, " : "LogoMerchant, ";
                }

                if (listtmschekInstall[i].LogoSlip == "" || listtmschekInstall[i].LogoSlip == null)
                {
                    FlagStatus = false;
                    a += (a == "") ? "LogoSlip, " : "LogoSlip, ";
                }
                if (listtmschekInstall[i].LogoSlip == "N")
                {
                    FlagStatus = false;
                    a += (a == "") ? "LogoSlip, " : "LogoSlip, ";
                }

                if (listtmschekInstall[i].SlipCode == "" || listtmschekInstall[i].SlipCode == null)
                {
                    FlagStatus = false;
                    a += (a == "") ? "SlipCode, " : "SlipCode, ";
                }
                if (listtmschekInstall[i].SlipCode == "N")
                {
                    FlagStatus = false;
                    a += (a == "") ? "SlipCode, " : "SlipCode, ";
                }


                if (FlagStatus == false)
                {
                    a = a.Remove(a.Trim().Length - 1);
                    msgreturn.InvalidItem = a;
                    msgreturn.StatusSuccess = "N";
                    msgreturn.RegisterSuccess = "ลงทะเบียนไม่สมบูรณ์";
                    lmsgreturn.Add(msgreturn);
                }
                else
                {
                    msgreturn.InvalidItem = "ไม่พบรายการที่ขาด";
                    msgreturn.StatusSuccess = "Y";
                    msgreturn.RegisterSuccess = "ลงทะเบียนสมบูรณ์";
                    lmsgreturn.Add(msgreturn);
                }
            }
            foreach (var resultitemV in lmsgreturn)
            {
                resultcheckInstall.messagereturninstall.Add(new MessageReturnInstall() { InvalidItem = resultitemV.InvalidItem, RegisterSuccess = resultitemV.RegisterSuccess, StatusSuccess = resultitemV.StatusSuccess });
            }
            return Ok<ResultCheckInstall>(resultcheckInstall);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertTMSTerminal")]
        public IHttpActionResult PostInsert([FromBody]TMSInsertHistory tmsinsertHistory)
        {
            ResutlTerminalHistory reulthistory = new ResutlTerminalHistory();
            MenuMobileDAO MenuTMSDao = new MenuMobileDAO();
            int? sum = MenuTMSDao.InsertTMSTerminalHistory(tmsinsertHistory);

            InsertTMSHistoryReturn resultjobMessage = new InsertTMSHistoryReturn();
            if (sum > 0)
            {
                resultjobMessage.tMSHistoryMessageReturns.Add(new TMSHistoryMessageReturn() { TerminalID = tmsinsertHistory.TerminalID, StatusSuccess = "Y", StatusMessage = "บันทึกข้อมูลเรียบร้อย" });
            }
            else
            {
                resultjobMessage.tMSHistoryMessageReturns.Add(new TMSHistoryMessageReturn() { TerminalID = tmsinsertHistory.TerminalID, StatusSuccess = "N", StatusMessage = "ไม่สามารถบันทึกข้อมูลได้" });
            }

            return Ok<InsertTMSHistoryReturn>(resultjobMessage);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateDevice")]
        public IHttpActionResult updateDevicereceiveTerminalSerialNumber([FromBody]UpdateDevice updateDevice)
        {
            UpdateDeviceModelReturn updateDeviceModelReturn = new UpdateDeviceModelReturn();
            MenuMobileDAO MenuTMSDao = new MenuMobileDAO();
            int sum = MenuTMSDao.UpdateTMSDevice(updateDevice);

            if (sum > 0)
            {
                updateDeviceModelReturn.updateDeviceMessageReturns.Add(new UpdateDeviceMessageReturn() { TerminalSerialNumber = updateDevice.TerminalSerialNumber, StatusSuccess = "Y", StatusMessage = "บันทึกข้อมูลเรียบร้อย" });
            }
            else
            {
                updateDeviceModelReturn.updateDeviceMessageReturns.Add(new UpdateDeviceMessageReturn() { TerminalSerialNumber = updateDevice.TerminalSerialNumber, StatusSuccess = "N", StatusMessage = "ไม่สามารถบันทึกข้อมูลได้" });
            }

            return Ok<UpdateDeviceModelReturn>(updateDeviceModelReturn);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetDeviceJobInformation")]
        public IHttpActionResult ServiceJobDevice([FromBody]ServiceJobDevice serviceJobDevice)
        {
            ResultServiceJobDevice resultServiceJobDevice = new ResultServiceJobDevice();
            List<ServiceJobDevice> lserviceJobDevice = new List<ServiceJobDevice>();
            MenuMobileDAO MenuTMSDao = new MenuMobileDAO();
            lserviceJobDevice = MenuTMSDao.GetServiceJobDevice(serviceJobDevice);

            foreach (var VjobDevice in lserviceJobDevice)
            {
                resultServiceJobDevice.ServiceJobDeviceList.Add(new ServiceJobDevice() { TerminalSerialNumber = VjobDevice.TerminalSerialNumber, JobStatus = VjobDevice.JobStatus, TypeOfJob = VjobDevice.TypeOfJob, Appversion = VjobDevice.Appversion });
            }

            return Ok<ResultServiceJobDevice>(resultServiceJobDevice);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateJobDeviceStatus")]
        public IHttpActionResult UpdateJobDeviceStatus([FromBody]GetJobStatus getjobstatus)
        {
            UpdateJobDeviceStatusReturn updatejobdevicestatusReturn = new UpdateJobDeviceStatusReturn();
            MenuMobileDAO MenuTMSDao = new MenuMobileDAO();
            int sum0 = MenuTMSDao.UpdateJobStatusTempJob(getjobstatus);
            int sum1 = MenuTMSDao.UpdateJobStatusTerminal(getjobstatus);

            if (sum0 > 0 && sum1 > 0)
            {
                updatejobdevicestatusReturn.updateJobDeviceStatusMessageReturn.Add(new UpdateJobDeviceStatusMessageReturn() { JobStatus = getjobstatus.JobStatus, StatusSuccess = "Y", StatusMessage = "บันทึกข้อมูลเรียบร้อย" });
            }
            else
            {
                updatejobdevicestatusReturn.updateJobDeviceStatusMessageReturn.Add(new UpdateJobDeviceStatusMessageReturn() { JobStatus = getjobstatus.JobStatus, StatusSuccess = "N", StatusMessage = "ไม่สามารถบันทึกข้อมูลได้" });
            }
            return Ok<UpdateJobDeviceStatusReturn>(updatejobdevicestatusReturn);
        }
    }
}
