using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SALEORDER.Common
{
    class Configuration
    {
        public static string K2Host
        {
            get
            {
                return ConfigurationManager.AppSettings["K2Server"];
            }
        }
        public static ushort K2HostPort
        {
            get
            {
                return Convert.ToUInt16(ConfigurationManager.AppSettings["K2HostPort"]);
            }

        }
        public static ushort K2WorkflowPort
        {
            get
            {
                return Convert.ToUInt16(ConfigurationManager.AppSettings["K2WorkflowPort"]);
            }
        }
        public static string K2SecurityLabel
        {
            get
            {
                return ConfigurationManager.AppSettings["SecurityLabel"];
            }
        }
        public static string DomainName
        {
            get
            {
                return ConfigurationManager.AppSettings["WindowsDomain"];
            }
        }
        public static string LoginUser
        {
            get
            {
                return ConfigurationManager.AppSettings["K2User"];
            }
        }
        public static string LoginUserPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["K2Password"];
            }
        }
    }
}
