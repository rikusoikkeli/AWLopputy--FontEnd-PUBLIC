using AW_Lopputyo_Web.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AW_Lopputyo_Web
{
    public class LoginApi
    {
        private readonly string _endPoint;
        private readonly string _subscriptionkey;
        private ILogger _logger;

        public object HttpsStatusCode { get; private set; }

        public LoginApi(IConfiguration configuration, ILogger logger)
        {
            _endPoint = configuration.GetConnectionString("EndPoint");
            _subscriptionkey = configuration.GetConnectionString("SubscriptionKey");
            _logger = logger;
        }

        public async Task<bool> UserExists(LoginInfo loginInfo)
        {
            using (var client = new HttpClient())
            {
                var device = JsonConvert.SerializeObject(loginInfo);
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_endPoint}/api/device?deviceId={loginInfo.DeviceId}&password={loginInfo.Password}"),
                };
                request.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionkey);

                var response = await client.SendAsync(request);

                var contentString = await response.Content.ReadAsStringAsync();
                if (contentString == "true")
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> CreateUser(NewUserInfo newUserInfo)
        {
            using (var client = new HttpClient())
            {
                var device = JsonConvert.SerializeObject(new { DeviceID = newUserInfo.DeviceId, Password = newUserInfo.Password1 });
                StringContent data = new StringContent(device, Encoding.UTF8, "application/json");
                data.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionkey);
                var response = await client.PostAsync($"{_endPoint}/api/device", data);

                var contentString = await response.Content.ReadAsStringAsync();
                if (contentString == "true")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
