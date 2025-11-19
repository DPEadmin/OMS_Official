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
using APPCOREMODEL.Datas.OMSDTO;
using System.Configuration;

namespace APPCOREMODEL.OMSDAO
{
    public class CallInformationDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<CallInformationListReturn> ListCallinformationInfoByCriteria(CallInformationInfo cInfo)
        {
            string strcond = "";

            if((cInfo.CallInId != null) && (cInfo.CallInId != 0))
            {
                strcond = strcond == "" ? strcond += "where c.Id = " + cInfo.CallInId : strcond += "AND c.Id = " + cInfo.CallInId;
            }
            if((cInfo.CallInNumber != null) && (cInfo.CallInNumber != ""))
            {
                strcond = strcond == "" ? strcond += "where c.CallInNumber LIKE '%" + cInfo.CallInNumber.Trim() + "%'" : strcond += "AND c.CallInNumber LIKE '%" + cInfo.CallInNumber.Trim() + "%'";
            }
            if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += "where c.CustomerFName LIKE '%" + cInfo.CustomerFName.Trim() + "%'" : strcond += "AND c.CustomerFName LIKE '%" + cInfo.CustomerFName.Trim() + "%'";
            }
            if ((cInfo.MerchantCode != null) && (cInfo.MerchantCode != ""))
            {
                strcond = strcond == "" ? strcond += "where c.MerchantCode LIKE '%" + cInfo.MerchantCode.Trim() + "%'" : strcond += "AND c.MerchantCode LIKE '%" + cInfo.MerchantCode .Trim() + "%'";
            }
            if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += "where c.CustomerLName LIKE '%" + cInfo.CustomerLName.Trim() + "%'" : strcond += "AND c.CustomerLName LIKE '%" + cInfo.CustomerLName.Trim() + "%'";
            }



            var LCallIn = new List<CallInformationListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " SELECT oi.OrderCode,c.*,cu.*,DATEDIFF(year, cu.BirthDate, GETDATE()) AS Age,l.LookupValue," +
                    "g.GenderName, m.LookupValue as MaritalStatusName,o.OccupationCode, o.OccupationName, cu.Occupation " +
                    "FROM " + dbName + ".dbo.CallInfo AS c " +
                                " LEFT JOIN " + dbName + ".dbo.Customer AS cu on cu.CustomerCode = c.CustomerCode " +
                                " LEFT JOIN " + dbName + ".dbo.Merchant AS me on me.MerchantCode = c.MerchantCode" +
                                " LEFT JOIN " + dbName + ".dbo.Gender AS g ON g.GenderCode = cu.Gender " +
                                " LEFT JOIN " + dbName + ".dbo.Occupation AS o ON o.OccupationCode = cu.Occupation " +
                                " LEFT JOIN " + dbName + ".dbo.Lookup m on m.LookupCode = cu.MaritalStatus AND m.LookupType = 'MARITALSTATUS' " +
                                " LEFT JOIN " + dbName + ".dbo.Lookup AS l ON l.LookupCode = c.CONTACTSTATUS AND l.LookupType LIKE 'CONTACTSTATUS' " +
                                " LEFT JOIN " + dbName + ".dbo.Orderinfo as oi on oi.Callinfo_id = c.id " +
                                " AND c.FlagDelete = 'N' " + strcond +
                                " Order BY c.CreateDate DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCallIn = (from DataRow dr in dt.Rows

                           select new CallInformationListReturn()
                           {
                               CallInId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                               CallInNumber = dr["CallInNumber"].ToString().Trim(),
                               CustomerCode = dr["CustomerCode"].ToString().Trim(),
                               CustomerFName = dr["CustomerFName"].ToString().Trim(),
                               CustomerLName = dr["CustomerLName"].ToString().Trim(),
                               CreateBy = dr["CreateBy"].ToString(),
                               CreateDate = dr["CreateDate"].ToString(),
                               FlagDelete = dr["FlagDelete"].ToString(),
                               CONTACTSTATUS = dr["LookupValue"].ToString(),

                               ContactTel = dr["ContactTel"].ToString().Trim(),
                               Mobile = dr["Mobile"].ToString().Trim(),
                               Mail = dr["Mail"].ToString().Trim(),
                               Title = dr["Title"].ToString().Trim(),
                               Gender = dr["gender"].ToString().Trim(),
                               Identification = dr["Identification_Number"].ToString().Trim(),
                               BirthDate = dr["BirthDate"].ToString(),
                               MaritalStatusCode = dr["MaritalStatus"].ToString(),
                               OccupationCode = dr["OccupationCode"].ToString(),
                               OccupationName = dr["OccupationName"].ToString(),
                               Occupation = dr["Occupation"].ToString(),
                               Income = (dr["Income"].ToString() != "") ? Convert.ToInt32(dr["Income"]) : 0,
                               HomePhone = dr["HomePhone"].ToString().Trim(),
                               Age = (dr["Age"].ToString() != "") ? Convert.ToInt32(dr["Age"]) : 0,
                               MerchantCode = dr["MerchantCode"].ToString().Trim(),
                               OrderCode = dr["OrderCode"].ToString().Trim(),
                           }).ToList();

            } 
            
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCallIn;
        }

        public List<CallInformationListReturn> ListNewCallinformationInfoByCriteria(CallInformationInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CallInId != null) && (cInfo.CallInId != 0))
            {
                strcond = strcond == "" ? strcond += "where c.Id = " + cInfo.CallInId : strcond += "AND c.Id = " + cInfo.CallInId;
            }
            if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += "where c.CustomerFName LIKE '%" + cInfo.CustomerFName.Trim() + "%'" : strcond += "AND c.CustomerFName LIKE '%" + cInfo.CustomerFName.Trim() + "%'";
            }
            if ((cInfo.MerchantCode != null) && (cInfo.MerchantCode != ""))
            {
                strcond += " AND  me.MerchantCode = '" + cInfo.MerchantCode + "'";
            }
            if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += "where c.CustomerLName LIKE '%" + cInfo.CustomerLName.Trim() + "%'" : strcond += "AND c.CustomerLName LIKE '%" + cInfo.CustomerLName.Trim() + "%'";
            }
            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += "where c.CustomerCode LIKE '%" + cInfo.CustomerCode.Trim() + "%'" : strcond += "AND c.CustomerCode LIKE '%" + cInfo.CustomerCode.Trim() + "%'";
            }
            else
            {
                strcond += " OR  c.CustomerCode LIKE '%NEW%' ";
            }
            if ((cInfo.CallInNumber != null) && (cInfo.CallInNumber != ""))
            {
                strcond = strcond == "" ? strcond += "where c.CallInNumber LIKE '%" + cInfo.CallInNumber.Trim() + "%'" : strcond += "AND c.CallInNumber LIKE '%" + cInfo.CallInNumber.Trim() + "%'";
            }

            var LCallIn = new List<CallInformationListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " SELECT oi.OrderCode,c.*,cu.*,DATEDIFF(year, cu.BirthDate, GETDATE()) AS Age,l.LookupValue,me.MerchantCode," +
                    "g.GenderName, m.LookupValue as MaritalStatusName,o.OccupationCode, o.OccupationName, cu.Occupation " +
                    "FROM " + dbName + ".dbo.CallInfo AS c " +
                                " LEFT JOIN " + dbName + ".dbo.Customer AS cu on cu.CustomerCode = c.CustomerCode " +
                                " LEFT JOIN " + dbName + ".dbo.Merchant AS me on me.MerchantCode = cu.MerchantCode" +
                                " LEFT JOIN " + dbName + ".dbo.Gender AS g ON g.GenderCode = cu.Gender " +
                                " LEFT JOIN " + dbName + ".dbo.Occupation AS o ON o.OccupationCode = cu.Occupation " +
                                " LEFT JOIN " + dbName + ".dbo.Lookup m on m.LookupCode = cu.MaritalStatus AND m.LookupType = 'MARITALSTATUS' " +
                                " LEFT JOIN " + dbName + ".dbo.Lookup AS l ON l.LookupCode = c.CONTACTSTATUS AND l.LookupType LIKE 'CONTACTSTATUS' " +
                                " LEFT JOIN " + dbName + ".dbo.Orderinfo as oi on oi.Callinfo_id = c.id " +
                                " AND c.FlagDelete = 'N' " + strcond +
                                " Order BY c.CreateDate DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCallIn = (from DataRow dr in dt.Rows

                           select new CallInformationListReturn()
                           {
                               CallInId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                               CallInNumber = dr["CallInNumber"].ToString().Trim(),
                               CustomerCode = dr["CustomerCode"].ToString().Trim(),
                               CustomerFName = dr["CustomerFName"].ToString().Trim(),
                               CustomerLName = dr["CustomerLName"].ToString().Trim(),
                               CreateBy = dr["CreateBy"].ToString(),
                               CreateDate = dr["CreateDate"].ToString(),
                               FlagDelete = dr["FlagDelete"].ToString(),
                               CONTACTSTATUS = dr["LookupValue"].ToString(),

                               ContactTel = dr["ContactTel"].ToString().Trim(),
                               Mobile = dr["Mobile"].ToString().Trim(),
                               Mail = dr["Mail"].ToString().Trim(),
                               Title = dr["Title"].ToString().Trim(),
                               Gender = dr["gender"].ToString().Trim(),
                               Identification = dr["Identification_Number"].ToString().Trim(),
                               BirthDate = dr["BirthDate"].ToString(),
                               MaritalStatusCode = dr["MaritalStatus"].ToString(),
                               OccupationCode = dr["OccupationCode"].ToString(),
                               OccupationName = dr["OccupationName"].ToString(),
                               Occupation = dr["Occupation"].ToString(),
                               Income = (dr["Income"].ToString() != "") ? Convert.ToInt32(dr["Income"]) : 0,
                               HomePhone = dr["HomePhone"].ToString().Trim(),
                               Age = (dr["Age"].ToString() != "") ? Convert.ToInt32(dr["Age"]) : 0,
                               MerchantCode = dr["MerchantCode"].ToString().Trim(),
                               OrderCode = dr["OrderCode"].ToString().Trim(),
                           }).ToList();

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCallIn;
        }

        public int InsertCallInformation(CallInformationInfo cInfo)
        {
            int i = 0;
            string strsql = "INSERT INTO " + dbName + ".dbo.CallInfo (CallInNumber,CustomerCode,CustomerFName,CustomerLName,CONTACTSTATUS,CreateDate,CreateBy,FlagDelete,MerchantCode)" +
                "VALUES (" +
                "'" + cInfo.CallInNumber + "'," +
                "'" + cInfo.CustomerCode + "'," +
                "'" + cInfo.CustomerFName + "'," +
                "'" + cInfo.CustomerLName + "'," +
                "'" + cInfo.CONTACTSTATUS + "'," +
                "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                "'" + cInfo.CreateBy + "'," +
                "'" + cInfo.FlagDelete + "'," +
                "'" + cInfo.MerchantCode + "'" +
                ")";

            strsql = strsql + "SELECT SCOPE_IDENTITY();";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            //i = db.ExcuteBeginTransectionText(com);
            i = db.ExcuteBeginTransectionTextReturnID(com);

            return i;
        }

        public int? UpdateCustomerCode(CallInformationInfo cInfo)
        {

            int i = 0;
            string strcon = "";

            if(cInfo.CallInId != null && cInfo.CallInId != 0)
            {
                strcon += " AND ID = " + cInfo.CallInId;
            }

            string strsql = " Update " + dbName + ".dbo.CallInfo set " +
                            " CustomerCode = '" + cInfo.CustomerCode + "'" +
                            " where CustomerFName like '" + cInfo.CustomerFName + "'" + 
                            " and CustomerLName like '" + cInfo.CustomerLName + "'" +
                            " and MerchantCode like '" + cInfo.MerchantCode + "'" +
                            strcon;

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int? UpdateCONTACTSTATUS(CallInformationInfo cInfo)
        {

            int i = 0;

            string strsql = " Update " + dbName + ".dbo.CallInfo set " +
                            " CONTACTSTATUS = '" + cInfo.CONTACTSTATUS + "'" +
                            " where id = '" + cInfo.CallInId + "'" ;

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<CallInformationListReturn> ListCustomerCodeCallInValidate(CallInformationInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CallInId != null) && (cInfo.CallInId != 0))
            {
                strcond = strcond == "" ? strcond += " where c.Id = " + cInfo.CallInId : strcond += "AND c.Id = " + cInfo.CallInId;
            }
            if ((cInfo.MerchantCode != null) && (cInfo.MerchantCode != ""))
            {
                strcond += " where  c.MerchantCode = '" + cInfo.MerchantCode + "'";
            }
            if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            {
                strcond += (strcond == "") ? " where  c.CustomerFName like '%" + cInfo.CustomerFName.Trim() + "%'" : " and c.CustomerFName like '%" + cInfo.CustomerFName.Trim() + "%'";
            }

            if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
            {
                strcond += (strcond == "") ? " where  c.CustomerLName like '%" + cInfo.CustomerLName.Trim() + "%'" : " and c.CustomerLName like '%" + cInfo.CustomerLName.Trim() + "%'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<CallInformationListReturn>();

            try
            {
                string strsql = "select c.*" +
                                " from " + dbName + ".dbo.CallInfo c " +
                                
                                strcond;



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new CallInformationListReturn()
                              {
                                  CallInId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  CustomerFName = dr["CustomerFName"].ToString().Trim(),
                                  CustomerLName = dr["CustomerLName"].ToString().Trim(),
                                  FlagDelete = dr["FlagDelete"].ToString().Trim(),
                                  CONTACTSTATUS = dr["CONTACTSTATUS"].ToString().Trim(),
                                  MerchantCode = dr["MerchantCode"].ToString().Trim(),
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public List<CallInformationListReturn> ListCallinformationInfoByCriteria_Manual(CallInformationInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CallInId != null) && (cInfo.CallInId != 0))
            {
                strcond = strcond == "" ? strcond += " where c.Id = " + cInfo.CallInId : strcond += "AND c.Id = " + cInfo.CallInId;
            }
            if ((cInfo.CallInNumber != null) && (cInfo.CallInNumber != ""))
            {
                strcond = strcond == "" ? strcond += " where c.CallInNumber LIKE '%" + cInfo.CallInNumber.Trim() + "%'" : strcond += "AND c.CallInNumber LIKE '%" + cInfo.CallInNumber.Trim() + "%'";
            }
            if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " where c.CustomerFName LIKE '%" + cInfo.CustomerFName.Trim() + "%'" : strcond += "AND c.CustomerFName LIKE '%" + cInfo.CustomerFName.Trim() + "%'";
            }
            if ((cInfo.MerchantCode != null) && (cInfo.MerchantCode != ""))
            {
                strcond = strcond == "" ? strcond += " where c.MerchantCode LIKE '%" + cInfo.MerchantCode.Trim() + "%'" : strcond += "AND c.MerchantCode LIKE '%" + cInfo.MerchantCode.Trim() + "%'";
            }
            if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " where c.CustomerLName LIKE '%" + cInfo.CustomerLName.Trim() + "%'" : strcond += "AND c.CustomerLName LIKE '%" + cInfo.CustomerLName.Trim() + "%'";
            }

            strcond = strcond == "" ? strcond += " where  c.FlagDelete = 'N'" : strcond += "   AND c.FlagDelete = 'N'";

            var LCallIn = new List<CallInformationListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " SELECT c.*,cu.*,DATEDIFF(year, cu.BirthDate, GETDATE()) AS Age" +
                    
                    " FROM " + dbName + ".dbo.CallInfo AS c " +
                                " LEFT JOIN " + dbName + ".dbo.Customer AS cu on cu.CustomerCode = c.CustomerCode " +
                                " LEFT JOIN " + dbName + ".dbo.Merchant AS me on me.MerchantCode = c.MerchantCode" +
               
                                 strcond +
                                " Order BY c.CreateDate DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCallIn = (from DataRow dr in dt.Rows

                           select new CallInformationListReturn()
                           {
                               CallInId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                               CallInNumber = dr["CallInNumber"].ToString().Trim(),
                               CustomerCode = dr["CustomerCode"].ToString().Trim(),
                               CustomerFName = dr["CustomerFName"].ToString().Trim(),
                               CustomerLName = dr["CustomerLName"].ToString().Trim(),
                               CreateBy = dr["CreateBy"].ToString(),
                               CreateDate = dr["CreateDate"].ToString(),
                               FlagDelete = dr["FlagDelete"].ToString(),
                           
                               ContactTel = dr["ContactTel"].ToString().Trim(),
                               Mobile = dr["Mobile"].ToString().Trim(),
                               Mail = dr["Mail"].ToString().Trim(),
                               Title = dr["Title"].ToString().Trim(),
                               Gender = dr["gender"].ToString().Trim(),
                               Identification = dr["Identification_Number"].ToString().Trim(),
                               BirthDate = dr["BirthDate"].ToString(),                    
                               Income = (dr["Income"].ToString() != "") ? Convert.ToInt32(dr["Income"]) : 0,
                               HomePhone = dr["HomePhone"].ToString().Trim(),
                               Age = (dr["Age"].ToString() != "") ? Convert.ToInt32(dr["Age"]) : 0,
                               MerchantCode = dr["MerchantCode"].ToString().Trim(),
                            
                           }).ToList();

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCallIn;
        }
    }
}
