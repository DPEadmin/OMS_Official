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
    public class RoutingMappingDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();



        public List<RoutingMapDriverListReturn> ListRoutingDriverByCriteriaNoPaging(RoutingMapDriverInfo vInfo)
        {
            string strcond = "";

            if ((vInfo.Routing_code != null) && (vInfo.Routing_code != ""))
            {
                strcond += " and Routing_code like '%" + vInfo.Routing_code + "%'";
            }


            DataTable dt = new DataTable();
            var LRouting = new List<RoutingMapDriverListReturn>();

            try
            {
                string strsql = " select d.FName,d.LName,r.* from RoutingMapDriver r" +
                                " left join Driver d on d.Driver_no = r.Driver_no and d.FlagDelete = 'N'" +
                                " where FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                            select new RoutingMapDriverListReturn()
                            {
                                RoutingDriverId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                Routing_code = dr["Routing_code"].ToString().Trim(),
                                Driver_no = dr["Driver_no"].ToString().Trim(),
                                FName = dr["FName"].ToString().Trim(),
                                LName = dr["LName"].ToString().Trim(),
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

            return LRouting;
        }

        public List<RoutingMapDriverListReturn> ListRoutingDriverByCriteria_showgv(RoutingMapDriverInfo vInfo)
        {
            string strcond = "";

            if ((vInfo.Routing_code != null) && (vInfo.Routing_code != ""))
            {
                strcond += " and Routing_code like '%" + vInfo.Routing_code + "%'";
            }

       
            if ((vInfo.Driver_no != null) && (vInfo.Driver_no != ""))
            {
                strcond += " and d.Driver_no like '%" + vInfo.Driver_no + "%'";
            }
            if ((vInfo.FName != null) && (vInfo.FName != ""))
            {
                strcond += " and d.FName like '%" + vInfo.FName + "%'";
            }
            if ((vInfo.LName != null) && (vInfo.LName != ""))
            {
                strcond += " and d.LName like '%" + vInfo.LName + "%'";
            }
            if ((vInfo.RoleCode != null) && (vInfo.RoleCode != "") && (vInfo.RoleCode != "-99"))
            {
                strcond += " and er.RoleCode = '" + vInfo.RoleCode + "'";
            }


            DataTable dt = new DataTable();
            var LRouting = new List<RoutingMapDriverListReturn>();

            try
            {
                string strsql = " select e.EmpFname_TH, e.EmpLName_TH,re.RoleName,r.* from RoutingMapDriver r" +
                                " inner join Emp AS e ON e.EmpCode = r.Driver_no AND e.FlagDelete = 'N'" +
                                " inner join EmpRole er on er.EmpCode=e.EmpCode" +
                                "  inner join role re on re.RoleCode = er.RoleCode " +
                                " where r.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY e.EmpCode DESC OFFSET " + vInfo.rowOFFSet + " ROWS FETCH NEXT " + vInfo.rowFetch + " ROWS ONLY";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                             select new RoutingMapDriverListReturn()
                             {
                                 RoutingDriverId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 Routing_code = dr["Routing_code"].ToString().Trim(),
                                 Driver_no = dr["Driver_no"].ToString().Trim(),
                                 FName = dr["EmpFname_TH"].ToString().Trim(),
                                 LName = dr["EmpLName_TH"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 RoleName = dr["RoleName"].ToString(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LRouting;
        }

        //the old name (CountCustomerListByCriteria) is already used in CustomerDAO 
        public int? CountRoutingDriverListByCriteria(RoutingMapDriverInfo vInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((vInfo.Routing_code != null) && (vInfo.Routing_code != ""))
            {
                strcond += " and Routing_code ='" + vInfo.Routing_code + "'";
            }

            

            if ((vInfo.Driver_no != null) && (vInfo.Driver_no != ""))
            {
                strcond += " and d.Driver_no like '%" + vInfo.Driver_no + "%'";
            }


            DataTable dt = new DataTable();
            var LRouting = new List<RoutingMapDriverListReturn>();


            try
            {
                string strsql = " select count(r.Id) as countRoutingDriver from RoutingMapDriver r" +
                " inner join Emp AS e ON e.EmpCode = r.Driver_no AND e.FlagDelete = 'N'" +
                " inner join EmpRole er on er.EmpCode=e.EmpCode" +
                "  inner join role re on re.RoleCode = er.RoleCode " +
                " where r.FlagDelete ='N' " + strcond;
                

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                             select new RoutingMapDriverListReturn()
                             {
                                 countRoutingDriver = Convert.ToInt32(dr["countRoutingDriver"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LRouting.Count > 0)
            {
                count = LRouting[0].countRoutingDriver;
            }

            return count;
        }

        //the old name (InsertCustomer) is already used in CustomerDAO 
        public int InsertRoutingDriver(RoutingMapDriverInfo vInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.RoutingMapDriver  (Routing_code,Driver_no,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + vInfo.Routing_code + "'," +
                           "'" + vInfo.Driver_no + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + vInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + vInfo.UpdateBy + "'," +
                           "'" + vInfo.FlagDelete + "'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        //the old name (UpdateCustomer) is already used in CustomerDAO 
        public int? UpdateRoutingDriver(RoutingMapDriverInfo vInfo)
        {
            int? i = 0;

            string strsql = " Update " + dbName + ".dbo.RoutingMapDriver set " +
                            " Routing_code = '" + vInfo.Routing_code + "'," +
                            " Driver_no = '" + vInfo.Driver_no + "'," +
                            " UpdateBy = '" + vInfo.UpdateBy + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'"+
                            " where id ='" + vInfo.RoutingDriverId + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        //the old name (DeleteCustomer) is already used in CustomerDAO 
        public int DeleteRoutingDriver(RoutingMapDriverInfo vInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.RoutingMapDriver set FlagDelete = 'Y' where Id in (" + vInfo.Routing_code + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        //the old name (CustomerCheck) is already used in CustomerDAO 
        public List<RoutingListReturn> RoutingDriverCheck(RoutingInfo vInfo)
        {
            string strcond = "";

            if ((vInfo.Routing_code != null) && (vInfo.Routing_code != ""))
            {
                strcond += " and Routing_code ='" + vInfo.Routing_code + "'";
            }

            DataTable dt = new DataTable();
            var LRouting = new List<RoutingListReturn>();

            try
            {
                string strsql = " select * from " + dbName + ".dbo.Routing " +

                               " where FlagDelete ='N' " + strcond;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                             select new RoutingListReturn()
                             {
                                 Routing_code = dr["Routing_code"].ToString().Trim(),
                                 

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LRouting;
        }

        //cannot find any related material between params and the old name (ListCustomerByCriteria_DDl)
        //the name has changed
        public List<VechicleListReturn> ListRoutingDriverByCriteria_DDl(VechicleInfo vInfo)
        {
            string strcond = "";

            if ((vInfo.VechicleId != null) && (vInfo.VechicleId != 0))
            {
                strcond += " and  c.Id =" + vInfo.VechicleId;
            }

            
            if ((vInfo.Vechicle_Band != null) && (vInfo.Vechicle_Band != ""))
            {
                strcond += " and  c.Vechicle_Band = '" + vInfo.Vechicle_Band + "'";
            }
            if ((vInfo.Vechicle_Lookup != null) && (vInfo.Vechicle_Lookup != ""))
            {
                strcond += " and  c.Vechicle_Lookup = '" + vInfo.Vechicle_Lookup + "'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<VechicleListReturn>();

            try
            {
                string strsql = "  select c.*,Vechicle_Band as car_band,ltype.LookupValue as CAR_TYPE from " + dbName + ".dbo.Vechicle c  " +
                              //   "inner join lookup lb on c.vechicle_band = lb.lookupcode and lb.LookupType = 'CAR_BAND' " +
                                  "inner join lookup ltype on c.Vechicle_Lookup = ltype.lookupcode and ltype.LookupType = 'CAR_TYPE' " +

                               " where c.FlagDelete ='N' " + strcond;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new VechicleListReturn()
                             {
                                 VechicleId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 Vechicle_No = dr["Vechicle_No"].ToString().Trim(),
                                 Vechicle_Band = dr["Vechicle_Band"].ToString().Trim(),
                                 Name_Band = dr["car_band"].ToString().Trim(),
                                 Vechicle_Lookup = dr["Vechicle_Lookup"].ToString().Trim(),
                                 Name_TypeCar = dr["CAR_TYPE"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 CarId = dr["Id"].ToString(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public List<RoutingMapVehicleListReturn> ListRoutingVechicleNoPagingByCriteria(RoutingMapVehicleInfo vInfo)
        {
            string strcond = "";

            if ((vInfo.Routing_code != null) && (vInfo.Routing_code != ""))
            {
                strcond += " and Routing_code like '%" + vInfo.Routing_code + "%'";
            }

            if ((vInfo.Vechicle_No != null) && (vInfo.Vechicle_No != ""))
            {
                strcond += " and Vechicle_No like '%" + vInfo.Vechicle_No + "%'";
            }


            DataTable dt = new DataTable();
            var LRouting = new List<RoutingMapVehicleListReturn>();

            try
            {
                string strsql = " select * from RoutingMapVehicle" +
                                " where FlagDelete ='N' " + strcond;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                            select new RoutingMapVehicleListReturn()
                            {
                                RoutingVechicleId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                Routing_code = dr["Routing_code"].ToString().Trim(),
                                Vechicle_No = dr["Vechicle_No"].ToString().Trim(),
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

            return LRouting;
        }

        public List<RoutingMapVehicleListReturn> ListRoutingVechicleByCriteria_showgv(RoutingMapVehicleInfo vInfo)
        {
            string strcond = "";

            if ((vInfo.Routing_code != null) && (vInfo.Routing_code != ""))
            {
                strcond += " and r.Routing_code like '%" + vInfo.Routing_code + "%'";
            }

            if ((vInfo.Vechicle_No != null) && (vInfo.Vechicle_No != ""))
            {
                strcond += " and r.Vechicle_No like '%" + vInfo.Vechicle_No + "%'";
            }

            if ((vInfo.Vechicle_Band != null) && (vInfo.Vechicle_Band != "") && (vInfo.Vechicle_Band != "-99"))
            {
                strcond += " and Vechicle_Band like '%" + vInfo.Vechicle_Band + "%'";
            }
            
            if ((vInfo.Vechicle_Lookup != null) && (vInfo.Vechicle_Lookup != "") && (vInfo.Vechicle_Lookup != "-99"))
            {
                strcond += " and Vechicle_Lookup = '" + vInfo.Vechicle_Lookup + "'";
            }


            if ((vInfo.Vechicle_Active != "-99") && (vInfo.Vechicle_Active != "")&& (vInfo.Vechicle_Active != null))
            {
                strcond += " and v.active like '%" + vInfo.Vechicle_Active + "%'";
            }

            DataTable dt = new DataTable();
            var LRouting = new List<RoutingMapVehicleListReturn>();

            try
            {
                string strsql = " select rt.Routing_name,v.Vechicle_Band as car_band,ltype.LookupValue as car_type,Vechicle_Lookup,r.* " +
                    " , r.FlagDelete,v.Active,v.Vechicle_Model from RoutingMapVehicle as r" +
                                " left join Vechicle v on v.Vechicle_No = r.Vechicle_No and v.FlagDelete = 'N'" +
                                " left join Routing rt on rt.Routing_code = r.Routing_code and rt.FlagDelete = 'N'"+
                             
                                " inner join lookup ltype on v.Vechicle_Lookup = ltype.lookupcode and ltype.LookupType = 'CAR_TYPE' " +
                                " where r.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY Id DESC OFFSET " + vInfo.rowOFFSet + " ROWS FETCH NEXT " + vInfo.rowFetch + " ROWS ONLY";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                            select new RoutingMapVehicleListReturn()
                            {
                                RoutingVechicleId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                Routing_code = dr["Routing_code"].ToString().Trim(),
                                name_Routing = dr["Routing_name"].ToString().Trim(),
                                Vechicle_No = dr["Vechicle_No"].ToString().Trim(),
                                Band_Name = dr["car_band"].ToString().Trim(),
                                TypeCar_Name = dr["car_type"].ToString().Trim(),
                                CreateBy = dr["CreateBy"].ToString(),
                                CreateDate = dr["CreateDate"].ToString(),
                                UpdateBy = dr["UpdateBy"].ToString(),
                                UpdateDate = dr["UpdateDate"].ToString(),
                                FlagDelete = dr["FlagDelete"].ToString(),
                                Vechicle_Model = dr["Vechicle_Model"].ToString(),
                                Vechicle_Active = dr["Active"].ToString(),
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LRouting;
        }

        //the old name (CountCustomerListByCriteria) is already used in CustomerDAO 
        public int? CountRoutingVechicleListByCriteria(RoutingMapVehicleInfo vInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((vInfo.Routing_code != null) && (vInfo.Routing_code != ""))
            {
                strcond += " and r.Routing_code ='" + vInfo.Routing_code + "'";
            }

            if ((vInfo.Vechicle_No != null) && (vInfo.Vechicle_No != ""))
            {
                strcond += " and r.Vechicle_No like '%" + vInfo.Vechicle_No + "%'";
            }

            if ((vInfo.Vechicle_Band != null) && (vInfo.Vechicle_Band != ""))
            {
                strcond += " and Vechicle_Band like '%" + vInfo.Vechicle_Band + "%'";
            }


            if ((vInfo.Vechicle_Lookup != null) && (vInfo.Vechicle_Lookup != ""))
            {
                strcond += " and ltype.LookupValue like '%" + vInfo.Vechicle_Lookup + "%'";
            }


            DataTable dt = new DataTable();
            var LRouting = new List<RoutingMapVehicleListReturn>();


            try
            {
                string strsql = "select count(r.Id) as countRoutingVechicle from " + dbName + ".dbo.RoutingMapVehicle r" +
                                " left join Vechicle v on v.Vechicle_No = r.Vechicle_No and v.FlagDelete = 'N'" +
                                " left join Routing rt on rt.Routing_code = r.Routing_code and rt.FlagDelete = 'N'" +
                              
                                " inner join lookup ltype on v.Vechicle_Lookup = ltype.lookupcode and ltype.LookupType = 'CAR_TYPE' " +
                                " where r.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                            select new RoutingMapVehicleListReturn()
                            {
                                countRoutingVechicle = Convert.ToInt32(dr["countRoutingVechicle"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LRouting.Count > 0)
            {
                count = LRouting[0].countRoutingVechicle;
            }

            return count;
        }

        //the old name (InsertCustomer) is already used in CustomerDAO 
        public int InsertRoutingVechicle(RoutingMapVehicleInfo vInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.RoutingMapVehicle  (Routing_code,Vechicle_No,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + vInfo.Routing_code + "'," +
                           "'" + vInfo.Vechicle_No + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + vInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + vInfo.UpdateBy + "'," +
                           "'N'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        //the old name (UpdateCustomer) is already used in CustomerDAO 
        public int? UpdateRoutingVechicle(RoutingMapVehicleInfo vInfo)
        {
            int? i = 0;

            string strsql = " Update " + dbName + ".dbo.RoutingMapVehicle  " +
                            " set Flagdelete = 'Y' , UpdateDate = '" + DateTime.Now.ToString("MM / dd / yyyy HH: mm: ss") + "'," +
                            " UpdateBy = '" + vInfo.UpdateBy + "' where Routing_code = '" + vInfo.Routing_code + "' and FlagDelete = 'N'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        //the old name (DeleteCustomer) is already used in CustomerDAO 
        public int DeleteRoutingVechicle(RoutingMapVehicleInfo vInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.RoutingMapVehicle set FlagDelete = 'Y' where Id in (" + vInfo.Routing_code + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        //the old name (CustomerCheck) is already used in CustomerDAO 
        public List<RoutingMapVehicleListReturn> RoutingVechicleCheck(RoutingMapVehicleInfo vInfo)
        {
            string strcond = "";

            if ((vInfo.Routing_code != null) && (vInfo.Routing_code != ""))
            {
                strcond += " and Routing_code ='" + vInfo.Routing_code + "'";
            }

            DataTable dt = new DataTable();
            var LRouting = new List<RoutingMapVehicleListReturn>();

            try
            {
                string strsql = " select * from " + dbName + ".dbo.RoutingMapVehicle " +

                               " where FlagDelete ='N' " + strcond;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                            select new RoutingMapVehicleListReturn()
                            {
                                Routing_code = dr["Routing_code"].ToString().Trim(),


                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LRouting;
        }

        //cannot find any related material between params and the old name (ListCustomerByCriteria_DDl)
        //the name has changed
        public List<VechicleListReturn> ListRoutingVechicleByCriteria_DDl(VechicleInfo vInfo)
        {
            string strcond = "";

            if ((vInfo.VechicleId != null) && (vInfo.VechicleId != 0))
            {
                strcond += " and  c.Id =" + vInfo.VechicleId;
            }

            
            if ((vInfo.Vechicle_Band != null) && (vInfo.Vechicle_Band != ""))
            {
                strcond += " and  c.Vechicle_Band = '" + vInfo.Vechicle_Band + "'";
            }
            if ((vInfo.Vechicle_Lookup != null) && (vInfo.Vechicle_Lookup != ""))
            {
                strcond += " and  c.Vechicle_Lookup = '" + vInfo.Vechicle_Lookup + "'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<VechicleListReturn>();

            try
            {
                string strsql = "  select c.*,Vechicle_Band as car_band,ltype.LookupValue as CAR_TYPE from " + dbName + ".dbo.Vechicle c  " +
                                 "inner join lookup lb on c.vechicle_band = lb.lookupcode and lb.LookupType = 'CAR_BAND' " +
                                  "inner join lookup ltype on c.Vechicle_Lookup = ltype.lookupcode and ltype.LookupType = 'CAR_TYPE' " +

                               " where c.FlagDelete ='N' " + strcond;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new VechicleListReturn()
                             {
                                 VechicleId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 Vechicle_No = dr["Vechicle_No"].ToString().Trim(),
                                 Vechicle_Band = dr["Vechicle_Band"].ToString().Trim(),
                                 Name_Band = dr["car_band"].ToString().Trim(),
                                 Vechicle_Lookup = dr["Vechicle_Lookup"].ToString().Trim(),
                                 Name_TypeCar = dr["CAR_TYPE"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 CarId = dr["Id"].ToString(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }


        public List<DriverListReturn> ListRoutingDriverInsertByCriteria_showgv(DriverInfo vInfo)
        {
            string strcond = "";

            

            if ((vInfo.FName != null) && (vInfo.FName != ""))
            {
                strcond += " and e.EmpFname_TH like '%" + vInfo.FName + "%'";
            }
            if ((vInfo.LName != null) && (vInfo.LName != ""))
            {
                strcond += " and e.EmpLName_TH like '%" + vInfo.LName + "%'";
            }
            if ((vInfo.Driver_No != null) && (vInfo.Driver_No != ""))
            {
                 strcond += " and e.EmpCode like '%" + vInfo.Driver_No + "%'";
            }
            if ((vInfo.Driver_No != null) && (vInfo.Driver_No != ""))
            {
                strcond += " and e.EmpCode like '%" + vInfo.Driver_No + "%'";
            }
            if ((vInfo.RoleCode != null) && (vInfo.RoleCode != "") && (vInfo.RoleCode != "-99"))
            {
                strcond += " and er.RoleCode = '" + vInfo.RoleCode + "'";
            }
            DataTable dt = new DataTable();
            var LRouting = new List<DriverListReturn>();

            try
            {
                string strsql = " select e.EmpCode,e.EmpFname_TH,e.EmpLName_TH,r.RoleName ,rmd.Driver_no from emp e" +
                             " inner join EmpRole er on e.EmpCode=er.EmpCode" +
                             " inner join Role r	on er.RoleCode= r.RoleCode" +
                             " left join RoutingMapDriver rmd on rmd.Driver_no =e.EmpCode and rmd.FlagDelete = 'N' and rmd.Routing_code = '" + vInfo.Routing_code + "' " +
                             " where rmd.Routing_code is null and e.ActiveFlag='Y' and e.FlagDelete='N' " + strcond;

                

                strsql += " ORDER BY e.EmpFname_TH ASC OFFSET " + vInfo.rowOFFSet + " ROWS FETCH NEXT " + vInfo.rowFetch + " ROWS ONLY";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                            select new DriverListReturn()
                            {
                              
                                Driver_No = dr["EmpCode"].ToString().Trim(),
                                FName = dr["EmpFname_TH"].ToString().Trim(),
                                LName = dr["EmpLName_TH"].ToString().Trim(),
                                RoleName = dr["RoleName"].ToString().Trim(),


                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LRouting;
        }


        public int? CountRoutingDriverInsertByCriteria(DriverInfo vInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((vInfo.FName != null) && (vInfo.FName != ""))
            {
                strcond += " and e.EmpFname_TH like '%" + vInfo.FName + "%'";
            }
            if ((vInfo.LName != null) && (vInfo.LName != ""))
            {
                strcond += " and e.EmpLName_TH like '%" + vInfo.LName + "%'";
            }

            if ((vInfo.Driver_No != null) && (vInfo.Driver_No != ""))
            {
                strcond += " and e.EmpCode like '%" + vInfo.Driver_No + "%'";
            }


            if ((vInfo.RoleCode != null) && (vInfo.RoleCode != "") && (vInfo.RoleCode != "-99"))
            {
                strcond += " and er.RoleCode = '" + vInfo.RoleCode + "'";
            }

            DataTable dt = new DataTable();
            var LRouting = new List<DriverListReturn>();


            try
            {
                string strsql = " select count(e.EmpCode) as countDriver from emp e" +
             " inner join EmpRole er on e.EmpCode=er.EmpCode" +
             " inner join Role r	on er.RoleCode= r.RoleCode" +
             " left join RoutingMapDriver rmd on rmd.Driver_no =e.EmpCode and rmd.FlagDelete = 'N' and rmd.Routing_code = '" + vInfo.Routing_code + "' " +
             " where rmd.Routing_code is null and e.ActiveFlag='Y' and e.FlagDelete='N' " + strcond;


                

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                            select new DriverListReturn()
                            {
                                countDriver = Convert.ToInt32(dr["countDriver"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LRouting.Count > 0)
            {
                count = LRouting[0].countDriver;
            }

            return count;
        }



        public int? CountRoutingVehicleInsertByCriteria(VechicleInfo vInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((vInfo.Vechicle_No != null) && (vInfo.Vechicle_No != ""))
            {
                strcond += " and v.Vechicle_No like '%" + vInfo.Vechicle_No + "%'";
            }

            if ((vInfo.Vechicle_Band != null) && (vInfo.Vechicle_Band != ""))
            {
                strcond += " and lb.Lookup_Value like '%" + vInfo.Vechicle_Band + "%'";
            }


            if ((vInfo.Vechicle_Lookup != null) && (vInfo.Vechicle_Lookup != ""))
            {
                strcond += " and ltype.Lookup_Value like '%" + vInfo.Vechicle_Lookup + "%'";
            }



            DataTable dt = new DataTable();
            var LRouting = new List<VechicleListReturn>();


            try
            {
                string strsql = " select count(v.Id) as countVehicle from " + dbName + ".dbo.Vechicle as v " +
                                " inner join lookup lb on v.vechicle_band = lb.lookupcode and lb.LookupType = 'CAR_BAND' " +
                                " inner join lookup ltype on v.Vechicle_Lookup = ltype.lookupcode and ltype.LookupType = 'CAR_TYPE' " +
                                " left join RoutingMapVehicle as rv on rv.Vechicle_No = v.Vechicle_No and v.FlagDelete = 'N' and rv.FlagDelete = 'N' and rv.Routing_code = '" + vInfo.Routing_code + "' " +
                                " where rv.Routing_code is null and v.FlagDelete = 'N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                            select new VechicleListReturn()
                            {
                                countVechicle = Convert.ToInt32(dr["countVehicle"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LRouting.Count > 0)
            {
                count = LRouting[0].countVechicle;
            }

            return count;
        }



        public List<VechicleListReturn> ListRoutingVechicleInsertByCriteria_showgv(VechicleInfo vInfo)
        {
            string strcond = "";

            

            if ((vInfo.Vechicle_No != null) && (vInfo.Vechicle_No != ""))
            {
                strcond += " and v.Vechicle_No like '%" + vInfo.Vechicle_No + "%'";
            }

            if ((vInfo.Vechicle_Band != null) && (vInfo.Vechicle_Band != ""))
            {
                strcond += " and Vechicle_Band like '%" + vInfo.Vechicle_Band + "%'";
            }


            if ((vInfo.Vechicle_Lookup != "-99") && (vInfo.Vechicle_Lookup != ""))
            {
                strcond += " and v.Vechicle_Lookup like '%" + vInfo.Vechicle_Lookup + "%'";
            }
            if ((vInfo.Vechicle_Active != "-99") && (vInfo.Vechicle_Active != "") && (vInfo.Vechicle_Active != null))
            {
                strcond += " and v.Active = '" + vInfo.Vechicle_Active + "'";
            }


            DataTable dt = new DataTable();
            var LRouting = new List<VechicleListReturn>();

            try
            {
                string strsql = " select v.*,v.Vechicle_No,Vechicle_Band as car_band,ltype.LookupValue as car_type from vechicle as v " +
                               // " inner join lookup lb on v.vechicle_band = lb.lookupcode and lb.LookupType = 'CAR_BAND' " +
                                " inner join lookup ltype on v.Vechicle_Lookup = ltype.lookupcode and ltype.LookupType = 'CAR_TYPE' " +
                                " left join RoutingMapVehicle as rv on rv.Vechicle_No = v.Vechicle_No and v.FlagDelete = 'N' and rv.FlagDelete = 'N' and rv.Routing_code = '" + vInfo.Routing_code + "' " +
                                " where rv.Routing_code is null and v.FlagDelete = 'N' " + strcond;

                strsql += " ORDER BY v.Id DESC OFFSET " + vInfo.rowOFFSet + " ROWS FETCH NEXT " + vInfo.rowFetch + " ROWS ONLY";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                            select new VechicleListReturn()
                            {
                                VechicleId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                Vechicle_No = dr["Vechicle_No"].ToString().Trim(),
                                Vechicle_Band = dr["Vechicle_Band"].ToString().Trim(),
                                Name_Band = dr["car_band"].ToString().Trim(),
                                Vechicle_Lookup = dr["Vechicle_Lookup"].ToString().Trim(),
                                Name_TypeCar = dr["CAR_TYPE"].ToString().Trim(),
                                CreateBy = dr["CreateBy"].ToString(),
                                CreateDate = dr["CreateDate"].ToString(),
                                UpdateBy = dr["UpdateBy"].ToString(),
                                UpdateDate = dr["UpdateDate"].ToString(),
                                FlagDelete = dr["FlagDelete"].ToString(),
                                Vechicle_Model = dr["Vechicle_Model"].ToString(),
                                Active = dr["Active"].ToString(),
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LRouting;
        }


        public int InsertRoutingInventory(RoutingMapInventoryInfo vInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.RoutingMapInventory  (Routing_code,Inventory_Code,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + vInfo.Routing_code + "'," +
                           "'" + vInfo.Inventory_Code + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + vInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + vInfo.UpdateBy + "'," +
                           "'" + vInfo.FlagDelete + "'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public List<RoutingMapInventoryDetailInfo> ListRoutingInventoryByCriteria_showgv(RoutingMapInventoryDetailInfo vInfo)
        {
            string strcond = "";



            if ((vInfo.Inventory_Code != null) && (vInfo.Inventory_Code != ""))
            {
                strcond += " and i.InventoryCode like '%" + vInfo.Inventory_Code + "%'";
            }
            if ((vInfo.Inventory_name != null) && (vInfo.Inventory_name != ""))
            {
                strcond += " and i.Inventoryname like '%" + vInfo.Inventory_name + "%'";
            }
            if ((vInfo.Routing_code != null) && (vInfo.Routing_code != ""))
            {
                strcond += " and r.Routing_code like '%" + vInfo.Routing_code + "%'";
            }
            if ((vInfo.Routing_name != null) && (vInfo.Routing_name != ""))
            {
                strcond += " and r.Routing_name like '%" + vInfo.Routing_name + "%'";
            }

            DataTable dt = new DataTable();
            var LRouting = new List<RoutingMapInventoryDetailInfo>();

            try
            {
                string strsql = " SELECT i.InventoryCode, i.InventoryName, r.Routing_name,r.Routing_code,rmi.id,rmi.FlagDelete,rmi.CreateDate,rmi.CreateBy,rmi.UpdateBy,rmi.UpdateDate from Inventory as i" +
                                " left join RoutingMapInventory as rmi on rmi.Inventory_Code = i.InventoryCode  " +
                                " inner join Routing as r on r.Routing_code = rmi.Routing_code " +
                                " where  i.FlagDelete = 'N' and r.FlagDelete='N'  and rmi.FlagDelete = 'N' " + strcond;

                strsql += " ORDER BY rmi.Id ASC OFFSET " + vInfo.rowOFFSet + " ROWS FETCH NEXT " + vInfo.rowFetch + " ROWS ONLY";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                            select new RoutingMapInventoryDetailInfo()
                            {
                                RoutinginventoryId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                Routing_code = dr["Routing_code"].ToString().Trim(),
                                Inventory_name = dr["Inventoryname"].ToString().Trim(),
                                Inventory_Code = dr["InventoryCode"].ToString().Trim(),
                                Routing_name = dr["Routing_name"].ToString().Trim(),
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

            return LRouting;
        }

        public List<RoutingMapInventoryDetailInfo> ListRoutingInventoryNoPagingByCriteria(RoutingMapInventoryDetailInfo vInfo)
        {
            string strcond = "";



            if ((vInfo.Inventory_Code != null) && (vInfo.Inventory_Code != ""))
            {
                strcond += " and i.InventoryCode like '%" + vInfo.Inventory_Code + "%'";
            }
            if ((vInfo.Inventory_name != null) && (vInfo.Inventory_name != ""))
            {
                strcond += " and i.Inventoryname like '%" + vInfo.Inventory_name + "%'";
            }
            if ((vInfo.Routing_code != null) && (vInfo.Routing_code != ""))
            {
                strcond += " and r.Routing_code like '%" + vInfo.Routing_code + "%'";
            }
            if ((vInfo.Routing_name != null) && (vInfo.Routing_name != ""))
            {
                strcond += " and r.Routing_name like '%" + vInfo.Routing_name + "%'";
            }

            DataTable dt = new DataTable();
            var LRouting = new List<RoutingMapInventoryDetailInfo>();

            try
            {
                string strsql = " SELECT i.InventoryCode, i.InventoryName, r.Routing_name,r.Routing_code,rmi.id,rmi.FlagDelete,rmi.CreateDate,rmi.CreateBy,rmi.UpdateBy,rmi.UpdateDate from Inventory as i" +
                                " left join RoutingMapInventory as rmi on rmi.Inventory_Code = i.InventoryCode  " +
                                " inner join Routing as r on r.Routing_code = rmi.Routing_code " +
                                " where  i.FlagDelete = 'N' and r.FlagDelete='N'  and rmi.FlagDelete = 'N' " + strcond;

                strsql += " ORDER BY rmi.Id";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                            select new RoutingMapInventoryDetailInfo()
                            {
                                RoutinginventoryId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                Routing_code = dr["Routing_code"].ToString().Trim(),
                                Inventory_name = dr["Inventoryname"].ToString().Trim(),
                                Inventory_Code = dr["InventoryCode"].ToString().Trim(),
                                Routing_name = dr["Routing_name"].ToString().Trim(),
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

            return LRouting;
        }

        public int? CountRoutinginventoryByCriteria(RoutingMapInventoryDetailInfo vInfo)
        {
            string strcond = "";
            int? count = 0;


            if ((vInfo.Routing_code != null) && (vInfo.Routing_code != ""))
            {
                strcond += " and r.Routing_code = '" + vInfo.Routing_code + "'";
            }



            DataTable dt = new DataTable();
            var LRouting = new List<RoutingMapInventoryDetailInfo>();


            try
            {


                string strsql = " select count(rmi.Id) as countRoutingInventory from Inventory as i" +
                              " left join RoutingMapInventory as rmi on rmi.Inventory_Code = i.InventoryCode  " +
                              " inner join Routing as r on r.Routing_code = rmi.Routing_code " +
                              " where  i.FlagDelete = 'N' and rmi.FlagDelete = 'N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                            select new RoutingMapInventoryDetailInfo()
                            {
                                countRoutingInventory = Convert.ToInt32(dr["countRoutingInventory"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LRouting.Count > 0)
            {
                count = LRouting[0].countRoutingInventory;
            }

            return count;
        }


      
        
        public int DeleteRoutingInventory(RoutingMapInventoryDetailInfo vInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.RoutingMapInventory set FlagDelete = 'Y' where Id in (" + vInfo.Routing_code + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int updateRoutingInventory(RoutingMapInventoryInfo vInfo)
        {
            int i = 0;

            string strcond = "";

 
            if ((vInfo.Inventory_Code != null) && (vInfo.Inventory_Code != ""))
            {
                strcond += " Inventory_Code = '" + vInfo.Inventory_Code + "',";
            }
            string strsql = " Update " + dbName + ".dbo.RoutingMapInventory set " + strcond + " UpdateBy = '" + vInfo.UpdateBy + "'," +

                      " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +


                      " where Routing_code = '" + vInfo.Routing_code + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;

       
        }
    }
}
