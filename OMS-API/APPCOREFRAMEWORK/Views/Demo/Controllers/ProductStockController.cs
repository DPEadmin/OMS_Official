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
    public class ProductStockController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductStockByCriteria")]
        public IHttpActionResult ListPdStockByCriteria([FromBody]ProductStockInfo cInfo)
        {
            ProductStockListReturn productStockList = new ProductStockListReturn();
            List<ProductStockListReturn> listProductStock = new List<ProductStockListReturn>();
            ProductStockDAO cDAO = new ProductStockDAO();

            listProductStock = cDAO.ListProductStockByCriteria(cInfo);

            return Ok<List<ProductStockListReturn>>(listProductStock);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountProductStockListByCriteria")]
        public IHttpActionResult CountProductStockListByCriteria([FromBody]ProductStockInfo cInfo)
        {
            //CampaignListReturn campaignList = new CampaignListReturn();
            //List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            ProductStockDAO cDAO = new ProductStockDAO();
            int? i = 0;
            i = cDAO.CountProductStockListByCriteria(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteProductstock")]
        public IHttpActionResult DelProductstock([FromBody]ProductStockInfo cInfo)
        {
            ProductStockDAO cDAO = new ProductStockDAO();
            int i = 0;
            i = cDAO.DeleteProductstock(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertProductStock")]
        public IHttpActionResult InsProductStock([FromBody]ProductStockInfo cInfo)
        {
            ProductStockDAO cDAO = new ProductStockDAO();
            int i = 0;
            i = cDAO.InsertProductStock(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateProductStock")]
        public IHttpActionResult UpdProductStock([FromBody]ProductStockInfo cInfo)
        {
            ProductStockDAO cDAO = new ProductStockDAO();
            int i = 0;
            i = cDAO.UpdateProductStock(cInfo);

            return Ok<int>(i);
        }
    }
}
