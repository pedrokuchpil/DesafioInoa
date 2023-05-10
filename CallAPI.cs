using Newtonsoft.Json.Linq;

namespace Broker
{
    public class CallAPI
    {
        public async Task checkValue(Asset asset)
        {
            var email = new Email();
            HttpClient client = new HttpClient { BaseAddress = new Uri("https://brapi.dev/api/quote/") };

            while(true)
            {
                var result = await client.GetAsync(asset.name);
                
                var json = await result.Content.ReadAsStringAsync();
                var root = JObject.Parse(json);
                
                double value = root["results"][0]["regularMarketPrice"].ToObject<double>();

                if (value >= asset.sell_value && email.status != "SENT_SELL")
                {
                    Console.WriteLine($"Vender {asset.name}");
                    email.status = "SENT_SELL";
                }
                else if (value <= asset.buy_value && email.status != "SENT_BUY")
                {
                    Console.WriteLine($"Comprar {asset.name}");
                    email.status = "SENT_BUY";
                }
                else if (value < asset.sell_value && value > asset.buy_value)
                {   
                    if (email.status != "NONSENT")
                    {
                        Console.WriteLine($"O valor da ação {asset.name} não corresponde mais ao desejado.");
                        email.status = "NONSENT";
                    }
                    else
                    {
                        Console.WriteLine($"Não fazer nada com {asset.name}");
                    }
                }
                Thread.Sleep(10000);
            }
            
        }
    }
}