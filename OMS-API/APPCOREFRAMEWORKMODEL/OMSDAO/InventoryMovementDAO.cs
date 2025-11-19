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
    public class InventoryMovementDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int? CountInventoryMovementByCriteria(InventoryMovementInfo imInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((imInfo.InventoryMovementId != null) && (imInfo.InventoryMovementId != 0))
            {
                strcond += " and  i.Id =" + imInfo.InventoryMovementId;
            }
            if ((imInfo.InventoryDetailId != null) && (imInfo.InventoryDetailId != 0))
            {
                strcond += " and  i.InventoryDetailId ='" + imInfo.InventoryDetailId + "'";
            }
            if ((imInfo.InventoryMovementCode != null) && (imInfo.InventoryMovementCode != ""))
            {
                strcond += " and  i.InventoryMovementCode = '" + imInfo.InventoryMovementCode + "'";
            }
            if ((imInfo.InventoryManualLotCode != null) && (imInfo.InventoryManualLotCode != ""))
            {
                strcond += " and  i.InventoryManualLotCode ='" + imInfo.InventoryManualLotCode + "'";
            }
            if ((imInfo.ProductCode != null) && (imInfo.ProductCode != ""))
            {
                strcond += " and  i.ProductCode = '" + imInfo.ProductCode + "'";
            }
            if ((imInfo.POCode != null) && (imInfo.POCode != ""))
            {
                strcond += " and i.POCode = '" + imInfo.POCode + "'";
            }
            if ((imInfo.GRCode != null) && (imInfo.GRCode != ""))
            {
                strcond += " and i.GRCode = '" + imInfo.GRCode + "'";
            }
            if ((imInfo.SupplierName != null) && (imInfo.SupplierName != ""))
            {
                strcond += " and s.SupplierName = '" + imInfo.SupplierName.Trim() + "'";
            }
            if ((imInfo.OrderNo != null) && (imInfo.OrderNo != ""))
            {
                strcond += " and i.OrderNo = '" + imInfo.OrderNo + "'";
            }
            if ((imInfo.CreateDateFrom != null) && (imInfo.CreateDateFrom != ""))
            {
                strcond += " AND i.CreateDate BETWEEN CONVERT(DATETIME, '" + imInfo.CreateDateFrom + "',103) AND CONVERT(DATETIME,'" + imInfo.CreateDateTo + " 23:59:59:999',103)";
            }
            if ((imInfo.EmpFNameTH != null) && (imInfo.EmpFNameTH != ""))
            {
                strcond += " and e.EmpFname_TH like '%" + imInfo.EmpFNameTH + "%'";
            }
            if ((imInfo.EmpLNameTH != null) && (imInfo.EmpLNameTH != ""))
            {
                strcond += " and e.EmpLName_TH like '%" + imInfo.EmpLNameTH + "%'";
            }

            DataTable dt = new DataTable();
            var LInventoryMovement = new List<InventoryMovementListReturn>();


            try
            {
                string strsql = "select count(i.Id) as countInventoryMovement from " + dbName + ".dbo.InventoryMovement i " +
                                " left join Product p on p.ProductCode = i.ProductCode" +
                                " left join PO po on po.POCode = i.POCode" +
                                " left join Supplier s on s.SupplierCode = po.SupplierCode" +
                                " left join Emp e on e.EmpCode = i.CreateBy" +
                                " where i.ActiveFlag ='Y' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventoryMovement = (from DataRow dr in dt.Rows

                             select new InventoryMovementListReturn()
                             {
                                 countInventoryMovement = Convert.ToInt32(dr["countInventoryMovement"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LInventoryMovement.Count > 0)
            {
                count = LInventoryMovement[0].countInventoryMovement;
            }

            return count;
        }

        public int ListMaxSeqIdByCriteria(InventoryMovementInfo imInfo)
        {
            int i = 0;

          

            DataTable dt = new DataTable();
            var LInventoryMovement = new List<InventoryMovementListReturn>();

            try
            {
                string strsql = "select ISNULL(Max(SeqId), 0) + 1 SeqId from " + dbName + ".dbo.InventoryMovement i" +
                            " where InventoryDetailId = '" + imInfo.InventoryDetailId + "' and ProductCode = '" + imInfo.ProductCode + "'";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventoryMovement = (from DataRow dr in dt.Rows

                                      select new InventoryMovementListReturn()
                                      {
                                          SeqId = Convert.ToInt32(dr["SeqId"])
                                      }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LInventoryMovement.Count > 0)
            {
                i = Convert.ToInt32(LInventoryMovement[0].SeqId);
            }


            return i;
        }

        public int ListMaxSeqManualIdByCriteria(InventoryMovementInfo imInfo)
        {
            int i = 0;

            DataTable dt = new DataTable();
            var LInventoryMovement = new List<InventoryMovementListReturn>();

            try
            {
                string strsql = "select ISNULL(Max(SeqManId), 0) + 1 SeqManId from " + dbName + ".dbo.InventoryMovement i" +
                            " where InventoryDetailId = '" + imInfo.InventoryDetailId + "' and ProductCode = '" + imInfo.ProductCode + "'";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventoryMovement = (from DataRow dr in dt.Rows

                                      select new InventoryMovementListReturn()
                                      {
                                          SeqManId = Convert.ToInt32(dr["SeqManId"])
                                      }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LInventoryMovement.Count > 0)
            {
                i = Convert.ToInt32(LInventoryMovement[0].SeqManId);
            }


            return i;
        }

        public List<InventoryMovementListReturn> ListInventoryMovementInfoPagingByCriteria(InventoryMovementInfo imInfo)
        {
            string strcond = "";

            if ((imInfo.InventoryMovementId != null) && (imInfo.InventoryMovementId != 0))
            {
                strcond += " and  i.Id =" + imInfo.InventoryMovementId;
            }            
            if ((imInfo.InventoryDetailId != null) && (imInfo.InventoryDetailId != 0))
            {
                strcond += " and  i.InventoryDetailId ='" + imInfo.InventoryDetailId + "'";
            }
            if ((imInfo.InventoryMovementCode != null) && (imInfo.InventoryMovementCode != ""))
            {
                strcond += " and  i.InventoryMovementCode = '" + imInfo.InventoryMovementCode + "'";
            }
            if ((imInfo.InventoryManualLotCode != null) && (imInfo.InventoryManualLotCode != ""))
            {
                strcond += " and  i.InventoryManualLotCode ='" + imInfo.InventoryManualLotCode + "'";
            }
            if ((imInfo.ProductCode != null) && (imInfo.ProductCode != ""))
            {
                strcond += " and  i.ProductCode = '" + imInfo.ProductCode + "'";
            }
            if ((imInfo.POCode != null) && (imInfo.POCode != ""))
            {
                strcond += " and i.POCode = '" + imInfo.POCode + "'";
            }
            if ((imInfo.GRCode != null) && (imInfo.GRCode != ""))
            {
                strcond += " and i.GRCode = '" + imInfo.GRCode + "'";
            }
            if ((imInfo.SupplierName != null) && (imInfo.SupplierName != ""))
            {
                strcond += " and s.SupplierName = '" + imInfo.SupplierName.Trim() + "'";
            }
            if ((imInfo.OrderNo != null) && (imInfo.OrderNo != ""))
            {
                strcond += " and i.OrderNo = '" + imInfo.OrderNo + "'";
            }
            if ((imInfo.CreateDateFrom != null) && (imInfo.CreateDateFrom != ""))
            {
                strcond += " AND i.CreateDate BETWEEN CONVERT(DATETIME, '" + imInfo.CreateDateFrom + "',103) AND CONVERT(DATETIME,'" + imInfo.CreateDateTo + " 23:59:59:999',103)";
            }
            if ((imInfo.EmpFNameTH != null) && (imInfo.EmpFNameTH != ""))
            {
                strcond += " and e.EmpFname_TH like '%" + imInfo.EmpFNameTH + "%'";
            }
            if ((imInfo.EmpLNameTH != null) && (imInfo.EmpLNameTH != ""))
            {
                strcond += " and e.EmpLName_TH like '%" + imInfo.EmpLNameTH + "%'";
            }

            DataTable dt = new DataTable();
            var LInventoryMovement = new List<InventoryMovementListReturn>();

            try
            {
                string strsql = "select i.*,p.ProductName, po.SupplierCode, s.SupplierName, e.EmpFname_TH, e.EmpLName_TH from " + dbName + ".dbo.InventoryMovement i" +
                                " left join Product p on p.ProductCode = i.ProductCode" +
                                " left join PO po on po.POCode = i.POCode" +
                                " left join Supplier s on s.SupplierCode = po.SupplierCode" +
                                " left join Emp e on e.EmpCode = i.CreateBy" +
                                " where i.ActiveFlag ='Y' " + strcond;

                strsql += " ORDER BY i.Id DESC OFFSET " + imInfo.rowOFFSet + " ROWS FETCH NEXT " + imInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventoryMovement = (from DataRow dr in dt.Rows

                             select new InventoryMovementListReturn()
                             {
                                 InventoryMovementId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 InventoryDetailId = (dr["InventoryDetailId"].ToString() != "") ? Convert.ToInt32(dr["InventoryDetailId"]) : 0,
                                 InventoryMovementCode = dr["InventoryMovementCode"].ToString(),
                                 InventoryManualLotCode = dr["InventoryManualLotCode"].ToString(),
                                 ProductCode = dr["ProductCode"].ToString(),
                                 ProductName = dr["ProductName"].ToString().Trim(),
                                 POCode = dr["POCode"].ToString().Trim(),
                                 GRCode = dr["GRCode"].ToString().Trim(),
                                 OrderNo = dr["OrderNo"].ToString().Trim(),
                                 StatusCode = dr["Status"].ToString().Trim(),
                                 SeqId = (dr["SeqId"].ToString() != "") ? Convert.ToInt32(dr["SeqId"]) : 0,
                                 CreateDate = dr["CreateDate"].ToString().Trim(),
                                 EmpName = dr["EmpFname_TH"].ToString().Trim() + " " + dr["EmpLName_TH"].ToString().Trim(),
                                 Remark = dr["Remark"].ToString().Trim(),
                                 SupplierCode = dr["SupplierCode"].ToString().Trim(),
                                 SupplierName = dr["SupplierName"].ToString().Trim(),
                                 Price = (dr["price"].ToString() != "") ? Convert.ToDecimal(dr["price"]) : 0,
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventoryMovement;
        }

        public int InsertInventoryMovement(InventoryMovementInfo imInfo)
        {
            int i = 0;

            string strsql = "insert into InventoryMovement(InventoryDetailId,InventoryMovementCode,InventoryManualLotCode,POCode,OrderNo,ProductCode,Status,SeqId,SeqManId,EntryDate,CreateDate,CreateBy,UpdateDate,UpdateBy,Remark,SupplierCode,ActiveFlag) values (" +
                             "'" + imInfo.InventoryDetailId + "', " +
                             "'" + imInfo.InventoryMovementCode + "', " +
                             "'" + imInfo.InventoryManualLotCode + "', " +
                             "'" + imInfo.POCode + "', " +
                             "'" + imInfo.OrderNo + "', " +
                             "'" + imInfo.ProductCode + "', " +
                             "'" + imInfo.StatusCode + "', " +
                             "'" + imInfo.SeqId + "', " +
                             "'" + imInfo.SeqManId + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                             "'" + imInfo.CreateBy + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                             "'" + imInfo.UpdateBy + "', " +
                             "'" + imInfo.Remark + "', " +
                             "'" + imInfo.SupplierCode + "', " +
                             "'" + imInfo.ActiveFlag + "')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteInventoryMovement(InventoryMovementInfo imInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.InventoryMovement set " +

                         " ActiveFlag = 'N'" +

                         " where Id in(" + imInfo.InventoryMovementId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateInventoryMovement(InventoryMovementInfo imInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.InventoryMovement set " +
                            " InventoryDetailId = '" + imInfo.InventoryDetailId + "'," +
                            " InventoryMovementCode = '" + imInfo.InventoryMovementCode + "'," +
                            " POCode = '" + imInfo.POCode + "'," +
                            " OrderNo = '" + imInfo.OrderNo + "'," +
                            " ProductCode = '" + imInfo.ProductCode + "'," +
                            " Status = '" + imInfo.StatusCode + "'," +
                            " SeqId = '" + imInfo.SeqId + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy = '" + imInfo.UpdateBy + "'" +
                            " where Id =" + imInfo.InventoryMovementId;


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<InventoryMovementListReturn> ListInventoryMovementNoPagingSelectedFormTakeOrderRetail(InventoryMovementInfo imInfo)
        {
            string strcond = "";
            string counttop = ""; 

            if ((imInfo.InventoryMovementId != null) && (imInfo.InventoryMovementId != 0))
            {
                strcond += " and  i.Id =" + imInfo.InventoryMovementId;
            }
            if ((imInfo.InventoryDetailId != null) && (imInfo.InventoryDetailId != 0))
            {
                strcond += " and  i.InventoryDetailId ='" + imInfo.InventoryDetailId + "'";
            }
            if ((imInfo.InventoryMovementCode != null) && (imInfo.InventoryMovementCode != ""))
            {
                strcond += " and  i.InventoryMovementCode = '" + imInfo.InventoryMovementCode + "'";
            }
            if ((imInfo.InventoryManualLotCode != null) && (imInfo.InventoryManualLotCode != ""))
            {
                strcond += " and  i.InventoryManualLotCode ='" + imInfo.InventoryManualLotCode + "'";
            }
            if ((imInfo.ProductCode != null) && (imInfo.ProductCode != ""))
            {
                strcond += " and  i.ProductCode = '" + imInfo.ProductCode + "'";
            }
            if ((imInfo.POCode != null) && (imInfo.POCode != ""))
            {
                strcond += " and i.POCode = '" + imInfo.POCode + "'";
            }
            if ((imInfo.GRCode != null) && (imInfo.GRCode != ""))
            {
                strcond += " and i.GRCode = '" + imInfo.GRCode + "'";
            }
            if ((imInfo.SupplierName != null) && (imInfo.SupplierName != ""))
            {
                strcond += " and s.SupplierName = '" + imInfo.SupplierName.Trim() + "'";
            }
            if ((imInfo.OrderNo != null) && (imInfo.OrderNo != ""))
            {
                strcond += " and i.OrderNo = '" + imInfo.OrderNo + "'";
            }
            if ((imInfo.CreateDateFrom != null) && (imInfo.CreateDateFrom != ""))
            {
                strcond += " AND i.CreateDate BETWEEN CONVERT(DATETIME, '" + imInfo.CreateDateFrom + "',103) AND CONVERT(DATETIME,'" + imInfo.CreateDateTo + " 23:59:59:999',103)";
            }
            if ((imInfo.EmpFNameTH != null) && (imInfo.EmpFNameTH != ""))
            {
                strcond += " and e.EmpFname_TH like '%" + imInfo.EmpFNameTH + "%'";
            }
            if ((imInfo.EmpLNameTH != null) && (imInfo.EmpLNameTH != ""))
            {
                strcond += " and e.EmpLName_TH like '%" + imInfo.EmpLNameTH + "%'";
            }

            if ((imInfo.CountTop != null) && (imInfo.EmpLNameTH != ""))
            {
                counttop = imInfo.CountTop;
            }
            else if (imInfo.CountTop == "")
            {
                counttop = "0";
            }

            DataTable dt = new DataTable();
            var LInventoryMovement = new List<InventoryMovementListReturn>();

            try
            {
                string strsql = "select top(" + counttop + ")" + " i.*,p.ProductName, po.SupplierCode, s.SupplierName, e.EmpFname_TH, e.EmpLName_TH from " + dbName + ".dbo.InventoryMovement i" +
                                " left join Product p on p.ProductCode = i.ProductCode" +
                                " left join PO po on po.POCode = i.POCode" +
                                " left join Supplier s on s.SupplierCode = po.SupplierCode" +
                                " left join Emp e on e.EmpCode = i.CreateBy" +
                                " where (i.ActiveFlag ='Y') and (i.OrderNo = '') " + strcond;

                strsql += " ORDER BY i.InventoryMovementCode";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventoryMovement = (from DataRow dr in dt.Rows

                                      select new InventoryMovementListReturn()
                                      {
                                          InventoryMovementId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                          InventoryDetailId = (dr["InventoryDetailId"].ToString() != "") ? Convert.ToInt32(dr["InventoryDetailId"]) : 0,
                                          InventoryMovementCode = dr["InventoryMovementCode"].ToString(),
                                          InventoryManualLotCode = dr["InventoryManualLotCode"].ToString(),
                                          ProductCode = dr["ProductCode"].ToString(),
                                          ProductName = dr["ProductName"].ToString().Trim(),
                                          POCode = dr["POCode"].ToString().Trim(),
                                          GRCode = dr["GRCode"].ToString().Trim(),
                                          OrderNo = dr["OrderNo"].ToString().Trim(),
                                          StatusCode = dr["Status"].ToString().Trim(),
                                          SeqId = (dr["SeqId"].ToString() != "") ? Convert.ToInt32(dr["SeqId"]) : 0,
                                          CreateDate = dr["CreateDate"].ToString().Trim(),
                                          EmpName = dr["EmpFname_TH"].ToString().Trim() + " " + dr["EmpLName_TH"].ToString().Trim(),
                                          Remark = dr["Remark"].ToString().Trim(),
                                          SupplierCode = dr["SupplierCode"].ToString().Trim(),
                                          SupplierName = dr["SupplierName"].ToString().Trim(),

                                      }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventoryMovement;
        }

        public int UpdateInventoryMovementfromTakeOrderRetail(InventoryMovementInfo imInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.InventoryMovement set " +
                            " OrderNo = '" + imInfo.OrderNo + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy = '" + imInfo.UpdateBy + "'" +
                            " where Id =" + imInfo.InventoryMovementId;

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<InventoryMovementListReturn> ListInventoryMovementNoPagingByCriteria(InventoryMovementInfo imInfo)
        {
            string strcond = "";

            if ((imInfo.InventoryMovementId != null) && (imInfo.InventoryMovementId != 0))
            {
                strcond += " and  i.Id =" + imInfo.InventoryMovementId;
            }
            if ((imInfo.InventoryDetailId != null) && (imInfo.InventoryDetailId != 0))
            {
                strcond += " and  i.InventoryDetailId ='" + imInfo.InventoryDetailId + "'";
            }
            if ((imInfo.InventoryMovementCode != null) && (imInfo.InventoryMovementCode != ""))
            {
                strcond += " and  i.InventoryMovementCode = '" + imInfo.InventoryMovementCode + "'";
            }
            if ((imInfo.InventoryManualLotCode != null) && (imInfo.InventoryManualLotCode != ""))
            {
                strcond += " and  i.InventoryManualLotCode ='" + imInfo.InventoryManualLotCode + "'";
            }
            if ((imInfo.ProductCode != null) && (imInfo.ProductCode != ""))
            {
                strcond += " and  i.ProductCode = '" + imInfo.ProductCode + "'";
            }
            if ((imInfo.POCode != null) && (imInfo.POCode != ""))
            {
                strcond += " and i.POCode = '" + imInfo.POCode + "'";
            }
            if ((imInfo.GRCode != null) && (imInfo.GRCode != ""))
            {
                strcond += " and i.GRCode = '" + imInfo.GRCode + "'";
            }
            if ((imInfo.SupplierName != null) && (imInfo.SupplierName != ""))
            {
                strcond += " and s.SupplierName = '" + imInfo.SupplierName.Trim() + "'";
            }
            if ((imInfo.OrderNo != null) && (imInfo.OrderNo != ""))
            {
                strcond += " and i.OrderNo = '" + imInfo.OrderNo + "'";
            }
            if ((imInfo.CreateDateFrom != null) && (imInfo.CreateDateFrom != ""))
            {
                strcond += " AND i.CreateDate BETWEEN CONVERT(DATETIME, '" + imInfo.CreateDateFrom + "',103) AND CONVERT(DATETIME,'" + imInfo.CreateDateTo + " 23:59:59:999',103)";
            }
            if ((imInfo.EmpFNameTH != null) && (imInfo.EmpFNameTH != ""))
            {
                strcond += " and e.EmpFname_TH like '%" + imInfo.EmpFNameTH + "%'";
            }
            if ((imInfo.EmpLNameTH != null) && (imInfo.EmpLNameTH != ""))
            {
                strcond += " and e.EmpLName_TH like '%" + imInfo.EmpLNameTH + "%'";
            }

            DataTable dt = new DataTable();
            var LInventoryMovement = new List<InventoryMovementListReturn>();

            try
            {
                string strsql = " select i.*,p.ProductName, po.SupplierCode, s.SupplierName, e.EmpFname_TH, e.EmpLName_TH from " + dbName + ".dbo.InventoryMovement i" +
                                " left join Product p on p.ProductCode = i.ProductCode" +
                                " left join PO po on po.POCode = i.POCode" +
                                " left join Supplier s on s.SupplierCode = po.SupplierCode" +
                                " left join Emp e on e.EmpCode = i.CreateBy" +
                                " where (i.ActiveFlag ='Y') " + strcond;

                strsql += " ORDER BY i.InventoryMovementCode";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventoryMovement = (from DataRow dr in dt.Rows

                                      select new InventoryMovementListReturn()
                                      {
                                          InventoryMovementId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                          InventoryDetailId = (dr["InventoryDetailId"].ToString() != "") ? Convert.ToInt32(dr["InventoryDetailId"]) : 0,
                                          InventoryMovementCode = dr["InventoryMovementCode"].ToString(),
                                          InventoryManualLotCode = dr["InventoryManualLotCode"].ToString(),
                                          ProductCode = dr["ProductCode"].ToString(),
                                          ProductName = dr["ProductName"].ToString().Trim(),
                                          POCode = dr["POCode"].ToString().Trim(),
                                          GRCode = dr["GRCode"].ToString().Trim(),
                                          OrderNo = dr["OrderNo"].ToString().Trim(),
                                          StatusCode = dr["Status"].ToString().Trim(),
                                          SeqId = (dr["SeqId"].ToString() != "") ? Convert.ToInt32(dr["SeqId"]) : 0,
                                          CreateDate = dr["CreateDate"].ToString().Trim(),
                                          EmpName = dr["EmpFname_TH"].ToString().Trim() + " " + dr["EmpLName_TH"].ToString().Trim(),
                                          Remark = dr["Remark"].ToString().Trim(),
                                          SupplierCode = dr["SupplierCode"].ToString().Trim(),
                                          SupplierName = dr["SupplierName"].ToString().Trim(),

                                      }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventoryMovement;
        }
    }
}
