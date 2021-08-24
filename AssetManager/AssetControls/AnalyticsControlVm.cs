﻿using System.Collections.ObjectModel;
using System.Linq;
using AssetManager.Models;

namespace AssetManager.AssetControls
{
    public class AnalyticsControlVm
    {
        private readonly ObservableCollection<object> _analyticsView;
        
        public AnalyticsControlVm()
        {
            var database = new DataContext();
            var assetAnalytics = database.AssetAnalytics.ToList();
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