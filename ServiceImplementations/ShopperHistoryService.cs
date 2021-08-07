using eXercise.Entities;
using eXercise.ServiceInterfaces;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ServiceImplementations;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace eXercise.ServiceImplementations
{
    public class ShopperHistoryService : IShopperHistoryService
    {
        private readonly string _apiBaseUrl;
        private readonly string _token;
        public ShopperHistoryService(IOptions<ExternalServiceSettings> options)
        {
            _apiBaseUrl = options.Value.ExternalServiceBaseUrl;
            _token = options.Value.Token;
        }

        public async Task<IEnumerable<ShopperHistory>> GetShopperHistoryAsync()
        {
            var shopperHistoryRequestUrl = $"{_apiBaseUrl}/resource/shopperHistory?token={_token}";
            var shopperHistoryJsonString = await shopperHistoryRequestUrl.GetStringAsync();

            return JsonConvert.DeserializeObject<List<ShopperHistory>>(shopperHistoryJsonString);
        }
    }
}
