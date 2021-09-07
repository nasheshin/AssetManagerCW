using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AssetManager.Annotations;
using AssetManager.DataUtils;
using AssetManager.Models;
using AssetManager.Utils;

namespace AssetManager.AssetControls
{
    public class PortfolioControlVm : INotifyPropertyChanged
    {
        private readonly DataProcessorOperations _dataProcessorOperations;
        private readonly DataContext _database;
        private readonly SellAssetControlVm _sellAssetControlVm;
        private readonly BuyAssetControlVm _buyAssetControlVm;
        
        private List<Operation> _operations;
        private List<Broker> _brokers;
        private List<AssetAnalytic> _assetAnalytics;
        
        private readonly ObservableCollection<PortfolioElementView> _portfolioView;
        private PortfolioElementView _selectedPortfolioElement;
        
        public PortfolioControlVm(SellAssetControlVm sellAssetControlVm, BuyAssetControlVm buyAssetControlVm)
        {
            _dataProcessorOperations = MainWindow.DataProcessorOperations;
            _dataProcessorOperations.DatabaseChanged += UpdateData;
            _database = new DataContext();
            _sellAssetControlVm = sellAssetControlVm;
            _buyAssetControlVm = buyAssetControlVm;
            
            _operations = _database.Operations.ToList();
            _brokers = _database.Brokers.ToList();
            _assetAnalytics = _database.AssetAnalytics.ToList();
            
            var userOperations = _operations.Where(operation => operation.UserId == SessionInfo.UserId).ToList();
            var groupedOperations = userOperations.GroupBy(operation =>
                (operation.AssetName, operation.AssetType, operation.AssetTicker, operation.BrokerId)).ToList();
            
            var portfolio = groupedOperations.Select(g => new PortfolioElementView
            {
                AssetName = g.Key.Item1, AssetTicker = g.Key.Item3, AssetType = g.Key.Item2,
                BrokerName = _brokers.FirstOrDefault(broker => broker.Id == g.FirstOrDefault()?.BrokerId)?.Name,
                Count = g.Sum(o => o.Type),
                BuyRate = _assetAnalytics.FirstOrDefault(analytic => analytic.Id == g.First().AssetAnalyticId)?.StringBuyRate,
                SellRate = _assetAnalytics.FirstOrDefault(analytic => analytic.Id == g.First().AssetAnalyticId)?.StringSellRate,
                Id = g.FirstOrDefault()?.Id
            }).Where(elem => elem.Count > 0).ToList();

            _portfolioView = new ObservableCollection<PortfolioElementView>(portfolio);
            SelectedPortfolioElement = _portfolioView.First();
        }

        public ObservableCollection<PortfolioElementView> PortfolioView => _portfolioView;
        
        public PortfolioElementView SelectedPortfolioElement
        {
            get => _selectedPortfolioElement;
            set
            {
                _selectedPortfolioElement = value;
                _sellAssetControlVm.SetControl(value);
                _buyAssetControlVm.SetControl(value);
                
                OnPropertyChanged(nameof(SelectedPortfolioElement));
            }
        }
        
        private PortfolioElementView ConvertOperation(Operation operation)
        {
            var brokerName = _brokers.FirstOrDefault(broker => broker.Id == operation.BrokerId)?.Name;
            var buyRate = _assetAnalytics.FirstOrDefault(analytic => analytic.Id == operation.AssetAnalyticId)?.StringBuyRate;
            var sellRate = _assetAnalytics.FirstOrDefault(analytic => analytic.Id == operation.AssetAnalyticId)?.StringSellRate;

            return new PortfolioElementView{
                Id = operation.Id, AssetName = operation.AssetName, AssetTicker = operation.AssetTicker,
                BrokerName = brokerName, AssetType = operation.AssetType, Count = 1, BuyRate = buyRate, 
                SellRate = sellRate
            };
        }

        private void UpdateData(object sender, EventArgs e)
        {
            if (!(e is OperationEventArgs commandInfo))
                return;

            var sameAssetName = _portfolioView.Any(op => op.AssetName == commandInfo.Operation.AssetName);
            var sameAssetTicker = _portfolioView.Any(op => op.AssetTicker == commandInfo.Operation.AssetTicker);
            var sameAssetType = _portfolioView.Any(op => op.AssetType == commandInfo.Operation.AssetType);
            
            var brokerName = _database.Brokers.FirstOrDefault(br => br.Id == commandInfo.Operation.BrokerId)?.Name;
            if (brokerName == null)
                return;
            
            var sameBrokerName = _portfolioView.Any(op => op.BrokerName == brokerName);

            var isOperationAdded = commandInfo.CommandType == OperationCommandType.Add;
            var isOperationTypeBuy = commandInfo.Operation.Type == 1;
            var sameElement = sameAssetName && sameAssetTicker && sameAssetType && sameBrokerName;

            switch (isOperationAdded)
            {
                case false when !sameElement:
                    throw new Exception("There is a try to remove an element which don't exist");
                case true when !sameElement:
                    _portfolioView.Add(ConvertOperation(commandInfo.Operation));
                    return;
            }

            var portfolioElementView =
                _portfolioView.First(op => op.AssetName == commandInfo.Operation.AssetName);
            _portfolioView.Remove(portfolioElementView);
            portfolioElementView.Count += isOperationAdded == isOperationTypeBuy ? 1 : -1;
            _portfolioView.Add(portfolioElementView);
            
            if (portfolioElementView.Count <= 0)
                _portfolioView.Remove(portfolioElementView);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}