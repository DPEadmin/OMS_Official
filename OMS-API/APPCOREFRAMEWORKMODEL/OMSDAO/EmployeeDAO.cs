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
    public class EmployeeDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();
        public string dbNameEmp_Profile = ConfigurationManager.AppSettings["dbNameEmp_Profile"].ToString();
        public string CompanyCode = ConfigurationManager.AppSettings["CompanyCode"].ToString();
        
        public List<EmpListReturn> ListEmp(EmpInfo eInfo)
        {
            string strcond = "";

            if ((eInfo.ActiveFlag != null) && (eInfo.ActiveFlag != ""))
            {
                strcond += (strcond == "") ? " where  e.ActiveFlag = '" + eInfo.ActiveFlag.Trim() + "'" : " and e.ActiveFlag = '" + eInfo.ActiveFlag.Trim() + "'";
            }

            if ((eInfo.TechnicianFlag != null) && (eInfo.TechnicianFlag != ""))
            {
                strcond += (strcond == "") ? " where  e.TechnicianFlag = '" + eInfo.TechnicianFlag.Trim() + "'" : " and e.TechnicianFlag = '" + eInfo.TechnicianFlag.Trim() + "'";
            }

            if ((eInfo.RefCode != null) && (eInfo.RefCode != ""))
            {
                strcond += (strcond == "") ? " where  e.RefCode = '" + eInfo.RefCode.Trim() + "'" : " and e.RefCode = '" + eInfo.RefCode.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var LEmployee = new List<EmpListReturn>();

            try
            {
                string strsql = " select e.*,p.PositionName" +
                                " from " + dbName + ".dbo.Emp e " +
                                " left join  " + dbName + ".dbo.Position p on e.PositionCode = p.PositionCode " +

                                 strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpListReturn()
                             {
                                 EmpId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                                 Title_TH = dr["Title_TH"].ToString().Trim(),
                                 EmpFname_TH = dr["EmpFname_TH"].ToString().Trim(),
                                 EmpLname_TH = dr["EmpLname_TH"].ToString().Trim(),
                                 EmpName_TH = dr["EmpFname_TH"].ToString() + "  " + dr["EmpLname_TH"].ToString(),
                                 Title_EN = dr["Title_EN"].ToString().Trim(),
                                 EmpFname_EN = dr["EmpFname_EN"].ToString().Trim(),
                                 EmpLname_EN = dr["EmpLname_EN"].ToString().Trim(),
                                 EmpName_EN = dr["EmpFname_EN"].ToString() + "  " + dr["EmpLname_EN"].ToString(),
                                 TechnicianFlag = dr["TechnicianFlag"].ToString().Trim(),
                                 PositionCode = dr["PositionCode"].ToString().Trim(),
                                 PositionName = dr["PositionName"].ToString().Trim(),
                                 ActiveFlag = dr["ActiveFlag"].ToString().Trim(),
                                 Mail = dr["Mail"].ToString().Trim()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }
        public List<EmpListReturn> ListEmpValidateInsert(EmpInfo eInfo)
        {
            string strcond = "";

            if ((eInfo.EmpCodeValidate != null) && (eInfo.EmpCodeValidate != ""))
            {
                strcond += (strcond == "") ? " where  e.EmpCode = '" + eInfo.EmpCodeValidate.Trim() + "'" : " and e.EmpCode = '" + eInfo.EmpCodeValidate.Trim() + "'";
            }
           

            DataTable dt = new DataTable();
            var Lem = new List<EmpListReturn>();

            try
            {
                string strsql = " select e.* " +
                                " from " + dbName + ".dbo.Emp e " +
                                 strcond + " and e.FlagDelete = 'N'";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                Lem = (from DataRow dr in dt.Rows

                             select new EmpListReturn()
                             {
                                 EmpId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return Lem;
        }

        public List<EmpListReturn> ListEmpByCriteria(EmpInfo eInfo)
        {
            string strcond = "";

            if ((eInfo.ActiveFlag != null) && (eInfo.ActiveFlag != ""))
            {
                strcond += (strcond == "") ? " where  e.ActiveFlag = '" + eInfo.ActiveFlag.Trim() + "'" : " and e.ActiveFlag = '" + eInfo.ActiveFlag.Trim() + "'";
            }

            if ((eInfo.TechnicianFlag != null) && (eInfo.TechnicianFlag != ""))
            {
                strcond += (strcond == "") ? " where  e.TechnicianFlag = '" + eInfo.TechnicianFlag.Trim() + "'" : " and e.TechnicianFlag = '" + eInfo.TechnicianFlag.Trim() + "'";
            }

            if ((eInfo.EmpCode != null) && (eInfo.EmpCode != ""))
            {
                strcond += (strcond == "") ? " where  e.EmpCode like '%" + eInfo.EmpCode.Trim() + "%'" : " and e.EmpCode like '%" + eInfo.EmpCode.Trim() + "%'";
            }

            if ((eInfo.EmpCodeTemp != null) && (eInfo.EmpCodeTemp != ""))
            {
                strcond += (strcond == "") ? " where  e.EmpCodeTemp = '" + eInfo.EmpCodeTemp.Trim() + "'" : " and e.EmpCodeTemp = '" + eInfo.EmpCodeTemp.Trim() + "'";
            }

            if ((eInfo.EmpCodeValidate != null) && (eInfo.EmpCodeValidate != ""))
            {
                strcond += (strcond == "") ? " where  e.EmpCode = '" + eInfo.EmpCodeValidate.Trim() + "'" : " and e.EmpCode = '" + eInfo.EmpCodeValidate.Trim() + "'";
            }

            if ((eInfo.EmpFname_TH != null) && (eInfo.EmpFname_TH != ""))
            {
                strcond += (strcond == "") ? " where  e.EmpFname_TH like '%" + eInfo.EmpFname_TH.Trim() + "%'" : " and e.EmpFname_TH like '%" + eInfo.EmpFname_TH.Trim() + "%'";
            }

            if ((eInfo.EmpLname_TH != null) && (eInfo.EmpLname_TH != ""))
            {
                strcond += (strcond == "") ? " where  e.EmpLname_TH like '%" + eInfo.EmpLname_TH.Trim() + "%'" : " and e.EmpLname_TH like '%" + eInfo.EmpLname_TH.Trim() + "%'";
            }

            if ((eInfo.RefCode != null) && (eInfo.RefCode != ""))
            {
                strcond += (strcond == "") ? " where  e.RefCode = '" + eInfo.RefCode.Trim() + "'" : " and e.RefCode = '" + eInfo.RefCode.Trim() + "'";
            }

            if ((eInfo.BUCode != null) && (eInfo.BUCode != "-99"))
            {
                strcond += (strcond == "") ? " where  e.BUCode = '" + eInfo.BUCode.Trim() + "'" : " and e.BUCode = '" + eInfo.BUCode.Trim() + "'";
            }
            if ((eInfo.rowOFFSet != 0) || (eInfo.rowFetch != 0))
            {
                strcond += " ORDER BY e.Id DESC OFFSET " + eInfo.rowOFFSet + " ROWS FETCH NEXT " + eInfo.rowFetch + " ROWS ONLY";
            }            

            DataTable dt = new DataTable();
            var LEmployee = new List<EmpListReturn>();

            try
            {
                string strsql = " select e.*,p.PositionName,l.LookupValue,l.LookupValue as TitleName_TH,te.LookupValue as TitleName_EN,b.LookupValue as BUName,u.Id as UserLoginId,u.Username,u.Password,e.ExtensionID" +
                                " from " + dbName + ".dbo.Emp e " +
                                 " left join  " + dbName + ".dbo.UserLogin u on e.EmpCode = u.EmpCode " +
                               " left join  " + dbName + ".dbo.Position p on e.PositionCode = p.PositionCode " +
                                " left join  " + dbName + ".dbo.Lookup l on e.Title_TH = l.LookupCode and l.LookupType ='TITLE'" +
                                 " left join  " + dbName + ".dbo.Lookup te on e.Title_EN = te.LookupCode and te.LookupType ='TITLE'" +
                                " left join  " + dbName + ".dbo.Lookup b on e.BUCode = b.LookupCode and b.LookupType ='BU'" +
                                 strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpListReturn()
                             {
                                 EmpId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                                 UserLoginId = dr["UserLoginId"].ToString().Trim(),
                                 Username = dr["Username"].ToString().Trim(),
                                 Password = dr["Password"].ToString().Trim(),
                                 EmpCodeTemp = dr["EmpCodeTemp"].ToString().Trim(),
                                 Title_TH = dr["Title_TH"].ToString().Trim(),
                                 TitleName_TH = dr["TitleName_TH"].ToString().Trim(),
                                 EmpFname_TH = dr["EmpFname_TH"].ToString().Trim(),
                                 EmpLname_TH = dr["EmpLname_TH"].ToString().Trim(),
                                 EmpName_TH = dr["EmpFname_TH"].ToString() + "  " + dr["EmpLname_TH"].ToString(),
                                 Title_EN = dr["Title_EN"].ToString().Trim(),
                                 TitleName_EN = dr["TitleName_EN"].ToString().Trim(),
                                 EmpFname_EN = dr["EmpFname_EN"].ToString().Trim(),
                                 EmpLname_EN = dr["EmpLname_EN"].ToString().Trim(),
                                 EmpName_EN = dr["EmpFname_EN"].ToString() + "    " + dr["EmpLname_EN"].ToString(),
                                 TechnicianFlag = dr["TechnicianFlag"].ToString().Trim(),
                                 PositionCode = dr["PositionCode"].ToString().Trim(),
                                 PositionName = dr["PositionName"].ToString().Trim(),
                                 LookupValue = dr["LookupValue"].ToString().Trim(),
                                 ActiveFlag = dr["ActiveFlag"].ToString().Trim(),
                                 Mail = dr["Mail"].ToString().Trim(),
                                 Mobile = dr["Mobile"].ToString().Trim(),
                                 BUCode = dr["BUCode"].ToString().Trim(),
                                 BUName = dr["BUName"].ToString().Trim(),
                                 RefCode = dr["RefCode"].ToString().Trim(),
                                 ExtensionID = dr["ExtensionID"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public List<EmpListReturn> GetLogin(EmpInfo eInfo)
        {
            string strcond = "";

            if ((eInfo.ActiveFlag != null) && (eInfo.ActiveFlag != ""))
            {
                strcond += (strcond == "") ? " where  e.ActiveFlag = '" + eInfo.ActiveFlag.Trim() + "'" : " and e.ActiveFlag = '" + eInfo.ActiveFlag.Trim() + "'";
            }
            if ((eInfo.Username != null) && (eInfo.Username != ""))
            {
                strcond += (strcond == "") ? " where  u.UserName = '" + eInfo.Username.Trim() + "'" : " and  u.UserName = '" + eInfo.Username.Trim() + "'";
            }
            if ((eInfo.RefUserName != null) && (eInfo.RefUserName != ""))
            {
                strcond += (strcond == "") ? " where  u.RefUserName = '" + eInfo.RefUserName.Trim() + "'" : " and  u.RefUserName = '" + eInfo.RefUserName.Trim() + "'";
            }            
            
            if ((eInfo.Password != null) && (eInfo.Password != ""))
            {
                strcond += (strcond == "") ? " where  u.Password = '" + eInfo.Password.Trim() + "'" : " and  u.Password = '" + eInfo.Password.Trim() + "'";
            }
            else
            {
                
            }
            
            
            if ((eInfo.EmpCode != null) && (eInfo.EmpCode != ""))
            {
                strcond += (strcond == "") ? " where  e.EmpCode = '" + eInfo.EmpCode.Trim() + "'" : " and  e.EmpCode = '" + eInfo.EmpCode.Trim() + "'";
            }
            if ((eInfo.RefCode != null) && (eInfo.RefCode != ""))
            {
                strcond += (strcond == "") ? " where  u.RefCode = '" + eInfo.RefCode.Trim() + "'" : " and  u.RefCode = '" + eInfo.RefCode.Trim() + "'";
            }            

            DataTable dt = new DataTable();
            var LEmployee = new List<EmpListReturn>();

            try
            {
                string strsql = " select u.AgentId,u.Username,u.Password,u.RefUserName,e.BUCode, e.Id as EmpId,e.Title_TH,e.Title_EN,e.ActiveFlag,e.EmpCode,e.Title_TH,e.EmpFname_TH,e.EmpLname_TH,e.Title_EN,e.EmpFname_EN,e.EmpLname_EN," +
                                "  e.Mail, e.PositionCode,p.PositionName, eb.BranchCode, b.BranchName , e.ExtensionID ,r.RoleCode" +
                                " from " + dbName + ".dbo.UserLogin u " +
                                " left join  " + dbName + ".dbo.Emp e on u.EmpCode =  e.EmpCode and e.FlagDelete = 'N' " +
                                " left join  " + dbName + ".dbo.Position p on e.PositionCode = p.PositionCode " +
                                " left join  " + dbName + ".dbo.EmpBranch eb on eb.EmpCode = e.EmpCode " +
                                " left join  " + dbName + ".dbo.Branch b on b.BranchCode = eb.BranchCode " +
                                " left join  " + dbName + ".dbo.EmpRole r on r.EmpCode = u.EmpCode " +


                                 strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpListReturn()
                             {
                                 EmpId = (dr["EmpId"].ToString() != "") ? Convert.ToInt32(dr["EmpId"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                                 BUCode = dr["BUCode"].ToString().Trim(),
                                 RoleCode = dr["RoleCode"].ToString().Trim(),
                                 Username = dr["Username"].ToString().Trim(),
                                 Password = dr["Password"].ToString().Trim(),
                                 Title_TH = dr["Title_TH"].ToString().Trim(),
                                 EmpFname_TH = dr["EmpFname_TH"].ToString().Trim(),
                                 EmpLname_TH = dr["EmpLname_TH"].ToString().Trim(),
                                 EmpName_TH = dr["EmpFname_TH"].ToString() + "  " + dr["EmpLname_TH"].ToString(),
                                 Title_EN = dr["Title_EN"].ToString().Trim(),
                                 AgentId = dr["AgentId"].ToString().Trim(),
                                 EmpFname_EN = dr["EmpFname_EN"].ToString().Trim(),
                                 EmpLname_EN = dr["EmpLname_EN"].ToString().Trim(),
                                 EmpName_EN = dr["EmpFname_EN"].ToString() + "  " + dr["EmpLname_EN"].ToString(),
                                 
                                 RefUserName = dr["RefUserName"].ToString().Trim(),
                                 PositionCode = dr["PositionCode"].ToString().Trim(),
                                 PositionName = dr["PositionName"].ToString().Trim(),
                                 ActiveFlag = dr["ActiveFlag"].ToString().Trim(),
                                 Mail = dr["Mail"].ToString().Trim(),
                                 BranchCode = dr["BranchCode"].ToString().Trim(),
                                 BranchName = dr["BranchName"].ToString().Trim(),
                                 ExtensionID = dr["ExtensionID"].ToString().Trim(),
                                 
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public List<EmpListReturn> GetLoginTakeOrder(EmpInfo eInfo)
        {
            string strcond = "";

            if ((eInfo.ActiveFlag != null) && (eInfo.ActiveFlag != ""))
            {
                strcond += (strcond == "") ? " where  e.ActiveFlag = '" + eInfo.ActiveFlag.Trim() + "'" : " and e.ActiveFlag = '" + eInfo.ActiveFlag.Trim() + "'";
            }
            if ((eInfo.Username != null) && (eInfo.Username != ""))
            {
                strcond += (strcond == "") ? " where  u.UserName = '" + eInfo.Username.Trim() + "'" : " and  u.UserName = '" + eInfo.Username.Trim() + "'";
            }
            if ((eInfo.Password != null) && (eInfo.Password != ""))
            {
                strcond += (strcond == "") ? " where  u.Password = '" + eInfo.Password.Trim() + "'" : " and  u.Password = '" + eInfo.Password.Trim() + "'";
            }
            if ((eInfo.EmpCode != null) && (eInfo.EmpCode != ""))
            {
                strcond += (strcond == "") ? " where  u.EmpCode = '" + eInfo.EmpCode.Trim() + "'" : " and  u.EmpCode = '" + eInfo.EmpCode.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var LEmployee = new List<EmpListReturn>();

            try
            {
                string strsql = " select u.EmpCode,u.AgentId,u.Username,u.Password,e.Id as EmpId,e.Title_TH,e.Title_EN,e.ActiveFlag,e.Title_TH,e.EmpFname_TH,e.EmpLname_TH,e.Title_EN,e.EmpFname_EN,e.EmpLname_EN," +
                                "  e.Mail, e.PositionCode,p.PositionName" +
                                " from " + dbName + ".dbo.UserLogin u " +
                                " left join  " + dbName + ".dbo.Emp e on u.EmpCode =  e.EmpCode " +
                                " left join  " + dbName + ".dbo.Position p on e.PositionCode = p.PositionCode " +

                                 strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpListReturn()
                             {
                                 EmpId = (dr["EmpId"].ToString() != "") ? Convert.ToInt32(dr["EmpId"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                                 Title_TH = dr["Title_TH"].ToString().Trim(),
                                 EmpFname_TH = dr["EmpFname_TH"].ToString().Trim(),
                                 EmpLname_TH = dr["EmpLname_TH"].ToString().Trim(),
                                 EmpName_TH = dr["EmpFname_TH"].ToString() + "  " + dr["EmpLname_TH"].ToString(),
                                 Title_EN = dr["Title_EN"].ToString().Trim(),
                                 AgentId = dr["AgentId"].ToString().Trim(),
                                 EmpFname_EN = dr["EmpFname_EN"].ToString().Trim(),
                                 EmpLname_EN = dr["EmpLname_EN"].ToString().Trim(),
                                 EmpName_EN = dr["EmpFname_EN"].ToString() + "  " + dr["EmpLname_EN"].ToString(),

                                 PositionCode = dr["PositionCode"].ToString().Trim(),
                                 PositionName = dr["PositionName"].ToString().Trim(),
                                 ActiveFlag = dr["ActiveFlag"].ToString().Trim(),
                                 Mail = dr["Mail"].ToString().Trim()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public List<EmpListReturn> GetEmpByUnitCode(EmpInfo eInfo)
        {
            //string strcond = "";


            DataTable dt = new DataTable();
            var LEmployee = new List<EmpListReturn>();

            try
            {
                string strsql = " select e.Id as EmpId,e.Title_TH,e.Title_EN,e.ActiveFlag,e.EmpCode,e.Title_TH,e.EmpFname_TH,e.EmpLname_TH,e.Title_EN,e.EmpFname_EN,e.EmpLname_EN," +
                                "  e.Mail, e.PositionCode " +
                                " from " + dbName + ".dbo.Emp e  " +
                                " inner join  " + dbName + ".dbo.EmpUnit eu on e.EmpCode = eu.EmpCode " +
                                " inner join  " + dbName + ".dbo.Unit u on eu.UnitCode = u.UnitCode where u.unitCode = '" + eInfo.UnitCode + "'";

                //There are not tables name EmpUnit and Unit on OMS database

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpListReturn()
                             {
                                 EmpId = (dr["EmpId"].ToString() != "") ? Convert.ToInt32(dr["EmpId"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                                 Title_TH = dr["Title_TH"].ToString().Trim(),
                                 EmpFname_TH = dr["EmpFname_TH"].ToString().Trim(),
                                 EmpLname_TH = dr["EmpLname_TH"].ToString().Trim(),
                                 EmpName_TH = dr["EmpFname_TH"].ToString() + "  " + dr["EmpLname_TH"].ToString(),
                                 Title_EN = dr["Title_EN"].ToString().Trim(),
                                 EmpFname_EN = dr["EmpFname_EN"].ToString().Trim(),
                                 EmpLname_EN = dr["EmpLname_EN"].ToString().Trim(),
                                 EmpName_EN = dr["EmpFname_EN"].ToString() + "  " + dr["EmpLname_EN"].ToString(),

                                 PositionCode = dr["PositionCode"].ToString().Trim(),
                                 
                                 ActiveFlag = dr["ActiveFlag"].ToString().Trim(),
                                 Mail = dr["Mail"].ToString().Trim()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public List<UserLoginListReturn> ListUserLoginByCriteria(UserLoginInfo uInfo)
        {
            string strcond = "";

            if ((uInfo.Username != null) && (uInfo.Username != ""))
            {
                strcond += " and  u.Username like '%" + uInfo.Username + "%'";
            }

            if ((uInfo.Password != null) && (uInfo.Password != ""))
            {
                strcond += " and  u.Password like '%" + uInfo.Password + "%'";
            }
            if ((uInfo.EmpCode != null) && (uInfo.EmpCode != ""))
            {
                strcond += " and  u.EmpCode like '%" + uInfo.EmpCode + "%'";
            }
            if ((uInfo.EmpCodeTemp != null) && (uInfo.EmpCodeTemp != ""))
            {
                strcond += " and  u.EmpCodeTemp like '%" + uInfo.EmpCodeTemp + "%'";
            }

            DataTable dt = new DataTable();
            var LEmployee = new List<UserLoginListReturn>();

            try
            {
                string strsql = " select * from " + dbName + ".dbo.UserLogin u where 1=1" + strcond;



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new UserLoginListReturn()
                             {
                                 UserLoginId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 Username = dr["Username"].ToString().Trim(),
                                 Password = dr["Password"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                                 EmpCodeTemp = dr["EmpCodeTemp"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public List<RoleListReturn> ListRoleNotInEmpRoleByCriteria(EmpRole eRole)
        {
         
            DataTable dt = new DataTable();
            var LEmployee = new List<RoleListReturn>();

            try
            {
                string strsql = "SELECT r.* FROM " + dbName + ".dbo.Role AS r "+
                    "WHERE(FlagDelete = 'N') AND(RoleCode NOT IN(SELECT RoleCode " +
                    " FROM " + dbName + ".dbo.EmpRole AS e " +
                    " WHERE(EmpCode like '%" + eRole.EmpCode + "%' and FlagDelete = 'N')))";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new RoleListReturn()
                             {

                                 RoleId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  RoleCode = dr["RoleCode"].ToString().Trim(),
                                 RoleName = dr["RoleName"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public List<EmpRoleListReturn> ListEmpRoleByCriteria(EmpRole eRole)
        {
            string strcond = "";

            if ((eRole.EmpCode != null) && (eRole.EmpCode != ""))
            {
                strcond += " and  e.EmpCode = '" + eRole.EmpCode + "'";
            }

            if ((eRole.RoleCode != null) && (eRole.RoleCode != ""))
            {
                strcond += " and  e.RoleCode = '" + eRole.RoleCode + "'";
            }

            DataTable dt = new DataTable();
            var LEmployee = new List<EmpRoleListReturn>();

            try
            {
                string strsql = " select e.*,r.RoleName  from " + dbName + ".dbo.EmpRole e " +
                                " LEFT join " + dbName + ".dbo.Role r on e.RoleCode = r.RoleCode "+
                               " where e.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY e.Id DESC OFFSET " + eRole.rowOFFSet + " ROWS FETCH NEXT " + eRole.rowFetch + " ROWS ONLY";



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpRoleListReturn()
                             {

                                 EmpRoleId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                                 RoleCode = dr["RoleCode"].ToString().Trim(),
                                 RoleName = dr["RoleName"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public List<EmpRoleListReturn> ListEmpRoleNoPagingByCriteria(EmpRole eRole)
        {
            string strcond = "";

            if ((eRole.EmpCode != null) && (eRole.EmpCode != ""))
            {
                strcond += " and  e.EmpCode like '%" + eRole.EmpCode + "%'";
            }

            if ((eRole.RoleCode != null) && (eRole.RoleCode != ""))
            {
                strcond += " and  e.RoleCode like '%" + eRole.RoleCode + "%'";
            }

            DataTable dt = new DataTable();
            var LEmployee = new List<EmpRoleListReturn>();

            try
            {
                string strsql = " select *  from " + dbName + ".dbo.EmpRole e " +

                               " where e.FlagDelete ='N' " + strcond;



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpRoleListReturn()
                             {
                                 EmpRoleId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                                 RoleCode = dr["RoleCode"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public int DeleteEmpInventory(EmpInventoryInfo eiInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.EmpInventory set " +

                           " FlagDelete = 'Y'" +

                           " where Id in(" + eiInfo.EmpInventoryId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public List<EmpBranchListReturn> ListEmpBranchByCriteria(EmpBranch eRole)
        {
            string strcond = "";
            if ((eRole.EmpCode != null) && (eRole.EmpCode != ""))
            {
                strcond += (strcond == "") ? " where  b.EmpCode = '" + eRole.EmpCode.Trim() + "'" : " and b.EmpCode = '" + eRole.EmpCode.Trim() + "'";
            }

            if ((eRole.BranchCode != null) && (eRole.BranchCode != ""))
            {
                strcond += (strcond == "") ? " where  b.BranchCode = '" + eRole.BranchCode.Trim() + "'" : " and b.BranchCode = '" + eRole.BranchCode.Trim() + "'";
            }

            if ((eRole.FlagDelete != null) && (eRole.FlagDelete != ""))
            {
                strcond += (strcond == "") ? " where  b.FlagDelete = '" + eRole.FlagDelete.Trim() + "'" : " and b.FlagDelete = '" + eRole.FlagDelete.Trim() + "'";
            }

            
            DataTable dt = new DataTable();
            var LEmployee = new List<EmpBranchListReturn>();

            try
            {
                string strsql = " select b.Id, e.EmpCode, e.EmpFname_TH, e.EmpLname_TH, b.BranchCode, bo.BranchName " +
                                " from " + dbName + ".dbo.Emp e " +
                                " left join  " + dbName + ".dbo.EmpBranch b on b.EmpCode = e.EmpCode " +
                                " left join  " + dbName + ".dbo.Branch bo on bo.BranchCode = b.BranchCode " +
                                 strcond;
                strsql += " ORDER BY b.Id DESC OFFSET " + eRole.rowOFFSet + " ROWS FETCH NEXT " + eRole.rowFetch + " ROWS ONLY";



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpBranchListReturn()
                             {
                                 EmpBranchId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                                 EmpFname_TH = dr["EmpFname_TH"].ToString().Trim(),
                                 EmpLname_TH = dr["EmpLname_TH"].ToString().Trim(),
                                 EmpName_TH = dr["EmpFname_TH"].ToString() + "  " + dr["EmpLname_TH"].ToString(),
                                 BranchCode = dr["BranchCode"].ToString().Trim(),
                                 BranchName = dr["BranchName"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public List<EmpBranchListReturn> ListEmpBranchNoPagingByCriteria(EmpBranch eRole)
        {
            string strcond = "";
            if ((eRole.EmpCode != null) && (eRole.EmpCode != ""))
            {
                strcond += (strcond == "") ? " where  b.EmpCode = '" + eRole.EmpCode.Trim() + "'" : " and b.EmpCode = '" + eRole.EmpCode.Trim() + "'";
            }

            if ((eRole.BranchCode != null) && (eRole.BranchCode != ""))
            {
                strcond += (strcond == "") ? " where  b.BranchCode = '" + eRole.BranchCode.Trim() + "'" : " and b.BranchCode = '" + eRole.BranchCode.Trim() + "'";
            }

            if ((eRole.FlagDelete != null) && (eRole.FlagDelete != ""))
            {
                strcond += (strcond == "") ? " where  b.FlagDelete = '" + eRole.FlagDelete.Trim() + "'" : " and b.FlagDelete = '" + eRole.FlagDelete.Trim() + "'";
            }


            DataTable dt = new DataTable();
            var LEmployee = new List<EmpBranchListReturn>();

            try
            {
                string strsql = " select b.Id, e.EmpCode, e.EmpFname_TH, e.EmpLname_TH, b.BranchCode, bo.BranchName " +
                                " from " + dbName + ".dbo.Emp e " +
                                " left join  " + dbName + ".dbo.EmpBranch b on b.EmpCode = e.EmpCode " +
                                " left join  " + dbName + ".dbo.Branch bo on bo.BranchCode = b.BranchCode " +
                                 strcond;
                strsql += " ORDER BY b.Id";



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpBranchListReturn()
                             {
                                 EmpBranchId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                                 EmpFname_TH = dr["EmpFname_TH"].ToString().Trim(),
                                 EmpLname_TH = dr["EmpLname_TH"].ToString().Trim(),
                                 EmpName_TH = dr["EmpFname_TH"].ToString() + "  " + dr["EmpLname_TH"].ToString(),
                                 BranchCode = dr["BranchCode"].ToString().Trim(),
                                 BranchName = dr["BranchName"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }


        public List<EmpRoleListReturn> ListEmpRoleByCriteria_01(EmpRole eRole)
        {
            string strcond = "";
            if ((eRole.EmpCode != null) && (eRole.EmpCode != ""))
            {
                strcond += (strcond == "") ? " where  r.EmpCode = '" + eRole.EmpCode.Trim() + "'" : " and r.EmpCode = '" + eRole.EmpCode.Trim() + "'";
            }

            if ((eRole.RoleCode != null) && (eRole.RoleCode != ""))
            {
                strcond += (strcond == "") ? " where  e.RoleCode = '" + eRole.RoleCode.Trim() + "'" : " and e.RoleCode = '" + eRole.RoleCode.Trim() + "'";
            }

            if ((eRole.FlagDelete != null) && (eRole.FlagDelete != ""))
            {
                strcond += (strcond == "") ? " where  r.FlagDelete = '" + eRole.FlagDelete.Trim() + "'" : " and r.FlagDelete = '" + eRole.FlagDelete.Trim() + "'";
            }

            if ((eRole.RoleFlagDelete != null) && (eRole.RoleFlagDelete != ""))
            {
                strcond += (strcond == "") ? " where  ro.FlagDelete = '" + eRole.RoleFlagDelete.Trim() + "'" : " and ro.FlagDelete = '" + eRole.RoleFlagDelete.Trim() + "'";
            }

            if ((eRole.rowOFFSet != 0) || (eRole.rowFetch != 0))
            {
                strcond += " ORDER BY r.Id DESC OFFSET " + eRole.rowOFFSet + " ROWS FETCH NEXT " + eRole.rowFetch + " ROWS ONLY";
            }

            DataTable dt = new DataTable();
            var LEmployee = new List<EmpRoleListReturn>();

            try
            {
                string strsql = " select r.Id, e.EmpCode, e.EmpFname_TH, e.EmpLname_TH, r.RoleCode, ro.RoleName " +
                                " from " + dbName + ".dbo.Emp e " +
                                " left join  " + dbName + ".dbo.EmpRole r on r.EmpCode = e.EmpCode " +
                                " left join  " + dbName + ".dbo.Role ro on ro.RoleCode = r.RoleCode " +
                                 strcond;
                



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpRoleListReturn()
                             {
                                 EmpRoleId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                                 EmpFname_TH = dr["EmpFname_TH"].ToString().Trim(),
                                 EmpLname_TH = dr["EmpLname_TH"].ToString().Trim(),
                                 EmpName_TH = dr["EmpFname_TH"].ToString() + "  " + dr["EmpLname_TH"].ToString(),
                                 RoleCode = dr["RoleCode"].ToString().Trim(),
                                 RoleName = dr["RoleName"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public List<EmpInventoryListReturn> ListEmpInventoryByCriteria(EmpInventoryInfo eiInfo)
        {
            string strcond = "";
            if ((eiInfo.EmpCode != null) && (eiInfo.EmpCode != ""))
            {
                strcond += " and  ei.EmpCode like '%" + eiInfo.EmpCode + "%'";
            }

            if ((eiInfo.InventoryCode != null) && (eiInfo.InventoryCode != ""))
            {
                strcond += " and  i.InventoryCode like '%" + eiInfo.InventoryCode + "%'";
            }

            DataTable dt = new DataTable();
            var LEmployee = new List<EmpInventoryListReturn>();

            try
            {
                string strsql = " select ei.id, ei.empcode, ei.inventorycode, i.inventoryname, ei.createby, ei.createdate, ei.updateby, ei.updatedate, ei.flagdelete from " + dbName + ".dbo.EmpInventory ei left join" +
                                " Inventory i ON i.InventoryCode = ei.InventoryCode" +
                               " where ei.FlagDelete ='N' and i.FlagDelete = 'N'" + strcond;

                strsql += " ORDER BY ei.Id DESC OFFSET " + eiInfo.rowOFFSet + " ROWS FETCH NEXT " + eiInfo.rowFetch + " ROWS ONLY";



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpInventoryListReturn()
                             {
                                 EmpInventoryId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                                 InventoryCode = dr["InventoryCode"].ToString().Trim(),
                                 InventoryName = dr["InventoryName"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public List<EmpRoleListReturn> RoleCodeValidate(EmpRole eRole)
        {
            string strcond = "";
            
            if ((eRole.RoleCode != null) && (eRole.RoleCode != ""))
            {
              
                    strcond += " and e.RoleCode = '" + eRole.RoleCode.Trim() + "'"; 
            }

            if ((eRole.EmpCode != null) && (eRole.EmpCode != ""))
            {
               
                    strcond += " and e.EmpCode = '" + eRole.EmpCode.Trim() + "'";
               
              
            }


            DataTable dt = new DataTable();
            var LEmployee = new List<EmpRoleListReturn>();

            try
            {
                string strsql = " select e.* from EmpRole e" + " where 1=1 " +
                                         strcond;



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpRoleListReturn()
                             {
                                 EmpRoleId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                                 RoleCode = dr["RoleCode"].ToString().Trim(),
                                 FlagDelete = dr["FlagDelete"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public List<EmpInventoryListReturn> EmpInventoryValidateInsert(EmpInventoryInfo eiInfo)
        {
            string strcond = "";
            if ((eiInfo.InventoryCode != null) && (eiInfo.InventoryCode != ""))
            {
                
                strcond += (strcond == "") ? " where ei.InventoryCode = '" + eiInfo.InventoryCode.Trim() + "'" : " and ei.InventoryCode = '" + eiInfo.InventoryCode.Trim() + "'";
            }

            if ((eiInfo.EmpCode != null) && (eiInfo.EmpCode != ""))
            {
                
                strcond += (strcond == "") ? " where ei.EmpCode = '" + eiInfo.EmpCode.Trim() + "'" : " and ei.EmpCode = '" + eiInfo.EmpCode.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var LEmployee = new List<EmpInventoryListReturn>();

            try
            {
                string strsql = " select ei.* from EmpInventory ei" +
                                  strcond;

                strsql += "and ei.flagdelete = 'N'";



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpInventoryListReturn()
                             {
                                 EmpInventoryId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                                 InventoryCode = dr["InventoryCode"].ToString().Trim(),
                                 FlagDelete = dr["FlagDelete"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public List<EmpRoleListReturn> BindDdlNotInbyCriteria(EmpRole eRole)
        {
            string strcond = "";

            if ((eRole.EmpCode != null) && (eRole.EmpCode != ""))
            {
                strcond = " where  er.EmpCode in ('" + eRole.EmpCode + "'))";
            }
            


            DataTable dt = new DataTable();
            var LEmployee = new List<EmpRoleListReturn>();

            try
            {
                string strsql = " select r.RoleCode, r.RoleName from " + dbName + ".dbo.Role r where r.RoleCode not in (select er.RoleCode from " +
                             dbName + ".dbo.EmpRole er " +
                             strcond;



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpRoleListReturn()
                             {
                                 RoleCode = dr["RoleCode"].ToString().Trim(),
                                 RoleName = dr["RoleName"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public List<EmpRoleListReturn> BindDdlbyCriteria(EmpRole eRole)
        {
            string strcond = "";

            if ((eRole.EmpCode != null) && (eRole.EmpCode != ""))
            {
                strcond += " and er.EmpCode in ('" + eRole.EmpCode + "'))";
            }



            DataTable dt = new DataTable();
            var LEmployee = new List<EmpRoleListReturn>();

            try
            {
                string strsql = " select r.RoleCode, r.RoleName from " + dbName + ".dbo.Role r where r.FlagDelete = 'N'";



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpRoleListReturn()
                             {
                                 RoleCode = dr["RoleCode"].ToString().Trim(),
                                 RoleName = dr["RoleName"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public List<InventoryListReturn> BindDdlInventorybyCriteria(InventoryInfo iInfo)
        {
            string strcond = "";

            if ((iInfo.EmpCode != null) && (iInfo.EmpCode != ""))
            {
                strcond += " and i.EmpCode in ('" + iInfo.EmpCode + "'))";
            }



            DataTable dt = new DataTable();
            var LInventory = new List<InventoryListReturn>();

            try
            {
                string strsql = " select i.InventoryCode, i.InventoryName from " + dbName + ".dbo.Inventory i where i.FlagDelete = 'N'";



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                             select new InventoryListReturn()
                             {
                                 InventoryCode = dr["InventoryCode"].ToString().Trim(),
                                 InventoryName = dr["InventoryName"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public List<EmpRoleListReturn> BindDdlUpdatebyCriteria(EmpRole eRole)
        {
            string strcond = "";

            if ((eRole.EmpCode != null) && (eRole.EmpCode != ""))
            {
                strcond = " where  er.EmpCode in ('" + eRole.EmpCode + "')";
            }



            DataTable dt = new DataTable();
            var LEmployee = new List<EmpRoleListReturn>();

            try
            {
                string strsql = " select r.RoleCode, r.RoleName from " + dbName + ".dbo.Role r " +
                                
                                " where r.FlagDelete = 'Y'";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpRoleListReturn()
                             {
                                 RoleCode = dr["RoleCode"].ToString().Trim(),
                                 RoleName = dr["RoleName"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public List<EmpRoleListReturn> BindDdlPlusbyCriteria(EmpRole eRole)
        {
            string strcond = "";

            if ((eRole.EmpCode != null) && (eRole.EmpCode != ""))
            {
                strcond = " where  er.EmpCode in ('" + eRole.EmpCode + "'))";
            }


            DataTable dt = new DataTable();
            var LEmployee = new List<EmpRoleListReturn>();

            try
            {
                string strsql = " select r.RoleCode, r.RoleName from " + dbName + ".dbo.Role r where r.RoleCode not in (select er.RoleCode from " +
                                dbName + ".dbo.EmpRole er " +
                                strcond +
                                " UNION" +
                                " select ro.RoleCode, ro.RoleName FROM EmpRole es INNER JOIN Role ro ON ro.RoleCode = es.RoleCode WHERE (es.EmpCode = '" +
                                eRole.EmpCode + "') AND (es.FlagDelete = 'Y')";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpRoleListReturn()
                             {
                                 RoleCode = dr["RoleCode"].ToString().Trim(),
                                 RoleName = dr["RoleName"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public int? CountEmployeeListByCriteria(EmpInfo eInfo)
        {
            string strcond = "";
            int? count = 0;


            if ((eInfo.ActiveFlag != null) && (eInfo.ActiveFlag != ""))
            {
                strcond += (strcond == "") ? " where  e.ActiveFlag = '" + eInfo.ActiveFlag.Trim() + "'" : " and e.ActiveFlag = '" + eInfo.ActiveFlag.Trim() + "'";
            }

            if ((eInfo.TechnicianFlag != null) && (eInfo.TechnicianFlag != ""))
            {
                strcond += (strcond == "") ? " where  e.TechnicianFlag = '" + eInfo.TechnicianFlag.Trim() + "'" : " and e.TechnicianFlag = '" + eInfo.TechnicianFlag.Trim() + "'";
            }

            if ((eInfo.EmpCode != null) && (eInfo.EmpCode != ""))
            {
                strcond += (strcond == "") ? " where  e.EmpCode like '%" + eInfo.EmpCode.Trim() + "%'" : " and e.EmpCode like '%" + eInfo.EmpCode.Trim() + "%'";
            }

            if ((eInfo.EmpCodeTemp != null) && (eInfo.EmpCodeTemp != ""))
            {
                strcond += (strcond == "") ? " where  e.EmpCodeTemp = '" + eInfo.EmpCodeTemp.Trim() + "'" : " and e.EmpCodeTemp = '" + eInfo.EmpCodeTemp.Trim() + "'";
            }

            if ((eInfo.EmpCodeValidate != null) && (eInfo.EmpCodeValidate != ""))
            {
                strcond += (strcond == "") ? " where  e.EmpCode = '" + eInfo.EmpCodeValidate.Trim() + "'" : " and e.EmpCode = '" + eInfo.EmpCodeValidate.Trim() + "'";
            }

            if ((eInfo.EmpFname_TH != null) && (eInfo.EmpFname_TH != ""))
            {
                strcond += (strcond == "") ? " where  e.EmpFname_TH like '%" + eInfo.EmpFname_TH.Trim() + "%'" : " and e.EmpFname_TH like '%" + eInfo.EmpFname_TH.Trim() + "%'";
            }

            if ((eInfo.EmpLname_TH != null) && (eInfo.EmpLname_TH != ""))
            {
                strcond += (strcond == "") ? " where  e.EmpLname_TH like '%" + eInfo.EmpLname_TH.Trim() + "%'" : " and e.EmpLname_TH like '%" + eInfo.EmpLname_TH.Trim() + "%'";
            }

            if ((eInfo.RefCode != null) && (eInfo.RefCode != ""))
            {
                strcond += (strcond == "") ? " where  e.RefCode = '" + eInfo.RefCode.Trim() + "'" : " and e.RefCode = '" + eInfo.RefCode.Trim() + "'";
            }

            if ((eInfo.BUCode != null) && (eInfo.BUCode != "-99"))
            {
                strcond += (strcond == "") ? " where  e.BUCode = '" + eInfo.BUCode.Trim() + "'" : " and e.BUCode = '" + eInfo.BUCode.Trim() + "'";
            }
         
            DataTable dt = new DataTable();
            var LEmployee = new List<EmpListReturn>();


            try
            {
                
                string strsql = "select count(e.Id) as countEmp from " + dbName + ".dbo.Emp e  " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpListReturn()
                             {
                                 countEmp = Convert.ToInt32(dr["countEmp"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LEmployee.Count > 0)
            {
                count = LEmployee[0].countEmp;
            }

            return count;
        }

        public int? CountUserLoginByCriteria(UserLoginInfo uInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((uInfo.Username != null) && (uInfo.Username != ""))
            {
                strcond += " and  u.Username like '%" + uInfo.Username + "%'";
            }

            if ((uInfo.Password != null) && (uInfo.Password != ""))
            {
                strcond += " and  u.Password like '%" + uInfo.Password + "%'";
            }
            if ((uInfo.EmpCode != null) && (uInfo.EmpCode != ""))
            {
                strcond += " and  u.EmpCode like '%" + uInfo.EmpCode + "%'";
            }

            DataTable dt = new DataTable();
            var LUserLogin = new List<UserLoginListReturn>();


            try
            {
                string strsql = "select count(u.Id) as countLogin from " + dbName + ".dbo.UserLogin u where 1=1" + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LUserLogin = (from DataRow dr in dt.Rows

                             select new UserLoginListReturn()
                             {
                                 countLogin = Convert.ToInt32(dr["countLogin"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LUserLogin.Count > 0)
            {
                count = LUserLogin[0].countLogin;
            }

            return count;
        }

        public int? CountEmpRoleByCriteria(EmpRole eRole)
        {
            string strcond = "";
            int? count = 0;

            if ((eRole.EmpCode != null) && (eRole.EmpCode != ""))
            {
                strcond += " and  e.EmpCode like '%" + eRole.EmpCode + "%'";
            }

            if ((eRole.RoleCode != null) && (eRole.RoleCode != ""))
            {
                strcond += " and  e.RoleCode like '%" + eRole.RoleCode + "%'";
            }

            DataTable dt = new DataTable();
            var LEmpRole = new List<EmpRoleListReturn>();


            try
            {
                string strsql = "select count(e.Id) as countEmpRole from " + dbName + ".dbo.EmpRole e " +
                            "left join Role r on r.RoleCode = e.RoleCode " +

                             " where e.FlagDelete ='N' and r.FlagDelete = 'N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmpRole = (from DataRow dr in dt.Rows

                             select new EmpRoleListReturn()
                             {
                                 countEmpRole = Convert.ToInt32(dr["countEmpRole"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LEmpRole.Count > 0)
            {
                count = LEmpRole[0].countEmpRole;
            }

            return count;
        }

        public int? CountEmpInventoryByCriteria(EmpInventoryInfo eiInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((eiInfo.EmpCode != null) && (eiInfo.EmpCode != ""))
            {
                strcond += " and  ei.EmpCode like '%" + eiInfo.EmpCode + "%'";
            }

            if ((eiInfo.InventoryCode != null) && (eiInfo.InventoryCode != ""))
            {
                strcond += " and  ei.InventoryCode like '%" + eiInfo.InventoryCode + "%'";
            }

            DataTable dt = new DataTable();
            var LEmpInventory = new List<EmpInventoryListReturn>();


            try
            {
                string strsql = "select count(ei.Id) as countEmpInventory from " + dbName + ".dbo.EmpInventory ei left join " +
                            dbName + ".dbo.Inventory i on i.InventoryCode = ei.InventoryCode" +

                             " where ei.FlagDelete ='N' and i.FlagDelete = 'N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmpInventory = (from DataRow dr in dt.Rows

                            select new EmpInventoryListReturn()
                            {
                                countEmpInventory = Convert.ToInt32(dr["countEmpInventory"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LEmpInventory.Count > 0)
            {
                count = LEmpInventory[0].countEmpInventory;
            }

            return count;
        }

        public int InsertEmployee(EmpInfo eInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO Emp  (EmpCode,Title_TH,EmpFname_TH,EmpLName_TH,EmpFname_EN,EmpLName_EN,CreateDate,CreateBy,UpdateDate,UpdateBy,Mail,Mobile, ActiveFlag, PositionCode, EmpCodeTemp, BUCode,ExtensionID, FlagDelete)" +
                            " OUTPUT inserted.Id VALUES (" +
                           "'" + eInfo.EmpCode + "'," +
                           "'" + eInfo.Title_TH + "'," +
                           "'" + eInfo.EmpFname_TH + "'," +
                           "'" + eInfo.EmpLname_TH + "'," + 
                           "'" + eInfo.EmpFname_EN + "'," +
                           "'" + eInfo.EmpLname_EN + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + eInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + eInfo.UpdateBy + "'," +
                           "'" + eInfo.Mail + "'," +
                           "'" + eInfo.Mobile + "', " +
                           "'" + eInfo.ActiveFlag + "'," +
                           "'" + eInfo.PositionCode + "', " +
                           "'" + eInfo.EmpCode + "', " +
                           "'" + eInfo.BUCode + "', " +
                              "'" + eInfo.ExtensionID + "', " +
                           "'" + "N" + "' " +
                           ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertEmpRole(EmpRole eInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO EmpRole  (EmpCode,RoleCode,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + eInfo.EmpCode + "'," +
                           "'" + eInfo.RoleCode + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + eInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + eInfo.UpdateBy + "'," +
                           "'" + eInfo.FlagDelete + "'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertUserLogin(UserLoginInfo eInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO UserLogin  (UserName,Password,EmpCode,EmpCodeTemp,CreateDate,CreateBy,UpdateDate,UpdateBy)" +
                            " VALUES (" +
                           "'" + eInfo.Username + "'," +
                           "'" + eInfo.Password + "'," +
                           "'" + eInfo.EmpCode + "'," +
                           "'" + eInfo.EmpCodeTemp + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + eInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + eInfo.UpdateBy + "'" +
                            ");SELECT SCOPE_IDENTITY();";


           

            
            SqlConnection _con = new SqlConnection(APPHELPPERS.Driver.ConntectionString());

            _con.Open();


            using (SqlCommand command = new SqlCommand(strsql, _con))
            {
                i = Convert.ToInt32(command.ExecuteScalar());
                
            }

            return i;
        }

        

        public int DeleteEmpRole(EmpRole eInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.EmpRole set " +

                           " FlagDelete = 'Y'" +

                           " where Id in(" + eInfo.EmpRoleId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int DeleteEmpRoleByRoleCodeList(EmpRole eInfo)
        {
            int i = 0;

            string strsql = " Delete " + dbName + ".dbo.EmpRole " +

                           " where RoleCode in(" + eInfo.EmpRoleIdList + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int DeleteEmpRoleList(EmpRole eInfo)
        {
            int i = 0;

            string strsql = " Delete " + dbName + ".dbo.EmpRole "+

                           " where Id in(" + eInfo.EmpRoleIdList + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int DeleteUserlogin(UserLoginInfo eInfo)
        {
            int i = 0;

            string strsql = " delete " + dbName + ".dbo.UserLogin  " +

                           " where EmpCode = '" + eInfo.EmpCode + "'";
            
            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateEmployee(EmpInfo eInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Emp set " +
                            " Title_TH = '" + eInfo.Title_TH + "'," +
                            " EmpFname_TH = '" + eInfo.EmpFname_TH + "'," +
                            " EmpLName_TH ='" + eInfo.EmpLname_TH + "'," +
                            " EmpFname_EN = '" + eInfo.EmpFname_EN + "'," +
                            " EmpLName_EN ='" + eInfo.EmpLname_EN + "'," +
                            " UpdateDate ='" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " ActiveFlag ='" + eInfo.ActiveFlag + "'," +
                            " UpdateBy ='" + eInfo.UpdateBy + "'," +
                            " Mail ='" + eInfo.Mail + "'," +
                            " Mobile ='" + eInfo.Mobile + "'," +
                            " PositionCode='" + eInfo.PositionCode + "'," +
                                " ExtensionID='" + eInfo.ExtensionID + "'," +
                            " BUCode ='" + eInfo.BUCode + "'" +

                            " where Id = " + eInfo.EmpId;

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateCreateRefCodefromOneApp(EmpInfo eInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Emp set " +
                            " RefCode = '" + eInfo.RefCode + "'," +
                            " EmpCode = '" + eInfo.RefCode + "'," +
                            " UpdateDate ='" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy ='" + eInfo.UpdateBy + "'" +
                            " where Id = " + eInfo.EmpId;

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateEmpRole(EmpRole eInfo)
        {
            int i = 0;

            string strsql = "Update EmpRole  set " +
                            " RoleCode = '" + eInfo.RoleCode + "'," +
                           " UpdateDate ='" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           " UpdateBy ='" + eInfo.EmpId + "'" +
                           " where Id = " + eInfo.EmpRoleId;

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateEmpMapRole(EmpRole eInfo)
        {
            int i = 0;

            string strsql = "Update EmpRole  set " +
                            " RoleCode = '" + eInfo.RoleCode + "'," +
                           " UpdateDate ='" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           " UpdateBy ='" + eInfo.EmpId + "'" +
                           " where EmpCode = '" + eInfo.EmpCode + "'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertEmpMapRole(EmpRole eInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO EmpRole  (EmpCode,RoleCode,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + eInfo.EmpCode + "'," +
                           "'" + eInfo.RoleCode + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + eInfo.EmpId + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + eInfo.EmpId + "'," +
                           "'" + eInfo.FlagDelete + "'" +
                            ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateEmpInventory(EmpInventoryInfo eiInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.EmpInventory set " +
                            " InventoryCode = '" + eiInfo.InventoryCode + "'," +
                            " EmpCode = '" + eiInfo.EmpCode + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy = '" + eiInfo.UpdateBy + "'" +
                            " where Id =" + eiInfo.EmpInventoryId;


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertEmpInventory(EmpInventoryInfo eiInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO EmpInventory  (EmpCode,InventoryCode,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + eiInfo.EmpCode + "'," +
                           "'" + eiInfo.InventoryCode + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + eiInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + eiInfo.UpdateBy + "'," +
                           "'" + eiInfo.FlagDelete + "'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteEmployeeList(EmpInfo eInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Emp set " +

                           " ActiveFlag = 'N', FlagDelete= 'Y' " +

                           " where empcode in(" + eInfo.EmpCode + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }


        public int DeleteEmployee(EmpInfo eInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Emp set " +

                           " ActiveFlag = 'N'" +

                           " where Id in(" + eInfo.EmpId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        //Old ver has 2 params get
        public int InsertWFMapEmpRole(EmpRole eRole, int empId )
        {
            int i = 0;

            string strsql = "INSERT INTO WF_Map_Emp_Role(Role_Id,Emp_Id,UpdateDate,WF_Type_Id)" +
                            "VALUES (" +
                           "'" + eRole.RoleCode + "'," +
                           "'" + empId + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + eRole.WFType + "'" +
                           ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateWFMapEmpRole(EmpRole eRole)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.WF_Map_Emp_Role set " +
                            " Role_Id = '" + eRole.RoleCode + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " WF_Type_Id = '" + eRole.WFType + "'" +
                            " where Emp_Id =" + eRole.EmpId;


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteWFMapEmpRole(EmpRole eRole)
        {
            int i = 0;

            string strsql = " Delete " + dbName + ".dbo.WF_Map_Emp_Role" +

                           " where Emp_Id in(" + eRole.EmpId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateUserLogin(UserLoginInfo eInfo)
        {
            int i = 0;

            string strsql = "UPDATE UserLogin Set " +
                            "UserName = '" + eInfo.Username + "'," +
                            "Password = '" + eInfo.Password + "'," +
                            "EmpCode = '" + eInfo.EmpCode + "'," +
                            "UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            "UpdateBy = '" + eInfo.UpdateBy + "'" +
                            " where Id in(" + eInfo.UserLoginId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdateEmpCodefromRefCodefromOneApp(UserLoginInfo eInfo)
        {
            int i = 0;

            string strsql = "UPDATE UserLogin Set " +
                            "EmpCode = '" + eInfo.EmpCode + "'," +
                            "UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            "UpdateBy = '" + eInfo.UpdateBy + "'" +
                            " where Id in(" + eInfo.UserLoginId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
            public List<EmpListReturn> CheckRefCodeofEmployee(String refCode)
        {
            string strcond = "";

            if ((refCode != null) && (refCode != ""))
            {
                strcond += " and e.RefCode = '" + refCode + "'";
            }

            DataTable dt = new DataTable();
            var LEmployee = new List<EmpListReturn>();

            try
            {
                string strsql = " select e.* from Emp e" + " where 1=1 " +
                                         strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpListReturn()
                             {
                                 RefCode = dr["RefCode"].ToString().Trim(),
                                 
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }
        public String UpdateEmpfromOneApp(EmpInfo eInfo)
        {
            int i = 0;

            string strsql = " Update Emp  set " +
                            " EmpFname_TH = '" + eInfo.EmpFName + "'," +
                            " EmpLname_TH = '" + eInfo.EmpLName + "'," +
                            " ActiveFlag = '" + eInfo.EmpStatus + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where RefCode = " + eInfo.RefCode;


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return eInfo.RefCode;
        }
        public String InsertEmpfromOneApp(EmpInfo eInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.Emp  (RefCode,EmpCode,EmpFname_TH,EmpLname_TH,ActiveFlag,CreateDate,UpdateDate)" +
                            "VALUES (" +
                            "'" + eInfo.RefCode + "'," +
                            "'" + eInfo.RefCode + "'," +
                            "'" + eInfo.EmpFName + "'," +
                            "'" + eInfo.EmpLName + "'," +
                            "'" + eInfo.EmpStatus + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            if (i > 0)
            {
                return eInfo.RefCode;
            }
            return "";
        }
        public List<EmpListReturn> EmpCodeValidate(EmpInfo eInfo)
        {
            string strcond = "";

            if ((eInfo.EmpCode != null) && (eInfo.EmpCode != ""))
            {
                strcond += " and e.EmpCode = '" + eInfo.EmpCode.Trim() + "'";
            }
            if ((eInfo.EmpCodeTemp != null) && (eInfo.EmpCodeTemp != ""))
            {
                strcond += " and e.EmpCodeTemp = '" + eInfo.EmpCodeTemp.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var LEmployee = new List<EmpListReturn>();

            try
            {
                string strsql = " select e.* from Emp e" + " where 1=1 " +
                                         strcond;



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpListReturn()
                             {
                                 EmpId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }
        public String InsertEmployeewithRefCode(EmpInfo eInfo)
        {
            int i = 0;

            int RefCode = getMaxRef(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
            String genRefCode = "OneAppOMSEmpRef" + (DateTime.Now.Year + 543).ToString() + DateTime.Now.ToString("MM") + String.Format("{0:00000000}", RefCode);

            string strsql = "INSERT INTO " + dbName + ".dbo.Emp  (EmpCode,RunningNo,RefCode,EmpFname_TH,EmpLname_TH,ActiveFlag,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                           "VALUES (" +
                          "'" + eInfo.EmpCode + "'," +
                          "'" + RefCode + "'," +
                          "'" + genRefCode + "'," +
                          "'" + eInfo.EmpFname_TH + "'," +
                          "'" + eInfo.EmpLname_TH + "'," +
                          "'" + eInfo.ActiveFlag + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + eInfo.CreateBy + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + eInfo.UpdateBy + "'," +
                          "'" + "N" + "'" +
                          ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);
            if (i > 0)
            {
                return genRefCode;
            }
            return "";
        }
        public int getMaxRef(String year, String month)
        {
            int maxEmp = 1;

            DataTable dt = new DataTable();

            string strsql = @" select isnull(max(isnull(runningno,0)),0) + 1 max_no from " + dbName + @".dbo.Emp
                             where month(createdate) = " + month + " and year(createdate) = " + year + " and FlagDelete ='N' ";

            try
            {
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                if (dt.Rows.Count > 0)
                {
                    maxEmp = (dt.Rows[0]["max_no"] != null) ? int.Parse(dt.Rows[0]["max_no"].ToString()) : 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return maxEmp;
        }

        public int InsertUserLogin_LC(UserLoginInfo eInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO " + dbNameEmp_Profile + ".dbo.Emp_Profile  (UserName,Password,EmpCode,CompanyCode,CreateDate)" +
                            " VALUES (" +
                           "'" + eInfo.Username + "'," +
                           "'" + eInfo.Password + "'," +
                           "'" + eInfo.EmpCode + "'," +
                           "'" + CompanyCode + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +

                            ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);
            return i;
        }

        public int UpdateUserLogin_LC(UserLoginInfo eInfo)
        {
            int i = 0;

            string strsql = "UPDATE " + dbNameEmp_Profile + ".dbo.Emp_Profile Set " +
                            "UserName = '" + eInfo.Username + "'," +
                            "Password = '" + eInfo.Password + "'" +
                          

                            " where EmpCode in('" + eInfo.EmpCode + "')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<EmpListReturn> ListEmpByCriteriaUserDatail(EmpInfo eInfo)
        {
            string strcond = "";
            if ((eInfo.FlagDelete != null) && (eInfo.FlagDelete != ""))
            {
                strcond += (strcond == "") ? " where  e.FlagDelete = '" + eInfo.FlagDelete.Trim() + "'" : " and e.FlagDelete = '" + eInfo.FlagDelete.Trim() + "'";
            }

            if ((eInfo.ActiveFlag != null) && (eInfo.ActiveFlag != ""))
            {
                strcond += (strcond == "") ? " where  e.ActiveFlag = '" + eInfo.ActiveFlag.Trim() + "'" : " and e.ActiveFlag = '" + eInfo.ActiveFlag.Trim() + "'";
            }

            if ((eInfo.TechnicianFlag != null) && (eInfo.TechnicianFlag != ""))
            {
                strcond += (strcond == "") ? " where  e.TechnicianFlag = '" + eInfo.TechnicianFlag.Trim() + "'" : " and e.TechnicianFlag = '" + eInfo.TechnicianFlag.Trim() + "'";
            }

            if ((eInfo.EmpCode != null) && (eInfo.EmpCode != ""))
            {
                strcond += (strcond == "") ? " where  e.EmpCode = '" + eInfo.EmpCode.Trim() + "'" : " and e.EmpCode = '" + eInfo.EmpCode.Trim() + "'";
            }

            if ((eInfo.EmpCodeTemp != null) && (eInfo.EmpCodeTemp != ""))
            {
                strcond += (strcond == "") ? " where  e.EmpCodeTemp = '" + eInfo.EmpCodeTemp.Trim() + "'" : " and e.EmpCodeTemp = '" + eInfo.EmpCodeTemp.Trim() + "'";
            }

            if ((eInfo.EmpCodeValidate != null) && (eInfo.EmpCodeValidate != ""))
            {
                strcond += (strcond == "") ? " where  e.EmpCode = '" + eInfo.EmpCodeValidate.Trim() + "'" : " and e.EmpCode = '" + eInfo.EmpCodeValidate.Trim() + "'";
            }

            if ((eInfo.EmpFname_TH != null) && (eInfo.EmpFname_TH != ""))
            {
                strcond += (strcond == "") ? " where  e.EmpFname_TH like '%" + eInfo.EmpFname_TH.Trim() + "%'" : " and e.EmpFname_TH like '%" + eInfo.EmpFname_TH.Trim() + "%'";
            }

            if ((eInfo.EmpLname_TH != null) && (eInfo.EmpLname_TH != ""))
            {
                strcond += (strcond == "") ? " where  e.EmpLname_TH like '%" + eInfo.EmpLname_TH.Trim() + "%'" : " and e.EmpLname_TH like '%" + eInfo.EmpLname_TH.Trim() + "%'";
            }

            if ((eInfo.RefCode != null) && (eInfo.RefCode != ""))
            {
                strcond += (strcond == "") ? " where  e.RefCode = '" + eInfo.RefCode.Trim() + "'" : " and e.RefCode = '" + eInfo.RefCode.Trim() + "'";
            }

            if ((eInfo.BUCode != null) && (eInfo.BUCode != "-99"))
            {
                strcond += (strcond == "") ? " where  e.BUCode = '" + eInfo.BUCode.Trim() + "'" : " and e.BUCode = '" + eInfo.BUCode.Trim() + "'";
            }
            if ((eInfo.rowOFFSet != 0) || (eInfo.rowFetch != 0))
            {
                strcond += " ORDER BY e.Id DESC OFFSET " + eInfo.rowOFFSet + " ROWS FETCH NEXT " + eInfo.rowFetch + " ROWS ONLY";
            }

            DataTable dt = new DataTable();
            var LEmployee = new List<EmpListReturn>();

            try
            {
                string strsql = " select e.*,p.PositionName,l.LookupValue,l.LookupValue as TitleName_TH,te.LookupValue as TitleName_EN,b.LookupValue as BUName,u.Id as UserLoginId,u.Username,u.Password,e.ExtensionID" +
                                " from " + dbName + ".dbo.Emp e " +
                                 " left join  " + dbName + ".dbo.UserLogin u on e.EmpCode = u.EmpCode " +
                               " left join  " + dbName + ".dbo.Position p on e.PositionCode = p.PositionCode " +
                                " left join  " + dbName + ".dbo.Lookup l on e.Title_TH = l.LookupCode and l.LookupType ='TITLE'" +
                                 " left join  " + dbName + ".dbo.Lookup te on e.Title_EN = te.LookupCode and te.LookupType ='TITLE'" +
                                " left join  " + dbName + ".dbo.Lookup b on e.BUCode = b.LookupCode and b.LookupType ='BU'" +
                                 strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpListReturn()
                             {
                                 EmpId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                                 UserLoginId = dr["UserLoginId"].ToString().Trim(),
                                 Username = dr["Username"].ToString().Trim(),
                                 Password = dr["Password"].ToString().Trim(),
                                 EmpCodeTemp = dr["EmpCodeTemp"].ToString().Trim(),
                                 Title_TH = dr["Title_TH"].ToString().Trim(),
                                 TitleName_TH = dr["TitleName_TH"].ToString().Trim(),
                                 EmpFname_TH = dr["EmpFname_TH"].ToString().Trim(),
                                 EmpLname_TH = dr["EmpLname_TH"].ToString().Trim(),
                                 EmpName_TH = dr["EmpFname_TH"].ToString() + "  " + dr["EmpLname_TH"].ToString(),
                                 Title_EN = dr["Title_EN"].ToString().Trim(),
                                 TitleName_EN = dr["TitleName_EN"].ToString().Trim(),
                                 EmpFname_EN = dr["EmpFname_EN"].ToString().Trim(),
                                 EmpLname_EN = dr["EmpLname_EN"].ToString().Trim(),
                                 EmpName_EN = dr["EmpFname_EN"].ToString() + "    " + dr["EmpLname_EN"].ToString(),
                                 TechnicianFlag = dr["TechnicianFlag"].ToString().Trim(),
                                 PositionCode = dr["PositionCode"].ToString().Trim(),
                                 PositionName = dr["PositionName"].ToString().Trim(),
                                 LookupValue = dr["LookupValue"].ToString().Trim(),
                                 ActiveFlag = dr["ActiveFlag"].ToString().Trim(),
                                 Mail = dr["Mail"].ToString().Trim(),
                                 Mobile = dr["Mobile"].ToString().Trim(),
                                 BUCode = dr["BUCode"].ToString().Trim(),
                                 BUName = dr["BUName"].ToString().Trim(),
                                 RefCode = dr["RefCode"].ToString().Trim(),
                                 ExtensionID = dr["ExtensionID"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public List<EmpListReturn> ListEmpNoPagingByCriteria(EmpInfo eInfo)
        {
            string strcond = "";

            if ((eInfo.ActiveFlag != null) && (eInfo.ActiveFlag != ""))
            {
                strcond += " and e.ActiveFlag = '" + eInfo.ActiveFlag.Trim() + "'";
            }

            if ((eInfo.TechnicianFlag != null) && (eInfo.TechnicianFlag != ""))
            {
                strcond += " and e.TechnicianFlag = '" + eInfo.TechnicianFlag.Trim() + "'";
            }

            if ((eInfo.RefCode != null) && (eInfo.RefCode != ""))
            {
                strcond += " and e.RefCode = '" + eInfo.RefCode.Trim() + "'";
            }

            if ((eInfo.BUCode != null) && (eInfo.BUCode != ""))
            {
                strcond += " and e.BUCode = '" + eInfo.BUCode.Trim() + "'";
            }

            if ((eInfo.EmpCode != null) && (eInfo.EmpCode != ""))
            {
                strcond += " and e.EmpCode = '" + eInfo.EmpCode.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var LEmployee = new List<EmpListReturn>();

            try
            {
                string strsql = " select e.* " +
                                " from " + dbName + ".dbo.Emp e " +
                                " where FlagDelete = 'N' " +
                                strcond +
                                " ORDER BY CreateDate DESC";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpListReturn()
                             {
                                 EmpId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                                 Title_TH = dr["Title_TH"].ToString().Trim(),
                                 EmpFname_TH = dr["EmpFname_TH"].ToString().Trim(),
                                 EmpLname_TH = dr["EmpLname_TH"].ToString().Trim(),
                                 EmpName_TH = dr["EmpFname_TH"].ToString() + "  " + dr["EmpLname_TH"].ToString(),
                                 Title_EN = dr["Title_EN"].ToString().Trim(),
                                 EmpFname_EN = dr["EmpFname_EN"].ToString().Trim(),
                                 EmpLname_EN = dr["EmpLname_EN"].ToString().Trim(),
                                 EmpName_EN = dr["EmpFname_EN"].ToString() + "  " + dr["EmpLname_EN"].ToString(),
                                 TechnicianFlag = dr["TechnicianFlag"].ToString().Trim(),
                                 ActiveFlag = dr["ActiveFlag"].ToString().Trim(),
                                 Mail = dr["Mail"].ToString().Trim(),
                                 BUCode = dr["BUCode"].ToString().Trim()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }

        public List<EmpRoleListReturn> ListEmpRoleNoPagingforDDLByCriteria(EmpRole eRole)
        {
            string strcond = "";

            if ((eRole.EmpCode != null) && (eRole.EmpCode != ""))
            {
                strcond += " and  er.EmpCode like '%" + eRole.EmpCode + "%'";
            }

            if ((eRole.RoleCode != null) && (eRole.RoleCode != ""))
            {
                strcond += " and  er.RoleCode like '%" + eRole.RoleCode + "%'";
            }

            DataTable dt = new DataTable();
            var LEmployee = new List<EmpRoleListReturn>();

            try
            {
                string strsql = "SELECT er.RoleCode, r.RoleName from " + dbName + ".dbo.EmpRole er " +
                                " LEFT OUTER JOIN " + dbName + ".dbo.Role AS r ON r.RoleCode = er.RoleCode" +
                                " where (er.FlagDelete = 'N') AND (r.FlagDelete = 'N') " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmployee = (from DataRow dr in dt.Rows

                             select new EmpRoleListReturn()
                             {
                                 RoleCode = dr["RoleCode"].ToString().Trim(),
                                 RoleName = dr["RoleName"].ToString().Trim(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmployee;
        }                        
        
    }
}
