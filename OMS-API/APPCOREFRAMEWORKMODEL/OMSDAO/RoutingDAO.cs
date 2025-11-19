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
    public class RoutingDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();



        public List<RoutingListReturn> ListRoutingNoPagingByCriteria(RoutingInfo vInfo)
        {
            string strcond = "";

            if ((vInfo.Routing_code != null) && (vInfo.Routing_code != ""))
            {
                strcond += " and Routing_code like '%" + vInfo.Routing_code + "%'";
            }

            if ((vInfo.Routing_name != null) && (vInfo.Routing_name != ""))
            {
                strcond += " and Routing_name like '%" + vInfo.Routing_name + "%'";
            }


            DataTable dt = new DataTable();
            var LRouting = new List<RoutingListReturn>();

            try
            {
                string strsql = " select * from Routing" +
                                " where FlagDelete ='N' " + strcond;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                            select new RoutingListReturn()
                            {
                                RoutingId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                Routing_code = dr["Routing_code"].ToString().Trim(),
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

        public List<RoutingListReturn> ListRoutingByCriteria_showgv(RoutingInfo vInfo)
        {
            string strcond = "";

            if ((vInfo.Routing_code != null) && (vInfo.Routing_code != ""))
            {
                strcond += " and Routing_code like '%" + vInfo.Routing_code + "%'";
            }

            if ((vInfo.Routing_name != null) && (vInfo.Routing_name != ""))
            {
                strcond += " and Routing_name like '%" + vInfo.Routing_name + "%'";
            }


            DataTable dt = new DataTable();
            var LRouting = new List<RoutingListReturn>();

            try
            {
                string strsql = " select * from Routing" +
                                " where FlagDelete ='N' " + strcond;

                strsql += " ORDER BY Id DESC OFFSET " + vInfo.rowOFFSet + " ROWS FETCH NEXT " + vInfo.rowFetch + " ROWS ONLY";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                             select new RoutingListReturn()
                             {
                                 RoutingId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 Routing_code = dr["Routing_code"].ToString().Trim(),
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

        //the old name (CountCustomerListByCriteria) is already used in CustomerDAO 
        public int? CountRoutingListByCriteria(RoutingInfo vInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((vInfo.Routing_code != null) && (vInfo.Routing_code != ""))
            {
                strcond += " and Routing_code ='" + vInfo.Routing_code + "'";
            }

            if ((vInfo.Routing_name != null) && (vInfo.Routing_name != ""))
            {
                strcond += " and Routing_name ='" + vInfo.Routing_name + "'";
            }

            DataTable dt = new DataTable();
            var LRouting = new List<RoutingListReturn>();


            try
            {
                string strsql = "select count(Id) as countRouting from " + dbName + ".dbo.Routing" +

                           " where FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                             select new RoutingListReturn()
                             {
                                 countRouting = Convert.ToInt32(dr["countRouting"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LRouting.Count > 0)
            {
                count = LRouting[0].countRouting;
            }

            return count;
        }

        //the old name (InsertCustomer) is already used in CustomerDAO 
        public int InsertRouting(RoutingInfo vInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.Routing  (Routing_name,Routing_code,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + vInfo.Routing_name + "'," +
                           "'" + vInfo.Routing_code + "'," +
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
        public int? UpdateRouting(RoutingInfo vInfo)
        {
            int? i = 0;

            string strsql = " Update " + dbName + ".dbo.Routing set " +
                            " Routing_code = '" + vInfo.Routing_code + "'," +
                            " Routing_name = '" + vInfo.Routing_name + "'," +
                            " UpdateBy = '" + vInfo.UpdateBy + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'"+
                            " where id ='" + vInfo.RoutingId + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        //the old name (DeleteCustomer) is already used in CustomerDAO 
        public int DeleteRouting(RoutingInfo vInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Routing set FlagDelete = 'Y' where Id in (" + vInfo.Routing_code + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        //the old name (CustomerCheck) is already used in CustomerDAO 
        public List<RoutingListReturn> RoutingCheck(RoutingInfo vInfo)
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
        public List<VechicleListReturn> ListRoutingByCriteria_DDl(VechicleInfo vInfo)
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

        public List<RoutingDetailListReturn> ListRoutingDetailByCriteria_showgv(RoutingDetailInfo vInfo)
        {
            string strcond = "";

            if ((vInfo.Routing_code != null) && (vInfo.Routing_code != ""))
            {
                strcond += " and r.Routing_code = '" + vInfo.Routing_code + "'";
            }
            if ((vInfo.ProvinceCode != null) && (vInfo.ProvinceCode != ""))
            {
                strcond += " and p.ProvinceName like '%" + vInfo.ProvinceCode + "%'";
            }
            if ((vInfo.DistrictCode != null) && (vInfo.DistrictCode != ""))
            {
                strcond += " and d.DistrictName like '%" + vInfo.DistrictCode + "%'";
            }
            if ((vInfo.SubDistrictCode != null) && (vInfo.SubDistrictCode != ""))
            {
                strcond += " and sd.SubDistrictName like '%" + vInfo.SubDistrictCode + "%'";
            }
            if ((vInfo.PostCode != null) && (vInfo.PostCode != ""))
            {
                strcond += " and r.PostCode like '%" + vInfo.PostCode + "%'";
            }


            DataTable dt = new DataTable();
            var LRouting = new List<RoutingDetailListReturn>();

            try
            {
                string strsql = " select p.ProvinceName,d.DistrictName,sd.SubDistrictName,r.* from RoutingManagement as r" +
                                " left join Province as p on p.ProvinceCode = r.ProvinceCode" +
                                " left join District as d on d.DistrictCode = r.DistrictCode" +
                                " left join SubDistrict as sd on sd.SubDistrictCode = r.SubDistrictCode" +
                                " where r.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY r.Id DESC OFFSET " + vInfo.rowOFFSet + " ROWS FETCH NEXT " + vInfo.rowFetch + " ROWS ONLY";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                            select new RoutingDetailListReturn()
                            {
                                RoutingDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ProvinceName = dr["ProvinceName"].ToString().Trim(),
                                DistrictName = dr["DistrictName"].ToString().Trim(),
                                SubDistrictName = dr["SubDistrictName"].ToString().Trim(),
                                ProvinceCode = dr["ProvinceCode"].ToString().Trim(),
                                DistrictCode = dr["DistrictCode"].ToString().Trim(),
                                SubDistrictCode = dr["SubDistrictCode"].ToString().Trim(),
                                PostCode = dr["PostCode"].ToString().Trim(),  
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

        //the old name (CountCustomerListByCriteria) is already used in CustomerDAO 
        public int? CountRoutingDetailListByCriteria(RoutingDetailInfo vInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((vInfo.Routing_code != null) && (vInfo.Routing_code != ""))
            {
                strcond += " and r.Routing_code = '" + vInfo.Routing_code + "'";
            }
            if ((vInfo.ProvinceCode != null) && (vInfo.ProvinceCode != ""))
            {
                strcond += " and p.ProvinceName like '%" + vInfo.ProvinceCode + "%'";
            }
            if ((vInfo.DistrictCode != null) && (vInfo.DistrictCode != ""))
            {
                strcond += " and d.DistrictName like '%" + vInfo.DistrictCode + "%'";
            }
            if ((vInfo.SubDistrictCode != null) && (vInfo.SubDistrictCode != ""))
            {
                strcond += " and sd.SubDistrictName like '%" + vInfo.SubDistrictCode + "%'";
            }
            if ((vInfo.PostCode != null) && (vInfo.PostCode != ""))
            {
                strcond += " and r.PostCode like '%" + vInfo.PostCode + "%'";
            }

            DataTable dt = new DataTable();
            var LRouting = new List<RoutingListReturn>();


            try
            {
                string strsql = "select count(r.Id) as countRouting from " + dbName + ".dbo.RoutingManagement as r" +
                                " left join Province as p on p.ProvinceCode = r.ProvinceCode" +
                                " left join District as d on d.DistrictCode = r.DistrictCode" +
                                " left join SubDistrict as sd on sd.SubDistrictCode = r.SubDistrictCode" +
                                " where r.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                            select new RoutingListReturn()
                            {
                                countRouting = Convert.ToInt32(dr["countRouting"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LRouting.Count > 0)
            {
                count = LRouting[0].countRouting;
            }

            return count;
        }

        //the old name (InsertCustomer) is already used in CustomerDAO 
        public int InsertRoutingDetail(RoutingDetailInfo vInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.RoutingManagement  (Routing_code,ProvinceCode,DistrictCode,SubDistrictCode,PostCode,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + vInfo.Routing_code + "'," +
                           "'" + vInfo.ProvinceCode + "'," +
                           "'" + vInfo.DistrictCode + "'," +
                           "'" + vInfo.SubDistrictCode + "'," +
                           "'" + vInfo.PostCode + "'," +
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
        public int? UpdateRoutingDetail(RoutingDetailInfo vInfo)
        {
            int? i = 0;

            string strsql = " Update " + dbName + ".dbo.RoutingManagement set " +
                            " Routing_code = '" + vInfo.Routing_code + "'," +
                            " ProvinceCode = '" + vInfo.ProvinceCode + "'," +
                            " DistrictCode = '" + vInfo.DistrictCode + "'," +
                            " SubDistrictCode = '" + vInfo.SubDistrictCode + "'," +
                            " PostCode = '" + vInfo.PostCode + "'," +
                            " UpdateBy = '" + vInfo.UpdateBy + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where id ='" + vInfo.RoutingDetailId + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        //the old name (DeleteCustomer) is already used in CustomerDAO 
        public int DeleteRoutingDetail(RoutingDetailInfo vInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.RoutingManagement set FlagDelete = 'Y' where Id in (" + vInfo.Routing_code + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        //the old name (CustomerCheck) is already used in CustomerDAO 
        public List<RoutingDetailListReturn> RoutingDetailCheck(RoutingDetailInfo vInfo)
        {
            string strcond = "";

            if ((vInfo.Routing_code != null) && (vInfo.Routing_code != ""))
            {
                strcond += " and Routing_code ='" + vInfo.Routing_code + "'";
            }
            if ((vInfo.ProvinceCode != null) && (vInfo.ProvinceCode != ""))
            {
                strcond += " and r.ProvinceCode = '" + vInfo.ProvinceCode + "'";
            }

            if ((vInfo.DistrictCode != null) && (vInfo.DistrictCode != ""))
            {
                strcond += " and r.DistrictCode = '" + vInfo.DistrictCode + "'";
            }

            if ((vInfo.SubDistrictCode != null) && (vInfo.SubDistrictCode != ""))
            {
                strcond += " and r.SubDistrictCode = '" + vInfo.SubDistrictCode + "'";
            }

            if ((vInfo.PostCode != null) && (vInfo.PostCode != ""))
            {
                strcond += " and r.PostCode = '" + vInfo.PostCode + "'";
            }

            DataTable dt = new DataTable();
            var LRouting = new List<RoutingDetailListReturn>();

            try
            {
                string strsql = " select p.ProvinceName,d.DistrictName,sd.SubDistrictName,r.* from RoutingManagement as r" +
                                " left join Province as p on p.ProvinceCode = r.ProvinceCode" +
                                " left join District as d on d.DistrictCode = r.DistrictCode" +
                                " left join SubDistrict as sd on sd.SubDistrictCode = r.SubDistrictCode" +
                                " where r.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRouting = (from DataRow dr in dt.Rows

                            select new RoutingDetailListReturn()
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
                strcond += " and i.Inventory_Code like '%" + vInfo.Inventory_Code + "%'";
            }
            if ((vInfo.Routing_code != null) && (vInfo.Routing_code != ""))
            {
                strcond += " and r.Routing_code like '%" + vInfo.Routing_code + "%'";
            }

            DataTable dt = new DataTable();
            var LRouting = new List<RoutingMapInventoryDetailInfo>();

            try
            {
                string strsql = " SELECT i.InventoryCode, i.InventoryName, r.Routing_name,r.Routing_code,rmi.id,rmi.FlagDelete,rmi.CreateDate,rmi.CreateBy,rmi.UpdateBy,rmi.UpdateDate from Inventory as i" +
                                " left join RoutingMapInventory as rmi on rmi.Inventory_Code = i.InventoryCode  " +
                                " inner join Routing as r on r.Routing_code = rmi.Routing_code " +
                                " where  i.FlagDelete = 'N' and rmi.FlagDelete = 'N' " + strcond;

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
    }
}
