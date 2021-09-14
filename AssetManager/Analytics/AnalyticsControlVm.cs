using System.Collections.ObjectModel;
using System.Linq;
using AssetManager.Models;

namespace AssetManager.Analytics
{
    public class AnalyticsControlVm
    {
        private readonly ObservableCollection<object> _analyticsView;
        
        public AnalyticsControlVm()
        {
            var assetAnalytics = App.DataProcessorAnalytics.AssetAnalytics;
            var convertedAnalytics = assetAnalytics.Where(analytic => analytic.Id != 3).Select(analytic => new
            {
                AssetName = analytic.AssetName, BuyRate = analytic.StringBuyRate, 
                SellRate = analytic.StringSellRate, Id = analytic.Id
            });

            _analyticsView = new ObservableCollection<object>(convertedAnalytics);
        }

        public ObservableCollection<object> AnalyticsView => _analyticsView;
    }
}