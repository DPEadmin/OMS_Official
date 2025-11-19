using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceService
{
    public interface ILineService
    {
        Task<dynamic> PushMessageBroadcastAsync(string BearerToken, object message);
    }

    public class LineService : ILineService
    {
        private readonly ILineClient _lineClient;

        public LineService(ILineClient lineClient)
        {
            _lineClient = lineClient;
        }




        public async Task<dynamic> PushMessageBroadcastAsync(string BearerToken, object messages)
        {
            var result = await _lineClient.PostAsync<dynamic>(BearerToken, "message/broadcast", new { messages });
            return result;
        }

    }
}
