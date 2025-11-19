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
    public class ProductCategoryController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductCategoryNopagingByCriteria")]
        public IHttpActionResult ListPrdCategoryBuild([FromBody]ProductCategoryInfo pInfo)
        {
            ProductCategoryListReturn prodcategoryList = new ProductCategoryListReturn();
            List<ProductCategoryListReturn> listProdcategory = new List<ProductCategoryListReturn>();
            ProductCategoryDAO lDAO = new ProductCategoryDAO();
            listProdcategory = lDAO.ListProductCategoryNopagingByCriteria(pInfo);

            return Ok<List<ProductCategoryListReturn>>(listProdcategory);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductCategoryLazadaNopagingByCriteria")]
        public IHttpActionResult ListPrdCategoryLazadaBuild([FromBody] ProductCategoryInfo pInfo)
        {
            ProductCategoryListReturn prodcategoryList = new ProductCategoryListReturn();
            List<ProductCategoryListReturn> listProdcategory = new List<ProductCategoryListReturn>();
            ProductCategoryDAO lDAO = new ProductCategoryDAO();
            listProdcategory = lDAO.ListProductCategoryLazadaNopagingByCriteria(pInfo);

            return Ok<List<ProductCategoryListReturn>>(listProdcategory);
        }


    }
}
