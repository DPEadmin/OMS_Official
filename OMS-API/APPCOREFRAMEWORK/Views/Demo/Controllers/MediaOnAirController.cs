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
    public class MediaOnAirController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateMediaOnAir")]
        public IHttpActionResult UpdMediaOnAir([FromBody]MediaOnAirInfo pInfo)
        {

            MediaOnAirDAO pDAO = new MediaOnAirDAO();
            int i = 0;
            i = pDAO.UpdateMediaOnAir(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteMediaOnAir")]
        public IHttpActionResult DelMediaOnAir([FromBody]MediaOnAirInfo pInfo)
        {

            MediaOnAirDAO pDAO = new MediaOnAirDAO();
            int i = 0;
            i = pDAO.DeleteMediaOnAirList(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertMediaOnAir")]
        public IHttpActionResult InsMediaOnAir([FromBody]MediaOnAirInfo pInfo)
        {

            MediaOnAirDAO pDAO = new MediaOnAirDAO();
            int i = 0;  
            i = pDAO.InsertMediaOnAir(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMediaOnAirNoPagingByCriteria")]
        public IHttpActionResult ListPmNoPagingByCriteria([FromBody]MediaOnAirInfo pInfo)
        {
            MediaOnAirListReturn MediaOnAirList = new MediaOnAirListReturn();
            List<MediaOnAirListReturn> listMediaOnAir = new List<MediaOnAirListReturn>();
            MediaOnAirDAO pDAO = new MediaOnAirDAO();

            listMediaOnAir = pDAO.ListMediaOnAirNoPagingByCriteria(pInfo);

            return Ok<List<MediaOnAirListReturn>>(listMediaOnAir);
        }

        

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountMediaOnAirListByCriteria")]
        public IHttpActionResult CountPromoListByCriteria([FromBody]MediaOnAirInfo pInfo)
        {
            MediaOnAirDAO pDAO = new MediaOnAirDAO();
            int? i = 0;
            i = pDAO.CountMediaOnAirListByCriteria(pInfo);

            return Ok<int?>(i);
        }

        

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListMediaOnAir")]
        public IHttpActionResult CountListPromo([FromBody]MediaOnAirInfo pInfo)
        {
            MediaOnAirDAO pDAO = new MediaOnAirDAO();
            int? i = 0;
            i = pDAO.CountListMediaOnAir(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetMediaOnAirTakeOrder")]
        public IHttpActionResult GetMediaOnAirTakeOrder([FromBody]MediaOnAirInfo pInfo)
        {
            MediaOnAirListReturn MediaOnAirList = new MediaOnAirListReturn();
            List<MediaOnAirListReturn> listMediaOnAir = new List<MediaOnAirListReturn>();
            MediaOnAirDAO pDAO = new MediaOnAirDAO();

            listMediaOnAir = pDAO.GetMediaOnAirTakeOrder(pInfo);

            return Ok<List<MediaOnAirListReturn>>(listMediaOnAir);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMediaSaleChannelNoPagingByCriteria")]
        public IHttpActionResult ListMediaSaleChannelNoPagingByCriteria([FromBody]MediaSaleChannelInfo pInfo)
        {
            MediaSaleChannelReturnInfo MediaOnAirList = new MediaSaleChannelReturnInfo();
            List<MediaSaleChannelReturnInfo> listMediaOnAir = new List<MediaSaleChannelReturnInfo>();
            MediaOnAirDAO pDAO = new MediaOnAirDAO();

            listMediaOnAir = pDAO.ListMediaSaleChannelByCriteria(pInfo);

            return Ok<List<MediaSaleChannelReturnInfo>>(listMediaOnAir);
        }


    }
}
