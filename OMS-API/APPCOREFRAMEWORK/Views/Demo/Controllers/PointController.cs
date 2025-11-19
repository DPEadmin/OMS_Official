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
    public class PointController : ApiController
    {
        
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdatePointRate")]
        public IHttpActionResult UpdatePointRate([FromBody] PointInfo pInfo)
        {
            PointDAO cDAO = new PointDAO();
            int i = 0;
            i = cDAO.UpdatePointRate(pInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertPointRange")]
        public IHttpActionResult InsertPointRange([FromBody] PointInfo pInfo)
        {
            PointDAO cDAO = new PointDAO();
            int i = 0;
            i = cDAO.InsertPointRange(pInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertPointRate")]
        public IHttpActionResult InsertPointRate([FromBody] PointInfo pInfo)
        {
            PointDAO cDAO = new PointDAO();
            int i = 0;
            i = cDAO.InsertPointRate(pInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeletePointRange")]
        public IHttpActionResult DeletePointRange([FromBody] PointInfo pInfo)
        {
         
            PointDAO cDAO = new PointDAO();
            int i = 0;
            i = cDAO.DeletePointRange(pInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdatePointRange")]
        public IHttpActionResult UpdatePointRange([FromBody] PointInfo pInfo)
        {
            PointDAO cDAO = new PointDAO();
            int i = 0;
            i = cDAO.UpdatePointRange(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPointRangePagingbyCriteria")]
        public IHttpActionResult ListPointRangePagingbyCriteria([FromBody] PointInfo pInfo)
        {
            List<PointInfo> listPoint = new List<PointInfo>();
            PointDAO pDAO = new PointDAO();

            listPoint = pDAO.ListPointRangePagingbyCriteria(pInfo);

            return Ok<List<PointInfo>>(listPoint);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountPointRangePagingbyCriteria")]
        public IHttpActionResult CountPointRangePagingbyCriteria([FromBody] PointInfo pInfo)
        {
            PointDAO pDAO = new PointDAO();
            int? i = 0;
            i = pDAO.CountPointRangePagingbyCriteria(pInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPointRateNoPagingbyCriteria")]
        public IHttpActionResult ListPointRateNoPagingbyCriteria([FromBody] PointInfo pInfo)
        {
            List<PointInfo> listPoint = new List<PointInfo>();
            PointDAO pDAO = new PointDAO();

            listPoint = pDAO.ListPointRateNoPagingbyCriteria(pInfo);

            return Ok<List<PointInfo>>(listPoint);
        }
         [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListPromotionPoint")]
        public IHttpActionResult CountListPromo([FromBody]PromotionInfo pInfo)
        {
            PointDAO pDAO = new PointDAO();
            int? i = 0;
            i = pDAO.CountListPromotionPointListUsed(pInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPromotionPointList")]
        public IHttpActionResult ListPromoList([FromBody] PromotionInfo pInfo)
        {
            PromotionListReturn promotionList = new PromotionListReturn();
            List<PromotionListReturn> listPromotion = new List<PromotionListReturn>();
            PointDAO pDAO = new PointDAO();

            listPromotion = pDAO.ListPromotionPointListUsed(pInfo);

            return Ok<List<PromotionListReturn>>(listPromotion);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListReportPromotionPoint")]
        public IHttpActionResult ListReportPromotionPoint([FromBody] ReportPointInfo pInfo)
        {
            ReportPointInfo promotionList = new ReportPointInfo();
            List<ReportPointInfo> listPromotion = new List<ReportPointInfo>();
            PointDAO pDAO = new PointDAO();

            listPromotion = pDAO.ListReportPromotionPoint(pInfo);

            return Ok<List<ReportPointInfo>>(listPromotion);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountReportPromotionPoint")]
        public IHttpActionResult CountReportPromotionPoint([FromBody] ReportPointInfo pInfo)
        {
            PointDAO pDAO = new PointDAO();
            int? i = 0;
            i = pDAO.CountReportPromotionPoint(pInfo);

            return Ok<int?>(i);
        }
    }
}
