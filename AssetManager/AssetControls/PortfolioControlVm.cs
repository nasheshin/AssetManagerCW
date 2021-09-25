using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using AssetManager.Annotations;
using AssetManager.DataUtils;
using AssetManager.Models;

namespace AssetManager.AssetControls
{
    public sealed class PortfolioControlVm : INotifyPropertyChanged
    {
        private readonly DataProcessorOperations _dataProcessorOperations;
        private readonly DataProcessorBrokers _dataProcessorBrokers;
        private readonly DataProcessorAnalytics _dataProcessorAnalytics;
        
        private readonly SellAssetControlVm _sellAssetControlVm;
        private readonly BuyAssetControlVm _buyAssetControlVm;

        private readonly ObservableCollection<PortfolioElementView> _portfolioViews;
        private PortfolioElementView _selectedPortfolioElement;
        
        public PortfolioControlVm(SellAssetControlVm sellAssetControlVm, BuyAssetControlVm buyAssetControlVm)
        {
            _sellAssetControlVm = sellAssetControlVm;
            _buyAssetControlVm = buyAssetControlVm;
            
            _dataProcessorOperations = App.DataProcessorOperations;
            _dataProcessorBrokers = App.DataProcessorBrokers;
            _dataProcessorAnalytics = App.DataProcessorAnalytics;
            
            _dataProcessorOperations.DatabaseChanged += UpdateData;
            
            var groupedOperations = _dataProcessorOperations.Operations.GroupBy(operation =>
                (operation.AssetName, operation.AssetType, operation.AssetTicker, operation.BrokerId)).ToList();

            var brokers = _dataProcessorBrokers.Brokers;
            var analytics = _dataProcessorAnalytics.AssetAnalytics;
            var portfolio = groupedOperations.Select(g => new PortfolioElementView
            {
                AssetName = g.Key.Item1, AssetTicker = g.Key.Item3, AssetType = g.Key.Item2,
                BrokerName = brokers.FirstOrDefault(broker => broker.Id == g.FirstOrDefault()?.BrokerId)?.Name,
                Count = g.Sum(o => o.Type),
                BuyRate = analytics.FirstOrDefault(analytic => analytic.Id == g.First().AssetAnalyticId)?.StringBuyRate,
                SellRate = analytics.FirstOrDefault(analytic => analytic.Id == g.First().AssetAnalyticId)?.StringSellRate,
                Id = g.FirstOrDefault()?.Id
            }).Where(elem => elem.Count > 0).ToList();

            _portfolioViews = new ObservableCollection<PortfolioElementView>(portfolio);
            var firstElement = _portfolioViews.FirstOrDefault();
            if (firstElement != null)
                SelectedPortfolioElement = firstElement;
        }

        public ObservableCollection<PortfolioElementView> PortfolioViews => _portfolioViews;
        
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
            var brokerName = _dataProcessorBrokers.Brokers.FirstOrDefault(broker => broker.Id == operation.BrokerId)?.Name;
            var analytic = _dataProcessorAnalytics.AssetAnalytics.FirstOrDefault(a => a.Id == operation.AssetAnalyticId);
            var buyRate = analytic?.StringBuyRate;
            var sellRate = analytic?.StringSellRate;

            return new PortfolioElementView{
                Id = operation.Id, AssetName = operation.AssetName, AssetTicker = operation.AssetTicker,
                BrokerName = brokerName, AssetType = operation.AssetType, Count = 1, BuyRate = buyRate, 
                SellRate = sellRate
            };
        }

        private void UpdateData(object sender, EventArgs e)
        {
            try
            {
                if (!(e is OperationEventArgs commandInfo))
                    return;

                var sameAssetName = _portfolioViews.Any(op => op.AssetName == commandInfo.Operation.AssetName);
                var sameAssetTicker = _portfolioViews.Any(op => op.AssetTicker == commandInfo.Operation.AssetTicker);
                var sameAssetType = _portfolioViews.Any(op => op.AssetType == commandInfo.Operation.AssetType);

                var brokerName = _dataProcessorBrokers.Brokers
                    .FirstOrDefault(br => br.Id == commandInfo.Operation.BrokerId)?.Name;
                if (brokerName == null)
                    return;

                var sameBrokerName = _portfolioViews.Any(op => op.BrokerName == brokerName);

                var isOperationAdded = commandInfo.CommandType == OperationCommandType.Add;
                var isOperationTypeBuy = commandInfo.Operation.Type == 1;
                var sameElement = sameAssetName && sameAssetTicker && sameAssetType && sameBrokerName;

                switch (isOperationAdded)
                {
                    case false when !sameElement:
                        throw new Exception("There is a try to remove an element which don't exist");
                    case true when !sameElement:
                        _portfolioViews.Add(ConvertOperation(commandInfo.Operation));
                        return;
                }

                var portfolioElementView =
                    _portfolioViews.First(op => op.AssetName == commandInfo.Operation.AssetName);
                _portfolioViews.Remove(portfolioElementView);
                portfolioElementView.Count += isOperationAdded == isOperationTypeBuy ? 1 : -1;
                _portfolioViews.Add(portfolioElementView);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                MessageBox.Show(Localization.Error.Standard);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}