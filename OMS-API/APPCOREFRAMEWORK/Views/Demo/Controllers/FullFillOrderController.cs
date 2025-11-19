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
using System.Data;
namespace APPCOREVIEW.Views.Demo.Controllers
{
    public class FullFillOrderController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateOrderInfoUpdateStatus")]
        public IHttpActionResult UpdateOdInfoUpdateStatus([FromBody]OrderInfo oInfo)
        {
            FullFillOrderDAO ffoDAO = new FullFillOrderDAO();
            int i = 0;
            i = ffoDAO.UpdateOrderInfoUpdateStatus(oInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderByCriteriaOrderlist_credit")]
        public IHttpActionResult ListOdByCriteriaOrderlist_credit([FromBody]OrderInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            FullFillOrderDAO oDAO = new FullFillOrderDAO();

            listOrder = oDAO.ListOrderByCriteriaOrderlist_credit(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderByCriteriaOrderActivity")]
        public IHttpActionResult ListOdByCriteriaOdActivity([FromBody]OrderInfoActivity oInfo)
        {
            OrderInfoActivityListReturn orderList = new OrderInfoActivityListReturn();
            List<OrderInfoActivityListReturn> listOrder = new List<OrderInfoActivityListReturn>();
            FullFillOrderDAO oDAO = new FullFillOrderDAO();

            listOrder = oDAO.ListOrderByCriteriaOrderActivity(oInfo);

            return Ok<List<OrderInfoActivityListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderByCriteriaOrderlist_ReceiptReturn1")]
        public IHttpActionResult ListOlByCriteriaOl_ReceiptReturn1([FromBody]ReceiptReturnOrderInfo cInfo)
        {
            ReceiptReturnOrderListReturn customerOrderList = new ReceiptReturnOrderListReturn();
            List<ReceiptReturnOrderListReturn> listCustomerOrder = new List<ReceiptReturnOrderListReturn>();
            FullFillOrderDAO cDAO = new FullFillOrderDAO();

            listCustomerOrder = cDAO.ListOrderByCriteriaOrderlist_ReceiptReturn1(cInfo);

            return Ok<List<ReceiptReturnOrderListReturn>>(listCustomerOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderByCriteriaBookOrder")]
        public IHttpActionResult ListOdByCriteriaBookOrder([FromBody]OrderInfo_StatusPageModel infos)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            FullFillOrderDAO oDAO = new FullFillOrderDAO();

            listOrder = oDAO.ListOrderByCriteriaBookOrder(infos.OdInfo, infos.StatusPage);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountOrderListByCriteriaBookOrderCar")]
        public IHttpActionResult CountOdListByCriteriaBookOrderCar([FromBody]OrderInfo_StatusPageModel infos)
        {

            FullFillOrderDAO oDAO = new FullFillOrderDAO();
            int? i = 0;
            i = oDAO.CountOrderListByCriteriaBookOrderCar(infos.OdInfo, infos.StatusPage);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderByCriteriaOLPL")]
        public IHttpActionResult ListOdByCriteriaOLPL([FromBody]OrderInfo infos)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            FullFillOrderDAO oDAO = new FullFillOrderDAO();


            listOrder = oDAO.ListOrderByCriteriaOLPL(infos);


            return Ok<List<OrderListReturn>>(listOrder);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderByCriteriaORderDoOLPL")]
        public IHttpActionResult CountOrderListByCriteriaOrderInfo([FromBody]OrderInfo infos)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListOrderByCriteriaOrderInfo(infos, infos.ShipmentStatus);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountOrderListByCriteriaOrderInfo2")]
        public IHttpActionResult CountOrderListByCriteriaOrderInfo2([FromBody]OrderInfo infos)
        {

            OrderDAO oDAO = new OrderDAO();
            int? i = 0;
            i = oDAO.CountOrderListByCriteriaOrderInfo(infos, infos.ShipmentStatus);

            return Ok<int?>(i);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/OList")]
        public IHttpActionResult OList([FromBody]OrderInfo infos)
        {

            OrderDAO oDAO = new OrderDAO();
            DataTable dt = new DataTable();
            List<OrderOLInfo> listOrder = new List<OrderOLInfo>();
            listOrder = oDAO.DtOl(infos);
            return Ok<List<OrderOLInfo>>(listOrder);

        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/barchart")]
        public IHttpActionResult barchart([FromBody]OrderInfo infos)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            FullFillOrderDAO oDAO = new FullFillOrderDAO();


            listOrder = oDAO.ListOrderBarChartByCriteria(infos);


            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/PieChart")]
        public IHttpActionResult PieChart([FromBody]OrderPieChartInfo infos)
        {

            FullFillOrderDAO oDAO = new FullFillOrderDAO();
            List<OrderPieChartInfo> listOrder = new List<OrderPieChartInfo>();
            listOrder = oDAO.ListOrderByCriteriaPiechart(infos);
            return Ok<List<OrderPieChartInfo>>(listOrder);

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/OListToExcel")]
        public IHttpActionResult OListToExcel([FromBody]OrderInfo infos)
        {

            OrderDAO oDAO = new OrderDAO();
            DataTable dt = new DataTable();
            List<OrderOLInfo> listOrder = new List<OrderOLInfo>();
            listOrder = oDAO.DtOlExcel(infos);
            return Ok<List<OrderOLInfo>>(listOrder);

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountOrderFullfillManagementListByCriteria")]
        public IHttpActionResult CountOrderManagementListByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderDAO oDAO = new OrderDAO();
            int? i = 0;
            i = oDAO.CountOrderManagementListByCriteria(oInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderFullfillManagementByCriteria_showgv")]
        public IHttpActionResult ListVhByCriteria_showgv([FromBody]OrderInfo vInfo)
        {
            OrderListReturn vechicleList = new OrderListReturn();
            List<OrderListReturn> listVechicle = new List<OrderListReturn>();
            OrderDAO vDAO = new OrderDAO();

            listVechicle = vDAO.ListOrderManagementByCriteria_showgv(vInfo);

            return Ok<List<OrderListReturn>>(listVechicle);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DtOlExcelReport")]
        public IHttpActionResult DtOlExcelReport([FromBody]OrderOLInfo infos)
        {

            OrderDAO oDAO = new OrderDAO();
            DataTable dt = new DataTable();
            List<OrderOLInfo> listOrder = new List<OrderOLInfo>();
            listOrder = oDAO.DtOlExcelReport(infos);
            return Ok<List<OrderOLInfo>>(listOrder);

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/LotLazadaReviewSearch")]
        public IHttpActionResult LotLazadaReviewSearch([FromBody] OrderOLInfo infos)
        {

            OrderDAO oDAO = new OrderDAO();
            DataTable dt = new DataTable();
            List<OrderOLInfo> listOrder = new List<OrderOLInfo>();
            listOrder = oDAO.LotLazadaReviewSearch(infos);
            return Ok<List<OrderOLInfo>>(listOrder);

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountLotLazadaReviewSearch")]
        public IHttpActionResult CountLotLazadaReviewSearch([FromBody] OrderInfo oInfo)
        {

            OrderDAO oDAO = new OrderDAO();
            int? i = 0;
            i = oDAO.CountLotLazadaReviewSearch(oInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ReportPaymenttype")]
        public IHttpActionResult ReportPaymenttype([FromBody] OrderPaymenttypeInfo infos)
        {

            OrderDAO oDAO = new OrderDAO();
            DataTable dt = new DataTable();
            List<OrderPaymenttypeInfo> listOrder = new List<OrderPaymenttypeInfo>();
            listOrder = oDAO.ReportPaymenttype(infos);
            return Ok<List<OrderPaymenttypeInfo>>(listOrder);

        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/LogfileExcelReport")]
        public IHttpActionResult LogfileExcelReport([FromBody] OrderOLInfo infos)
        {

            OrderDAO oDAO = new OrderDAO();
            DataTable dt = new DataTable();
            List<OrderOLInfo> listOrder = new List<OrderOLInfo>();
            listOrder = oDAO.LogfileExcelReport(infos);
            return Ok<List<OrderOLInfo>>(listOrder);

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DtOlExcelReportSearch")]
        public IHttpActionResult DtOlExcelReportSearch([FromBody]OrderOLInfo infos)
        {

            OrderDAO oDAO = new OrderDAO();
            DataTable dt = new DataTable();
            List<OrderOLInfo> listOrder = new List<OrderOLInfo>();
            listOrder = oDAO.DtOlExcelReportSearch(infos);
            return Ok<List<OrderOLInfo>>(listOrder);

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DtOlExcelReportSearchCount")]
        public IHttpActionResult DtOlExcelReportSearchCount([FromBody]OrderOLInfo infos)
        {
            int? i = 0;
            OrderDAO oDAO = new OrderDAO();
            //DataTable dt = new DataTable();
            //List<OrderOLInfo> listOrder = new List<OrderOLInfo>();
            //listOrder = oDAO.DtOlExcelReportSearch(infos);
            //return Ok<List<OrderOLInfo>>(listOrder);
           
            i = oDAO.DtOlExcelReportSearchCount(infos);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ReportPaymenttypeCount")]
        public IHttpActionResult ReportPaymenttypeCount([FromBody] OrderPaymenttypeInfo infos)
        {
            int? i = 0;
            OrderDAO oDAO = new OrderDAO();
            //DataTable dt = new DataTable();
            //List<OrderOLInfo> listOrder = new List<OrderOLInfo>();
            //listOrder = oDAO.DtOlExcelReportSearch(infos);
            //return Ok<List<OrderOLInfo>>(listOrder);

            i = oDAO.ReportPaymenttypeCount(infos);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ReportPaymenttypeSearch")]
        public IHttpActionResult ReportPaymenttypeSearch([FromBody] OrderPaymenttypeInfo infos)
        {

            OrderDAO oDAO = new OrderDAO();
            DataTable dt = new DataTable();
            List<OrderPaymenttypeInfo> listOrder = new List<OrderPaymenttypeInfo>();
            listOrder = oDAO.ReportPaymenttypeSearch(infos);
            return Ok<List<OrderPaymenttypeInfo>>(listOrder);

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ReportOrderHistoryCount")]
        public IHttpActionResult ReportOrderHistoryCount([FromBody] OrderHistory infos)
        {
            int? i = 0;
            OrderDAO oDAO = new OrderDAO();
            //DataTable dt = new DataTable();
            //List<OrderOLInfo> listOrder = new List<OrderOLInfo>();
            //listOrder = oDAO.DtOlExcelReportSearch(infos);
            //return Ok<List<OrderOLInfo>>(listOrder);

            i = oDAO.ReportOrderHistoryCount(infos);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ReportOrderHistorySearch")]
        public IHttpActionResult ReportOrderHistorySearch([FromBody] OrderHistory infos)
        {

            OrderDAO oDAO = new OrderDAO();
            DataTable dt = new DataTable();
            List<OrderHistory> listOrder = new List<OrderHistory>();
            listOrder = oDAO.ReportOrderHistorySearch(infos);
            return Ok<List<OrderHistory>>(listOrder);

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ReportOrderHistory")]
        public IHttpActionResult ReportOrderHistory([FromBody] OrderHistory infos)
        {

            OrderDAO oDAO = new OrderDAO();
            DataTable dt = new DataTable();
            List<OrderHistory> listOrder = new List<OrderHistory>();
            listOrder = oDAO.ReportOrderHistory(infos);
            return Ok<List<OrderHistory>>(listOrder);

        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/OrderChangeInventory")]
        //public IHttpActionResult UpdateOrderChangeInvent([FromBody]L_OrderChangestatus IInfo)
            public IHttpActionResult UpdateOrderChangeInvent([FromBody]L_OrderChangeInventory IInfo)
        {

            OrderChangestatusDAO oDAO = new OrderChangestatusDAO();
            int i = 0;
            foreach (var ordercode in IInfo.L_OrderChangeInventoryInfo.ToList())
            {
                i = oDAO.UpdateOrderinventoryInfo(ordercode);
                i = oDAO.UpdateOrderDetailinventoryInfo(ordercode);
                i = oDAO.UpdateOrderTransportinventoryInfo(ordercode);

            }



            return Ok<int>(i);


        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InvoiceOrder")]
        public IHttpActionResult InvoiceOrder([FromBody] InvoiceOrderInfo infos)
        {

            OrderDAO oDAO = new OrderDAO();
            DataTable dt = new DataTable();
            List<InvoiceOrderInfo> listOrder = new List<InvoiceOrderInfo>();
            listOrder = oDAO.InvoiceOrder(infos);
            return Ok<List<InvoiceOrderInfo>>(listOrder);

        }
    }
}
