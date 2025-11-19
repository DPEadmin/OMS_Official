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
    public class WorkflowDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<WorkflowListReturn> ListWorkflowTaskDetailByCriteria(WorkflowInfo wfInfo)
        {
            string strcond = "";

            if ((wfInfo.OMSCode != "") && (wfInfo.OMSCode != null))
            {
                strcond += " and OMSCode like '%" + wfInfo.OMSCode + "%'";
            }
            if ((wfInfo.OMSName != "") && (wfInfo.OMSName != null))
            {
                strcond += " and OMSName like '%" + wfInfo.OMSName + "%'";
            }
            if ((wfInfo.OMSId != 0) && (wfInfo.OMSId != null))
            {
                strcond += " and OMSId like '%" + wfInfo.OMSId + "%'";
            }
            if ((wfInfo.WFTypeList != "") && (wfInfo.WFTypeList != null))
            {
                strcond += " and WF_TYPE in (" + wfInfo.WFTypeList + ")";
            }

            if ((wfInfo.ID != -99) && (wfInfo.ID != null))
            {
                strcond += " and t.ID =" + wfInfo.ID + "";
            }

            if ((wfInfo.Status != -99) && (wfInfo.Status != null))
            {
                strcond += " and t.Status =" + wfInfo.Status + "";
            }

            if ((wfInfo.WFType != "") && (wfInfo.WFType != null))
            {
                strcond += " and WF_TYPE = " + wfInfo.WFType + "";
            }

            DataTable dt = new DataTable();
            var LWorkflow = new List<WorkflowListReturn>();

            try
            {
                string strsql = @"select distinct t.ID,s.NAME as StatusName,t.OMS_Id as OMSId,t.WF_ID as WFId,t.WF_TYPE as WFType," +
                                    " t.WF_NAME as WFName ,t.Status as Status,t.CREATEDATE as CreateDate" +

                                    " from " + dbName + ".dbo.WF_Task_List t " +
                                    " left join WF_Map_Emp_Role we on t.WF_TYPE = we.WF_TYPE_ID " +
                                    " left join WF_Status s on t.Status = s.Code" +
                                    " left join CampaignPromotion p on t.OMS_Id = p.Id" +
                                    " where 1=1 " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LWorkflow = (from DataRow dr in dt.Rows

                             select new WorkflowListReturn()
                             {
                                 ID = (dr["ID"].ToString() != "") ? Convert.ToDecimal(dr["ID"]) : 0,
                                 OMSId = (dr["OMSId"].ToString() != "") ? Convert.ToDecimal(dr["OMSId"]) : 0,
                                 WFId = (dr["WFId"].ToString() != "") ? Convert.ToDecimal(dr["WFId"]) : 0,
                                 WFType = dr["WFType"].ToString(),
                                 WFName = dr["WFName"].ToString(),
                                 Status = (dr["Status"].ToString() != "") ? Convert.ToDecimal(dr["Status"]) : 0,
                                 StatusName = dr["StatusName"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LWorkflow;
        }

        public int InsertWFTaskList(WorkflowInfo wfInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO WF_Task_List(OMS_id,WF_Id,WF_Name," +
                            "WF_Type,Status,CreateDate,CreateBy,UpdateDate,UpdateBy)" +
                            "VALUES (" +
                            "'" + wfInfo.OMSId + "'," +
                            "'" + wfInfo.WFId + "'," +
                            "'" + wfInfo.WFName + "'," +
                            "'" + wfInfo.WFType + "'," +
                            "'" + wfInfo.Status + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            "'" + wfInfo.CreateBy + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            "'" + wfInfo.UpdateBy + "'" +
                            ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateWFTaskList(WorkflowInfo wfInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.WF_Task_List set " +
                            " Status = '" + wfInfo.Status + "'," +
                            " UpdateBy = '" + wfInfo.UpdateBy + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where OMS_id ='" + wfInfo.OMSId + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<WorkflowListReturn> ListWorkflowStatusNoPagingByCriteria(WorkflowInfo wInfo)
        {
            string strcond = "";

            if ((wInfo.WFStatusCode != null) && (wInfo.WFStatusCode != ""))
            {
                strcond += " and  w.Code like '%" + wInfo.WFStatusCode + "%'";
            }

            DataTable dt = new DataTable();
            var LWF_Status = new List<WorkflowListReturn>();

            try
            {
                string strsql = " select w.* from " + dbName + ".dbo.WF_Status w " +
                               " where w.Active ='Y' " + strcond;

                strsql += " ORDER BY w.Id Desc";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LWF_Status = (from DataRow dr in dt.Rows

                              select new WorkflowListReturn()
                              {
                                  WFStatusId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  WFStatusCode = dr["Code"].ToString().Trim(),
                                  WFStatusName = dr["Name"].ToString().Trim(),
                                  Active = dr["Active"].ToString().Trim(),
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LWF_Status;
        }

    }
}
