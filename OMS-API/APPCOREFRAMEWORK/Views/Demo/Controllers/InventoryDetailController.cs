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
    public class InventoryDetailController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountInventoryDetailListByCriteria")]
        public IHttpActionResult CountInvenDetailListByCriteria([FromBody]InventoryInfo cInfo)
        {
            InventoryDetailDAO cDAO = new InventoryDetailDAO();
            int? i = 0;
            i = cDAO.CountInventoryDetailListByCriteria(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventoryDetailInfoPagingByCriteria")]
        public IHttpActionResult ListInvenDetailInfoPagingByCriteria([FromBody]InventoryInfo cInfo)
        {
            InventoryListReturn inventoryList = new InventoryListReturn();
            List<InventoryListReturn> listInventory = new List<InventoryListReturn>();
            InventoryDetailDAO cDAO = new InventoryDetailDAO();

            listInventory = cDAO.ListInventoryDetailInfoPagingByCriteria(cInfo);

            return Ok<List<InventoryListReturn>>(listInventory);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListInventoryDetailByCriteria")]
        public IHttpActionResult CountListInventoryDetailByCriteria([FromBody]InventoryInfo cInfo)
        {
            InventoryDetailDAO cDAO = new InventoryDetailDAO();
            int? i = 0;
            i = cDAO.CountListInventoryDetailByCriteria(cInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventoryDetailPagingByCriteria")]
        public IHttpActionResult ListInventoryDetailPagingByCriteria([FromBody]InventoryInfo cInfo)
        {
            InventoryListReturn inventoryList = new InventoryListReturn();
            List<InventoryListReturn> listInventory = new List<InventoryListReturn>();
            InventoryDetailDAO cDAO = new InventoryDetailDAO();

            listInventory = cDAO.ListInventoryDetailPagingByCriteria(cInfo);

            return Ok<List<InventoryListReturn>>(listInventory);
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventoryDetailInfoNoPagingByCriteria")]
        public IHttpActionResult ListInvenDetailInfoNoPagingByCriteria([FromBody]InventoryInfo cInfo)
        {
            InventoryListReturn inventoryList = new InventoryListReturn();
            List<InventoryListReturn> listInventory = new List<InventoryListReturn>();
            InventoryDetailDAO cDAO = new InventoryDetailDAO();

            listInventory = cDAO.ListInventoryDetailInfoNoPagingByCriteria(cInfo);

            return Ok<List<InventoryListReturn>>(listInventory);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventoryBalanceNoPagingByCritriaForTakeOrderRetail")]
        public IHttpActionResult ListInventoryBalanceNoPagingByCritriaForTakeOrderRetail([FromBody]InventoryBalanceInfo iInfo)
        {
            InventoryBalanceListReturn inventoryList = new InventoryBalanceListReturn();
            List<InventoryBalanceListReturn> listInventory = new List<InventoryBalanceListReturn>();
            InventoryBalanceDAO iDAO = new InventoryBalanceDAO();

            listInventory = iDAO.ListInventoryBalanceNopagingByCriteria(iInfo);

            return Ok<List<InventoryBalanceListReturn>>(listInventory);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventoryDetailStandardInfoNoPagingByCriteria")]
        public IHttpActionResult ListInvenDetailStandardInfoNoPagingByCriteria([FromBody]InventoryInfo cInfo)
        {
            InventoryListReturn inventoryList = new InventoryListReturn();
            List<InventoryListReturn> listInventory = new List<InventoryListReturn>();
            InventoryDetailDAO cDAO = new InventoryDetailDAO();

            listInventory = cDAO.ListInventoryDetailStandardInfoNoPagingByCriteria(cInfo);

            return Ok<List<InventoryListReturn>>(listInventory);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventoryDetailValidateByCriteria")]
        public IHttpActionResult ListInvenDetailValidateByCriteria([FromBody]InventoryDetailInfo cInfo)
        {
            InventoryDetailListReturn inventoryDetailList = new InventoryDetailListReturn();
            List<InventoryDetailListReturn> listInventoryDetail = new List<InventoryDetailListReturn>();
            InventoryDetailDAO cDAO = new InventoryDetailDAO();

            listInventoryDetail = cDAO.ListInventoryDetailValidateByCriteria(cInfo);

            return Ok<List<InventoryDetailListReturn>>(listInventoryDetail);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventoryDetailGetFromTakeOrderRetail")]
        public IHttpActionResult ListInventoryDetailGetFromTakeOrderRetail([FromBody]InventoryInfo cInfo)
        {
            InventoryListReturn inventoryDetailList = new InventoryListReturn();
            List<InventoryListReturn> listInventoryDetail = new List<InventoryListReturn>();
            InventoryDetailDAO cDAO = new InventoryDetailDAO();

            listInventoryDetail = cDAO.ListInventoryDetailGetFromTakeOrderRetail(cInfo);

            return Ok<List<InventoryListReturn>>(listInventoryDetail);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertInventoryDetail")]
        public IHttpActionResult InsertInvenDetail([FromBody]InventoryDetailInfo cInfo)
        {
            InventoryDetailDAO cDAO = new InventoryDetailDAO();
            int i = 0;
            i = cDAO.InsertInventoryDetail(cInfo);

            InventoryBalanceInfo ibInfo = new InventoryBalanceInfo();
            ibInfo.ProductCode = cInfo.ProductCode;
            ibInfo.InventoryCode = cInfo.InventoryCode;
            ibInfo.QTY = cInfo.QTY;
            

            int j = 0;
            InventoryBalanceDAO iDAO = new InventoryBalanceDAO();
            List<InventoryBalanceListReturn> inventoryBalanceList = new List<InventoryBalanceListReturn>();

            if (i > 0)
            {
                inventoryBalanceList = iDAO.ListInventoryBalanceNopagingByCriteria(ibInfo);

                if(inventoryBalanceList.Count > 0)
                {
                    ibInfo.InventoryBalanceId = inventoryBalanceList[0].InventoryBalanceId;
                    ibInfo.QTY += inventoryBalanceList[0].QTY;
                    ibInfo.Reserved = inventoryBalanceList[0].Reserved;
                    ibInfo.Balance = ((inventoryBalanceList[0].Balance + cInfo.QTY) - ibInfo.Reserved);

                    j = iDAO.UpdateInventoryBalance(ibInfo);

                }
                else
                {
                    ibInfo.Reserved = 0;
                    ibInfo.Balance = ibInfo.QTY - ibInfo.Reserved;
                    ibInfo.FlagActive = "Y";

                    j = iDAO.InsertInventoryBalance(ibInfo);

                }
                
            }

            return Ok<int>(j);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertInventoryDetailfromImport")]
        public IHttpActionResult InsertInvenDetailFromImport([FromBody]INV_DetailListData invlist)
        {
            InventoryDetailDAO cDAO = new InventoryDetailDAO();
            int j = 0;
            j = cDAO.InsertInventoryDetailfromImport(invlist.inventorydetaillistData.ToList());

            InventoryBalanceInfo ibInfo = new InventoryBalanceInfo();
            List<InventoryBalanceInfo> libInfo = new List<InventoryBalanceInfo>();
            List<InventoryBalanceListReturn> inventoryBalanceList = new List<InventoryBalanceListReturn>();
            InventoryBalanceDAO iDAO = new InventoryBalanceDAO();

            foreach(var ivd in invlist.inventorydetaillistData.ToList())
            {
                ibInfo.ProductCode = ivd.ProductCode;
                ibInfo.InventoryCode = ivd.InventoryCode;
                ibInfo.QTY = ivd.QTY;

                libInfo.Add(new InventoryBalanceInfo() { ProductCode = ivd.ProductCode, InventoryCode = ivd.InventoryCode, QTY = ivd.QTY });
            }


            if (j > 0)
            {
                for (int i = 0; i < libInfo.Count; i++)
                {
                    inventoryBalanceList = iDAO.ListInventoryBalanceNopagingByCriteria(libInfo[i]);

                    if (inventoryBalanceList.Count > 0)
                    {
                        int? idInv = inventoryBalanceList[0].InventoryBalanceId;
                        libInfo[i].InventoryBalanceId = idInv;
                        libInfo[i].QTY = libInfo[i].QTY + inventoryBalanceList[0].QTY;
                        libInfo[i].Reserved = inventoryBalanceList[0].Reserved;
                        libInfo[i].Balance = libInfo[i].QTY - libInfo[i].Reserved;

                        j = iDAO.UpdateInventoryBalance(libInfo[i]);

                    }
                    else
                    {
                        libInfo[i].Reserved = 0;
                        libInfo[i].Balance = libInfo[i].QTY - libInfo[i].Reserved;
                        libInfo[i].FlagActive = "Y";

                        j = iDAO.InsertInventoryBalance(libInfo[i]);

                    }
                }
            }

            return Ok<int>(j);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertInventoryMapPO")]
        public IHttpActionResult InsertInventoryMapPOclass([FromBody]InventoryInfo cInfo)
        {
            InventoryDetailDAO cDAO = new InventoryDetailDAO();
            int i = 0;
            i = cDAO.InsertInventoryMapPO(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteInventoryDetail")]
        public IHttpActionResult DeleteInvenDetail([FromBody]InventoryInfo cInfo)
        {
            InventoryDetailDAO cDAO = new InventoryDetailDAO();
            int i = 0;
            i = cDAO.DeleteInventoryDetail(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateInventoryDetail")]
        public IHttpActionResult UpdateInvenDetail([FromBody]InventoryInfo cInfo)
        {
            InventoryDetailDAO cDAO = new InventoryDetailDAO();
            int i = 0;
            i = cDAO.UpdateInventoryDetail(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertInventoryDetailfromTakeOrderRetail")]
        public IHttpActionResult InsertInventoryDetailfromTakeOrderRetail([FromBody]InventoryDetailInfo cInfo)
        {
            InventoryDetailDAO cDAO = new InventoryDetailDAO();
            int i = 0;

            i = cDAO.InsertInventoryDetailfromTakeOrderRetail(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateInventoryDetailfromTakeOrderRetail")]
        public IHttpActionResult UpdateInventoryDetailfromTakeOrderRetail([FromBody]InventoryDetailInfo cInfo)
        {
            InventoryDetailDAO cDAO = new InventoryDetailDAO();
            int i = 0;

            i = cDAO.UpdateInventoryDetailfromTakeOrderRetail(cInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateInventoryDetailfromEcommerce")]
        public IHttpActionResult UpdateInventoryDetailfromEcommerce([FromBody] InventoryInfo cInfo)
        {
            InventoryDetailDAO cDAO = new InventoryDetailDAO();
            int i = 0;

            i = cDAO.UpdateInventoryDetailfromEcommerce(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertInventoryDetailfromUploadInvDetail")]
        public IHttpActionResult InsertInventoryDetailfromUploadInvDetail([FromBody] InventoryDetailInfo cInfo)
        {
            InventoryDetailDAO cDAO = new InventoryDetailDAO();
            int i = 0;

            i = cDAO.InsertInventoryDetailfromUploadInvDetail(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListProductShowAll_InventoryDetail")]
        public IHttpActionResult ListPdShowAll_InventoryDetail([FromBody]InventoryDetailInfo cInfo)
        {
            InventoryDetailListReturn inventoryDetailList = new InventoryDetailListReturn();
            List<InventoryDetailListReturn> listInventoryDetail = new List<InventoryDetailListReturn>();
            InventoryDetailDAO cDAO = new InventoryDetailDAO();

            listInventoryDetail = cDAO.ListProductShowAll_InventoryDetail(cInfo);

            return Ok<List<InventoryDetailListReturn>>(listInventoryDetail);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInvenDetailInfoProductNoPagingByCriteria")] // Controller from Select ProductList in Select Product of Take Order
        public IHttpActionResult ListInvenDetailInfoProdNoPagingByCriteria([FromBody]InventoryDetailInfo cInfo)
        {
            InventoryDetailListReturn inventoryList = new InventoryDetailListReturn();
            List<InventoryDetailListReturn> listInventory = new List<InventoryDetailListReturn>();
            InventoryDetailDAO cDAO = new InventoryDetailDAO();

            listInventory = cDAO.ListInvenDetailInfoProductNoPagingByCriteria(cInfo);

            return Ok<List<InventoryDetailListReturn>>(listInventory);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInvenDetailExactNoPagingByCriteria")] // Controller from Select All Product in InventoryDetail
        public IHttpActionResult ListInvenDetailInfoExactNoPagingByCriteria([FromBody]InventoryDetailInfo cInfo)
        {
            InventoryDetailListReturn inventoryList = new InventoryDetailListReturn();
            List<InventoryDetailListReturn> listInventory = new List<InventoryDetailListReturn>();
            InventoryDetailDAO cDAO = new InventoryDetailDAO();

            listInventory = cDAO.ListInvenDetailExactNoPagingByCriteria(cInfo);

            return Ok<List<InventoryDetailListReturn>>(listInventory);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateInventoryDetailfromUploadFile")]
        public IHttpActionResult UpdateInventoryDetailfromUploadFile([FromBody] InventoryInfo cInfo)
        {
            InventoryDetailDAO cDAO = new InventoryDetailDAO();
            int i = 0;
            i = cDAO.UpdateInventoryDetailfromUploadFile(cInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventoryDetailInfoNoPagingExportExcelByCriteria")]
        public IHttpActionResult ListInventoryDetailInfoNoPagingExportExcelByCriteria([FromBody] InventoryInfo cInfo)
        {
            InventoryListReturn inventoryList = new InventoryListReturn();
            List<InventoryListReturn> listInventory = new List<InventoryListReturn>();
            InventoryDetailDAO cDAO = new InventoryDetailDAO();

            listInventory = cDAO.ListInventoryDetailInfoNoPagingExportExcelByCriteria(cInfo);

            return Ok<List<InventoryListReturn>>(listInventory);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListInventoryDetailInfoNoPagingByCriteriaMoveStock")]
        public IHttpActionResult ListInventoryDetailInfoNoPagingByCriteriaMoveStock([FromBody] InventoryInfo iInfo)
        {
            InventoryListReturn inventoryList = new InventoryListReturn();
            List<InventoryListReturn> listInventory = new List<InventoryListReturn>();
            InventoryDetailDAO iDAO = new InventoryDetailDAO();

            listInventory = iDAO.ListInventoryDetailInfoNoPagingByCriteriaMoveStock(iInfo);

            return Ok<List<InventoryListReturn>>(listInventory);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InventoryMove")]
        public IHttpActionResult UpdateOdInfo([FromBody] L_InventoryMove oInfo)
        {

            InventoryDetailDAO oDAO = new InventoryDetailDAO();
            int i = 0,j=0;
            //add move
            foreach (var invent in oInfo.L_InventoryDetailInfoNew.ToList())
            {
                i = oDAO.UpdateInventoryMove(invent);
            }
            //add move
            foreach (var invent in oInfo.L_InventoryDetailInfoNew.ToList())
            {
                j = oDAO.UpdateInventoryMoveDelete(invent);
            }

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateInventoryDetailforMarketPlace")]
        public IHttpActionResult UpdateInventoryDetailforMarketPlace([FromBody] InventoryDetailInfo iInfo)
        {
            InventoryInfo inventoryInfo = new InventoryInfo();            
            inventoryInfo.ProductCode = iInfo.ProductCode;
            InventoryListReturn inventoryReturn = new InventoryListReturn();
            List<InventoryListReturn> listInventory = new List<InventoryListReturn>();
            InventoryDetailDAO cDAO = new InventoryDetailDAO();

            listInventory = cDAO.ListInventoryDetailInfoNoPagingByCriteria(inventoryInfo);

            string inventoryCode = "";
            string productcode = "";
            int? qty = 0;
            int? reserved = 0;
            int? current = 0;
            int? balance = 0;

            int i = 0;

            InventoryDetailInfo ivdt = new InventoryDetailInfo();
           
            if (listInventory.Count > 0)
            {
                foreach (var od in listInventory)
                {                    
                    inventoryCode = od.InventoryCode;
                    productcode = od.ProductCode;
                    qty = od.QTY;
                    reserved = od.Reserved + iInfo.Reserved;
                    current = od.QTY - reserved;
                    balance = od.QTY - reserved;

                    ivdt = new InventoryDetailInfo();

                    ivdt.InventoryCode = inventoryCode;
                    ivdt.ProductCode = productcode;
                    ivdt.QTY = qty;
                    ivdt.Reserved = reserved;
                    ivdt.Current = current;
                    ivdt.Balance = balance;

                    i = 0;

                    i = cDAO.UpdateInventoryDetailforMarketPlace(ivdt);
                }
            }
                      
            

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateInvenDetailfromOrderChangeStatus")]
        public IHttpActionResult UpdateInvenDetailfromOrderChangeStatus([FromBody] InventoryDetailInfo cInfo)
        {
            InventoryDetailDAO cDAO = new InventoryDetailDAO();
            int i = 0;
            i = cDAO.UpdateInvenDetailfromOrderChangeStatus(cInfo);

            return Ok<int>(i);
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetNearestInventory")]
        public IHttpActionResult GetNearestInventory([FromBody] InventoryInfo cInfo)
        {
            InventoryDetailDAO cDAO = new InventoryDetailDAO();
            List<InventoryEcommerceReturn> listInventory = new List<InventoryEcommerceReturn>();

            int countproduct = 0;
            int countproductfromcheck = 0;

            if (cInfo.ProductCode != null)
            {
                countproduct = cInfo.ProductCode.Split(',').Length;
                cInfo.CountProductCodeInitial = countproduct;
            }

            listInventory = cDAO.GetNearestInventory(cInfo);

            countproductfromcheck = listInventory.Count;

            return Ok<List<InventoryEcommerceReturn>>(listInventory);
        }


    }
}