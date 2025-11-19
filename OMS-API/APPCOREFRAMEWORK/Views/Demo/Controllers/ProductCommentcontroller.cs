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
    public class ProductCommentcontroller : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductComment")]
        public IHttpActionResult ProductCommentListNoPagingCriteria([FromBody] ProductCommentInfo pInfo)
        {
            
            List<ProductCommentInfo> lProductCommentInfo = new List<ProductCommentInfo>();
            ProductCommentDAO mDAO = new ProductCommentDAO();

            lProductCommentInfo = mDAO.ProductCommentListNoPagingCriteria(pInfo);

            return Ok<List<ProductCommentInfo>>(lProductCommentInfo);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertProductComment")]
        public IHttpActionResult InsertProductComment([FromBody] ProductCommentInfo PfInfo)
        {
            int iProductCommentInfo = 0;
            ProductCommentDAO mDAO = new ProductCommentDAO();

            iProductCommentInfo = mDAO.InsertProductComment(PfInfo);

            return Ok<int>(iProductCommentInfo);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertProductCommentR")]
        public IHttpActionResult InsertProductCommentR([FromBody] ProductCommentInfo RfInfo)
        {
            int iProductCommentInfoR = 0;
            ProductCommentDAO mDAO = new ProductCommentDAO();

            iProductCommentInfoR = mDAO.InsertProductComment(RfInfo);

            return Ok<int>(iProductCommentInfoR);
        }
    }
    
}

