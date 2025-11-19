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
    public class SaleChannelController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateSaleChannel")]
        public IHttpActionResult UpdSaleChannel([FromBody]SaleChannelInfo pInfo)
        {

            SaleChannelDAO pDAO = new SaleChannelDAO();
            int i = 0;
            i = pDAO.UpdateSaleChannel(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteSaleChannel")]
        public IHttpActionResult DelSaleChannel([FromBody]SaleChannelInfo pInfo)
        {

            SaleChannelDAO pDAO = new SaleChannelDAO();
            int i = 0;
            i = pDAO.DeleteSaleChannelList(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertSaleChannel")]
        public IHttpActionResult InsSaleChannel([FromBody]SaleChannelInfo pInfo)
        {

            SaleChannelDAO pDAO = new SaleChannelDAO();
            int i = 0;  
            i = pDAO.InsertSaleChannel(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListSaleChannelNoPagingByCriteria")]
        public IHttpActionResult ListPmNoPagingByCriteria([FromBody]SaleChannelInfo pInfo)
        {
            SaleChannelListReturn SaleChannelList = new SaleChannelListReturn();
            List<SaleChannelListReturn> listSaleChannel = new List<SaleChannelListReturn>();
            SaleChannelDAO pDAO = new SaleChannelDAO();

            listSaleChannel = pDAO.ListSaleChannelNoPagingByCriteria(pInfo);

            return Ok<List<SaleChannelListReturn>>(listSaleChannel);
        }

        

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountSaleChannelListByCriteria")]
        public IHttpActionResult CountPromoListByCriteria([FromBody]SaleChannelInfo pInfo)
        {
            SaleChannelDAO pDAO = new SaleChannelDAO();
            int? i = 0;
            i = pDAO.CountSaleChannelListByCriteria(pInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListSaleChannel")]
        public IHttpActionResult CountListSaleChannel([FromBody]SaleChannelInfo pInfo)
        {
            SaleChannelDAO pDAO = new SaleChannelDAO();
            int? i = 0;
            i = pDAO.CountListSaleChannel(pInfo);

            return Ok<int?>(i);
        }

    }
}
