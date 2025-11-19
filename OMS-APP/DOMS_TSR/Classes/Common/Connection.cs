using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
//using SourceCode.Hosting.Client.BaseAPI;

namespace SALEORDER.Common
{
    public  static class Connection
    {
        /// <summary>
        /// default connection สำหรับ workflow
        /// </summary>
        //public static string WorkflowClientConnectionString
        //{
        //    get
        //    {
        //        SCConnectionStringBuilder _ConnectionString = new SCConnectionStringBuilder();
                
        //        _ConnectionString.Clear();

        //        _ConnectionString.Authenticate = true;
        //        _ConnectionString.Host = Configuration.K2Host;
        //        _ConnectionString.Integrated = true;
        //        _ConnectionString.IsPrimaryLogin = true;
        //        _ConnectionString.Port = Configuration.K2WorkflowPort;
        //        _ConnectionString.SecurityLabelName = Configuration.K2SecurityLabel;
        //        _ConnectionString.AuthData = string.Empty;
        //        _ConnectionString.WindowsDomain = Configuration.DomainName;

        //        //#warning ตอน deploy ให้ set Configuration.UseCurrentCredential เป็น true ด้วยเด้อ

        //        //if (!Configuration.UseCurrentCredential)
        //        //{
        //        //    _ConnectionString.UserID = Configuration.LoginUser;
        //        //    _ConnectionString.Password = Configuration.LoginUserPassword;
        //        //}
                
        //        return _ConnectionString.ConnectionString;
        //    }
        //}

        ///// <summary>
        ///// จะใช้ User Credential ใน web.config
        ///// หากมีการเรียก overload method ที่มี impersonateTo, user ที่ใส่ใน web.config ต้องได้สิทธิ์ impersonate
        ///// </summary>
        //public static string WorkflowClientAdminConnectionString
        //{
        //    get
        //    {
        //        SCConnectionStringBuilder _ConnectionString = new SCConnectionStringBuilder();

        //        _ConnectionString.Clear();

        //        _ConnectionString.Authenticate = true;
        //        _ConnectionString.Host = Configuration.K2Host;
        //        _ConnectionString.Integrated = true;
        //        _ConnectionString.IsPrimaryLogin = true;
        //        _ConnectionString.Port = Configuration.K2WorkflowPort;
        //        _ConnectionString.SecurityLabelName = Configuration.K2SecurityLabel;
        //        _ConnectionString.AuthData = string.Empty;
        //        _ConnectionString.WindowsDomain = Configuration.DomainName;
                
        //        //impersonate permission must be apply
        //        _ConnectionString.UserID = Configuration.LoginUser;
        //        _ConnectionString.Password = Configuration.LoginUserPassword;

        //        return _ConnectionString.ConnectionString;
        //    }
        //}


        ///// <summary>
        ///// User ที่ใส่ใน web.config ต้องได้สิทธิ์ Admin บน Workflow Server
        ///// </summary>
        //public static string WorkflowManagementConnectionString
        //{
        //    get
        //    {
        //        SCConnectionStringBuilder _ConnectionString = new SCConnectionStringBuilder();

        //        _ConnectionString.Clear();

        //        _ConnectionString.Authenticate = true;
        //        _ConnectionString.Host = Configuration.K2Host;
        //        _ConnectionString.Integrated = true;
        //        _ConnectionString.IsPrimaryLogin = true;
        //        _ConnectionString.Port = Configuration.K2HostPort;
        //        _ConnectionString.SecurityLabelName = Configuration.K2SecurityLabel;
        //        _ConnectionString.AuthData = string.Empty;
        //        _ConnectionString.WindowsDomain = Configuration.DomainName;

        //        //workflow admin permission must be apply
        //        _ConnectionString.UserID = Configuration.LoginUser;
        //        _ConnectionString.Password = Configuration.LoginUserPassword;

        //        return _ConnectionString.ConnectionString;
        //    }
        //}

        ////by wi
        //public static string WorkflowAdminConnectionString2
        //{
        //    get
        //    {
        //        SCConnectionStringBuilder _ConnectionString = new SCConnectionStringBuilder();

        //        _ConnectionString.Clear();

        //        _ConnectionString.Authenticate = true;
        //        _ConnectionString.Host = ConfigurationManager.AppSettings["K2Server"];
        //        _ConnectionString.Integrated = true;
        //        _ConnectionString.IsPrimaryLogin = true;
        //        _ConnectionString.Port = Convert.ToUInt16(ConfigurationManager.AppSettings["K2HostPort"]);
        //        _ConnectionString.SecurityLabelName = ConfigurationManager.AppSettings["SecurityLabel"];
        //        _ConnectionString.AuthData = string.Empty;
        //        _ConnectionString.WindowsDomain = ConfigurationManager.AppSettings["WindowsDomain"];
        //        _ConnectionString.UserID = ConfigurationManager.AppSettings["K2User"];
        //        _ConnectionString.Password = ConfigurationManager.AppSettings["K2Password"];

        //        return _ConnectionString.ConnectionString;

        //        //SCConnectionStringBuilder _ConnectionString = new SCConnectionStringBuilder();

        //        //_ConnectionString.Clear();

        //        //_ConnectionString.Authenticate = true;
        //        //_ConnectionString.Host = Configuration.K2Host;
        //        //_ConnectionString.Integrated = true;
        //        //_ConnectionString.IsPrimaryLogin = true;
        //        //_ConnectionString.Port = Configuration.K2HostPort;
        //        //_ConnectionString.SecurityLabelName = Configuration.K2SecurityLabel;
        //        //_ConnectionString.AuthData = string.Empty;
        //        //_ConnectionString.WindowsDomain = Configuration.DomainName;

        //        ////workflow admin permission must be apply
        //        //_ConnectionString.UserID = Configuration.LoginUser;
        //        //_ConnectionString.Password = Configuration.LoginUserPassword;

        //        //return _ConnectionString.ConnectionString;
        //    }
        //}
    
    } 
}
