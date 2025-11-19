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
    public class MerchantDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();
        public List<MerchantListReturn> MerchantListNoPagingCriteria(MerchantInfo mInfo)
        {
            string strcond = "";

            if ((mInfo.MerchantId != null) && (mInfo.MerchantId != 0))
            {
                strcond += " and  m.Id =" + mInfo.MerchantId;
            }

            if ((mInfo.MerchantCode != null) && (mInfo.MerchantCode != ""))
            {
                strcond += " and  m.MerchantCode like '%" + mInfo.MerchantCode + "%'";
            }
            if ((mInfo.MerchantName != null) && (mInfo.MerchantName != ""))
            {
                strcond += " and  m.MerchantName like '%" + mInfo.MerchantName + "%'";
            }

            DataTable dt = new DataTable();
            var LMerchant = new List<MerchantListReturn>();

            try
            {
                string strsql = " select m.* from " + dbName + ".dbo.Merchant m " +
                                    " where m.FlagDelete ='N' and m.ActiveFlag ='Y' " + strcond;

                strsql += " ORDER BY m.Id DESC ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMerchant = (from DataRow dr in dt.Rows

                             select new MerchantListReturn()
                             {
                                 MerchantId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                 MerchantName = dr["MerchantName"].ToString().Trim(),
                                 MerchantType = dr["MerchantType"].ToString().Trim(),
                                 CompanyCode = dr["CompanyCode"].ToString().Trim(),
                                 TaxId = dr["TaxId"].ToString().Trim(),
                                 Address = dr["Address"].ToString().Trim(),
                                 SubDistrictCode = dr["SubDistrictCode"].ToString().Trim(),
                                 DistrictCode = dr["DistrictCode"].ToString().Trim(),
                                 ProvinceCode = dr["ProvinceCode"].ToString().Trim(),
                                 ZipCode = dr["ZipCode"].ToString().Trim(),
                                 ContactTel = dr["ContactTel"].ToString().Trim(),
                                 FaxNum = dr["FaxNum"].ToString().Trim(),
                                 Email = dr["Email"].ToString().Trim(),
                                 CreateDate = dr["CreateDate"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString().Trim(),
                                 UpdateDate = dr["UpdateDate"].ToString().Trim(),
                                 UpdateBy = dr["UpdateBy"].ToString().Trim(),
                                 FlagDelete = dr["FlagDelete"].ToString().Trim(),
                                 ActiveFlag = dr["ActiveFlag"].ToString().Trim()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMerchant;
        }
        public List<MerchantListReturn> MerchantListPagingCriteria(MerchantInfo mInfo)
        
        
        {

            string strcond = "";

            if ((mInfo.MerchantId != null) && (mInfo.MerchantId != 0))
            {
                strcond += " and  m.Id =" + mInfo.MerchantId;
            }

            if ((mInfo.MerchantCode != null) && (mInfo.MerchantCode != ""))
            {
                strcond += " and  m.MerchantCode like '%" + mInfo.MerchantCode.Trim() + "%'";
            }
            if ((mInfo.MerchantName != null) && (mInfo.MerchantName != ""))
            {
                strcond += " and  m.MerchantName like '%" + mInfo.MerchantName.Trim() + "%'";
            }
            if ((mInfo.ActiveFlag != null) && (mInfo.ActiveFlag != ""))
            {
                strcond += " and  m.ActiveFlag = '" + mInfo.ActiveFlag.Trim() + "'";
            }


            DataTable dt = new DataTable();
            var LMerchant = new List<MerchantListReturn>();

            try
            {
                string strsql = " select m.* from " + dbName + ".dbo.Merchant m " +
                                    " where m.FlagDelete ='N' and m.ActiveFlag ='Y' " + strcond;
                if (mInfo.rowFetch == 0 && mInfo.rowOFFSet == 0)
                {
                    strsql += " ORDER BY m.Id DESC  ";
                }
                else 
                {
                    strsql += " ORDER BY m.Id DESC OFFSET " + mInfo.rowOFFSet + " ROWS FETCH NEXT " + mInfo.rowFetch + " ROWS ONLY";
                }
               

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMerchant = (from DataRow dr in dt.Rows

                             select new MerchantListReturn()
                             {
                                 MerchantId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                 MerchantName = dr["MerchantName"].ToString().Trim(),
                                 MerchantType = dr["MerchantType"].ToString().Trim(),
                                 CompanyCode = dr["CompanyCode"].ToString().Trim(),
                                 TaxId = dr["TaxId"].ToString().Trim(),
                                 Address = dr["Address"].ToString().Trim(),
                                 SubDistrictCode = dr["SubDistrictCode"].ToString().Trim(),
                                 DistrictCode = dr["DistrictCode"].ToString().Trim(),
                                 ProvinceCode = dr["ProvinceCode"].ToString().Trim(),
                                 ZipCode = dr["ZipCode"].ToString().Trim(),
                                 ContactTel = dr["ContactTel"].ToString().Trim(),
                                 FaxNum = dr["FaxNum"].ToString().Trim(),
                                 Email = dr["Email"].ToString().Trim(),
                                 PictureMerchantURL = dr["PictureMerchantURL"].ToString().Trim(),
                                 CreateDate = dr["CreateDate"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString().Trim(),
                                 UpdateDate = dr["UpdateDate"].ToString().Trim(),
                                 UpdateBy = dr["UpdateBy"].ToString().Trim(),
                                 FlagDelete = dr["FlagDelete"].ToString().Trim(),
                                 ActiveFlag = dr["ActiveFlag"].ToString().Trim()

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMerchant;
        }

        public List<MerchantListReturn> GetMerchantType(MerchantInfo mInfo)
        {
            string strcond = "";

            if ((mInfo.MerchantId != null) && (mInfo.MerchantId != 0))
            {
                strcond += " and  m.Id =" + mInfo.MerchantId;
            }

            if ((mInfo.MerchantCode != null) && (mInfo.MerchantCode != ""))
            {
                strcond += " and  m.MerchantCode like '%" + mInfo.MerchantCode.Trim() + "%'";
            }
            if ((mInfo.MerchantName != null) && (mInfo.MerchantName != ""))
            {
                strcond += " and  m.MerchantName like '%" + mInfo.MerchantName.Trim() + "%'";
            }
            if ((mInfo.ActiveFlag != null) && (mInfo.ActiveFlag != ""))
            {
                strcond += " and  m.ActiveFlag = '" + mInfo.ActiveFlag.Trim() + "'";
            }


            DataTable dt = new DataTable();
            var LMerchant = new List<MerchantListReturn>();


            try
            {
                string strsql = " select distinct m.MerchantType from " + dbName + ".dbo.Merchant AS m " + strcond;

                //strsql += " ORDER BY m.Id DESC ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMerchant = (from DataRow dr in dt.Rows

                             select new MerchantListReturn()
                             {
                                 MerchantType = dr["MerchantType"].ToString().Trim(),
                             }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMerchant;


         }

        public int? CountMerchantListByCriteria(MerchantInfo mInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((mInfo.MerchantId != null) && (mInfo.MerchantId != 0))
            {
                strcond += " and  m.Id =" + mInfo.MerchantId;
            }
            if ((mInfo.MerchantCode != null) && (mInfo.MerchantCode != ""))
            {
                strcond += " and  m.MerchantCode like '%" + mInfo.MerchantCode + "%'";
            }
            if ((mInfo.MerchantName != null) && (mInfo.MerchantName != ""))
            {
                strcond += " and m.MerchantName like '%" + mInfo.MerchantName + "%'";
            }
            if ((mInfo.ActiveFlag != null) && (mInfo.ActiveFlag != ""))
            {
                strcond += " and m.ActiveFlag = '" + mInfo.ActiveFlag + "'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<MerchantListReturn>();


            try
            {
                string strsql = "select count(m.Id) as countMerchant from " + dbName + ".dbo.Merchant m " +

                           " where m.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new MerchantListReturn()
                              {
                                  countMerchant = Convert.ToInt32(dr["countMerchant"])
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LInventory.Count > 0)
            {
                count = LInventory[0].countMerchant;
            }

            return count;
        }
        public int UpdateMerchant(MerchantInfo mInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Merchant set " +

                            " MerchantCode = '" + mInfo.MerchantCode + "'," +
                            " MerchantName = '" + mInfo.MerchantName + "'," +
                            " MerchantType = '" + mInfo.MerchantType + "'," +
                            " CompanyCode = '" + mInfo.CompanyCode + "'," +
                            " TaxId = '" + mInfo.TaxId + "'," +
                            " Address = '" + mInfo.Address + "'," +
                            " SubDistrictCode = '" + mInfo.SubDistrictCode + "'," +
                            " DistrictCode = '" + mInfo.DistrictCode + "'," +
                            " ProvinceCode = '" + mInfo.ProvinceCode + "'," +
                            " ZipCode = '" + mInfo.ZipCode + "'," +
                            " ContactTel = '" + mInfo.ContactTel + "'," +
                            " FaxNum = '" + mInfo.FaxNum + "'," +
                            " Email = '" + mInfo.Email + "'," +
                            " PictureMerchantURL = '" + mInfo.PictureMerchantURL + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy = '" + mInfo.UpdateBy + "'" +

                            " where Id = '" + mInfo.MerchantId + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }



        public List<MerchantListReturn> ListMerchantValidatePagingByCriteria(MerchantInfo mInfo)
        {
            string strcond = "";

            if ((mInfo.MerchantCode != null) && (mInfo.MerchantCode != ""))
            {
                strcond = " and m.MerchantCode like '" + mInfo.MerchantCode.Trim() + "'";
            }
            if ((mInfo.MerchantName != null) && (mInfo.MerchantName != ""))
            {
                strcond = " and m.MerchantName like '%" + mInfo.MerchantName.Trim() + "%'";
            }

            DataTable dt = new DataTable();
            var LMerchant = new List<MerchantListReturn>();

            try
            {
                string strsql = "select m.*" +
                                " from " + dbName + ".dbo.Merchant m where m.FlagDelete = 'N' " +
                                strcond;



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMerchant = (from DataRow dr in dt.Rows

                              select new MerchantListReturn()
                              {
                                  MerchantId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  MerchantCode = dr["MerchantCode"].ToString(),
                                  MerchantName = dr["MerchantName"].ToString().Trim(),
                                  FlagDelete = dr["FlagDelete"].ToString().Trim()
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMerchant;
        }

        public int InsertMerchantMapping(MerchantInfo mInfo)
        {
            int i = 0;

            string strsql = "insert into MerchantMapping (UserName, MerchantCode, CreateDate, CreateBy, UpdateDate, UpdateBy, " +
                "FlagDelete) values ( " +
                " '" + mInfo.Username + "'," +
                " '" + mInfo.MerchantCode + "'," +
                " '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                " '" + mInfo.CreateBy + "'," +
                " '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                " '" + mInfo.UpdateBy + "'," +
                " '" + mInfo.FlagDelete + "')";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int InsertMerchantImage(MerchantImageInfo mInfo)
        {
            int i = 0;
            string strsql = "insert into MerchantImage (MerchantCode, MerchantImageUrl, FlagDelete, Active, CreateDate, CreateBy, UpdateDate, UpdateBy) values (" +
                             "'" + mInfo.MerchantCode + "', " +
                             "'" + mInfo.MerchantImageUrl + "', " +
                             "'" + mInfo.FlagDelete + "', " +
                             "'" + mInfo.Active + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                             "'" + mInfo.CreateBy + "', " +
                             "'" + mInfo.UpdateDate + "', " +
                             "'" + mInfo.UpdateBy + "')"; 


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);
            return i;
        }

            public int InsertMerchant(MerchantInfo mInfo)
        {
            int i = 0;

            string strsql = "insert into Merchant (MerchantCode, MerchantName, MerchantType, CompanyCode, TaxId, Address, SubDistrictCode, DistrictCode, ProvinceCode, ZipCode, ContactTel, FaxNum, Email, PictureMerchantURL, CreateDate, CreateBy, FlagDelete, ActiveFlag) values (" +
                             "'" + mInfo.MerchantCode + "', " +
                             "'" + mInfo.MerchantName + "', " +
                             "'" + mInfo.MerchantType + "', " +
                             "'" + mInfo.CompanyCode + "', " +
                             "'" + mInfo.TaxId + "', " +
                             "'" + mInfo.Address + "', " +
                             "'" + mInfo.SubDistrictCode + "', " +
                             "'" + mInfo.DistrictCode + "', " +
                             "'" + mInfo.ProvinceCode + "', " +
                             "'" + mInfo.ZipCode + "', " +
                             "'" + mInfo.ContactTel + "', " +
                             "'" + mInfo.FaxNum + "', " +
                             "'" + mInfo.Email + "', " +
                             "'" + mInfo.PictureMerchantURL + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                             "'" + mInfo.CreateBy + "', " +
                             "'" + mInfo.FlagDelete + "', " +
                             "'" + mInfo.ActiveFlag + "')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteMerchant(MerchantInfo mInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Merchant set " +

                         " ActiveFlag = 'N', FlagDelete= 'Y' " +

                         " where Id in(" + mInfo.MerchantCode + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteMapMerchant(MerchantInfo mInfo)
        {
            int i = 0;

            string strsql = " Delete " + dbName + ".dbo.MerchantMapping " +

                         

                         " where Id in(" + mInfo.MerchantIdList + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<MerchantListReturn> GetNearestMerchantNopaging(CustomerInfo caInfo, CompanyInfo mInfo)
        {
           

           

            DataTable dt = new DataTable();
            var LMerchant = new List<MerchantListReturn>();

            try
            {
                string strsql = "select m.MerchantCode,m.MerchantName,m.Lat,m.Long	from " + dbName + ".dbo.CustomerAddressDetail c " +
                                    "inner join RiderManagement rd on rd.DistrictCode =c.District " +
                                    "inner join Merchant m on c.district = m.DistrictCode " +
                                   
                                    "where c.CustomerCode = '" + caInfo.CustomerCode + "' and c.addresstype='01' AND m.FlagDelete ='N' and m.CompanyCode = '" + mInfo.CompanyCode + "'";
                                    
                strsql += " ORDER BY c.UpdateDate desc";

                


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMerchant = (from DataRow dr in dt.Rows

                             select new MerchantListReturn()
                             {
                               
                                 MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                 MerchantName = dr["MerchantName"].ToString().Trim(),
                                 
                                 Lat = (dr["Lat"].ToString().Trim() != "") ? Convert.ToDouble(dr["Lat"]) : 0,
                                 Long = (dr["Long"].ToString().Trim() != "") ? Convert.ToDouble(dr["Long"]) : 0,

                                 
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMerchant;
        }

        public List<MerchantListReturn> GetNearestMerchant(CustomerInfo caInfo, CompanyInfo mInfo)
        {
            



            DataTable dt = new DataTable();
            var LMerchant = new List<MerchantListReturn>();

            try
            {
                string strsql = "select m.MerchantCode,m.MerchantName,m.Lat,m.Long	from " + dbName + ".dbo.CustomerAddressDetail c " +
                                   "inner join RiderManagement rd on rd.DistrictCode =c.District " +
                                   "inner join Merchant m on c.subdistrict = m.SubDistrictCode " +
                                   
                                   "where c.CustomerCode = '" + caInfo.CustomerCode + "' and c.addresstype='01' and m.FlagDelete ='N' and m.CompanyCode = '" + mInfo.CompanyCode + "'";

                strsql += " ORDER BY c.UpdateDate desc OFFSET " + caInfo.rowOFFSet + " ROWS FETCH NEXT " + caInfo.rowFetch + " ROWS ONLY";

                


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMerchant = (from DataRow dr in dt.Rows

                             select new MerchantListReturn()
                             {
                                 
                                 MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                 MerchantName = dr["MerchantName"].ToString().Trim(),
                                
                                 Lat = (dr["Lat"].ToString().Trim() != "") ? Convert.ToDouble(dr["Lat"]) : 0,
                                 Long = (dr["Long"].ToString().Trim() != "") ? Convert.ToDouble(dr["Long"]) : 0,

                                 
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMerchant;
        }

        public List<MerchantListReturn> ListMerchantEnterpriseForAuth(MerchantInfo mInfo)
        {
            string strcond = "";

            

            if ((mInfo.Username != null) && (mInfo.Username != ""))
            {
                strcond += " and  m.UserName = '" + mInfo.Username + "'";
            }
            else
            {
                strcond += " and  m.UserName = '" + mInfo.Username + "'";
            }

            if ((mInfo.MerchantCode != null) && (mInfo.MerchantCode != ""))
            {
                strcond += " and  m.MerchantCode = '" + mInfo.MerchantCode + "'";
            }

            DataTable dt = new DataTable();
            var LMerchant = new List<MerchantListReturn>();

            try
            {
                string strsql = " select m.*, me.MerchantCode, me.MerchantName from " + dbName + ".dbo.MerchantMapping m " +
                                " left join " + dbName + ".dbo.Merchant AS me on me.MerchantCode = m.MerchantCode" +
                                " where m.FlagDelete ='N' and me.FlagDelete='N' and me.ActiveFlag='Y' " + strcond;

                strsql += " ORDER BY m.Id ASC ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMerchant = (from DataRow dr in dt.Rows

                             select new MerchantListReturn()
                             {
                                 MerchantId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                 MerchantName = dr["MerchantName"].ToString().Trim(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMerchant;
            
        }

        public List<MerchantListReturn> ListMerchantEnterprise(MerchantInfo mInfo)
        {
            string strcond = "";
            string strInnerjoin = "";


            if ((mInfo.Username != null) && (mInfo.Username != ""))
            {
                strInnerjoin += " inner join MerchantMapping as mp on m.MerchantCode = mp.MerchantCode ";
                strcond += " and  mp.UserName = '" + mInfo.Username + "'";
            }
            

            DataTable dt = new DataTable();
            var LMerchant = new List<MerchantListReturn>();

            try
            {
                string strsql = " select m.* from " + dbName + ".dbo.Merchant m " + strInnerjoin +
                                " where m.FlagDelete ='N' and m.CompanyCode = 'ENTERPRISE' and m.ActiveFlag = 'Y'" + strcond;
                
                strsql += " ORDER BY m.Id ASC ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMerchant = (from DataRow dr in dt.Rows

                             select new MerchantListReturn()
                             {
                                 MerchantId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                 MerchantName = dr["MerchantName"].ToString().Trim(),
                                 
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMerchant;
        }

        
       
        public List<MerchantListReturn> ListMerchantMappingUserName(MerchantInfo mInfo)
        {
            string strcond = "";

            

            if ((mInfo.Username != null) && (mInfo.Username != ""))
            {
                strcond += " and  m.UserName = '" + mInfo.Username + "'";
            }
            if ((mInfo.MerchantCode != null) && (mInfo.MerchantCode != ""))
            {
                strcond += " and  m.MerchantCode = '" + mInfo.MerchantCode + "'";
            }

            DataTable dt = new DataTable();
            var LMerchant = new List<MerchantListReturn>();

            try
            {
                string strsql = " select m.*, mc.MerchantName from " + dbName + ".dbo.MerchantMapping m " +
                                " left join Merchant mc on mc.MerchantCode = m.MerchantCode and mc.FlagDelete = 'N' " +
                                " where m.FlagDelete ='N'" + strcond;

                strsql += " ORDER BY m.Id ASC ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMerchant = (from DataRow dr in dt.Rows

                             select new MerchantListReturn()
                             {
                                 MerchantId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                 MerchantName = dr["MerchantName"].ToString().Trim(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMerchant;
        }

        public List<MerchantListReturn> ListMerchantAddress(MerchantInfo mInfo)
        {
            string strcond = "";
         
            if ((mInfo.MerchantCode != null) && (mInfo.MerchantCode != ""))
            {
                strcond += " and  m.MerchantCode = '" + mInfo.MerchantCode + "'";
            }

            DataTable dt = new DataTable();
            var LMerchant = new List<MerchantListReturn>();

            try
            {
                string strsql = " select m.id,m.MerchantName,m.MerchantCode,m.ContactTel, m.Address,p.ProvinceName," +
                    " d.DistrictName,sd.SubDistrictName,m.ZipCode" +
                    " " +
                    " from " + dbName + ".dbo.Merchant m  " +
                                " inner join Province p on p.Province_ID = m.ProvinceCode " +
                                " inner join District d on m.DistrictCode = d.DistrictCode" +
                                " inner join SubDistrict sd on m.SubDistrictCode =sd.SubDistrictCode " +
                                " where m.FlagDelete ='N'" + strcond;

                strsql += " ORDER BY m.Id ASC ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMerchant = (from DataRow dr in dt.Rows

                             select new MerchantListReturn()
                             {
                                 MerchantId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                 MerchantName = dr["MerchantName"].ToString().Trim(),
                                 ProvinceName = dr["ProvinceName"].ToString().Trim(),
                                 SubDistictName = dr["SubDistrictName"].ToString().Trim(),
                                 DistictName = dr["DistrictName"].ToString().Trim(),
                                 Address = dr["Address"].ToString().Trim(),
                                 ContactTel = dr["ContactTel"].ToString().Trim(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMerchant;
        }
    }
}
