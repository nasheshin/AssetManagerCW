namespace AssetManager.AssetControls
{
    public class PortfolioElementView
    {
        public int? Id { get; set; }
        public string AssetName { get; set; }
        public string AssetTicker { get; set; }
        public string AssetType { get; set; }
        public string BrokerName { get; set; }
        public int Count { get; set; }
        public string BuyRate { get; set; }
        public string SellRate { get; set; }
    }
}