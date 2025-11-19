using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SALEORDER.WebContent.Common
{
    public class BaseForm : System.Web.UI.Page
    {
        protected static string _DELETE_ERROR = ConfigurationManager.AppSettings["_DELETE_ERROR"].ToString();
        protected static string _LOGIN_ERROR = ConfigurationManager.AppSettings["_LOGIN_ERROR"].ToString();
        protected static string _INSERT_ERROR = ConfigurationManager.AppSettings["_INSERT_ERROR"].ToString();
        protected static string _INSERT_SUCCESS = ConfigurationManager.AppSettings["_INSERT_SUCCESS"].ToString();
        protected static string _UPDATE_ERROR = ConfigurationManager.AppSettings["_UPDATE_ERROR"].ToString();
        protected static string _UPDATE_SUCCESS = ConfigurationManager.AppSettings["_UPDATE_SUCCESS"].ToString();
        protected static string _MSG_VALIDATE_PLEASEINSERT = ConfigurationManager.AppSettings["_MSG_VALIDATE_PLEASEINSERT"].ToString();
        protected static string _MSG_PLEASEINSERT = ConfigurationManager.AppSettings["_MSG_PLEASEINSERT"].ToString();
        protected static string _MSG_PLEASESELECT = ConfigurationManager.AppSettings["_MSG_PLEASESELECT"].ToString();
        protected static string _MSG_NOT_DUPLICATE = ConfigurationManager.AppSettings["_MSG_NOT_DUPLICATE"].ToString();
        protected static string _CONFIRM_DELETE = ConfigurationManager.AppSettings["_CONFIRM_DELETE"].ToString();
        protected static string _UPDATE_REQUERY_DAR_SUCCESS = ConfigurationManager.AppSettings["_UPDATE_REQUERY_DAR_SUCCESS"].ToString();
        protected static string _UPDATE_REQUERY_DAR_ERROR = ConfigurationManager.AppSettings["_UPDATE_REQUERY_DAR_ERROR"].ToString();
        protected static string _ACTION_SUBMIT = ConfigurationManager.AppSettings["_ACTION_SUBMIT"].ToString();
        protected static string _ACTION_SUBMITEDIT = ConfigurationManager.AppSettings["_ACTION_SUBMITEDIT"].ToString();
        protected static string _ACTION_SUPERUSER_SUBMITEDIT = ConfigurationManager.AppSettings["_ACTION_SUPERUSER_SUBMITEDIT"].ToString();
        protected static string _ACTION_SAVEDRAFT = ConfigurationManager.AppSettings["_ACTION_SAVEDRAFT"].ToString();
        protected static string _ACTION_APPROVE = ConfigurationManager.AppSettings["_ACTION_APPROVE"].ToString();
        protected static string _ACTION_REJECT = ConfigurationManager.AppSettings["_ACTION_REJECT"].ToString();
        protected static string _ACTION_REVISE = ConfigurationManager.AppSettings["_ACTION_REVISE"].ToString();
        protected static string _ACTION_NOTAPPROVE = ConfigurationManager.AppSettings["_ACTION_NOTAPPROVE"].ToString();
        
    }
}