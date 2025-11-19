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
    public class MerchantController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/MerchantListNoPagingCriteria")]
        public IHttpActionResult MchantListNoPaging([FromBody] MerchantInfo mInfo)
        {
            MerchantListReturn merchantReturn = new MerchantListReturn();
            List<MerchantListReturn> lMerchantListReturn = new List<MerchantListReturn>();
            MerchantDAO mDAO = new MerchantDAO();

            lMerchantListReturn = mDAO.MerchantListNoPagingCriteria(mInfo);

            return Ok<List<MerchantListReturn>>(lMerchantListReturn);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/MerchantListPagingCriteria")]
        public IHttpActionResult MchantListPaging([FromBody] MerchantInfo mInfo)
        {
            MerchantListReturn merchantReturn = new MerchantListReturn();
            List<MerchantListReturn> lMerchantListReturn = new List<MerchantListReturn>();
            MerchantDAO mDAO = new MerchantDAO();

            lMerchantListReturn = mDAO.MerchantListPagingCriteria(mInfo);

            return Ok<List<MerchantListReturn>>(lMerchantListReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetMerchantType")]
        public IHttpActionResult GetMerchantType([FromBody] MerchantInfo mInfo)
        {
            MerchantListReturn merchantReturn = new MerchantListReturn();
            List<MerchantListReturn> lmerchantListReturns = new List<MerchantListReturn>();
            MerchantDAO mDAO = new MerchantDAO();

            lmerchantListReturns = mDAO.GetMerchantType(mInfo);

            return Ok<List<MerchantListReturn>>(lmerchantListReturns);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountMerchantListByCriteria")]
        public IHttpActionResult CountMerchantList([FromBody] MerchantInfo mInfo)
        {
            MerchantDAO mDAO = new MerchantDAO();
            int? i = 0;
            i = mDAO.CountMerchantListByCriteria(mInfo);

            return Ok<int?>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateMerchant")]
        public IHttpActionResult Updatemerchnt([FromBody] MerchantInfo mInfo)
        {
            MerchantDAO mDAO = new MerchantDAO();
            int i = 0;
            i = mDAO.UpdateMerchant(mInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMerchantValidatePagingByCriteria")]
        public IHttpActionResult ListMChantValidatePagingByCriteria([FromBody] MerchantInfo mInfo)
        {
            MerchantListReturn merchantList = new MerchantListReturn();
            List<MerchantListReturn> listMerchant = new List<MerchantListReturn>();
            MerchantDAO mDAO = new MerchantDAO();

            listMerchant = mDAO.ListMerchantValidatePagingByCriteria(mInfo);

            return Ok<List<MerchantListReturn>>(listMerchant);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertMerchant")]
        public IHttpActionResult InsertMchant([FromBody] MerchantInfo mInfo)
        {
            MerchantDAO mDAO = new MerchantDAO();
            int i = 0;
            i = mDAO.InsertMerchant(mInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertMerchantImage")]
        public IHttpActionResult InsertMerchantImage([FromBody] MerchantImageInfo mInfo)
        {
            MerchantDAO mDAO = new MerchantDAO();
            int i = 0;
            i = mDAO.InsertMerchantImage(mInfo);

            return Ok<int>(i);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteMerchant")]
        public IHttpActionResult DeleteMChant([FromBody] MerchantInfo mInfo)
        {
            MerchantDAO mDAO = new MerchantDAO();
            int i = 0;
            i = mDAO.DeleteMerchant(mInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteMapMerchant")]
        public IHttpActionResult DeleteMapMChant([FromBody] MerchantInfo mInfo)
        {
            MerchantDAO mDAO = new MerchantDAO();
            int i = 0;
            i = mDAO.DeleteMapMerchant(mInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetNearestMerchantNopaging")]
        public IHttpActionResult GetNearestMCNopaging([FromBody] Customer_CompanyModel mInfo)
        {
            MerchantListReturn merchantReturn = new MerchantListReturn();
            List<MerchantListReturn> lMerchantListReturn = new List<MerchantListReturn>();
            MerchantDAO mDAO = new MerchantDAO();
            //CustomerAddressListReturn cusaddReturn = new CustomerAddressListReturn();
            //List<CustomerAddressListReturn> lcusaddReturn = new List<CustomerAddressListReturn>();
            //CustomerAddressDAO cDAO = new CustomerAddressDAO();


            lMerchantListReturn = mDAO.GetNearestMerchantNopaging(mInfo.CustomerInfo, mInfo.CompanyInfo);

            return Ok<List<MerchantListReturn>>(lMerchantListReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetNearestMerchant")]
        public IHttpActionResult GetNearestMC([FromBody] Customer_CompanyModel mInfo)
        {
            MerchantListReturn merchantReturn = new MerchantListReturn();
            List<MerchantListReturn> lMerchantListReturn = new List<MerchantListReturn>();
            MerchantDAO mDAO = new MerchantDAO();

            lMerchantListReturn = mDAO.GetNearestMerchant(mInfo.CustomerInfo, mInfo.CompanyInfo);

            return Ok<List<MerchantListReturn>>(lMerchantListReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMerchantEnterprise")]
        public IHttpActionResult ListMerchantEnterprise([FromBody] MerchantInfo mInfo)
        {
            MerchantListReturn merchantReturn = new MerchantListReturn();
            List<MerchantListReturn> lMerchantListReturn = new List<MerchantListReturn>();
            MerchantDAO mDAO = new MerchantDAO();

            lMerchantListReturn = mDAO.ListMerchantEnterprise(mInfo);

            return Ok<List<MerchantListReturn>>(lMerchantListReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMerchantEnterpriseForAuth")]
        public IHttpActionResult ListMerchantEnterpriseForAuth([FromBody] MerchantInfo mInfo)
        {
            MerchantListReturn merchantReturn = new MerchantListReturn();
            List<MerchantListReturn> lMerchantListReturn = new List<MerchantListReturn>();
            MerchantDAO mDAO = new MerchantDAO();

            lMerchantListReturn = mDAO.ListMerchantEnterpriseForAuth(mInfo);

            return Ok<List<MerchantListReturn>>(lMerchantListReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMerchantMappingUserName")]
        public IHttpActionResult ListMerchantMappingUserName([FromBody] MerchantInfo mInfo)
        {
            MerchantListReturn merchantReturn = new MerchantListReturn();
            List<MerchantListReturn> lMerchantListReturn = new List<MerchantListReturn>();
            MerchantDAO mDAO = new MerchantDAO();

            lMerchantListReturn = mDAO.ListMerchantMappingUserName(mInfo);

            return Ok<List<MerchantListReturn>>(lMerchantListReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertMermap")]
        public IHttpActionResult InsertMerchantMapping([FromBody] MerchantInfo mInfo)
        {
            MerchantDAO mDAO = new MerchantDAO();
            int i = 0;
            i = mDAO.InsertMerchantMapping(mInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/GetMerchantAddress")]
        public IHttpActionResult ListMerchantAddress([FromBody] MerchantInfo mInfo)
        {
            MerchantListReturn merchantReturn = new MerchantListReturn();
            List<MerchantListReturn> lMerchantListReturn = new List<MerchantListReturn>();
            MerchantDAO mDAO = new MerchantDAO();

            lMerchantListReturn = mDAO.ListMerchantAddress(mInfo);

            return Ok<List<MerchantListReturn>>(lMerchantListReturn);
        }


    }
}
