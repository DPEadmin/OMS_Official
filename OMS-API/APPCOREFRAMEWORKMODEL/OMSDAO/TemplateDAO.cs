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
    public class TemplateDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();
        public List<TemplateInfo> ListTemplateListPagingByCriteria(TemplateInfo cInfo)
        {
            string strcond = "";
            string stroffset = "";
            string strNotin = "";

            if ((cInfo.TemplateId != null) && (cInfo.TemplateId != 0))
            {
                strcond += " and  t.TemplateId =" + cInfo.TemplateId;
            }

            if ((cInfo.TemplateCode != null) && (cInfo.TemplateCode != ""))
            {
                strcond += " and  t.TemplateCode = '" + cInfo.TemplateCode + "'";
            }
            if ((cInfo.TemplateName != null) && (cInfo.TemplateName != ""))
            {
                strcond += " and  t.TemplateName like '%" + cInfo.TemplateName + "%'";
            }

            if ((cInfo.FlagActive != null) && (cInfo.FlagActive != ""))
            {
                strcond += " and  t.FlagActive = '" + cInfo.FlagActive + "'";
            }
            if ((cInfo.MerchantCode != null) && (cInfo.MerchantCode != ""))
            {
                strcond += " and  t.MerchantCode = '" + cInfo.MerchantCode + "'";
            }
            if ((cInfo.TemplateNotInPromotionCode != null) && (cInfo.TemplateNotInPromotionCode != ""))
            {
                strcond += " and (t.TemplateCode not in(SELECT p.TemplateCode" +
                              " FROM  PromotionTemplateDetailInfo AS p " +
                              "  WHERE(p.FlagDelete = 'N')" + " AND(p.PromotionCode = '" + cInfo.TemplateNotInPromotionCode + "')" +
                              " )) ";
            }

            if ((cInfo.rowOFFSet != null) && (cInfo.rowFetch != 0) && (cInfo.rowOFFSet != null) && (cInfo.rowFetch != 0))
            {
                stroffset += " OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY ";
            }
            var LTemplate = new List<TemplateInfo>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select t.* " +
                                " from " + dbName + ".dbo.Template t " +
                                " where t.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY t.TemplateId DESC " + stroffset;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LTemplate = (from DataRow dr in dt.Rows

                             select new TemplateInfo()
                             {
                                 TemplateId = (dr["TemplateId"].ToString() != "") ? Convert.ToInt32(dr["TemplateId"]) : 0,
                                 TemplateCode = dr["TemplateCode"].ToString().Trim(),
                                 TemplateName = dr["TemplateName"].ToString().Trim(),
                                 TemplateType = dr["TemplateType"].ToString().Trim(),
                                 TemplateBody = dr["TemplateBody"].ToString().Trim(),
                                 TemplateImageURL = dr["TemplateImageURL"].ToString().Trim(),
                                 TemplateVideoURL = dr["TemplateVideoURL"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 FlagActive = dr["FlagActive"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LTemplate;
        }
        public int? CountTemplateListPagingByCriteria(TemplateInfo cInfo)
        {

            string strcond = "";
            int? count = 0;


            if ((cInfo.TemplateId != null) && (cInfo.TemplateId != 0))
            {
                strcond += " and  t.TemplateId =" + cInfo.TemplateId;
            }

            if ((cInfo.TemplateCode != null) && (cInfo.TemplateCode != ""))
            {
                strcond += " and  t.TemplateCode = '" + cInfo.TemplateCode + "'";
            }
            if ((cInfo.TemplateName != null) && (cInfo.TemplateName != ""))
            {
                strcond += " and  t.TemplateName like '%" + cInfo.TemplateName + "%'";
            }

            if ((cInfo.FlagActive != null) && (cInfo.FlagActive != ""))
            {
                strcond += " and  t.FlagActive = '" + cInfo.FlagActive + "'";
            }
            if ((cInfo.MerchantCode != null) && (cInfo.MerchantCode != ""))
            {
                strcond += " and  t.MerchantCode = '" + cInfo.MerchantCode + "'";
            }

            var LTemplate = new List<TemplateInfo>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select count(t.TemplateId) as CountTemplate from " + dbName + ".dbo.Template t " +
                                " where t.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LTemplate = (from DataRow dr in dt.Rows

                             select new TemplateInfo()
                             {
                                 CountTemplate = Convert.ToInt32(dr["CountTemplate"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LTemplate.Count > 0)
            {
                count = LTemplate[0].CountTemplate;
            }

            return count;
        }
        public List<TemplateParamInfo> ListTemplateParamNoPagingByCriteria(TemplateParamInfo cInfo)
        {
            string strcond = "";
            string stroffset = "";

          
            var LTemplate = new List<TemplateParamInfo>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select t.* " +
                                " from " + dbName + ".dbo.TemplateParam t ";

                strsql += " ORDER BY t.TemplateParamID ASC " + stroffset;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LTemplate = (from DataRow dr in dt.Rows

                             select new TemplateParamInfo()
                             {
                                 TemplateParamID = (dr["TemplateParamID"].ToString() != "") ? Convert.ToInt32(dr["TemplateParamID"]) : 0,
                                 TemplateParamCode = dr["TemplateParamCode"].ToString().Trim(),
                                 TemplateParamName = dr["TemplateParamName"].ToString().Trim(),

                                 TemplateParamFieldName = dr["TemplateParamFieldName"].ToString().Trim(),
                                 TemplateParamTable = dr["TemplateParamTable"].ToString().Trim(),
                                 TemplateParamTableLookup = dr["TemplateParamTableLookup"].ToString().Trim(),
                                 TemplateParamFieldLookup = dr["TemplateParamFieldLookup"].ToString(),
                               
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LTemplate;
        }
        public List<TemplateParamInfo> ListDatabyTemplateParam(TemplateParamInfo cInfo)
        {
            string strsql = "select ";
            string strcon = "";

            if ((cInfo.TemplateParamFieldName != null) && (cInfo.TemplateParamFieldName != "") && (cInfo.TemplateParamTableLookup != null) && (cInfo.TemplateParamTableLookup != ""))
            {
                strsql += cInfo.TemplateParamTableLookup + "." + cInfo.TemplateParamFieldName;
            }
            else
            {
                strsql += cInfo.TemplateParamTable + "." + cInfo.TemplateParamFieldName;
            }
            if ((cInfo.TemplateParamTable != null) && (cInfo.TemplateParamTable != ""))
            {
                strsql += " from " + dbName + ".dbo."+ cInfo.TemplateParamTable;
            }
            if ((cInfo.TemplateParamTableLookup != null) && (cInfo.TemplateParamTableLookup != "") &&
                (cInfo.TemplateParamFieldLookup != null) && (cInfo.TemplateParamFieldLookup != "") &&
                (cInfo.TemplateParamTableLookup != null) && (cInfo.TemplateParamTableLookup != "") &&
                (cInfo.TemplateParamTableCondition2 != null) && (cInfo.TemplateParamTableCondition2 != ""))
            {
                strsql += " inner join " + cInfo.TemplateParamTableLookup + " on " + cInfo.TemplateParamTable+"."+cInfo.TemplateParamFieldLookup + " = "+ cInfo.TemplateParamTableLookup + "." + cInfo.TemplateParamFieldLookup +" and "+cInfo.TemplateParamTable+"."+cInfo.TemplateParamTableCondition2;
            }
            if ((cInfo.TemplateParamTableCondition != null) && (cInfo.TemplateParamTableCondition != "") && (cInfo.ConditionCode != null) && (cInfo.ConditionCode != ""))
            {
                strsql += " where  " + cInfo.TemplateParamTableCondition + "= '"+ cInfo.ConditionCode + "'";
            }
            var LTemplate = new List<TemplateParamInfo>();
            DataTable dt = new DataTable();
            try
            {
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LTemplate = (from DataRow dr in dt.Rows

                             select new TemplateParamInfo()
                             {
                                 Data = dr[cInfo.TemplateParamFieldName].ToString().Trim(),
                             }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LTemplate;
        }
            public List<TemplateFieldInfo> ListTemplateFieldNoPagingByCriteria(TemplateFieldInfo cInfo)
        {
            string strcond = "";
            string stroffset = "";

            if ((cInfo.TemplateCode != null) && (cInfo.TemplateCode != ""))
            {
                strcond = strcond == "" ? strcond = " where t.TemplateCode = '" + cInfo.TemplateCode + "' " : " and t.TemplateCode = '" + cInfo.TemplateCode + "' ";
            }

            var LTemplate = new List<TemplateFieldInfo>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select t.*,tp.TemplateParamName,tp.TemplateParamFieldName,tp.TemplateParamTable,tp.TemplateParamTableLookup," +
                                " tp.TemplateParamTableCondition,tp.TemplateParamFieldLookup,tp.TemplateParamTableCondition2 " +
                                " from " + dbName + ".dbo.TemplateField t " +
                                " inner join TemplateParam as tp on tp.TemplateParamCode = t.TemplateParamCode " +
                                strcond;

                strsql += " ORDER BY t.TemplateFieldID ASC " + stroffset;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LTemplate = (from DataRow dr in dt.Rows

                             select new TemplateFieldInfo()
                             {
                                 TemplateFieldID = (dr["TemplateFieldID"].ToString() != "") ? Convert.ToInt32(dr["TemplateFieldID"]) : 0,
                                 TemplateCode = dr["TemplateCode"].ToString().Trim(),
                                 TemplateParamCode = dr["TemplateParamCode"].ToString().Trim(),
                                 TemplateParamName = dr["TemplateParamName"].ToString().Trim(),
                                 TemplateParamFieldName = dr["TemplateParamFieldName"].ToString().Trim(),
                                 TemplateParamTable = dr["TemplateParamTable"].ToString().Trim(),
                                 TemplateParamTableLookup = dr["TemplateParamTableLookup"].ToString().Trim(),
                                 TemplateParamTableCondition = dr["TemplateParamTableCondition"].ToString().Trim(),
                                 TemplateParamFieldLookup = dr["TemplateParamFieldLookup"].ToString().Trim(),
                                 TemplateParamTableCondition2 = dr["TemplateParamTableCondition2"].ToString().Trim(),
                                 TemplateFieldParamSeq = (dr["TemplateFieldParamSeq"].ToString() != "") ? Convert.ToInt32(dr["TemplateFieldParamSeq"]) : 0,
                               

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LTemplate;
        }
        public List<TemplatePlatformInfo> ListTemplatePlatformNoPagingByCriteria(TemplatePlatformInfo cInfo)
        {
            string strcond = "";
            string stroffset = "";

            if ((cInfo.TemplateCode != null) && (cInfo.TemplateCode != ""))
            {
                strcond = " and t.TemplateCode = '" + cInfo.TemplateCode + "' ";
            }

            var LTemplate = new List<TemplatePlatformInfo>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select t.* " +
                                " from " + dbName + ".dbo.TemplatePlatform t " +
                                " where t.FlagDelete = 'N' " +
                                strcond;

                strsql += " ORDER BY t.TemplatePlatformID ASC " + stroffset;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LTemplate = (from DataRow dr in dt.Rows

                             select new TemplatePlatformInfo()
                             {
                                 TemplatePlatformID = (dr["TemplatePlatformID"].ToString() != "") ? Convert.ToInt32(dr["TemplatePlatformID"]) : 0,
                                 TemplateCode = dr["TemplateCode"].ToString().Trim(),
                                 TemplatePlatformCode = dr["TemplatePlatformCode"].ToString().Trim(),
                                


                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LTemplate;
        }
        public List<PromotionTemplateInfo> ListPromotionTemplatePagingByCriteria(PromotionTemplateInfo cInfo)
        {
            string strcond = "";
            string stroffset = "";

            if ((cInfo.PromotionCode != null) && (cInfo.PromotionCode != ""))
            {
                strcond = " and t.PromotionCode = '" + cInfo.PromotionCode + "' ";
            }
            if ((cInfo.rowOFFSet != null) && (cInfo.rowFetch != 0) && (cInfo.rowOFFSet != null) && (cInfo.rowFetch != 0))
            {
                stroffset += " OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY ";
            }
            var LTemplate = new List<PromotionTemplateInfo>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select t.*,tp.TemplateName,tp.TemplateImageURL,tp.TemplateBody " +
                                " from " + dbName + ".dbo.PromotionTemplateDetailInfo t " +
                                " inner join template as tp on tp.TemplateCode = t.TemplateCode " +
                                " where t.FlagDelete = 'N' " +
                                strcond;

                strsql += " ORDER BY t.Id ASC " + stroffset;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LTemplate = (from DataRow dr in dt.Rows

                             select new PromotionTemplateInfo()
                             {
                                 Id = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 TemplateCode = dr["TemplateCode"].ToString().Trim(),
                                 TemplateName = dr["TemplateName"].ToString().Trim(),
                                 TemplateBody = dr["TemplateBody"].ToString().Trim(),
                                 TemplateImageURL = dr["TemplateImageURL"].ToString().Trim(),
                                 PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                 FlagLine = dr["FlagLine"].ToString().Trim(),
                                 FlagFacebook = dr["FlagFacebook"].ToString().Trim(),


                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LTemplate;
        }
        public int? CountPromotionTemplatePagingByCriteria(PromotionTemplateInfo cInfo)
        {

            string strcond = "";
            int? count = 0;


            if ((cInfo.PromotionCode != null) && (cInfo.PromotionCode != ""))
            {
                strcond = " and t.PromotionCode = '" + cInfo.PromotionCode + "' ";
            }

            var LTemplate = new List<PromotionTemplateInfo>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select count(t.Id) as CountTemplate from " + dbName + ".dbo.PromotionTemplateDetailInfo t " +
                                " where t.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LTemplate = (from DataRow dr in dt.Rows

                             select new PromotionTemplateInfo()
                             {
                                 CountTemplate = Convert.ToInt32(dr["CountTemplate"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LTemplate.Count > 0)
            {
                count = LTemplate[0].CountTemplate;
            }

            return count;
        }
        public int InsertTemplate(TemplateInfo cInfo)
        {

            int i = 0;
           

            string strsql = "INSERT INTO " + dbName + ".dbo.Template  " +
                "(TemplateCode,TemplateName,TemplateBody,CreateDate," +
                "CreateBy,UpdateDate,UpdateBy,TemplateType,TemplateImageURL,TemplateVideoURL," +
                "FlagDelete,FlagActive,MerchantCode)" +

                           "VALUES (" +
                          "'" + cInfo.TemplateCode + "'," +
                          "'" + cInfo.TemplateName + "'," +
                          "'" + cInfo.TemplateBody + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + cInfo.CreateBy + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + cInfo.UpdateBy + "'," +
                          "'" + cInfo.TemplateType + "'," +
                          "'" + cInfo.TemplateImageURL + "'," +
                          "'" + cInfo.TemplateVideoURL + "'," +
                          "'" + cInfo.FlagDelete + "'," +
                          "'" + cInfo.FlagActive + "'," +
                          "'" + cInfo.MerchantCode + "'" +
                           ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int InsertTemplatePlatform(TemplatePlatformInfo cInfo)
        {

            int i = 0;


            string strsql = "INSERT INTO " + dbName + ".dbo.TemplatePlatform  " +
                "(TemplateCode,TemplatePlatformCode,CreateDate," +
                "CreateBy,UpdateDate,UpdateBy," +
                "FlagDelete)" +

                           "VALUES (" +
                          "'" + cInfo.TemplateCode + "'," +
                          "'" + cInfo.TemplatePlatformCode + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + cInfo.CreateBy + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + cInfo.UpdateBy + "'," +
                          "'" + cInfo.FlagDelete + "'" +
                           ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int InsertTemplateField(TemplateFieldInfo cInfo)
        {

            int i = 0;


            string strsql = "INSERT INTO " + dbName + ".dbo.TemplateField  " +
                "(TemplateCode,TemplateParamCode,TemplateFieldParamSeq)" +

                           "VALUES (" +
                          "'" + cInfo.TemplateCode + "'," +
                          "'" + cInfo.TemplateParamCode + "'," +
                          "" + cInfo.TemplateFieldParamSeq + "" +
                           ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int InsertPromotionTemplate(PromotionTemplateInfo cInfo)
        {

            int i = 0;


            string strsql = "INSERT INTO " + dbName + ".dbo.PromotionTemplateDetailInfo  " +
                "(PromotionCode,TemplateCode,CreateDate," +
                "CreateBy,UpdateDate,UpdateBy," +
                "FlagDelete,FlagLine,FlagFacebook)" +

                           "VALUES (" +
                          "'" + cInfo.PromotionCode + "'," +
                          "'" + cInfo.TemplateCode + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + cInfo.CreateBy + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + cInfo.UpdateBy + "'," +
                          "'" + cInfo.FlagDelete + "'," +
                          "'" + cInfo.FlagLine + "'," +
                          "'" + cInfo.FlagFacebook + "'" +
                           ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public List<TemplatePlatformInfo> ListTemplatePlaformNoPagingByCriteria(TemplatePlatformInfo cInfo)
        {
            string strcond = "";
            if ((cInfo.TemplateCode != null) && (cInfo.TemplateCode != ""))
            {
                strcond += " and  t.TemplateCode =" + cInfo.TemplateCode;
            }


            var LTemplate = new List<TemplatePlatformInfo>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select t.* " +
                                " from " + dbName + ".dbo.TemplatePlatform t " +
                                " where t.FlagDelete = 'N'" + strcond;

                strsql += " ORDER BY t.TemplateParamID ASC " ;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LTemplate = (from DataRow dr in dt.Rows

                             select new TemplatePlatformInfo()
                             {
                                 TemplatePlatformID = (dr["TemplatePlatformID"].ToString() != "") ? Convert.ToInt32(dr["TemplatePlatformID"]) : 0,
                                 TemplateCode = dr["TemplateCode"].ToString().Trim(),
                                 TemplatePlatformCode = dr["TemplatePlatformCode"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString().Trim(),
                                 UpdateDate = dr["UpdateDate"].ToString().Trim(),
                                 UpdateBy = dr["UpdateBy"].ToString().Trim(),
                                 FlagDelete = dr["FlagDelete"].ToString(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LTemplate;
        }
        public int? DeleteTemplate(TemplateInfo pInfo)
        {
            int i = 0;
            string strsql = "Update " + dbName + ".dbo.Template set FlagDelete = 'Y' where TemplateId in (" + pInfo.TempIdforDelete + ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
    }
    
}
