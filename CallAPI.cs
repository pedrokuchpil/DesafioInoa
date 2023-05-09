using Newtonsoft.Json.Linq;

namespace Broker
{
    public class CallAPI
    {
        public async Task checkValue(Asset asset)
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri("https://brapi.dev/api/quote/") };

            var result = await client.GetAsync(asset.name);
            if (result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                var root = JObject.Parse(json);
                
                double value = root["results"][0]["regularMarketPrice"].ToObject<double>();

                Console.WriteLine($"Valor atual: {value}");

                if (value >= asset.sell_value)
                {
                    Console.WriteLine($"Vender {asset.name}");
                }
                else if (value <= asset.buy_value)
                {
                    Console.WriteLine($"Comprar {asset.name}");
                }
                else
                {
                    Console.WriteLine($"Não fazer nada com {asset.name}");
                }
            }
        }
    }
}