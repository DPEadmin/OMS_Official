using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class lazPromotionRes
    {
        public lazPromotionRes(string json, string type)
        {
            if (json != null)
            {
                JObject jObject = JObject.Parse(json);
                code = jObject["code"].ToString();
                if (type == "create")
                {
                    data = jObject["data"].ToString();
                }
                else if (type == "deactivate")
                {
                    success = jObject["success"].ToString();
                }

            }
        }
        public string code { get; set; }
        public string data { get; set; }
        public string success { get; set; }
    }
    public class lazPromotioninfo : TokenInfo
    {
        public string apply { get; set; }
        public string API_Name { get; set; }
        public string sample_skus { get; set; }
        public string criteria_type { get; set; }
        public string criteria_value { get; set; }
        public string order_numbers { get; set; }
        public string name { get; set; }
        public string platform_channel { get; set; }
        public string gift_skus { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string discount_type { get; set; }
        public string discount_value { get; set; }
        public string PromotionFlexiId { get; set; }
    }
}
