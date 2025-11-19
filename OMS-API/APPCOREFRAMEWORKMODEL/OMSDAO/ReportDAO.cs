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
    public class ReportDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        //old name (ListOrderByCriteria) was the same as orderDAO's method, changed into "ListOrderReportByCriteria" instead
        public DataSet ListOrderReportByCriteria(OrderInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderId != null) && (oInfo.OrderId != 0))
            {
                strcond += " and  o.Id =" + oInfo.OrderId;
            }

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != ""))
            {
                strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }
            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != ""))
            {
                strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }
            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            DataTable dt = new DataTable();
            var OrderDetailDs = new DataSet("DataSet1");

            try
            {
                string strsql = @" SELECT        o.Id AS orderId, o.OrderCode, o.OrderStatusCode, o.OrderStateCode, o.BUCode, o.NetPrice, o.TotalPrice, o.Vat, o.CreateDate, o.CreateBy, o.UpdateDate, o.UpdateBy, o.FlagDelete, o.CustomerCode, o.RunningNo, 
                                                         d.Id AS DetailID, d.PromotionCode, d.Price, d.ProductCode, d.PromotionDetailId, d.Unit, d.Vat AS detailVat, d.Amount AS detailAmount, d.NetPrice AS detailNetPrice, p.Id AS paymentId, p.PaymentTypeCode, 
                                                         p.PayAmount, p.Installment, p.InstallmentPrice, p.FirstInstallment, p.CardIssuename, p.CardNo, p.CardType, p.CVCNo, p.CardOwnerName, p.CardExpMonth, p.CardExpYear, p.CitizenId, p.BirthDate, p.BankCode, 
                                                         p.BankBranch, p.AccountName, p.AccountType, p.AccountNo, p.PaymentOtherDetail, t.Id AS TransportID, t.Address, t.Subdistrict, t.District, t.Province, t.Zipcode, t.TransportPrice, t.TransportType, 
                                                         t.CreateDate AS Expr19, t.AddressType, t.TransportTypeOther, os.LookupValue AS OrderStatusName, ost.LookupValue AS OrderStateName, pt.LookupValue AS PaymentTypeName, pv.ProvinceName, 
                                                         sd.SubDistrictName, dt.DistrictName, pv.ProvinceName AS ProvinceName2, b.LookupValue AS BankName, ct.LookupValue AS CardTypeName
                                ,c.CustomerFName,c.CustomerLName,c.Identification_Number,ti.LookupValue TitleName,pd.ProductName,pro.PromotionName,addt.LookupValue addressTypeName

                                FROM            OrderInfo AS o 

                                LEFT OUTER JOIN customer c on o.CustomerCode = c.CustomerCode
                                LEFT OUTER JOIN
                                                             (SELECT        Id, LookupCode, LookupType, LookupValue, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete, LookupDesc
                                                               FROM            Lookup AS l
                                                               WHERE        (LookupType = 'TITLE') AND (FlagDelete = 'N')) AS ti ON ti.LookupCode = c.Title 
                             
                                LEFT OUTER JOIN
                                                         OrderDetail AS d ON o.OrderCode = d.OrderCode
						                                 LEFT OUTER JOIN
                                                         Product AS pd ON d.ProductCode = pd.ProductCode 
						                                 LEFT OUTER JOIN
                                                         Promotion AS pro ON d.PromotionCode = pro.PromotionCode
						                                 LEFT OUTER JOIN
                                                         OrderPayment AS p ON o.OrderCode = p.OrderCode LEFT OUTER JOIN
                                                         OrderTransport AS t ON o.OrderCode = t.OrderCode LEFT OUTER JOIN
                                                             (SELECT        Id, LookupCode, LookupType, LookupValue, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete, LookupDesc
                                                               FROM            Lookup AS l
                                                               WHERE        (LookupType = 'ORDERSTATUS') AND (FlagDelete = 'N')) AS os ON os.LookupCode = o.OrderStatusCode LEFT OUTER JOIN
                                                             (SELECT        Id, LookupCode, LookupType, LookupValue, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete, LookupDesc
                                                               FROM            Lookup AS l
                                                               WHERE        (LookupType = 'ORDERSTATE') AND (FlagDelete = 'N')) AS ost ON ost.LookupCode = o.OrderStateCode LEFT OUTER JOIN
                                                             (SELECT        Id, LookupCode, LookupType, LookupValue, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete, LookupDesc
                                                               FROM            Lookup AS l
                                                               WHERE        (LookupType = 'PAYMENTMETHOD') AND (FlagDelete = 'N')) AS pt ON pt.LookupCode = p.PaymentTypeCode LEFT OUTER JOIN
                                                             (SELECT        Id, ProvinceCode, ProvinceName, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete
                                                               FROM            Province
                                                               WHERE        (FlagDelete = 'N')) AS pv ON t.Province = pv.ProvinceCode LEFT OUTER JOIN
                                                             (SELECT        Id, DistrictCode, DistrictName, ProvinceCode, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete
                                                               FROM            District
                                                               WHERE        (FlagDelete = 'N')) AS dt ON t.District = dt.DistrictCode LEFT OUTER JOIN
                                                             (SELECT        Id, SubDistrictCode, SubDistrictName, DistrictCode, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete
                                                               FROM            SubDistrict
                                                               WHERE        (FlagDelete = 'N')) AS sd ON t.Subdistrict = sd.DistrictCode LEFT OUTER JOIN
                                                             (SELECT        Id, LookupCode, LookupType, LookupValue, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete, LookupDesc
                                                               FROM            Lookup AS l
                                                               WHERE        (LookupType = 'BANK') AND (FlagDelete = 'N')) AS b ON b.LookupCode = p.BankCode LEFT OUTER JOIN
                                                             (SELECT        Id, LookupCode, LookupType, LookupValue, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete, LookupDesc
                                                               FROM            Lookup AS l
                                                               WHERE        (LookupType = 'CARDTYPE') AND (FlagDelete = 'N')) AS ct ON ct.LookupCode = p.CardType LEFT OUTER JOIN
                                                             (SELECT        Id, LookupCode, LookupType, LookupValue, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete, LookupDesc
                                                               FROM            Lookup AS l
                                                               WHERE        (LookupType = 'ACCOUNTTYPE') AND (FlagDelete = 'N')) AS at ON at.LookupCode = p.AccountType
							                                    LEFT OUTER JOIN
                                                             (SELECT        Id, LookupCode, LookupType, LookupValue, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete, LookupDesc
                                                               FROM            Lookup AS l
                                                               WHERE        (LookupType = 'ADDRESSTYPE') AND (FlagDelete = 'N')) AS addt ON addt.LookupCode = t.AddressType
                                 where 1=1 " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(OrderDetailDs, "DataTable1");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return OrderDetailDs;
        }

        public List<OrderListReturn> ListReportOrderByCriteria(OrderInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderId != null) && (oInfo.OrderId != 0))
            {
                strcond += " and  o.Id =" + oInfo.OrderId;
            }

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }
            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != ""))
            {
                strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if (((oInfo.CreateDateFrom != "") && (oInfo.CreateDateFrom != null)) && ((oInfo.CreateDateTo != "") && (oInfo.CreateDateTo != null)))
            {
                strcond += " and  o.CreateDate BETWEEN CONVERT(VARCHAR, '" + oInfo.CreateDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + oInfo.CreateDateTo + "', 103)),'23:59:59')";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {

                string strsql = " select o.Id,o.OrderCode,ls.LookupValue as OrderStatusName,c.CustomerCode,c.CustomerFName, c.CustomerLName,CONVERT(varchar(10), o.CreateDate, 103) AS CreateDate from OrderInfo o " +
                                " inner join Customer c on o.CustomerCode = c.CustomerCode " +
                                " inner join Lookup ls on ls.LookupCode = o.OrderStatusCode and ls.LookupType= 'ORDERSTATUS' " + strcond;

                strsql += " ORDER BY o.CreateDate DESC ";// + " OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              OrderId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                              CustomerCode = dr["CustomerCode"].ToString().Trim(),
                              CustomerFName = dr["CustomerFName"].ToString().Trim(),
                              CustomerLName = dr["CustomerLName"].ToString().Trim(),
                              CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                              CreateDate = dr["CreateDate"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public int? CountListReportOrderByCriteria(OrderInfo oInfo)
        {
            int? count = 0;
            string strcond = "";

            if ((oInfo.OrderId != null) && (oInfo.OrderId != 0))
            {
                strcond += " and  o.Id =" + oInfo.OrderId;
            }

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }
            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != ""))
            {
                strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if (((oInfo.CreateDateFrom != "") && (oInfo.CreateDateFrom != null)) && ((oInfo.CreateDateTo != "") && (oInfo.CreateDateTo != null)))
            {
                strcond += " and  o.CreateDate BETWEEN CONVERT(VARCHAR, '" + oInfo.CreateDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + oInfo.CreateDateTo + "', 103)),'23:59:59')";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();


            try
            {
                string strsql = " select count(o.Id) as countOrder from OrderInfo o " +
                                " inner join Customer c on o.CustomerCode = c.CustomerCode " +
                                " inner join Lookup ls on ls.LookupCode = o.OrderStatusCode and ls.LookupType= 'ORDERSTATUS' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              countOrder = Convert.ToInt32(dr["countOrder"])
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LOrder.Count > 0)
            {
                count = LOrder[0].countOrder;
            }

            return count;
        }

        public List<OrderListReturn> ListReportTransactionOrderByCriteria(OrderInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderId != null) && (oInfo.OrderId != 0))
            {
                strcond += " and  o.Id =" + oInfo.OrderId;
            }

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }
            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != ""))
            {
                strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if (((oInfo.CreateDateFrom != "") && (oInfo.CreateDateFrom != null)) && ((oInfo.CreateDateTo != "") && (oInfo.CreateDateTo != null)))
            {
                strcond += " and  o.CreateDate BETWEEN CONVERT(VARCHAR, '" + oInfo.CreateDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + oInfo.CreateDateTo + "', 103)),'23:59:59')";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {

                string strsql = " select o.Id,o.OrderCode,pm.PromotionName,p.ProductName,od.Amount,od.TotalPrice,ls.LookupValue as OrderStatusName,c.CustomerCode,c.CustomerFName,c.CustomerLName,CONVERT (varchar(10), o.CreateDate, 103) AS CreateDate from OrderInfo o " +
                                " inner join Customer c on o.CustomerCode = c.CustomerCode " +
                                " inner join OrderDetail  od on o.OrderCode = od.OrderCode " +
                                " inner join Product p on od.ProductCode = p.ProductCode and p.FlagDelete ='N' " +
                                " inner join Promotion  pm on pm.PromotionCode = od.PromotionCode and pm.FlagDelete ='N' " +
                                " inner join Lookup ls on ls.LookupCode = o.OrderStatusCode and ls.LookupType= 'ORDERSTATUS' " + strcond;

                strsql += " ORDER BY o.CreateDate DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              OrderId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                              CustomerCode = dr["CustomerCode"].ToString().Trim(),
                              CustomerFName = dr["CustomerFName"].ToString().Trim(),
                              CustomerLName = dr["CustomerLName"].ToString().Trim(),
                              CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                              CreateDate = dr["CreateDate"].ToString(),
                              PromotionName = dr["PromotionName"].ToString(),
                              ProductName = dr["ProductName"].ToString(),
                              Amount = dr["Amount"].ToString(),
                              TotalPrice = dr["TotalPrice"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public int? CountListReportTransactionOrderByCriteria(OrderInfo oInfo)
        {
            int? count = 0;
            string strcond = "";

            if ((oInfo.OrderId != null) && (oInfo.OrderId != 0))
            {
                strcond += " and  o.Id =" + oInfo.OrderId;
            }

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }
            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != ""))
            {
                strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if (((oInfo.CreateDateFrom != "") && (oInfo.CreateDateFrom != null)) && ((oInfo.CreateDateTo != "") && (oInfo.CreateDateTo != null)))
            {
                strcond += " and  o.CreateDate BETWEEN CONVERT(VARCHAR, '" + oInfo.CreateDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + oInfo.CreateDateTo + "', 103)),'23:59:59')";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();


            try
            {
                string strsql = " select count(o.Id) as countOrder from OrderInfo o " +
                                " inner join Customer c on o.CustomerCode = c.CustomerCode " +
                                " inner join OrderDetail  od on o.OrderCode = od.OrderCode " +
                                " inner join Product p on od.ProductCode = p.ProductCode and p.FlagDelete ='N' " +
                                " inner join Promotion  pm on pm.PromotionCode = od.PromotionCode and pm.FlagDelete ='N' " +
                                " inner join Lookup ls on ls.LookupCode = o.OrderStatusCode and ls.LookupType= 'ORDERSTATUS' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              countOrder = Convert.ToInt32(dr["countOrder"])
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LOrder.Count > 0)
            {
                count = LOrder[0].countOrder;
            }

            return count;
        }

        public List<ReportSaleAstonInfo> ReaportSaleAston(OrderOLInfo oInfo)
        {
            #region condition

            string strcond = "";
            string strnumpage = "";
            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
            }

            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo == null) || (oInfo.DeliveryDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)" : strcond += " AND o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";
            }

            if (((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")) && ((oInfo.DeliveryDate == null) || (oInfo.DeliveryDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)" : strcond += " AND o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";
            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcond += " and  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'";
            }

            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            #endregion

            var LOrder = new List<ReportSaleAstonInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " select o.ordercode,FORMAT (o.CreateDate, 'dd/MM/yyyy') as Saledate "
                    + " , o.CreateBy ,e.EmpFname_TH + ' ' + e.EmpLName_TH as Sale_Name,cpc.CamCate_name "
                    + " , cust.CustomerFName + ' ' + cust.CustomerLName as Customer_Name,cust.Mobile as Customer_Moblie  "
                     + " ,(case when(select top(1) ptt.ProductName from OrderInfo ott inner join OrderDetail oltt on oltt.OrderCode = ott.OrderCode "
                    + " inner join Product ptt on ptt.ProductCode = oltt.ProductCode "
                    + " where (CONVERT(datetime2, ott.CreateDate) < (select CONVERT(datetime2, oaa.CreateDate) as createdate "
                    + " from OrderInfo oaa where oaa.OrderCode = o.OrderCode )) and ott.CustomerCode = o.CustomerCode order by ott.OrderCode desc) is not null "
                    + " then(select top(1) ptt.ProductName from OrderInfo ott inner "
                     + " join OrderDetail oltt on oltt.OrderCode = ott.OrderCode "
                     + " inner join Product ptt on ptt.ProductCode = oltt.ProductCode "
                    + " where (CONVERT(datetime2, ott.CreateDate) < (select CONVERT(datetime2, oaa.CreateDate) as createdate "
                    + " from OrderInfo oaa where oaa.OrderCode = o.OrderCode )) "
                    + "and ott.CustomerCode = o.CustomerCode "
                    + "order by ott.OrderCode desc) "

                    + "else (select top(1) pmt.PromotionName from OrderInfo ott "
                    + "inner join OrderDetail oltt on oltt.OrderCode = ott.OrderCode "
                    + "inner join Promotion pmt on pmt.PromotionCode = oltt.PromotionCode "
                    + "where(CONVERT(datetime2, ott.CreateDate) < (select  CONVERT(datetime2, oaa.CreateDate) as createdate "
                    + "from OrderInfo oaa where oaa.OrderCode = o.OrderCode )) "
                    + "and ott.CustomerCode = o.CustomerCode order by ott.OrderCode desc) "
                    + "end "
                    + ") as OrderLastProduct,(select top(1) pmt.PromotionName from OrderInfo ott "
                    + "inner join OrderDetail oltt on oltt.OrderCode = ott.OrderCode "
                    + "inner join Promotion pmt on pmt.PromotionCode = oltt.PromotionCode "
                    + "where(CONVERT(datetime2, ott.CreateDate) < (select  CONVERT(datetime2, oaa.CreateDate) as createdate "
                    + " from OrderInfo oaa where oaa.OrderCode = o.OrderCode )) "
                    + " and ott.CustomerCode = o.CustomerCode order by ott.OrderCode desc ) as PromotionLastOrder "
                    + " ,(select top(1) cad.address + ' ' + subd.SubDistrictName + ' ' + d.DistrictName + ' ' + p.ProvinceName "
                    + " from CustomerAddressDetail cad "
                    + " inner join OrderInfo ocad on cad.CustomerCode = ocad.CustomerCode "
                    + " inner join SubDistrict subd on subd.SubDistrictCode = cad.subdistrict "
                    + " inner join District d on d.DistrictCode = cad.district "
                    + " inner join Province p  on p.ProvinceCode = cad.province "
                    + " where ocad.CustomerCode = o.CustomerCode) as AddressCustomer "
                    + " ,(select top(1) FORMAT(ott.CreateDate, 'dd/MM/yyyy') from OrderInfo ott "
                    + " inner join OrderDetail oltt on oltt.OrderCode = ott.OrderCode "
                    + " inner join Product ptt on ptt.ProductCode = oltt.ProductCode "
                    + " where (CONVERT(datetime2, ott.CreateDate) < (select CONVERT(datetime2, oaa.CreateDate) as createdate "
                    + " from OrderInfo oaa where oaa.OrderCode = o.OrderCode )) and ott.CustomerCode = o.CustomerCode "
                    + " order by ott.OrderCode desc ) as SaleDateLastOrder "
                    + " ,o.TotalPrice, "
                    + "                 case when(p.ProductName ) is not null then(p.ProductName) else promocurrent.PromotionName end as product_Current "
                    + " , ol.PromotionCode,promocurrent.PromotionName,o.OrderStatusCode,LStatus.LookupValue as orderstatusname,o.OrderTracking "
                    + " , o.OrderStateCode,LState.LookupValue as orderstatename,FORMAT (o.DeliveryDate, 'dd/MM/yyyy') as DeliveryDateSale,o.OrderNote,o.OrderNote "
                    + "                     from OrderInfo o join(    select * from( select *, row_number() over( partition by OrderCode "
                    + "            order by OrderCode desc ) as row_num from OrderDetail    ) as ol1 "
                    + "     where ol1.row_num = 1) as ol "
                    + " on o.OrderCode = ol.OrderCode "
                    + " left join CampaignCategory cpc  on cpc.CampaignCategoryCode = o.CampaignCategoryCode "
                    + " left join Customer cust  on cust.CustomerCode = o.CustomerCode "
                    + " left join Product p on p.ProductCode = ol.ProductCode "
                    + " left join Emp e on o.CreateBy = e.EmpCode "
                    + " inner join Lookup LStatus on LStatus.LookupCode = o.OrderStatusCode and LStatus.LookupType = 'ORDERSTATUS' "
                    + " inner join Lookup LState on LState.LookupCode = o.OrderStateCode and LState.LookupType = 'ORDERSTATE' "

                    + " inner join Promotion promocurrent on promocurrent.PromotionCode = ol.PromotionCode "
                     + strcond
                    + " order by o.OrderCode desc";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new ReportSaleAstonInfo()
                          {
                              ordercode = dr["ordercode"].ToString().Trim(),
                              Saledate = dr["Saledate"].ToString().Trim(),
                              Sale_Name = dr["Sale_Name"].ToString().Trim(),
                              CamCate_name = dr["CamCate_name"].ToString().Trim(),
                              Customer_Name = dr["CamCate_name"].ToString().Trim(),
                              Customer_Moblie = dr["Customer_Moblie"].ToString().Trim(),
                              OrderLastProduct = dr["OrderLastProduct"].ToString().Trim(),
                              PromotionLastOrder = dr["PromotionLastOrder"].ToString().Trim(),
                              AddressCustomer = dr["AddressCustomer"].ToString().Trim(),
                              SaleDateLastOrder = dr["SaleDateLastOrder"].ToString().Trim(),
                              product_Current = dr["product_Current"].ToString().Trim(),
                              PromotionCode = dr["PromotionCode"].ToString().Trim(),
                              PromotionName = dr["PromotionName"].ToString().Trim(),
                              OrderStatusCode = dr["OrderStatusCode"].ToString().Trim(),
                              orderstatusname = dr["orderstatusname"].ToString().Trim(),
                              OrderTracking = dr["OrderTracking"].ToString().Trim(),
                              OrderStateCode = dr["OrderStateCode"].ToString().Trim(),
                              orderstatename = dr["orderstatename"].ToString().Trim(),
                              DeliveryDateSale = dr["DeliveryDateSale"].ToString().Trim(),
                              OrderNote = dr["OrderNote"].ToString().Trim()
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public List<ReportSaleOrderInfo> ReaportSaleOrder(OrderOLInfo oInfo)
        {
            #region condition

            string strcond = "";
            string strnumpage = "";
            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
            }

            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo == null) || (oInfo.DeliveryDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)" : strcond += " AND o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";
            }

            if (((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")) && ((oInfo.DeliveryDate == null) || (oInfo.DeliveryDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)" : strcond += " AND o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";
            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcond += " and  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'";
            }

            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
            }
            if ((oInfo.SALE_CODE != null) && (oInfo.SALE_CODE != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.SALE_CODE + "'" : strcond += " and  o.CreateBy = '" + oInfo.SALE_CODE + "'";
            }
            if ((oInfo.SALE_FNAME != null) && (oInfo.SALE_FNAME != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  e.EmpFname_TH  like '%" + oInfo.SALE_FNAME + "%'" : strcond += " and  e.EmpFname_TH like '%" + oInfo.SALE_FNAME + "%'";
            }
            if ((oInfo.SALE_LNAME != null) && (oInfo.SALE_LNAME != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  e.EmpLName_TH like '%" + oInfo.SALE_LNAME + "%'" : strcond += " and  e.EmpLName_TH like '%" + oInfo.SALE_LNAME + "%'";
            }
            if ((oInfo.MerchantMapCode != null) && (oInfo.MerchantMapCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.MerchantMapCode = '" + oInfo.MerchantMapCode + "'" : strcond += " and  o.MerchantMapCode = '" + oInfo.MerchantMapCode + "'";
            }

            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            #endregion

            var LOrder = new List<ReportSaleOrderInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " select e.EmpCode,e.EmpFname_TH,e.EmpLName_TH,o.OrderCode,CONVERT(varchar, o.CreateDate,103) CreateDate,p.ProductCode,p.ProductName+'('+pmt.PromotionName+')' as ProductName,o.CampaignCategoryCode,"
                    + " cpc.CamCate_name,ch.ChannelName,od.Amount,od.Price,od.ProductDiscountPercent,od.ProductDiscountAmount,od.TotalPrice,op.PaymentTypeCode "
                    + ",cust.CustomerCode ,cust.CustomerFName+' '+cust.CustomerLName as customername,cust.Mobile"
                    + ",CAD.address + sd.SubDistrictName + d.DistrictName + pro.ProvinceName as customeraddress ,CAD.zipcode"
                    + ",CAD2.address + sd2.SubDistrictName + d2.DistrictName + pro2.ProvinceName as customeraddresstax ,CAD2.zipcode as zipcode2"
                    + ",cust.TaxID,o.DeliveryDate"
                    + " ,o.OrderNote,o.Transport,o.Vat,o.TransportPrice,o.PercentVat , od.TotalPrice - o.Vat + o.TransportPrice as FinalPrice,o.OrderTracking"
                    + " ,lstatus.LookupValue as OrderStatusname,lstatate.LookupValue as OrderStatatename,pm.PaymentTypeName,o.UpdateDate "
                    + " ,eupdate.EmpFname_TH as userupdateFname,eupdate.EmpLName_TH as userupdateLname "

                    + " from OrderInfo o " +
                    " inner join emp e on e.EmpCode =o.CreateBy" +
                    " inner join emp eupdate on eupdate.EmpCode =o.UpdateBy" +
                    " inner join OrderDetail od on od.OrderCode = o.OrderCode" +
                    " inner join Product p on p.ProductCode = od.ProductCode" +
                    " left join CampaignCategory cpc on cpc.CampaignCategoryCode =o.CampaignCategoryCode" +
                    " left join Channel ch on ch.ChannelCode = o.ChannelCode" +
                    " left join OrderPayment op on op.OrderCode = o.OrderCode" +
                    " inner join Promotion pmt on od.PromotionCode = pmt.PromotionCode" +
                    " inner join Customer cust on o.CustomerCode = cust.CustomerCode" +
                    " inner join CustomerAddressDetail CAD on CAD.CustomerCode = cust.CustomerCode and CAD.AddressType = '01'" +
                    " left join SubDistrict sd on CAD.subdistrict = sd.SubDistrictCode and CAD.AddressType = '01'" +
                    " inner join District d on CAD.district = d.DistrictCode and CAD.AddressType = '01'" +
                    " inner join Province pro on CAD.province = pro.ProvinceCode and CAD.AddressType = '01'" +
                    " left join CustomerAddressDetail CAD2 on CAD2.CustomerCode = cust.CustomerCode and CAD2.AddressType = '02'" +
                    " left join SubDistrict sd2 on CAD2.subdistrict = sd2.SubDistrictCode and CAD2.AddressType = '02'" +
                    " left join District d2 on CAD2.district = d2.DistrictCode and CAD2.AddressType = '02'" +
                    " left join Province pro2 on CAD2.province = pro2.ProvinceCode and CAD2.AddressType = '02'" +
                    " left join Lookup lstatus on lstatus.LookupCode = o.OrderStatusCode and lstatus.LookupType='ORDERSTATUS'" +
                    " inner join Lookup lstatate on lstatate.LookupCode = o.OrderStateCode and lstatate.LookupType='ORDERSTATE'" +
                    " inner join PaymentType pm on pm.PaymentTypeCode=op.PaymentTypeCode "
                     + strcond
                    + " order by o.CreateDate desc " +
                    strnumpage +
                    " ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new ReportSaleOrderInfo()
                          {
                              Order_Code = dr["ordercode"].ToString().Trim(),
                              SALE_CODE = dr["EmpCode"].ToString().Trim(),
                              SALE_NAME = dr["EmpFname_TH"].ToString().Trim() + " " + dr["EmpLname_TH"].ToString().Trim(),
                              ORDER_NO = dr["ordercode"].ToString().Trim(),
                              ORDER_DATE = dr["CreateDate"].ToString().Trim(),

                              ORDER_NOTE = dr["OrderNote"].ToString().Trim(),
                              ORDER_TRANS = dr["Transport"].ToString().Trim(),
                              ORDER_TRANSPRICE = dr["TransportPrice"].ToString().Trim(),
                              ORDER_VAT = dr["Vat"].ToString().Trim(),
                              PER_VAT = dr["PercentVat"].ToString().Trim(),
                              FINAL_PRICE = dr["FinalPrice"].ToString().Trim(),
                              ORDER_TRACK = dr["OrderTracking"].ToString().Trim(),


                              CODE_PRODUCT = dr["ProductCode"].ToString().Trim(),
                              PRODUCT_NAME = dr["ProductName"].ToString().Trim(),
                              BRAND = dr["CamCate_name"].ToString().Trim(),

                              CHANNEL = dr["ChannelName"].ToString().Trim(),
                              AMOUNT = dr["Amount"].ToString().Trim(),
                              PRICE = dr["Price"].ToString().Trim(),
                              DISC_PERCENT = dr["ProductDiscountPercent"].ToString().Trim(),
                              DISC_THB = dr["ProductDiscountAmount"].ToString().Trim(),
                              TOTAL_PRICE = dr["TotalPrice"].ToString().Trim(),
                              ORDER_STATUS = dr["OrderStatusname"].ToString().Trim(),
                              FULFILL_STATUS = dr["OrderStatatename"].ToString().Trim(),
                              PAYMENT_TERM = dr["PaymentTypeName"].ToString().Trim(),
                              LAST_UPDATE = dr["UpdateDate"].ToString().Trim(),
                              LAST_UPDATE_BY = dr["userupdateFname"].ToString().Trim() + " " + dr["userupdateLname"].ToString().Trim(),

                              CUS_CODE = dr["CustomerCode"].ToString().Trim(),
                              CUS_NAME = dr["customername"].ToString().Trim(),
                              CUS_MOBILE = dr["Mobile"].ToString().Trim(),
                              CUS_ADD = dr["customeraddress"].ToString().Trim(),
                              CUS_POSTCODE = dr["zipcode"].ToString().Trim(),
                              CUS_ADD2 = dr["customeraddresstax"].ToString().Trim(),
                              CUS_POSTCODE2 = dr["zipcode2"].ToString().Trim(),
                              CUS_TAXID = dr["TaxID"].ToString().Trim(),
                              DELIVER_DATE = dr["DeliveryDate"].ToString().Trim(),

                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public int? CountListReaportSaleOrderByCriteria(OrderOLInfo oInfo)
        {
            #region condition
            int? count = 0;
            string strcond = "";
            string strnumpage = "";
            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
            }

            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo == null) || (oInfo.DeliveryDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)" : strcond += " AND o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";
            }

            if (((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")) && ((oInfo.DeliveryDate == null) || (oInfo.DeliveryDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)" : strcond += " AND o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";
            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcond += " and  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'";
            }

            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
            }

            if ((oInfo.SALE_CODE != null) && (oInfo.SALE_CODE != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.SALE_CODE + "'" : strcond += " and  o.CreateBy = '" + oInfo.SALE_CODE + "'";
            }
            if ((oInfo.SALE_FNAME != null) && (oInfo.SALE_FNAME != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  e.EmpFname_TH  like '%" + oInfo.SALE_FNAME + "%'" : strcond += " and  e.EmpFname_TH like '%" + oInfo.SALE_FNAME + "%'";
            }
            if ((oInfo.SALE_LNAME != null) && (oInfo.SALE_LNAME != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  e.EmpLName_TH like '%" + oInfo.SALE_LNAME + "%'" : strcond += " and  e.EmpLName_TH like '%" + oInfo.SALE_LNAME + "%'";
            }
            #endregion

            DataTable dt = new DataTable();
            var LOrder = new List<CountReportSaleOrderInfo>();


            try
            {
                string strsql = "SELECT count(d.ProductCode) as countOrder"

                      + " from OrderDetail AS d " +
                      " LEFT OUTER JOIN OMS_ASTON_DEV.dbo.OrderInfo AS o ON o.OrderCode = d.OrderCode" +
                      " LEFT OUTER JOIN OMS_ASTON_DEV.dbo.Product AS p ON p.ProductCode = d.ProductCode and p.FlagDelete='N'" +
                      " LEFT OUTER JOIN  Promotion AS pr ON pr.PromotionCode = d.PromotionCode and pr.FlagDelete='N'  " +
                      " inner  join (SELECT  p0.ProductCode,sum(od0.Price) sumprice,sum(od0.Amount) sumqty  FROM    Product AS p0 " +
                      " inner join OrderDetail od0 on od0.ProductCode =p0.ProductCode " +
                      " inner join orderinfo o0 on od0.OrderCode = o0.OrderCode " +
                      " GROUP BY p0.ProductCode ) AS o00 ON o00.ProductCode =d.ProductCode" +
                      " GROUP BY  d.ProductCode, p.ProductName,o00.sumprice,o00.sumqty  ORDER BY o00.sumprice desc" +
                      " inner join Lookup lstatate on lstatate.LookupCode = o.OrderStateCode and lstatate.LookupType='ORDERSTATE'" +
                      " inner join PaymentType pm on pm.PaymentTypeCode=op.PaymentTypeCode "
                       + strcond
                      + " ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new CountReportSaleOrderInfo()
                          {
                              CountReportSaleOrder = Convert.ToInt32(dr["countOrder"])
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LOrder.Count > 0)
            {
                count = LOrder[0].CountReportSaleOrder;
            }

            return count;
        }

        public int? NewCountReaportSaleOrderByCriteria(MonthlyInfo oInfo)
        {
            #region condition
            int? count = 0;
            string strcond = "";
            string strnumpage = "";

            if ((oInfo.MerchantCode != null) && (oInfo.MerchantCode != ""))
            {
                strcond += " and  o.MerchantMapCode = '" + oInfo.MerchantCode + "'";
            }

            #endregion

            DataTable dt = new DataTable();
            var LOrder = new List<CountReportSaleOrderInfo>();


            try
            {
                string strsql = "SELECT count(d.ProductCode) as countOrder"

                      + " from OrderDetail AS d " +
                      " LEFT OUTER JOIN OrderInfo AS o ON o.OrderCode = d.OrderCode" +
                      " LEFT OUTER JOIN Product AS p ON p.ProductCode = d.ProductCode and p.FlagDelete='N'" +
                      " LEFT OUTER JOIN  Promotion AS pr ON pr.PromotionCode = d.PromotionCode and pr.FlagDelete='N'  " +
                      " inner  join (SELECT  p0.ProductCode,sum(od0.Price) sumprice,sum(od0.Amount) sumqty  FROM    Product AS p0 " +
                      " inner join OrderDetail od0 on od0.ProductCode =p0.ProductCode " +
                      " inner join orderinfo o0 on od0.OrderCode = o0.OrderCode " +
                      " GROUP BY p0.ProductCode ) AS o00 ON o00.ProductCode =d.ProductCode" +

                      " Where 1=1 " + strcond +

                      " GROUP BY  d.ProductCode, p.ProductName,o00.sumprice,o00.sumqty  ORDER BY o00.sumprice desc "                         
                      + " ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new CountReportSaleOrderInfo()
                          {
                              CountReportSaleOrder = Convert.ToInt32(dr["countOrder"])
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LOrder.Count > 0)
            {
                count = LOrder[0].CountReportSaleOrder;
            }

            return count;
        }
        public List<ReportSaleOrderAmountInfo> NewSaleorder(MonthlyInfo suminfo)
        {
            string strcond = "";
            string Substrcond = "";

            if ((suminfo.MerchantCode != null) && (suminfo.MerchantCode != ""))
            {
                strcond += " and  o.MerchantMapCode = '" + suminfo.MerchantCode + "'";
            }

            DataTable dt = new DataTable();
            var LProduuct = new List<ReportSaleOrderAmountInfo>();

            try
            {
                string strsql = "SELECT o.MerchantMapCode, m.MerchantName, d.ProductCode, p.ProductName ,o00.sumprice,o00.sumqty" +

                   " from " + dbName + ".dbo.OrderDetail AS d LEFT OUTER JOIN" +
                   " " + dbName + ".dbo.OrderInfo AS o ON o.OrderCode = d.OrderCode LEFT OUTER JOIN" +
                   " " + dbName + ".dbo.Product AS p ON p.ProductCode = d.ProductCode and p.FlagDelete='N' LEFT OUTER JOIN" +
                   " " + dbName + ".dbo.Merchant AS m ON m.MerchantCode = o.MerchantMapCode AND m.FlagDelete = 'N' LEFT OUTER JOIN " +
                   "  Promotion AS pr ON pr.PromotionCode = d.PromotionCode and pr.FlagDelete='N'" +
                   "  inner  join (SELECT  p0.ProductCode,sum(od0.Price) sumprice,sum(od0.Amount) sumqty" +
                   "  FROM    Product AS p0" +
                   "   inner join OrderDetail od0 on od0.ProductCode =p0.ProductCode" +
                   " inner join orderinfo o0 on od0.OrderCode = o0.OrderCode" +
                   
                   "      GROUP BY p0.ProductCode ) AS o00 ON o00.ProductCode =d.ProductCode" +
                   " " +
                   " " +



                                " where d.FlagProSetHeader !='Y' " 
                                
                                ;


                strsql += strcond + " GROUP BY o.MerchantMapCode, m.MerchantName, d.ProductCode, p.ProductName,o00.sumprice,o00.sumqty  ORDER BY o00.sumprice desc";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduuct = (from DataRow dr in dt.Rows

                             select new ReportSaleOrderAmountInfo()
                             {
                                 MerchantCode = dr["MerchantMapCode"].ToString().Trim(),
                                 MerchantName = dr["MerchantName"].ToString().Trim(),
                                 ProductCode = dr["ProductCode"].ToString().Trim(),
                                 ProductName = dr["ProductName"].ToString().Trim(),
                                 Quanlity = dr["sumqty"].ToString().Trim(),
                                 Amount = dr["sumprice"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LProduuct;
        }
        public List<ReportResultSOSAInfo> ReaportResultSOSA(OrderOLInfo oInfo)
        {
            #region condition
            string strcond = "";
            string strcondSub = "";
            string strnumpage = "";
            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.OrderCode like '%" + oInfo.OrderCode + "%'" : strcondSub += " AND  o2.OrderCode like '%" + oInfo.OrderCode + "%'";

            }
            if ((oInfo.SALE_CODE != null) && (oInfo.SALE_CODE != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  e.EmpCode like '%" + oInfo.SALE_CODE + "%'" : strcond += " AND  e.EmpCode like '%" + oInfo.SALE_CODE + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  e2.EmpCode like '%" + oInfo.SALE_CODE + "%'" : strcondSub += " AND  e2.EmpCode like '%" + oInfo.SALE_CODE + "%'";

            }
            if ((oInfo.SALE_FNAME != null) && (oInfo.SALE_FNAME != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE e.EmpFname_TH like '%" + oInfo.SALE_FNAME + "%'" : strcond += " AND  e.EmpFname_TH like '%" + oInfo.SALE_FNAME + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  e2.EmpFname_TH like '%" + oInfo.SALE_FNAME + "%'" : strcondSub += " AND  e2.EmpFname_TH like '%" + oInfo.SALE_FNAME + "%'";

            }
            if ((oInfo.SALE_LNAME != null) && (oInfo.SALE_LNAME != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE e.EmpLname_TH like '%" + oInfo.SALE_LNAME + "%'" : strcond += " AND  e.EmpLname_TH like '%" + oInfo.SALE_LNAME + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  e2.EmpLname_TH like '%" + oInfo.SALE_LNAME + "%'" : strcondSub += " AND  e2.EmpLname_TH like '%" + oInfo.SALE_LNAME + "%'";

            }
            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcondSub += " and  o2.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";

            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcondSub += " AND o2.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";

            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcondSub += " AND o2.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";

            }

            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcondSub += " AND o2.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";

            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";

                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcondSub += " AND o2.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo == null) || (oInfo.DeliveryDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)" : strcond += " AND o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)" : strcondSub += " AND o2.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";

            }

            if (((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")) && ((oInfo.DeliveryDate == null) || (oInfo.DeliveryDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)" : strcond += " AND o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)" : strcondSub += " AND o2.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";

            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o.C2ustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o2.CustomerCode like '%" + oInfo.CustomerCode + "%'";

            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  c2.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcondSub += " and  c2.CustomerFName like '%" + oInfo.CustomerFName + "%'";

            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  c2.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcondSub += " and  c2.CustomerLName like '%" + oInfo.CustomerLName + "%'";

            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcond += " and  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'";
                strcondSub = strcondSub == "" ? strcond += " WHERE  cp2.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcondSub += " and  cp2.PhoneNumber like '%" + oInfo.CustomerContact + "%'";

            }

            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.ChannelCode = '" + oInfo.ChannelCode + "'" : strcondSub += " and  o2.ChannelCode = '" + oInfo.ChannelCode + "'";

            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcondSub += " and  o2.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";

            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcondSub += " and  o2.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";

            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcondSub += " and  o2.OrderStateCode = '" + oInfo.OrderStateCode + "'";

            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.CreateBy = '" + oInfo.CreateBy + "'" : strcondSub += " and  o2.CreateBy = '" + oInfo.CreateBy + "'";

            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.FlagApproved = '" + oInfo.FlagApproved + "'" : strcondSub += " and  o2.FlagApproved = '" + oInfo.FlagApproved + "'";

            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            #endregion

            var LOrder = new List<ReportResultSOSAInfo>();
            DataTable dt = new DataTable();


            try
            {


                string strsql = " select Main.EmpCode ,Main.EmpFname_TH,Main.EmpLName_TH,Main.CamCate_name,Main.ChannelName,Main.ORDER_DATE,"
                   + " Main.TOTAL_PRICE, Main.OrderStateCode,Main.OrderStatusCode, Main.OrderStatusname, Main.OrderStatatename "

                   + ",Main.num_of_amount ,sub.num_of_order,sub.TOTAL_PRICE2"

                   + " from (select e.EmpCode ,e.EmpFname_TH,e.EmpLName_TH,cpc.CamCate_name,ch.ChannelName,CONVERT(varchar, o.CreateDate,103) as ORDER_DATE, " +
                   " sum(o.TotalPrice) as TOTAL_PRICE, o.OrderStateCode,o.OrderStatusCode,lstatus.LookupValue as OrderStatusname" +
                   " ,lstatate.LookupValue as OrderStatatename,count(od.Id) as num_of_amount " +
                   " from OrderInfo o inner join emp e on e.EmpCode =o.CreateBy  " +
                   " inner join OrderDetail od on od.OrderCode = o.OrderCode" +
                   " inner join CampaignCategory cpc on cpc.CampaignCategoryCode =o.CampaignCategoryCode" +
                   " inner join Channel ch on ch.ChannelCode = o.ChannelCode" +
                   " inner join Lookup lstatus on lstatus.LookupCode = o.OrderStatusCode and lstatus.LookupType='ORDERSTATUS'" +
                   " inner join Lookup lstatate on lstatate.LookupCode = o.OrderStateCode and lstatate.LookupType='ORDERSTATE'" +
                    strcond

                   + " group by e.EmpCode,e.EmpFname_TH,e.EmpLName_TH,cpc.CamCate_name,ch.ChannelName " +
                   " ,lstatus.LookupValue,lstatate.LookupValue ,CONVERT(varchar, o.CreateDate,103), o.OrderStateCode, o.OrderStatusCode  ) Main " +
                   "" +
                   // table2
                   " INNER JOIN (select count(o2.Id) as num_of_order,sum(o2.TotalPrice) as TOTAL_PRICE2 ,e2.EmpCode ,CONVERT(varchar, o2.CreateDate,103) as ORDER_DATE," +
                   " lstatus2.LookupValue as OrderStatusname,lstatate2.LookupValue as OrderStatatename, o2.OrderStateCode,o2.OrderStatusCode" +
                   " from OrderInfo o2 inner join emp e2 on e2.EmpCode =o2.CreateBy" +
                   " inner join Lookup lstatus2 on lstatus2.LookupCode = o2.OrderStatusCode and lstatus2.LookupType='ORDERSTATUS'" +
                   " inner join Lookup lstatate2 on lstatate2.LookupCode = o2.OrderStateCode and lstatate2.LookupType='ORDERSTATE' " +
                   strcondSub
                   + " group by e2.EmpCode,CONVERT(varchar, o2.CreateDate,103),lstatus2.LookupValue,lstatate2.LookupValue, o2.OrderStateCode,o2.OrderStatusCode" +
                   " ) sub on sub.EmpCode=Main.EmpCode and sub.ORDER_DATE=Main.ORDER_DATE and sub.OrderStateCode=Main.OrderStateCode and sub.OrderStatusCode = Main.OrderStatusCode" +

                   " order by convert(datetime,Main.ORDER_DATE,103),EmpCode desc " +
                    strnumpage +
                    " ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new ReportResultSOSAInfo()
                          {

                              SALE_CODE = dr["EmpCode"].ToString().Trim(),
                              SALE_NAME = dr["EmpFname_TH"].ToString().Trim() + " " + dr["EmpLname_TH"].ToString().Trim(),
                              ORDER_DATE = dr["ORDER_DATE"].ToString().Trim(),
                              CHANNEL = dr["ChannelName"].ToString().Trim(),
                              BRAND = dr["CamCate_name"].ToString().Trim(),
                              ORDER_STAGE = dr["OrderStatusname"].ToString().Trim(),
                              ORDER_STATUS = dr["OrderStatatename"].ToString().Trim(),
                              TOTAL_QTY = dr["num_of_amount"].ToString().Trim(),
                              TOTAL_AMOUNT = dr["TOTAL_PRICE2"].ToString().Trim(),
                              TOTAL_ORDER = dr["num_of_order"].ToString().Trim(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public int? CountReportResultSOSA(OrderOLInfo oInfo)
        {
            #region condition
            int? count = 0;
            string strcond = "";
            string strcondSub = "";
            string strnumpage = "";
            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.OrderCode like '%" + oInfo.OrderCode + "%'" : strcondSub += " AND  o2.OrderCode like '%" + oInfo.OrderCode + "%'";

            }

            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcondSub += " and  o2.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";

            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcondSub += " AND o2.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";

            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcondSub += " AND o2.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";

            }

            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcondSub += " AND o2.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";

            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";

                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcondSub += " AND o2.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo == null) || (oInfo.DeliveryDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)" : strcond += " AND o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)" : strcondSub += " AND o2.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";

            }

            if (((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")) && ((oInfo.DeliveryDate == null) || (oInfo.DeliveryDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)" : strcond += " AND o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)" : strcondSub += " AND o2.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";

            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o.C2ustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o2.CustomerCode like '%" + oInfo.CustomerCode + "%'";

            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  c2.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcondSub += " and  c2.CustomerFName like '%" + oInfo.CustomerFName + "%'";

            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  c2.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcondSub += " and  c2.CustomerLName like '%" + oInfo.CustomerLName + "%'";

            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcond += " and  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'";
                strcondSub = strcondSub == "" ? strcond += " WHERE  cp2.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcondSub += " and  cp2.PhoneNumber like '%" + oInfo.CustomerContact + "%'";

            }

            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.ChannelCode = '" + oInfo.ChannelCode + "'" : strcondSub += " and  o2.ChannelCode = '" + oInfo.ChannelCode + "'";

            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcondSub += " and  o2.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";

            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcondSub += " and  o2.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";

            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcondSub += " and  o2.OrderStateCode = '" + oInfo.OrderStateCode + "'";

            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.CreateBy = '" + oInfo.CreateBy + "'" : strcondSub += " and  o2.CreateBy = '" + oInfo.CreateBy + "'";

            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.FlagApproved = '" + oInfo.FlagApproved + "'" : strcondSub += " and  o2.FlagApproved = '" + oInfo.FlagApproved + "'";

            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            #endregion

            var LOrder = new List<CountReportResultSOSAInfo>();
            DataTable dt = new DataTable();


            try
            {


                string strsql = " select count(Main.EmpCode) as countrow "



                   + " from (select e.EmpCode ,e.EmpFname_TH,e.EmpLName_TH,cpc.CamCate_name,ch.ChannelName,CONVERT(varchar, o.CreateDate,103) as ORDER_DATE, " +
                   " sum(o.TotalPrice) as TOTAL_PRICE, o.OrderStateCode,o.OrderStatusCode,lstatus.LookupValue as OrderStatusname" +
                   " ,lstatate.LookupValue as OrderStatatename,count(od.Id) as num_of_amount " +
                   " from OrderInfo o inner join emp e on e.EmpCode =o.CreateBy  " +
                   " inner join OrderDetail od on od.OrderCode = o.OrderCode" +
                   " inner join CampaignCategory cpc on cpc.CampaignCategoryCode =o.CampaignCategoryCode" +
                   " inner join Channel ch on ch.ChannelCode = o.ChannelCode" +
                   " inner join Lookup lstatus on lstatus.LookupCode = o.OrderStatusCode and lstatus.LookupType='ORDERSTATUS'" +
                   " inner join Lookup lstatate on lstatate.LookupCode = o.OrderStateCode and lstatate.LookupType='ORDERSTATE'" +
                    strcond

                   + " group by e.EmpCode,e.EmpFname_TH,e.EmpLName_TH,cpc.CamCate_name,ch.ChannelName " +
                   " ,lstatus.LookupValue,lstatate.LookupValue ,CONVERT(varchar, o.CreateDate,103), o.OrderStateCode, o.OrderStatusCode  ) Main " +
                   "" +
                   // table2
                   " INNER JOIN (select count(o2.Id) as num_of_order ,e2.EmpCode ,CONVERT(varchar, o2.CreateDate,103) as ORDER_DATE," +
                   " lstatus2.LookupValue as OrderStatusname,lstatate2.LookupValue as OrderStatatename, o2.OrderStateCode,o2.OrderStatusCode" +
                   " from OrderInfo o2 inner join emp e2 on e2.EmpCode =o2.CreateBy" +
                   " inner join Lookup lstatus2 on lstatus2.LookupCode = o2.OrderStatusCode and lstatus2.LookupType='ORDERSTATUS'" +
                   " inner join Lookup lstatate2 on lstatate2.LookupCode = o2.OrderStateCode and lstatate2.LookupType='ORDERSTATE' " +
                   strcondSub
                   + " group by e2.EmpCode,CONVERT(varchar, o2.CreateDate,103),lstatus2.LookupValue,lstatate2.LookupValue, o2.OrderStateCode,o2.OrderStatusCode" +
                   " ) sub on sub.EmpCode=Main.EmpCode and sub.ORDER_DATE=Main.ORDER_DATE and sub.OrderStateCode=Main.OrderStateCode and sub.OrderStatusCode = Main.OrderStatusCode" +


                   "";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new CountReportResultSOSAInfo()
                          {
                              CountReportResultSOSA = Convert.ToInt32(dr["countRow"])
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LOrder.Count > 0)
            {
                count = LOrder[0].CountReportResultSOSA;
            }

            return count;
        }
        public List<ReaportCampaignPromotionByProductInfo> ReaportCampaignPromotionByproduct(OrderOLInfo oInfo)
        {
            #region condition

            string strcond = " where o.MerchantMapCode = 'GP2021001' ";
            string strnumpage = "";
            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.SALE_CODE != null) && (oInfo.SALE_CODE != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  e.EmpCode like '%" + oInfo.SALE_CODE + "%'" : strcond += " AND  e.EmpCode like '%" + oInfo.SALE_CODE + "%'";
            }
            if ((oInfo.SALE_FNAME != null) && (oInfo.SALE_FNAME != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  e.EmpFname_TH like '%" + oInfo.SALE_FNAME + "%'" : strcond += " AND  e.EmpFname_TH like '%" + oInfo.SALE_FNAME + "%'";
            }
            if ((oInfo.SALE_LNAME != null) && (oInfo.SALE_LNAME != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  e.EmpLname_TH like '%" + oInfo.SALE_LNAME + "%'" : strcond += " AND  e.EmpLname_TH like '%" + oInfo.SALE_LNAME + "%'";
            }
            if ((oInfo.CAMPAIGN_CODE != null) && (oInfo.CAMPAIGN_CODE != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.CampaignCode like '%" + oInfo.CAMPAIGN_CODE + "%'" : strcond += " AND  cp.CampaignCode like '%" + oInfo.CAMPAIGN_CODE + "%'";
            }
            if ((oInfo.CAMPAIGN_NAME != null) && (oInfo.CAMPAIGN_NAME != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.CampaignName like '%" + oInfo.CAMPAIGN_NAME + "%'" : strcond += " AND  cp.CampaignName like '%" + oInfo.CAMPAIGN_NAME + "%'";
            }
            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE od.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND od.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
            }

            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE od.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND od.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo == null) || (oInfo.DeliveryDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)" : strcond += " AND o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";
            }

            if (((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")) && ((oInfo.DeliveryDate == null) || (oInfo.DeliveryDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)" : strcond += " AND o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";
            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcond += " and  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'";
            }

            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            #endregion

            var LOrder = new List<ReaportCampaignPromotionByProductInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = "select od.CampaignCode,cp.CampaignName,od.PromotionCode,pmt.PromotionName,p.ProductName " +
                ",sum(od.Amount) as Amount ,sum(od.TotalPrice) as TotalPrice ,CONVERT(VARCHAR(10), od.CreateDate, 111) as Createdate from OrderInfo o " +
                " inner join OrderDetail od on od.OrderCode = o.OrderCode " +
                " inner join Promotion pmt on od.PromotionCode = pmt.PromotionCode " +
                " inner join Campaign cp on od.CampaignCode = cp.CampaignCode " +
                " inner join Product p on p.ProductCode = od.ProductCode "
                + strcond +
                " group by od.CampaignCode,cp.CampaignName,od.PromotionCode,p.ProductName,pmt.PromotionName,CONVERT(VARCHAR(10), od.CreateDate, 111) " +
                " order by CONVERT(VARCHAR(10), od.CreateDate, 111) desc  " + strnumpage
                ;

                

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new ReaportCampaignPromotionByProductInfo()
                          {
                             
                              ORDER_DATE = dr["Createdate"].ToString().Trim(),
                              PRODUCT_NAME = dr["ProductName"].ToString().Trim(),
                              CAMPAIGN_CODE = dr["CampaignCode"].ToString().Trim(),
                              CAMPAIGN_NAME = dr["CampaignName"].ToString().Trim(),
                              PROMOTION_CODE = dr["PromotionCode"].ToString().Trim(),
                              PROMOTION_NAME = dr["PromotionName"].ToString().Trim(),
                              AMOUNT = dr["Amount"].ToString().Trim(),
                              PRICE = dr["TotalPrice"].ToString().Trim(),


                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public int? CountReaportCampaignPromotionByproduct(OrderOLInfo oInfo)
        {
            #region condition
            int? count = 0;
            string strcond = " where o.MerchantMapCode = 'GP2021001'  ";
            string strnumpage = "";
            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }
            if ((oInfo.CAMPAIGN_CODE != null) && (oInfo.CAMPAIGN_CODE != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.CampaignCode like '%" + oInfo.CAMPAIGN_CODE + "%'" : strcond += " AND  cp.CampaignCode like '%" + oInfo.CAMPAIGN_CODE + "%'";
            }
            if ((oInfo.CAMPAIGN_NAME != null) && (oInfo.CAMPAIGN_NAME != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.CampaignName like '%" + oInfo.CAMPAIGN_NAME + "%'" : strcond += " AND  cp.CampaignName like '%" + oInfo.CAMPAIGN_NAME + "%'";
            }
            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE od.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND od.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
            }

            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE od.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND od.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo == null) || (oInfo.DeliveryDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)" : strcond += " AND o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";
            }

            if (((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")) && ((oInfo.DeliveryDate == null) || (oInfo.DeliveryDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)" : strcond += " AND o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";
            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcond += " and  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'";
            }

            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            #endregion

            var LOrder = new List<CountReaportCampaignPromotionByProductInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " select COUNT(*) as countRow from "
                    + "(select od.CampaignCode,cp.CampaignName,od.PromotionCode,pmt.PromotionName,p.ProductName " +
                    "  ,sum(od.Amount) as Amount ,sum(od.TotalPrice) as TotalPrice " +
                    "  ,CONVERT(VARCHAR(10), od.CreateDate, 111) as Createdate from OrderInfo o" +
                    "  inner join OrderDetail od on od.OrderCode = o.OrderCode" +
                    "  inner join Promotion pmt on od.PromotionCode = pmt.PromotionCode" +
                    "  inner join Campaign cp on od.CampaignCode = cp.CampaignCode" +
                    "  inner join Product p on p.ProductCode = od.ProductCode " 
                     + strcond +
                     " group by od.CampaignCode,cp.CampaignName,od.PromotionCode,p.ProductName,pmt.PromotionName,CONVERT(VARCHAR(10), od.CreateDate, 111)) as agg "


                  ;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new CountReaportCampaignPromotionByProductInfo()
                          {
                              CountReaportCampaignPromotionByProduct = Convert.ToInt32(dr["countRow"])
                          }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LOrder.Count > 0)
            {
                count = LOrder[0].CountReaportCampaignPromotionByProduct;
            }

            return count;
        }
        public List<ReaportMediaDailySaleInfo> ReaportMediaDailySale(OrderOLInfo oInfo)
        {
            #region condition

            string strcond = "";
            strcond = " where  o.FlagMediaPlan='Y' ";
            string strnumpage = "";
            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
            }

            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo == null) || (oInfo.DeliveryDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)" : strcond += " AND o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";
            }

            if (((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")) && ((oInfo.DeliveryDate == null) || (oInfo.DeliveryDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)" : strcond += " AND o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";
            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcond += " and  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'";
            }

            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  od.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  od.ChannelCode = '" + oInfo.ChannelCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  od.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  od.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            #endregion

            var LOrder = new List<ReaportMediaDailySaleInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " select CONVERT(varchar,  mdp.MediaPlanDate,103) as MediaPlanDate, convert(char(5),mdp.MediaPlanTime, 108) as mediaplantime,mdp.MediaPhone,mdp.ProgramName,  "
                    + " cp.CampaignCode,pmt.PromotionCode,ch.ChannelCode, cp.CampaignName,pmt.PromotionName, o.OrderCode,CONVERT(varchar, o.CreateDate,103) order_date," +
                    " od.ProductCode,p.ProductName,cpc.CamCate_name,ch.ChannelName,od.Amount,od.Price,od.TotalPrice,pm.PaymentTypeName,o.UpdateDate, "
                    + " e.EmpCode as sale_Code,e.EmpFname_TH+' '+e.EmpLName_TH as Sale_Name ,cust.CustomerCode ,cust.CustomerFName+' '+cust.CustomerLName as customername "
                    + ",convert(char(5),  mdp.TimeStart, 108) as TimeStart ,convert(char(5),  mdp.TimeEnd, 108) as TimeEnd,mdp.Duration"

                    + " from OrderDetail od " +
                    " inner join OrderInfo o on o.OrderCode = od.OrderCode" +
                    " inner join emp e on e.EmpCode =o.CreateBy" +
                    " inner join emp eupdate on eupdate.EmpCode =o.UpdateBy" +
                    " inner join Product p on od.ProductCode = p.ProductCode" +
                    " inner join Promotion pmt on od.PromotionCode = pmt.PromotionCode" +
                    " inner join Customer cust on o.CustomerCode = cust.CustomerCode" +
                    " inner join CampaignCategory cpc on cpc.CampaignCategoryCode = od.CampaignCategoryCode" +
                    " inner join OrderPayment op on op.OrderCode = o.OrderCode" +
                    " inner join PaymentType pm on pm.PaymentTypeCode=op.PaymentTypeCode" +
                    " inner join Campaign cp on od.CampaignCode = cp.CampaignCode " +
                    " left join MediaPlan mdp on mdp.SALE_CHANNEL = od.ChannelCode and mdp.FlagApprove = 'Y' and mdp.FlagDelete = 'N' and od.FlagMediaPlan = 'Y'" +
                    " inner join Channel ch on ch.ChannelCode = od.ChannelCode "
                     + strcond
                    + " order by o.CreateDate desc " +
                    strnumpage +
                    " ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new ReaportMediaDailySaleInfo()
                          {
                              ORDER_NO = dr["ordercode"].ToString().Trim(),
                              SALE_CODE = dr["sale_Code"].ToString().Trim(),
                              SALE_NAME = dr["Sale_Name"].ToString().Trim(),
                              ORDER_DATE = dr["order_date"].ToString().Trim(),
                              PRODUCT_CODE = dr["ProductCode"].ToString().Trim(),
                              PRODUCT_NAME = dr["ProductName"].ToString().Trim(),

                              CHANNEL = dr["ChannelName"].ToString().Trim(),
                              CAMPAIGN_CODE = dr["CampaignCode"].ToString().Trim(),
                              CAMPAIGN_NAME = dr["CampaignName"].ToString().Trim(),

                              PROMOTION_NAME = dr["PromotionName"].ToString().Trim(),
                              AMOUNT = dr["Amount"].ToString().Trim(),
                              CUSTOMER_CODE = dr["CustomerCode"].ToString().Trim(),
                              CUSTOMER_NAME = dr["customername"].ToString().Trim(),
                              PAYMENT_TERM = dr["PaymentTypeName"].ToString().Trim(),
                              PRICE = dr["PRICE"].ToString().Trim(),


                              MEDIA_DATE = dr["MediaPlanDate"].ToString().Trim(),
                              MEDIA_PHONE = dr["MediaPhone"].ToString().Trim(),
                              MEDIA_TIME = dr["mediaplantime"].ToString().Trim(),
                              PROGRAM_NAME = dr["ProgramName"].ToString().Trim(),
                              PROMOTION_CODE = dr["PromotionCode"].ToString().Trim(),

                              TIME_START = dr["TimeStart"].ToString().Trim(),
                              TIME_END = dr["TimeEnd"].ToString().Trim(),
                              DURATION = dr["Duration"].ToString().Trim(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public List<ReaportMediaDailySaleInfo> ReaportMediaDailySale_Excel(OrderOLInfo oInfo)
        {
            #region condition

            string strcond = "";
            strcond = " where  o.FlagMediaPlan='Y' ";
            string strnumpage = "";
            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
            }

            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo == null) || (oInfo.DeliveryDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)" : strcond += " AND o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";
            }

            if (((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")) && ((oInfo.DeliveryDate == null) || (oInfo.DeliveryDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)" : strcond += " AND o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";
            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcond += " and  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'";
            }

            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            #endregion

            var LOrder = new List<ReaportMediaDailySaleInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " select CONVERT(varchar,  mdp.MediaPlanDate,103) as MediaPlanDate, convert(char(5),mdp.MediaPlanTime, 108) as mediaplantime,mdp.MediaPhone,mdp.ProgramName,  "
                     + " cp.CampaignCode,pmt.PromotionCode,ch.ChannelCode, cp.CampaignName,pmt.PromotionName, o.OrderCode,CONVERT(varchar, o.CreateDate,103) order_date," +
                     " od.ProductCode,p.ProductName,cpc.CamCate_name,ch.ChannelName,od.Amount,od.Price,pm.PaymentTypeName,o.UpdateDate, "
                     + " e.EmpCode as sale_Code,e.EmpFname_TH+' '+e.EmpLName_TH as Sale_Name ,cust.CustomerCode ,cust.CustomerFName+' '+cust.CustomerLName as customername "
                     + ",convert(char(5),  mdp.TimeStart, 108) as TimeStart ,convert(char(5),  mdp.TimeEnd, 108) as TimeEnd,mdp.Duration,od.SumPrice"
                     + ",(SELECT SUM(CAST(NetPrice AS decimal(10, 2) )*ol0.Amount) from OrderDetail ol0 where ol0.OrderCode =o.OrderCode ) as SumbyProductprice"
                     + ",cast(o.TotalPrice as decimal(10,2)) TotalPrice ,od.TransportPrice"

                     + " from OrderDetail od " +
                     " inner join OrderInfo o on o.OrderCode = od.OrderCode" +
                     " inner join emp e on e.EmpCode =o.CreateBy" +
                     " inner join emp eupdate on eupdate.EmpCode =o.UpdateBy" +
                     " left join Product p on od.ProductCode = p.ProductCode" +
                     " inner join Promotion pmt on od.PromotionCode = pmt.PromotionCode" +
                     " inner join Customer cust on o.CustomerCode = cust.CustomerCode" +
                     " inner join CampaignCategory cpc on cpc.CampaignCategoryCode = od.CampaignCategoryCode" +
                     " inner join OrderPayment op on op.OrderCode = o.OrderCode" +
                     " inner join PaymentType pm on pm.PaymentTypeCode=op.PaymentTypeCode" +
                     " inner join Campaign cp on od.CampaignCode = cp.CampaignCode " +
                     " left join MediaPlan mdp on mdp.SALE_CHANNEL = od.ChannelCode and mdp.FlagApprove = 'Y' and mdp.FlagDelete = 'N' and od.FlagMediaPlan = 'Y'" +
                     " inner join Channel ch on ch.ChannelCode = od.ChannelCode "
                      + strcond
                     + " order by o.CreateDate desc " +
                     strnumpage +
                     " ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new ReaportMediaDailySaleInfo()
                          {
                              ORDER_NO = dr["ordercode"].ToString().Trim(),
                              SALE_CODE = dr["sale_Code"].ToString().Trim(),
                              SALE_NAME = dr["Sale_Name"].ToString().Trim(),
                              ORDER_DATE = dr["order_date"].ToString().Trim(),
                              PRODUCT_CODE = dr["ProductCode"].ToString().Trim(),
                              PRODUCT_NAME = dr["ProductName"].ToString().Trim(),

                              CHANNEL = dr["ChannelName"].ToString().Trim(),
                              CAMPAIGN_CODE = dr["CampaignCode"].ToString().Trim(),
                              CAMPAIGN_NAME = dr["CampaignName"].ToString().Trim(),

                              PROMOTION_NAME = dr["PromotionName"].ToString().Trim(),
                              AMOUNT = dr["Amount"].ToString().Trim(),
                              CUSTOMER_CODE = dr["CustomerCode"].ToString().Trim(),
                              CUSTOMER_NAME = dr["customername"].ToString().Trim(),
                              PAYMENT_TERM = dr["PaymentTypeName"].ToString().Trim(),

                              PRICE = dr["PRICE"].ToString().Trim(),
                              TransportPrice = (dr["TransportPrice"].ToString().Trim() != "") ? dr["TransportPrice"].ToString().Trim() : "0",
                              orderTotalPrice = (dr["TotalPrice"].ToString() != "") ? Convert.ToDecimal(dr["TotalPrice"].ToString().Trim()) : 0,
                              SumbyProductprice = dr["SumbyProductprice"].ToString().Trim(),

                              MEDIA_DATE = dr["MediaPlanDate"].ToString().Trim(),
                              MEDIA_PHONE = dr["MediaPhone"].ToString().Trim(),
                              MEDIA_TIME = dr["mediaplantime"].ToString().Trim(),
                              PROGRAM_NAME = dr["ProgramName"].ToString().Trim(),
                              PROMOTION_CODE = dr["PromotionCode"].ToString().Trim(),

                              TIME_START = dr["TimeStart"].ToString().Trim(),
                              TIME_END = dr["TimeEnd"].ToString().Trim(),
                              DURATION = dr["Duration"].ToString().Trim(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public int? CountReaportMediaDailySale(OrderOLInfo oInfo)
        {
            #region condition
            int? count = 0;
            string strcond = "";
            strcond = " where  o.FlagMediaPlan='Y' ";
            string strnumpage = "";
            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
            }

            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo == null) || (oInfo.DeliveryDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)" : strcond += " AND o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";
            }

            if (((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")) && ((oInfo.DeliveryDate == null) || (oInfo.DeliveryDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)" : strcond += " AND o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";
            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcond += " and  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'";
            }

            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  od.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  od.ChannelCode = '" + oInfo.ChannelCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  od.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  od.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            #endregion

            var LOrder = new List<CountReaportMediaDailySaleInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " select count(CONVERT(varchar,  mdp.MediaPlanDate,103)) as countRow "

                   + " from OrderDetail od " +
                    " inner join OrderInfo o on o.OrderCode = od.OrderCode" +
                    " inner join emp e on e.EmpCode =o.CreateBy" +
                    " inner join emp eupdate on eupdate.EmpCode =o.UpdateBy" +
                    " inner join Product p on od.ProductCode = p.ProductCode" +
                    " inner join Promotion pmt on od.PromotionCode = pmt.PromotionCode" +
                    " inner join Customer cust on o.CustomerCode = cust.CustomerCode" +
                    " inner join CampaignCategory cpc on cpc.CampaignCategoryCode = od.CampaignCategoryCode" +
                    " inner join OrderPayment op on op.OrderCode = o.OrderCode" +
                    " inner join PaymentType pm on pm.PaymentTypeCode=op.PaymentTypeCode" +
                    " inner join Campaign cp on od.CampaignCode = cp.CampaignCode " +
                    " left join MediaPlan mdp on mdp.FlagApprove = 'Y' and mdp.FlagDelete = 'N' and od.ChannelCode = mdp.SALE_CHANNEL" +
                    " inner join Channel ch on ch.ChannelCode = od.ChannelCode "
                     + strcond;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new CountReaportMediaDailySaleInfo()
                          {
                              CountReaportMediaDailySale = Convert.ToInt32(dr["countRow"])
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LOrder.Count > 0)
            {
                count = LOrder[0].CountReaportMediaDailySale;
            }

            return count;
        }
        //
        public List<ReaportPerformanceSOSAInfo> ReaportPerformanceSOSA(OrderOLInfo oInfo)
        {
            #region condition

            string strcond = "";
            string strcondSub = "";
            string strnumpage = "";
            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.OrderCode like '%" + oInfo.OrderCode + "%'" : strcondSub += " AND  o2.OrderCode like '%" + oInfo.OrderCode + "%'";

            }

            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcondSub += " and  o2.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";

            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcondSub += " AND o2.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";

            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcondSub += " AND o2.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";

            }

            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcondSub += " AND o2.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";

            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";

                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcondSub += " AND o2.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo == null) || (oInfo.DeliveryDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)" : strcond += " AND o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)" : strcondSub += " AND o2.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";

            }

            if (((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")) && ((oInfo.DeliveryDate == null) || (oInfo.DeliveryDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)" : strcond += " AND o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)" : strcondSub += " AND o2.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";

            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o.C2ustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o2.CustomerCode like '%" + oInfo.CustomerCode + "%'";

            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  c2.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcondSub += " and  c2.CustomerFName like '%" + oInfo.CustomerFName + "%'";

            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  c2.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcondSub += " and  c2.CustomerLName like '%" + oInfo.CustomerLName + "%'";

            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcond += " and  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'";
                strcondSub = strcondSub == "" ? strcond += " WHERE  cp2.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcondSub += " and  cp2.PhoneNumber like '%" + oInfo.CustomerContact + "%'";

            }

            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.ChannelCode = '" + oInfo.ChannelCode + "'" : strcondSub += " and  o2.ChannelCode = '" + oInfo.ChannelCode + "'";

            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcondSub += " and  o2.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";

            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcondSub += " and  o2.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";

            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcondSub += " and  o2.OrderStateCode = '" + oInfo.OrderStateCode + "'";

            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.CreateBy = '" + oInfo.CreateBy + "'" : strcondSub += " and  o2.CreateBy = '" + oInfo.CreateBy + "'";

            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.FlagApproved = '" + oInfo.FlagApproved + "'" : strcondSub += " and  o2.FlagApproved = '" + oInfo.FlagApproved + "'";

            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            #endregion

            var LOrder = new List<ReaportPerformanceSOSAInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " select  convert(varchar,main.order_date,103) as order_date , main.Sale_Name,main.sale_Code,main.ChannelName, "
                    + " (case when main.qtybbkarea is not null then convert(int,main.qtybbkarea) else 0 end  +  case when sub.Total is not null then convert(int,sub.Qtysubarea) else 0 end ) as AllTotal," +
                    " case when main.qtybbkarea is not null then main.qtybbkarea else '0' end as qtybbkarea,main.TotalBKK, "
                    + " case when sub.Qtysubarea is not null then sub.Qtysubarea else '0' end as Qtysubarea," +
                    " case when sub.Total is not null then sub.Total else '0' end as TotalUPC " +

                    "  from(select  e.EmpCode as sale_Code,e.EmpFname_TH+' '+e.EmpLName_TH as Sale_Name" +
                    " ,ch.ChannelName,CONVERT(date, o.CreateDate,103) order_date, sum(o.TotalPrice) as TotalBKK,pv.typearea, " +
                    " case when  COUNT(pv.TypeArea) !=0 then  COUNT(pv.TypeArea) else '0' end as qtybbkarea " +
                    " from OrderInfo o " +
                    " inner join emp e on e.EmpCode =o.CreateBy" +
                    " inner join Channel ch on ch.ChannelCode = o.ChannelCode" +
                    " inner join OrderPayment op on op.OrderCode = o.OrderCode" +
                    " inner join OrderTransport ot on ot.OrderCode= o.OrderCode and ot.AddressType='01'" +
                    " inner join Province pv on pv.ProvinceCode = ot.Province" +
                     strcond +
                    " and pv.typearea='BKK' " +
                    " group by CONVERT(date, o.CreateDate,103),e.EmpCode,e.EmpFname_TH+' '+e.EmpLName_TH " +
                    " ,pv.typearea,ch.ChannelName) main" +

                    " left join (select  e2.EmpCode as sale_Code,e2.EmpFname_TH+' '+e2.EmpLName_TH as Sale_Name" +
                    " ,ch2.ChannelName,CONVERT(date, o2.CreateDate,103) order_date," +
                    "  sum(o2.TotalPrice) as Total,pv2.typearea ,COUNT(pv2.TypeArea) as subtypearea," +
                    "  COUNT(pv2.TypeArea) as Qtysubarea" +
                    " from OrderInfo o2 " +
                    " inner join emp e2 on e2.EmpCode =o2.CreateBy" +
                    " inner join Channel ch2 on ch2.ChannelCode = o2.ChannelCode" +
                    " inner join OrderPayment op2 on op2.OrderCode = o2.OrderCode" +
                    " inner join OrderTransport ot2 on ot2.OrderCode= o2.OrderCode and ot2.AddressType='01'" +
                    " inner join Province pv2 on pv2.ProvinceCode = ot2.Province" +
                    strcondSub +
                    " and pv2.typearea='UPC'" +
                    " group by  CONVERT(date, o2.CreateDate,103),e2.EmpCode,e2.EmpFname_TH+' '+e2.EmpLName_TH " +
                    " ,pv2.typearea,ch2.ChannelName)  sub on sub.order_date=main.order_date and sub.sale_Code=main.sale_Code" +
                    "" +
                     
                     " order by main.order_date desc " +
                    strnumpage +
                    " ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new ReaportPerformanceSOSAInfo()
                          {
                              SALE_CODE = dr["sale_Code"].ToString().Trim(),
                              SALE_NAME = dr["Sale_Name"].ToString().Trim(),
                              CHANNEL = dr["ChannelName"].ToString().Trim(),
                              ORDER_DATE = dr["order_date"].ToString().Trim(),
                              TOTAL = dr["AllTotal"].ToString().Trim(),
                              BKK_ORDER = dr["qtybbkarea"].ToString().Trim(),
                              BKK_REVENUE = dr["TotalBKK"].ToString().Trim(),
                              UPC_ORDER = dr["Qtysubarea"].ToString().Trim(),
                              UPC_REVENUE = dr["TotalUPC"].ToString().Trim(),


                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public int? CountReaportPerformanceSOSA(OrderOLInfo oInfo)
        {
            #region condition
            int? count = 0;
            string strcond = "";
            string strcondSub = "";
            string strnumpage = "";
            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.OrderCode like '%" + oInfo.OrderCode + "%'" : strcondSub += " AND  o2.OrderCode like '%" + oInfo.OrderCode + "%'";

            }

            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcondSub += " and  o2.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";

            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcondSub += " AND o2.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";

            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcondSub += " AND o2.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";

            }

            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcondSub += " AND o2.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";

            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";

                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcondSub += " AND o2.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo == null) || (oInfo.DeliveryDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)" : strcond += " AND o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)" : strcondSub += " AND o2.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";

            }

            if (((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")) && ((oInfo.DeliveryDate == null) || (oInfo.DeliveryDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)" : strcond += " AND o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)" : strcondSub += " AND o2.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";

            }

            //salecode
            if ((oInfo.SALE_CODE != null) && (oInfo.SALE_CODE != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  e.EmpCode like '%" + oInfo.SALE_CODE + "%'" : strcond += " and  e.EmpCode like '%" + oInfo.SALE_CODE + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  e.EmpCode like '%" + oInfo.SALE_CODE + "%'" : strcond += " and  e.EmpCode like '%" + oInfo.SALE_CODE + "%'";
            }

            //employee name
            if ((oInfo.SALE_FNAME != null) && (oInfo.SALE_FNAME != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  e.EmpFname_TH like '%" + oInfo.CustomerFName + "%'" : strcond += " and  e.EmpFname_TH like '%" + oInfo.CustomerFName + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  e2.EmpFname_TH like '%" + oInfo.CustomerFName + "%'" : strcondSub += " and  e2.EmpFname_TH like '%" + oInfo.CustomerFName + "%'";

            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  c2.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcondSub += " and  c2.CustomerLName like '%" + oInfo.CustomerLName + "%'";

            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcond += " and  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'";
                strcondSub = strcondSub == "" ? strcond += " WHERE  cp2.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcondSub += " and  cp2.PhoneNumber like '%" + oInfo.CustomerContact + "%'";

            }

            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.ChannelCode = '" + oInfo.ChannelCode + "'" : strcondSub += " and  o2.ChannelCode = '" + oInfo.ChannelCode + "'";

            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcondSub += " and  o2.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";

            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcondSub += " and  o2.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";

            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcondSub += " and  o2.OrderStateCode = '" + oInfo.OrderStateCode + "'";

            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.CreateBy = '" + oInfo.CreateBy + "'" : strcondSub += " and  o2.CreateBy = '" + oInfo.CreateBy + "'";

            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.FlagApproved = '" + oInfo.FlagApproved + "'" : strcondSub += " and  o2.FlagApproved = '" + oInfo.FlagApproved + "'";

            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            #endregion

            var LOrder = new List<CountReaportPerformanceSOSAInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " select count(main.sale_Code) as countRow" +

                    "  from(select  e.EmpCode as sale_Code,e.EmpFname_TH+' '+e.EmpLName_TH as Sale_Name" +
                    " ,ch.ChannelName,CONVERT(date, o.CreateDate,103) order_date, sum(o.TotalPrice) as TotalBKK,pv.typearea, " +
                    " case when  COUNT(pv.TypeArea) !=0 then  COUNT(pv.TypeArea) else '0' end as qtybbkarea " +
                    " from OrderInfo o " +
                    " inner join emp e on e.EmpCode =o.CreateBy" +
                    " inner join Channel ch on ch.ChannelCode = o.ChannelCode" +
                    " inner join OrderPayment op on op.OrderCode = o.OrderCode" +
                    " inner join OrderTransport ot on ot.OrderCode= o.OrderCode and ot.AddressType='01'" +
                    " inner join Province pv on pv.ProvinceCode = ot.Province" +
                     strcond +
                    " and pv.typearea='BKK' " +
                    " group by CONVERT(date, o.CreateDate,103),e.EmpCode,e.EmpFname_TH+' '+e.EmpLName_TH " +
                    " ,pv.typearea,ch.ChannelName) main" +

                    " left join (select  e2.EmpCode as sale_Code,e2.EmpFname_TH+' '+e2.EmpLName_TH as Sale_Name" +
                    " ,ch2.ChannelName,CONVERT(date, o2.CreateDate,103) order_date," +
                    "  sum(o2.TotalPrice) as Total,pv2.typearea ,COUNT(pv2.TypeArea) as subtypearea," +
                    "  COUNT(pv2.TypeArea) as Qtysubarea" +
                    " from OrderInfo o2 " +
                    " inner join emp e2 on e2.EmpCode =o2.CreateBy" +
                    " inner join Channel ch2 on ch2.ChannelCode = o2.ChannelCode" +
                    " inner join OrderPayment op2 on op2.OrderCode = o2.OrderCode" +
                    " inner join OrderTransport ot2 on ot2.OrderCode= o2.OrderCode and ot2.AddressType='01'" +
                    " inner join Province pv2 on pv2.ProvinceCode = ot2.Province" +
                    strcondSub +
                    " and pv2.typearea='UPC'" +
                    " group by  CONVERT(date, o2.CreateDate,103),e2.EmpCode,e2.EmpFname_TH+' '+e2.EmpLName_TH " +
                    " ,pv2.typearea,ch2.ChannelName)  sub on sub.order_date=main.order_date and sub.sale_Code=main.sale_Code" +
                    "" +
                    "";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new CountReaportPerformanceSOSAInfo()
                          {
                              CountReaportPerformanceSOSA = Convert.ToInt32(dr["countRow"])

                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LOrder.Count > 0)
            {
                count = LOrder[0].CountReaportPerformanceSOSA;
            }

            return count;
        }
        public List<ReportResultCallinInfo> ReportResultCallin(OrderOLInfo oInfo)
        {
            #region condition

            string strcond = "";
            string strcondSub = "";
            string strnumpage = "";


            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcondSub += " AND o2.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";

            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcondSub += " AND o2.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";

            }

            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcondSub += " AND o2.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";

            }

            //salecode
            if ((oInfo.SALE_CODE != null) && (oInfo.SALE_CODE != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  e.EmpCode like '%" + oInfo.SALE_CODE + "%'" : strcond += " and  e.EmpCode like '%" + oInfo.SALE_CODE + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  e2.EmpCode like '%" + oInfo.SALE_CODE + "%'" : strcond += " and  e2.EmpCode like '%" + oInfo.SALE_CODE + "%'";

            }
            //employee Fname
            if ((oInfo.SALE_FNAME != null) && (oInfo.SALE_FNAME != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  e.EmpFname_TH like '%" + oInfo.SALE_FNAME + "%'" : strcond += " and  e.EmpFname_TH like '%" + oInfo.SALE_FNAME + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  e2.EmpFname_TH like '%" + oInfo.SALE_FNAME + "%'" : strcondSub += " and  e2.EmpFname_TH like '%" + oInfo.SALE_FNAME + "%'";

            }

            //employee Lname
            if ((oInfo.SALE_LNAME != null) && (oInfo.SALE_LNAME != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  e.EmpLName_TH like '%" + oInfo.SALE_LNAME + "%'" : strcond += " and  e.EmpLName_TH like '%" + oInfo.SALE_LNAME + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  e2.EmpLName_TH like '%" + oInfo.SALE_LNAME + "%'" : strcondSub += " and  e2.EmpLName_TH like '%" + oInfo.SALE_LNAME + "%'";

            }

            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.ChannelCode = '" + oInfo.ChannelCode + "'" : strcondSub += " and  o2.ChannelCode = '" + oInfo.ChannelCode + "'";

            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.FlagApproved = '" + oInfo.FlagApproved + "'" : strcondSub += " and  o2.FlagApproved = '" + oInfo.FlagApproved + "'";

            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }


            


            #endregion

            var LOrder = new List<ReportResultCallinInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = "select (case when dtbkk.EmpCode is not null then dtbkk.EmpCode else dtupc.EmpCode end) as sale_code"
                    + ",  (case when dtbkk.EmpFname_TH+' '+ dtbkk.EmpLName_TH is not null then dtbkk.EmpFname_TH+' '+ dtbkk.EmpLName_TH else dtupc.EmpFname_TH+' '+ dtupc.EmpLName_TH end) as sale_name"
                    + ",  (case when dtbkk.Call_status is not null then dtbkk.Call_status else dtupc.Call_status end) as Call_status" +
                    ",  (case when dtbkk.count_call is not null then convert(int,dtbkk.count_call) else 0 end  +  case when dtupc.count_call is not null then convert(int,dtupc.count_call) else 0 end ) as AllTotal"
                    + ",  (case when dtbkk.count_order is not null then convert(int,dtbkk.count_order) else 0 end +  case when dtupc.count_order is not null then convert(int, dtupc.count_order) else 0 end ) as Total_Order" +
                    ",  (case when dtbkk.ChannelName is not null then dtbkk.ChannelName else dtupc.ChannelName end) as ChannelName" +
                    ",case when dtbkk.count_call is not null then convert(int,dtbkk.count_call) else 0 end  as BKK_ORDER " +
                    ",case when dtbkk.total_price is not null then convert(decimal(18,2),dtbkk.total_price) else 0 end  as BKK_REVENUE " +
                    ",case when dtupc.count_call is not null then convert(int,dtupc.count_call) else 0 end as UPC_ORDER " +
                    ",case when dtupc.total_price is not null then convert(decimal(18,2),dtupc.total_price) else 0 end  as UPC_REVENUE" +
                    ",(case when dtbkk.orderdate is not null then CONVERT(date, dtbkk.orderdate,103) else CONVERT(date, dtupc.orderdate,103) end) as ORDER_DATE" +
                    " from(select e.EmpCode ,e.EmpFname_TH,e.EmpLName_TH,lstatus.LookupValue as Call_status" +
                    " ,ch.ChannelName,sum(o.TotalPrice) as total_price,count(cin.id) as count_call,count(o.id) as count_order,CONVERT(date, o.CreateDate,103) as orderdate,pv.typearea " +
                    " from OrderInfo o " +
                    " inner join emp e on e.EmpCode =o.CreateBy" +
                    " inner join emp eupdate on eupdate.EmpCode =o.UpdateBy" +
                    " inner join Channel ch on ch.ChannelCode = o.ChannelCode" +
                    " inner join CallInfo cin on cin.id = o.callinfo_id and cin.CreateBy=o.CreateBy" +
                    " inner join OrderTransport ot on ot.OrderCode= o.OrderCode and ot.AddressType='01'" +
                    " inner join Province pv on pv.ProvinceCode = ot.Province" +
                    " inner join Lookup lstatus on lstatus.LookupCode = cin.CONTACTSTATUS and lstatus.LookupType='CONTACTSTATUS'  " +
                     strcond +
                     " and pv.typearea='BKK' " +
                    " group by  e.EmpCode,e.EmpFname_TH,e.EmpLName_TH, " +
                    " lstatus.LookupValue ,ch.ChannelName,CONVERT(date, o.CreateDate,103),pv.typearea) as dtbkk" +

                    " full join (select e2.EmpCode,e2.EmpFname_TH,e2.EmpLName_TH,lstatus2.LookupValue as Call_status" +
                    " ,ch2.ChannelName,sum(o2.TotalPrice) as total_price,count(cin2.id) as count_call,count(o2.id) as count_order,CONVERT(date, o2.CreateDate,103) as orderdate,pv2.typearea" +

                    " from OrderInfo o2 " +
                    " inner join emp e2 on e2.EmpCode =o2.CreateBy" +
                    " inner join emp eupdate2 on eupdate2.EmpCode =o2.UpdateBy" +
                    " inner join Channel ch2 on ch2.ChannelCode = o2.ChannelCode" +
                    " inner join CallInfo cin2 on cin2.id = o2.callinfo_id and cin2.CreateBy=o2.CreateBy" +
                    " inner join OrderTransport ot2 on ot2.OrderCode= o2.OrderCode and ot2.AddressType='01'" +
                    " inner join Province pv2 on pv2.ProvinceCode = ot2.Province" +
                    " inner join Lookup lstatus2 on lstatus2.LookupCode = cin2.CONTACTSTATUS and lstatus2.LookupType='CONTACTSTATUS'" +
                    strcondSub +
                    " and pv2.TypeArea='UPC'" +
                    " group by  e2.EmpCode,e2.EmpFname_TH,e2.EmpLName_TH, " +
                    " lstatus2.LookupValue ,ch2.ChannelName,CONVERT(date, o2.CreateDate,103),pv2.typearea) as dtupc on dtbkk.EmpCode=dtupc.EmpCode and dtbkk.orderdate=dtupc.orderdate and dtbkk.ChannelName = dtupc.ChannelName and dtbkk.EmpCode = dtupc.EmpCode"

                    + " order by ORDER_DATE desc ,sale_code  " +
                    strnumpage +
                    " ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new ReportResultCallinInfo()
                          {

                              SALE_CODE = dr["sale_Code"].ToString().Trim(),
                              SALE_NAME = dr["Sale_Name"].ToString().Trim(),
                              CHANNEL = dr["ChannelName"].ToString().Trim(),
                              COUNT_CALL = dr["AllTotal"].ToString().Trim(),
                              CALL_STATUS = dr["Call_status"].ToString().Trim(),
                              BKK_ORDER = dr["BKK_ORDER"].ToString().Trim(),
                              BKK_REVENUE = dr["BKK_REVENUE"].ToString().Trim(),
                              UPC_ORDER = dr["UPC_ORDER"].ToString().Trim(),
                              UPC_REVENUE = dr["UPC_REVENUE"].ToString().Trim(),
                              ORDER_DATE = dr["ORDER_DATE"].ToString().Trim(),
                              TOTAL_ORDER = dr["Total_Order"].ToString().Trim(),

                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        //CountReportResultCallin

        public int? CountReportResultCallin(OrderOLInfo oInfo)
        {
            int? count = 0;
            #region condition

            string strcond = "";
            string strcondSub = "";
            string strnumpage = "";



            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcondSub += " AND o2.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";

            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcondSub += " AND o2.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";

            }

            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE o2.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcondSub += " AND o2.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";

            }

            if ((oInfo.SALE_CODE != null) && (oInfo.SALE_CODE != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o2.CustomerCode like '%" + oInfo.CustomerCode + "%'";

            }

            //employee name
            if ((oInfo.SALE_FNAME != null) && (oInfo.SALE_FNAME != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  e.EmpFname_TH like '%" + oInfo.SALE_FNAME + "%'" : strcond += " and  e.EmpFname_TH like '%" + oInfo.SALE_FNAME + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  e2.EmpFname_TH like '%" + oInfo.SALE_FNAME + "%'" : strcondSub += " and  e2.EmpFname_TH like '%" + oInfo.SALE_FNAME + "%'";

            }

            if ((oInfo.SALE_LNAME != null) && (oInfo.SALE_LNAME != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  e.EmpLName_TH like '%" + oInfo.SALE_LNAME + "%'" : strcond += " and  e.EmpLName_TH like '%" + oInfo.SALE_LNAME + "%'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  e2.EmpLName_TH like '%" + oInfo.SALE_LNAME + "%'" : strcondSub += " and  e2.EmpLName_TH like '%" + oInfo.SALE_LNAME + "%'";

            }

            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.ChannelCode = '" + oInfo.ChannelCode + "'" : strcondSub += " and  o2.ChannelCode = '" + oInfo.ChannelCode + "'";

            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
                strcondSub = strcondSub == "" ? strcondSub += " WHERE  o2.CreateBy = '" + oInfo.CreateBy + "'" : strcondSub += " and  o2.CreateBy = '" + oInfo.CreateBy + "'";

            }

            
            #endregion

            var LOrder = new List<CountReportResultCallinInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = "select count(case when dtbkk.EmpCode is not null then dtbkk.EmpCode else dtupc.EmpCode end) as countRow" +

                    " from(select e.EmpCode ,e.EmpFname_TH,e.EmpLName_TH,lstatus.LookupValue as Call_status" +
                    " ,ch.ChannelName,sum(o.TotalPrice) as total_price,count(cin.id) as count_call,count(o.id) as count_order,CONVERT(date, o.CreateDate,103) as orderdate,pv.typearea " +
                    " from OrderInfo o " +
                    " inner join emp e on e.EmpCode =o.CreateBy" +
                    " inner join emp eupdate on eupdate.EmpCode =o.UpdateBy" +
                    " inner join Channel ch on ch.ChannelCode = o.ChannelCode" +
                    " inner join CallInfo cin on cin.id = o.callinfo_id and cin.CreateBy=o.CreateBy" +
                    " inner join OrderTransport ot on ot.OrderCode= o.OrderCode and ot.AddressType='01'" +
                    " inner join Province pv on pv.ProvinceCode = ot.Province" +
                    " inner join Lookup lstatus on lstatus.LookupCode = cin.CONTACTSTATUS and lstatus.LookupType='CONTACTSTATUS'  " +
                     strcond +
                     " and pv.typearea='BKK' " +
                    " group by  e.EmpCode,e.EmpFname_TH,e.EmpLName_TH, " +
                    " lstatus.LookupValue ,ch.ChannelName,CONVERT(date, o.CreateDate,103),pv.typearea) as dtbkk" +

                    " full join (select e2.EmpCode,e2.EmpFname_TH,e2.EmpLName_TH,lstatus2.LookupValue as Call_status" +
                    " ,ch2.ChannelName,sum(o2.TotalPrice) as total_price,count(cin2.id) as count_call,count(o2.id) as count_order,CONVERT(date, o2.CreateDate,103) as orderdate,pv2.typearea" +

                    " from OrderInfo o2 " +
                    " inner join emp e2 on e2.EmpCode =o2.CreateBy" +
                    " inner join emp eupdate2 on eupdate2.EmpCode =o2.UpdateBy" +
                    " inner join Channel ch2 on ch2.ChannelCode = o2.ChannelCode" +
                    " inner join CallInfo cin2 on cin2.id = o2.callinfo_id and cin2.CreateBy=o2.CreateBy" +
                    " inner join OrderTransport ot2 on ot2.OrderCode= o2.OrderCode and ot2.AddressType='01'" +
                    " inner join Province pv2 on pv2.ProvinceCode = ot2.Province" +
                    " inner join Lookup lstatus2 on lstatus2.LookupCode = cin2.CONTACTSTATUS and lstatus2.LookupType='CONTACTSTATUS'" +
                    strcondSub +
                    " and pv2.TypeArea='UPC'" +
                    " group by  e2.EmpCode,e2.EmpFname_TH,e2.EmpLName_TH, " +
                    " lstatus2.LookupValue ,ch2.ChannelName,CONVERT(date, o2.CreateDate,103),pv2.typearea) as dtupc on dtbkk.EmpCode=dtupc.EmpCode and dtbkk.orderdate=dtupc.orderdate and dtbkk.ChannelName = dtupc.ChannelName and dtbkk.EmpCode = dtupc.EmpCode"
                    ;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);


                LOrder = (from DataRow dr in dt.Rows

                          select new CountReportResultCallinInfo()
                          {
                              CountReportResultCallin = Convert.ToInt32(dr["countRow"])
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LOrder.Count > 0)
            {
                count = LOrder[0].CountReportResultCallin;
            }

            return count;

        }
    }
}
