using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas
{
    public class ModelDemo4
    {
        public List<MasterLookup> masterlookupStatus { get; set; } = new List<MasterLookup>();
        public List<MenuMobile> MenuVendor { get; set; } = new List<MenuMobile>();
        public List<GroupItemDetail> GroupItem { get; set; } = new List<GroupItemDetail>();
        public List<LogoVendor> LogoV { get; set; } = new List<LogoVendor>();
        public List<ConfigMaster> ConfigMastersV { get; set; } = new List<ConfigMaster>();
        public List<PathUpload> PathUploadV { get; set; } = new List<PathUpload>();
    }
    public class ModelPermission
    {
        public string pname { get; set; }
        public int pstatus { get; set; }
    }
    public class MasterLookup
    {
        public int lookup_id { get; set; }
        public string LookupCode { get; set; }
        public string LookupType { get; set; }
        public string LookupValue { get; set; }
        public string RefferenceJobstatus { get; set; }
    }
    public class MenuMobile
    {
        public string MenuCode { get; set; }
        public string Vendorid { get; set; }
    }
    public class EmpLogin
    {
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string Role { get; set; }
        public string EmpCode { get; set; }
        public string VendorCode { get; set; }
    }

    public class GroupItemDetail
    {
        public string GroupCode { get; set; }
        public string GroupName { get; set; }

        public string ItemName { get; set; }
        public string ItemID { get; set; }
        public string Amount { get; set; }

    }

    public class LogoVendor
    {
        public string id { get; set; }
        public string MediaBase64 { get; set; }
        public string VendorCode { get; set; }
    }


    public class ConfigMaster
    {
        public string NameTB { get; set; }
        public string Version { get; set; }
        public string Vendor { get; set; }
        public string Role { get; set; }

    }
    public class UpdateJobStatusID
    {
        public string serial { get; set; }
        public List<JOBASSIGN> JobAssign { get; set; }
        public string Success { get; set; }
    }
    public class JOBASSIGN
    {
        public int jobid { get; set; }
        public string jobstatus { get; set; }
        public string success { get; set; }
        public string jobnote { get; set; }
    }
    public class UpdateJobStatusIDTecnicain
    {
        public string serial { get; set; }
        public string Success { get; set; }
        public List<JOBASSIGNTechnician> JobAssign { get; set; }
      
    }
    public class JOBASSIGNTechnician
    {
        public string jobid { get; set; }
        public string jobstatus { get; set; }
        public string jobitemstatus { get; set; }
        public string jobproblemstatus { get; set; }
        public string JOBBotRejectCode { get; set; }
        public string checkitemrecieve { get; set; }
        public string jobnote { get; set; }
        public string success { get; set; }
        public List<JobItemTechnicianRecieve> ItemRecieve { get; set; }
    }
    public class JobItemTechnicianRecieve
    {
        public string ItemID { get; set; }
      
        public string ItemRecieve { get; set; }
        public string ItemIncomplete { get; set; }
    }

    public class UpdateJobStatusIDTecnicainMeetCustomer
    {
        public string serial { get; set; }
        public string Success { get; set; }
        public List<JOBASSIGNTechnicianMeetCustomer> JobAssign { get; set; }
    
    }
    public class JOBASSIGNTechnicianMeetCustomer
    {
        public string jobid { get; set; }
        public string jobstatus { get; set; }
        public string jobitemstatus { get; set; }
        public string jobproblemstatus { get; set; }
        public string jobnote { get; set; }
        public string success { get; set; } 
        public string DateMeet { get; set; }
        public string TimeMeet { get; set; }
    }
    public class InsertJobFile
    {
        public string serial { get; set; }
        public List<JOBASSIGNTechnicianSuccess> JOBASSIGNTechnicianSuccess { get; set; }

        

    }
    public class UpdateJobFileNotComplete
    {
        public string serial { get; set; }
        public List<JOBASSIGNTechnicianSuccessUpdate> JOBASSIGNTechnicianSuccess { get; set; }



    }
    public class JOBASSIGNJOBKEEPFILE
    {
        public string jobid { get; set; }
        public string namefile { get; set; }
        public string Typefile { get; set; }
        public string notefile { get; set; }
    }
    public class JOBASSIGNJOBKEEPFILEUpdate
    {
        public string jobid { get; set; }
        public string namefile { get; set; }
        public string Typefile { get; set; }
        public string notefile { get; set; }
    }
    public class JOBASSIGNTechnicianSuccess
    {
        public string jobid { get; set; }
        public string jobstatus { get; set; }
        public string jobitemstatus { get; set; }
        public string jobproblemstatus { get; set; }
        public string JOBBoiRejectCode { get; set; }
        public string jobnote { get; set; }
        public string success { get; set; }
        public string LatLong { get; set; }
        public string JOBBoiNote { get; set; }
        public string JobProblemNote { get; set; }
        public List<JOBASSIGNJOBKEEPFILE> JobAssignKeepfile { get; set; }
        public List<JobItemTechnicianRecieve> ItemRecieve { get; set; }
    }
    public class PathUpload
    {
       
        public string Path { get; set; }
    }
    public class JOBASSIGNTechnicianSuccessUpdate
    {
        public string jobid { get; set; }
        public string jobstatus { get; set; }
        public string jobitemstatus { get; set; }
        public string jobproblemstatus { get; set; }
        public string JOBBoiRejectCode { get; set; }
        public string jobnote { get; set; }
        public string success { get; set; }
        public string LatLong { get; set; }
        public string JOBBoiNote { get; set; }
        public string JobProblemNote { get; set; }
        public List<JOBASSIGNJOBKEEPFILEUpdate> JobAssignKeepfile { get; set; }
    }
    public class JobModelToken_Mobile
    {
        public string Token { get; set; }
        public string Serial_Mobile { get; set; }
        public string Createdate { get; set; }
        public string UpdateDate { get; set; }
        public string Phone_Mobile { get; set; }
        public string UpdateBy { get; set; }
        public string FlagDelete { get; set; }
        public string active { get; set; }
        public string Vendor { get; set; }
    }
}
