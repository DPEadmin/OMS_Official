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
    public class VechicleDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        //seems this method has incorrect query, Customer table doesn't have any vehicle keys
        //so this method cannot run properly
        //the old name (ListCustomerByCriteria) is already used in CustomerDAO 
        public List<VechicleListReturn> ListVechicleByCriteria(VechicleInfo vInfo)
        {
            string strcond = "";

            if ((vInfo.VechicleId != null) && (vInfo.VechicleId != 0))
            {
                strcond += " and  c.Id =" + vInfo.VechicleId;
            }

            if ((vInfo.Vechicle_No != null) && (vInfo.Vechicle_No != ""))
            {
                strcond += " and  c.Vechicle_No like '%" + vInfo.Vechicle_No + "%'";
            }
            if ((vInfo.Vechicle_Lookup != null) && (vInfo.Vechicle_Lookup != ""))
            {
                strcond += " and  c.Vechicle_Lookup like '%" + vInfo.Vechicle_Lookup + "%'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<VechicleListReturn>();

            try
            {
                string strsql = " select c.*,p.PhoneNumber,p.PhoneType  from " + dbName + ".dbo.Customer c left join  " + dbName + ".dbo.CustomerPhone p on c.CustomerCode = p.CustomerCode " +

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
                                 Vechicle_Lookup = dr["Vechicle_Lookup"].ToString().Trim(),
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

            return LCampaign;
        }

        //seems this method has incorrect query, Customer table doesn't have any vehicle keys
        //so this method cannot run properly
        //the old name (ListCustomerDetailByCriteria) is already used in CustomerDAO 
        public List<VechicleListReturn> ListVechicleDetailByCriteria(CustomerPhoneInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.PhoneNumber != null) && (cInfo.PhoneNumber != ""))
            {
                strcond += " and  p.PhoneNumber ='" + cInfo.PhoneNumber + "'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<VechicleListReturn>();

            try
            {
                string strsql = " select c.*,p.PhoneNumber,p.PhoneType  from " + dbName + ".dbo.Customer c left join  " + dbName + ".dbo.CustomerPhone p on c.CustomerCode = p.CustomerCode " +

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
                                 Vechicle_Lookup = dr["Vechicle_Lookup"].ToString().Trim(),
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

            return LCampaign;
        }

        //the old name (ListCustomerByCriteria_showgv) is already used in CustomerDAO 
        public List<VechicleListReturn> ListVechicleByCriteria_showgv(VechicleInfo vInfo)
        {
            string strcond = "";

            if ((vInfo.VechicleId != null) && (vInfo.VechicleId != 0))
            {
                strcond += " and  c.Id =" + vInfo.VechicleId;
            }

            if ((vInfo.Vechicle_No != null) && (vInfo.Vechicle_No != ""))
            {
                strcond += " and  c.Vechicle_No like '%" + vInfo.Vechicle_No + "%'";
            }
            if ((vInfo.Vechicle_Lookup != null) && (vInfo.Vechicle_Lookup != ""))
            {
                strcond += " and  c.Vechicle_Lookup like '%" + vInfo.Vechicle_Lookup + "%'";
            }

            if ((vInfo.Vechicle_Band != null) && (vInfo.Vechicle_Band != ""))
            {
                strcond += " and   c.Vechicle_Band  like '%" + vInfo.Vechicle_Band + "%'";
            }
            if ((vInfo.Vechicle_Band != null) && (vInfo.Vechicle_Band != ""))
            {
                strcond += " and   c.Vechicle_Band  like '%" + vInfo.Vechicle_Band + "%'";
            }
            if ((vInfo.Vechicle_Active != null) && (vInfo.Vechicle_Active != ""))
            {
                strcond += " and   c.active  like '%" + vInfo.Vechicle_Active + "%'";
            }
            DataTable dt = new DataTable();
            var LCampaign = new List<VechicleListReturn>();
        
            try
            {
                string strsql = "  select c.*,c.Vechicle_Band as car_band,ltype.LookupValue as CAR_TYPE from Vechicle c  " +
                                
                                  "inner join lookup ltype on c.Vechicle_Lookup = ltype.lookupcode and ltype.LookupType = 'CAR_TYPE' " +

                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + vInfo.rowOFFSet + " ROWS FETCH NEXT " + vInfo.rowFetch + " ROWS ONLY";

                
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
                                 Vechicle_Model = dr["Vechicle_Model"].ToString().Trim(),
                                 Active = dr["Active"].ToString().Trim(),
                                 Vechicle_Lookup = dr["Vechicle_Lookup"].ToString().Trim(),
                                 Name_TypeCar = dr["CAR_TYPE"].ToString().Trim(),
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

            return LCampaign;
        }


        //the old name (ListCustomerByCriteria_showgv) is already used in CustomerDAO 
        public List<VechicleListReturn> ListVechicleByCriteriaNoPaging(VechicleInfo vInfo)
        {
            string strcond = "";

            if ((vInfo.VechicleId != null) && (vInfo.VechicleId != 0))
            {
                strcond += " and  c.Id =" + vInfo.VechicleId;
            }

            if ((vInfo.Vechicle_No != null) && (vInfo.Vechicle_No != ""))
            {
                strcond += " and  c.Vechicle_No like '%" + vInfo.Vechicle_No + "%'";
            }
            if ((vInfo.Vechicle_Lookup != null) && (vInfo.Vechicle_Lookup != ""))
            {
                strcond += " and  c.Vechicle_Lookup like '%" + vInfo.Vechicle_Lookup + "%'";
            }

            if ((vInfo.Vechicle_Band != null) && (vInfo.Vechicle_Band != ""))
            {
                strcond += " and  c.Vechicle_Band like '%" + vInfo.Vechicle_Band + "%'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<VechicleListReturn>();

            try
            {
                string strsql = "  select c.*,Vechicle_Band as car_band,ltype.LookupValue as CAR_TYPE from Vechicle c  " +
                                
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
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }




        public List<VechicleListReturn> ListVechicleByCriteria_ddl(VechicleInfo vInfo)
        {
            string strcond = "";

            if ((vInfo.VechicleId != null) && (vInfo.VechicleId != 0))
            {
                strcond += " and  c.Id =" + vInfo.VechicleId;
            }

            if ((vInfo.Vechicle_No != null) && (vInfo.Vechicle_No != ""))
            {
                strcond += " and  c.Vechicle_No like '%" + vInfo.Vechicle_No + "%'";
            }
            if ((vInfo.Vechicle_Lookup != null) && (vInfo.Vechicle_Lookup != ""))
            {
                strcond += " and  c.Vechicle_Lookup like '%" + vInfo.Vechicle_Lookup + "%'";
            }

            if ((vInfo.Vechicle_Band != null) && (vInfo.Vechicle_Band != ""))
            {
                strcond += " and  c.Vechicle_Band like '%" + vInfo.Vechicle_Band + "%'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<VechicleListReturn>();

            try
            {
                string strsql = "  select c.*,Vechicle_Band as car_band,ltype.LookupValue as CAR_TYPE from Vechicle c  " +
                            
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
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        //the old name (CountCustomerListByCriteria) is already used in CustomerDAO 
        public int? CountVechicleListByCriteria(VechicleInfo vInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((vInfo.VechicleId != null) && (vInfo.VechicleId != 0))
            {
                strcond += " and  c.Id =" + vInfo.VechicleId;
            }

            if ((vInfo.Vechicle_No != null) && (vInfo.Vechicle_No != ""))
            {
                strcond += " and  c.Vechicle_No like '%" + vInfo.Vechicle_No + "%'";
            }
            if ((vInfo.Vechicle_Lookup != null) && (vInfo.Vechicle_Lookup != ""))
            {
                strcond += " and  c.Vechicle_Lookup like '%" + vInfo.Vechicle_Lookup + "%'";
            }

            if ((vInfo.Vechicle_Band != null) && (vInfo.Vechicle_Band != ""))
            {
                strcond += " and  c.Vechicle_Band like '%" + vInfo.Vechicle_Band + "%'";
            }

            DataTable dt = new DataTable();
            var LVehicle = new List<VechicleListReturn>();


            try
            {
                string strsql = "select count(c.Id) as countVechicle from " + dbName + ".dbo.Vechicle c " +

                           " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LVehicle = (from DataRow dr in dt.Rows

                             select new VechicleListReturn()
                             {
                                 countVechicle = Convert.ToInt32(dr["countVechicle"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LVehicle.Count > 0)
            {
                count = LVehicle[0].countVechicle;
            }

            return count;
        }

        //the old name (InsertCustomer) is already used in CustomerDAO 
        public int InsertVechicle(VechicleInfo vInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.Vechicle  (Vechicle_No,Vechicle_Lookup,Vechicle_Band,Vechicle_Model,Active,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + vInfo.Vechicle_No + "'," +
                           "'" + vInfo.Vechicle_Lookup + "'," +
                           "'" + vInfo.Vechicle_Band + "'," +
                            "'" + vInfo.Vechicle_Model + "'," +
                             "'" + vInfo.Active + "'," +
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
        public int? UpdateVechicle(VechicleInfo vInfo)
        {
            int? i = 0;

            string strsql = " Update " + dbName + ".dbo.Vechicle set " +
                            " Vechicle_No = '" + vInfo.Vechicle_No + "'," +
                            " Vechicle_Lookup = '" + vInfo.Vechicle_Lookup + "'," +
                            " Vechicle_Band = '" + vInfo.Vechicle_Band + "'," +
                               " Vechicle_Model = '" + vInfo.Vechicle_Model + "'," +
                            " Active = '" + vInfo.Active + "'," +
                             " UpdateBy = '" + vInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'";
            
            strsql +=  " where id ='" + vInfo.VechicleId + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        //the old name (DeleteCustomer) is already used in CustomerDAO 
        public int DeleteVechicle(VechicleInfo vInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Vechicle set FlagDelete = 'Y' where Id in (" + vInfo.Vechicle_No + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        //the old name (CustomerCheck) is already used in CustomerDAO 
        public List<VechicleListReturn> VechicleCheck(VechicleInfo vInfo)
        {
            string strcond = "";

            if ((vInfo.Vechicle_No != null) && (vInfo.Vechicle_No != ""))
            {
                strcond += " and  c.Vechicle_No ='" + vInfo.Vechicle_No + "'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<VechicleListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Vechicle c  " +

                               " where c.FlagDelete ='N' " + strcond;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new VechicleListReturn()
                             {
                                 Vechicle_No = dr["Vechicle_No"].ToString().Trim(),
                                 

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        //cannot find any related material between params and the old name (ListCustomerByCriteria_DDl)
        //the name has changed
        public List<VechicleListReturn> ListVechicleByCriteria_DDl(VechicleInfo vInfo)
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
                string strsql = "  select c.*,c.Vechicle_Band as car_band,ltype.LookupValue as CAR_TYPE from " + dbName + ".dbo.Vechicle c  " +
                               //  "inner join lookup lb on c.vechicle_band = lb.lookupcode and lb.LookupType = 'CAR_BAND' " +
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
    }
}
