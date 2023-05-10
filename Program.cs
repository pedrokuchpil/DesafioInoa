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

            CallAPI call = new CallAPI();
            await call.checkValue(asset);
                                
        }
    }
}    