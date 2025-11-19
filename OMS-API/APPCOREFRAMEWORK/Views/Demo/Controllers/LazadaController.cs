using APPCOREMODEL.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data;
using System.Web.Http;
using APPCOREMODEL.OMSDAO;
using APPCOREMODEL.Datas.OMSDTO;
using APPCOREMODEL.Services.LazadaOpe;
using System.Security.Claims;
using System.Configuration;
using System.Web.Helpers;
using System.IO;


namespace APPCOREVIEW.Views.Demo.Controllers
{
    public class LazadaController : ApiController
    {

        [AllowAnonymous]
        [HttpGet]
        [Route("GetLazAuthToken")]
        public LazopResponse GetToken(string code)
        {
            TokenInfo tInfo = new TokenInfo();
            tInfo.url = "https://api.lazada.co.th/rest";
            tInfo.appkey = "101668";
            tInfo.appSecret = "6f8SiWKWPpXrQfZ73XU54bWEgfd5bDOl";
            ILazopClient client = new LazopClient(tInfo.url, tInfo.appkey, tInfo.appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/auth/token/create");
            request.AddApiParameter("code", code);
            //request.AddApiParameter("uuid", "38284839234");
            LazopResponse response = client.Execute(request);
            Console.WriteLine(response.IsError());
            Console.WriteLine(response.Body);
            return response;

        }
        [AllowAnonymous]
        [HttpGet]
        [Route("PostLazGetOrder")]
        public LazopResponse GetOrder([FromBody] TokenInfo tInfo)
        {
            ILazopClient client = new LazopClient(tInfo.url, tInfo.appkey, tInfo.appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/orders/get");
            request.SetHttpMethod("GET");
            request.AddApiParameter("update_before", "2018-02-10T16:00:00+08:00");
            request.AddApiParameter("sort_direction", "DESC");
            request.AddApiParameter("offset", "0");
            request.AddApiParameter("limit", "10");
            request.AddApiParameter("update_after", "2017-02-10T09:00:00+08:00");
            request.AddApiParameter("sort_by", "updated_at");
            request.AddApiParameter("created_before", "2018-02-10T16:00:00+08:00");
            request.AddApiParameter("created_after", "2017-02-10T09:00:00+08:00");
            request.AddApiParameter("status", "shipped");
            LazopResponse response = client.Execute(request, tInfo.AccessToken);
            Console.WriteLine(response.IsError());
            Console.WriteLine(response.Body);
            return response;

        }
        [AllowAnonymous]
        [HttpGet]
        [Route("PostLazRefreshToken")]
        public LazopResponse PostRefreshToken([FromBody] TokenInfo tInfo)
        {
            ILazopClient client = new LazopClient(tInfo.url, tInfo.appkey, tInfo.appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/auth/token/refresh");
            request.AddApiParameter("refresh_token", tInfo.RefreshToken);
            LazopResponse response = client.Execute(request);
            Console.WriteLine(response.IsError());
            Console.WriteLine(response.Body);
            return response;

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("PostLazAuthToken")]
        public LazopResponse PostToken([FromBody] TokenInfo tInfo)
        {

            ILazopClient client = new LazopClient(tInfo.url, tInfo.appkey, tInfo.appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/auth/token/create");
            request.AddApiParameter("code", "0_101668_5jN66S48AKTFIdvpX4t2GbvU17906");
            //request.AddApiParameter("uuid", "38284839234");
            LazopResponse response = client.Execute(request);
            Console.WriteLine(response.IsError());
            Console.WriteLine(response.Body);
            return response;

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("LazCreateProduct")]
        public List<lazProductinfo> CreateProduct([FromBody] TokenInfo tInfo)
        {
            ILazopClient client = new LazopClient(tInfo.url, tInfo.appkey, tInfo.appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/product/create");
            request.AddApiParameter("payload", tInfo.XML);
            LazopResponse response = client.Execute(request, tInfo.AccessToken);
            //string text = "{\"data\":{\"item_id\":2542405463,\"sku_list\":[{\"shop_sku\":\"2542405463_TH-9043283487\",\"seller_sku\":\"3433981000000\",\"sku_id\":9043283487}]},\"code\":\"0\",\"request_id\":\"0b0f469216250485078331317\"}";
            lazProductRes Res = new lazProductRes(response.Body);
            var LProduct = new List<lazProductinfo>();
            try
            {
                lazProductinfo pinfo = new lazProductinfo(Res.jdata.ToString());
                DataTable dt = new DataTable();
                dt.Columns.Add("sku_id");
                dt.Columns.Add("itemId");
                DataRow dr = null;
                foreach (var item in pinfo.sku_list)
                {
                    SKUList sku = new SKUList(item.ToString());
                    dr = dt.NewRow();
                    dr["sku_id"] = sku.sku_id;
                    dr["itemId"] = pinfo.Lazada_ItemId;
                    dt.Rows.Add(dr);
                }

                LProduct = (from DataRow row in dt.Rows

                            select new lazProductinfo(null)
                            {
                                Lazada_skuId = row["sku_id"].ToString().Trim(),
                                Lazada_ItemId = row["itemId"].ToString().Trim(),


                            }).ToList();

            }
            catch (Exception e)
            {


            }
            return LProduct;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("LazCreatePromotionFlexiCombo")]
        public string CreateFlexiCombo([FromBody] lazPromotioninfo tInfo)
        {
            ILazopClient client = new LazopClient(tInfo.url, tInfo.appkey, tInfo.appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/promotion/flexicombo/create");
            request.AddApiParameter("apply", tInfo.apply);
            //request.AddApiParameter("sample_skus", tInfo.sample_skus);
            request.AddApiParameter("criteria_type", tInfo.criteria_type);
            request.AddApiParameter("criteria_value", tInfo.criteria_value);
            request.AddApiParameter("order_numbers", tInfo.order_numbers);
            request.AddApiParameter("name", tInfo.name);
            //request.AddApiParameter("platform_channel", "1");
            //request.AddApiParameter("gift_skus", "[{\"productId\":\"442156001\",\"skuId\":\"1174240001\"}]");
            request.AddApiParameter("start_time", tInfo.start_time);
            request.AddApiParameter("discount_type", tInfo.discount_type);
            request.AddApiParameter("end_time", tInfo.end_time);
            request.AddApiParameter("discount_value", tInfo.discount_value);
            LazopResponse response = client.Execute(request, tInfo.AccessToken);

            //string text = "{\"error_msg\": \"null\",\"code\": \"0\",\"data\": \"9616200353530\",\"success\": \"true\",\"error_code\": \"null\",\"request_id\": \"0ba2887315178178017221014\"}";
            string LazadaPromotionId = "";
            try
            {
                lazPromotionRes Res = new lazPromotionRes(response.Body, "create");
                LazadaPromotionId = Res.data.ToString();
                if (tInfo.sample_skus != "")
                {
                    LazopRequest requestAddProduct = new LazopRequest();
                    requestAddProduct.SetApiName("/promotion/flexicombo/products/add");
                    requestAddProduct.AddApiParameter("id", LazadaPromotionId);
                    requestAddProduct.AddApiParameter("sku_ids", tInfo.sample_skus);
                    LazopResponse responseAddProduct = client.Execute(requestAddProduct, tInfo.AccessToken);

                }

            }
            catch (Exception e)
            {


            }

            return LazadaPromotionId;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("LazDeORActivatePromotionFlexiCombo")]
        public bool DeORActivateFlexiCombo([FromBody] lazPromotioninfo tInfo)
        {
            bool flag = false;
            LazopClient client = new LazopClient(tInfo.url, tInfo.appkey, tInfo.appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName(tInfo.API_Name);
            request.AddApiParameter("id", tInfo.PromotionFlexiId);
            LazopResponse response = client.Execute(request, tInfo.AccessToken);
            try
            {
                lazPromotionRes Res = new lazPromotionRes(response.Body, "deactivate");
                if (Res.success == "True")
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception e)
            {
                flag = false;
            }
            return flag;

        }
    }
}
