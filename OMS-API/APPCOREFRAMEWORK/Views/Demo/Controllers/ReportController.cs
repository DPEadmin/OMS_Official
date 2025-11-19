using System.Collections.Generic;
using System.Web.Http;
using APPCOREMODEL.OMSDAO;
using APPCOREMODEL.Datas.OMSDTO;
using System.Data;


namespace APPCOREVIEW.Views.Demo.Controllers
{
    public class ReportController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderReportByCriteria")]
        public IHttpActionResult ListOdReportByCriteria([FromBody]OrderInfo oInfo)
        {
            DataSet orderSet = new DataSet();
            ReportDAO oDAO = new ReportDAO();

            orderSet = oDAO.ListOrderReportByCriteria(oInfo);

            return Ok<DataSet>(orderSet);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListReportOrderByCriteria")]
        public IHttpActionResult ListReportOrderByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            ReportDAO oDAO = new ReportDAO();

            listOrder = oDAO.ListReportOrderByCriteria(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListReportOrderByCriteria")]
        public IHttpActionResult CountListReportOrderByCriteria([FromBody]OrderInfo oInfo)
        {

            ReportDAO oDAO = new ReportDAO();
            int? i = 0;
            i = oDAO.CountListReportOrderByCriteria(oInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListReportTransactionOrderByCriteria")]
        public IHttpActionResult ListReportTransactionOrderByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            ReportDAO oDAO = new ReportDAO();

            listOrder = oDAO.ListReportTransactionOrderByCriteria(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListReportTransactionOrderByCriteria")]
        public IHttpActionResult CountListReportTransactionOrderByCriteria([FromBody]OrderInfo oInfo)
        {

            ReportDAO oDAO = new ReportDAO();
            int? i = 0;
            i = oDAO.CountListReportTransactionOrderByCriteria(oInfo);

            return Ok<int?>(i);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ThemplateAston")]
        public IHttpActionResult ThemplateAston([FromBody]OrderOLInfo infos)
        {

            ReportDAO oDAO = new ReportDAO();
            DataTable dt = new DataTable();
            List<ReportSaleAstonInfo> listOrder = new List<ReportSaleAstonInfo>();
            listOrder = oDAO.ReaportSaleAston(infos);
            return Ok<List<ReportSaleAstonInfo>>(listOrder);

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/Saleorder")]
        public IHttpActionResult Saleorder([FromBody]OrderOLInfo infos)
        {

            ReportDAO oDAO = new ReportDAO();
            DataTable dt = new DataTable();
            List<ReportSaleOrderInfo> listOrder = new List<ReportSaleOrderInfo>();
            listOrder = oDAO.ReaportSaleOrder(infos);
            return Ok<List<ReportSaleOrderInfo>>(listOrder);

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountSaleorder")]
        public IHttpActionResult CountSaleorder([FromBody]OrderOLInfo oInfo)
        {

            ReportDAO oDAO = new ReportDAO();
            int? i = 0;
            i = oDAO.CountListReaportSaleOrderByCriteria(oInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListSaleorderByCriteria")]
        public IHttpActionResult CountListSaleorderByCriteria([FromBody]OrderOLInfo oInfo)
        {

            ReportDAO oDAO = new ReportDAO();
            int? i = 0;
            i = oDAO.CountListReaportSaleOrderByCriteria(oInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/NewCountListSaleorderByCriteria")]
        public IHttpActionResult NewCountListSaleorderByCriteria([FromBody] MonthlyInfo oInfo)
        {

            ReportDAO oDAO = new ReportDAO();
            int? i = 0;
            i = oDAO.NewCountReaportSaleOrderByCriteria(oInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/NewSaleorder")]
        public IHttpActionResult NewSaleorder([FromBody] MonthlyInfo cInfo)
        {
            ReportDAO oDAO = new ReportDAO();
            List<ReportSaleOrderAmountInfo> listOrder = new List<ReportSaleOrderAmountInfo>();
            listOrder = oDAO.NewSaleorder(cInfo);

            return Ok<List<ReportSaleOrderAmountInfo>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ReaportResultSOSA")]
        public IHttpActionResult ReaportResultSOSA([FromBody]OrderOLInfo infos)
        {

            ReportDAO oDAO = new ReportDAO();
            DataTable dt = new DataTable();
            List<ReportResultSOSAInfo> listOrder = new List<ReportResultSOSAInfo>();
            listOrder = oDAO.ReaportResultSOSA(infos);
            return Ok<List<ReportResultSOSAInfo>>(listOrder);

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountReaportResultSOSA")]
        public IHttpActionResult CountReaportResultSOSA([FromBody]OrderOLInfo infos)
        {

            ReportDAO oDAO = new ReportDAO();
            int? i = 0;
            i = oDAO.CountReportResultSOSA(infos);

            return Ok<int?>(i);

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ReaportCampaignPromotionByProduct")]
        public IHttpActionResult ReaportCampaignPromotionByProduct([FromBody]OrderOLInfo infos)
        {

            ReportDAO oDAO = new ReportDAO();
            DataTable dt = new DataTable();
            List<ReaportCampaignPromotionByProductInfo> listOrder = new List<ReaportCampaignPromotionByProductInfo>();
            listOrder = oDAO.ReaportCampaignPromotionByproduct(infos);
            return Ok<List<ReaportCampaignPromotionByProductInfo>>(listOrder);

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountReaportCampaignPromotionByProduct")]
        public IHttpActionResult CountReaportCampaignPromotionByProduct([FromBody]OrderOLInfo oInfo)
        {

            ReportDAO oDAO = new ReportDAO();
            int? i = 0;
            i = oDAO.CountReaportCampaignPromotionByproduct(oInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ReaportMediaDailySale_Excel")]
        public IHttpActionResult ReaportMediaDailySale_Excel([FromBody] OrderOLInfo infos)
        {

            ReportDAO oDAO = new ReportDAO();
            DataTable dt = new DataTable();
            List<ReaportMediaDailySaleInfo> listOrder = new List<ReaportMediaDailySaleInfo>();
            listOrder = oDAO.ReaportMediaDailySale_Excel(infos);
            return Ok<List<ReaportMediaDailySaleInfo>>(listOrder);

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ReaportMediaDailySale")]
        public IHttpActionResult ReaportMediaDailySale([FromBody]OrderOLInfo infos)
        {

            ReportDAO oDAO = new ReportDAO();
            DataTable dt = new DataTable();
            List<ReaportMediaDailySaleInfo> listOrder = new List<ReaportMediaDailySaleInfo>();
            listOrder = oDAO.ReaportMediaDailySale(infos);
            return Ok<List<ReaportMediaDailySaleInfo>>(listOrder);

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountReaportMediaDailySale")]
        public IHttpActionResult CountReaportMediaDailySale([FromBody]OrderOLInfo oInfo)
        {

            ReportDAO oDAO = new ReportDAO();
            int? i = 0;
            i = oDAO.CountReaportMediaDailySale(oInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ReaportPerformanceSOSA")]
        public IHttpActionResult ReaportPerformanceSOSA([FromBody]OrderOLInfo infos)
        {

            ReportDAO oDAO = new ReportDAO();
            DataTable dt = new DataTable();
            List<ReaportPerformanceSOSAInfo> listOrder = new List<ReaportPerformanceSOSAInfo>();
            listOrder = oDAO.ReaportPerformanceSOSA(infos);
            return Ok<List<ReaportPerformanceSOSAInfo>>(listOrder);

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CounReaportPerformanceSOSA")]
        public IHttpActionResult CountReaportPerformanceSOSA([FromBody]OrderOLInfo oInfo)
        {

            ReportDAO oDAO = new ReportDAO();
            int? i = 0;
            i = oDAO.CountReaportPerformanceSOSA(oInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ReportResultCallin")]
        public IHttpActionResult ReportResultCallin([FromBody]OrderOLInfo infos)
        {

            ReportDAO oDAO = new ReportDAO();
            DataTable dt = new DataTable();
            List<ReportResultCallinInfo> listOrder = new List<ReportResultCallinInfo>();
            listOrder = oDAO.ReportResultCallin(infos);
            return Ok<List<ReportResultCallinInfo>>(listOrder);

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountReportResultCallin")]
        public IHttpActionResult CountReportResultCallin([FromBody]OrderOLInfo oInfo)
        {

            ReportDAO oDAO = new ReportDAO();
            int? i = 0;
            i = oDAO.CountReportResultCallin(oInfo);

            return Ok<int?>(i);
        }
    }
}
