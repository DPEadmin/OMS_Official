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
    public class AddressController : ApiController
    {

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListAddressByCriteria")]
        public IHttpActionResult ListAddrByCriteria([FromBody]AddressInfo aInfo)
        {
            AddressListReturn AddressList = new AddressListReturn();
            List<AddressListReturn> listAddress = new List<AddressListReturn>();
            AddressDAO aDAO = new AddressDAO();

            listAddress = aDAO.ListAddressByCriteria(aInfo);

            return Ok<List<AddressListReturn>>(listAddress);
        }
    }
}
