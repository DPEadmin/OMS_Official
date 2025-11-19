using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MarketplaceService
{
    public interface IFacebookService
    {
        Task<dynamic> PostOnPageWallAsync(string accessToken, string PageID, string message);
    }

    public class FacebookService : IFacebookService
    {
        private readonly IFacebookClient _facebookClient;

        public FacebookService(IFacebookClient facebookClient)
        {
            _facebookClient = facebookClient;
        }

 



        public async Task<dynamic> PostOnPageWallAsync(string accessToken, string PageID, string message)
        {
            var result = await _facebookClient.PostAsync<dynamic>(accessToken, "" + PageID + "/feed", new { message });
            return result;
        }

    }
}
