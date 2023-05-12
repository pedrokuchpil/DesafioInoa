namespace Broker
{
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

            Asset asset = new Asset (name, sell_value, buy_value);

            var email = new Email("stmpconfig.json");

            string status = "NONSENT";

            CallAPI call = new CallAPI();
            while(true)
            {     
                double value = await call.checkValue(asset);

                if (value >= asset.sell_value && status != "SENT_SELL")
                {
                    Console.WriteLine($"Vender {asset.name}");
                    email.sendEmail("Venda de ação", $"Venda {asset.name} ao preço de {value}");
                    status = "SENT_SELL";
                }
                else if (value <= asset.buy_value && status != "SENT_BUY")
                {
                    Console.WriteLine($"Comprar {asset.name}");
                    email.sendEmail("Compra de ação", $"Compre {asset.name} ao preço de {value}");
                    status = "SENT_BUY";
                }
                else if (value < asset.sell_value && value > asset.buy_value)
                {   
                    if (status != "NONSENT")
                    {
                        Console.WriteLine($"O valor da ação {asset.name} não corresponde mais ao desejado.");
                        email.sendEmail($"O valor de {asset.name} mudou", $"O valor de {asset.name} mudou e não corresponde mais ao desejado.");
                        status = "NONSENT";
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