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
    public class MenuController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMenuByCriteria")]
        public IHttpActionResult ListMnByCriteria([FromBody]MenuInfo mInfo)
        {
            MenuListReturn menuList = new MenuListReturn();
            List<MenuListReturn> listMenu = new List<MenuListReturn>();
            MenuDAO mDAO = new MenuDAO();

            listMenu = mDAO.ListMenuByCriteria(mInfo);

            return Ok<List<MenuListReturn>>(listMenu);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMenuNull")]
        public IHttpActionResult ListMnNull([FromBody]MenuInfo mInfo)
        {
            MenuListReturn menuList = new MenuListReturn();
            List<MenuListReturn> listMenu = new List<MenuListReturn>();
            MenuDAO mDAO = new MenuDAO();

            listMenu = mDAO.ListMenuNull(mInfo);

            return Ok<List<MenuListReturn>>(listMenu);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/CountMenubyCriteria")]
        public IHttpActionResult CountMnbyCriteria()
        {
           
            MenuDAO mDAO = new MenuDAO();
            int? i = 0;
            i = mDAO.CountMenubyCriteria();

            return Ok<int?>(i);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/ListMenuPageLoadPageEditRanking")]
        public IHttpActionResult ListMnPageLoadPageEditRanking([FromBody]MenuInfo mInfo)
        {
            MenuListReturn menuList = new MenuListReturn();
            List<MenuListReturn> listMenu = new List<MenuListReturn>();
            MenuDAO mDAO = new MenuDAO();

            listMenu = mDAO.ListMenuPageLoadPageEditRanking(mInfo);

            return Ok<List<MenuListReturn>>(listMenu);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/support/UpdateMenuRanking")]
        public IHttpActionResult UpdateMenuRanking([FromBody]MenuInfo mInfo)
        {
            
            MenuDAO mDAO = new MenuDAO();
            int? i = 0;
            i = mDAO.UpdateMenuRanking(mInfo);

            return Ok<int?>(i);
        }
    }
}
