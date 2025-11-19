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
using System.Globalization;

namespace APPCOREMODEL.OMSDAO
{
    public class PODAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int UpdatePO(POInfo poInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.PO set ";

            //found wrong condition (poInfo.Price != null) ---> (poInfo.POCode != null)
            if ((poInfo.POCode != null) && (poInfo.POCode != ""))
            {
                strsql += " POCode = '" + poInfo.POCode + "',";
            }

            if ((poInfo.PODate != null) && (poInfo.PODate != ""))
            {
                strsql += " PODate = '" + poInfo.PODate + "',";
            }

            if ((poInfo.SupplierCode != null) && (poInfo.SupplierCode != ""))
            {
                strsql += " SupplierCode = '" + poInfo.SupplierCode + "',";
            }

            if ((poInfo.InventoryCode != null) && (poInfo.InventoryCode != ""))
            {
                strsql += " InventoryCode = '" + poInfo.InventoryCode + "',";
            }

            if ((poInfo.Price != null) && (poInfo.Price != 0))
            {
                strsql += " Price = '" + poInfo.Price + "',";
            }

            if ((poInfo.StatusCode != null) && (poInfo.StatusCode != ""))
            {
                strsql += " Status = '" + poInfo.StatusCode + "',";
            }
            if ((poInfo.RequestDate != null) && (poInfo.RequestDate != ""))
            {
                strsql += " RequestDate = '" + DateTime.ParseExact(poInfo.RequestDate.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture) + "',";
            }
            if ((poInfo.ExpectDate != null) && (poInfo.ExpectDate != ""))
            {
                strsql += " ExpectDate = '" + DateTime.ParseExact(poInfo.RequestDate.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture) + "',";
            }
            if ((poInfo.Credit != null) && (poInfo.Credit != ""))
            {
                strsql += " Credit = '" + poInfo.Credit + "',";
            }
            if ((poInfo.POObjectiveCode != null) && (poInfo.POObjectiveCode != ""))
            {
                strsql += " POObjective = '" + poInfo.POObjectiveCode + "',";
            }
            if ((poInfo.Description != null) && (poInfo.Description != ""))
            {
                strsql += " Description = '" + poInfo.Description + "',";
            }
            if ((poInfo.PaymentMethodCode != null) && (poInfo.PaymentMethodCode != ""))
            {
                strsql += " PaymentMethod = '" + poInfo.PaymentMethodCode + "',";
            }
            if ((poInfo.PaymentTermCode != null) && (poInfo.PaymentTermCode != ""))
            {
                strsql += " PaymentTerm = '" + poInfo.PaymentTermCode + "',";
            }

            strsql += " UpdateBy = '" + poInfo.UpdateBy + "'," +
                        " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                        " where Id =" + poInfo.POId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeletePO(POInfo poInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.PO set FlagDelete = 'Y' where Id in (" + poInfo.POId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int Insertpo(POInfo poInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO PO  (POCode,SupplierCode,InventoryCode, Price,Status,RequestDate,ExpectDate,POObjective,Description,PaymentTerm,PaymentMethod,PODate,Credit,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + poInfo.POCode + "'," +
                           "'" + poInfo.SupplierCode + "'," +
                           "'" + poInfo.InventoryCode + "'," +
                           "'" + poInfo.Price + "'," +
                           "'" + poInfo.StatusCode + "'," +
                           "'" + DateTime.Parse(poInfo.RequestDate).ToString("MM/dd/yyyy HH:mm:ss") + @"'," +
                           "'" + DateTime.Parse(poInfo.ExpectDate).ToString("MM/dd/yyyy HH:mm:ss") + @"'," +
                           "'" + poInfo.POObjectiveCode + "'," +
                           "'" + poInfo.Description + "'," +
                           "'" + poInfo.PaymentTermCode + "'," +
                           "'" + poInfo.PaymentMethodCode + "'," +
                           "'" + poInfo.PODate + "'," +
                           "'" + poInfo.Credit + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + poInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + poInfo.UpdateBy + "'," +
                           "'" + poInfo.FlagDelete + "'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<POListReturn> ListPONopagingByCriteria(POInfo poInfo)
        {
            string strcond = "";

            if ((poInfo.POId != null) && (poInfo.POId != 0))
            {
                strcond += " and  c.Id =" + poInfo.POId;
            }

            if ((poInfo.POCode != null) && (poInfo.POCode != ""))
            {
                strcond += " and  c.POCode like '%" + poInfo.POCode + "%'";
            }

            if ((poInfo.POCodeValidate != null) && (poInfo.POCodeValidate != ""))
            {
                strcond += " and  c.POCode = '" + poInfo.POCodeValidate + "'";
            }

            if ((poInfo.StatusCode != null) && (poInfo.StatusCode != ""))
            {
                strcond += " and  c.Status = '" + poInfo.StatusCode + "'";
            }

            DataTable dt = new DataTable();
            var LPO = new List<POListReturn>();

            try
            {
                string strsql = " select pm.LookupCode as PaymentMethodCode,c.PaymentMethod,pm.LookupValue,py.PaymentTypeName," +
                                "pt.LookupCode as PaymentTermCode,pt.LookupValue as PaymentTermName," +
                                " po.LookupCode as POObjectiveCode,po.LookupValue as POObjectiveName," +
                                "c.*,s.SupplierName,i.InventoryName,stat.Name as StatusName from " + dbName + ".dbo.PO c " +
                                " left join Supplier s on c.SupplierCode = s.SupplierCode " +
                                " left join Inventory i on c.InventoryCode = i.InventoryCode " +
                                " left join WF_Task_List t on c.Id = t.OMS_id " +
                                " left join WF_Status stat on t.Status = stat.Code  " +
                                " left join Lookup pm on c.PaymentMethod = pm.LookupCode and pm.LookupType='PAYMENTMETHOD' " +
                                " left join Lookup pt on c.PaymentTerm = pt.LookupCode and pt.LookupType='PAYMENTTERM' " +
                               " left join Lookup po on c.POObjective = po.LookupCode and po.LookupType='POOBJECTTIVE' " +
                               " left join PaymentType py on c.PaymentMethod = py.PaymentTypeCode " +
                             " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPO = (from DataRow dr in dt.Rows

                       select new POListReturn()
                       {
                           POId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                           POCode = dr["POCode"].ToString().Trim(),
                           SupplierCode = dr["SupplierCode"].ToString().Trim(),
                           SupplierName = dr["SupplierName"].ToString().Trim(),
                           InventoryCode = dr["InventoryCode"].ToString().Trim(),
                           InventoryName = dr["InventoryName"].ToString().Trim(),
                           StatusCode = dr["Status"].ToString().Trim(),
                           Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                           PODate = dr["PODate"].ToString(),
                           RequestDate = dr["RequestDate"].ToString(),
                           ExpectDate = dr["ExpectDate"].ToString(),
                           POObjectiveCode = dr["POObjectiveCode"].ToString(),
                           POObjectiveName = dr["POObjectiveName"].ToString(),
                           PaymentMethodCode = dr["PaymentMethod"].ToString(),
                           PaymentMethodName = dr["PaymentTypeName"].ToString(),
                           PaymentTermCode = dr["PaymentTermCode"].ToString(),
                           PaymentTermName = dr["PaymentTermName"].ToString(),
                           Description = dr["Description"].ToString(),
                           CreateBy = dr["CreateBy"].ToString(),
                           CreateDate = dr["CreateDate"].ToString(),
                           UpdateBy = dr["UpdateBy"].ToString(),
                           UpdateDate = dr["UpdateDate"].ToString(),
                           FlagDelete = dr["FlagDelete"].ToString(),
                           StatusName = dr["StatusName"].ToString(),
                           Credit = dr["Credit"].ToString(),

                       }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPO;
        }

        public List<POListReturn> ListPOByCriteria(POInfo poInfo)
        {
            string strcond = "";

            if ((poInfo.POId != null) && (poInfo.POId != 0))
            {
                strcond += " and  c.Id =" + poInfo.POId;
            }
            if ((poInfo.POCode != null) && (poInfo.POCode != ""))
            {
                strcond += " and  c.POCode like '%" + poInfo.POCode + "%'";
            }
            if ((poInfo.SupplierCode != null) && (poInfo.SupplierCode != "") && (poInfo.SupplierCode != "-99"))
            {
                strcond += " and  c.SupplierCode like '%" + poInfo.SupplierCode + "%'";
            }
            if ((poInfo.InventoryCode != null) && (poInfo.InventoryCode != "") && (poInfo.InventoryCode != "-99"))
            {
                strcond += " and  c.InventoryCode like '%" + poInfo.InventoryCode + "%'";
            }
            if ((poInfo.StatusCode != null) && (poInfo.StatusCode != "") && (poInfo.StatusCode != "-99"))
            {
                strcond += " and  t.Status = '" + poInfo.StatusCode + "'";
            }
            if (((poInfo.CreateDate != "") && (poInfo.CreateDate != null)) && ((poInfo.CreateDateTo != "") && (poInfo.CreateDateTo != null)))
            {
                strcond += " and  c.CreateDate BETWEEN CONVERT(VARCHAR, '" + poInfo.CreateDate + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + poInfo.CreateDateTo + "', 103)),'23:59:59')";
            }
            if (((poInfo.ExpectDate != "") && (poInfo.ExpectDate != null)) && ((poInfo.ExpectDateTo != "") && (poInfo.ExpectDateTo != null)))
            {
                strcond += " and  c.ExpectDate BETWEEN CONVERT(VARCHAR, '" + poInfo.ExpectDate + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + poInfo.ExpectDateTo + "', 103)),'23:59:59')";
            }
            if (((poInfo.PODate != "") && (poInfo.PODate != null)) && ((poInfo.PODateTo != "") && (poInfo.PODateTo != null)))
            {
                strcond += " and  c.PODate BETWEEN CONVERT(VARCHAR, '" + poInfo.PODate + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + poInfo.PODateTo + "', 103)),'23:59:59')";
            }
            if ((poInfo.CreateByNameTH != null) && (poInfo.CreateByNameTH != ""))
            {
                strcond += " and  cb.EmpFname_TH like '%" + poInfo.CreateByNameTH + "%'";
            }

            DataTable dt = new DataTable();
            var LPO = new List<POListReturn>();

            try
            {
                string strsql = " select pm.LookupCode as PaymentMethodCode,pm.LookupValue as PaymentMethodName," +
                                " pt.LookupCode as PaymentTermCode,pt.LookupValue as PaymentTermName," +
                                " po.LookupCode as POObjectiveCode,po.LookupValue as POObjectiveName," +
                                " c.*,s.SupplierName,stat.Name as StatusName,t.Status AS WFStatusCode, " +
                                " cb.EmpFname_TH AS CreateByFNameTH, cb.EmpLName_TH AS CreateByLNameTH, ub.EmpFname_TH AS UpdateByFNameTH, ub.EmpLName_TH AS UpdateByLNameTH, i.InventoryName" +
                                " from " + dbName + ".dbo.PO c " +
                                " left join Supplier s on c.SupplierCode = s.SupplierCode " +
                                " left join Inventory i on c.InventoryCode = i.InventoryCode " +
                                " left join WF_Task_List t on c.Id = t.OMS_id " +
                                " left join WF_Status stat on t.Status = stat.Code  " +
                                " left join Lookup pm on c.PaymentMethod = pm.LookupCode and pm.LookupType='PAYMENTMETHOD' " +
                                " left join Lookup po on c.POObjective = po.LookupCode and po.LookupType='POOBJECTTIVE' " +
                                " left join Lookup pt on c.PaymentTerm = pt.LookupCode and pt.LookupType='PAYMENTTERM' " +
                                " left join Emp cb on cb.EmpCode = c.CreateBy " +
                                " left join Emp ub on ub.EmpCode = c.UpdateBy " +
                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + poInfo.rowOFFSet + " ROWS FETCH NEXT " + poInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPO = (from DataRow dr in dt.Rows

                       select new POListReturn()
                       {
                           POId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                           POCode = dr["POCode"].ToString().Trim(),
                           SupplierCode = dr["SupplierCode"].ToString().Trim(),
                           SupplierName = dr["SupplierName"].ToString().Trim(),
                           InventoryCode = dr["InventoryCode"].ToString().Trim(),
                           InventoryName = dr["InventoryName"].ToString().Trim(),
                           StatusCode = dr["Status"].ToString().Trim(),
                           StatusName = dr["StatusName"].ToString().Trim(),
                           WFStatusCode = dr["WFStatusCode"].ToString().Trim(),
                           Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                           RequestDate = dr["RequestDate"].ToString(),
                           ExpectDate = dr["ExpectDate"].ToString(),
                           POObjectiveCode = dr["POObjectiveCode"].ToString(),
                           POObjectiveName = dr["POObjectiveName"].ToString(),
                           PaymentMethodCode = dr["PaymentMethodCode"].ToString(),
                           PaymentMethodName = dr["PaymentMethodName"].ToString(),
                           PaymentTermCode = dr["PaymentTermCode"].ToString(),
                           PaymentTermName = dr["PaymentTermName"].ToString(),
                           Description = dr["Description"].ToString(),
                           PODate = dr["PODate"].ToString(),
                           CreateBy = dr["CreateBy"].ToString(),
                           CreateDate = dr["CreateDate"].ToString(),
                           UpdateBy = dr["UpdateBy"].ToString(),
                           UpdateDate = dr["UpdateDate"].ToString(),
                           CreateByNameTH = dr["CreateByFNameTH"].ToString() + " " + dr["CreateByLNameTH"].ToString(),
                           UpdateByNameTH = dr["UpdateByFNameTH"].ToString() + " " + dr["UpdateByLNameTH"].ToString(),
                           FlagDelete = dr["FlagDelete"].ToString(),

                       }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPO;
        }

        public int? CountPOSumpriceWorkListByCriteria(POInfo poInfo)
        {
            string strcond = "";

            int? count = 0;

            if ((poInfo.POId != null) && (poInfo.POId != 0))
            {
                strcond += " and  c.Id =" + poInfo.POId;
            }

            if ((poInfo.POCode != null) && (poInfo.POCode != ""))
            {
                strcond += " and  c.POCode like '%" + poInfo.POCode + "%'";
            }

            if ((poInfo.WFStatusCode != null) && (poInfo.WFStatusCode != ""))
            {
                strcond += " and  w.Status = '" + poInfo.WFStatusCode + "'";
            }
            if ((poInfo.StatusCode != null) && (poInfo.StatusCode != ""))
            {
                strcond += " and  c.Status = '" + poInfo.StatusCode + "'";
            }

            DataTable dt = new DataTable();
            var LPO = new List<POListReturn>();

            try
            {
                string strsql = "select count(c.Id) as countPO from " + dbName + ".dbo.PO c " +
                              " left join Supplier s on c.SupplierCode = s.SupplierCode " +
                              " left join Lookup t on c.Status = t.LookupValue and t.LookupType='POSTATUS' " +
                                " left join WF_Task_List w on c.Id = w.OMS_id " +
                               " left join WF_Status ws on w.Status = ws.Code " +

                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPO = (from DataRow dr in dt.Rows

                       select new POListReturn()
                       {
                           countPO = Convert.ToInt32(dr["countPO"])
                       }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LPO.Count > 0)
            {
                count = LPO[0].countPO;
            }

            return count;
        }

        public List<POListReturn> ListPOSumPriceWorklistByCriteria(POInfo poInfo)
        {
            string strcond = "";

            if ((poInfo.POId != null) && (poInfo.POId != 0))
            {
                strcond += " and  c.Id =" + poInfo.POId;
            }

            if ((poInfo.POCode != null) && (poInfo.POCode != ""))
            {
                strcond += " and  c.POCode like '%" + poInfo.POCode + "%'";
            }

            if ((poInfo.WFStatusCode != null) && (poInfo.WFStatusCode != ""))
            {
                strcond += " and  w.Status = '" + poInfo.WFStatusCode + "'";
            }
            if ((poInfo.StatusCode != null) && (poInfo.StatusCode != ""))
            {
                strcond += " and  c.Status = '" + poInfo.StatusCode + "'";
            }

            DataTable dt = new DataTable();
            var LPO = new List<POListReturn>();

            try
            {
                //added Objective code, payment terms and method field
                //added flagdelete field on where
                string strsql = " select ws.Name as WFStatusName,c.Id, c.POCode, c.SupplierCode, c.CreateDate, c.CreateBy, c.UpdateDate, c.UpdateBy, c.FlagDelete, c.Price, c.Status, c.POCode,s.SupplierName,t.LookupValue as StatusName,sum(pi.totPrice) as SumPrice, c.POObjective, c.PaymentMethod, c.PaymentTerm, c.Description , c.ExpectDate, c.RequestDate " +
                                " from " + dbName + ".dbo.PO c " +
                                " left join Supplier s on c.SupplierCode = s.SupplierCode " +
                                " left join Lookup t on c.Status = t.LookupValue and t.LookupType='POSTATUS' " +
                                " left join POItem pi on pi.POCode = c.POCode and pi.FlagDelete ='N' " +
                                " left join WF_Task_List w on c.Id = w.OMS_id " +
                                " left join WF_Status ws on w.Status = ws.Code " +

                              " where c.FlagDelete ='N'  " + strcond +

                              "GROUP BY  ws.Name,c.Id, c.POCode, c.SupplierCode, c.CreateDate, c.CreateBy, c.UpdateDate, c.UpdateBy, c.FlagDelete, c.Price, c.Status, c.POCode, s.SupplierName,t.LookupValue, c.POObjective, c.PaymentMethod, c.PaymentTerm, c.Description , c.ExpectDate, c.RequestDate ";

                strsql += " ORDER BY c.Id DESC OFFSET " + poInfo.rowOFFSet + " ROWS FETCH NEXT " + poInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPO = (from DataRow dr in dt.Rows

                       select new POListReturn()
                       {
                           POId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                           POCode = dr["POCode"].ToString().Trim(),
                           SupplierCode = dr["SupplierCode"].ToString().Trim(),
                           SupplierName = dr["SupplierName"].ToString().Trim(),
                           StatusCode = dr["Status"].ToString().Trim(),
                           StatusName = dr["StatusName"].ToString().Trim(),
                           WFStatusName = dr["WFStatusName"].ToString().Trim(),
                           Price = (dr["SumPrice"].ToString() != "") ? Convert.ToDouble(dr["SumPrice"]) : 0,

                           POObjectiveCode = dr["POObjective"].ToString().Trim(),
                           PaymentMethodCode = dr["PaymentMethod"].ToString().Trim(),
                           PaymentTermCode = dr["PaymentTerm"].ToString().Trim(),
                           Description = dr["Description"].ToString().Trim(),
                           RequestDate = dr["RequestDate"].ToString().Trim(),
                           ExpectDate = dr["ExpectDate"].ToString().Trim(),

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

            return LPO;
        }

        public int? CountPOSumpriceListByCriteria(POInfo poInfo)
        {
            string strcond = "";

            int? count = 0;

            if ((poInfo.POId != null) && (poInfo.POId != 0))
            {
                strcond += " and  c.Id =" + poInfo.POId;
            }

            if ((poInfo.POCode != null) && (poInfo.POCode != ""))
            {
                strcond += " and  c.POCode like '%" + poInfo.POCode + "%'";
            }
            if ((poInfo.StatusCode != null) && (poInfo.StatusCode != ""))
            {
                strcond += " and  c.Status = '" + poInfo.StatusCode + "'";
                //c.StatusCode -> c.Status
            }

            DataTable dt = new DataTable();
            var LPO = new List<POListReturn>();

            try
            {
                string strsql = "select count(c.Id) as countPO from " + dbName + ".dbo.PO c " +
                              " left join Supplier s on c.SupplierCode = s.SupplierCode " +
                                " left join Lookup t on c.Status = t.LookupValue and t.LookupType='POSTATUS' " +

                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPO = (from DataRow dr in dt.Rows

                       select new POListReturn()
                       {
                           countPO = Convert.ToInt32(dr["countPO"])
                       }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LPO.Count > 0)
            {
                count = LPO[0].countPO;
            }

            return count;
        }

        public List<POListReturn> ListPOSumPriceByCriteria(POInfo poInfo)
        {
            string strcond = "";

            if ((poInfo.POId != null) && (poInfo.POId != 0))
            {
                strcond += " and  c.Id =" + poInfo.POId;
            }

            if ((poInfo.POCode != null) && (poInfo.POCode != ""))
            {
                strcond += " and  c.POCode like '%" + poInfo.POCode + "%'";
            }
            if ((poInfo.StatusCode != null) && (poInfo.StatusCode != ""))
            {
                strcond += " and  c.Status = '" + poInfo.StatusCode + "'";
                //c.StatusCode -> c.Status
            }

            DataTable dt = new DataTable();
            var LPO = new List<POListReturn>();

            try
            {
                string strsql = " select c.Id, c.POCode, c.SupplierCode, c.CreateDate, c.CreateBy, c.UpdateDate, c.UpdateBy, c.FlagDelete, c.Price, c.Status, c.POCode,s.SupplierName,t.LookupValue as StatusName,sum(pi.totPrice) as SumPrice " +
                                " from " + dbName + ".dbo.PO c " +
                                " left join Supplier s on c.SupplierCode = s.SupplierCode " +
                                " left join Lookup t on c.Status = t.LookupValue and t.LookupType='POSTATUS' " +
                                " left join POItem pi on pi.POCode = c.POCode " +
                              " where c.FlagDelete ='N' " + strcond +

                              "GROUP BY  c.Id, c.POCode, c.SupplierCode, c.CreateDate, c.CreateBy, c.UpdateDate, c.UpdateBy, c.FlagDelete, c.Price, c.Status, c.POCode, s.SupplierName,t.LookupValue ";

                strsql += " ORDER BY c.Id DESC OFFSET " + poInfo.rowOFFSet + " ROWS FETCH NEXT " + poInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPO = (from DataRow dr in dt.Rows

                       select new POListReturn()
                       {
                           POId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                           POCode = dr["POCode"].ToString().Trim(),
                           SupplierCode = dr["SupplierCode"].ToString().Trim(),
                           SupplierName = dr["SupplierName"].ToString().Trim(),
                           StatusCode = dr["Status"].ToString().Trim(),
                           StatusName = dr["StatusName"].ToString().Trim(),
                           Price = (dr["SumPrice"].ToString() != "") ? Convert.ToDouble(dr["SumPrice"]) : 0,

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

            return LPO;
        }

        public int? CountPOListByCriteria(POInfo poInfo)
        {
            string strcond = "";

            int? count = 0;

            if ((poInfo.POId != null) && (poInfo.POId != 0))
            {
                strcond += " and  c.Id =" + poInfo.POId;
            }
            if ((poInfo.POCode != null) && (poInfo.POCode != ""))
            {
                strcond += " and  c.POCode like '%" + poInfo.POCode + "%'";
            }
            if ((poInfo.SupplierCode != null) && (poInfo.SupplierCode != "") && (poInfo.SupplierCode != "-99"))
            {
                strcond += " and  c.SupplierCode like '%" + poInfo.SupplierCode + "%'";
            }
            if ((poInfo.InventoryCode != null) && (poInfo.InventoryCode != "") && (poInfo.InventoryCode != "-99"))
            {
                strcond += " and  c.InventoryCode like '%" + poInfo.InventoryCode + "%'";
            }
            if ((poInfo.StatusCode != null) && (poInfo.StatusCode != "") && (poInfo.StatusCode != "-99"))
            {
                strcond += " and  t.Status = '" + poInfo.StatusCode + "'";
            }
            if (((poInfo.CreateDate != "") && (poInfo.CreateDate != null)) && ((poInfo.CreateDateTo != "") && (poInfo.CreateDateTo != null)))
            {
                strcond += " and  c.CreateDate BETWEEN CONVERT(VARCHAR, '" + poInfo.CreateDate + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + poInfo.CreateDateTo + "', 103)),'23:59:59')";
            }
            if (((poInfo.ExpectDate != "") && (poInfo.ExpectDate != null)) && ((poInfo.ExpectDateTo != "") && (poInfo.ExpectDateTo != null)))
            {
                strcond += " and  c.ExpectDate BETWEEN CONVERT(VARCHAR, '" + poInfo.ExpectDate + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + poInfo.ExpectDateTo + "', 103)),'23:59:59')";
            }
            if (((poInfo.PODate != "") && (poInfo.PODate != null)) && ((poInfo.PODateTo != "") && (poInfo.PODateTo != null)))
            {
                strcond += " and  c.PODate BETWEEN CONVERT(VARCHAR, '" + poInfo.PODate + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + poInfo.PODateTo + "', 103)),'23:59:59')";
            }
            if ((poInfo.CreateByNameTH != null) && (poInfo.CreateByNameTH != ""))
            {
                strcond += " and  cb.EmpFname_TH like '%" + poInfo.CreateByNameTH + "%'";
            }

            DataTable dt = new DataTable();
            var LPO = new List<POListReturn>();

            try
            {
                string strsql = " Select COUNT(c.Id) AS countPO" +
                                " from " + dbName + ".dbo.PO c " +
                                " left join Supplier s on c.SupplierCode = s.SupplierCode " +
                                " left join Inventory i on c.InventoryCode = i.InventoryCode " +
                                " left join WF_Task_List t on c.Id = t.OMS_id " +
                                " left join WF_Status stat on t.Status = stat.Code  " +
                                " left join Lookup pm on c.PaymentMethod = pm.LookupCode and pm.LookupType='PAYMENTMETHOD' " +
                                " left join Lookup po on c.POObjective = po.LookupCode and po.LookupType='POOBJECTTIVE' " +
                                " left join Lookup pt on c.PaymentTerm = pt.LookupCode and pt.LookupType='PAYMENTTERM' " +
                                " left join Emp cb on cb.EmpCode = c.CreateBy " +
                                " left join Emp ub on ub.EmpCode = c.UpdateBy " +
                                " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPO = (from DataRow dr in dt.Rows

                       select new POListReturn()
                       {
                           countPO = Convert.ToInt32(dr["countPO"])
                       }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LPO.Count > 0)
            {
                count = LPO[0].countPO;
            }

            return count;
        }


        public int UpdatePOItem(POItemInfo poInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.POItem set " +
                          
                            " POCode = '" + poInfo.POCode + "'," +
                            " ProductCode = '" + poInfo.ProductCode + "'," +
                           " QTY = '" + poInfo.QTY + "'," +
                           " TotPrice = '" + poInfo.TotPrice + "'," +
                           " Price = '" + poInfo.Price + "'," +
                            " UpdateBy = '" + poInfo.UpdateBy + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where Id =" + poInfo.POItemId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeletePOItem(POItemInfo poInfo)
        {
            int i = 0;

            
            string strsql = "Delete from " + dbName + ".dbo.POItem where POCode in ('" + poInfo.POCode + "')";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertpoItem(POItemInfo poInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO POItem  (POCode,ProductCode,SupplierCode,InventoryCode,QTY,TotPrice,Price,DiscountAmount,DiscountPercent,DiscountBill,RunningNo,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete,Active)" +
                            "VALUES (" +
                           
                           "'" + poInfo.POCode + "'," +
                           "'" + poInfo.ProductCode + "'," +
                           "'" + poInfo.SupplierCode + "'," +
                           "'" + poInfo.InventoryCode + "'," +
                           "'" + poInfo.QTY + "'," +
                           "'" + poInfo.TotPrice + "'," +
                           "'" + poInfo.Price + "'," +
                           "'" + poInfo.DiscountAmount + "'," +
                           "'" + poInfo.DiscountPercent + "'," +
                           "'" + poInfo.DiscountBill + "'," +
                           "'" + poInfo.RunningNo + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + poInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + poInfo.UpdateBy + "'," +
                           "'" + poInfo.FlagDelete + "'," +
                           "'" + poInfo.Active + "'" +
                            ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int InsertfromImportPOItem(POItemInfo poInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO POItem  (POCode,ProductCode,InventoryCode,MerchantCode,QTY,TotPrice,Price,CreateDate,CreateBy,StatusPOItem,Active,FlagDelete)" +
                            "VALUES (" +
                           
                           "'" + poInfo.POCode + "'," +
                           "'" + poInfo.ProductCode + "'," +
                           "'" + poInfo.InventoryCode + "'," +
                           "'" + poInfo.MerchantCode + "'," +
                           "'" + poInfo.QTY + "'," +
                           "'" + poInfo.TotPrice + "'," +
                           "'" + poInfo.Price + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + poInfo.CreateBy + "'," +
                           "'" + poInfo.StatusPOItem + "'," +
                           "'" + poInfo.Active + "'," +
                           "'" + poInfo.FlagDelete + "'" +
                            ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<POItemListReturn> ListPOItemNopagingByCriteria(POItemInfo poInfo)
        {
            string strcond = "";

            if ((poInfo.POItemId != null) && (poInfo.POItemId != 0))
            {
                strcond += " and  c.Id =" + poInfo.POItemId;
            }

            if ((poInfo.POCode != null) && (poInfo.POCode != ""))
            {
                strcond += " and  c.POCode like '%" + poInfo.POCode + "%'";
            }
            if ((poInfo.POCodeValidate != null) && (poInfo.POCodeValidate != ""))
            {
                strcond += " and  c.POCode = '" + poInfo.POCodeValidate + "'";
            }
            if ((poInfo.POItemCode != null) && (poInfo.POItemCode != ""))
            {
                strcond += " and  c.POItemCode like '%" + poInfo.POItemCode + "%'";
            }

            if ((poInfo.ProductCode != null) && (poInfo.ProductCode != ""))
            {
                strcond += " and  c.ProductCode like '%" + poInfo.ProductCode + "%'";
            }

            DataTable dt = new DataTable();
            var LPO = new List<POItemListReturn>();

            try
            {
                string strsql = " select c.*, p.ProductName, u.LookupValue AS UnitName from " + dbName + ".dbo.POItem c " +
                                " LEFT OUTER JOIN " + dbName + ".dbo.Product AS p ON c.ProductCode = p.ProductCode " +
                                " LEFT OUTER JOIN " + dbName + ".dbo.Lookup AS u ON u.LookupCode = p.Unit AND u.LookupType = 'UNIT' " +
                                " where c.flagdelete = 'N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPO = (from DataRow dr in dt.Rows

                       select new POItemListReturn()
                       {
                           POItemId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                           POCode = dr["POCode"].ToString().Trim(),
                           ProductCode = dr["ProductCode"].ToString().Trim(),
                           ProductName = dr["ProductName"].ToString().Trim(),
                           QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                           TotPrice = (dr["TotPrice"].ToString() != "") ? Convert.ToDouble(dr["TotPrice"]) : 0.00,
                           Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0.00,
                           UnitName = dr["UnitName"].ToString().Trim(),
                           DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["DiscountAmount"]) : 0.00,
                           DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToDouble(dr["DiscountPercent"]) : 0.00,
                           DiscountBill = (dr["DiscountBill"].ToString() != "") ? Convert.ToDouble(dr["DiscountBill"]) : 0.00,
                           RunningNo = dr["RunningNo"].ToString().Trim(),
                       }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPO;
        }

        public List<POItemListReturn> ListPOItemByCriteria(POItemInfo poInfo)
        {
            string strcond = "";

            if ((poInfo.POItemId != null) && (poInfo.POItemId != 0))
            {
                strcond += " and  c.Id =" + poInfo.POItemId;
            }

            if ((poInfo.POCode != null) && (poInfo.POCode != ""))
            {
                strcond += " and  c.POCode like '%" + poInfo.POCode + "%'";
            }

            if ((poInfo.POCodeValidate != null) && (poInfo.POCodeValidate != ""))
            {
                strcond += " and  c.POCode = '" + poInfo.POCodeValidate + "'";
            }

            if ((poInfo.POItemCode != null) && (poInfo.POItemCode != ""))
            {
                strcond += " and  c.POItemCode like '%" + poInfo.POItemCode + "%'";
            }

            if ((poInfo.ProductCode != null) && (poInfo.ProductCode != ""))
            {
                strcond += " and  c.ProductCode like '%" + poInfo.ProductCode + "%'";
            }

            DataTable dt = new DataTable();
            var LPO = new List<POItemListReturn>();

            try
            {
                string strsql = " select c.*,p.ProductName from " + dbName + ".dbo.POItem c " +
                                " left join Product p on c.ProductCode = p.ProductCode " +
                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + poInfo.rowOFFSet + " ROWS FETCH NEXT " + poInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPO = (from DataRow dr in dt.Rows

                       select new POItemListReturn()
                       {
                           POItemId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                           POCode = dr["POCode"].ToString().Trim(),
                           ProductCode = dr["ProductCode"].ToString().Trim(),
                           ProductName = dr["ProductName"].ToString().Trim(),
                           QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                           TotPrice = (dr["TotPrice"].ToString() != "") ? Convert.ToDouble(dr["TotPrice"]) : 0,
                           Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
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

            return LPO;
        }

        public int? CountPOItemListByCriteria(POItemInfo poInfo)
        {
            string strcond = "";

            int? count = 0;

            if ((poInfo.POItemId != null) && (poInfo.POItemId != 0))
            {
                strcond += " and  c.Id =" + poInfo.POItemId;
            }

            if ((poInfo.POCode != null) && (poInfo.POCode != ""))
            {
                strcond += " and  c.POCode like '%" + poInfo.POCode + "%'";
            }

            if ((poInfo.POCodeValidate != null) && (poInfo.POCodeValidate != ""))
            {
                strcond += " and  c.POCode = '" + poInfo.POCodeValidate + "'";
            }

            if ((poInfo.POItemCode != null) && (poInfo.POItemCode != ""))
            {
                strcond += " and  c.POItemCode like '%" + poInfo.POItemCode + "%'";
            }

            if ((poInfo.ProductCode != null) && (poInfo.ProductCode != ""))
            {
                strcond += " and  c.ProductCode like '%" + poInfo.ProductCode + "%'";
            }

            DataTable dt = new DataTable();
            var LPO = new List<POItemListReturn>();

            try
            {
                string strsql = "select count(c.Id) as countPOItem from " + dbName + ".dbo.POItem c " +
                               " left join Product p on c.ProductCode = p.ProductCode " +
                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPO = (from DataRow dr in dt.Rows

                       select new POItemListReturn()
                       {
                           countPOItem = Convert.ToInt32(dr["countPOItem"])
                       }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LPO.Count > 0)
            {
                count = LPO[0].countPOItem;
            }

            return count;
        }

        public List<POListReturn> ListPOFromInvenDetail(POInfo poInfo)
        {
            string strcond = "";

            if ((poInfo.POId != null) && (poInfo.POId != 0))
            {
                strcond += " and  c.Id =" + poInfo.POId;
            }

            if ((poInfo.POCode != null) && (poInfo.POCode != ""))
            {
                strcond += " and  c.POCode like '%" + poInfo.POCode + "%'";
            }
            if ((poInfo.StatusCode != null) && (poInfo.StatusCode != ""))
            {
                strcond += " and  c.Status = '" + poInfo.StatusCode + "'";
            }
            if (((poInfo.CreateDate != "") && (poInfo.CreateDate != null)) && ((poInfo.CreateDateTo != "") && (poInfo.CreateDateTo != null)))
            {
                strcond += " and  c.CreateDate BETWEEN CONVERT(VARCHAR, '" + poInfo.CreateDate + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + poInfo.CreateDateTo + "', 103)),'23:59:59')";
            }
            if (((poInfo.ExpectDate != "") && (poInfo.ExpectDate != null)) && ((poInfo.ExpectDateTo != "") && (poInfo.ExpectDateTo != null)))
            {
                strcond += " and  c.ExpectDate BETWEEN CONVERT(VARCHAR, '" + poInfo.ExpectDate + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + poInfo.ExpectDateTo + "', 103)),'23:59:59')";
            }

            DataTable dt = new DataTable();
            var LPO = new List<POListReturn>();

            try
            {
                string strsql = " select pm.LookupCode as PaymentMethodCode,pm.LookupValue as PaymentMethodName," +
                                "pt.LookupCode as PaymentTermCode,pt.LookupValue as PaymentTermName," +
                               " po.LookupCode as POObjectiveCode,po.LookupValue as POObjectiveName," +
                                "c.*,s.SupplierName,stat.Name as StatusName from " + dbName + ".dbo.PO c " +
                                " left join Supplier s on c.SupplierCode = s.SupplierCode  and s.FlagDelete = 'N' " +
                                " left join WF_Task_List t on c.Id = t.OMS_id " +
                                " left join WF_Status stat on t.Status = stat.Code  " +
                               " left join Lookup pm on c.PaymentMethod = pm.LookupCode and pm.LookupType='PAYMENTMETHOD' " +
                               " left join Lookup po on c.POObjective = po.LookupCode and po.LookupType='POOBJECTTIVE' " +
                               " left join Lookup pt on c.PaymentTerm = pt.LookupCode and pt.LookupType='PAYMENTTERM' " +
                               " left join InventoryDetail i on c.POCode = i.POCode and i.FlagDelete = 'N' " +
                             " where c.FlagDelete ='N' and i.InventoryCode is NULL " + strcond;

                strsql += " ORDER BY c.Id  DESC OFFSET  " + poInfo.rowOFFSet + " ROWS FETCH NEXT " + poInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPO = (from DataRow dr in dt.Rows

                       select new POListReturn()
                       {
                           POId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                           POCode = dr["POCode"].ToString().Trim(),
                           SupplierCode = dr["SupplierCode"].ToString().Trim(),
                           SupplierName = dr["SupplierName"].ToString().Trim(),
                           StatusCode = dr["Status"].ToString().Trim(),
                           StatusName = dr["StatusName"].ToString().Trim(),
                           Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                           RequestDate = dr["RequestDate"].ToString(),
                           ExpectDate = dr["ExpectDate"].ToString(),
                           POObjectiveCode = dr["POObjectiveCode"].ToString(),
                           POObjectiveName = dr["POObjectiveName"].ToString(),
                           PaymentMethodCode = dr["PaymentMethodCode"].ToString(),
                           PaymentMethodName = dr["PaymentMethodName"].ToString(),
                           PaymentTermCode = dr["PaymentTermCode"].ToString(),
                           PaymentTermName = dr["PaymentTermName"].ToString(),
                           Description = dr["Description"].ToString(),
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

            return LPO;
        }

        public List<POListReturn> ListPOmapInventoryDetailByCriteria(POInfo poInfo)
        {
            string strcond = "";

            if ((poInfo.POId != null) && (poInfo.POId != 0))
            {
                strcond += " and  id.Id =" + poInfo.POId;
            }

            if ((poInfo.POCode != null) && (poInfo.POCode != ""))
            {
                strcond += " and  id.POCode like '%" + poInfo.POCode + "%'";
            }
            if ((poInfo.StatusCode != null) && (poInfo.StatusCode != ""))
            {
                strcond += " and  c.Status = '" + poInfo.StatusCode + "'";
            }
            if (((poInfo.CreateDate != "") && (poInfo.CreateDate != null)) && ((poInfo.CreateDateTo != "") && (poInfo.CreateDateTo != null)))
            {
                strcond += " and  c.CreateDate BETWEEN CONVERT(VARCHAR, '" + poInfo.CreateDate + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + poInfo.CreateDateTo + "', 103)),'23:59:59')";
            }
            if (((poInfo.ExpectDate != "") && (poInfo.ExpectDate != null)) && ((poInfo.ExpectDateTo != "") && (poInfo.ExpectDateTo != null)))
            {
                strcond += " and  c.ExpectDate BETWEEN CONVERT(VARCHAR, '" + poInfo.ExpectDate + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + poInfo.ExpectDateTo + "', 103)),'23:59:59')";
            }

            DataTable dt = new DataTable();
            var LPO = new List<POListReturn>();

            try
            {
                

                string strsql =
                    " select distinct po.Id,s.SupplierName,m.MerchantName,po.CreateDate,po.RequestDate,po.FlagDelete, im.POCode AS POCodeInvMovement " +
                    " from " + dbName + ".dbo.PO as po " +
                    " left join " + dbName + ".dbo.POItem as pitem on pitem.POCode = po.POCode and pitem.FlagDelete = 'N' " +
                    " left join " + dbName + ".dbo.Product as p on p.ProductCode = pitem.ProductCode and p.FlagDelete = 'N' " +
                    " left join " + dbName + ".dbo.Supplier as s on p.SupplierCode = s.SupplierCode and s.FlagDelete = 'N' " +
                    " left join " + dbName + ".dbo.Merchant as m on m.MerchantCode = p.MerchantCode and m.FlagDelete = 'N' " +
                    " left join " + dbName + ".dbo.InventoryDetail as id on id.POCode = po.POCode and id.FlagDelete = 'N' " +
                    " left join " + dbName + ".dbo.InventoryMovement as im on im.InventoryDetailId = id.Id " +
                    " where im.POCode is not null and po.FlagDelete = 'N' " + strcond;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPO = (from DataRow dr in dt.Rows

                       select new POListReturn()
                       {
                           POId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                           POCode = dr["POCodeInvMovement"].ToString().Trim(),
                           SupplierName = dr["SupplierName"].ToString().Trim(),
                           MerchantName = dr["MerchantName"].ToString().Trim(),
                           RequestDate = dr["RequestDate"].ToString(),
                           CreateDate = dr["CreateDate"].ToString(),
                           FlagDelete = dr["FlagDelete"].ToString(),

                       }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPO;
        }

        public List<POListReturn> ListPOMOdalmapInventoryDetailByCriteria(POInfo poInfo)
        {
            string strcond = "";

            if ((poInfo.POId != null) && (poInfo.POId != 0))
            {
                strcond += " and  po.Id =" + poInfo.POId;
            }
            if ((poInfo.POCode != null) && (poInfo.POCode != ""))
            {
                strcond += " and  po.POCode like '%" + poInfo.POCode + "%'";
            }
            if ((poInfo.POCodeList != null) && (poInfo.POCodeList != ""))
            {
                strcond += " and (po.POCode NOT IN ('" + poInfo.POCodeList + "'))";
            }
            if (((poInfo.CreateDate != "") && (poInfo.CreateDate != null)) && ((poInfo.CreateDateTo != "") && (poInfo.CreateDateTo != null)))
            {
                strcond += " and  po.CreateDate BETWEEN CONVERT(VARCHAR, '" + poInfo.CreateDate + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + poInfo.CreateDateTo + "', 103)),'23:59:59')";
            }
            if (((poInfo.ExpectDate != "") && (poInfo.ExpectDate != null)) && ((poInfo.ExpectDateTo != "") && (poInfo.ExpectDateTo != null)))
            {
                strcond += " and  po.ExpectDate BETWEEN CONVERT(VARCHAR, '" + poInfo.ExpectDate + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + poInfo.ExpectDateTo + "', 103)),'23:59:59')";
            }
            if (((poInfo.RequestDate != "") && (poInfo.RequestDate != null)) && ((poInfo.RequestDateTo != "") && (poInfo.RequestDateTo != null)))
            {
                strcond += " and  po.RequestDate BETWEEN CONVERT(VARCHAR, '" + poInfo.RequestDate + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + poInfo.RequestDateTo + "', 103)),'23:59:59')";
            }
            if ((poInfo.SupplierCode != null) && (poInfo.SupplierCode != "") && (poInfo.SupplierCode != "-99"))
            {
                strcond += " and  po.SupplierCode like '%" + poInfo.SupplierCode + "%'";
            }
            if ((poInfo.SupplierName != null) && (poInfo.SupplierName != ""))
            {
                strcond += " and  s.SupplierName like '%" + poInfo.SupplierName + "%'";
            }
            if ((poInfo.MerchantCode != null) && (poInfo.MerchantCode != "") && (poInfo.MerchantCode != "-99"))
            {
                strcond += " and  po.MerchantCode like '%" + poInfo.MerchantCode + "%'";
            }
            if ((poInfo.MerchantName != null) && (poInfo.SupplierName != ""))
            {
                strcond += " and  m.MerchantName like '%" + poInfo.MerchantName + "%'";
            }

            DataTable dt = new DataTable();
            var LPO = new List<POListReturn>();

            try
            {                
                string strsql =
                    " select po.Id, po.POCode, po.CreateDate, po.RequestDate, po.FlagDelete, m.MerchantName, s.SupplierName " +
                    " from " + dbName + ".dbo.PO AS po " +
                    " left join " + dbName + ".dbo.Merchant AS m ON m.MerchantCode = po.MerchantCode " +
                    " left join " + dbName + ".dbo.Supplier AS s ON s.SupplierCode = po.SupplierCode " +
                    " where (po.FlagDelete = 'N') and (m.FlagDelete = 'N') and (s.FlagDelete = 'N')  " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPO = (from DataRow dr in dt.Rows

                       select new POListReturn()
                       {
                           POId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                           POCode = dr["POCode"].ToString().Trim(),
                           SupplierName = dr["SupplierName"].ToString().Trim(),
                           MerchantName = dr["MerchantName"].ToString().Trim(),
                           RequestDate = dr["RequestDate"].ToString(),
                           CreateDate = dr["CreateDate"].ToString(),
                           FlagDelete = dr["FlagDelete"].ToString(),

                       }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPO;
        }

        public List<POListReturn> ListPOItemMapProductByCriteria(POInfo poInfo)
        {
            string strcond = "";

            if ((poInfo.POId != null) && (poInfo.POId != 0))
            {
                strcond += " and  id.Id =" + poInfo.POId;
            }

            if ((poInfo.POCode != null) && (poInfo.POCode != ""))
            {
                strcond += (strcond == "") ? " where  item.POCode like '%" + poInfo.POCode.Trim() + "%'" : " and item.POCode like '%" + poInfo.POCode.Trim() + "%'";
            }
            if ((poInfo.FlagDelete != null) && (poInfo.FlagDelete != ""))
            {
                strcond += (strcond == "")? " WHERE item.FlagDelete = '" + poInfo.FlagDelete + "'" : "AND item.FlagDelete = '" + poInfo.FlagDelete + "'";
            }

            DataTable dt = new DataTable();
            var LPO = new List<POListReturn>();

            try
            {
                string strsql =
                    " SELECT item.Id, item.POCode, item.ProductCode, p.ProductName, item.QTY " +
                    " FROM " + dbName + ".dbo.POItem AS item " +
                    " LEFT OUTER JOIN " + dbName + ".dbo.Product AS p ON p.ProductCode = item.ProductCode AND p.FlagDelete = 'N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPO = (from DataRow dr in dt.Rows

                       select new POListReturn()
                       {
                           POItemId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                           POCode = dr["POCode"].ToString().Trim(),
                           ProductCode = dr["ProductCode"].ToString().Trim(),
                           ProductName = dr["ProductName"].ToString().Trim(),
                           QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,

                       }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPO;
        }

        public int? CountPOCodeRunningNumber(POInfo poInfo)
        {
            string strcond = "";
            int? count = 0;


            if ((poInfo.POId != null) && (poInfo.POId != 0))
            {
                strcond += " and  p.Id =" + poInfo.POId;
            }

            if ((poInfo.POCode != null) && (poInfo.POCode != ""))
            {
                strcond += " and  p.POCode like '%" + poInfo.POCode + "%'";
            }

            DataTable dt = new DataTable();
            var LPO = new List<POListReturn>();

            try
            {
                string strsql = "SELECT   ISNULL(MAX(ISNULL(p.id, 0)), 0) + 1 AS countPO from " + dbName + ".dbo.PO p " +
                                 " where 1=1 " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPO = (from DataRow dr in dt.Rows

                       select new POListReturn()
                       {
                           countPO = Convert.ToInt32(dr["countPO"])
                       }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LPO.Count > 0)
            {
                count = LPO[0].countPO;
            }

            return count;
        }
    }

}
