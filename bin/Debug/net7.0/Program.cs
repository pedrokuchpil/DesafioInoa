using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Broker
{

    public class Result
    {
        public string symbol { get; set; }
        public string shortName { get; set; }
        public string longName { get; set; }
        public string? currency { get; set; }
        public double regularMarketPrice { get; set; }
        public double regularMarketDayHigh { get; set; }
        public double regularMarketDayLow { get; set; }
        public double regularMMarketDayLow { get; set; }
        public string regularMarketDayRange { get; set; }
        public double regularMarketChange { get; set; }
        public double regularMarketChangePercent { get; set; }
        public DateTime regularMarketTime { get; set; }
        public long marketCap { get; set; }
        public int regularMarketVolume { get; set; }
        public double regularMarketPreviousClose { get; set; }
        public double regularMarketOpen { get; set; }
        public int averageDailyVolume10Day { get; set; }
        public int averageDailyVolume3Month { get; set; }
        public double fiftyTwoWeekLowChange { get; set; }
        public double fiftyTwoWeekLowChangePercent { get; set; }
        public string fiftyTwoWeekRange { get; set; }
        public double fiftyTwoWeekHighChange { get; set; }
        public double fiftyTwoWeekHighChangePercent { get; set; }
        public double fiftyTwoWeekLow { get; set; }
        public double fiftyTwoWeekHigh { get; set; }
        public double twoHundredDayAverage { get; set; }
        public double twoHundredDayAverageChange { get; set; }
        public double twoHundredDayAverageChangePercent { get; set; }
    }

    public class Root
    {
        public List<Result> results { get; set; }
        public DateTime requestedAt { get; set; }
    }
    public class Asset
    {
        public string name { get; set; }
        public double sell_value { get; set; }
        public double buy_value { get; set; }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("São necessários 3 argumentos para o programa.");
                return;
            }

            string name = args[0];
            double sell_value = double.Parse(args[1]);
            double buy_value = double.Parse(args[2]);

            Asset asset = new Asset
            {
                name = name,
                sell_value = sell_value,
                buy_value = buy_value
            };

            HttpClient client = new HttpClient { BaseAddress = new Uri("https://brapi.dev/api/quote/") };

            while (true)
            {
                
                var result = await client.GetAsync(asset.name);
                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();
                    var root = JsonSerializer.Deserialize<Root>(json);
                    
                    double value = root.results[0].regularMarketPrice;

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

                    Thread.Sleep(10000);
                }
            }
        }
    }
}    