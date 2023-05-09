namespace Broker
{
    public class Asset
    {
        public string name { get; set; }
        public double sell_value { get; set; }
        public double buy_value { get; set; }

        public Asset (string name, double sell_value, double buy_value)
        {
            this.name = name;
            this.sell_value = sell_value;
            this.buy_value = buy_value;
        }
    }
}    