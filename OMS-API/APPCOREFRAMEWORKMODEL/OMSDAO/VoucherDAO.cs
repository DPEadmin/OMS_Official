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
    public class VoucherDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int? CountVoucherListByCriteria(VoucherInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.VoucherId != null) && (pInfo.VoucherId != 0))
            {
                strcond += " and  p.Id =" + pInfo.VoucherId;
            }

            if ((pInfo.VoucherCode != null) && (pInfo.VoucherCode != ""))
            {
                strcond += " and  p.VoucherCode like '%" + pInfo.VoucherCode + "%'";
            }
            if ((pInfo.VoucherName != null) && (pInfo.VoucherName != ""))
            {
                strcond += " and  p.VoucherName like '%" + pInfo.VoucherName + "%'";
            }
            if ((pInfo.VoucherTypeCode != null) && (pInfo.VoucherTypeCode != ""))
            {
                strcond += " and  p.VoucherTypeCode = '" + pInfo.VoucherTypeCode + "'";
            }
            if ((pInfo.CampaignCategoryCode != null) && (pInfo.CampaignCategoryCode != ""))
            {
                strcond += " and  p.CampaignCategoryCode = '" + pInfo.CampaignCategoryCode + "'";
            }
            if ((pInfo.StatusCode != null) && (pInfo.StatusCode != ""))
            {
                strcond += " and  p.StatusCode = '" + pInfo.StatusCode + "'";
            }
            if (pInfo.Price != null)
            {
                strcond += " and  p.Price = " + pInfo.Price + "";
            }
            if (((pInfo.StartDateFrom != null) && (pInfo.StartDateFrom != "")) && ((pInfo.StartDateTo != null) && (pInfo.StartDateTo != "")))
            {
                
                strcond += " and  p.StartDate BETWEEN CONVERT(datetime, '" + pInfo.StartDateFrom + "', 103)  AND  DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime, '" + pInfo.StartDateTo + "', 103)), '23:59:59')";

            }
            if (((pInfo.EndDateFrom != null) && (pInfo.EndDateFrom != "")) && ((pInfo.EndDateTo != null) && (pInfo.EndDateTo != "")))
            {
                
                strcond += " and  p.EndDate BETWEEN CONVERT(datetime, '" + pInfo.EndDateFrom + "', 103)  AND  DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime, '" + pInfo.EndDateTo + "', 103)), '23:59:59')";

            }


            DataTable dt = new DataTable();
            var lvoucher = new List<VoucherListReturn>();


            try
            {
                string strsql = "select count(p.Id) as countVoucher from " + dbName + ".dbo.Voucher p " +
                                " left join Lookup vt on vt.LookupCode = p.VoucherTypeCode and vt.LookupType= 'VOUCHERTYPE'" +
                               " left join Lookup s on s.LookupCode = p.StatusCode and s.LookupType= 'VOUCHERSTATUS'" +
                                " left join CampaignCategory c on c.CampaignCategoryCode = p.CampaignCategoryCode" +
                                " where p.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                lvoucher = (from DataRow dr in dt.Rows

                             select new VoucherListReturn()
                             {
                                 countVoucher = Convert.ToInt32(dr["countVoucher"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (lvoucher.Count > 0)
            {
                count = lvoucher[0].countVoucher;
            }

            return count;
        }

        public int InsertVoucher(VoucherInfo pInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO Voucher  (VoucherCode,VoucherName,VoucherTypeCode,CampaignCategoryCode," +
                "StatusCode,Price,StartDate,EndDate,Quantity,Reserve,Balance,Remark,FlagUnlimit,CreateDate,CreateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + pInfo.VoucherCode + "'," +
                           "'" + pInfo.VoucherName + "'," +
                           "'" + pInfo.VoucherTypeCode + "'," +
                           "'" + pInfo.CampaignCategoryCode + "'," +
                           "'" + pInfo.StatusCode + "'," +
                           "" + pInfo.Price + "," +
                           "'" + pInfo.StartDate + "'," +
                           "'" + pInfo.EndDate + "'," +
                           "" + pInfo.Quantity + "," +
                           "" + pInfo.Reserve + "," +
                           "" + pInfo.Balance + "," +
                           "'" + pInfo.Remark + "'," +
                           "'" + pInfo.FlagUnlimit + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pInfo.CreateBy + "'," +
                           "'" + pInfo.FlagDelete + "'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateVoucher(VoucherInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Voucher set " +
                            " VoucherCode = '" + pInfo.VoucherCode + "'," +
                            " VoucherName = '" + pInfo.VoucherName + "'," +
                            " VoucherTypeCode = '" + pInfo.VoucherTypeCode + "'," +
                            " CampaignCategoryCode = '" + pInfo.CampaignCategoryCode + "'," +
                            " StatusCode = '" + pInfo.StatusCode + "'," +
                            " Price = " + pInfo.Price + "," +
                            " StartDate = '" + pInfo.StartDate + "'," +
                            " EndDate = '" + pInfo.EndDate + "'," +
                            " Quantity = " + pInfo.Quantity + "," +
                            " Reserve = " + pInfo.Reserve + "," +
                            " Balance = " + pInfo.Balance + "," +
                            " Remark = '" + pInfo.Remark + "'," +
                            " FlagUnlimit = '" + pInfo.FlagUnlimit + "'," +
                             " UpdateBy = '" + pInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where Id =" + pInfo.VoucherId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteVoucher(VoucherInfo pInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Voucher set FlagDelete = 'Y' where Id in (" + pInfo.VoucherIdList + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<VoucherListReturn> ListVoucherNopagingByCriteria(VoucherInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.VoucherId != null) && (pInfo.VoucherId != 0))
            {
                strcond += " and  p.Id =" + pInfo.VoucherId;
            }

            if ((pInfo.VoucherCode != null) && (pInfo.VoucherCode != ""))
            {
                strcond += " and  p.VoucherCode = '" + pInfo.VoucherCode + "'";
            }
            if ((pInfo.VoucherName != null) && (pInfo.VoucherName != ""))
            {
                strcond += " and  p.VoucherName like '%" + pInfo.VoucherName + "%'";
            }
            if ((pInfo.VoucherTypeCode != null) && (pInfo.VoucherTypeCode != ""))
            {
                strcond += " and  p.VoucherTypeCode = '" + pInfo.VoucherTypeCode + "'";
            }
            if ((pInfo.CampaignCategoryCode != null) && (pInfo.CampaignCategoryCode != ""))
            {
                strcond += " and  p.CampaignCategoryCode = '" + pInfo.CampaignCategoryCode + "'";
            }
            if ((pInfo.StatusCode != null) && (pInfo.StatusCode != ""))
            {
                strcond += " and  p.StatusCode = '" + pInfo.StatusCode + "'";
            }
            if (pInfo.Price != null)
            {
                strcond += " and  p.Price = " + pInfo.Price + "";
            }
            if (((pInfo.StartDateFrom != null) && (pInfo.StartDateFrom != "")) && ((pInfo.StartDateTo != null) && (pInfo.StartDateTo != "")))
            {
                
                strcond += " and  p.StartDate BETWEEN CONVERT(datetime, '" + pInfo.StartDateFrom + "', 103)  AND  DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime, '" + pInfo.StartDateTo + "', 103)), '23:59:59')";

            }
            if (((pInfo.EndDateFrom != null) && (pInfo.EndDateFrom != "")) && ((pInfo.EndDateTo != null) && (pInfo.EndDateTo != "")))
            {
                
                strcond += " and  p.EndDate BETWEEN CONVERT(datetime, '" + pInfo.EndDateFrom + "', 103)  AND  DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime, '" + pInfo.EndDateTo + "', 103)), '23:59:59')";

            }

            DataTable dt = new DataTable();
            var lvoucher = new List<VoucherListReturn>();

            try
            {
                string strsql = " select vt.LookupValue as VoucherTypeName,s.LookupValue as StatusName,c.CamCate_Name as CampaignCategoryName" +
                                ",p.* from " + dbName + ".dbo.Voucher p " +
                               " left join Lookup vt on vt.LookupCode = p.VoucherTypeCode and vt.LookupType= 'VOUCHERTYPE'" +
                               " left join Lookup s on s.LookupCode = p.StatusCode and s.LookupType= 'VOUCHERSTATUS'" +
                                " left join CampaignCategory c on c.CampaignCategoryCode = p.CampaignCategoryCode" +
                                " where p.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY p.Id desc ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                lvoucher = (from DataRow dr in dt.Rows

                             select new VoucherListReturn()
                             {
                                 VoucherId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 VoucherCode = dr["VoucherCode"].ToString().Trim(),
                                 VoucherName = dr["VoucherName"].ToString().Trim(),
                                 VoucherTypeCode = dr["VoucherTypeCode"].ToString().Trim(),
                                 VoucherTypeName = dr["VoucherTypeName"].ToString().Trim(),
                                 CampaignCategoryCode = dr["CampaignCategoryCode"].ToString().Trim(),
                                 CampaignCategoryName = dr["CampaignCategoryName"].ToString().Trim(),
                                 StatusCode = dr["StatusCode"].ToString().Trim(),
                                 StatusName = dr["StatusName"].ToString().Trim(),
                                 StartDate = dr["StartDate"].ToString().Trim(),
                                 EndDate = dr["EndDate"].ToString().Trim(),
                                 Remark = dr["Remark"].ToString().Trim(),
                                 Quantity = (dr["Quantity"].ToString() != "") ? Convert.ToInt32(dr["Quantity"]) : 0,
                                 Reserve = (dr["Reserve"].ToString() != "") ? Convert.ToInt32(dr["Reserve"]) : 0,
                                 Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
                                 Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                                
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),

                                 FlagDelete = dr["FlagDelete"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return lvoucher;
        }
        
        public List<VoucherListReturn> ListVoucherByCriteria(VoucherInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.VoucherId != null) && (pInfo.VoucherId != 0))
            {
                strcond += " and  p.Id =" + pInfo.VoucherId;
            }

            if ((pInfo.VoucherCode != null) && (pInfo.VoucherCode != ""))
            {
                strcond += " and  p.VoucherCode like '%" + pInfo.VoucherCode + "%'";
            }
            if ((pInfo.VoucherName != null) && (pInfo.VoucherName != ""))
            {
                strcond += " and  p.VoucherName like '%" + pInfo.VoucherName + "%'";
            }
            if ((pInfo.VoucherTypeCode != null) && (pInfo.VoucherTypeCode != ""))
            {
                strcond += " and  p.VoucherTypeCode = '" + pInfo.VoucherTypeCode + "'";
            }
            if ((pInfo.CampaignCategoryCode != null) && (pInfo.CampaignCategoryCode != ""))
            {
                strcond += " and  p.CampaignCategoryCode = '" + pInfo.CampaignCategoryCode + "'";
            }
            if ((pInfo.StatusCode != null) && (pInfo.StatusCode != ""))
            {
                strcond += " and  p.StatusCode = '" + pInfo.StatusCode + "'";
            }
            if (pInfo.Price != null)
            {
                strcond += " and  p.Price = " + pInfo.Price + "";
            }
            if (((pInfo.StartDateFrom != null) && (pInfo.StartDateFrom != "")) && ((pInfo.StartDateTo != null) && (pInfo.StartDateTo != "")))
            {
               
                strcond += " and  p.StartDate BETWEEN CONVERT(datetime, '" + pInfo.StartDateFrom + "', 103)  AND  DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime, '" + pInfo.StartDateTo + "', 103)), '23:59:59')";

            }
            if (((pInfo.EndDateFrom != null) && (pInfo.EndDateFrom != "")) && ((pInfo.EndDateTo != null) && (pInfo.EndDateTo != "")))
            {
               
                strcond += " and  p.EndDate BETWEEN CONVERT(datetime, '" + pInfo.EndDateFrom + "', 103)  AND  DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime, '" + pInfo.EndDateTo + "', 103)), '23:59:59')";

            }

            DataTable dt = new DataTable();
            var lvoucher = new List<VoucherListReturn>();

            try
            {
                string strsql = " select vt.LookupValue as VoucherTypeName,s.LookupValue as StatusName,c.CamCate_Name as CampaignCategoryName" +
                                ",p.* from " + dbName + ".dbo.Voucher p " +
                                " left join Lookup vt on vt.LookupCode = p.VoucherTypeCode and vt.LookupType= 'VOUCHERTYPE'" +
                               " left join Lookup s on s.LookupCode = p.StatusCode and s.LookupType= 'VOUCHERSTATUS'" +
                                " left join CampaignCategory c on c.CampaignCategoryCode = p.CampaignCategoryCode" +
                                " where p.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY p.Id DESC OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                lvoucher = (from DataRow dr in dt.Rows

                            select new VoucherListReturn()
                            {
                                VoucherId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                VoucherCode = dr["VoucherCode"].ToString().Trim(),
                                VoucherName = dr["VoucherName"].ToString().Trim(),
                                VoucherTypeCode = dr["VoucherTypeCode"].ToString().Trim(),
                                VoucherTypeName = dr["VoucherTypeName"].ToString().Trim(),
                                CampaignCategoryCode = dr["CampaignCategoryCode"].ToString().Trim(),
                                CampaignCategoryName = dr["CampaignCategoryName"].ToString().Trim(),
                                StatusCode = dr["StatusCode"].ToString().Trim(),
                                StatusName = dr["StatusName"].ToString().Trim(),
                                StartDate = dr["StartDate"].ToString().Trim(),
                                EndDate = dr["EndDate"].ToString().Trim(),
                                Remark = dr["Remark"].ToString().Trim(),
                                Quantity = (dr["Quantity"].ToString() != "") ? Convert.ToInt32(dr["Quantity"]) : 0,
                                Reserve = (dr["Reserve"].ToString() != "") ? Convert.ToInt32(dr["Reserve"]) : 0,
                                Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
                                Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,

                                CreateBy = dr["CreateBy"].ToString(),
                                CreateDate = dr["CreateDate"].ToString(),
                                UpdateBy = dr["UpdateBy"].ToString(),

                                FlagDelete = dr["FlagDelete"].ToString(),
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return lvoucher;
        }

        
    }
}
