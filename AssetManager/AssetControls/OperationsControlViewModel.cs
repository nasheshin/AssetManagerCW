using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AssetManager.Annotations;
using AssetManager.Models;
using AssetManager.Utils;

namespace AssetManager.AssetControls
{
    public sealed class OperationsControlViewModel : INotifyPropertyChanged
    {
        private readonly DataContext _database;
        private readonly List<Operation> _operations;
        private readonly List<Broker> _brokers;
        private readonly List<AssetAnalytic> _assetAnalytics;
        
        private readonly ObservableCollection<object> _operationsView;
        
        public OperationsControlViewModel()
        {
            _database = new DataContext();
            _operations = _database.Operations.ToList();
            _brokers = _database.Brokers.ToList();
            _assetAnalytics = _database.AssetAnalytics.ToList();
            
            var userOperations = _operations.Where(operation => operation.UserId == SessionInfo.UserId);
            _operationsView = new ObservableCollection<object>(userOperations.Select(ConvertOperation));
        }

        private object ConvertOperation(Operation operation)
        {
            var typeName = operation.Type == 1 ? "Покупка" : "Продажа";
            var brokerName = _brokers.FirstOrDefault(broker => broker.Id == operation.BrokerId)?.Name;
            var buyRate = _assetAnalytics.FirstOrDefault(analytic => analytic.Id == operation.AssetAnalyticId)?.StringBuyRate;
            var sellRate = _assetAnalytics.FirstOrDefault(analytic => analytic.Id == operation.AssetAnalyticId)?.StringSellRate;
            
            return new {
                AssetName = operation.AssetName, AssetTicker = operation.AssetTicker, AssetType = operation.AssetType,
                Datetime = operation.Datetime, Type = typeName, Price = operation.Price, Broker = brokerName, 
                BuyRate = buyRate, SellRate = sellRate
            };
        }

        public ObservableCollection<object> OperationsView => _operationsView;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}