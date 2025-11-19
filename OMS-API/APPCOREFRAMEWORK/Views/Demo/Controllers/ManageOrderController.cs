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
    public class ManageOrderController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/L_orderdataInsert")]
        public IHttpActionResult L_orderdataInsertAPI([FromBody]L_orderdata lorderdata)
        {
            OrderdataDAO oDAO = new OrderdataDAO();
            int sum = 0;
            string ordercode = "";

            ordercode = oDAO.InsertOrderdata(lorderdata.orderdataInfo.ToList(), lorderdata.EmpCode);
            return Ok<string>(ordercode);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/L_transportdataInsert")]
        public IHttpActionResult L_transportInsertAPI([FromBody]L_transportdata ltransportdata)
        {

            String OrderTracking = "";

            if (ltransportdata.transportdataInfo[0].TransportType == "03" )
            {
                
                OrderTracking += "TSR-" + ltransportdata.transportdataInfo[0].OrderCode;
            }
            int sum = 0;
            int sum1 = 0;

            OrderDAO odDAO = new OrderDAO();

            sum1 = odDAO.UpdateOrderTrackinginOrderInfo(OrderTracking, ltransportdata.transportdataInfo[0].OrderCode);

            if (sum1 == 1)
            {

                OrderdataDAO oDAO = new OrderdataDAO();
                

                sum = oDAO.L_transportdataInsert(ltransportdata.transportdataInfo.ToList());

            }
                return Ok<int>(sum);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/L_paymentdataInsert")]
        public IHttpActionResult L_paymentInsertAPI([FromBody]L_paymentdata lpaymentdata)
        {
            OrderdataDAO oDAO = new OrderdataDAO();
            int sum = 0;

            sum = oDAO.L_paymentdataInsert(lpaymentdata.paymentdataInfo.ToList());

            return Ok<int>(sum);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/L_inventorydataUpdate")]
        public IHttpActionResult L_inventoryUpdateAPI([FromBody]L_inventorydata linventorydata)
        {
            String InventoryCode = "";

            for (int i = 0; i < linventorydata.inventorydataInfo.Count; i++)
            {
                InventoryCode = linventorydata.inventorydataInfo[0].InventoryCode;
            }

            String CodeList = "";
            foreach (var od in linventorydata.inventorydataInfo.ToList())
            if(CodeList == "") {
                    CodeList += "'" + od.ProductCode + "'";
            }else {
                    CodeList += ",'" + od.ProductCode + "'";
            }

            InventoryDetailDAO iDAO = new InventoryDetailDAO();
            InventoryDetailListReturn iinfo = new InventoryDetailListReturn();
            List<InventoryDetailListReturn> linfo = new List<InventoryDetailListReturn>();
            linfo = iDAO.GetInventoryCodeList(InventoryCode, CodeList);

            inventorydataInfo inveninfo = new inventorydataInfo();
            List<inventorydataInfo> linveninfo = new List<inventorydataInfo>();

            foreach (var oc in linfo)
            {
                foreach (var od in linventorydata.inventorydataInfo.ToList())
                {
                    if(od.ProductCode == oc.ProductCode)
                    {
                        oc.ProductCode = od.ProductCode;
                        oc.InventoryCode = od.InventoryCode;
                        oc.Reserved = od.Reserved;
                        oc.Balance = od.Balance;
                        oc.UpdateBy = od.UpdateBy;

                        linveninfo.Add(new inventorydataInfo() { ProductCode = od.ProductCode, InventoryCode = od.InventoryCode });
                    }
                    else
                    {
                        String a = "";
                    }
                    //var itemToRemove = linfo.SingleOrDefault(r => r.ProductCode != od.ProductCode);
                    //od.Remove(itemToRemove);
                    //linveninfo.Add(inveninfo);
                }
            }

            OrderdataDAO oDAO = new OrderdataDAO();
            int sum = 0;

            sum = oDAO.L_inventorydataUpdate(linfo.ToList());

            return Ok<int>(sum);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/L_paymentMKInsert")]
        public IHttpActionResult paymenMKInsert([FromBody]paymentdataInfo pInfo)
        {
            OrderdataDAO oDAO = new OrderdataDAO();
            int sum = 0;

            sum = oDAO.L_paymentMKInsert(pInfo);

            return Ok<int>(sum);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/L_paymentRetailInsert")]
        public IHttpActionResult L_paymentRetailInsert([FromBody]paymentdataInfo pInfo)
        {
            OrderdataDAO oDAO = new OrderdataDAO();
            int sum = 0;

            sum = oDAO.L_paymentRetailInsert(pInfo);

            return Ok<int>(sum);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPaymentCreditByCriteria")]
        public IHttpActionResult ListPaymentCreditByCriteria([FromBody] paymentdataInfo pInfo)
        {
            OrderdataDAO oDAO = new OrderdataDAO();
            List<returnpaymentdataInfo> listpayment = new List<returnpaymentdataInfo>();
            listpayment = oDAO.ListPaymentCreditByCriteria(pInfo);
            return Ok<List<returnpaymentdataInfo>>(listpayment);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertMKOrderdetaildata")]
        public IHttpActionResult InsertMKOrderdetaildataAPI([FromBody]orderdataInfo dInfo)
        {
            OrderdataDAO oDAO = new OrderdataDAO();
            int sum = 0;
            string ordercode = "";

            ordercode = oDAO.InsertMKOrderdetaildata(dInfo);
            return Ok<string>(ordercode);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertRetailOrderdetaildata")]
        public IHttpActionResult InsertRetailOrderdetaildata([FromBody]orderdataInfo dInfo)
        {
            OrderdataDAO oDAO = new OrderdataDAO();
            int sum = 0;
            string ordercode = "";

            ordercode = oDAO.InsertRetailOrderdetaildata(dInfo);
            return Ok<string>(ordercode);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertMKOrderdata")]
        public IHttpActionResult InsertMKOrderdataAPI([FromBody]orderdataInfo dInfo)
        {
            OrderdataDAO oDAO = new OrderdataDAO();
            int sum = 0;
            string ordercode = "";

            ordercode = oDAO.InsertMKOrderdata(dInfo);
            return Ok<string>(ordercode);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertRetailOrderdata")]
        public IHttpActionResult InsertRetailOrderdata([FromBody]orderdataInfo dInfo)
        {
            OrderdataDAO oDAO = new OrderdataDAO();
            int sum = 0;
            string ordercode = "";

            ordercode = oDAO.InsertRetailOrderdata(dInfo);
            return Ok<string>(ordercode);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertLotNo")]
        public IHttpActionResult InsertLotNo([FromBody] orderdataInfo dInfo)
        {
            OrderdataDAO oDAO = new OrderdataDAO();
            int sum = 0;

            sum = oDAO.InsertLotNo(dInfo);
            return Ok<int>(sum);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateRetailOrderdata")]
        public IHttpActionResult UpdateRetailOrderdata([FromBody]orderdataInfo dInfo)
        {
            OrderdataDAO oDAO = new OrderdataDAO();
            int sum = 0;

            sum = oDAO.UpdateRetailOrderdata(dInfo);
            return Ok<int>(sum);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertMKTransportdata")]
        public IHttpActionResult InsertMKTransportAPI([FromBody]transportdataInfo tInfo)
        {
            OrderdataDAO oDAO = new OrderdataDAO();
            int sum = 0;
            string ordercode = "";

            sum = oDAO.InsertMKTransportdata(tInfo);
            return Ok<int>(sum);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertRetailTransportdata")]
        public IHttpActionResult InsertRetailTransportdata([FromBody]transportdataInfo tInfo)
        {
            OrderdataDAO oDAO = new OrderdataDAO();
            int sum = 0;
            string ordercode = "";

            sum = oDAO.InsertRetailTransportdata(tInfo);
            return Ok<int>(sum);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/LoadOrderPaymentRetail")]
        public IHttpActionResult LoadOrderPaymentRetail([FromBody]paymentdataInfo pInfo)
        {
            paymentdataInfo paymentReturnInfo = new paymentdataInfo();
            List<paymentdataInfo> listpaymentReturnInfo = new List<paymentdataInfo>();
            OrderdataDAO oDAO = new OrderdataDAO();

            listpaymentReturnInfo = oDAO.LoadOrderPaymentRetail(pInfo);

            return Ok<List<paymentdataInfo>>(listpaymentReturnInfo);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/LoadOrderTransportRetail")]
        public IHttpActionResult LoadOrderTransportRetail([FromBody]transportdataInfo pInfo)
        {
            transportdataInfo transportReturnInfo = new transportdataInfo();
            List<transportdataInfo> listtransportReturnInfo = new List<transportdataInfo>();
            OrderdataDAO oDAO = new OrderdataDAO();

            listtransportReturnInfo = oDAO.LoadOrderTransportRetail(pInfo);

            return Ok<List<transportdataInfo>>(listtransportReturnInfo);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteOrderDetailofRetailTakeOrder")]
        public IHttpActionResult DeleteOrderDetailofRetailTakeOrder([FromBody]orderdataInfo dInfo)
        {
            OrderdataDAO oDAO = new OrderdataDAO();
            int sum = 0;

            sum = oDAO.DeleteOrderDetailofRetailTakeOrder(dInfo);
            return Ok<int>(sum);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateRetailOrderPayment")]
        public IHttpActionResult UpdateRetailOrderPayment([FromBody]paymentdataInfo pInfo)
        {
            OrderdataDAO oDAO = new OrderdataDAO();
            int sum = 0;

            sum = oDAO.UpdateRetailOrderPayment(pInfo);
            return Ok<int>(sum);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateRetailOrderTransport")]
        public IHttpActionResult UpdateRetailOrderTransport([FromBody]transportdataInfo tInfo)
        {
            OrderdataDAO oDAO = new OrderdataDAO();
            int sum = 0;

            sum = oDAO.UpdateRetailOrderTransport(tInfo);
            return Ok<int>(sum);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/LoadOrderTransprtbyLatestUpdate")]
        public IHttpActionResult channalListNoPaging([FromBody] OrderTransportInfo cInfo)
        {
            OrderTransportListReturn ordertransportReturn = new OrderTransportListReturn();
            List<OrderTransportListReturn> lordertransportReturn = new List<OrderTransportListReturn>();
            OrderdataDAO cDAO = new OrderdataDAO();

            lordertransportReturn = cDAO.LoadOrderTransprtbyLatestUpdate(cInfo);

            return Ok<List<OrderTransportListReturn>>(lordertransportReturn);
        }
    }
}
