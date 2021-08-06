using eXercise.Entities;
using eXercise.ServiceInterfaces;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ServiceImplementations;
using System;
using System.Threading.Tasks;

namespace eXercise.ServiceImplementations
{
    public class TrolleyService : ITrolleyService
    {
        private readonly string _apiBaseUrl;
        private readonly string _token;
        public TrolleyService(IOptions<ExternalServiceSettings> options)
        {
            _apiBaseUrl = options.Value.ExternalServiceBaseUrl;
            _token = options.Value.Token;
        }

        public async Task<decimal> GetTrolleyTotalAsync(TrolleyRequest trolleyRequest)
        {
            try
            {
                var requestUrl = $"{_apiBaseUrl}/resource/trolleyCalculator?token={_token}";

                var debugJson = JsonConvert.SerializeObject(trolleyRequest);

                var result = await requestUrl.PostJsonAsync(trolleyRequest).ReceiveString();

                if(string.IsNullOrWhiteSpace(result) == false)
                {
                    return Convert.ToDecimal(result);
                }
                else
                {
                    return 0;
                }
            }
            catch(Exception ex)
            {
                var err = ex.Message;
            }
            return 0;
        }
    }
}
