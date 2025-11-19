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
using System.Globalization;
namespace APPCOREMODEL.DAO
{
public	class JobAssignDAO
    {
		public List<JobAssignDetail> GetJobAssign(string status,string itemstatus,string vendercode,string Empcode)
		{

			string strcond = "", strcondL="";
			DataTable dt = new DataTable();
			var LJobDetail = new List<JobAssignDetail>();
            
            try
			{
                if (status != "")
                {
                    strcond += " and  ja.status in (" + status + ")";
                }
                if (itemstatus != "")
                {
                    strcond += " and  ja.JOBITEMSTATUS in (" + itemstatus + ")";
                }
             
                if (Empcode != "")
                {
                    strcond += " and  ja.AssignTo in ('" + Empcode + "')";
                }
                if (vendercode != "")
                {
                    strcond += " and  ja.Vendor in ('" + vendercode + "')";
                }
                strcond += " order by ja.ServiceDate,ja.id desc";

                string strsql = "SELECT        ja.Id, ja.AcqBank, ja.TypeOfJob, ja.EdcSerialNo,ja.Status as jobstatus,Ljobstatus.LookupValue as jobstatusname, "+
                           " ja.JOBITEMSTATUS,LJOBITEMSTATUS.LookupValue as JOBITEMSTATUSNAME, ja.JOBPROBLEMSTATUS,LJOBPROBLEMSTATUS.LookupValue as JOBPROBLEMSTATUSNAME," +
                    
                            "ja.Line1,ja.Line2,ja.Line3,ja.MerchantId, ja.NameTh AS merchanname,ja.Brand, "+
                            "ja.Item, ja.Class AS jobclass, ja.AssignTo,ja.RequestDate,ja.TackingNo, "+
                            " ja.ContactPhoneNo,ja.model,ja.JobNote,ja.ContactName,LJOBCLASS.LookupValue as JobclassName,ja.Vendor,ja.checkitemrecieve, CASE WHEN (ja.ServiceDate != '') THEN CONVERT(varchar(16),  CONVERT(datetime,ja.ServiceDate, 105),23) + SPACE(1)+ ja.ServiceTime END AS ServiceDate" +

                             ",ja.SerialNo as SerialSim ,CASE  WHEN(ja.TidKtbOnUs != '')   THEN ja.TidKtbOnUs  " +
                              "  when ja.TerminalId != '' and ja.TidKtbOnUs = ''  then ja.TerminalId  "+
                              "  when ja.TidBase24Eps != '' and ja.TidKtbOnUs = '' and ja.TerminalId = '' then ja.TidBase24Eps  "+
                              "  when  ja.TerminalQrCode != '' and ja.TidKtbOnUs = '' and ja.TerminalId = '' and ja.TidBase24Eps = ''then ja.TerminalQrCode  "+
                              "else  ja.TidKtbOnUs  end as TerminalId  "+
                           " FROM            JobAssign AS ja  " +
                           " left join Lookup as Ljobstatus on Ljobstatus.LookupCode = ja.Status and Ljobstatus.LookupType = 'JOBSTATUS'  "+
                           " left join Lookup as LJOBITEMSTATUS on LJOBITEMSTATUS.LookupCode = ja.JOBITEMSTATUS and LJOBITEMSTATUS.LookupType = 'JOBITEMSTATUS'  "+
                           " left join Lookup as LJOBPROBLEMSTATUS on LJOBPROBLEMSTATUS.LookupCode = ja.JOBPROBLEMSTATUS and LJOBPROBLEMSTATUS.LookupType = 'JOBPROBLEMSTATUS' "+
                            " LEFT OUTER JOIN Lookup AS LJOBCLASS ON LJOBCLASS.LookupCode = ja.Class AND LJOBCLASS.LookupType = 'JOBCLASS'  "+
                            "WHERE(ja.TerminalId IS NOT NULL) " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());        
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                string aaa = dt.Rows[0]["checkitemrecieve"].ToString();
                LJobDetail = (from DataRow dr in dt.Rows

                              select new JobAssignDetail()
                              {
                                  JobID = dr["Id"].ToString().Trim(),
                                  TypeOfJob = dr["TypeOfJob"].ToString().Trim(),
                                  AcqBank = dr["acqbank"].ToString().Trim(),
                                  JobStatus = dr["jobstatus"].ToString().Trim(),
                                  JobStatusName = dr["jobstatusname"].ToString().Trim(),

                                  JOBITEMSTATUS = dr["JOBITEMSTATUS"].ToString().Trim(),
                                  JOBITEMSTATUS_Name = dr["JOBITEMSTATUSNAME"].ToString().Trim(),
                                  MerchanID = dr["MerchantId"].ToString().Trim(),
                                  MerchanName = dr["merchanname"].ToString().Trim(),
                                  TerminalId = dr["TerminalId"].ToString().Trim(),

                                  TerminalAddress = dr["Line1"].ToString().Trim() + " " + dr["Line2"].ToString().Trim() + " " + dr["Line3"].ToString().Trim(),
                                  TerminalContract = dr["ContactName"].ToString().Trim(),
                                  Brand = dr["Brand"].ToString().Trim(),
                                  EdcSerialNo = dr["EdcSerialNo"].ToString().Trim(),

                                  jobclass = dr["jobclass"].ToString().Trim(),
                                  JobclassName = dr["JobclassName"].ToString().Trim(),
                                  RequestDate = dr["RequestDate"].ToString().Trim(),
                                  JOBPROBLEMSTATUS = dr["JOBPROBLEMSTATUS"].ToString().Trim(),
                                  JOBPROBLEMSTATUS_Name = dr["JOBPROBLEMSTATUSNAME"].ToString().Trim(),
                                  TrackingNo = dr["TackingNo"].ToString().Trim(),

                                  TerminalPhone = dr["ContactPhoneNo"].ToString().Trim(),
                                  Model = dr["model"].ToString().Trim(),
                                  jobNote = dr["JobNote"].ToString().Trim(),
                                  assignto = dr["AssignTo"].ToString().Trim(),
                                  Gitem = dr["item"].ToString().Trim(),

                                  Vendor = dr["Vendor"].ToString().Trim(),
                                  checkitemrecieve = (dr["checkitemrecieve"].ToString()!="" ) ? dr["checkitemrecieve"].ToString() : "00",
                                  ServiceDate = (dr["ServiceDate"] != null || dr["ServiceDate"].ToString() != "") ? dr["ServiceDate"].ToString() : "",
                                  SerialSim = dr["SerialSim"].ToString().Trim(),



                              }).ToList();

            }
            catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}

			return LJobDetail;
		}
        public List<JobItem> GetJobItem(string item)
        {

            string strcond = "";
            DataTable dt = new DataTable();
            var LJobItemDetail = new List<JobItem>();

            try
            {
                if (item != "")
                {
                    strcond += " and  ja.item = '" + item + "'";
                }
               
                string strsql = "select distinct  i.Id,i.ItemName,gid.Amount from JobAssign ja" +
                                " inner join GroupItemDetail gid on gid.GroupCode = ja.Item  " +
                                " inner join Item i on gid.ItemID = i.Id " +
                                "  where gid.FlagDelete = 'N' and i.FlagDelete = 'N' " + strcond;

                

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LJobItemDetail = (from DataRow dr in dt.Rows

                              select new JobItem()
                              {
                                  ItemID = dr["Id"].ToString().Trim(),
                                  ItemName = dr["ItemName"].ToString().Trim(),
                                  Amount = dr["Amount"].ToString().Trim()
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LJobItemDetail;
        }
        public List<JobHistory> GetJobHistory(string jobid)
        {

            string strcond = "";
            DataTable dt = new DataTable();
            var LJobItemDetail = new List<JobHistory>();

            try
            {
                if (jobid != "")
                {
                    strcond += "   jobid = '" + jobid + "'";
                }

                string strsql = "select jobstatus,JOBITEMSTATUS,LJOBITEMSTATUS.LookupValue as JOBITEMSTATUSname,jh.JOBPROBLEMSTATUS,LJOBPROBLEMSTATUS.LookupValue as JOBPROBLEMSTATUSname,JOBID,JOBTYPE,e. EmpFname_TH + ' ' + e.EmpLName_TH  as NameActor" +

                                "   ,     CASE   WHEN jh.JOBSTATUS = '02' AND " +
                               "   jh.JOBITEMSTATUS = '02' THEN LJOBITEMSTATUS.LookupValue + 'ให้ ช่าง' " +
                                "    WHEN jh.JOBSTATUS = '01' AND " +
                              "    jh.JOBITEMSTATUS = '02' THEN LJOBITEMSTATUS.LookupValue + 'ให้ Vendor' " +
                              "    WHEN jh.JOBSTATUS = '02' AND " +
                              "    jh.JOBITEMSTATUS = '05' THEN 'ช่าง' + LJOBITEMSTATUS.LookupValue " +
                              "         WHEN jh.JOBSTATUS = '09' AND " +
                              "    jh.JOBITEMSTATUS = '05' THEN 'ช่าง' + LJOBITEMSTATUS.LookupValue " +
                              "         WHEN jh.JOBSTATUS = '06' AND " +
                              "    jh.JOBITEMSTATUS = '05' THEN 'ช่าง' + LJOBITEMSTATUS.LookupValue " +
                              "    ELSE LJOBITEMSTATUS.LookupValue END AS jobstatusname " +
                                " ,	 CASE WHEN jh.JOBSTATUS = '06' AND "+
                                " jh.JOBITEMSTATUS = '05' then  "+
                                " 'นัด' + jh.ServiceDate+' - '+jh.NOTE ELSE jh.NOTE END AS note  " +
                            "	, convert(varchar, jh.CreateDate, 103)+''+convert(VARCHAR(8), jh.CreateDate, 14) as CreateDate "+
                            "  from JobHistory jh " +
                              "  left join Lookup as Ljobstatus on Ljobstatus.LookupCode = jh.JOBSTATUS and Ljobstatus.LookupType = 'JOBSTATUS'  "+
                              " left join Lookup as LJOBITEMSTATUS on LJOBITEMSTATUS.LookupCode = jh.JOBITEMSTATUS and LJOBITEMSTATUS.LookupType = 'JOBITEMSTATUS'  "+
                              "  left join Lookup as LJOBPROBLEMSTATUS on LJOBPROBLEMSTATUS.LookupCode = jh.JOBPROBLEMSTATUS and LJOBPROBLEMSTATUS.LookupType = 'JOBPROBLEMSTATUS' "+
                              " 	LEFT OUTER JOIN Emp e on e.EmpCode = jh.CreateBy "+
                              " where " + strcond + "order by jh.CreateDate desc ";



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LJobItemDetail = (from DataRow dr in dt.Rows

                                  select new JobHistory()
                                  {
                                      JOBID = dr["JOBID"].ToString().Trim(),
                                      JOBSTATUS = dr["jobstatus"].ToString().Trim(),
                                      //JOBSTATUS_NAME = dr["jobstatusname"].ToString().Trim(),
                                      JOBSTATUS_NAME = dr["jobstatusname"].ToString().Trim(),
                                      JOBITEMSTATUS = dr["JOBITEMSTATUS"].ToString().Trim(),
                                      JOBITEMSTATUS_NAME = dr["JOBITEMSTATUSname"].ToString().Trim(),
                                      JOBPROBLEMSTATUS = dr["JOBPROBLEMSTATUS"].ToString().Trim(),
                                      JOBPROBLEMSTATUS_NAME = dr["JOBPROBLEMSTATUSname"].ToString().Trim(),
                                      JOBTYPE = dr["JOBTYPE"].ToString().Trim(),
                                      NOTE = dr["Note"].ToString().Trim(),
                                      JOBDate = dr["CreateDate"].ToString().Trim(),
                                      NameActor = dr["NameActor"].ToString().Trim()
                                  }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LJobItemDetail;
        }

        public int UpdateJob (string jobid, string Jstatus,string Jitems,string Jprobs,string jnote,string updateby,string checkitemrecieve)
        {
            int i = 0;
            string strsql = " Update JobAssign set ";

            if ((Jstatus != null) && (Jstatus != ""))
            {
                strsql += " Status = '" + Jstatus + "',";
            }
            if ((Jitems != null) && (Jitems != ""))
            {
                strsql += " JOBITEMSTATUS = '" + Jitems + "',";
            }
            if ((Jprobs != null) && (Jprobs != ""))
            {
                strsql += " JOBPROBLEMSTATUS = '" + Jprobs + "',";
            }
            if ((jnote != null) && (jnote != ""))
            {
                strsql += " JobNote = '" + jnote + "',";
            }
            if ((updateby != null) && (updateby != ""))
            {
                strsql += " UpdateBy = '" + updateby + "',";
            }
            if ((checkitemrecieve != null) && (checkitemrecieve != ""))
            {
                strsql += " checkitemrecieve = '" + checkitemrecieve + "',";
            }
            strsql += " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +

                    " where Id =" + jobid + "";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i= db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdateJobTechnician(string jobid, string Jstatus, string Jitems, string Jprobs,string JNote,List<JobItemTechnicianRecieve> JobItemTechnician,string JBoi,string checkitemrecieve,string updateby)
        {
            int i = 0;
            string strsql = " Update JobAssign set ";

            if ((Jstatus != null) && (Jstatus != ""))
            {
                strsql += " Status = '" + Jstatus + "',";
            }
            if ((Jitems != null) && (Jitems != ""))
            {
                strsql += " JOBITEMSTATUS = '" + Jitems + "',";
            }
            if ((Jprobs != null) && (Jprobs != ""))
            {
                strsql += " JOBPROBLEMSTATUS = '" + Jprobs + "',";
            }
            if ((JNote != null) && (JNote != ""))
            {
                strsql += " JobNote = '" + JNote + "',";
            }
            else
            {
                strsql += " JobNote = '',";
            }
            if ((updateby != null) && (updateby != ""))
            {
                strsql += " UpdateBy = '" + updateby + "',";
            }
            if ((JBoi != null) && (JBoi != ""))
            {
                strsql += " BotRejectCode = '" + JBoi + "',";
            }
            if ((checkitemrecieve != null) && (checkitemrecieve != ""))
            {
                strsql += " checkitemrecieve = '" + checkitemrecieve + "',";
            }
            strsql += " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +

                    " where Id =" + jobid + "";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            foreach (var JobV in JobItemTechnician.ToList())
            {
                int a = 0;
                string strsqla = " Update JobItem set ";

                if ((JobV.ItemRecieve != null) && (JobV.ItemRecieve != ""))
                {
                    strsqla += " ItemRecieve = '" + JobV.ItemRecieve + "',";
                }
            
                if ((Jstatus == "04") || Jitems == "07")
                {
                    strsqla += " FlagRecieve = 'N',";
                    if ((JobV.ItemIncomplete != null) && (JobV.ItemIncomplete != ""))
                    {
                        strsqla += " ItemIncomplete = '" + JobV.ItemIncomplete + "',";
                    }
                }
                else
                {

                    strsqla += " FlagRecieve = 'Y',";
                    strsqla += " ItemIncomplete = '0',";
                }


                strsqla += " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +

                        " where jobid ='" + jobid + "' and Item  = '" + JobV.ItemID + "' ";


                com.CommandText = strsqla;
                com.CommandType = System.Data.CommandType.Text;
                a = db.ExcuteBeginTransectionText(com);
            }


            return i;
        }
        public int UpdateJobTechnicianMeetCustomer(string jobid, string Jstatus, string Jitems, string Jprobs, string JNote, string datemeet, string Timemeet, string updateBy)
        {
            int i = 0;
            string strsql = " Update JobAssign set ";

            if ((Jstatus != null) && (Jstatus != ""))
            {
                strsql += " Status = '" + Jstatus + "',";
            }
            if ((Jitems != null) && (Jitems != ""))
            {
                strsql += " JOBITEMSTATUS = '" + Jitems + "',";
            }
            //else
            //{
            //    strsql += " JOBITEMSTATUS = '',";
            //}
            if ((Jprobs != null) && (Jprobs != ""))
            {
                strsql += " JOBPROBLEMSTATUS = '" + Jprobs + "',";
            }
            else
            {
                strsql += " JOBPROBLEMSTATUS = '',";
            }
            if ((JNote != null) && (JNote != ""))
            {
                strsql += " JobNote = '" + JNote + "',";
            }
            else
            {
                strsql += " JobNote = '',";
            }
            if ((datemeet != null) && (datemeet != ""))
            {
                strsql += "   ServiceDate =  '" + datemeet + "',";
         
            }
            if ((Timemeet != null) && (Timemeet != ""))
            {
                strsql += " ServiceTime = '" + Timemeet + "',";
            }
            if ((updateBy != null) && (updateBy != ""))
            {
                strsql += " UpdateBy = '" + updateBy + "',";
            }
      
            strsql += " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +

                    " where Id =" + jobid + "";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);
            
            return i;
        }
        public int inserJobKeepFile(string jobid, string namefile,string jnote,string TypeFile,string SerialDevice, string updateby)
        {
            int i = 0;
            string strsql = "INSERT INTO JobKeepFile  (job_id,Note,NameFile,CreateDate,TypeFile,SerialDevice,CreateBy)" +
                  "VALUES (" +
                 "'" + jobid + "'," +
                 "'" + jnote + "'," +
                 "'" + namefile + "'," +
                 "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                 "'" + TypeFile + "'," +
                 "'" + SerialDevice + "'," +
                 "'" + updateby + "'"+
                  ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateJobTechnicianJobSuccess(string jobid, string Jstatus, string Jprobs, string JNote, string JBoi,string JBoiNote,string LatLong,string JprobsNote, List<JobItemTechnicianRecieve> JobItemTechnician)
        {
            int i = 0;
            string strsql = " Update JobAssign set ";

            if ((Jstatus != null) && (Jstatus != ""))
            {
                strsql += " Status = '" + Jstatus + "',";
            }
            //if ((Jitems != null) && (Jitems != ""))
            //{
            //    strsql += " JOBITEMSTATUS = '" + Jitems + "',";
            //}
            if ((Jprobs != null) && (Jprobs != ""))
            {
                strsql += " JOBPROBLEMSTATUS = '" + Jprobs + "',";
            }
            if ((JNote != null) && (JNote != ""))
            {
                strsql += " JobNote = '" + JNote + "',";
            }
            else
            {
                strsql += " JobNote = '',";
            }
            if ((JBoi != null) && (JBoi != ""))
            {
                strsql += " BotRejectCode = '" + JBoi + "',";
            }
            if ((JBoiNote != null) && (JBoiNote != ""))
            {
                strsql += " RejectDetail = '" + JBoiNote + "',";
            }
            if ((LatLong != null) && (LatLong != ""))
            {
                strsql += " LatLong = '" + LatLong + "',";
            }
            if ((JprobsNote != null) && (JprobsNote != ""))
            {
                strsql += " ProblemDetail = '" + JprobsNote + "',";
            }
            strsql += " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +

                    " where Id =" + jobid + "";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);


            foreach (var JobV in JobItemTechnician.ToList())
            {
                int a = 0;
                string strsqla = " Update JobItem set ";

                if ((JobV.ItemRecieve != null) && (JobV.ItemRecieve != ""))
                {
                    strsqla += " ItemRecieve = '" + JobV.ItemRecieve + "',";
                }
                if ((JobV.ItemIncomplete != null) && (JobV.ItemIncomplete != ""))
                {
                    strsqla += " ItemIncomplete = '" + JobV.ItemIncomplete + "',";
                }
                else
                {
                    strsqla += " ItemIncomplete = '0',";
                }

                 

                strsqla += " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +

                        " where jobid ='" + jobid + "' and Item  = '" + JobV.ItemID + "' ";


                com.CommandText = strsqla;
                com.CommandType = System.Data.CommandType.Text;
                a = db.ExcuteBeginTransectionText(com);
            }
            return i;
        }

        public int UpdateJobTechnicianJobSuccessNotComplete(string jobid, string Jstatus)
        {
            int i = 0;
            string strsql = " Update JobAssign set ";

            if ((Jstatus != null) && (Jstatus != ""))
            {
                strsql += " Status = '" + Jstatus + "',";
            }
        
            strsql += " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +

                    " where Id =" + jobid + "";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);



            return i;
        }

        public int insertTokenMobile(string Token, string Serial_Mobile, string Phone_Mobile, string Vendor)
        {
            int i = 0;
            string strsql = "INSERT INTO Token_Mobile  (Token,Serial_Mobile,Createdate,Phone_Mobile,UpdateDate,FlagDelete,active,Vendor)" +
                  "VALUES (" +
                 "'" + Token + "'," +
                 "'" + Serial_Mobile + "'," +
                 "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                 "'" + Phone_Mobile + "'," +
                 "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +

                 "'N'," +
                 "'Y'," +         
                 "'" + Vendor + "'" +
                  ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int checkTokenmobile(string Serial_Mobile)
        {
            int i = 0; DataTable dt = new DataTable();
            string strsql = "select count(id) as countMobile from Token_Mobile where Serial_Mobile ='" + Serial_Mobile + "' and FlagDelete='N' ";




            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            dt = db.ExcuteDataReaderText(com);

            i =int.Parse( dt.Rows[0]["countMobile"].ToString());

            return i;
        }

        public int UpdateTokenMobile(string Token, string Serial_Mobile, string Phone_Mobile, string Vendor)
        {
            int i = 0;
            //string strsql1 = "INSERT INTO Token_Mobile  (Token,Serial_Mobile,Createdate,Phone_Mobile,UpdateDate,UpdateBy,FlagDelete,active,Vendor)" +
            //      "VALUES (" +
            //     "'" + Token + "'," +
            //     "'" + Serial_Mobile + "'," +
            //     "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
            //     "'" + Phone_Mobile + "'," +
            //     "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
            //     "'" + Usercode + "'," +
            //     "'N'," +
            //     "'Y'," +
            //     "'" + Vendor + "'" +
            //      ")";

            //int i = 0;
            string strsql = " Update Token_Mobile set ";

            if ((Token != null) && (Token != ""))
            {
                strsql += " Token = '" + Token + "',";
            }
            if ((Phone_Mobile != null) && (Phone_Mobile != ""))
            {
                strsql += " Phone_Mobile = '" + Phone_Mobile + "',";
            }
            if ((Vendor != null) && (Vendor != ""))
            {
                strsql += " Vendor = '" + Vendor + "',";
            }
            //if ((Usercode != null) && (Usercode != ""))
            //{
            //    strsql += " UpdateBy = '" + Usercode + "',";
            //}
            strsql += " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +

                    " where Serial_Mobile =" + Serial_Mobile + "";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public List<JobTokenMobile> GetTokenMobile()
        {

            string strcond = "";
            DataTable dt = new DataTable();
            var LJobItemDetail = new List<JobTokenMobile>();

            try
            {


                string strsql = "select  * from Token_Mobile where  FlagDelete = 'N' and active ='Y' ";



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LJobItemDetail = (from DataRow dr in dt.Rows

                                  select new JobTokenMobile()
                                  {
                                      Tokenid = dr["Id"].ToString().Trim(),
                                      Token = dr["Token"].ToString().Trim(),
                                      Phone_Mobile = dr["Phone_Mobile"].ToString().Trim(),
                                      active = dr["active"].ToString().Trim(),
                                      Vendor = dr["Vendor"].ToString().Trim(),
                                      Serial_Mobile = dr["Serial_Mobile"].ToString().Trim()
                                  }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LJobItemDetail;
        }
    }
}
