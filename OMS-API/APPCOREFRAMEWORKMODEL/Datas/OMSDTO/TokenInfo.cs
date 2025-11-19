using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class TokenInfo
    {
        public string url { get; set; }
        public string appkey { get; set; }
        public string appSecret { get; set; }
        public string XML { get; set; }
        public string oauthCode { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}
