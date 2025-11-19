using APPCOREMODEL.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APPCOREMODEL.OMSDAO;
using APPCOREMODEL.Datas.OMSDTO;
using System.Security.Claims;
using System.Configuration;
using System.Web.Helpers;
using System.IO;

namespace APPCOREVIEW.Views.Demo.Controllers
{
    public class ProductController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListComplementaryPromotionDetailByCriteria")]
        public IHttpActionResult FirstBuild([FromBody]ProductInfo pInfo)
        {
            ResultProductList resultProductList = new ResultProductList();
            ProductListReturn productList = new ProductListReturn();
            List<ProductListReturn> listProduct = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listProduct = pDAO.ListComplementaryPromotionDetailByCriteria(pInfo);

            foreach (var productV in listProduct)
            {
                resultProductList.productList.Add(new ProductListReturn() { ProductCode = productV.ProductCode });
            }

            return Ok<List<ProductListReturn>>(listProduct);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductNopagingByCriteria")]
        public IHttpActionResult SecondBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productList = new ProductListReturn();
            List<ProductListReturn> listProduct = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listProduct = pDAO.ListProductNopagingByCriteria(pInfo);

            return Ok<List<ProductListReturn>>(listProduct);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductNopagingByEcommerce")]
        public IHttpActionResult ListProductNopagingByEcommerce([FromBody] ProductInfo pInfo)
        {
            ProductListReturn productList = new ProductListReturn();
            List<ProductListReturn> listProduct = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listProduct = pDAO.ListProductNopagingByEcommerce(pInfo);

            return Ok<List<ProductListReturn>>(listProduct);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductDetailNopagingByEcommerce")]
        public IHttpActionResult ListProductDetailNopagingByEcommerce([FromBody] ProductInfo pInfo)
        {
            ProductListReturn productList = new ProductListReturn();
            List<ProductListReturn> listProduct = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listProduct = pDAO.ListProductDetailNopagingByEcommerce(pInfo);

            if (listProduct.Count > 0)
            {
                foreach (var od in listProduct)
                {
                    char[] separator = new char[1] { ',' };
                    string result = "";
                    string[] subResult = od.PromotionTagCode.Split(separator);

                    for (int i = 0; i <= subResult.Length - 1; i++)
                    {
                        if (subResult[i].ToString() != "" && subResult[i].ToString() != null)
                        {
                            LookupInfo lInfo = new LookupInfo();
                            LookupDAO lDao = new LookupDAO();

                            lInfo.LookupCode = subResult[i];
                            lInfo.LookupType = "TAGPROMOTION";

                            List<LookupListReturn> lreturnInfo = new List<LookupListReturn>();

                            lreturnInfo = lDao.ListLookupNopagingByCriteria(lInfo);

                            if (lreturnInfo.Count > 0)
                            {
                                result += lreturnInfo[0].LookupValue + ",";
                            }
                        }
                    }

                    if (result != "" && result != null)
                    {
                        result = result.Remove(result.Length - 1);
                    }

                    od.PromotionTagName = result;

                    char[] separator1 = new char[1] { ',' };
                    string result1 = "";
                    string[] subResult1 = od.PromotionTagCode.Split(separator1);

                    for (int i = 0; i <= subResult1.Length - 1; i++)
                    {
                        if (subResult1[i].ToString() != "" && subResult1[i].ToString() != null)
                        {
                            LookupInfo lInfo1 = new LookupInfo();
                            LookupDAO lDao = new LookupDAO();

                            lInfo1.LookupCode = subResult1[i];
                            lInfo1.LookupType = "TAGPRODUCT";

                            List<LookupListReturn> lreturnInfo1 = new List<LookupListReturn>();

                            lreturnInfo1 = lDao.ListLookupNopagingByCriteria(lInfo1);

                            if (lreturnInfo1.Count > 0)
                            {
                                result1 += lreturnInfo1[0].LookupValue + ",";
                            }
                        }
                    }

                    if (result1 != "" && result1 != null)
                    {
                        result1 = result1.Remove(result1.Length - 1);
                    }

                    od.ProductTagName = result1;
                }
            }

            return Ok<List<ProductListReturn>>(listProduct);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateProductDetailQuotaBalancebyEcommerce")]
        public IHttpActionResult UpdateProductDetailQuotaBalancebyEcommerce([FromBody] ProductInfo pInfo)
        {
            ProductDAO pDAO = new ProductDAO();
            List<ProductListReturn> lpreturn = new List<ProductListReturn>();

            lpreturn = pDAO.ListProductDetailNopagingByEcommerce(pInfo);

            int? i = 0;

            if (lpreturn.Count > 0)
            {
                ProductListReturn productReturn = new ProductListReturn();
                int? quotareserved = 0;
                int? quotabalance = 0;
                
                quotareserved = lpreturn[0].QuotaReserved + pInfo.Amount;
                quotabalance = lpreturn[0].QuotaOnHand - quotareserved;
                
                pInfo.QuotaReserved = quotareserved;
                pInfo.QuotaBalance = quotabalance;
                pInfo.PromotionDetailId = lpreturn[0].PromotionDetailId;

                i = 0;
                ProductDAO pDAO1 = new ProductDAO();

                i = pDAO1.UpdateProductDetailQuotaBalancebyEcommerce(pInfo);
                
            }

            if (i > 0)
            {
                return Ok<int?>(i);
            }
            else
            {
                i = 0;

                return Ok<int?>(i);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductBrandNopagingByCriteria")]
        public IHttpActionResult SecondyBuild([FromBody]ProductBrandInfo pInfo)
        {

            List<ProductBrandListReturn> listProduct = new List<ProductBrandListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listProduct = pDAO.ListProductBrandNopagingByCriteria(pInfo);

            return Ok<List<ProductBrandListReturn>>(listProduct);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ProductList")]
        public IHttpActionResult ThirdBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ProductList(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListTopReservedProductStockByCriteria")]
        public IHttpActionResult FourthBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ListTopReservedProductStockByCriteria(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListTopReservedProductInventoryDetailByCriteria")]
        public IHttpActionResult FifthBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ListTopReservedProductInventoryDetailByCriteria(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductMasterNopagingByCriteria")]
        public IHttpActionResult SixthBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ListProductMasterNopagingByCriteria(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductMasterByCriteria")]
        public IHttpActionResult SeventhhBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ListProductMasterByCriteria(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ProductCheck")]
        public IHttpActionResult EighthBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ProductCheck(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountProductMasterListByCriteria")]
        public IHttpActionResult NinthBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.CountProductMasterListByCriteria(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountProductListModalByCriteria")]
        public IHttpActionResult CountProductListModalBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.CountProductListModalByCriteria(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountProduct")]
        public IHttpActionResult TenthBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.CountProduct(pInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductByCriteria")]
        public IHttpActionResult EleventhBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ListProductByCriteria(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountProductListByCriteria")]
        public IHttpActionResult TwelthBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.CountProductListByCriteria(pInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteProduct")]
        public IHttpActionResult ThirteenthBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.DeleteProductList(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertProduct")]
        public IHttpActionResult FourteenthBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.InsertProduct(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateProduct")]
        public IHttpActionResult FifteenthBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.UpdateProduct(pInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateLazadaProduct")]
        public IHttpActionResult UpdateLazadaProduct([FromBody] ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.UpdateLazadaProduct(pInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductShowAll")]
        public IHttpActionResult SixteenthBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ListProductShowAll(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetProductName")]
        public IHttpActionResult SeventeenthBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.GetProductName(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetProductImageUrl")]
        public IHttpActionResult EighteenthBuild([FromBody]ProductImageInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductImageListReturn> listproductReturn = new List<ProductImageListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.GetProductImageUrl(pInfo);

            return Ok<List<ProductImageListReturn>>(listproductReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductImageValidate")]
        public IHttpActionResult NineteenthBuild([FromBody]ProductImageInfo pInfo)
        {
            ProductImageListReturn productReturn = new ProductImageListReturn();
            List<ProductImageListReturn> listproductReturn = new List<ProductImageListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ListProductImageValidate(pInfo);

            return Ok<List<ProductImageListReturn>>(listproductReturn);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertProductImage")]
        public IHttpActionResult TwentiethBuild([FromBody]ProductImageInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.InsertProductImage(pInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateProductImage")]
        public IHttpActionResult TwentyfirstBuild([FromBody]ProductImageInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.UpdateProductImage(pInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListProductBOMByCriteria")]
        public IHttpActionResult TwentysecondBuild([FromBody]ProductBOMInfo pInfo)
        {
            ProductBOMListReturn productReturn = new ProductBOMListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.CountListProductBOMByCriteria(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductBOMByCriteria")]
        public IHttpActionResult TwentythridBuild([FromBody]ProductBOMInfo pInfo)
        {
            ProductBOMListReturn productReturn = new ProductBOMListReturn();
            List<ProductBOMListReturn> listproductReturn = new List<ProductBOMListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ListProductBOMByCriteria(pInfo);

            return Ok<List<ProductBOMListReturn>>(listproductReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertProductBOM")]
        public IHttpActionResult InsertProductBOM([FromBody] ProductBOMInfo pInfo)
        {
            ProductBOMListReturn productReturn = new ProductBOMListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.InsertProductBOM(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteProductBOM")]
        public IHttpActionResult DeleteProductBOM([FromBody] ProductBOMInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.DeleteProductBOM(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/Savepicfromjsonstring64/")]
        public IHttpActionResult TwentyfourBuild([FromBody]ProductImageInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();

            String mediaPath = ConfigurationManager.AppSettings["UploadPicProductPath"];
            string filename = "";

            //string path = @"~\Images\";
            String adate = DateTime.Now.ToString("ddMMyyyyHHmmss");
            filename = "S" + adate + ".jpg";
            // string filePath = System.Web.HttpContext.Current.Server.MapPath(mediaPath + "\\" + filename);
            string filePath = System.Web.HttpContext.Current.Server.MapPath(mediaPath + "\\" + pInfo.ProductImageName);

            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(pInfo.ProductImageBase64);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(filePath);
            }
            return Ok();
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ProductCodeValidateInsert")]
        public IHttpActionResult TwentyThreeBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ProductCodeValidateInsert(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ProductCodeValidateInventorydetail")]
        public IHttpActionResult ProductCodeValidateInventorydetail([FromBody] ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ProductCodeValidateInventorydetail(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertProductfromImportProduct")]
        public IHttpActionResult InsertProductfromImportProduct([FromBody] ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.InsertProductfromImportProduct(pInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateProductfromImportProduct")]
        public IHttpActionResult UpdateProductfromImportProduct([FromBody] ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.UpdateProductfromImportProduct(pInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductInPromotion")]
        public IHttpActionResult TwentyForthBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ListProductInPromotion(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductInPromotionNoPaging")]
        public IHttpActionResult TwentyFifthBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ListProductInPromotionNoPaging(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountProductListInPromotion")]
        public IHttpActionResult TwentythirdBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.CountProductListInPromotion(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertProductImport")]
        public IHttpActionResult InsertProdImport([FromBody]L_ProductList lproductdata)
        {
            ProductDAO pDAO = new ProductDAO();
            int sum = 0;
            string transportationtypecode = "";
            string productcode = "";

            foreach (var od in lproductdata.ProductListReturn.ToList())
            {
                transportationtypecode = CutStringMethod(od.TransportationTypeCode);
            }

            sum = pDAO.InsertProductImport(lproductdata.ProductListReturn.ToList());

            return Ok<int>(sum);
        }
        public string CutStringMethod(string strCode)
        {
            char[] separator = new char[1] { ',' };
            string result = "";
            string[] subResult = strCode.Split(separator);
            for (int i = 0; i <= subResult.Length - 1; i++)
            {
                if (i < subResult.Length - 1)
                {
                    result += "'" + subResult[i] + "',";
                }
                else
                {
                    result += "'" + subResult[i] + "'";
                }
            }
            return result;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListTop5ProductodOrderCustomerByCriteria")]
        public IHttpActionResult ListTop5ProductofOrderCUs([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
           listproductReturn = pDAO.ListTop5ProductodOrderCustomerByCriteria(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountProductRunningNumber")]
        public IHttpActionResult GodBuild([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.CountProductRunningNumber(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductMainByRecipeNameCriteria")]
        public IHttpActionResult ListofProductMainByRecipeNameCriteria([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ListProductMainByRecipeNameCriteria(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductExchangeByRecipeNameCriteria")]
        public IHttpActionResult ListofProductExchangeByRecipeNameCriteria([FromBody]ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ListProductExchangeByRecipeNameCriteria(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListAllergyNopagingByCriteria")]
        public IHttpActionResult TTBuild([FromBody]AllergyInfo pInfo)
        {

            List<AllergyListReturn> listproductReturn = new List<AllergyListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ListAllergyNopagingByCriteria(pInfo);

            return Ok<List<AllergyListReturn>>(listproductReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertProductfromImportInventoryDetail")]
        public IHttpActionResult InsertProductfromImportInventoryDetail([FromBody] ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.InsertProductfromImportInventoryDetail(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateProductfromImportInventoryDetail")]
        public IHttpActionResult UpdateProductfromImportInventoryDetail([FromBody] ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            int? i = 0;
            ProductDAO pDAO = new ProductDAO();
            i = pDAO.UpdateProductfromImportInventoryDetail(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ValidateProductinPromotion")]
        public IHttpActionResult ValidateProductinPromotion([FromBody] ProductInfo pInfo)
        {
            ProductListReturn productReturn = new ProductListReturn();
            List<ProductListReturn> listproductReturn = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listproductReturn = pDAO.ValidateProductinPromotion(pInfo);

            return Ok<List<ProductListReturn>>(listproductReturn);
        }

    }
}
