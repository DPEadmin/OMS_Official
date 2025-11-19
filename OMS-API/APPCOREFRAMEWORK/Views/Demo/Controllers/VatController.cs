using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using APPCOREMODEL.OMSDAO;
using APPCOREMODEL.Datas.OMSDTO;

namespace APPCOREVIEW.Views.Demo.Controllers
{
    public class VatController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListVatByCriteria")]
        public IHttpActionResult ListVatByCriteria([FromBody]VatInfo vInfo)
        {
            List<VatListReturn> listVat = new List<VatListReturn>();
            VatDAO vDAO = new VatDAO();

            listVat = vDAO.ListVatByCriteria(vInfo);

            return Ok<List<VatListReturn>>(listVat);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountListVatByCriteria")]
        public IHttpActionResult CountListVatByCriteria([FromBody]VatInfo vInfo)
        {
            VatDAO vDAO = new VatDAO();
            int? i = 0;
            i = vDAO.CountListVatByCriteria(vInfo);

            return Ok<int?>(i);
        }
    }
}