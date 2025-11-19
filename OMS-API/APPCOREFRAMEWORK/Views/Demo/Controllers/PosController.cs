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
    public class PosController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdatePosterminal")]
        public IHttpActionResult UpdatePosterminal([FromBody] PosTerminalInfo pInfo)
        {
            PosDAO cDAO = new PosDAO();
            int i = 0;
            i = cDAO.UpdatePosterminal(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertPosterminal")]
        public IHttpActionResult InsertPosterminal([FromBody] PosTerminalInfo pInfo)
        {
            PosDAO cDAO = new PosDAO();
            int i = 0;
            i = cDAO.InsertPosterminal(pInfo);

            return Ok<int>(i);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeletePosterminalRange")]
        public IHttpActionResult DeletePosterminalRange([FromBody] PosTerminalInfo pInfo)
        {

            PosDAO cDAO = new PosDAO();
            int i = 0;
            i = cDAO.DeletePosterminalRange(pInfo);

            return Ok<int>(i);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPosterminalPagingbyCriteria")]
        public IHttpActionResult ListPosterminalPagingbyCriteria([FromBody] PosTerminalInfo pInfo)
        {
            List<PosTerminalInfo> listPoint = new List<PosTerminalInfo>();
            PosDAO cDAO = new PosDAO();

            listPoint = cDAO.ListPosterminalPagingbyCriteria(pInfo);

            return Ok<List<PosTerminalInfo>>(listPoint);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPosAmountManagement")]
        public IHttpActionResult ListPosAmountManagement([FromBody] PosAmountManagementInfo pInfo)
        {
            List<PosAmountManagementInfo> listPoint = new List<PosAmountManagementInfo>();
            PosDAO cDAO = new PosDAO();

            listPoint = cDAO.ListPosAmountManagement(pInfo);

            return Ok<List<PosAmountManagementInfo>>(listPoint);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertPosAmountManagement")]
        public IHttpActionResult InsertPosAmountManagement([FromBody] PosAmountManagementInfo pInfo)
        {
            PosDAO cDAO = new PosDAO();
            int i = 0;
            i = cDAO.InsertPosAmountManagement(pInfo);

            return Ok<int>(i);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdatePosAmountManagement")]
        public IHttpActionResult UpdatePosAmountManagement([FromBody] PosAmountManagementInfo pInfo)
        {
            PosDAO cDAO = new PosDAO();
            int i = 0;
            i = cDAO.UpdatePosAmountManagement(pInfo);

            return Ok<int>(i);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPosOpenSaleorder")]
        public IHttpActionResult ListPosOpenSaleorder([FromBody] PosopenSaleReturn pInfo)
        {
            List<PosopenSaleReturn> listPoint = new List<PosopenSaleReturn>();
            PosDAO cDAO = new PosDAO();

            listPoint = cDAO.ListPosOpenSaleorder(pInfo);

            return Ok<List<PosopenSaleReturn>>(listPoint);
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdatePosOpenSaleId")]
        public IHttpActionResult UpdatePosOpenSaleId([FromBody] PosopenSaleInfo pInfo)
        {
            PosDAO cDAO = new PosDAO();
            int i = 0;
            i = cDAO.UpdatePosOpenSaleId(pInfo);

            return Ok<int>(i);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertPosOpenSaleId")]
        public IHttpActionResult InsertPosOpenSaleId([FromBody] PosopenSaleInfo pInfo)
        {
            PosDAO cDAO = new PosDAO();
            int i = 0;
            i = cDAO.InsertPosOpenSaleId(pInfo);

            return Ok<int>(i);
        }

    }
}
