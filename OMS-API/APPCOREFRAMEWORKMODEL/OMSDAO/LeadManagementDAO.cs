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
    public class LeadManagementDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int UpdateLeadManagement(LeadManagementInfo cInfo)
        {
            int i = 0;
            
            string strsql = " Update " + dbName + ".dbo.LeadManagement set " +
                           " CustomerCode = '" + cInfo.CustomerCode + "'," +
                           " CustomerPhone = '" + cInfo.CustomerPhone + "'," +
                          
                          
                          " UpdateBy = '" + cInfo.UpdateBy + "'," +
                          " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                          " where Id =" + cInfo.LeadID + "";
            

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteLeadManagement(LeadManagementInfo cInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.LeadManagement set FlagDelete = 'Y' where Id in (" + cInfo.LeadManagementIdDelete + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertLeadManagement(LeadManagementInfo cInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO LeadManagement  (ref_code,lot_name,channel_from,channel_to,merchant_code," +
                "brand_no,media_phone,prefix_th,firstname_th,lastname_th," +
                "full_name_th,mobile_1,mobile_2,mobile_3,mobile_4," +
                "mobile_5,mobile_6,phone_1,phone_2,phone_3," +
                "addr_no,place,addr_subdistrict,addr_subdistrict_id,addr_district," +
                "addr_district_id,addr_province,addr_province_id,addr_zipcode,previous_sale_name," +
                "previous_order_date,previous_order_brand,previous_product,CAMPAIGN_ID" +

                ",CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete,Status)" +
                            "VALUES (" +
                           "'" + cInfo.REF_CODE + "','" + cInfo.LOT_NAME + "',  '" + cInfo.CHANNEL_FROM + "','" + cInfo.CHANNEL_TO + "','" + cInfo.MERCHANT_CODE + "'," +
                          "'" + cInfo.BRAND_NO + "','" + cInfo.MEDIA_PHONE + "',  '" + cInfo.PREFIX_TH + "','" + cInfo.FIRSTNAME_TH + "','" + cInfo.LASTNAME_TH + "'," +
                           "'" + cInfo.FULL_NAME_TH + "','" + cInfo.MOBILE_1 + "',  '" + cInfo.MOBILE_2 + "','" + cInfo.MOBILE_3 + "','" + cInfo.MOBILE_4 + "'," +
                           "'" + cInfo.MOBILE_5 + "','" + cInfo.MOBILE_6 + "',  '" + cInfo.PHONE_1 + "','" + cInfo.PHONE_2 + "','" + cInfo.PHONE_3 + "'," +
                           "'" + cInfo.addr_no + "','" + cInfo.PLACE + "',  '" + cInfo.ADDR_SUBDISTRICT + "','" + cInfo.ADDR_SUBDISTRICT_ID + "','" + cInfo.ADDR_DISTRICT + "'," +
                           "'" + cInfo.ADDR_DISTRICT_ID + "','" + cInfo.ADDR_PROVINCE + "',  '" + cInfo.ADDR_PROVINCE_ID + "','" + cInfo.ADDR_ZIPCODE + "','" + cInfo.PREVIOUS_SALE_NAME + "'," +

                               "'" + cInfo.PREVIOUS_ORDER_DATE + "','" + cInfo.PREVIOUS_ORDER_BRAND + "',  '" + cInfo.PREVIOUS_PRODUCT + "','" + cInfo.CAMPAIGN_ID + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.UpdateBy + "'," +
                           "'N'," +
                           "'"+ cInfo.Status + "' " +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<LeadManagementInfo> ListLeadManagementNoPagingByCriteria(LeadManagementInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.LeadID != null) && (cInfo.LeadID != 0))
            {
                strcond += " and  c.Id =" + cInfo.LeadID;
            }

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode like '%" + cInfo.CustomerCode + "%'";
            }
            if ((cInfo.CustomerPhone != null) && (cInfo.CustomerPhone != ""))
            {
                strcond += " and  c.CustomerPhone like '%" + cInfo.CustomerPhone + "%'";
            }
            if ((cInfo.NoHaveAssignEmpCode != null) && (cInfo.NoHaveAssignEmpCode != ""))
            {
                strcond += " and  (c.AssignEmpCode IS NULL) OR (AssignEmpCode = '') ";
            }
            if ((cInfo.HaveAssignEmpCode != null) && (cInfo.HaveAssignEmpCode != ""))
            {
                strcond += " and  (c.AssignEmpCode <> '') ";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<LeadManagementInfo>();

            try
            {
                string strsql = " select c.*, l.lookupvalue as TransactionTypeName  " +
                    "  ,m.MerchantName" +
                    " from " + dbName + ".dbo.LeadManagement c " +
                                " left join " + dbName + ".dbo.lookup l on l.lookupcode = c.TransactionTypeCode and l.lookuptype = 'TRANSACTIONTYPE' " +
                                "  inner join Merchant m on c.merchant_code =m.MerchantCode" +
                                "" +                              
                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new LeadManagementInfo()
                             {
                                 LeadID = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                 CustomerPhone = dr["CustomerPhone"].ToString().Trim(),
                                 Promotion_Code = dr["Promotion_Code"].ToString().Trim(),
                                 CampaignCode = dr["CampaignCode"].ToString().Trim(),
                                 Email = dr["Email"].ToString().Trim(),
                                 Telephone = dr["Telephone"].ToString().Trim(),
                                 Name = dr["Name"].ToString().Trim(),
                                 Insurancetype = dr["Insurancetype"].ToString().Trim(),
                                 Insurancedate = dr["Insurancedate"].ToString().Trim(),
                                 Caryear = dr["Caryear"].ToString().Trim(),
                                 Cartype = dr["Cartype"].ToString().Trim(),
                                 Carmodel = dr["Carmodel"].ToString().Trim(),
                                 Carsubmodel = dr["Carsubmodel"].ToString().Trim(),
                                 LeadCode = dr["LeadCode"].ToString().Trim(),
                                 Status = dr["Status"].ToString().Trim(),
                                 CallStatus = dr["CallStatus"].ToString().Trim(),
                                 CallSituation = dr["CallSituation"].ToString().Trim(),
                                 Description = dr["Description"].ToString().Trim(),
                                 CusReason = dr["CusReason"].ToString().Trim(),
                                 CusReasonOther = dr["CusReasonOther"].ToString().Trim(),
                                 TransactionTypeCode = dr["TransactionTypeCode"].ToString().Trim(),
                                 TransactionTypeName = dr["TransactionTypeName"].ToString().Trim(),
                                 OrderCode = dr["OrderCode"].ToString().Trim(),
                                 RecontactbackDate = dr["RecontactbackDate"].ToString().Trim(),
                                 RecontactbactPeriodTime = dr["RecontactbactPeriodTime"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 MERCHANT_CODE = dr["merchant_code"].ToString(),
                                 MerchantName = dr["MerchantName"].ToString(),
                                 FIRSTNAME_TH = dr["FIRSTNAME_TH"].ToString(),
                                 LASTNAME_TH = dr["LASTNAME_TH"].ToString(),
                                 MOBILE_1 = dr["MOBILE_1"].ToString(),
                                 MOBILE_2 = dr["MOBILE_2"].ToString(),
                                 MOBILE_3 = dr["MOBILE_3"].ToString(),
                                 MOBILE_4 = dr["MOBILE_4"].ToString(),
                                 MOBILE_5 = dr["MOBILE_5"].ToString(),
                                 MOBILE_6 = dr["MOBILE_6"].ToString(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public List<LeadManagementInfo> ListLeadManagementPagingByCriteria(LeadManagementInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.LeadID != null) && (cInfo.LeadID != 0))
            {
                strcond += " and  c.Id =" + cInfo.LeadID;
            }

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode like '%" + cInfo.CustomerCode + "%'";
            }
            if ((cInfo.CustomerPhone != null) && (cInfo.CustomerPhone != ""))
            {
                strcond += " and  c.CustomerPhone like '%" + cInfo.CustomerPhone + "%'";
            }
            if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            {
                strcond += " and  cm.CustomerFName like '%" + cInfo.CustomerFName + "%'";
            }
            if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
            {
                strcond += " and  cm.CustomerLName like '%" + cInfo.CustomerLName + "%'";
            }
            if ((cInfo.CreateDateFrom != null) && (cInfo.CreateDateFrom != ""))
            {
                strcond += " AND c.CreateDate BETWEEN CONVERT(DATETIME, '" + cInfo.CreateDateFrom + "',103) AND CONVERT(DATETIME,'" + cInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<LeadManagementInfo>();

            try
            {
                string strsql = " select c.*, cm.CustomerFName, cm.CustomerLName from " + dbName + ".dbo.LeadManagement c " +
                                " left join Customer cm on cm.CustomerCode=c.CustomerCode " +
                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new LeadManagementInfo()
                             {
                                 LeadID = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                 CustomerPhone = dr["CustomerPhone"].ToString().Trim(),
                                 CustomerFName = dr["CustomerFName"].ToString().Trim(),
                                 CustomerLName = dr["CustomerLName"].ToString().Trim(),
                                 CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
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

        public int? countLeadListByCriteria(LeadManagementInfo cInfo) {
            string strcond = "";
            int? count = 0;

            if ((cInfo.LeadID != null) && (cInfo.LeadID != 0))
            {
                strcond += " and  c.Id =" + cInfo.LeadID;
            }

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode like '%" + cInfo.CustomerCode + "%'";
            }
            if ((cInfo.CustomerPhone != null) && (cInfo.CustomerPhone != ""))
            {
                strcond += " and  c.CustomerPhone like '%" + cInfo.CustomerPhone + "%'";
            }
            if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            {
                strcond += " and  cm.CustomerFName like '%" + cInfo.CustomerFName + "%'";
            }
            if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
            {
                strcond += " and  cm.CustomerLName like '%" + cInfo.CustomerLName + "%'";
            }
            if ((cInfo.CreateDateFrom != null) && (cInfo.CreateDateFrom != ""))
            {
                strcond += " AND c.CreateDate BETWEEN CONVERT(DATETIME, '" + cInfo.CreateDateFrom + "',103) AND CONVERT(DATETIME,'" + cInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<LeadManagementInfo>();


            try
            {
                string strsql = "select count(c.Id) as countLead from " + dbName + ".dbo.LeadManagement c " +
                                " left join Customer cm on cm.CustomerCode=c.CustomerCode " +
                                " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new LeadManagementInfo()
                             {
                                 countLead = Convert.ToInt32(dr["countLead"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LCampaign.Count > 0)
            {
                count = LCampaign[0].countLead;
            }

            return count;
        }

     
        public List<LeadManagementInfo> CustomerCodeValidateInsert(LeadManagementInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode = '" + cInfo.CustomerCode + "'";
            }
            if ((cInfo.CustomerPhone != null) && (cInfo.CustomerPhone != ""))
            {
                strcond += " and  c.CustomerPhone = '" + cInfo.CustomerPhone + "'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<LeadManagementInfo>();

            try
            {
                string strsql = " select c.CustomerCode from " + dbName + ".dbo.LeadManagement c " +
                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new LeadManagementInfo()
                             {
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public int InsertLeadManagementImport(List<LeadManagementInfo> lcinfo)
        {
            List<String> lSQL = new List<string>();
            string strsql = "";
            int i = 0;

            foreach (var info in lcinfo.ToList())
            {
                strsql = "insert into " + dbName + ".dbo.LeadManagement (CustomerCode,CustomerPhone,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete) values (" +
                             "'" + info.CustomerCode + "', " +
                             "'" + info.CustomerPhone + "', " +
                                                  
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             "'" + info.CreateBy + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             "'" + info.UpdateBy + "', " +
                             "'" + info.FlagDelete + "')";
                lSQL.Add(strsql);
            }

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            foreach (string strq in lSQL)
            {
                com.CommandText = strq;
                com.CommandType = System.Data.CommandType.Text;
                i = db.ExcuteBeginTransectionText(com);
            }
            return i;
        }

        public List<LeadManagementInfo> LeadManagementListNoPagingCriteria(LeadManagementInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.LeadID != null) && (cInfo.LeadID != 0))
            {
                strcond += " and  c.Id =" + cInfo.LeadID;
            }

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode like '%" + cInfo.CustomerCode + "%'";
            }
            if ((cInfo.CustomerPhone != null) && (cInfo.CustomerPhone != ""))
            {
                strcond += " and  c.CustomerPhone like '%" + cInfo.CustomerPhone + "%'";
            }
            if ((cInfo.CampaignCode != null) && (cInfo.CampaignCode != ""))
            {
                strcond += " and  c.CampaignCode like '%" + cInfo.CampaignCode + "%'";
            }
            if ((cInfo.Promotion_Code != null) && (cInfo.Promotion_Code != ""))
            {
                strcond += " and  c.Promotion_Code like '%" + cInfo.Promotion_Code + "%'";
            }
            if ((cInfo.MERCHANT_CODE != null) && (cInfo.MERCHANT_CODE != ""))
            {
                strcond += " and  c.MERCHANT_CODE = '" + cInfo.MERCHANT_CODE + "'";
            }
            DataTable dt = new DataTable();
            var LMerchant = new List<LeadManagementInfo>();

            try
            {
                string strsql = " select c.*, p.ProductCode, p.ProductName, pr.PromotionCode, pr.PromotionName,  cp.CampaignCode, pd.Id AS PromotionDetailInfoId, c.Promotion_Code, pd.Price, " +
                                " u.LookupValue AS UnitName, pr.FreeShipping, pr.ProductDiscountPercent, pr.ProductDiscountAmount, " +
                                " pr.LockCheckbox, pr.LockAmountFlag, pr.DefaultAmount, pr.PromotionTypeCode, pr.MOQFlag, pr.MinimumQty, pr.DiscountPercent, pr.DiscountAMount, pr.GroupPrice from " + dbName + ".dbo.LeadManagement c " +
                                " left join Customer cm on cm.CustomerCode=c.CustomerCode " +
                                " left join Product p on p.Id=c.Productid " +
                                " left join Promotion pr on pr.PromotionCode=c.Promotion_Code " +
                                " left join CampaignPromotion AS cp ON cp.PromotionCode = pr.PromotionCode " +
                                " left join PromotionDetailInfo AS pd ON pd.PromotionCode = c.Promotion_Code AND p.ProductCode = pd.ProductCode " +
                                " left join Lookup AS u ON u.LookupCode = p.Unit AND u.LookupType = 'UNIT' " +
                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMerchant = (from DataRow dr in dt.Rows

                             select new LeadManagementInfo()
                             {
                                 LeadID = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                 CustomerPhone = dr["CustomerPhone"].ToString().Trim(),
                                 ProductCode = dr["ProductCode"].ToString().Trim(),
                                 ProductName = dr["ProductName"].ToString().Trim(),
                                 Promotion_Code = dr["Promotion_Code"].ToString().Trim(),
                                 PromotionName = dr["PromotionName"].ToString().Trim(),
                                 Telephone = dr["Telephone"].ToString().Trim(),
                                 PromotionDetailInfoId = (dr["PromotionDetailInfoId"].ToString() != "") ? Convert.ToInt32(dr["PromotionDetailInfoId"]) : 0,
                                 Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                                 UnitName = dr["UnitName"].ToString().Trim(),
                                 FreeShipping = dr["FreeShipping"].ToString().Trim(),
                                 CampaignCode = dr["CampaignCode"].ToString().Trim(),
                                 ProductDiscountAmount = (dr["ProductDiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["ProductDiscountAmount"]) : 0,
                                 ProductDiscountPercent = (dr["ProductDiscountPercent"].ToString() != "") ? Convert.ToDouble(dr["ProductDiscountPercent"]) : 0,
                                 LockCheckbox = dr["LockCheckbox"].ToString().Trim(),
                                 LockAmountFlag = dr["LockAmountFlag"].ToString().Trim(),
                                 DefaultAmount = (dr["DefaultAmount"].ToString() != "") ? Convert.ToInt32(dr["DefaultAmount"]) : 0,
                                 PromotionTypeCode = dr["PromotionTypeCode"].ToString().Trim(),
                                 MOQFlag = dr["MOQFlag"].ToString().Trim(),
                                 MinimumQty = (dr["MinimumQty"].ToString() != "") ? Convert.ToInt32(dr["MinimumQty"]) : 0,
                                 DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["DiscountAmount"]) : 0,
                                 DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToDouble(dr["DiscountPercent"]) : 0,
                                 GroupPrice = (dr["GroupPrice"].ToString() != "") ? Convert.ToDouble(dr["GroupPrice"]) : 0,
                                 CreateDate = dr["CreateDate"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString().Trim(),
                                 UpdateDate = dr["UpdateDate"].ToString().Trim(),
                                 UpdateBy = dr["UpdateBy"].ToString().Trim(),
                                 FlagDelete = dr["FlagDelete"].ToString().Trim()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMerchant;
        }

        public int? CountAssignLeadManagementListByCriteria(LeadManagementInfo cInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((cInfo.LeadID != null) && (cInfo.LeadID != 0))
            {
                strcond += " and  c.Id =" + cInfo.LeadID;
            }

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode like '%" + cInfo.CustomerCode + "%'";
            }
            if ((cInfo.CustomerPhone != null) && (cInfo.CustomerPhone != ""))
            {
                //strcond += " and  c.CustomerPhone like '%" + cInfo.CustomerPhone + "%'";

                strcond += " and  (c.mobile_1 like '%" + cInfo.CustomerPhone + "%'";
                strcond += " or  c.mobile_2 like '%" + cInfo.CustomerPhone + "%'";
                strcond += " or  c.mobile_3 like '%" + cInfo.CustomerPhone + "%'";
                strcond += " or  c.mobile_4 like '%" + cInfo.CustomerPhone + "%'";
                strcond += " or  c.mobile_5 like '%" + cInfo.CustomerPhone + "%'";
                strcond += " or  c.mobile_6 like '%" + cInfo.CustomerPhone + "%')";
            }
            if ((cInfo.CustomerFName == cInfo.CustomerLName))
            {
                if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
                {
                    strcond += " and  (c.firstname_th like '%" + cInfo.CustomerFName + "%'";
                    strcond += " or  c.lastname_th like '%" + cInfo.CustomerFName + "%')";
                }
            }
            else
            {
                if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
                {
                    strcond += " and  c.firstname_th like '%" + cInfo.CustomerFName + "%'";

                }
                if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
                {
                    strcond += " and  c.lastname_th like '%" + cInfo.CustomerLName + "%'";
                }
            }
            if ((cInfo.Status != null) && (cInfo.Status != ""))
            {
                strcond += " and  c.Status = '" + cInfo.Status + "'";
            }
            if ((cInfo.CreateDateFrom != null) && (cInfo.CreateDateFrom != ""))
            {
                strcond += " AND c.CreateDate BETWEEN CONVERT(DATETIME, '" + cInfo.CreateDateFrom + "',103) AND CONVERT(DATETIME,'" + cInfo.CreateDateTo + " 23:59:59:999',103)";
            }
            if ((cInfo.MERCHANT_CODE != null) && (cInfo.MERCHANT_CODE != ""))
            {
                strcond += " and  c.MERCHANT_CODE = '" + cInfo.MERCHANT_CODE + "'";
            }
            DataTable dt = new DataTable();
            var LCampaign = new List<LeadManagementInfo>();


            try
            {
                string strsql = "select count(c.Id) as countLead from " + dbName + ".dbo.LeadManagement c " +
                                " left join Customer cm on cm.CustomerCode=c.CustomerCode " +
                                " left join Product p on p.Id= c.ProductID " +
                                " left join " + dbName + ".dbo.Lookup AS lo ON lo.LookupCode = c.RecontactbactPeriodTime AND lo.LookupType = 'RECONTACTBACKPERIODTIME' " +
                                " left join " + dbName + ".dbo.Lookup AS tl ON tl.LookupCode = c.TransactionTypeCode AND tl.LookupType = 'TRANSACTIONTYPE' " +
                                " where c.FlagDelete ='N' and ((AssignEmpCode IS NULL) OR (AssignEmpCode = '')) " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new LeadManagementInfo()
                             {
                                 countLead = Convert.ToInt32(dr["countLead"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LCampaign.Count > 0)
            {
                count = LCampaign[0].countLead;
            }

            return count;
        }

        public List<LeadManagementInfo> ListAssignLeadManagementPagingByCriteria(LeadManagementInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.LeadID != null) && (cInfo.LeadID != 0))
            {
                strcond += " and  c.Id =" + cInfo.LeadID;
            }

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode like '%" + cInfo.CustomerCode + "%'";
            }
            if ((cInfo.CustomerPhone != null) && (cInfo.CustomerPhone != ""))
            {
                //strcond += " and  c.CustomerPhone like '%" + cInfo.CustomerPhone + "%'";

                strcond += " and  (c.mobile_1 like '%" + cInfo.CustomerPhone + "%'";
                strcond += " or  c.mobile_2 like '%" + cInfo.CustomerPhone + "%'";
                strcond += " or  c.mobile_3 like '%" + cInfo.CustomerPhone + "%'";
                strcond += " or  c.mobile_4 like '%" + cInfo.CustomerPhone + "%'";
                strcond += " or  c.mobile_5 like '%" + cInfo.CustomerPhone + "%'";
                strcond += " or  c.mobile_6 like '%" + cInfo.CustomerPhone + "%')";
            }
            if ((cInfo.CustomerFName == cInfo.CustomerLName))
            {
                if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
                {
                    strcond += " and  (c.firstname_th like '%" + cInfo.CustomerFName + "%'";
                    strcond += " or  c.lastname_th like '%" + cInfo.CustomerFName + "%')";
                }
            }
            else 
            {
                if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
                {
                    strcond += " and  c.firstname_th like '%" + cInfo.CustomerFName + "%'";

                }
                if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
                {
                    strcond += " and  c.lastname_th like '%" + cInfo.CustomerLName + "%'";
                }
            }
          
            if ((cInfo.Status != null) && (cInfo.Status != ""))
            {
                strcond += " and  c.Status = '" + cInfo.Status + "'";
            }
            if ((cInfo.CreateDateFrom != null) && (cInfo.CreateDateFrom != ""))
            {
                strcond += " AND c.CreateDate BETWEEN CONVERT(DATETIME, '" + cInfo.CreateDateFrom + "',103) AND CONVERT(DATETIME,'" + cInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<LeadManagementInfo>();

            try
            {
                string strsql = " select c.*, cm.CustomerFName, cm.CustomerLName, p.ProductName, pr.PromotionName, lo.LookupValue AS RecontactbactPeriodTimeName, tl.LookupValue AS TransactionTypeName from "
                                + dbName + ".dbo.LeadManagement c " +
                                " left join Customer cm on cm.CustomerCode=c.CustomerCode " +
                                " left join Product p on p.Id=c.Productid " +
                                " left join Promotion pr on pr.PromotionCode=c.Promotion_Code " +
                                " left join " + dbName + ".dbo.Lookup AS lo ON lo.LookupCode = c.RecontactbactPeriodTime AND lo.LookupType = 'RECONTACTBACKPERIODTIME' " +
                                " left join " + dbName + ".dbo.Lookup AS tl ON tl.LookupCode = c.TransactionTypeCode AND tl.LookupType = 'TRANSACTIONTYPE' " +
                                " where c.FlagDelete ='N' and ((AssignEmpCode IS NULL) OR (AssignEmpCode = '')) " + strcond;

                strsql += " ORDER BY c.Id ASC OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new LeadManagementInfo()
                             {
                                 LeadID = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                 CustomerPhone = dr["CustomerPhone"].ToString().Trim(),
                                 CustomerFName = (dr["CustomerFName"].ToString().Trim() != null) ?  dr["CustomerFName"].ToString().Trim() :  dr["firstname_th"].ToString().Trim(),                                
                                 CustomerLName =(dr["CustomerLName"].ToString().Trim() != null) ? dr["CustomerLName"].ToString().Trim() : dr["lastname_th"].ToString().Trim(),
                                 CustomerName = dr["firstname_th"].ToString().Trim() + " " + dr["lastname_th"].ToString().Trim(),
                                 MOBILE_1 = dr["MOBILE_1"].ToString().Trim(),
                                 MOBILE_2 = dr["MOBILE_2"].ToString().Trim(),
                                 MOBILE_3 = dr["MOBILE_3"].ToString().Trim(),
                                 MOBILE_4 = dr["MOBILE_4"].ToString().Trim(),
                                 MOBILE_5 = dr["MOBILE_5"].ToString().Trim(),
                                 MOBILE_6 = dr["MOBILE_6"].ToString().Trim(),
                                 PHONE_1 = dr["PHONE_1"].ToString().Trim(),
                                 PHONE_2 = dr["PHONE_2"].ToString().Trim(),
                                 PHONE_3 = dr["PHONE_3"].ToString().Trim(),
                                 CAMPAIGN_ID = dr["CAMPAIGN_ID"].ToString().Trim(),
                                 Insurancetype = dr["Insurancetype"].ToString().Trim(),
                                 Caryear = dr["Caryear"].ToString().Trim(),
                                 Cartype = dr["Cartype"].ToString().Trim(),
                                 Carmodel = dr["Carmodel"].ToString().Trim(),
                                 Carsubmodel = dr["Carsubmodel"].ToString().Trim(),
                                 Insurancedate = dr["Insurancedate"].ToString().Trim(),
                                 Name = dr["Name"].ToString().Trim(),
                                 Telephone = dr["Telephone"].ToString().Trim(),
                                 Email = dr["Email"].ToString().Trim(),
                                 Productid = (dr["Productid"].ToString() != "") ? Convert.ToInt32(dr["Productid"]) : 0,
                                 ProductName = dr["ProductName"].ToString().Trim(),
                                 Promotion_Code = dr["Promotion_Code"].ToString().Trim(),
                                 PromotionName = dr["PromotionName"].ToString().Trim(),
                                 Status = dr["Status"].ToString().Trim(),
                                 LeadCode = dr["LeadCode"].ToString().Trim(),
                                 RecontactbackDate = dr["RecontactbackDate"].ToString().Trim(),
                                 RecontactbactPeriodTime = dr["RecontactbactPeriodTime"].ToString().Trim(),
                                 RecontactbactPeriodTimeName = dr["RecontactbactPeriodTimeName"].ToString().Trim(),
                                 CusReason = dr["CusReason"].ToString().Trim(),
                                 CusReasonOther = dr["CusReasonOther"].ToString().Trim(),
                                 TransactionTypeCode = dr["TransactionTypeCode"].ToString().Trim(),
                                 TransactionTypeName = dr["TransactionTypeName"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 FULL_NAME_TH = dr["firstname_th"].ToString()+"  "+ dr["lastname_th"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public int UpdateAssignEmpLeadManagement(LeadManagementInfo cInfo)
        {
            string StrCon = "";
            int i = 0;
            if (cInfo.CallOutStatus!=null) 
            {
                StrCon += " CallOutStatus = '" + cInfo.CallOutStatus + "',";
            }
            if (cInfo.AssignEmpCode != null&& cInfo.AssignEmpCode!="")
            {
                StrCon += " AssignEmpCode = '" + cInfo.AssignEmpCode + "',";
            }
            if (cInfo.Status != null && cInfo.Status != "")
            {
                StrCon += " Status = '" + cInfo.Status + "',";
            }
            if (cInfo.UpdateBy != null && cInfo.UpdateBy != "")
            {
                StrCon += " UpdateBy = '" + cInfo.UpdateBy + "',";
            }
            if (cInfo.CustomerCode != null && cInfo.CustomerCode != "")
            {
                StrCon += " CustomerCode = '" + cInfo.CustomerCode + "',";
            }
            string strsql = " Update " + dbName + ".dbo.LeadManagement set " +
                           
                StrCon+
                       

                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where Id =" + cInfo.LeadID + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertLeadfromEcommerceOrder(LeadManagementInfo cInfo)
        {
            String genLeadCode = "";
            int leadCode = getMaxLead(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());

            genLeadCode = cInfo.TransactionTypeCode + (DateTime.Now.Year + 543).ToString() + DateTime.Now.ToString("MM") + String.Format("{0:00000}", leadCode);

            if (cInfo.Productid == 0 || cInfo.Productid == null)
            {
                cInfo.Productid = 0;
            }
            
            int i = 0;

            string strsql = "INSERT INTO LeadManagement  (" +
                "Insurancetype, Caryear, Cartype, Carmodel, Carsubmodel, Insurancedate, Name, Telephone, Email, Productid, Promotion_Code, Status, CampaignCode, LeadCode, RunningNo, CusReason, CusReasonOther, TransactionTypeCode, RecontactbackDate, RecontactbactPeriodTime, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete) " +
                            "VALUES (" +
                           "'" + cInfo.Insurancetype + "'," +
                           "'" + cInfo.Caryear + "'," +
                           "'" + cInfo.Cartype + "'," +
                           "'" + cInfo.Carmodel + "'," +
                           "'" + cInfo.Carsubmodel + "'," +
                           "'" + cInfo.Insurancedate + "'," +
                           "'" + cInfo.Name + "'," +
                           "'" + cInfo.Telephone + "'," +
                           "'" + cInfo.Email + "'," +
                           + cInfo.Productid + "," +
                           "'" + cInfo.Promotion_Code + "'," +
                           "'" + cInfo.Status + "'," +
                           "'" + cInfo.CampaignCode + "'," +
                           "'" + genLeadCode + "'," +
                               + leadCode + "," +
                           "'" + cInfo.CusReason + "'," +
                           "'" + cInfo.CusReasonOther + "'," +
                           "'" + cInfo.TransactionTypeCode + "'," +
                           "'" + cInfo.RecontactbackDate + "'," +
                           "'" + cInfo.RecontactbactPeriodTime + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.UpdateBy + "'," +
                           "'N'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateStatusTIBLeadfromTakeOrderRetail(LeadManagementInfo cInfo)
        {
            string strcond = "";
            int i = 0;

            if ((cInfo.Status != null) && (cInfo.Status != ""))
            {
                strcond += " Status = '" + cInfo.Status + "',";
            }
            if ((cInfo.OrderCode != null) && (cInfo.OrderCode != ""))
            {
                strcond += " OrderCode = '" + cInfo.OrderCode + "',";
            }

            string strsql = " Update " + dbName + ".dbo.LeadManagement set " +
                            strcond +
                            " UpdateBy = '" + cInfo.UpdateBy + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where Id =" + cInfo.LeadID + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int getMaxLead(String year, String month)
        {
            int maxLead = 1;

            DataTable dt = new DataTable();

            string strsql = @" select isnull(max(isnull(runningno,0)),0) + 1 max_no from " + dbName + @".dbo.LeadManagement
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
                    maxLead = (dt.Rows[0]["max_no"] != null) ? int.Parse(dt.Rows[0]["max_no"].ToString()) : 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return maxLead;
        }

        public int UpdateTIBLeadfromClickToCall(LeadManagementInfo cInfo)
        {
            string strcond = "";
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.LeadManagement set " +
                            " Status = '" + cInfo.Status + "'," +
                            " Name = '" + cInfo.Name + "'," +
                            " Telephone = '" + cInfo.Telephone + "'," +
                            " Insurancetype = '" + cInfo.Insurancetype + "'," +
                            " Caryear = '" + cInfo.Caryear + "'," +
                            " Cartype = '" + cInfo.Cartype + "'," +
                            " Carmodel = '" + cInfo.Carmodel + "'," +
                            " Carsubmodel = '" + cInfo.Carsubmodel + "'," +
                            " Insurancedate = '" + cInfo.Insurancedate + "'," +
                            " CallStatus = '" + cInfo.CallStatus + "'," +
                            " CallSituation = '" + cInfo.CallSituation + "'," +
                            " Description = '" + cInfo.Description + "'," +
                            " RecontactbackDate = CONVERT(DATETIME, '" + cInfo.RecontactbackDate + "', 103)," +
                            " RecontactbactPeriodTime = '" + cInfo.RecontactbactPeriodTime + "'," +
                            " CusReason = '" + cInfo.CusReason + "'," +
                            " CusReasonOther = '" + cInfo.CusReasonOther + "'," +                            
                            " UpdateBy = '" + cInfo.UpdateBy + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where Id =" + cInfo.LeadID + "";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int CountLeadManagement(LeadManagementInfo cInfo)
        {
            string strcond = "";
            int count = 0;

            if ((cInfo.LeadID != null) && (cInfo.LeadID != 0))
            {
                strcond += " and  c.Id =" + cInfo.LeadID;
            }

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode like '%" + cInfo.CustomerCode + "%'";
            }
            if ((cInfo.CustomerPhone != null) && (cInfo.CustomerPhone != ""))
            {
                //strcond += " and  c.CustomerPhone like '%" + cInfo.CustomerPhone + "%'";

                strcond += " and  (c.mobile_1 like '%" + cInfo.CustomerPhone + "%'";
                strcond += " or  c.mobile_2 like '%" + cInfo.CustomerPhone + "%'";
                strcond += " or  c.mobile_3 like '%" + cInfo.CustomerPhone + "%'";
                strcond += " or  c.mobile_4 like '%" + cInfo.CustomerPhone + "%'";
                strcond += " or  c.mobile_5 like '%" + cInfo.CustomerPhone + "%'";
                strcond += " or  c.mobile_6 like '%" + cInfo.CustomerPhone + "%')";
            }
            if ((cInfo.CustomerFName == cInfo.CustomerLName))
            {
                if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
                {
                    strcond += " and  (c.firstname_th like '%" + cInfo.CustomerFName + "%'";
                    strcond += " or  c.lastname_th like '%" + cInfo.CustomerFName + "%')";
                }
            }
            else
            {
                if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
                {
                    strcond += " and  c.firstname_th like '%" + cInfo.CustomerFName + "%'";

                }
                if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
                {
                    strcond += " and  c.lastname_th like '%" + cInfo.CustomerLName + "%'";
                }
            }
            if ((cInfo.Status != null) && (cInfo.Status != ""))
            {
                strcond += " and  c.Status in (" + cInfo.Status + ")";
            }
            if ((cInfo.CreateDateFrom != null) && (cInfo.CreateDateFrom != ""))
            {
                strcond += " AND c.CreateDate BETWEEN CONVERT(DATETIME, '" + cInfo.CreateDateFrom + "',103) AND CONVERT(DATETIME,'" + cInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            if ((cInfo.MERCHANT_CODE != null) && (cInfo.MERCHANT_CODE != ""))
            {
                strcond += " and  c.MERCHANT_CODE = '" + cInfo.MERCHANT_CODE + "'";
            }
            DataTable dt = new DataTable();
            var LCampaign = new List<LeadManagementInfo>();


            try
            {
                string strsql = "select count(c.Id) as countLead from " + dbName + ".dbo.LeadManagement c " +
                                " left join Customer cm on cm.CustomerCode=c.CustomerCode " +
                                " left join Product p on p.Id= c.ProductID " +
                                " left join " + dbName + ".dbo.Lookup AS lo ON lo.LookupCode = c.RecontactbactPeriodTime AND lo.LookupType = 'RECONTACTBACKPERIODTIME' " +
                                " left join " + dbName + ".dbo.Lookup AS tl ON tl.LookupCode = c.TransactionTypeCode AND tl.LookupType = 'TRANSACTIONTYPE' " +
                                " where c.FlagDelete ='N'  " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new LeadManagementInfo()
                             {
                                 countLead = Convert.ToInt32(dr["countLead"])

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LCampaign.Count > 0)
            {
                count = int.Parse(LCampaign[0].countLead.ToString());
            }

            return count;
        }
    }
   
}
