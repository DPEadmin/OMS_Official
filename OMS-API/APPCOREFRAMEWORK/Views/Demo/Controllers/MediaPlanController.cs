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
    public class MediaPlanController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertMediaPlan")]
        public IHttpActionResult InsMediaPlan([FromBody]MediaPlanInfo mInfo)
        {

            MediaPlanDAO mDAO = new MediaPlanDAO();
            int i = 0;
            i = mDAO.InsertMediaPlan(mInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertMediaPlanList")]
        public IHttpActionResult InsertMediaPlanList([FromBody]L_MediaPlan mInfo)
        {
            MediaPlanDAO mDAO = new MediaPlanDAO();
            int i = 0;
            foreach (var mediaPlanList in mInfo.L_MediaPlanInfo.ToList())
            {
                i = mDAO.InsertMediaPlanList(mediaPlanList);
            }
            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMediaPlanNoPagingByCriteria")]
        public IHttpActionResult ListMediaPlanNoPagingByCriteria([FromBody]MediaPlanInfo mInfo)
        {
            MediaPlanListReturn MediaPlanList = new MediaPlanListReturn();
            List<MediaPlanListReturn> listMediaPlan = new List<MediaPlanListReturn>();
            MediaPlanDAO mDAO = new MediaPlanDAO();

            listMediaPlan = mDAO.ListMediaPlanNoPagingByCriteria(mInfo);

            return Ok<List<MediaPlanListReturn>>(listMediaPlan);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListMediaPlan")]
        public IHttpActionResult CountListMediaPlan([FromBody]MediaPlanInfo mInfo)
        {
            MediaPlanDAO mDAO = new MediaPlanDAO();
            int? i = 0;
            i = mDAO.CountListMediaPlan(mInfo);

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteMediaPlan")]
        public IHttpActionResult DelMediaPlan([FromBody]MediaPlanInfo mInfo)
        {
            MediaPlanDAO mDAO = new MediaPlanDAO();
            int i = 0;
            i = mDAO.DeleteMediaPlanList(mInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ActiveMediaPlanList")]
        public IHttpActionResult ActiveMediaPlanList([FromBody]MediaPlanInfo mInfo)
        {
            MediaPlanDAO mDAO = new MediaPlanDAO();
            int i = 0;
            i = mDAO.ActiveMediaPlanList(mInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateMediaPlan")]
        public IHttpActionResult UpdateMediaPlan([FromBody]MediaPlanInfo pInfo)
        {

            MediaPlanDAO pDAO = new MediaPlanDAO();
            int i = 0;
            i = pDAO.UpdateMediaPlan(pInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertMediaPhone")]
        public IHttpActionResult InsertMediaPhone([FromBody] MediaPhoneInfo pInfo)
        {

            MediaPlanDAO pDAO = new MediaPlanDAO();
            int i = 0;
            i = pDAO.InsertMediaPhone(pInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateMediaPhone")]
        public IHttpActionResult UpdateMediaPhone([FromBody] MediaPhoneInfo pInfo)
        {

            MediaPlanDAO pDAO = new MediaPlanDAO();
            int i = 0;
            i = pDAO.UpdateMediaPhone(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMediaPhoneNoPagingByCriteria")]
        public IHttpActionResult ListMediaPhoneNoPagingByCriteria([FromBody] MediaPhoneInfo mInfo)
        {
            MediaPhoneInfo MediaPlanList = new MediaPhoneInfo();
            List<MediaPhoneInfo> listMediaPlan = new List<MediaPhoneInfo>();
            MediaPlanDAO mDAO = new MediaPlanDAO();

            listMediaPlan = mDAO.ListMediaPhoneNoPagingByCriteria(mInfo);

            return Ok<List<MediaPhoneInfo>>(listMediaPlan);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMerchantForValByCriteria")]
        public IHttpActionResult ListMerchantForValByCriteria([FromBody] MerchantInfo mInfo)
        {
            MerchantInfo MediaPlanList = new MerchantInfo();
            List<MerchantInfo> listMediaPlan = new List<MerchantInfo>();
            MediaPlanDAO mDAO = new MediaPlanDAO();

            listMediaPlan = mDAO.ListMerchantForValByCriteria(mInfo);

            return Ok<List<MerchantInfo>>(listMediaPlan);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMediaPhoneNoPagingByCriteria_Val")]
        public IHttpActionResult ListMediaPhoneNoPagingByCriteria_Val([FromBody] MediaPhoneInfo mInfo)
        {
            MediaPhoneInfo MediaPlanList = new MediaPhoneInfo();
            List<MediaPhoneInfo> listMediaPlan = new List<MediaPhoneInfo>();
            MediaPlanDAO mDAO = new MediaPlanDAO();

            listMediaPlan = mDAO.ListMediaPhoneNoPagingByCriteria_Val(mInfo);

            return Ok<List<MediaPhoneInfo>>(listMediaPlan);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteMediaPhone")]
        public IHttpActionResult DeleteMediaPhone([FromBody] MediaPhoneInfo pInfo)
        {

            MediaPlanDAO pDAO = new MediaPlanDAO();
            int i = 0;
            i = pDAO.DeleteMediaPhone(pInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListMediaPhone")]
        public IHttpActionResult CountListMediaPhone([FromBody] MediaPhoneInfo pInfo)
        {
            MediaPlanDAO pDAO = new MediaPlanDAO();
            int? i = 0;
            i = pDAO.CountListMediaPhone(pInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMediaChannelNoPagingByCriteria")]
        public IHttpActionResult ListMediaChannelNoPagingByCriteria([FromBody] MediaChannelInfo pInfo)
        {
            MediaChannelInfo SaleChannelList = new MediaChannelInfo();
            List<MediaChannelInfo> listSaleChannel = new List<MediaChannelInfo>();
            MediaPlanDAO pDAO = new MediaPlanDAO();

            listSaleChannel = pDAO.ListMediaChannelNoPagingByCriteria(pInfo);

            return Ok<List<MediaChannelInfo>>(listSaleChannel);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMediaProgramNoPagingByCriteria")]
        public IHttpActionResult ListMediaProgramNoPagingByCriteria([FromBody] MediaProgramInfo pInfo)
        {
            MediaProgramInfo SaleChannelList = new MediaProgramInfo();
            List<MediaProgramInfo> listSaleChannel = new List<MediaProgramInfo>();
            MediaPlanDAO pDAO = new MediaPlanDAO();

            listSaleChannel = pDAO.ListMediaProgramNoPagingByCriteria(pInfo);

            return Ok<List<MediaProgramInfo>>(listSaleChannel);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertMediaPhoneSub")]
        public IHttpActionResult InsertMediaPhoneSub([FromBody] MediaPhoneSubInfo pInfo)
        {

            MediaPlanDAO pDAO = new MediaPlanDAO();
            int i = 0;
            i = pDAO.InsertMediaPhoneSub(pInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateMediaPhoneSub")]
        public IHttpActionResult UpdateMediaPhoneSub([FromBody] MediaPhoneSubInfo pInfo)
        {

            MediaPlanDAO pDAO = new MediaPlanDAO();
            int i = 0;
            i = pDAO.UpdateMediaPhoneSub(pInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMediaPhoneSubNoPagingByCriteria")]
        public IHttpActionResult ListMediaPhoneSubNoPagingByCriteria([FromBody] MediaPhoneSubInfo mInfo)
        {
            MediaPhoneInfo MediaPlanList = new MediaPhoneInfo();
            List<MediaPhoneSubInfo> listMediaPlan = new List<MediaPhoneSubInfo>();
            MediaPlanDAO mDAO = new MediaPlanDAO();

            listMediaPlan = mDAO.ListMediaPhoneSubNoPagingByCriteria(mInfo);

            return Ok<List<MediaPhoneSubInfo>>(listMediaPlan);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListMediaPhoneSub")]
        public IHttpActionResult CountListMediaPhoneSub([FromBody] MediaPhoneSubInfo pInfo)
        {
            MediaPlanDAO pDAO = new MediaPlanDAO();
            int? i = 0;
            i = pDAO.CountListMediaPhoneSub(pInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteMediaPhoneSub")]
        public IHttpActionResult DeleteMediaPhoneSub([FromBody] MediaPhoneSubInfo pInfo)
        {

            MediaPlanDAO pDAO = new MediaPlanDAO();
            int i = 0;
            i = pDAO.DeleteMediaPhoneSub(pInfo);

            return Ok<int>(i);
        }


        // MediaChannel
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertMediaChannel")]
        public IHttpActionResult InsertMediaChannel([FromBody] MediaChannelInfo pInfo)
        {

            MediaPlanDAO pDAO = new MediaPlanDAO();
            int i = 0;
            i = pDAO.InsertMediaChannel(pInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateMediaChannel")]
        public IHttpActionResult UpdateMediaChannel([FromBody] MediaChannelInfo pInfo)
        {

            MediaPlanDAO pDAO = new MediaPlanDAO();
            int i = 0;
            i = pDAO.UpdateMediaChannel(pInfo);

            return Ok<int>(i);
        }

      


        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListMediaChannel")]
        public IHttpActionResult CountListMediaChannel([FromBody] MediaChannelInfo pInfo)
        {
            MediaPlanDAO pDAO = new MediaPlanDAO();
            int? i = 0;
            i = pDAO.CountListMediaChannel(pInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteMediaChannel")]
        public IHttpActionResult DeleteMediaChannel([FromBody] MediaChannelInfo pInfo)
        {

            MediaPlanDAO pDAO = new MediaPlanDAO();
            int i = 0;
            i = pDAO.DeleteMediaChannel(pInfo);

            return Ok<int>(i);
        }
    }
}