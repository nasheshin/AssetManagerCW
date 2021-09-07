using System;

namespace AssetManager.Operations
{
    public class OperationView
    {
        public int Id { get; set; }
        public string AssetName { get; set; }
        public string AssetTicker { get; set; }
        public string AssetType { get; set; }
        public DateTime Datetime { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public string BrokerName { get; set; }
        public string BuyRate { get; set; }
        public string SellRate { get; set; }
    }
}