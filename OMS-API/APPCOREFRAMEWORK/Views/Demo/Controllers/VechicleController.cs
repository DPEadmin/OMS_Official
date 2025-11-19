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
    public class VechicleController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListVechicleByCriteria")]
        public IHttpActionResult ListVhByCriteria([FromBody]VechicleInfo vInfo)
        {
            VechicleListReturn vechicleList = new VechicleListReturn();
            List<VechicleListReturn> listVechicle = new List<VechicleListReturn>();
            VechicleDAO vDAO = new VechicleDAO();

            listVechicle = vDAO.ListVechicleByCriteria(vInfo);

            return Ok<List<VechicleListReturn>>(listVechicle);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListVechicleDetailByCriteria")]
        public IHttpActionResult ListVhDetailByCriteria([FromBody]CustomerPhoneInfo vInfo)
        {
            VechicleListReturn vechicleList = new VechicleListReturn();
            List<VechicleListReturn> listVehicle = new List<VechicleListReturn>();
            VechicleDAO vDAO = new VechicleDAO();

            listVehicle = vDAO.ListVechicleDetailByCriteria(vInfo);

            return Ok<List<VechicleListReturn>>(listVehicle);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListVechicleByCriteria_showgv")]
        public IHttpActionResult ListVhByCriteria_showgv([FromBody]VechicleInfo vInfo)
        {
            VechicleListReturn vechicleList = new VechicleListReturn();
            List<VechicleListReturn> listVechicle = new List<VechicleListReturn>();
            VechicleDAO vDAO = new VechicleDAO();

            listVechicle = vDAO.ListVechicleByCriteria_showgv(vInfo);

            return Ok<List<VechicleListReturn>>(listVechicle);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListVechicleByCriteriaNoPaging")]
        public IHttpActionResult ListVhByCriteria_Nopaging([FromBody]VechicleInfo vInfo)
        {
            VechicleListReturn vechicleList = new VechicleListReturn();
            List<VechicleListReturn> listVechicle = new List<VechicleListReturn>();
            VechicleDAO vDAO = new VechicleDAO();

            listVechicle = vDAO.ListVechicleByCriteriaNoPaging(vInfo);

            return Ok<List<VechicleListReturn>>(listVechicle);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountVechicleListByCriteria")]
        public IHttpActionResult CountVhListByCriteria([FromBody]VechicleInfo vInfo)
        {
            VechicleDAO vDAO = new VechicleDAO();
            int? i = 0;
            i = vDAO.CountVechicleListByCriteria(vInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertVechicle")]
        public IHttpActionResult InsVechicle([FromBody]VechicleInfo vInfo)
        {
            VechicleDAO vDAO = new VechicleDAO();
            int i = 0;
            i = vDAO.InsertVechicle(vInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateVechicle")]
        public IHttpActionResult UpdVechicle([FromBody]VechicleInfo vInfo)
        {
            VechicleDAO vDAO = new VechicleDAO();
            int? i = 0;
            i = vDAO.UpdateVechicle(vInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteVechicle")]
        public IHttpActionResult DelVechicle([FromBody]VechicleInfo vInfo)
        {
            VechicleDAO vDAO = new VechicleDAO();
            int? i = 0;
            i = vDAO.DeleteVechicle(vInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/VechicleCheck")]
        public IHttpActionResult VhCheck([FromBody]VechicleInfo vInfo)
        {
            VechicleListReturn vechicleList = new VechicleListReturn();
            List<VechicleListReturn> listVechicle = new List<VechicleListReturn>();
            VechicleDAO vDAO = new VechicleDAO();

            listVechicle = vDAO.VechicleCheck(vInfo);

            return Ok<List<VechicleListReturn>>(listVechicle);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListVechicleByCriteria_DDl")]
        public IHttpActionResult ListVhByCriteria_DDl([FromBody]VechicleInfo vInfo)
        {
            VechicleListReturn vechicleList = new VechicleListReturn();
            List<VechicleListReturn> listVechicle = new List<VechicleListReturn>();
            VechicleDAO vDAO = new VechicleDAO();

            listVechicle = vDAO.ListVechicleByCriteria_ddl(vInfo);

            return Ok<List<VechicleListReturn>>(listVechicle);
        }
    }
}
