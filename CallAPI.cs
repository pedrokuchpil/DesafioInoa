using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Broker
{
    public class CallAPI
    {
        public async Task<Double> checkValue(Asset asset)
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri("https://brapi.dev/api/quote/") };

            var result = await client.GetAsync(asset.name);
            
            var json = await result.Content.ReadAsStringAsync();
            var root = JObject.Parse(json);
            
            double value = root["results"][0]["regularMarketPrice"].ToObject<double>();

            return value;
            
        }
    }
}