using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas
{
    public class ModelJobadssign
    {
        public List<JobAssignDetail> JobAssign { get; set; } = new List<JobAssignDetail>();
    
    }
    public class ModelJobadssignMessageReturn
    {
     
        public List<JobMessageReturn> JobAssignMessageReturn { get; set; } = new List<JobMessageReturn>();
    }
    public class ModelJobkeepfile
    {

        public List<JobMessageReturnFile> JobKeepMessageReturnfile { get; set; } = new List<JobMessageReturnFile>();
    }
    public class ModelTokenMobileMessage
    {

        public List<JobMessageTokenMobile> Token_MobileMessageReturn { get; set; } = new List<JobMessageTokenMobile>();
    }

    public class ModelJobTokenbile
    {
        public List<JobTokenMobile> JobTokenDetail { get; set; } = new List<JobTokenMobile>();

    }
    public class JobAssignDetail
    {
        public string JobID { get; set; }
        public string TypeOfJob { get; set; }
        public string AcqBank { get; set; }
        public string JobStatus { get; set; }
        public string JobStatusName { get; set; }

        public string JOBITEMSTATUS { get; set; }
        public string JOBITEMSTATUS_Name { get; set; }
        public string MerchanID { get; set; }
        public string MerchanName { get; set; }
        public string TerminalId { get; set; }

        public string TerminalAddress { get; set; }
        public string TerminalContract { get; set; }
        public string Brand { get; set; }
        public string EdcSerialNo { get; set; }
        //next Column
        public string jobclass { get; set; }
        public string JobclassName { get; set; }
        public string RequestDate { get; set; }
        public string JOBPROBLEMSTATUS { get; set; }
        public string JOBPROBLEMSTATUS_Name { get; set; }
        public List<JobItem> Item { get; set; }
        public string TrackingNo { get; set; }

        public string TerminalPhone { get; set; }
        public string Model { get; set; }
        public string jobNote { get; set; }
        public string assignto { get; set; }
        public List<JobHistory> JobHistory { get; set; }
        public string Gitem { get; set; }

        public string Vendor { get; set; }
        public string checkitemrecieve { get; set; }
        public string ServiceDate { get; set; }
        public string SerialSim { get; set; }
    }
    public class JobHistory
    {
        public string JOBSTATUS { get; set; }
        public string JOBSTATUS_NAME { get; set; }
        public string JOBITEMSTATUS { get; set; }
        public string JOBITEMSTATUS_NAME { get; set; }
        public string JOBPROBLEMSTATUS { get; set; }
        public string JOBPROBLEMSTATUS_NAME { get; set; }
        public string JOBID { get; set; }
        public string NOTE { get; set; }
        public string JOBTYPE { get; set; }
        public string JOBDate { get; set; }
        public string NameActor { get; set; }
    }
    public class JobItem
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string Amount { get; set; }
    }
    public class JobItemTechnician
    {
        public string ItemID { get; set; }
        public string GroupItemID { get; set; }    
        public string ItemRecieve { get; set; }
        public string ItmeIncomplete { get; set; } 
        public string Note { get; set; }
    }
    public class JobMessageReturn
    {
        public string JOBID { get; set; }
        public string StatusSuccess { get; set; }
        public string StatusMessage { get; set; }
    }
    public class JobKeepPic
    {
        public string JOBID { get; set; }
        public string NameFile { get; set; }
        public string Note { get; set; }
    }
    public class JobMessageReturnFile
    {
        public string JOBID { get; set; }
        public string StatusSuccess { get; set; }
        public string StatusMessage { get; set; }
    }
    public class Jobupload
    {
        public string namefile { get; set; }
        public string oldnamefile { get; set; }
        public string pathfile { get; set; }
       
    }
 
    public class JobMessageTokenMobile
    {
        //public string id { get; set; }
        public string StatusSuccess { get; set; }
        public string StatusMessage { get; set; }
        public string Token { get; set; }
        //public string Serial_Mobile { get; set; }
        //public string active { get; set; }
        //public string Vendor { get; set; }
    }

    public class JobTokenMobile
    {
        public string Tokenid { get; set; }
        public string Token { get; set; }
        public string Serial_Mobile { get; set; }
        public string Phone_Mobile { get; set; }
        public string active { get; set; }
        public string Vendor { get; set; }
        
    }
}
