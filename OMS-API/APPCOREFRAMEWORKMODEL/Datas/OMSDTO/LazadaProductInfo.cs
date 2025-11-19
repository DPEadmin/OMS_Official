using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class lazProductRes
    {
        public lazProductRes(string json)
        {
            if (json != null)
            {
                JObject jObject = JObject.Parse(json);
                jdata = jObject["data"];

            }
        }
        public JToken jdata { get; set; }


    }
    public class ProductCatagoryTree
    {
        public ProductCatagoryTree(string json)
        {
            if (json != null)
            {
                JObject jObject = JObject.Parse(json);
                jchildren = jObject["children"];
                name = jObject["name"].ToString();
                category_id = Int32.Parse(jObject["category_id"].ToString());
                leaf = bool.Parse(jObject["leaf"].ToString());
            }

        }
        public JToken jchildren { get; set; }
        public int category_id { get; set; }
        public bool leaf { get; set; }
        public string Refcategory_id { get; set; }
        public string name { get; set; }
        public bool var { get; set; }

    }
    public class lazProductinfo
    {
        public lazProductinfo(string json)
        {
            if (json != null)
            {
                JObject jObject = JObject.Parse(json);
                Lazada_ItemId = jObject["item_id"].ToString();
                sku_list = jObject["sku_list"].ToArray();
            }
        }
        public string Lazada_ItemId { get; set; }
        public Array sku_list { get; set; }

        public string Lazada_skuId { get; set; }

    }
    public class SKUList
    {
        public SKUList(string json)
        {
            if (json != null)
            {
                JObject jObject = JObject.Parse(json);
                shop_sku = jObject["shop_sku"].ToString();
                seller_sku = jObject["seller_sku"].ToString();
                sku_id = jObject["sku_id"].ToString();
            }
        }
        public string shop_sku { get; set; }
        public string seller_sku { get; set; }

        public string sku_id { get; set; }



    }
}
