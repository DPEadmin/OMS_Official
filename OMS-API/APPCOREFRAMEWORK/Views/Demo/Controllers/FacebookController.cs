using MarketplaceService;
using APPCOREMODEL.Datas.Marketplace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace APPCOREVIEW.Views.Demo.Controllers
{
    public class FacebookController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/PostOnPageWall")]
        public async Task<dynamic> PostOnPageWall([FromBody] FacebookPostOnPageWall PostOnPageWall)
        {
            var facebookClient = new FacebookClient();
            var facebookService = new FacebookService(facebookClient);
            var postOnPageWallTask = facebookService.PostOnPageWallAsync(PostOnPageWall.token, PostOnPageWall.PageID, PostOnPageWall.Message);
            var result = await Task.WhenAll(postOnPageWallTask);
            return result;
        }


    }
}
