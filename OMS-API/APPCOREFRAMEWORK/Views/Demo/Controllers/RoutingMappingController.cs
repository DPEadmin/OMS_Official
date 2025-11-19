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
    public class RoutingMappingController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRoutingVehicleByCriteriaNoPaging")]
        public IHttpActionResult ListRtvNPByCriteria_showgv([FromBody]RoutingMapVehicleInfo dInfo)
        {

            List<RoutingMapVehicleListReturn> listRouting = new List<RoutingMapVehicleListReturn>();
            RoutingMappingDAO dDAO = new RoutingMappingDAO();

            listRouting = dDAO.ListRoutingVechicleNoPagingByCriteria(dInfo);

            return Ok<List<RoutingMapVehicleListReturn>>(listRouting);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRoutingDriverByCriteriaNoPaging")]
        public IHttpActionResult ListRtvcNPByCriteria_showgv([FromBody]RoutingMapDriverInfo dInfo)
        {

            List<RoutingMapDriverListReturn> listRouting = new List<RoutingMapDriverListReturn>();
            RoutingMappingDAO dDAO = new RoutingMappingDAO();

            listRouting = dDAO.ListRoutingDriverByCriteriaNoPaging(dInfo);

            return Ok<List<RoutingMapDriverListReturn>>(listRouting);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRoutingVehicleByCriteria_showgv")]
        public IHttpActionResult ListRtvByCriteria_showgv([FromBody]RoutingMapVehicleInfo dInfo)
        {
       
            List<RoutingMapVehicleListReturn> listRouting = new List<RoutingMapVehicleListReturn>();
            RoutingMappingDAO dDAO = new RoutingMappingDAO();

            listRouting = dDAO.ListRoutingVechicleByCriteria_showgv(dInfo);

            return Ok<List<RoutingMapVehicleListReturn>>(listRouting);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountRoutingVehicleListByCriteria")]
        public IHttpActionResult CountRtvListByCriteria([FromBody]RoutingMapVehicleInfo dInfo)
        {
            RoutingMappingDAO dDAO = new RoutingMappingDAO();
            int? i = 0;
            i = dDAO.CountRoutingVechicleListByCriteria(dInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertRoutingVehicle")]
        public IHttpActionResult InsRoutingVehicle([FromBody]RoutingMapVehicleInfo dInfo)
        {

            RoutingMappingDAO dDAO = new RoutingMappingDAO();
            int i = 0;
            i = dDAO.InsertRoutingVechicle(dInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateRoutingVehicle")]
        public IHttpActionResult UpdRoutingVehicle([FromBody]RoutingMapVehicleInfo dInfo)
        {

            RoutingMappingDAO dDAO = new RoutingMappingDAO();
            int? i = 0;
            i = dDAO.UpdateRoutingVechicle(dInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteRoutingVehicle")]
        public IHttpActionResult DelRoutingVehicle([FromBody]RoutingMapVehicleInfo dInfo)
        {

            RoutingMappingDAO dDAO = new RoutingMappingDAO();
            int i = 0;
            i = dDAO.DeleteRoutingVechicle(dInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/RoutingVehicleCheck")]
        public IHttpActionResult RoutingVehicleChk([FromBody]RoutingMapVehicleInfo dInfo)
        {

            List<RoutingMapVehicleListReturn> listRouting = new List<RoutingMapVehicleListReturn>();
            RoutingMappingDAO dDAO = new RoutingMappingDAO();

            listRouting = dDAO.RoutingVechicleCheck(dInfo);

            return Ok<List<RoutingMapVehicleListReturn>>(listRouting);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRoutingVehicleByCriteria_dll")]
        public IHttpActionResult ListDvByCriteria_dll([FromBody]DriverInfo dInfo)
        {
            DriverListReturn driverList = new DriverListReturn();
            List<DriverListReturn> listDriver = new List<DriverListReturn>();
            DriverDAO dDAO = new DriverDAO();

            listDriver = dDAO.ListDriverByCriteria_dll(dInfo);

            return Ok<List<DriverListReturn>>(listDriver);
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRoutingDriverByCriteria_showgv")]
        public IHttpActionResult ListRtddByCriteria_showgv([FromBody]RoutingMapDriverInfo dInfo)
        {
  
            List<RoutingMapDriverListReturn> listRouting = new List<RoutingMapDriverListReturn>();
            RoutingMappingDAO dDAO = new RoutingMappingDAO();

            listRouting = dDAO.ListRoutingDriverByCriteria_showgv(dInfo);

            return Ok<List<RoutingMapDriverListReturn>>(listRouting);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountRoutingDriverListByCriteria")]
        public IHttpActionResult CountRtddListByCriteria([FromBody]RoutingMapDriverInfo dInfo)
        {
            RoutingMappingDAO dDAO = new RoutingMappingDAO();
            int? i = 0;
            i = dDAO.CountRoutingDriverListByCriteria(dInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertRoutingDriver")]
        public IHttpActionResult InsRoutingDriver([FromBody]RoutingMapDriverInfo dInfo)
        {

            RoutingMappingDAO dDAO = new RoutingMappingDAO();
            int i = 0;
            i = dDAO.InsertRoutingDriver(dInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateRoutingDriver")]
        public IHttpActionResult UpdRoutingDriver([FromBody]RoutingMapDriverInfo dInfo)
        {

            RoutingMappingDAO dDAO = new RoutingMappingDAO();
            int? i = 0;
            i = dDAO.UpdateRoutingDriver(dInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteRoutingDriver")]
        public IHttpActionResult DelDriverDetail([FromBody]RoutingMapDriverInfo dInfo)
        {

            RoutingMappingDAO dDAO = new RoutingMappingDAO();
            int i = 0;
            i = dDAO.DeleteRoutingDriver(dInfo);

            return Ok<int>(i);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/RoutingDriverCheck")]
        public IHttpActionResult RoutingDetailChk([FromBody]RoutingDetailInfo dInfo)
        {
            RoutingDetailListReturn RoutingList = new RoutingDetailListReturn();
            List<RoutingDetailListReturn> listRouting = new List<RoutingDetailListReturn>();
            RoutingDAO dDAO = new RoutingDAO();

            listRouting = dDAO.RoutingDetailCheck(dInfo);

            return Ok<List<RoutingDetailListReturn>>(listRouting);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRoutingDriverByCriteria_dll")]
        public IHttpActionResult ListDvgvdByCriteria_dll([FromBody]DriverInfo dInfo)
        {
            DriverListReturn driverList = new DriverListReturn();
            List<DriverListReturn> listDriver = new List<DriverListReturn>();
            DriverDAO dDAO = new DriverDAO();

            listDriver = dDAO.ListDriverByCriteria_dll(dInfo);

            return Ok<List<DriverListReturn>>(listDriver);
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRoutingDriverInsertByCriteria_showgv")]
        public IHttpActionResult ListDvgvdvvByCriteria_dll([FromBody]DriverInfo dInfo)
        {
            DriverListReturn driverList = new DriverListReturn();
            List<DriverListReturn> listDriver = new List<DriverListReturn>();
            RoutingMappingDAO dDAO = new RoutingMappingDAO();

            listDriver = dDAO.ListRoutingDriverInsertByCriteria_showgv(dInfo);

            return Ok<List<DriverListReturn>>(listDriver);
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRoutingVechicleInsertByCriteria_showgv")]
        public IHttpActionResult ListRtvxxByCriteria_showgv([FromBody]VechicleInfo dInfo)
        {

            List<VechicleListReturn> listRouting = new List<VechicleListReturn>();
            RoutingMappingDAO dDAO = new RoutingMappingDAO();

            listRouting = dDAO.ListRoutingVechicleInsertByCriteria_showgv(dInfo);

            return Ok<List<VechicleListReturn>>(listRouting);
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountDriverInsertByCriteria")]
        public IHttpActionResult CountRtvInsByCriteria([FromBody]DriverInfo dInfo)
        {
            RoutingMappingDAO dDAO = new RoutingMappingDAO();
            int? i = 0;
            i = dDAO.CountRoutingDriverInsertByCriteria(dInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountVehicleInsertByCriteria")]
        public IHttpActionResult CountRtvvInsByCriteria([FromBody]VechicleInfo dInfo)
        {
            RoutingMappingDAO dDAO = new RoutingMappingDAO();
            int? i = 0;
            i = dDAO.CountRoutingVehicleInsertByCriteria(dInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertRoutingInventory")]
        public IHttpActionResult InsRoutingInventory([FromBody]RoutingMapInventoryInfo dInfo)
        {

            RoutingMappingDAO dDAO = new RoutingMappingDAO();
            int i = 0;
            i = dDAO.InsertRoutingInventory(dInfo);

            return Ok<int>(i);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRoutingInventoryByCriteria_showgv")]
        public IHttpActionResult ListRoutingInventoryByCriteria([FromBody]RoutingMapInventoryDetailInfo dInfo)
        {
            RoutingMapInventoryDetailInfo driverList = new RoutingMapInventoryDetailInfo();
            List<RoutingMapInventoryDetailInfo> listDriver = new List<RoutingMapInventoryDetailInfo>();
            RoutingMappingDAO dDAO = new RoutingMappingDAO();

            listDriver = dDAO.ListRoutingInventoryByCriteria_showgv(dInfo);

            return Ok<List<RoutingMapInventoryDetailInfo>>(listDriver);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListRoutingInventoryNoPagingByCriteria")]
        public IHttpActionResult ListRoutingInventoryNoPagingByCriteria([FromBody]RoutingMapInventoryDetailInfo dInfo)
        {
            RoutingMapInventoryDetailInfo driverList = new RoutingMapInventoryDetailInfo();
            List<RoutingMapInventoryDetailInfo> listDriver = new List<RoutingMapInventoryDetailInfo>();
            RoutingMappingDAO dDAO = new RoutingMappingDAO();

            listDriver = dDAO.ListRoutingInventoryNoPagingByCriteria(dInfo);

            return Ok<List<RoutingMapInventoryDetailInfo>>(listDriver);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteRoutingInventory")]
        public IHttpActionResult DeleteRoutingInventory([FromBody]RoutingMapInventoryDetailInfo dInfo)
        {

            RoutingMappingDAO dDAO = new RoutingMappingDAO();
            int i = 0;
            i = dDAO.DeleteRoutingInventory(dInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountRoutingInventorytByCriteria")]
        public IHttpActionResult CountRoutinginventoryByCriteria([FromBody]RoutingMapInventoryDetailInfo dInfo)
        {
            RoutingMappingDAO dDAO = new RoutingMappingDAO();
            int? i = 0;
            i = dDAO.CountRoutinginventoryByCriteria(dInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/updateRoutingInventory")]
        public IHttpActionResult updateRoutingInventory([FromBody]RoutingMapInventoryInfo dInfo)
        {

            RoutingMappingDAO dDAO = new RoutingMappingDAO();
            int i = 0;
            i = dDAO.updateRoutingInventory(dInfo);

            return Ok<int>(i);
        }
    }
}
