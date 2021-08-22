namespace AssetManager.Models
{
    public class AssetAnalytic
    {
        public int Id { get; set; }
        public string AssetName { get; set; }
        public int BuyRate { get; set; }
        public int SellRate { get; set; }

        public string StringBuyRate => BuyRate == -1 ? "Неизвестно" : BuyRate.ToString();
        public string StringSellRate => SellRate == -1 ? "Неизвестно" : SellRate.ToString();
    }
}