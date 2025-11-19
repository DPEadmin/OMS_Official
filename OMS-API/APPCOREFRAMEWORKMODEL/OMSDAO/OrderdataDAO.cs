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
    public class OrderdataDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public string InsertMKOrderdata(orderdataInfo oinfo)
        {
            int i = 0;
            
            List<String> lSQL = new List<string>();
            string strsql = "";
            string genBranchOrderCode = "";

            int orderCode = getMaxOrder(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());

            String genOrderCode = "SO" + (DateTime.Now.Year + 543).ToString() + DateTime.Now.ToString("MM") + String.Format("{0:00000}", orderCode);
            if (oinfo.BranchCode.ToString() == "" || oinfo.BranchCode.ToString() == null)
            {
                genBranchOrderCode = "X0";
            }
            else
            {
                genBranchOrderCode = getBranchcode(oinfo.BranchCode.ToString());
            }

            strsql = "Insert into " + dbName + ".dbo.OrderInfo (OrderCode,OrderStatusCode,CampaignCategoryCode,OrderStateCode,TotalPrice,SubTotalPrice,DeliveryDate,Vat,TransportPrice,Customerpay,ReturnCashAMount,CreateDate,CreateBy,UpdateDate,UpdateBy,RunningNo,CustomerCode,CustomerPhone,BranchCode,SALEORDERTYPE,LandmarkLat,LandmarkLng,OrderNote,ChannelCode,FlagDelete,FlagApproved,BranchOrderID) " +
                     "VALUES (" +
                     "'" + genOrderCode + "'," +
                     "'" + oinfo.OrderStatusCode + "'," +
                     "'" + oinfo.CampaignCategoryCode + "'," +
                     "'" + oinfo.OrderStateCode + "'," +
                     "'" + oinfo.TotalPrice + "'," + //รวมราคา PromotionDetail Price หลังจากหัก DiscountAmount และ DiscountPercent Save ลง OrderInfo ในฟิลด์ TotalPrice
                     "'" + oinfo.SubTotalPrice + "'," + //รวมราคาสินค้าหลังคิด Vat เป็นราคาสุทธิ
                     "'" + oinfo.DeliveryDate + "'," +
                     "'" + oinfo.Vat + "'," +
                     "'" + oinfo.TransportPrice + "'," +
                     "'" + Convert.ToDecimal(oinfo.Customerpay) + "'," +
                     "'" + oinfo.ReturnCashAMount + "'," +
                     "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                     "'" + oinfo.CreateBy + "'," +
                     "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                     "'" + oinfo.UpdateBy + "'," +
                     "'" + orderCode + "'," +
                     "'" + oinfo.CustomerCode + "'," +
                     "'" + oinfo.CustomerPhone + "'," +
                     "'" + oinfo.BranchCode + "'," +
                     "'" + oinfo.SALEORDERTYPE + "'," +
                     "'" + oinfo.LandmarkLat + "'," +
                     "'" + oinfo.LandmarkLng + "'," +
                     "'" + oinfo.OrderNote + "'," +
                     "'" + oinfo.ChannelCode + "'," +
                     "'" + "N" + "'," +
                     "'" + oinfo.FlagApproved + "'," +
                     "'" + oinfo.CampaignCategoryCode+oinfo.BranchCode+genBranchOrderCode + "'" +
                     ")";
            lSQL.Add(strsql);

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            foreach (string strq in lSQL)
            {
                com.CommandText = strq;
                com.CommandType = System.Data.CommandType.Text;
                i = db.ExcuteBeginTransectionText(com);
            }
            if (i > 0)
            {
                return genOrderCode;
            }
            return "";
        }

        public string InsertRetailOrderdata(orderdataInfo oinfo)
        {
            int i = 0;

            List<String> lSQL = new List<string>();
            string strsql = "";
            string genBranchOrderCode = "";
            string channel = "";
            string prefixmerchant = oinfo.MerchantMapCode.Substring(0, 3);
            int orderCode = getMaxOrder(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());

            String genOrderCode = "";

            
                genOrderCode = oinfo.Prefix + prefixmerchant + (DateTime.Now.Year + 543).ToString() + DateTime.Now.ToString("MM") + String.Format("{0:00000}", orderCode);
            

            if (oinfo.BranchCode.ToString() == "" || oinfo.BranchCode.ToString() == null)
            {
                genBranchOrderCode = "X0";
            }
            else
            {
                genBranchOrderCode = getBranchcode(oinfo.BranchCode.ToString());
            }

            

            strsql = "Insert into " + dbName + ".dbo.OrderInfo (OrderCode,OrderType,OrderStatusCode,CampaignCategoryCode,OrderStateCode,TotalPrice,SubTotalPrice,DeliveryDate,Vat,PercentVat,TransportPrice,Customerpay,ReturnCashAMount,CreateDate,CreateBy,UpdateDate,UpdateBy,RunningNo,CustomerCode,CustomerPhone,BranchCode,SALEORDERTYPE,LandmarkLat,LandmarkLng,OrderNote,ChannelCode,InventoryCode,Callinfo_id,TaxID,FlagDelete,FlagApproved,FlagMediaPlan,BranchOrderID,MediaPhone,OrderSituation,LotNo,PlatformCode,GetInsurance_TIB,MerchantMapCode) " +
                     "VALUES (" +
                     "'" + genOrderCode + "'," +
                     "'" + oinfo.OrderType + "'," +
                     "'" + oinfo.OrderStatusCode + "'," +
                     "'" + oinfo.CampaignCategoryCode + "'," +
                     "'" + oinfo.OrderStateCode + "'," +
                     "'" + oinfo.TotalPrice + "'," + //รวมราคา PromotionDetail Price หลังจากหัก DiscountAmount และ DiscountPercent Save ลง OrderInfo ในฟิลด์ TotalPrice
                     "'" + oinfo.SubTotalPrice + "'," + //รวมราคาสินค้าหลังคิด Vat เป็นราคาสุทธิ
                     "'" + oinfo.DeliveryDate + "'," +
                     "'" + oinfo.Vat + "'," +
                     "'" + oinfo.PercentVat + "'," +
                     "" + (oinfo.TransportPrice = (oinfo.TransportPrice != null )? oinfo.TransportPrice : 0 )+ "," +
                     "'" + Convert.ToDecimal(oinfo.Customerpay) + "'," +
                     "'" + oinfo.ReturnCashAMount + "'," +
                     "'" + oinfo.CreateDate + "'," +
                     "'" + oinfo.CreateBy + "'," +
                     "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                     "'" + oinfo.UpdateBy + "'," +
                     "'" + orderCode + "'," +
                     "'" + oinfo.CustomerCode + "'," +
                     "'" + oinfo.CustomerPhone + "'," +
                     "'" + oinfo.BranchCode + "'," +
                     "'" + oinfo.SALEORDERTYPE + "'," +
                     "'" + oinfo.LandmarkLat + "'," +
                     "'" + oinfo.LandmarkLng + "'," +
                     "'" + oinfo.OrderNote + "'," +
                     "'" + oinfo.ChannelCode + "'," +
                     "'" + oinfo.InventoryCode + "'," +
                     "'" + oinfo.CallInfoID + "'," +
                     "'" + oinfo.TaxID + "'," +
                     "'" + "N" + "'," +
                     "'" + oinfo.FlagApproved + "'," +
                     "'" + oinfo.FlagMediaPlan + "'," +
                     "'" + oinfo.CampaignCategoryCode + oinfo.BranchCode + genBranchOrderCode + "'," +
                     "'" + oinfo.MediaPhone + "'," +
                     "'" + oinfo.OrderSituation + "'," +
                     "'" + oinfo.LotNo + "'," +
                     "'" + oinfo.PlatformCode + "'," + 
                     "'" + oinfo.GetInsurance_TIB + "'," +
                     "'" + oinfo.MerchantMapCode + "'" +
                     ")";
            lSQL.Add(strsql);

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            foreach (string strq in lSQL)
            {
                com.CommandText = strq;
                com.CommandType = System.Data.CommandType.Text;
                i = db.ExcuteBeginTransectionText(com);
            }
            if (i > 0)
            {
                return genOrderCode;
            }
            return "";
        }


        public string InsertOrderdata(List<orderdataInfo> oinfo, string empcode)
        {
            int i = 0;
            Decimal? price = 0;
            Decimal? vat = 0; Decimal? sumPrice = 0; Decimal? SubTotalPrice = 0; Decimal? totalPrice = 0;

            List<String> lSQL = new List<string>();
            string strsql = "";

            int orderCode = getMaxOrder(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());

            String genOrderCode = "SO" + (DateTime.Now.Year + 543).ToString() + DateTime.Now.ToString("MM") + String.Format("{0:00000}", orderCode);

            foreach (var info in oinfo.ToList())
            {
                strsql = "Insert into " + dbName + ".dbo.OrderDetail (OrderCode,PromotionCode,MerchantCode,Price,ProductCode,PromotionDetailId,Unit,DiscountAmount,DiscountPercent,Vat,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete,Amount,NetPrice,TotalPrice) " +
                         "VALUES (" +
                         "'" + genOrderCode + "'," +
                         "'" + info.PromotionCode + "'," +
                         "'" + info.MerchantCode + "'," +
                         "" + info.Price + "," +
                         "'" + info.ProductCode + "'," +
                         "'" + info.PromotionDetailId + "'," +
                         "'" + info.Unit + "'," +
                         "'" + info.DiscountAmount + "'," +
                         "'" + info.DiscountPercent + "'," +
                         "" + info.Vat + "," +
                         
                         "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                         "'" + empcode + "'," +
                         "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                         "'" + empcode + "'," +
                         "'" + info.FlagDelete + "'," +
                         "" + info.Amount + "," +
                         "" + info.NetPrice + "," +
                         "" + info.TotalPrice + "" +
                        ")";
                              
                price += info.Price; //รวมราคาจาก PromotionDetail Price
                SubTotalPrice += info.TotalPrice;
                vat += info.Vat; //รวมราคา Vat
                totalPrice = SubTotalPrice + vat;
                lSQL.Add(strsql);
            }
            strsql = "Insert into " + dbName + ".dbo.OrderInfo (OrderCode,OrderType,OrderStatusCode,OrderStateCode,BUCode," +
                "TotalPrice,SubTotalPrice,Vat,CreateDate,CreateBy," +
                "UpdateDate,UpdateBy,RunningNo,CustomerCode,FlagDelete ) " +
                     "VALUES (" +
                     "'" + genOrderCode + "'," +
                     "'" + oinfo[0].OrderType + "'," +
                     "'" + "01" + "'," +
                     "'" + "01" + "'," +
                     "'" + oinfo[0].BUCode + "'," +

                     "'" + totalPrice + "'," + //รวมราคา PromotionDetail Price หลังจากหัก DiscountAmount และ DiscountPercent Save ลง OrderInfo ในฟิลด์ TotalPrice
                     "'" + SubTotalPrice + "'," + //รวมราคาสินค้าหลังคิด Vat เป็นราคาสุทธิ
                     "'" + vat + "'," +
                     "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                     "'" + empcode + "'," +

                     "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                     "'" + empcode + "'," +
                     "'" + orderCode + "'," +
                     "'" + oinfo[0].CustomerCode + "'," +
                     "'" + "N" + "'" +
                     ")";
            lSQL.Add(strsql);

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            foreach (string strq in lSQL)
            {
                com.CommandText = strq;
                com.CommandType = System.Data.CommandType.Text;
                i = db.ExcuteBeginTransectionText(com);
            }
            if (i > 0)
            {
                return genOrderCode;
            }
            return "";
        }
        public int getMaxOrder(String year, String month)
        {

            int maxOrder = 1;

            DataTable dt = new DataTable();

            string strsql = @" select isnull(max(isnull(runningno,0)),0) + 1 max_no from " + dbName + @".dbo.orderinfo
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
                    maxOrder = (dt.Rows[0]["max_no"] != null) ? int.Parse(dt.Rows[0]["max_no"].ToString()) : 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return maxOrder;
        }
        public int L_transportdataInsert(List<transportdataInfo> tinfo)
        {
            List<String> lSQL = new List<string>();
            string strsql = "";
            int i = 0;

            foreach (var info in tinfo.ToList())
            {
                strsql = "insert into " + dbName + ".dbo.OrderTransport (OrderCode, Address, SubDistrict, District, Province, Zipcode, TransportPrice, TransportType, CreateDate, CreateBy, UpdateDate, UpdateBy, AddressType, TransportTypeOther) values (" +
                             "'" + info.OrderCode + "', " +
                             "'" + info.Address + "', " +
                             "'" + info.SubDistrictCode + "', " +
                             "'" + info.DistrictCode + "', " +
                             "'" + info.ProvinceCode + "', " +
                             "'" + info.Zipcode + "', " +
                             "'" + info.TransportPrice + "', " +
                             "'" + info.TransportType + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             "'" + info.CreateBy + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             "'" + info.UpdateBy + "', " +
                             "'" + info.AddressType + "', " +
                             "'" + info.TransportTypeOther + "')";
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
        public int L_paymentdataInsert(List<paymentdataInfo> pinfo)
        {
            List<String> lSQL = new List<string>();
            string strsql = "";
            int i = 0;

            foreach (var info in pinfo.ToList())
            {
                strsql = "insert into " + dbName + ".dbo.OrderPayment (OrderCode, PaymentTypeCode, Payamount, CardType, CardNo, CardHolderName, CardExpMonth, CardExpYear, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete) values (" +
                             "'" + info.OrderCode + "', " +
                             "'" + info.PaymentTypeCode + "', " +
                             "'" + info.Payamount + "', " +
                             "'" + info.CardType + "', " +
                             "'" + info.CardNo + "', " +
                             "'" + info.CardHolderName + "', " +
                             "'" + info.CardExpMonth + "', " +
                             "'" + info.CardExpYear + "', " +
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

        public int L_inventorydataUpdate(List<InventoryDetailListReturn> iinfo)
        {
            List<String> lSQL = new List<string>();
            string strsql = "";
            int i = 0;

            foreach (var info in iinfo.ToList())
            {
                if (info.Balance >= 0)
                {
                    strsql = " Update " + dbName + ".dbo.InventoryBalance set " +

                             " Reserved = " + info.Reserved + "," +
                             " Balance = " + info.Balance + "," +
                             " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             " UpdateBy = '" + info.UpdateBy + "'" +
                             " where (InventoryCode = '" + info.InventoryCode + "') and (ProductCode = '" + info.ProductCode + "')";
                    lSQL.Add(strsql);
                }
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
        public int L_paymentMKInsert(paymentdataInfo pInfo)
        {
            int i = 0;

            string strsql = "insert into " + dbName + ".dbo.OrderPayment (OrderCode, PaymentTypeCode, Payamount, CardType, CardNo, CardHolderName, CardExpMonth, CardExpYear, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete) values (" +
                             "'" + pInfo.OrderCode + "', " +
                             "'" + pInfo.PaymentTypeCode + "', " +
                             "'" + pInfo.Payamount + "', " +
                             "'" + pInfo.CardType + "', " +
                             "'" + pInfo.CardNo + "', " +
                             "'" + pInfo.CardHolderName + "', " +
                             "'" + pInfo.CardExpMonth + "', " +
                             "'" + pInfo.CardExpYear + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             "'" + pInfo.CreateBy + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             "'" + pInfo.UpdateBy + "', " +
                             "'" + pInfo.FlagDelete + "')";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public List<returnpaymentdataInfo> ListPaymentCreditByCriteria(paymentdataInfo pInfo)
        {
            var LPayment = new List<returnpaymentdataInfo>();
            DataTable dt = new DataTable();
            try
            {
                string strsql = "select Paymenttypecode,PayAmount,Installment,InstallmentPrice, FirstInstallment" +
                                ", CardIssuename, CardNo, CardType, CVCNo, CardHolderName, CardExpMonth, CardExpYear" +
                                ", CitizenId, BirthDate, PaymentGateway, Bankcode, BankBranch, AccountName" +
                                ", AccountType, AccountNo, MpayNum, MpayName, PaymentOtherDetail " +
                                "from "+ dbName + ".dbo.OrderPayment where OrderCode ="+"'"+ pInfo.OrderCode + "'";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPayment = (from DataRow dr in dt.Rows

                                select new returnpaymentdataInfo()
                                {
                                    PaymentTypeCode = dr["Paymenttypecode"].ToString().Trim(),
                                    Payamount = (dr["PayAmount"].ToString() != "") ? Convert.ToInt32(dr["PayAmount"]) : 0,
                                    Installment = dr["Installment"].ToString().Trim(),
                                    InstallmentPrice = dr["InstallmentPrice"].ToString().Trim(),
                                    FirstInstallment = dr["FirstInstallment"].ToString().Trim(),
                                    CardIssuename = dr["CardIssuename"].ToString().Trim(),
                                    CardNo = dr["CardNo"].ToString().Trim(),
                                    CardType = dr["CardType"].ToString().Trim(),
                                    CVCNo = dr["CVCNo"].ToString().Trim(),
                                    CardHolderName = dr["CardHolderName"].ToString().Trim(),
                                    CardExpMonth = dr["CardExpMonth"].ToString().Trim(),
                                    CardExpYear = dr["CardExpYear"].ToString().Trim(),
                                    CitizenId = dr["CitizenId"].ToString().Trim(),
                                    BirthDate = dr["BirthDate"].ToString().Trim(),
                                    PaymentGateway = dr["PaymentGateway"].ToString().Trim(),
                                    BankCode = dr["Bankcode"].ToString().Trim(),
                                    BankBranch = dr["BankBranch"].ToString().Trim(),
                                    AccountName = dr["AccountName"].ToString().Trim(),
                                    AccountType = dr["AccountType"].ToString().Trim(),
                                    AccountNo = dr["AccountNo"].ToString().Trim(),
                                    MpayNum = dr["MpayNum"].ToString().Trim(),
                                    MpayName = dr["MpayName"].ToString().Trim(),
                                    PaymentOtherdetail = dr["PaymentOtherDetail"].ToString().Trim(),
                                }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPayment;
        }
            public int L_paymentRetailInsert(paymentdataInfo pInfo)
            {
            int i = 0;

            string strsql = "insert into " + dbName + ".dbo.OrderPayment (OrderCode, PaymentTypeCode, Payamount, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete " +
            ", Installment, InstallmentPrice, FirstInstallment, CardIssuename, CardNo, CardType, CVCNo, CardHolderName, CardExpMonth, CardExpYear, CitizenId, BirthDate" +
            ", BankCode, BankBranch, AccountName, AccountType, AccountNo, PaymentOtherdetail, MpayNum, MpayName, PaymentGateway,MerchantMapCode) values (" +
                             "'" + pInfo.OrderCode + "', " +
                             "'" + pInfo.PaymentTypeCode + "', " +
                             "'" + pInfo.Payamount + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             "'" + pInfo.CreateBy + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             "'" + pInfo.UpdateBy + "', " +
                             "'" + pInfo.FlagDelete + "'," +
                             "'" + pInfo.Installment + "'," +
                             "'" + pInfo.InstallmentPrice + "'," +
                             "'" + pInfo.FirstInstallment + "'," +
                             "'" + pInfo.CardIssuename + "'," +
                             "'" + pInfo.CardNo + "'," +
                             "'" + pInfo.CardType + "'," +
                             "'" + pInfo.CVCNo + "'," +
                             "'" + pInfo.CardHolderName + "'," +
                             "'" + pInfo.CardExpMonth + "'," +
                             "'" + pInfo.CardExpYear + "'," +
                             "'" + pInfo.CitizenId + "'," +
                             "'" + pInfo.BirthDate + "'," +
                             "'" + pInfo.BankCode + "'," +
                             "'" + pInfo.BankBranch + "'," +
                             "'" + pInfo.AccountName + "'," +
                             "'" + pInfo.AccountType + "'," +
                             "'" + pInfo.AccountNo + "'," +
                             "'" + pInfo.PaymentOtherdetail + "'," +
                             "'" + pInfo.MpayNum + "'," +
                             "'" + pInfo.MpayName + "'," +
                             "'" + pInfo.PaymentGateway + "'," +
                             "'" + pInfo.MerchantMapCode + "'" +
                             ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int InsertLotNo(orderdataInfo pinfo)
        {
            int i = 0;

            string strsql = "insert into " + dbName + ".dbo.LazadaLotNo (LotNo, CreateDate) values (" +
                             "'" + pinfo.LotNo + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                             ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public string InsertMKOrderdetaildata(orderdataInfo oinfo)
        {
            int i = 0;
            Decimal? price = 0;
            Decimal? vat = 0; Decimal? sumPrice = 0; Decimal? SubTotalPrice = 0; Decimal? totalPrice = 0;

            string strsql = "";

       
         

            strsql = "Insert into " + dbName + ".dbo.OrderDetail (OrderCode,MerchantCode,Price,ProductCode,PromotionDetailId,ParentProductCode,Unit,DiscountAmount,DiscountPercent," +
                "Vat,FlagCombo,ComboCode,ComboName,PromotionCode,CampaignCode,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete,Amount,NetPrice,TotalPrice) " +
                         "VALUES (" +
                         "'" + oinfo.OrderCode + "'," +
                         "'" + oinfo.MerchantCode + "'," +
                         "" + ((oinfo.Price != null) ? oinfo.Price : 0) + "," +
                         "'" + oinfo.ProductCode + "'," +
                         "" + oinfo.PromotionDetailId + "," +
                         "'" + oinfo.ParentProductCode + "'," +
                         "'" + oinfo.Unit + "'," +
                         "" + ((oinfo.DiscountAmount != null) ? oinfo.DiscountAmount : "0") + "," +
                         "" + ((oinfo.DiscountPercent != null) ? oinfo.DiscountPercent : "0") + "," +
                         "" + oinfo.Vat + "," +
                         "'" + oinfo.FlagCombo + "'," +
                         "'" + oinfo.ComboCode + "'," +
                         "'" + oinfo.ComboName + "'," +
                         "'" + oinfo.PromotionCode + "',"+
                         "'" + oinfo.CampaignCode + "'," +
                         "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                         "'" + oinfo.CreateBy + "'," +
                         "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                         "'" + oinfo.UpdateBy + "'," +
                         "'" + oinfo.FlagDelete + "'," +
                         "" + ((oinfo.Amount != null) ? oinfo.Amount : "0") + "," +
                         "" + ((oinfo.NetPrice != null) ? oinfo.NetPrice : 0) + "," +
                         "" + ((oinfo.TotalPrice != null) ? oinfo.TotalPrice : 0) + "" +
                        ")";

                //price += oinfo.Price; //รวมราคาจาก PromotionDetail Price
                //SubTotalPrice += oinfo.TotalPrice;
                //vat += oinfo.Vat; //รวมราคา Vat
                //totalPrice = SubTotalPrice + vat;

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                i = db.ExcuteBeginTransectionText(com);
            
            if (i > 0)
            {
                return "1";
            }
            return "";
        }

        public string InsertRetailOrderdetaildata(orderdataInfo oinfo)
        {
            int i = 0;
            Decimal? price = 0;
            Decimal? vat = 0; Decimal? sumPrice = 0; Decimal? SubTotalPrice = 0; Decimal? totalPrice = 0;

            string strsql = "";

            strsql = "Insert into " + dbName + ".dbo.OrderDetail (OrderCode,MerchantCode,Price,ProductPrice,ProductCode,PromotionDetailId,ParentProductCode,ParentPromotionCode,Unit,DiscountAmount,DiscountPercent," +
                     "Vat,FlagCombo,ComboCode,ComboName,PromotionCode,CampaignCode,runningNo,LockAmountFlag,LockCheckbox,FreeShipping,FlagProSetHeader,PromotionTypeCode,PromotionTypeName,MOQFlag,InventoryCode,MinimumQty" +
                     ",ProductDiscountPercent,ProductDiscountAmount,FreeShippingBill,TradeFlag,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete,Amount,DefaultAmount,NetPrice,TransportPrice,SumPrice,TotalPrice" +
                     ",CampaignCategoryCode,ChannelCode,ShippingBrand,FlagMediaPlan) " +
                         "VALUES (" +
                         "'" + oinfo.OrderCode + "'," +
                         "'" + oinfo.MerchantCode + "'," +
                         "" + ((oinfo.Price != null) ? oinfo.Price : 0) + "," +
                         "" + ((oinfo.ProductPrice != null) ? oinfo.ProductPrice : 0) + "," +
                         "'" + oinfo.ProductCode + "'," +
                         "" + ((oinfo.PromotionDetailId != null) ? oinfo.PromotionDetailId : "0") + "," +
                         "'" + oinfo.ParentProductCode + "'," +
                         "'" + oinfo.ParentPromotionCode + "'," +
                         "'" + oinfo.Unit + "'," +
                         "" + ((oinfo.DiscountAmount != null) ? oinfo.DiscountAmount : "0") + "," +
                         "" + ((oinfo.DiscountPercent != null) ? oinfo.DiscountPercent : "0") + "," +
                         "" + oinfo.Vat + "," +
                         "'" + oinfo.FlagCombo + "'," +
                         "'" + oinfo.ComboCode + "'," +
                         "'" + oinfo.ComboName + "'," +
                         "'" + oinfo.PromotionCode + "'," +
                         "'" + oinfo.CampaignCode + "'," +
                         "'" + oinfo.runningNo + "'," +
                         "'" + oinfo.LockAmountFlag + "'," +
                         "'" + oinfo.LockCheckbox + "'," +
                         "'" + oinfo.FreeShipping + "'," +
                         "'" + oinfo.FlagProSetHeader + "'," +
                         "'" + oinfo.PromotionTypeCode + "'," +
                         "'" + oinfo.PromotionTypeName + "'," +
                         "'" + oinfo.MOQFlag + "'," +
                         "'" + oinfo.InventoryCode + "'," +
                         "" + ((oinfo.MinimumQty != null) ? oinfo.MinimumQty : 0) + "," +
                         "" + ((oinfo.ProductDiscountPercent != null) ? oinfo.ProductDiscountPercent : 0) + "," +
                         "" + ((oinfo.ProductDiscountAmount != null) ? oinfo.ProductDiscountAmount : 0) + "," +
                         "'" + oinfo.FreeShippingBill + "'," +
                         "'" + oinfo.TradeFlag + "'," +
                         "'" + oinfo.CreateDate + "'," +
                         "'" + oinfo.CreateBy + "'," +
                         "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                         "'" + oinfo.UpdateBy + "'," +
                         "'" + oinfo.FlagDelete + "'," +
                         "" + ((oinfo.Amount != null) ? oinfo.Amount : "0") + "," +
                         "" + ((oinfo.DefaultAmount != null) ? oinfo.DefaultAmount : "0") + "," +
                         "" + ((oinfo.NetPrice != null) ? oinfo.NetPrice : 0) + "," +
                         "" + ((oinfo.TransportPrice != null) ? oinfo.TransportPrice : 0) + "," +
                         "" + ((oinfo.SumPrice != null) ? oinfo.SumPrice : 0) + "," +
                         "" + ((oinfo.TotalPrice != null) ? oinfo.TotalPrice : 0) + "," +
                         "'" + oinfo.CampaignCategoryCode + "'," +
                         "'" + oinfo.ChannelCode + "'," +
                         "'" + oinfo.ShippingBrand + "'," +
                         "'" + oinfo.MediaPlanFlag + "'" +
                        ")";

            //price += oinfo.Price; //รวมราคาจาก PromotionDetail Price
            //SubTotalPrice += oinfo.TotalPrice;
            //vat += oinfo.Vat; //รวมราคา Vat
            //totalPrice = SubTotalPrice + vat;

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();

            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            if (i > 0)
            {
                return "1";
            }
            return "";
        }
        public int InsertMKTransportdata(transportdataInfo tInfo)
        {
            int i = 0;

            String strsql = "insert into " + dbName + ".dbo.OrderTransport (OrderCode, Address, SubDistrict, District, Province, Zipcode, TransportPrice, TransportType, CreateDate, CreateBy, UpdateDate, UpdateBy, AddressType, TransportTypeOther, BranchCode) values (" +
                             "'" + tInfo.OrderCode + "', " +
                             "'" + tInfo.Address + "', " +
                             "'" + tInfo.SubDistrictCode + "', " +
                             "'" + tInfo.DistrictCode + "', " +
                             "'" + tInfo.ProvinceCode + "', " +
                             "'" + tInfo.Zipcode + "', " +
                             "'" + tInfo.TransportPrice + "', " +
                             "'" + tInfo.TransportType + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             "'" + tInfo.CreateBy + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             "'" + tInfo.UpdateBy + "', " +
                             "'" + tInfo.AddressType + "', " +
                             "'" + tInfo.TransportTypeOther + "'," +
                             "'" + tInfo.BranchCode + "')";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int InsertRetailTransportdata(transportdataInfo tInfo)
        {
            int i = 0;

            String strsql = "insert into " + dbName + ".dbo.OrderTransport (OrderCode, CustomerCode,Address, SubDistrict, District, Province, Zipcode, TransportPrice, TransportType, CreateDate, CreateBy, UpdateDate, UpdateBy, AddressType, TransportTypeOther, InventoryCode, BranchCode ,MerchantMapCode) values (" +
                             "'" + tInfo.OrderCode + "', " +
                             "'" + tInfo.CustomerCode + "', " +
                             "'" + tInfo.Address + "', " +
                             "'" + tInfo.SubDistrictCode + "', " +
                             "'" + tInfo.DistrictCode + "', " +
                             "'" + tInfo.ProvinceCode + "', " +
                             "'" + tInfo.Zipcode + "', " +
                             "'" + tInfo.TransportPrice + "', " +
                             "'" + tInfo.TransportType + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             "'" + tInfo.CreateBy + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             "'" + tInfo.UpdateBy + "', " +
                             "'" + tInfo.AddressType + "', " +
                             "'" + tInfo.TransportTypeOther + "'," +
                             "'" + tInfo.InventoryCode + "'," +
                             "'" + tInfo.BranchCode + "'," +
                             "'" + tInfo.MerchantMapCode + "')";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public string getBranchcode(string branchcode)
        {

            int maxOrder = 1;

            DataTable dt = new DataTable();

            string strsql = @"SELECT NEXT VALUE FOR  SeqBranch" + branchcode + " AS BranchRunningNo ";

            try
            {
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                if (dt.Rows.Count > 0)
                {
                    maxOrder = (dt.Rows[0]["BranchRunningNo"] != null) ? int.Parse(dt.Rows[0]["BranchRunningNo"].ToString()) : 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return maxOrder.ToString();
        }

        public List<paymentdataInfo> LoadOrderPaymentRetail(paymentdataInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.OrderCode != null) && (pInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + pInfo.OrderCode + "%'";
            }

            DataTable dt = new DataTable();
            var Lpayment = new List<paymentdataInfo>();

            try
            {
                string strsql = " SELECT  Id, OrderCode, PaymentTypeCode, PayAmount, Installment, InstallmentPrice, FirstInstallment, " + 
                                " CardIssuename, CardNo, CardType, CVCNo, CardOwnerName, CardHolderName, CardExpMonth, CardExpYear, CitizenId, BirthDate, " + 
                                " BankCode, BankBranch, AccountName, AccountType, AccountNo, PaymentOtherDetail, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete" +
                                " from " + dbName + ".dbo.OrderPayment o " +
                                " where o.FlagDelete ='N' " +
                                strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                Lpayment = (from DataRow dr in dt.Rows

                              select new paymentdataInfo()
                              {
                                  OrderCode = dr["OrderCode"].ToString(),
                                  PaymentTypeCode = dr["PaymentTypeCode"].ToString().Trim(),
                                  Payamount = (dr["PayAmount"].ToString() != "") ? Convert.ToDouble(dr["PayAmount"]) : 0,
                                  FlagDelete = dr["FlagDelete"].ToString().Trim(),
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return Lpayment;
        }

        public List<transportdataInfo> LoadOrderTransportRetail(transportdataInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.OrderCode != null) && (pInfo.OrderCode != ""))
            {
                strcond += " and  t.OrderCode like '%" + pInfo.OrderCode + "%'";
            }

            DataTable dt = new DataTable();
            var Ltransport = new List<transportdataInfo>();

            try
            {
                string strsql = " SELECT  t.Id, t.OrderCode, t.CustomerCode, t.Address, t.Subdistrict, t.District, t.Province, t.Zipcode, t.TransportPrice, t.TransportType, " +
                                " t.CreateDate, t.CreateBy, t.UpdateDate, t.UpdateBy, t.AddressType, t.TransportTypeOther, t.BranchCode, t.InventoryCode, p.ProvinceName, s.SubDistrictName, d.DistrictName " +
                                " from " + dbName + ".dbo.OrderTransport as t LEFT OUTER JOIN " +
                                dbName + ".dbo.Province AS p ON t.Province = p.ProvinceCode LEFT OUTER JOIN " +
                                dbName + ".dbo.District AS d ON t.District = d.DistrictCode LEFT OUTER JOIN " +
                                dbName + ".dbo.SubDistrict AS s ON t.Subdistrict = s.SubDistrictCode " +
                                " where 1 = 1 " +
                                strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                Ltransport = (from DataRow dr in dt.Rows

                            select new transportdataInfo()
                            {
                                OrderTransportID = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                OrderCode = dr["OrderCode"].ToString(),
                                CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                TransportType = dr["TransportType"].ToString().Trim(),
                                AddressType = dr["AddressType"].ToString().Trim(),
                                TransportPrice = dr["TransportPrice"].ToString().Trim(),
                                Address = dr["Address"].ToString().Trim(),
                                ProvinceCode = dr["Province"].ToString().Trim(),
                                ProvinceName = dr["ProvinceName"].ToString().Trim(),
                                DistrictCode = dr["District"].ToString().Trim(),
                                DistrictName = dr["DistrictName"].ToString().Trim(),
                                SubDistrictCode = dr["Subdistrict"].ToString().Trim(),
                                SubDistrictName = dr["DistrictName"].ToString().Trim(),
                                Zipcode = dr["Zipcode"].ToString().Trim(),
                                InventoryCode = dr["InventoryCode"].ToString().Trim(),
                                CreateBy = dr["CreateBy"].ToString().Trim(),
                                UpdateBy = dr["UpdateBy"].ToString().Trim(),

                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return Ltransport;
        }

        public int UpdateRetailOrderdata(orderdataInfo oInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.OrderInfo set " +

                            " OrderType = '" + oInfo.OrderType + "'," +
                            " OrderStatusCode = '" + oInfo.OrderStatusCode + "'," +
                            " CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'," +
                            " OrderStateCode = '" + oInfo.OrderStateCode + "'," +
                            " TotalPrice = '" + oInfo.TotalPrice + "'," +
                            " SubTotalPrice = '" + oInfo.SubTotalPrice + "'," +
                            " DeliveryDate = '" + oInfo.DeliveryDate + "'," +
                            " TransportPrice = '" + oInfo.TransportPrice + "'," +
                            " Vat = '" + oInfo.Vat + "'," +
                            " PercentVat = '" + oInfo.PercentVat + "'," +
                            " Customerpay = '" + Convert.ToDecimal(oInfo.Customerpay) + "'," +
                            " ReturnCashAMount = '" + Convert.ToDecimal(oInfo.ReturnCashAMount) + "'," +
                            " CustomerCode = '" + oInfo.CustomerCode + "'," +
                            " CustomerPhone = '" + oInfo.CustomerPhone + "'," +
                            " BranchCode = '" + oInfo.BranchCode + "'," +
                            " SALEORDERTYPE = '" + oInfo.SALEORDERTYPE + "'," +
                            " LandmarkLat = '" + oInfo.LandmarkLat + "'," +
                            " LandmarkLng = '" + oInfo.LandmarkLng + "'," +
                            " OrderNote = '" + oInfo.OrderNote + "'," +
                            " ChannelCode = '" + oInfo.ChannelCode + "'," +
                            " FlagApproved = '" + oInfo.FlagApproved + "'," +
                            " BranchOrderID = '" + oInfo.BranchOrderID + "'," +
                            " InventoryCode = '" + oInfo.InventoryCode + "'," +
                            " TaxID = '" + oInfo.TaxID + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy = '" + oInfo.UpdateBy + "'," +
                            " MediaPhone = '" + oInfo.MediaPhone + "'," +

                            " FlagMediaPlan = '" + oInfo.FlagMediaPlan + "'" +
                            " where OrderCode ='" + oInfo.OrderCode + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteOrderDetailofRetailTakeOrder(orderdataInfo oInfo)
        {
            int i = 0;

            string strsql = " Delete " + dbName + ".dbo.OrderDetail where Id = " + oInfo.OrderDetailID;

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateRetailOrderPayment(paymentdataInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.OrderPayment set " +

                            " PaymentTypeCode = '" + pInfo.PaymentTypeCode + "'," +
                            " Payamount = '" + pInfo.Payamount + "'," +

                            " Installment = '" + pInfo.Installment + "'," +
                            " InstallmentPrice = '" + pInfo.InstallmentPrice + "'," +
                            " FirstInstallment = '" + pInfo.FirstInstallment + "'," +
                             " CardIssuename = '" + pInfo.CardIssuename + "'," +
                             " CardNo = '" + pInfo.CardNo + "'," +
                             " CardType = '" + pInfo.CardType + "'," +
                             " CVCNo = '" + pInfo.CVCNo + "'," +
                             " CardHolderName = '" + pInfo.CardHolderName + "'," +
                             " CardExpMonth = '" + pInfo.CardExpMonth + "'," +
                             " CardExpYear = '" + pInfo.CardExpYear + "'," +
                             " CitizenId = '" + pInfo.CitizenId + "'," +
                             " BirthDate = '" + pInfo.BirthDate + "'," +
                             " BankCode = '" + pInfo.BankCode + "'," +
                             " BankBranch = '" + pInfo.BankBranch + "'," +

                             " AccountName = '" + pInfo.AccountName + "'," +
                             " AccountType = '" + pInfo.AccountType + "'," +
                             " AccountNo = '" + pInfo.AccountNo + "'," +
                             " PaymentOtherdetail = '" + pInfo.PaymentOtherdetail + "'," +
                             " MpayNum = '" + pInfo.MpayNum + "'," +
                             " MpayName = '" + pInfo.MpayName + "'," +
                             " PaymentGateway = '" + pInfo.PaymentGateway + "'," +

                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy = '" + pInfo.UpdateBy + "'" +
                            " where OrderCode ='" + pInfo.OrderCode + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateRetailOrderTransport(transportdataInfo tInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.OrderTransport set " +

                            " OrderCode = '" + tInfo.OrderCode + "'," +
                            " CustomerCode = '" + tInfo.CustomerCode + "'," +
                            " Address = '" + tInfo.Address + "'," +
                            " SubDistrict = '" + tInfo.SubDistrictCode + "'," +
                            " District = '" + tInfo.DistrictCode + "'," +
                            " Province = '" + tInfo.ProvinceCode + "'," +
                            " Zipcode = '" + tInfo.Zipcode + "'," +
                            " TransportPrice = '" + tInfo.TransportPrice + "'," +
                            " TransportType = '" + tInfo.TransportType + "'," +
                            " AddressType = '" + tInfo.AddressType + "'," +
                            " TransportTypeOther = '" + tInfo.TransportTypeOther + "'," +
                            " InventoryCode = '" + tInfo.InventoryCode + "'," +
                            " BranchCode = '" + tInfo.BranchCode + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy = '" + tInfo.UpdateBy + "'" +
                            " where Id = " + tInfo.OrderTransportID;


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<OrderTransportListReturn> LoadOrderTransprtbyLatestUpdate(OrderTransportInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode = '" + oInfo.CustomerCode + "' ";
            }

            DataTable dt = new DataTable();
            var LOrdertransport = new List<OrderTransportListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.OrderTransport c " +
                                " where 1=1 " + strcond;

                strsql += " ORDER BY c.UpdateDate DESC ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrdertransport = (from DataRow dr in dt.Rows

                             select new OrderTransportListReturn()
                             {
                                 OrderTransportId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 OrderCode = dr["OrderCode"].ToString().Trim(),
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                 Address = dr["Address"].ToString().Trim(),
                                 SubDistrictCode = dr["Subdistrict"].ToString().Trim(),
                                 DistrictCode = dr["District"].ToString().Trim(),
                                 ProvinceCode = dr["Province"].ToString().Trim(),
                                 Zipcode = dr["Zipcode"].ToString().Trim(),
                                 TransportPrice = (dr["TransportPrice"].ToString() != "") ? Convert.ToDouble(dr["TransportPrice"]) : 0,
                                 TransportType = dr["TransportType"].ToString().Trim(),
                                 AddressType = dr["AddressType"].ToString().Trim(),
                                 TransportTypeOther = dr["TransportTypeOther"].ToString().Trim(),
                                 CreateDate = dr["CreateDate"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString().Trim(),
                                 UpdateDate = dr["UpdateDate"].ToString().Trim(),
                                 UpdateBy = dr["UpdateBy"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrdertransport;
        }

    }
}
