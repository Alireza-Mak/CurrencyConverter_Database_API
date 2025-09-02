namespace CurrencyConverter_Database_API
{
    public class Root
    {
        public string license { get; set; }
        public Rate rates { get; set; }
        public long timestamp { get; set; }
    }
}
