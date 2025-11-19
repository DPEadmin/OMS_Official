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

namespace APPCOREVIEW.Views.Cms.Controllers
{
    public class CmsProductCategoryController : ApiController
    {

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCategoriesProductList")]
        public IHttpActionResult CategoriesProductList([FromBody]ProductCategoryInfo pInfo)
        {

            ProductCategoryDAO lDAO = new ProductCategoryDAO();
            ProductDAO pDAO = new ProductDAO();
            ProductInfo ppInfo = new ProductInfo();

            List<CategoryProductListReturn> listCategoryProduct = new List<CategoryProductListReturn>();
            List<ProductCategoryListReturn> listProdcategory = new List<ProductCategoryListReturn>();

            List<ProductCategoryListReturn> ListCategory = new List<ProductCategoryListReturn>();
            List<ProductListReturn> ListProduct = new List<ProductListReturn>();

            listProdcategory = lDAO.ListProductCategoryNopagingByCriteria(pInfo);


            foreach (var Cate in listProdcategory)
            {
                ListCategory = lDAO.ListProductCategory(Cate.ProductCategoryCode);
                ListProduct = lDAO.ListProduct(Cate.ProductCategoryCode);

                listCategoryProduct.Add(new CategoryProductListReturn()
                {
                    Category = ListCategory,
                    Product = ListProduct

                });
            }


            return Ok<List<CategoryProductListReturn>>(listCategoryProduct);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetProductBySlug")]
        public IHttpActionResult GetProductBySlug([FromBody]ProductInfo pInfo)
        {

            List<ProductListReturn> listProd = new List<ProductListReturn>();
            ProductDAO pDAO = new ProductDAO();
            listProd = pDAO.GetProductBySlug(pInfo);

            return Ok<List<ProductListReturn>>(listProd);
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetCategoryBySlug")]
        public IHttpActionResult GetCategoryBySlug([FromBody]ProductCategoryInfo pInfo)
        {
            ProductCategoryListReturn prodcategoryList = new ProductCategoryListReturn();
            List<ProductCategoryListReturn> listProdcategory = new List<ProductCategoryListReturn>();
            ProductCategoryDAO lDAO = new ProductCategoryDAO();
            listProdcategory = lDAO.ListProductCategoryNopagingByCriteria(pInfo);

            return Ok<List<ProductCategoryListReturn>>(listProdcategory);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetProductList")]
        public IHttpActionResult GetProductList([FromBody]ProductCategoryInfo pInfo)
        {
            ProductCategoryListReturn prodcategoryList = new ProductCategoryListReturn();
            List<ProductListReturn> listProdcategory = new List<ProductListReturn>();
            ProductCategoryDAO lDAO = new ProductCategoryDAO();
            listProdcategory = lDAO.ListProduct(pInfo.ProductCategoryCode.ToString());

            return Ok<List<ProductListReturn>>(listProdcategory);
        }




        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetProductImage")]
        public IHttpActionResult GetProductImage([FromBody]ProductInfo pInfo)
        {
           
            List<ProductImageListReturn> listProductImg = new List<ProductImageListReturn>();
            ProductCategoryDAO lDAO = new ProductCategoryDAO();
            listProductImg = lDAO.ListProductImage(pInfo.ProductCode.ToString());

            return Ok<List<ProductImageListReturn>>(listProductImg);
        }
    }
}
