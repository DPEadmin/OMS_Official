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
    public class DashBoardController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDashBoardAmountOrder")]
        public IHttpActionResult ListDashBoardAmountOrder([FromBody]MonthlyInfo cInfo)
        {
            MonthlyInfo callInList = new MonthlyInfo();
            List<MonthlyInfo> listCallIn = new List<MonthlyInfo>();
            DashBoardDAO callDAO = new DashBoardDAO();

            listCallIn = callDAO.ListDashBoardByCriteria(cInfo);

            return Ok<List<MonthlyInfo>>(listCallIn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDashBoardPercentOrder")]
        public IHttpActionResult ListDashBoardPercentOrder([FromBody]MonthlyInfo cInfo)
        {
            MonthlyInfo callInList = new MonthlyInfo();
            List<MonthlyInfo> listCallIn = new List<MonthlyInfo>();
            DashBoardDAO callDAO = new DashBoardDAO();

            listCallIn = callDAO.ListDashBoardPercentOrderByCriteria(cInfo);

            return Ok<List<MonthlyInfo>>(listCallIn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDashAmountOrderandPrice")]
        public IHttpActionResult ListDashBoardAmountOrderandPrice([FromBody]MonthlyInfo cInfo)
        {
            MonthlyInfo callInList = new MonthlyInfo();
            List<MonthlyInfo> listCallIn = new List<MonthlyInfo>();
            DashBoardDAO callDAO = new DashBoardDAO();

            listCallIn = callDAO.ListDashBoardAmountOrderandPriceByCriteria(cInfo);

            return Ok<List<MonthlyInfo>>(listCallIn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDashBoardCallIn")]
        public IHttpActionResult ListDashBoardCallIn([FromBody]MonthlyInfo cInfo)
        {
            MonthlyInfo callInList = new MonthlyInfo();
            List<MonthlyInfo> listCallIn = new List<MonthlyInfo>();
            DashBoardDAO callDAO = new DashBoardDAO();

            listCallIn = callDAO.ListDashBoardCallInByCriteria(cInfo);

            return Ok<List<MonthlyInfo>>(listCallIn);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDashBoardAverCallIn")]
        public IHttpActionResult ListDashBoardAverCallIn([FromBody]MonthlyInfo cInfo)
        {
            MonthlyInfo callInList = new MonthlyInfo();
            List<MonthlyInfo> listCallIn = new List<MonthlyInfo>();
            DashBoardDAO callDAO = new DashBoardDAO();

            listCallIn = callDAO.ListDashBoardAVerCallInByCriteria(cInfo);

            return Ok<List<MonthlyInfo>>(listCallIn);
        }

       

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDashAverAmountOrderandPrice")]
        public IHttpActionResult ListDashBoardAverAmountOrderandPrice([FromBody]MonthlyInfo cInfo)
        {
            MonthlyInfo callInList = new MonthlyInfo();
            List<MonthlyInfo> listCallIn = new List<MonthlyInfo>();
            DashBoardDAO callDAO = new DashBoardDAO();

            listCallIn = callDAO.ListDashBoardAverAmountOrderandPriceByCriteria(cInfo);

            return Ok<List<MonthlyInfo>>(listCallIn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDashTotalHeader")]
        public IHttpActionResult ListDashBoardTotalHeader([FromBody]MonthlyInfo cInfo)
        {
            TotalMonthlyInfo callInList = new TotalMonthlyInfo();
            List<TotalMonthlyInfo> listCallIn = new List<TotalMonthlyInfo>();
            DashBoardDAO callDAO = new DashBoardDAO();

            listCallIn = callDAO.ListDashBoardTotalHeaderByCriteria(cInfo);

            return Ok<List<TotalMonthlyInfo>>(listCallIn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDashBoardAverAmountPerHour")]
        public IHttpActionResult ListDashBoardAverAmountPerHour([FromBody]MonthlyInfo cInfo)
        {
            MonthlyInfo callInList = new MonthlyInfo();
            List<MonthlyInfo> listCallIn = new List<MonthlyInfo>();
            DashBoardDAO callDAO = new DashBoardDAO();

            listCallIn = callDAO.ListDashBoardAverAmountPerHourByCriteria(cInfo);

            return Ok<List<MonthlyInfo>>(listCallIn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDashBoardProductAmount")]
        public IHttpActionResult ListDashBoardProductAmount([FromBody]MonthlyInfo cInfo)
        {
            MonthlyInfo callInList = new MonthlyInfo();
            List<ProductAmountInfo> listCallIn = new List<ProductAmountInfo>();
            DashBoardDAO callDAO = new DashBoardDAO();

            listCallIn = callDAO.ListDashBoardProductAmountByCriteria(cInfo);

            return Ok<List<ProductAmountInfo>>(listCallIn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDashBoardPromotionAmount")]
        public IHttpActionResult ListDashBoardPromotionAmount([FromBody] MonthlyInfo cInfo)
        {
            List<PromotionAmountInfo> listPromoAmount = new List<PromotionAmountInfo>();
            DashBoardDAO callDAO = new DashBoardDAO();

            listPromoAmount = callDAO.ListDashBoardPromotionAmountByCriteria(cInfo);

            return Ok<List<PromotionAmountInfo>>(listPromoAmount);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDashBoardTotalAmountOrder")]
        public IHttpActionResult ListDashBoardTotalAmountOrder([FromBody]MonthlyInfo cInfo)
        {
            MonthlyInfo callInList = new MonthlyInfo();
            List<MonthlyInfo> listCallIn = new List<MonthlyInfo>();
            DashBoardDAO callDAO = new DashBoardDAO();

            listCallIn = callDAO.ListDashBoardTotalAmountByCriteria(cInfo);

            return Ok<List<MonthlyInfo>>(listCallIn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListDashBoardStatus")]
        public IHttpActionResult ListDashBoardStatus([FromBody] StatusInfo cInfo)
        {
            List<StatusInfo> listStatus = new List<StatusInfo>();
            DashBoardDAO callDAO = new DashBoardDAO();
            listStatus = callDAO.ListDashBoardStatusByCriteria(cInfo);

            return Ok<List<StatusInfo>>(listStatus);
        }
    }
}
