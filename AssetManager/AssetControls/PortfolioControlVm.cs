using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AssetManager.Models;
using AssetManager.Utils;

namespace AssetManager.AssetControls
{
    public class PortfolioControlVm
    {
        private readonly DataContext _database;
        private List<Operation> _operations;
        private List<Broker> _brokers;
        private List<AssetAnalytic> _assetAnalytics;
        
        private readonly ObservableCollection<object> _portfolioView;
        
        public PortfolioControlVm()
        {
            _database = new DataContext();
            
            _operations = _database.Operations.ToList();
            _brokers = _database.Brokers.ToList();
            _assetAnalytics = _database.AssetAnalytics.ToList();
            
            var userOperations = _operations.Where(operation => operation.UserId == SessionInfo.UserId).ToList();
            var groupedOperations = userOperations.GroupBy(operation =>
                (operation.AssetName, operation.AssetType, operation.AssetTicker, operation.BrokerId)).ToList();
            
            var portfolio = groupedOperations.Select(g => new
            {
                AssetName = g.Key.Item1, AssetTicker = g.Key.Item2, AssetType = g.Key.Item3,
                BrokerName = _brokers.FirstOrDefault(broker => broker.Id == g.FirstOrDefault()?.BrokerId)?.Name,
                Count = g.Sum(o => o.Type),
                BuyRate = _assetAnalytics.FirstOrDefault(analytic => analytic.Id == g.First().AssetAnalyticId)?.StringBuyRate,
                SellRate = _assetAnalytics.FirstOrDefault(analytic => analytic.Id == g.First().AssetAnalyticId)?.StringSellRate,
                Id = g.FirstOrDefault()?.Id
            }).ToList();

            _portfolioView = new ObservableCollection<object>(portfolio);
        }

        public ObservableCollection<object> PortfolioView => _portfolioView;
    }
}