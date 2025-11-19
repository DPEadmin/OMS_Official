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
    public class BranchMapProductController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListBranchMapProductByCriteria")]
        public IHttpActionResult CountListBranchMapProductByCriteria([FromBody]BranchMapProductInfo bmpInfo)
        {
            BranchMapProductDAO bmpDAO = new BranchMapProductDAO();
            int? i = 0;
            i = bmpDAO.CountListBranchMapProductByCriteria(bmpInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListBranchMapProductByCriteria")]
        public IHttpActionResult ListBranchMapProductByCriteria([FromBody]BranchMapProductInfo bmpInfo)
        {
            List<BranchMapProductList> bpmList = new List<BranchMapProductList>();
            BranchMapProductDAO bmpDAO = new BranchMapProductDAO();

            bpmList = bmpDAO.ListBranchMapProductByCriteria(bmpInfo);

            return Ok<List<BranchMapProductList>>(bpmList);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListBranchMapProductByCriteriaWithOneTxt")]
        public IHttpActionResult CountListBranchMapProductByCriteriaWithOneTxt([FromBody]BranchMapProductInfo bmpInfo)
        {
            BranchMapProductDAO bmpDAO = new BranchMapProductDAO();
            int? i = 0;
            i = bmpDAO.CountListBranchMapProductByCriteriaWithOneTxtbox(bmpInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListBranchMapProductByCriteriaWithOneTxt")]
        public IHttpActionResult ListBranchMapProductByCriteriaWithOneTxt([FromBody]BranchMapProductInfo bmpInfo)
        {
            List<BranchMapProductList> bpmList = new List<BranchMapProductList>();
            BranchMapProductDAO bmpDAO = new BranchMapProductDAO();

            bpmList = bmpDAO.ListBranchMapProductByCriteriaWithOneTxtbox(bmpInfo);

            return Ok<List<BranchMapProductList>>(bpmList);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateBranchMapProductActiveFlag")]
        public IHttpActionResult UpdBranchMapProduct([FromBody]BranchMapProductInfo bmpInfo)
        {
            int i = 0;
            BranchMapProductDAO cDAO = new BranchMapProductDAO();

            i = cDAO.UpdateBranchMapProductActiveFlag(bmpInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateBranchMapProductCancelFlag")]
        public IHttpActionResult UpdBranchMapProductCancelFlag([FromBody]BranchMapProductInfo bmpInfo)
        {
            int i = 0;
            BranchMapProductDAO cDAO = new BranchMapProductDAO();

            i = cDAO.UpdateBranchMapProductCancelFlag(bmpInfo);

            return Ok<int>(i);
        }

        [HttpPost]
        [Route("api/support/UpdateBranchMapProductActiveFlagWithString")]
        public IHttpActionResult UpdBranchMapProductActiveFlagWithString([FromBody]BranchMapProductInfo bmpInfo)
        {
            int i = 0;
            BranchMapProductDAO cDAO = new BranchMapProductDAO();

            i = cDAO.UpdateBranchMapProductActiveFlagWithString(bmpInfo);

            return Ok<int>(i);
        }
    }
}