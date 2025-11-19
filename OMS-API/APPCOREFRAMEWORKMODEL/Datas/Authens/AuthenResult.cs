using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.Authens
{
    public class AuthenResult
    {
        public List<Emp> Empployee { get; set; } = new List<Emp>();
    }
    public class EmpLoginn
    {
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string Role { get; set; }
    }
    public class Emp
    {
        public string EmpUsername { get; set; }
        public string EmpPassword { get; set; }
        public string EmpVendor { get; set; }
        public string EmpRole { get; set; }
        public string NameRole { get; set; }
        public string EmpCode { get; set; }
    }
    public class TMSGet
    {
        public string TerminalSerialNumber { get; set; }
        public string TerminalID { get; set; }
        public string MerchantID { get; set; }
    }
    public class TMSInsertHistory
    {
        public string TerminalID { get; set; }
        public string Model { get; set; }
        public string Version { get; set; }
        public string BankCode { get; set; }
        public string TermSerialNumber { get; set; }
        public string SimSerialNum { get; set; }
        public string IP { get; set; }
        public string CellID { get; set; }
        public string LastTimeConnect { get; set; }
        public string StartTimeDownload { get; set; }
        public string EndTimeDownload { get; set; }
        public string DownloadResultCode { get; set; }
        public string Detail { get; set; }
        public string MachineModel { get; set; }
        public string PTID { get; set; }
        public string HwVer { get; set; }
        public string PartNo { get; set; }
        public string LifeTime { get; set; }
        public string OsVer { get; set; }
        public string CreateDate { get; set; }
        public string CreateBy { get; set; }
    }
    public class ResultMerchant
    {
        public List<TMSMerchantResult> tmsmerchantResult = new List<TMSMerchantResult>();
    }
    public class TMSMerchantResult
    {
        public string TerminalID { get; set; }
        public string MerchantID { get; set; }
        public string MerchantName { get; set; }
    }
    public class TMSTypeOfJob
    {
        public string TypeOfJob { get; set; }
    }
    public class ResultMerchantProfile
    {
        public List<MerchantProfile> merchantProfile = new List<MerchantProfile>();
    }
    public class MerchantProfile
    {
        public string TerminalID { get; set; }
        public string MerchantID { get; set; }
        public string SerialNumber { get; set; }
        public string AppVersion { get; set; }
        public string TypeOfJob { get; set; }
        public string BankName { get; set; }
        public string MultiMerchant { get; set; }
        public string MerchantName { get; set; }
        public string MerchantPassword { get; set; }
        public string Brand { get; set; }
        public string Address { get; set; }
        public string ContactPhoneNo { get; set; }
        public string SlipCode { get; set; }
        public string SlipName { get; set; }
        public string SlipHeader { get; set; }
        public string SlipFooter { get; set; }
        public string LogoAppUrl { get; set; }
        public string LogoUrl { get; set; }
        public string LogoAppName { get; set; }
        public string TaxNo { get; set; }
    }
    public class ResultCheckInstall
    {
        public List<MessageReturnInstall> messagereturninstall { get; set; } = new List<MessageReturnInstall>();
    }
    public class MessageReturnInstall
    {
        public string StatusSuccess { get; set; }
        public string RegisterSuccess { get; set; }
        public string InvalidItem { get; set; }
    }
    public class TMSCheckInstall
    {
        public string TerminalSerialNumber { get; set; }
        public string TerminalID { get; set; }
        public string MerchantID { get; set; }
        public string MerchantName { get; set; }
        public string MerchantPassword { get; set; }
        public string Brand { get; set; }
        public string AppVersion { get; set; }
        public string EDCMenu { get; set; }
        public string HostMenu { get; set; }
        public string MultiMerchant { get; set; }
        public string LogoMerchant { get; set; }
        public string LogoSlip { get; set; }
        public string SlipCode { get; set; }
    }
    public class ResutlTerminalHistory
    {
        public List<TMSHistory> TMSHistoryList { get; set; } = new List<TMSHistory>();
    }
    public class TMSHistory
    {
        public string TerminalID { get; set; }
        public string Model { get; set; }
        public string Version { get; set; }
        public string BankCode { get; set; }
        public string TermSerialNumber { get; set; }
        public string SimSerialNum { get; set; }
        public string IP { get; set; }
        public string CellID { get; set; }
        public string LastTimeConnect { get; set; }
        public string StartTimeDownload { get; set; }
        public string EndTimeDownload { get; set; }
        public string DownloadResultCode { get; set; }
        public string Detail { get; set; }
        public string MachineModel { get; set; }
        public string PTID { get; set; }
        public string HwVer { get; set; }
        public string PartNo { get; set; }
        public string LifeTime { get; set; }
        public string OsVer { get; set; }
        public string CreateDate { get; set; }
        public string CreateBy { get; set; }
    }
    public class InsertTMSHistoryReturn
    {
        public List<TMSHistoryMessageReturn> tMSHistoryMessageReturns { get; set; } = new List<TMSHistoryMessageReturn>();
    }
    public class TMSHistoryMessageReturn
    {
        public string TerminalID { get; set; }
        public string StatusSuccess { get; set; }
        public string StatusMessage { get; set; }
    }
    public class UpdateDevice
    {
        public string TerminalSerialNumber { get; set; }
        public string RomVersion { get; set; }
        public string BuildNo { get; set; }
        public string IpAddress { get; set; }
        public string SubnetMask { get; set; }
        public string MacAddress { get; set; }
        public string SimSerial { get; set; }
        public string FreeMemory { get; set; }
        public string FreeDiskspace { get; set; }
        public string KernelVersion { get; set; }
        public string AndroidVersion { get; set; }
        public string AppVersion { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
    public class UpdateDeviceModelReturn
    {
        public List<UpdateDeviceMessageReturn> updateDeviceMessageReturns { get; set; } = new List<UpdateDeviceMessageReturn>();
    }
    public class UpdateDeviceMessageReturn
    {
        public string TerminalSerialNumber { get; set; }
        public string StatusSuccess { get; set; }
        public string StatusMessage { get; set; }
    }
    public class ServiceJobDevice
    {
        public string TerminalSerialNumber { get; set; }
        public string JobStatus { get; set; }
        public string TypeOfJob { get; set; }
        public string Appversion { get; set; }
    }
    public class ResultServiceJobDevice
    {
        public List<ServiceJobDevice> ServiceJobDeviceList { get; set; } = new List<ServiceJobDevice>();
    }
    public class GetJobStatus
    {
        public string TerminalSerialNumber { get; set; }
        public string JobStatus { get; set; }
    }
    public class UpdateJobDeviceStatusReturn
    {
        public List<UpdateJobDeviceStatusMessageReturn> updateJobDeviceStatusMessageReturn { get; set; } = new List<UpdateJobDeviceStatusMessageReturn>();
    }
    public class UpdateJobDeviceStatusMessageReturn
    {
        public string JobStatus { get; set; }
        public string StatusSuccess { get; set; }
        public string StatusMessage { get; set; }
    }
}
