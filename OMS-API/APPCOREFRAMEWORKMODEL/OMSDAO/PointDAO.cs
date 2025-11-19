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
    public class PointDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int UpdatePointRange(PointInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.PointRange set " +
                            " PointCode = '" + pInfo.PointCode + "'," +
                            " PointName = '" + pInfo.PointName + "'," +
                            " PointSequence = '" + pInfo.PointSequence + "'," +
                            " PointBegin = '" + pInfo.PointBegin + "'," +
                            " PointEnd = '" + pInfo.PointEnd + "'," +
                            " UpdateBy = '" + pInfo.UpdateBy + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where ID =" + pInfo.PointId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int InsertPointRange(PointInfo pInfo)
        {

            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.PointRange (PointCode,PointName,PointBegin,PointSequence,PointEnd" +
                ",CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete,MerchantMapCode)" +
                           "VALUES (" +
                          "'" + pInfo.PointCode + "'," +
                          "'" + pInfo.PointName + "'," +
                          "'" + pInfo.PointBegin + "'," +
                          "'" + pInfo.PointSequence + "'," +
                          "'" + pInfo.PointEnd + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + pInfo.CreateBy + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + pInfo.UpdateBy + "'," +
                          "'" + pInfo.FlagDelete + "'," +
                          "'" + pInfo.MerchantMapCode + "'" +
                           ")";
            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int InsertPointRate(PointInfo pInfo)
        {

            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.PointRate (Price,GetPoint,CreateDate,CreateBy,UpdateDate" +
                ",UpdateBy,FlagDelete,MerchantMapCode)" +
                           "VALUES (" +
                          "'" + pInfo.Price + "'," +
                          "'" + pInfo.GetPoint + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + pInfo.CreateBy + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + pInfo.UpdateBy + "'," +
                          "'" + pInfo.FlagDelete + "'," +
                          "'" + pInfo.MerchantMapCode + "'" +
                           ")";
            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdatePointRate(PointInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.PointRate set " +
                            " Price = '" + pInfo.Price + "'," +
                            " GetPoint = '" + pInfo.GetPoint + "'," +
                            " UpdateBy = '" + pInfo.UpdateBy + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where ID =" + pInfo.PointId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeletePointRange(PointInfo pInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.PointRange set FlagDelete = 'Y' where Id in (" + pInfo.PointId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertPointRange(PromotionInfo pInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO Promotion  (MerchantCode,PromotionCode,PromotionName,PromotionDesc,PromotionTypeCode,CreateDate,CreateBy,FlagDelete, FreeShipping, PromotionStatus," +
                            " MOQFlag, MinimumQty, DiscountAmount, DiscountPercent, LockAmountFlag, LockCheckbox, DefaultAmount, ProductDiscountAmount, ProductDiscountPercent, MinimumTotPrice," + 
                            " RedeemFlag, ComplementaryFlag, PromotionLevel, GroupPrice, PicturePromotionUrl, CombosetName, CombosetFlag, StartDate, EndDate, ComplementaryChangeAble, Bu, Levels," +
                            " RequestByEmpCode, RequestDate, Status, FinishFlag, ProductBrandCode)" +
                            "VALUES (" +
                           "'" + pInfo.MerchantCode + "'," +
                           "'" + pInfo.PromotionCode + "'," +
                           "'" + pInfo.PromotionName + "'," +
                           "'" + pInfo.PromotionDesc + "'," +
                           "'" + pInfo.PromotionTypeCode + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pInfo.CreateBy + "'," +
                           
                           "'" + pInfo.FlagDelete + "'," +
                           "'" + pInfo.FreeShippingCode + "'," +
                           "'" + pInfo.PromotionStatusCode + "'," +
                           "'" + pInfo.MOQFlag + "'," +
                            pInfo.MinimumQty + "," +
                            pInfo.DiscountAmount + "," +
                            pInfo.DiscountPercent + "," +
                           "'" + pInfo.LockAmountFlag + "'," +
                           "'" + pInfo.LockCheckbox + "'," +
                           pInfo.DefaultAmount + "," +
                           pInfo.ProductDiscountAmount + "," +
                            pInfo.ProductDiscountPercent + "," +
                            pInfo.MinimumTotPrice + "," +
                            "'" + pInfo.RedeemFlag + "'," +
                            "'" + pInfo.ComplementaryFlag + "'," +
                            "'" + pInfo.PromotionLevel + "'," +
                            pInfo.GroupPrice + ", " +
                           "'" + pInfo.PicturePromotionUrl + "'," +
                           "'" + pInfo.CombosetName + "'," +
                           "'" + pInfo.CombosetFlag + "', " +
                           "CONVERT(DATETIME, '" + pInfo.StartDate + "',103), " +
                           "CONVERT(DATETIME, '" + pInfo.EndDate + "',103), " +
                            "'" + pInfo.ComplementaryChangeAble + "'," +
                            "'" + pInfo.Bu + "'," +
                            "'" + pInfo.levels + "'," +
                            "'" + pInfo.RequestByEmpCode + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            "'" + pInfo.wfStatus + "'," +
                            "'" + pInfo.wfFinishFlag + "'," +
                           "'" + pInfo.ProductBrandCode + "'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

      
        public List<PointInfo> ListPointRangePagingbyCriteria(PointInfo pInfo)
        {
            string strcond = ""; 
            string stroffset = "";
            if ((pInfo.PointCode != null) && (pInfo.PointCode != ""))
            {
                strcond += " and  p.PointCode = '" + pInfo.PointCode + "'";
            }
            if ((pInfo.MerchantMapCode != null) && (pInfo.MerchantMapCode != ""))
            {
                strcond += " and  p.MerchantMapCode = '" + pInfo.MerchantMapCode + "'";
            }
            if ((pInfo.rowOFFSet != null) && (pInfo.rowFetch != 0) && (pInfo.rowOFFSet != null) && (pInfo.rowFetch != 0))
            {
                stroffset += " OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY ";
            }
            DataTable dt = new DataTable();
            var LPoint = new List<PointInfo>();

            try
            {
                string strsql = " select p.* from " + dbName + ".dbo.PointRange p " +
                               " where p.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY p.PointSequence ASC " + stroffset;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPoint = (from DataRow dr in dt.Rows

                              select new PointInfo()
                              {
                                  PointId = (dr["ID"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  PointCode = dr["PointCode"].ToString().Trim(),
                                  PointName = dr["PointName"].ToString().Trim(),
                                  PointSequence = (dr["PointSequence"].ToString() != "") ? Convert.ToInt32(dr["PointSequence"]) : 0,
                                  PointBegin = (dr["PointBegin"].ToString() != "") ? Convert.ToInt32(dr["PointBegin"]) : 0,
                                  PointEnd = (dr["PointEnd"].ToString() != "") ? Convert.ToInt32(dr["PointEnd"]) : 0,


                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPoint;
        }
        public List<PointInfo> ListPointRateNoPagingbyCriteria(PointInfo pInfo)
        {
            string strcond = "";
            if ((pInfo.MerchantMapCode != null) && (pInfo.MerchantMapCode != ""))
            {
                strcond += " and  p.MerchantMapCode = '" + pInfo.MerchantMapCode + "'";
            }
            DataTable dt = new DataTable();
            var LPoint = new List<PointInfo>();

            try
            {
                string strsql = " select p.* from " + dbName + ".dbo.PointRate p " +
                               " where p.FlagDelete ='N' " + strcond;

               

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPoint = (from DataRow dr in dt.Rows

                          select new PointInfo()
                          {
                              PointId = (dr["ID"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                              Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                              GetPoint = (dr["GetPoint"].ToString() != "") ? Convert.ToInt32(dr["GetPoint"]) : 0,


                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPoint;
        }


        public int? CountPointRangePagingbyCriteria(PointInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.MerchantMapCode != null) && (pInfo.MerchantMapCode != ""))
            {
                strcond += " and  p.MerchantMapCode = '" + pInfo.MerchantMapCode + "'";
            }

            DataTable dt = new DataTable();
            var LPoint = new List<PointInfo>();


            try
            {
                string strsql = "select count(p.ID) as countPointRange from " + dbName + ".dbo.PointRange p " +
                           " where p.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPoint = (from DataRow dr in dt.Rows

                              select new PointInfo()
                              {
                                  countPointRange = Convert.ToInt32(dr["countPointRange"])
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LPoint.Count > 0)
            {
                count = LPoint[0].countPointRange;
            }

            return count;
        }
        public List<ReportPointInfo> ListReportPromotionPoint(ReportPointInfo pInfo)
        {
            string strcond = "";
            string stroffset = "";

            if ((pInfo.Id != null) && (pInfo.Id != 0))
            {
                strcond += (strcond == "") ? " where ca.Id =" + pInfo.Id : " and  ca.Id =" + pInfo.Id;
                
            }
            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += (strcond == "") ? " where ca.PromotionCode = '" + pInfo.PromotionCode + "'" : " and  ca.PromotionCode = '" + pInfo.PromotionCode + "'";
            }
            if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != ""))
            {
                strcond += (strcond == "") ? " where ca.MerchantCode = '" + pInfo.MerchantCode + "'" : " and  ca.MerchantCode = '" + pInfo.MerchantCode + "'";

            } 
            if ((pInfo.CustomerCode != null) && (pInfo.CustomerCode != ""))
            {
                strcond += (strcond == "") ? " where ca.CustomerCode = '" + pInfo.CustomerCode + "'" : " and  ca.CustomerCode = '" + pInfo.CustomerCode + "'";

            }
            if ((pInfo.ActionCode != null) && (pInfo.ActionCode != "") && (pInfo.ActionCode == "03"))
            {
                strcond += (strcond == "") ? " where ca.ActionCode = '" + pInfo.ActionCode + "'" : " and  ca.ActionCode = '" + pInfo.ActionCode + "'";

            }
            else if ((pInfo.ActionCode == null) || (pInfo.ActionCode == "") || (pInfo.ActionCode == "-99"))
            {
                strcond += (strcond == "") ? " where ca.ActionCode <> '03'"  : " and  ca.ActionCode <> '03'";
            }
            else
            {
                strcond += (strcond == "") ? " where ca.ActionCode <> '03'" + " and  ca.ActionCode = '" + pInfo.ActionCode + "'" : " and  ca.ActionCode <> '03'" + " and  ca.ActionCode = '" + pInfo.ActionCode + "'";
            }

            if ((pInfo.PointTypeCode != null) && (pInfo.PointTypeCode != "") && (pInfo.PointTypeCode != "-99"))
            {
                strcond += (strcond == "") ? " where  p.PointType = '" + pInfo.PointTypeCode + "'" : " and  p.PointType = '" + pInfo.PointTypeCode + "'";
            }
            if ((pInfo.PointRangeCode != null) && (pInfo.PointRangeCode != "") && (pInfo.PointRangeCode != "-99"))
            {
                strcond += (strcond == "") ? " where c.PointRangeCode = '" + pInfo.PointRangeCode + "'" : " and  c.PointRangeCode = '" + pInfo.PointRangeCode + "'";
            }
            if ((pInfo.ProPointCode != null) && (pInfo.ProPointCode != "") && (pInfo.ProPointCode != "-99"))
            {
                strcond += (strcond == "") ? " where p.ProPoint = '" + pInfo.ProPointCode + "'" : " and  p.ProPoint = '" + pInfo.ProPointCode + "'";
            }
            if ((pInfo.CompanyCode != null) && (pInfo.CompanyCode != "") && (pInfo.CompanyCode != "-99"))
            {
                strcond += (strcond == "") ? " where p.CompanyCode = '" + pInfo.CompanyCode + "'" : " and  p.CompanyCode = '" + pInfo.CompanyCode + "'";
            }
            if ((pInfo.rowOFFSet != null) && (pInfo.rowFetch != 0) && (pInfo.rowOFFSet != null) && (pInfo.rowFetch != 0))
            {
                stroffset += " OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY ";
            }

            DataTable dt = new DataTable();
            var ReportPoint = new List<ReportPointInfo>();

            try
            {
                string strsql = " select ca.Id,ca.CreateDate,com.CompanyNameTH, ca.CustomerCode ,pp.LookupValue as ProPointName , c.CustomerFName+' '+c.CustomerLName as CustomerFullName,p.PromotionName,pd.ProductName " +
                                " ,c.ContactTel,pt.LookupValue as PointTypeName ,pr.PointName as PointRangeName,ca.Amount ,ca.PointNum,oi.OrderNote,oi.OrderCode,oi.TotalPrice ,ca.ActionCode,pa.LookupValue as ActionName" +
                                " from " + dbName + ".dbo.CustomerActionPointCoupon ca" +
                                " left join OrderInfo as oi on oi.OrderCode = ca.OrderCode " +
                                " left join promotion as p on p.PromotionCode = ca.PromotionCode " +
                                " left join product as pd on pd.ProductCode = ca.ProductCode " +
                                " left join Customer as c on c.CustomerCode = ca.CustomerCode " +
                                " left join lookup as pt on pt.LookupCode = p.PointType and pt.LookupType = 'POINTTYPE' " +
                                " left join lookup as pp on pp.LookupCode = p.ProPoint and pp.LookupType = 'PROPOINT' " +
                                " left join lookup as pa on pa.LookupCode = ca.ActionCode and pa.LookupType = 'POINTACTION' " +
                                " left join Company as com on com.CompanyCode = p.CompanyCode and com.MerchantMapCode = '" + pInfo.MerchantCode + "' "+
                                " left join PointRange as pr on pr.PointCode = c.PointRangeCode and pr.MerchantMapCode = '" + pInfo.MerchantCode + "' " + strcond ;

                strsql += " ORDER BY ca.Id DESC " + stroffset;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                ReportPoint = (from DataRow dr in dt.Rows

                              select new ReportPointInfo()
                              {
                                  Id = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  CreateDate = dr["CreateDate"].ToString().Trim(),
                                  CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                  CustomerFullName = dr["CustomerFullName"].ToString().Trim(),
                                  PromotionName = dr["PromotionName"].ToString(),
                                  ProductName = dr["ProductName"].ToString(),
                                  ContactTel = dr["ContactTel"].ToString(),
                                  PointTypeName = dr["PointTypeName"].ToString(),
                                  ProPointName = dr["ProPointName"].ToString(),
                                  PointRangeName = dr["PointRangeName"].ToString(),
                                  OrderCode = dr["OrderCode"].ToString(),
                                  OrderNote = dr["OrderNote"].ToString(),
                                  Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]) : 0,
                                  PointNum = (dr["PointNum"].ToString() != "") ? Convert.ToInt32(dr["PointNum"]) : 0,
                                  TotalPrice = (dr["TotalPrice"].ToString() != "") ? Convert.ToDouble(dr["TotalPrice"]) : 0,
                                  ActionCode = dr["ActionCode"].ToString(),
                                  ActionName = dr["ActionName"].ToString(),
                                  CompanyNameTH = dr["CompanyNameTH"].ToString(),

                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return ReportPoint;
        }
        public int? CountReportPromotionPoint(ReportPointInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.Id != null) && (pInfo.Id != 0))
            {
                strcond += (strcond == "") ? " where ca.Id =" + pInfo.Id : " and  ca.Id =" + pInfo.Id;

            }
            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += (strcond == "") ? " where ca.PromotionCode = '" + pInfo.PromotionCode + "'" : " and  ca.PromotionCode = '" + pInfo.PromotionCode + "'";
            }
            if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != ""))
            {
                strcond += (strcond == "") ? " where ca.MerchantCode = '" + pInfo.MerchantCode + "'" : " and  ca.MerchantCode = '" + pInfo.MerchantCode + "'";

            }
            if ((pInfo.CustomerCode != null) && (pInfo.CustomerCode != ""))
            {
                strcond += (strcond == "") ? " where ca.CustomerCode = '" + pInfo.CustomerCode + "'" : " and  ca.CustomerCode = '" + pInfo.CustomerCode + "'";

            }
            if ((pInfo.ActionCode != null) && (pInfo.ActionCode != "") && (pInfo.ActionCode == "03"))
            {
                strcond += (strcond == "") ? " where ca.ActionCode = '" + pInfo.ActionCode + "'" : " and  ca.ActionCode = '" + pInfo.ActionCode + "'";

            }
            else
            {
                strcond += (strcond == "") ? " where ca.ActionCode <> '03'" : " and  ca.ActionCode <> '03'";
            }

            if ((pInfo.PointTypeCode != null) && (pInfo.PointTypeCode != "") && (pInfo.PointTypeCode != "-99"))
            {
                strcond += (strcond == "") ? " where  p.PointType = '" + pInfo.PointTypeCode + "'" : " and  p.PointType = '" + pInfo.PointTypeCode + "'";
            }
            if ((pInfo.PointRangeCode != null) && (pInfo.PointRangeCode != "") && (pInfo.PointRangeCode != "-99"))
            {
                strcond += (strcond == "") ? " where c.PointRangeCode = '" + pInfo.PointRangeCode + "'" : " and  c.PointRangeCode = '" + pInfo.PointRangeCode + "'";
            }
            if ((pInfo.ProPointCode != null) && (pInfo.ProPointCode != "") && (pInfo.ProPointCode != "-99"))
            {
                strcond += (strcond == "") ? " where p.ProPoint = '" + pInfo.ProPointCode + "'" : " and  p.ProPoint = '" + pInfo.ProPointCode + "'";
            }
            if ((pInfo.CompanyCode != null) && (pInfo.CompanyCode != "") && (pInfo.CompanyCode != "-99"))
            {
                strcond += (strcond == "") ? " where p.CompanyCode = '" + pInfo.CompanyCode + "'" : " and  p.CompanyCode = '" + pInfo.CompanyCode + "'";
            }

            DataTable dt = new DataTable();
            var LPromotion = new List<ReportPointInfo>();


            try
            {
                string strsql = " select COUNT(ca.Id) AS countgv " +
                                " from " + dbName + ".dbo.CustomerActionPointCoupon ca" +
                                " left join OrderInfo as oi on oi.OrderCode = ca.OrderCode " +
                                " left join promotion as p on p.PromotionCode = ca.PromotionCode " +
                                " left join product as pd on pd.ProductCode = ca.ProductCode " +
                                " left join Customer as c on c.CustomerCode = ca.CustomerCode " +
                                " left join lookup as pt on pt.LookupCode = p.PointType and pt.LookupType = 'POINTTYPE' " +
                                " left join lookup as pp on pp.LookupCode = p.ProPoint and pp.LookupType = 'PROPOINT' " +
                                " left join lookup as pa on pa.LookupCode = ca.ActionCode and pa.LookupType = 'POINTACTION' " +
                                " left join Company as com on com.CompanyCode = p.CompanyCode and com.MerchantMapCode = '" + pInfo.MerchantCode + "' " +
                                " left join PointRange as pr on pr.PointCode = c.PointRangeCode and pr.MerchantMapCode = '" + pInfo.MerchantCode + "' "+ strcond;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                              select new ReportPointInfo()
                              {
                                  countgv = Convert.ToInt32(dr["countgv"])
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LPromotion.Count > 0)
            {
                count = LPromotion[0].countgv;
            }

            return count;
        }

        public List<PromotionListReturn> ListPromotionPointListUsed(PromotionInfo pInfo)
            {
                string strcond = "";

                if ((pInfo.PromotionId != null) && (pInfo.PromotionId != 0))
                {
                    strcond += " and  p.Id =" + pInfo.PromotionId;
                }

                if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
                {
                    strcond += " and  p.PromotionCode like '%" + pInfo.PromotionCode + "%'";
                }
                if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != ""))
                {
                    strcond += " and  p.MerchantCode = '" + pInfo.MerchantCode + "'";
                }
                if ((pInfo.PromotionName != null) && (pInfo.PromotionName != ""))
                {
                    strcond += " and  p.PromotionName like '%" + pInfo.PromotionName.Trim() + "%'";
                }
           
                if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.StartDateTo == "") || (pInfo.StartDateTo == null)))
                {
                    strcond += " AND p.StartDate >= CONVERT(DATETIME, '" + pInfo.StartDate + "',103)";
                }
                if (((pInfo.StartDate == "") && (pInfo.StartDate == null)) && ((pInfo.StartDateTo != "") || (pInfo.StartDateTo != null)))
                {
                    strcond += " AND p.StartDate <= CONVERT(DATETIME, '" + pInfo.StartDateTo + "',103)";
                }
                if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.StartDateTo != "") && (pInfo.StartDateTo != null)))
                {
                    strcond += " AND p.StartDate BETWEEN CONVERT(DATETIME, '" + pInfo.StartDate + "',103) AND CONVERT(DATETIME,'" + pInfo.StartDateTo + " 23:59:59:999',103)";
                }
                if (((pInfo.EndDate != "") && (pInfo.EndDate != null)) && ((pInfo.EndDateTo == "") || (pInfo.EndDateTo == null)))
                {
                    strcond += " AND p.EndDate >= CONVERT(DATETIME, '" + pInfo.EndDate + "',103)";
                }
                if (((pInfo.EndDate == "") && (pInfo.EndDate == null)) && ((pInfo.EndDateTo != "") || (pInfo.EndDateTo != null)))
                {
                    strcond += " AND p.EndDate <= CONVERT(DATETIME, '" + pInfo.EndDateTo + "',103)";
                }
                if (((pInfo.EndDate != "") && (pInfo.EndDate != null)) && ((pInfo.EndDateTo != "") && (pInfo.EndDateTo != null)))
                {
                    strcond += " AND p.EndDate BETWEEN CONVERT(DATETIME, '" + pInfo.EndDate + "',103) AND CONVERT(DATETIME,'" + pInfo.EndDateTo + " 23:59:59:999',103)";
                }
                if ((pInfo.Propoint != null) && (pInfo.Propoint != "") && (pInfo.Propoint != "-99"))
                {
                    strcond += " and  p.ProPoint = '" + pInfo.Propoint + "'";
                }
                if ((pInfo.PointType != null) && (pInfo.PointType != "") && (pInfo.PointType != "-99"))
                {
                    strcond += " and  p.PointType = '" + pInfo.PointType + "'";
                }
                if ((pInfo.PointRangeCode != null) && (pInfo.PointRangeCode != "") && (pInfo.PointRangeCode != "-99"))
                {
                    strcond += " and  p.PointRangeCode = '" + pInfo.PointRangeCode + "'";
                }
                if ((pInfo.CompanyCode != null) && (pInfo.CompanyCode != "") && (pInfo.CompanyCode != "-99"))
                {
                    strcond += " and  p.CompanyCode = '" + pInfo.CompanyCode + "'";
                }
                if ((pInfo.FlagPointType != null) && (pInfo.FlagPointType != ""))
                {
                    strcond += " and  p.FlagPointType = '" + pInfo.FlagPointType + "'";
                }
                else
                {
                    strcond += " and  p.FlagPointType is null ";
                }

                DataTable dt = new DataTable();
                var LPromotion = new List<PromotionListReturn>();

                try
                {
                    string strsql = " select p.Id,p.PromotionCode,p.PromotionName,p.DiscountCode,p.StartDate,p.EndDate,p.PointType,p.FlagPatent,p.PatentAmount,p.ProPoint,p.CompanyCode " +
                                    " ,pt.LookupValue as PointTypeName,p.PromotionStatus ,pp.LookupValue as ProPointName ,pr.PointName,p.PointRangeCode" +
                                    " ,cn.CompanyNameEN,count(ca.Id) as PromotionUsed ,case when p.FlagPatent = '01' then p.PatentAmount - count(ca.Id) else 0 end as PromotionRemain from " + dbName + ".dbo.Promotion p " +
                                    " left join LookUp pt on pt.LookupCode =  p.PointType  and pt.LookupType='POINTTYPE' " +
                                    " left join LookUp pp on pp.LookupCode =  p.ProPoint  and pp.LookupType='PROPOINT' " +
                                    " left join CustomerActionPointCoupon ca on ca.PromotionCode = p.PromotionCode and ca.ActionCode <> '03' " +
                                    " left join Orderdetail ot on ot.PromotionCode = p.PromotionCode " +
                                    " left join PointRange pr on pr.PointCode =  p.PointRangeCode and pr.MerchantMapCode = '" + pInfo.MerchantCode + "' " +
                                    " left join Company cn on cn.CompanyCode =  p.CompanyCode and cn.MerchantMapCode = '" + pInfo.MerchantCode + "' " +
                                    " where p.FlagDelete ='N' " + strcond +
                                    " group by p.id,p.promotionCode,p.PromotionName,p.StartDate,p.EndDate,p.PointType,p.FlagPatent,p.ProPoint, p.CompanyCode,pt.LookupValue,pp.LookupValue,pr.PointName,p.PromotionStatus,cn.CompanyNameEN,p.PatentAmount,p.PointRangeCode,p.DiscountCode ";

                    strsql += " ORDER BY p.Id DESC OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";

                    Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                    SqlCommand com = new SqlCommand();
                    com.CommandText = strsql;
                    com.CommandType = System.Data.CommandType.Text;
                    dt = db.ExcuteDataReaderText(com);
                    LPromotion = (from DataRow dr in dt.Rows

                                  select new PromotionListReturn()
                                  {
                                      PromotionId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                      PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                      PromotionName = dr["PromotionName"].ToString().Trim(),
                                      StartDate = dr["StartDate"].ToString(),
                                      EndDate = dr["EndDate"].ToString(),
                                      Propoint = dr["ProPoint"].ToString(),
                                      PropointName = dr["ProPointName"].ToString(),
                                      PointType = dr["PointType"].ToString(),
                                      PointTypeName = dr["PointTypeName"].ToString(),
                                      CompanyCode = dr["CompanyCode"].ToString(),
                                      CompanyNameEN = dr["CompanyNameEN"].ToString(),
                                      FlagPatent = dr["FlagPatent"].ToString(),
                                      PatentAmount = (dr["PatentAmount"].ToString() != "") ? Convert.ToInt32(dr["PatentAmount"]) : 0,
                                      DiscountCode = dr["DiscountCode"].ToString(),
                                      PointRangeCode = dr["PointRangeCode"].ToString(),
                                      PointRangeName = dr["PointName"].ToString(),
                                      PromotionStatusCode = dr["PromotionStatus"].ToString(),
                                      PromotionUsed = (dr["PromotionUsed"].ToString() != "") ? Convert.ToInt32(dr["PromotionUsed"]) : 0,
                                      PromotionRemain = (dr["PromotionRemain"].ToString() != "") ? Convert.ToInt32(dr["PromotionRemain"]) : 0,



                                  }).ToList();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }

                return LPromotion;
            }
        public int? CountListPromotionPointListUsed(PromotionInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.PromotionId != null) && (pInfo.PromotionId != 0))
            {
                strcond += " and  p.Id =" + pInfo.PromotionId;
            }

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  p.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }
            if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != ""))
            {
                strcond += " and  p.MerchantCode = '" + pInfo.MerchantCode + "'";
            }
            if ((pInfo.PromotionName != null) && (pInfo.PromotionName != ""))
            {
                strcond += " and  p.PromotionName like '%" + pInfo.PromotionName.Trim() + "%'";
            }

            if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.StartDateTo == "") || (pInfo.StartDateTo == null)))
            {
                strcond += " AND p.StartDate >= CONVERT(DATETIME, '" + pInfo.StartDate + "',103)";
            }
            if (((pInfo.StartDate == "") && (pInfo.StartDate == null)) && ((pInfo.StartDateTo != "") || (pInfo.StartDateTo != null)))
            {
                strcond += " AND p.StartDate <= CONVERT(DATETIME, '" + pInfo.StartDateTo + "',103)";
            }
            if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.StartDateTo != "") && (pInfo.StartDateTo != null)))
            {
                strcond += " AND p.StartDate BETWEEN CONVERT(DATETIME, '" + pInfo.StartDate + "',103) AND CONVERT(DATETIME,'" + pInfo.StartDateTo + " 23:59:59:999',103)";
            }
            if (((pInfo.EndDate != "") && (pInfo.EndDate != null)) && ((pInfo.EndDateTo == "") || (pInfo.EndDateTo == null)))
            {
                strcond += " AND p.EndDate >= CONVERT(DATETIME, '" + pInfo.EndDate + "',103)";
            }
            if (((pInfo.EndDate == "") && (pInfo.EndDate == null)) && ((pInfo.EndDateTo != "") || (pInfo.EndDateTo != null)))
            {
                strcond += " AND p.EndDate <= CONVERT(DATETIME, '" + pInfo.EndDateTo + "',103)";
            }
            if (((pInfo.EndDate != "") && (pInfo.EndDate != null)) && ((pInfo.EndDateTo != "") && (pInfo.EndDateTo != null)))
            {
                strcond += " AND p.EndDate BETWEEN CONVERT(DATETIME, '" + pInfo.EndDate + "',103) AND CONVERT(DATETIME,'" + pInfo.EndDateTo + " 23:59:59:999',103)";
            }
            if ((pInfo.Propoint != null) && (pInfo.Propoint != "") && (pInfo.Propoint != "-99"))
            {
                strcond += " and  p.ProPoint = '" + pInfo.Propoint + "'";
            }
            if ((pInfo.PointType != null) && (pInfo.PointType != "") && (pInfo.PointType != "-99"))
            {
                strcond += " and  p.PointType = '" + pInfo.PointType + "'";
            }
            if ((pInfo.PointRangeCode != null) && (pInfo.PointRangeCode != "") && (pInfo.PointRangeCode != "-99"))
            {
                strcond += " and  p.PointRangeCode = '" + pInfo.PointRangeCode + "'";
            }
            if ((pInfo.CompanyCode != null) && (pInfo.CompanyCode != "") && (pInfo.CompanyCode != "-99"))
            {
                strcond += " and  p.CompanyCode = '" + pInfo.CompanyCode + "'";
            }
            if ((pInfo.FlagPointType != null) && (pInfo.FlagPointType != ""))
            {
                strcond += " and  p.FlagPointType = '" + pInfo.FlagPointType + "'";
            }
            else
            {
                strcond += " and  p.FlagPointType is null ";
            }
            DataTable dt = new DataTable();
            var LPromotion = new List<PromotionListReturn>();


            try
            {
                    string strsql = " SELECT COUNT(p.Id) AS countPromotion from " + dbName + ".dbo.Promotion p " +
                                    " left join LookUp pt on pt.LookupCode =  p.PointType  and pt.LookupType='POINTTYPE' " +
                                    " left join LookUp pp on pp.LookupCode =  p.ProPoint  and pp.LookupType='PROPOINT' " +
                                    " left join CustomerActionPointCoupon ca on ca.PromotionCode = p.PromotionCode and ca.ActionCode <> '03' " +
                                    " left join Orderdetail ot on ot.PromotionCode = p.PromotionCode " +
                                    " left join PointRange pr on pr.PointCode =  p.PointRangeCode and pr.MerchantMapCode = '" + pInfo.MerchantCode + "' " +
                                    " left join Company cn on cn.CompanyCode =  p.CompanyCode  and cn.MerchantMapCode = '" + pInfo.MerchantCode + "' " +
                                    " where p.FlagDelete ='N' " + strcond;
                

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                              select new PromotionListReturn()
                              {
                                  countPromotion = Convert.ToInt32(dr["countPromotion"])
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LPromotion.Count > 0)
            {
                count = LPromotion[0].countPromotion;
            }

            return count;
        }



    }
}
