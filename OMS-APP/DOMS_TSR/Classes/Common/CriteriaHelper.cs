using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.Common
{
    public static class CriteriaHelper
    {
        public static string LikeIn(string value)
        {
            string ret = "";
            if(value.Trim() == "")
            {
                ret = " = '' ";
            }
            else if (value.IndexOf("|") > 0)
            {
                string tmp = value.Trim().Replace("|", "','");
                ret = " in ('" + tmp + "') ";
            }
            else
            {
                ret = " like '" + value + "' ";
            }
            return ret;
        }
    }
}
