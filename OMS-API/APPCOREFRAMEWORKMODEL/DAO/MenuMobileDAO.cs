using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APPCOREMODEL.Datas;
using APPCOREMODEL.DAO;
using System.Data.SqlClient;
using System.Data;
using APPHELPPERS;
using APPCOREMODEL.Datas.Authens;
namespace APPCOREMODEL.DAO
{
    public class MenuMobileDAO
    {
        public List<MenuMobile> GetMenu(EmpLogin empInfo)
        {

            string strcond = "";
            DataTable dt = new DataTable();
            var LMenuMobile = new List<MenuMobile>();

            try
            {
                string strsql = " select mmv.VendorID,mm.Id from MenuMobileVendor mmv " +
                                "	inner join MenuMobile MM on mm.Id = mmv.MenuMobile_ID " +
                                "	inner join Vendor v on v.VenderID = mmv.VendorID " +

                                "	where mmv.VendorID in('" + empInfo.VendorCode + "')  and mmv.RolePosition_ID = '" + empInfo.Role + "'" +

                                strcond;
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMenuMobile = (from DataRow dr in dt.Rows

                               select new MenuMobile()
                               {
                                   MenuCode = dr["Id"].ToString().Trim(),
                                   Vendorid = dr["Vendorid"].ToString().Trim()
                               }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMenuMobile;
        }
        public List<Emp> GetEmpLogin(EmpLoginn empInfo)
        {

            string strcond = "";
            DataTable dt = new DataTable();
            var LMenuMobile = new List<Emp>();

            try
            {
                string strsql = "  SELECT        ul.Username,ul.Password,er.RoleCode,evm.VendorCode,r.RoleName,er.EmpCode	" +
                             " FROM UserLogin AS ul " +
                             " inner join EmpRole as er on ul.EmpCode = er.EmpCode " +
                              "  INNER JOIN  Emp AS e ON e.EmpCode = er.EmpCode " +
                             " inner join EmpVendorMapping evm on evm.EmpCode = ul.EmpCode " +
                             " left join Role r on r.RoleCode = er.RoleCode " +
                              " WHERE(Username = '" + empInfo.username + "') AND(Password = '" + empInfo.password + "') AND (er.FlagDelete = 'N') AND (e.ActiveFlag = 'Y') AND (e.FlagDelete = 'N')" +

                                strcond;
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMenuMobile = (from DataRow dr in dt.Rows

                               select new Emp()
                               {
                                   EmpUsername = dr["Username"].ToString().Trim(),
                                   EmpPassword = dr["Password"].ToString().Trim(),
                                   EmpVendor = dr["VendorCode"].ToString().Trim(),
                                   NameRole = dr["RoleName"].ToString().Trim(),
                                   EmpRole = dr["RoleCode"].ToString().Trim(),
                                   EmpCode = dr["EmpCode"].ToString().Trim()
                               }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMenuMobile;
        }
        public List<Emp> GetEmp(String Empcode, string EmpVendor)
        {

            string strcond = "";
            DataTable dt = new DataTable();
            var LMenuMobile = new List<Emp>();

            try
            {
                string strsql = "  SELECT        ul.Username,ul.Password,er.RoleCode,evm.VendorCode,r.RoleName,evm.EmpCode	" +
                             " FROM UserLogin AS ul " +
                             " inner join EmpRole as er on ul.EmpCode = er.EmpCode " +
                              " INNER JOIN  emp as e on e.EmpCode = er.EmpCode " +
                             " inner join EmpVendorMapping evm on evm.EmpCode = ul.EmpCode " +
                             " left join Role r on r.RoleCode = er.RoleCode " +
                              " WHERE(evm.EmpCode = '" + Empcode + "') AND( evm.VendorCode = '" + EmpVendor + "')    and er.FlagDelete='N'  and e.ActiveFlag='Y' and e.FlagDelete='N'" +

                                strcond;
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMenuMobile = (from DataRow dr in dt.Rows

                               select new Emp()
                               {
                                   EmpUsername = dr["Username"].ToString().Trim(),
                                   EmpPassword = dr["Password"].ToString().Trim(),
                                   EmpVendor = dr["VendorCode"].ToString().Trim(),
                                   NameRole = dr["RoleName"].ToString().Trim(),
                                   EmpCode = dr["EmpCode"].ToString().Trim(),
                                   EmpRole = dr["RoleCode"].ToString().Trim()
                               }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMenuMobile;
        }
        public List<TMSMerchantResult> GetTMSMerchant(TMSGet tmsGet)
        {
            string strcond = "";
            DataTable dt = new DataTable();
            var LTMSMerchant = new List<TMSMerchantResult>();

            try
            {
                string strsql = "SELECT        TerminalSerialNumber, TerminalID, MerchantID, MerchantName " +
                                "FROM            Merchant " +
                                "WHERE        (TerminalSerialNumber = " +
                                "                             (SELECT DISTINCT TerminalSerialNumber " +
                                "                               FROM            Terminal" +
                                "                               WHERE        (TerminalSerialNumber = '" +
                                tmsGet.TerminalSerialNumber + "') AND (Active = 'Y'))) AND (Active = 'Y') AND (FlagDelete = 'N') " +
                                strcond;
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LTMSMerchant = (from DataRow dr in dt.Rows

                                select new TMSMerchantResult()
                                {
                                    TerminalID = dr["TerminalID"].ToString().Trim(),
                                    MerchantID = dr["MerchantID"].ToString().Trim(),
                                    MerchantName = dr["MerchantName"].ToString().Trim(),
                                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LTMSMerchant;
        }
        public List<TMSTypeOfJob> CheckMerchantTypeOfJob(TMSGet tmsGet)
        {
            string strcond = "";
            DataTable dt = new DataTable();
            var LTMSMerchantTypeOfJob = new List<TMSTypeOfJob>();

            try
            {
                string strsql = "SELECT       t.*          FROM Terminal as T " +
                                "WHERE        (t.TerminalID = '" +
                                tmsGet.TerminalID + "') AND (t.MerchantID = '" +
                                tmsGet.MerchantID + "') AND (t.TerminalSerialNumber = '" +
                                tmsGet.TerminalSerialNumber + "')" +
                                strcond;
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LTMSMerchantTypeOfJob = (from DataRow dr in dt.Rows

                                         select new TMSTypeOfJob()
                                         {
                                             TypeOfJob = dr["TypeOfJob"].ToString().Trim(),

                                         }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LTMSMerchantTypeOfJob;
        }
        public List<MerchantProfile> GetMerchantProfile(TMSGet tmsGet)
        {
            string strcond = "";
            DataTable dt = new DataTable();
            var LTIDMID = new List<MerchantProfile>();

            try
            {
                string strsql = "SELECT        t.TerminalSerialNumber, t.MerchantID, t.TerminalID, d.AppVersion, t.TypeOfJob, b.BankFullName, d.MultiMerchant, m.MerchantName, m.MerchantUser, m.ContactPhoneNo, m.TaxNo, m.Brand, lm.LogoAppUrl, lm.LogoAppName, es.LogoSlipUrl, es.LogoSlipName, m.MerchantPassword, m.Address, m.SubDistrict, m.District, m.Province, es.SlipCode, es.SlipName, es.Header, es.Footer " +
                                "FROM            Merchant AS m LEFT OUTER JOIN " +
                                "Terminal AS t ON t.TerminalSerialNumber = m.TerminalSerialNumber " +
                                "AND t.TerminalSerialNumber = '" + tmsGet.TerminalSerialNumber + "' AND t.MerchantID = '" + tmsGet.MerchantID + "' AND t.TerminalID = '" + tmsGet.TerminalID + "' AND m.Active = 'Y' AND m.FlagDelete = 'N' LEFT OUTER JOIN " +
                                "EDCSlip AS es ON es.SlipCode = m.SlipCode AND es.Active = 'Y' AND es.FlagDelete = 'N' LEFT OUTER JOIN " +
                                "EDCLogoMerchantMapping AS lm ON lm.MerchantID = m.MerchantID " +
                                "AND lm.TerminalId = '" + tmsGet.TerminalID + "' AND lm.MerchantID = '" + tmsGet.MerchantID + "' AND lm.Active = 'Y' AND lm.FlagDelete = 'N' LEFT OUTER JOIN " +
                                "Device AS d ON d.DeviceSerialNumber = t.TerminalSerialNumber AND d.Active = 'Y' AND d.FlagDelete = 'N' LEFT OUTER JOIN " +
                                "Bank AS b ON b.BankCode = m.BankCode AND b.FlagDelete = 'N' " +
                                "WHERE        (m.TerminalSerialNumber = " +
                                "(SELECT DISTINCT TerminalSerialNumber FROM Terminal " +
                                "WHERE        (TerminalSerialNumber = '" + tmsGet.TerminalSerialNumber + "') AND (Active = 'Y'))) AND (m.MerchantID = '" +
                                tmsGet.MerchantID + "') AND (m.TerminalID = '" + tmsGet.TerminalID + "')" +
                                strcond;
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LTIDMID = (from DataRow dr in dt.Rows

                           select new MerchantProfile()
                           {
                               TerminalID = dr["TerminalID"].ToString().Trim(),
                               SerialNumber = dr["TerminalSerialNumber"].ToString().Trim(),
                               MerchantID = dr["MerchantID"].ToString().Trim(),
                               TypeOfJob = dr["TypeOfJob"].ToString().Trim(),
                               BankName = dr["BankFullName"].ToString().Trim(),
                               MultiMerchant = dr["MultiMerchant"].ToString().Trim(),
                               MerchantName = dr["MerchantName"].ToString().Trim(),
                               MerchantPassword = dr["MerchantPassword"].ToString().Trim(),
                               ContactPhoneNo = dr["ContactPhoneNo"].ToString().Trim(),
                               Address = dr["Address"].ToString().Trim() + " " + dr["SubDistrict"].ToString().Trim() + " " + dr["District"].ToString().Trim() + dr["Province"].ToString().Trim(),
                               SlipCode = dr["SlipCode"].ToString().Trim(),
                               SlipName = dr["SlipName"].ToString().Trim(),
                               SlipHeader = dr["Header"].ToString().Trim(),
                               SlipFooter = dr["Footer"].ToString().Trim(),
                               LogoAppUrl = dr["LogoAppUrl"].ToString().Trim(),
                               LogoAppName = dr["LogoAppName"].ToString().Trim(),
                               LogoUrl = dr["LogoSlipName"].ToString().Trim(),
                               TaxNo = dr["TaxNo"].ToString().Trim(),
                               AppVersion = dr["AppVersion"].ToString().Trim(),
                               Brand = dr["Brand"].ToString().Trim(),

                           }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LTIDMID;
        }
        public List<TMSCheckInstall> CheckMerchantInstall(TMSGet tmsGet)
        {
            string strcond = "";
            DataTable dt = new DataTable();
            var LTMSCheck = new List<TMSCheckInstall>();

            try
            {
                string strsql = "SELECT m.TerminalSerialNumber, m.MerchantName, m.MerchantPassword, m.MerchantID, m.TerminalID, m.Brand, d.AppVersion, m.EDCMenu, " +
                                "m.HOSTMenu, d.MultiMerchant, m.LogoMerchant, m.LogoSlip, m.SlipCode " +
                                "FROM            Merchant AS m LEFT OUTER JOIN " +
                                "Device AS d ON d.DeviceSerialNumber = m.TerminalSerialNumber " +
                                "WHERE        (m.TerminalSerialNumber = '" + tmsGet.TerminalSerialNumber + "') AND (m.TerminalID = '" + tmsGet.TerminalID +
                                "') AND (m.MerchantID = '" + tmsGet.MerchantID + "') " +
                                " AND (m.Active = 'Y') AND (m.FlagDelete = 'N') AND (d.Active = 'Y') AND (d.FlagDelete = 'N') " +
                                strcond;
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LTMSCheck = (from DataRow dr in dt.Rows

                             select new TMSCheckInstall()
                             {
                                 TerminalSerialNumber = dr["TerminalSerialNumber"].ToString().Trim(),
                                 MerchantName = dr["MerchantName"].ToString().Trim(),
                                 MerchantPassword = dr["MerchantPassword"].ToString().Trim(),
                                 MerchantID = dr["MerchantID"].ToString().Trim(),
                                 TerminalID = dr["TerminalID"].ToString().Trim(),
                                 Brand = dr["Brand"].ToString().Trim(),
                                 AppVersion = dr["AppVersion"].ToString().Trim(),
                                 EDCMenu = dr["EDCMenu"].ToString().Trim(),
                                 HostMenu = dr["HostMenu"].ToString().Trim(),
                                 MultiMerchant = dr["MultiMerchant"].ToString().Trim(),
                                 LogoMerchant = dr["LogoMerchant"].ToString().Trim(),
                                 LogoSlip = dr["LogoSlip"].ToString().Trim(),
                                 SlipCode = dr["SlipCode"].ToString().Trim(),

                             }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LTMSCheck;
        }
        public int? InsertTMSTerminalHistory(TMSInsertHistory TinsertInfo)
        {
            int? i = 0;
            String strsql = "insert into TerminalDownloadHistory (TerminalID, Model, Version, BankCode, TermSerialNumber, SIMSerialNumber, IP, CellID, LastTimeConnect, StartTimeDownload, EndTimeDownload, DownloadResultCode, Detail, MachineModel, PTID, HwVer, PartNo, LifeTime, OsVer, CreateDate, CreateBy) values (" +
                             "'" + TinsertInfo.TerminalID + "', " +
                             "'" + TinsertInfo.Model + "', " +
                             "'" + TinsertInfo.Version + "', " +
                             "'" + TinsertInfo.BankCode + "', " +
                             "'" + TinsertInfo.TermSerialNumber + "', " +
                             "'" + TinsertInfo.SimSerialNum + "', " +
                             "'" + TinsertInfo.IP + "', " +
                             "'" + TinsertInfo.CellID + "', " +
                             "'" + TinsertInfo.LastTimeConnect + "', " +
                             "'" + TinsertInfo.StartTimeDownload + "', " +
                             "'" + TinsertInfo.EndTimeDownload + "', " +
                             "'" + TinsertInfo.DownloadResultCode + "', " +
                             "'" + TinsertInfo.Detail + "', " +
                             "'" + TinsertInfo.MachineModel + "', " +
                             "'" + TinsertInfo.PTID + "', " +
                             "'" + TinsertInfo.HwVer + "', " +
                             "'" + TinsertInfo.PartNo + "', " +
                             "'" + TinsertInfo.LifeTime + "', " +
                             "'" + TinsertInfo.OsVer + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                             "'" + TinsertInfo.CreateBy + "')";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdateTMSDevice(UpdateDevice updateDevice)
        {
            int i = 0;
            string strsql = " Update Device set ";

            if ((updateDevice.RomVersion != null) && (updateDevice.RomVersion != ""))
            {
                strsql += " RomVersion = '" + updateDevice.RomVersion + "',";
            }
            if ((updateDevice.BuildNo != null) && (updateDevice.BuildNo != ""))
            {
                strsql += " BuildNo = '" + updateDevice.BuildNo + "',";
            }
            if ((updateDevice.IpAddress != null) && (updateDevice.IpAddress != ""))
            {
                strsql += " IpAddress = '" + updateDevice.IpAddress + "',";
            }
            if ((updateDevice.SubnetMask != null) && (updateDevice.SubnetMask != ""))
            {
                strsql += " SubnetMask = '" + updateDevice.SubnetMask + "',";
            }
            if ((updateDevice.SimSerial != null) && (updateDevice.SimSerial != ""))
            {
                strsql += " SimSerial = '" + updateDevice.SimSerial + "',";
            }
            if ((updateDevice.MacAddress != null) && (updateDevice.MacAddress != ""))
            {
                strsql += " MacAddress = '" + updateDevice.MacAddress + "',";
            }
            if ((updateDevice.FreeMemory != null) && (updateDevice.FreeMemory != ""))
            {
                strsql += " FreeMemory = '" + updateDevice.FreeMemory + "',";
            }
            if ((updateDevice.FreeDiskspace != null) && (updateDevice.FreeDiskspace != ""))
            {
                strsql += " FreeDiskspace = '" + updateDevice.FreeDiskspace + "',";
            }
            if ((updateDevice.KernelVersion != null) && (updateDevice.KernelVersion != ""))
            {
                strsql += " KernelVersion = '" + updateDevice.KernelVersion + "',";
            }
            if ((updateDevice.AndroidVersion != null) && (updateDevice.AndroidVersion != ""))
            {
                strsql += " AndroidVersion = '" + updateDevice.AndroidVersion + "',";
            }
            if ((updateDevice.AppVersion != null) && (updateDevice.AppVersion != ""))
            {
                strsql += " AppVersion = '" + updateDevice.AppVersion + "',";
            }
            if ((updateDevice.Latitude != null) && (updateDevice.Latitude != ""))
            {
                strsql += " Latitude = '" + updateDevice.Latitude + "',";
            }
            if ((updateDevice.Longitude != null) && (updateDevice.Longitude != ""))
            {
                strsql += " Longitude = '" + updateDevice.Longitude + "',";
            }
            if ((updateDevice.Description != null) && (updateDevice.Description != ""))
            {
                strsql += " Description = '" + updateDevice.Description + "',";
            }
            if ((updateDevice.Note != null) && (updateDevice.Note != ""))
            {
                strsql += " Note = '" + updateDevice.Note + "',";
            }
            if ((updateDevice.UpdateBy != null) && (updateDevice.UpdateBy != ""))
            {
                strsql += " UpdateBy = '" + updateDevice.UpdateBy + "',";
            }

            strsql += " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +

                    " where DeviceSerialNumber = '" + updateDevice.TerminalSerialNumber + "'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public List<ServiceJobDevice> GetServiceJobDevice(ServiceJobDevice sjobDevice)
        {
            string strcond = "";
            DataTable dt = new DataTable();
            var LJobDevice = new List<ServiceJobDevice>();

            try
            {
                string strsql = "SELECT        EdcSerialNo, JobStatus, TypeOfJob, RequestApplication AS Appversion " +
                                "FROM            TempJobManagement " +
                                "WHERE        (Id = " +
                                "                      (SELECT        MAX(Id) AS id " +
                                "FROM            TempJobManagement AS TempJobManagement_1 " +
                                "WHERE        (EdcSerialNo = '" + sjobDevice.TerminalSerialNumber +
                                "') AND (JobStatus = '" + sjobDevice.JobStatus + "')))" +
                                strcond;
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LJobDevice = (from DataRow dr in dt.Rows

                              select new ServiceJobDevice()
                              {
                                  TerminalSerialNumber = dr["EdcSerialNo"].ToString().Trim(),
                                  JobStatus = dr["JobStatus"].ToString().Trim(),
                                  TypeOfJob = dr["TypeOfJob"].ToString().Trim(),
                                  Appversion = dr["Appversion"].ToString().Trim(),

                              }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LJobDevice;
        }
        public int UpdateJobStatusTempJob(GetJobStatus getjobstatus)
        {
            int i = 0;
            string strsql = " Update TempJobManagement set ";

            if ((getjobstatus.JobStatus != null) && (getjobstatus.JobStatus != ""))
            {
                strsql += " JobStatus = '" + getjobstatus.JobStatus + "',";
            }

            strsql += " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +

                      " where (EdcSerialNo = '" + getjobstatus.TerminalSerialNumber + "')";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdateJobStatusTerminal(GetJobStatus getjstatus)
        {
            int i = 0;
            string strsql = " Update Terminal set ";

            if ((getjstatus.JobStatus != null) && (getjstatus.JobStatus != ""))
            {
                strsql += " JobStatus = '" + getjstatus.JobStatus + "',";
            }

            strsql += " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +

                    " where (TerminalSerialNumber = '" + getjstatus.TerminalSerialNumber + "')";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
    }
}
