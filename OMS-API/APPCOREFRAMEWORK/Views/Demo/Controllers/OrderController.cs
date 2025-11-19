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
    public class OrderController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountOrderListByCriteria")]
        public IHttpActionResult CountOdListByCriteria([FromBody]OrderInfo oInfo)
        {

            OrderDAO oDAO = new OrderDAO();
            int? i = 0;
            i = oDAO.CountOrderListByCriteria(oInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderNopagingByCriteria")]
        public IHttpActionResult ListOdNopagingByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListOrderNopagingByCriteria(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListLatestOrderNopagingByCriteria")]
        public IHttpActionResult ListLatestOrderNopagingByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListLatestOrderNopagingByCriteria(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/OrderReportByCriteria")]
        public IHttpActionResult OdReportByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderDetailListReturn orderList = new OrderDetailListReturn();
            List<OrderDetailListReturn> listOrder = new List<OrderDetailListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.OrderReportByCriteria(oInfo);

            return Ok<List<OrderDetailListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InterfaceByCriteria")]
        public IHttpActionResult IntfByCriteria([FromBody]OrderInfo oInfo)
        {
            DataSet orderSet = new DataSet();
            OrderDAO oDAO = new OrderDAO();

            orderSet = oDAO.InterfaceByCriteria(oInfo);

            return Ok<DataSet>(orderSet);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderByCriteria")]
        public IHttpActionResult ListOdByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListOrderByCriteria(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderApprovedByCriteria")]
        public IHttpActionResult ListOrderApprdByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListOrderApprovedByCriteria(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateOrderInfo")]
        public IHttpActionResult UpdateOdInfo([FromBody]OrderInfo oInfo)
        {

            OrderDAO oDAO = new OrderDAO();
            int i = 0;
            i = oDAO.UpdateOrderInfo(oInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/getMaximOrder")]
        public IHttpActionResult getMaximOrder([FromBody]YearMonthModel token)
        {

            OrderDAO oDAO = new OrderDAO();
            int i = 0;
            i = oDAO.getMaxOrder(token.Year, token.Month);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ManageOrder")]
        public IHttpActionResult MngOrder([FromBody]YearMonthModel token)
        {

            OrderDAO oDAO = new OrderDAO();
            int i = 0;
            //i = oDAO.ManageOrder(token.Year, token.Month);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertOrder")]
        public IHttpActionResult InsOrder([FromBody]OrderDetailInfo oInfo)
        {

            OrderDAO oDAO = new OrderDAO();
            int i = 0;
            i = oDAO.InsertOrder(oInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertOrderDetail")]
        public IHttpActionResult InsOrderDetail([FromBody]OrderDetailInfo oInfo)
        {

            OrderDAO oDAO = new OrderDAO();
            int i = 0;
            i = oDAO.InsertOrderDetail(oInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCustomerByCriteria_fromOrder")]
        public IHttpActionResult ListCusByCriteria_fromOrder([FromBody]CustomerInfo cInfo)
        {
            CustomerListReturn customerList = new CustomerListReturn();
            List<CustomerListReturn> listCustomer = new List<CustomerListReturn>();
            OrderDAO cDAO = new OrderDAO();

            listCustomer = cDAO.ListCustomerByCriteria_fromOrder(cInfo);

            return Ok<List<CustomerListReturn>>(listCustomer);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderPaymentNoPagingByCriteria")]
        public IHttpActionResult ListOdPaymentNoPagingByCriteria([FromBody]OrderPaymentInfo oInfo)
        {
            OrderPaymentListReturn orderList = new OrderPaymentListReturn();
            List<OrderPaymentListReturn> listOrder = new List<OrderPaymentListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListOrderPaymentNoPagingByCriteria(oInfo);

            return Ok<List<OrderPaymentListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetDatasetOrderDetailByCriteria")]
        public IHttpActionResult GetDsOrderDetailByCriteria([FromBody]OrderDetailInfo oInfo)
        {
            DataSet orderSet = new DataSet();
            OrderDAO oDAO = new OrderDAO();

            orderSet = oDAO.GetDatasetOrderDetailByCriteria(oInfo);

            return Ok<DataSet>(orderSet);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetDatasetOrderDetailByCriteria1")]
        public IHttpActionResult GetDsOrderDetailByCriteria1([FromBody]OrderDetailInfo oInfo)
        {
            OrderDetailListReturn orderList = new OrderDetailListReturn();
            List<OrderDetailListReturn> listOrder = new List<OrderDetailListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.GetDatasetOrderDetailByCriteria1(oInfo);

            return Ok<List<OrderDetailListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderDetailNoPagingByCriteria")]
        public IHttpActionResult ListOdDetailNoPagingByCriteria([FromBody]OrderDetailInfo oInfo)
        {
            OrderDetailListReturn orderList = new OrderDetailListReturn();
            List<OrderDetailListReturn> listOrder = new List<OrderDetailListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListOrderDetailNoPagingByCriteria(oInfo);

            return Ok<List<OrderDetailListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderTransport")]
        public IHttpActionResult ListOdTransport([FromBody]OrderTransportInfo oInfo)
        {
            OrderTransportListReturn orderList = new OrderTransportListReturn();
            List<OrderTransportListReturn> listOrder = new List<OrderTransportListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListOrderTransport(oInfo);

            return Ok<List<OrderTransportListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DtOl")]
        public IHttpActionResult DTOL([FromBody]ConfirmNo_DateModel info)
        {
            DataTable orderSet = new DataTable();
            OrderDAO oDAO = new OrderDAO();

            //orderSet = oDAO.DtOl(info.ConfirmNo, info.Date);

            return Ok<DataTable>(orderSet);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DtPl")]
        public IHttpActionResult DTPL([FromBody]ConfirmNo_DateModel info)
        {
            DataTable orderSet = new DataTable();
            OrderDAO oDAO = new OrderDAO();

            orderSet = oDAO.DtPl(info.ConfirmNo, info.Date);

            return Ok<DataTable>(orderSet);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountOrderListByCriteriaOrderInfo")]
        public IHttpActionResult CountOdListByCriteriaOrderInfo([FromBody]OrderInfo_StatusPageModel infos)
        {

            OrderDAO oDAO = new OrderDAO();
            int? i = 0;
            i = oDAO.CountOrderListByCriteriaOrderInfo(infos.OdInfo, infos.StatusPage);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountOrderApproveListByCriteria")]
        public IHttpActionResult CountOdListApprByCriteriaOrderInfo([FromBody]OrderInfo oInfo)
        {

            OrderDAO oDAO = new OrderDAO();
            int? i = 0;
            i = oDAO.CountOrderApproveListByCriteria(oInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderByCriteriaOrderInfo")]
        public IHttpActionResult ListOdByCriteriaOrderInfo([FromBody]OrderInfo_StatusPageModel infos)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListOrderByCriteriaOrderInfo(infos.OdInfo, infos.StatusPage);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateOrderInfoOrderlist")]
        public IHttpActionResult UpdOrderInfoOrderlist([FromBody]OrderInfo oInfo)
        {

            OrderDAO oDAO = new OrderDAO();
            int i = 0;
            i = oDAO.UpdateOrderInfoOrderlist(oInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderByCriteriaOrderlistConfirmNo")]
        public IHttpActionResult ListOdByCriteriaOrderlistConfirmNo([FromBody]OrderInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListOrderByCriteriaOrderlistConfirmNo(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetCampaignName")]
        public IHttpActionResult GetCpName([FromBody]OrderInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.GetCampaignName(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ManageOrder")]
        public IHttpActionResult MngOrder([FromBody]ManageOrderModel info)
        {
            string jsonStr;
            //OrderListReturn orderList = new OrderListReturn();
            //List<OrderListReturn> listOrder = new List<OrderListReturn>();
            OrderDAO oDAO = new OrderDAO();
            jsonStr = oDAO.ManageOrder(info.OrderDataList.OrderData, info.PaymentDataList.PaymentData, info.CustomerCode, info.TransportDataList.TransportData, info.EmpCode, info.InventoryDataList.InventoryData, info.InventoryCode);

            return Ok<string>(jsonStr);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/OrderApproveReport")]
        public IHttpActionResult orderapprreport([FromBody]OrderInfo oinfo)
        {
            OrderDetailListReturn orderList = new OrderDetailListReturn();
            List<OrderDetailListReturn> listOrder = new List<OrderDetailListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.OrderApproveReport(oinfo);

            return Ok<List<OrderDetailListReturn>>(listOrder);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/OrderDetailApproveReport")]
        public IHttpActionResult orderdetailapprreport([FromBody]OrderInfo oinfo)
        {
            OrderDetailListReturn orderList = new OrderDetailListReturn();
            List<OrderDetailListReturn> listOrder = new List<OrderDetailListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.OrderDetailApproveReport(oinfo);

            return Ok<List<OrderDetailListReturn>>(listOrder);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/getlogisticbyorder")]
        public IHttpActionResult getlogisticbyorderreport([FromBody]OrderTransportInfo oinfo)
        {
            OrderTransportListReturn orderList = new OrderTransportListReturn();
            List<OrderTransportListReturn> listOrder = new List<OrderTransportListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.getlogisticbyorder(oinfo);

            return Ok<List<OrderTransportListReturn>>(listOrder);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetAddressbyCustomerOrder")]
        public IHttpActionResult GetAddressbyCustomerOrderAPI([FromBody]OrderTransportInfo oinfo)
        {
            OrderTransportListReturn orderList = new OrderTransportListReturn();
            List<OrderTransportListReturn> listOrder = new List<OrderTransportListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.GetAddressbyCustomerOrder(oinfo);

            return Ok<List<OrderTransportListReturn>>(listOrder);
        }


        //โค้ดนี้ยังไม่ได้เขียน API ใหม่จึงมายืมใช้ตรงนี้ก่อน
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountRiderOrderListByCriteria")]
        public IHttpActionResult CountRiderOrderListByCriteria([FromBody]OrderInfo oInfo)
        {

            OrderDAO oDAO = new OrderDAO();
            int? i = 0;
            i = oDAO.CountRiderOrderListByCriteria(oInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRiderOrderByCriteria")]
        public IHttpActionResult ListRiderOrderByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListRiderOrderByCriteria(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRiderNoPagingByCriteria")]
        public IHttpActionResult ListRiderNoPagingByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListRiderNoPagingByCriteria(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMerChantAroundRiderMapNoPagingByCriteria")]
        public IHttpActionResult ListMerchantAroundRiderMapNoPagingByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListMerchantAroundRiderMapNoPagingByCriteria(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListFulfilOrderDetailByCriteria")]
        public IHttpActionResult ListFulfilOrderDetailByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListFulfilOrderDetailByCriteria(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPickingByCriteria")]
        public IHttpActionResult ListPickingByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListPickingByCriteria(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderFulfillByCriteria")]
        public IHttpActionResult ListOrderFulfillByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListOrderFulfillByCriteria(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertOrderMapRider")]
        public IHttpActionResult InsertOrderMapRider([FromBody]OrderInfo oInfo)
        {
            //CampaignListReturn campaignList = new CampaignListReturn();
            //List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            OrderDAO oDAO = new OrderDAO();
            int i = 0;
            i = oDAO.InsertOrderMapRider(oInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCancelOrderByCriteria")]
        public IHttpActionResult ListCancelOrderByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListCancelOrderByCriteria(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/countListCancelOrderByCriteria")]
        public IHttpActionResult countListCancelOrderByCriteria([FromBody]OrderInfo oInfo)
        {

            OrderDAO oDAO = new OrderDAO();
            int? i = 0;
            i = oDAO.countListCancelOrderByCriteria(oInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListAppointmentOrderByCriteria")]
        public IHttpActionResult ListAppointmentOrderByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListAppointmentOrderByCriteria(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/countListAppointmentOrderByCriteria")]
        public IHttpActionResult countListAppointmentOrderByCriteria([FromBody]OrderInfo oInfo)
        {

            OrderDAO oDAO = new OrderDAO();
            int? i = 0;
            i = oDAO.countListAppointmentOrderByCriteria(oInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateOrderApprove")]
        public IHttpActionResult UpdOrdApprove([FromBody]OrderInfo oInfo)
        {
            //CampaignListReturn campaignList = new CampaignListReturn();
            //List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            OrderDAO oDAO = new OrderDAO();
            int i = 0;
            i = oDAO.UpdateOrderApprove(oInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateOrderReject")]
        public IHttpActionResult UpdOrdReject([FromBody]OrderInfo oInfo)
        {
            //CampaignListReturn campaignList = new CampaignListReturn();
            //List<CampaignListReturn> listCampaign = new List<CampaignListReturn>();
            OrderDAO oDAO = new OrderDAO();
            int i = 0;
            i = oDAO.UpdateOrderReject(oInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListStampPeriodByCriteria")]
        public IHttpActionResult ListStampPeriodByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderListReturn orderList = new OrderListReturn();
            List<OrderListReturn> listOrder = new List<OrderListReturn>();
            OrderDAO oDAO = new OrderDAO();

            listOrder = oDAO.ListStampPeriodByCriteria(oInfo);

            return Ok<List<OrderListReturn>>(listOrder);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListStampPeriodByCriteria")]
        public IHttpActionResult CountListStampPeriodByCriteria([FromBody]OrderInfo oInfo)
        {

            OrderDAO oDAO = new OrderDAO();
            int? i = 0;
            i = oDAO.CountListStampPeriodByCriteria(oInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/MaxIndexPeriodDay")]
        public IHttpActionResult MaxIndexPeriodDay([FromBody]OrderInfo oInfo)
        {

            OrderDAO oDAO = new OrderDAO();
            int? i = 0;
            i = oDAO.MaxIndexPeriodDay(oInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateConfirmNo")]
        public IHttpActionResult UpdateConfirmNo([FromBody]OrderInfo oInfo)
        {
            OrderDAO oDAO = new OrderDAO();
            int i = 0;
            i = oDAO.UpdateConfirmNo(oInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountOrderManagementListByCriteria")]
        public IHttpActionResult CountOrderManagementListByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderDAO oDAO = new OrderDAO();
            int? i = 0;
            i = oDAO.CountOrderManagementListByCriteria(oInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListOrderManagementByCriteria_showgv")]
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
        [Route("api/support/ListOrderManagementNoPagingByCriteria")]
        public IHttpActionResult ListOrderManagementNoPagingByCriteria([FromBody]OrderInfo vInfo)
        {
            OrderListReturn vechicleList = new OrderListReturn();
            List<OrderListReturn> listVechicle = new List<OrderListReturn>();
            OrderDAO vDAO = new OrderDAO();

            listVechicle = vDAO.ListOrderManagementNoPagingByCriteria(vInfo);

            return Ok<List<OrderListReturn>>(listVechicle);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/FinishOrderAPI")]
        public IHttpActionResult FinishOrderAPI([FromBody]FinishOrderReturn vInfo)
        {
            FinishOrderReturn finishorderReturn = new FinishOrderReturn();
            List<order_detail> orderdetail = new List<order_detail>();

            order_detail ordetail = new order_detail();
            ordetail.order_code = "100001";
            ordetail.branch_name = "MK";
            ordetail.total_amount = 1200;
            ordetail.branch_name = "เซ็นทรัล เวสเกต";
            ordetail.order_status = "Order Completed";

            orderdetail.Add(ordetail);

            foreach (var orderdetailV in orderdetail)
            {
                finishorderReturn.order_detail.Add(new order_detail() { order_code = orderdetailV.order_code, brand_name = orderdetailV.brand_name, total_amount = orderdetailV.total_amount, branch_name = orderdetailV.branch_name, order_status = orderdetailV.order_status });
            }

            finishorderReturn.unique_id = "gg";
            finishorderReturn.customer_fname = "rr";
            finishorderReturn.customer_lname = "bb";
            finishorderReturn.channel = "rr";

            OrderDAO vDAO = new OrderDAO();

            return Ok<FinishOrderReturn>(finishorderReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/sumAmoutOrderDetailByCriteria")]
        public IHttpActionResult sumAmoutOrderDetailByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderDAO oDAO = new OrderDAO();
            int? i = 0;
            i = oDAO.sumAmoutOrderDetailByCriteria(oInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/sumTotalPriceOrderDetailByCriteria")]
        public IHttpActionResult sumTotalPriceOrderDetailByCriteria([FromBody]OrderInfo oInfo)
        {
            OrderDAO oDAO = new OrderDAO();
            int? i = 0;
            i = oDAO.sumTotalPriceOrderDetailByCriteria(oInfo);

            return Ok<int?>(i);
        }

    }
}
