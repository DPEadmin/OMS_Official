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
    public class CombosetController : ApiController
    {

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCombosetByCriteria")]
        public IHttpActionResult LstCombosetByCriteria([FromBody]CombosetInfo bInfo)
        {
            List<CombosetReturn> listComboset = new List<CombosetReturn>();
            CombosetDAO bDAO = new CombosetDAO();

            listComboset = bDAO.ListCombosetByCriteria(bInfo);

            return Ok<List<CombosetReturn>>(listComboset);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCombosetNopagingByCriteria")]
        public IHttpActionResult LstCombosetNopagingByCriteria([FromBody]CombosetInfo bInfo)
        {
            List<CombosetReturn> listComboset = new List<CombosetReturn>();
            CombosetDAO bDAO = new CombosetDAO();

            listComboset = bDAO.ListCombosetNopagingByCriteria(bInfo);

            return Ok<List<CombosetReturn>>(listComboset);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountCombosetByCriteria")]
        public IHttpActionResult CntCombosetByCriteria([FromBody]CombosetInfo bInfo)
        {
            int? count = 0;
            CombosetDAO bDAO = new CombosetDAO();

            count = bDAO.CountCombosetByCriteria(bInfo);

            return Ok<int?>(count);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/InsertComboset")]
        public IHttpActionResult InsComboset([FromBody]CombosetInfo bInfo)
        {
            int i = 0;
            CombosetDAO bDAO = new CombosetDAO();

            i = bDAO.InsertComboset(bInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateComboset")]
        public IHttpActionResult UpdComboset([FromBody]CombosetInfo bInfo)
        {
            int i = 0;
            CombosetDAO bDAO = new CombosetDAO();

            i = bDAO.UpdateComboset(bInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteComboset")]
        public IHttpActionResult DelComboset([FromBody]CombosetInfo bInfo)
        {
            int i = 0;
            CombosetDAO bDAO = new CombosetDAO();

            i = bDAO.DeleteComboset(bInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteCombosetByCode")]
        public IHttpActionResult DelCombosetByCode([FromBody]CombosetInfo bInfo)
        {
            int i = 0;
            CombosetDAO bDAO = new CombosetDAO();

            i = bDAO.DeleteCombosetByCode(bInfo);

            return Ok<int>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListPromotionCombosetNopagingByCriteria")]
        public IHttpActionResult ListPromotionCombosetNopagingByCriteria([FromBody]CombosetInfo bInfo)
        {
            List<CombosetReturn> listComboset = new List<CombosetReturn>();
            CombosetDAO bDAO = new CombosetDAO();

            listComboset = bDAO.ListPromotionCombosetNopagingByCriteria(bInfo);

            return Ok<List<CombosetReturn>>(listComboset);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListCombosetByCriteriaMaster")]
        public IHttpActionResult LstCombosetByCriteriaMaster([FromBody]CombosetInfo bInfo)
        {
            List<CombosetReturn> listComboset = new List<CombosetReturn>();
            CombosetDAO bDAO = new CombosetDAO();

            listComboset = bDAO.ListCombosetByCriteriaMaster(bInfo);

            return Ok<List<CombosetReturn>>(listComboset);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/DeleteCombosetDetailInfoByIdString")]
        public IHttpActionResult DeleteCombosetDetailInfoByIdString([FromBody]PromotionDetailInfo bInfo)
        {
            int i = 0;
            PromotionDetailDAO bDAO = new PromotionDetailDAO();

            i = bDAO.DeleteCombosetDetailInfoByIdString(bInfo);

            return Ok<int>(i);
        }

    }
}
