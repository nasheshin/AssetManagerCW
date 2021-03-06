using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using AssetManager.Annotations;
using AssetManager.DataUtils;
using AssetManager.Models;
using AssetManager.Utils;

namespace AssetManager.AssetControls
{
    public sealed class BuyAssetControlVm : INotifyPropertyChanged
    {
        private readonly DataProcessorOperations _dataProcessorOperations;
        private readonly DataProcessorBrokers _dataProcessorBrokers;
        private readonly DataProcessorAnalytics _dataProcessorAnalytics;

        private float _price;
        private int _count;
        private string _assetName;
        private string _assetTicker;

        private Operation _operationSample;
        private string _datetime;
        private RelayCommand _buyAssetCommand;
        private string _assetType;
        private string _brokerName;

        public BuyAssetControlVm()
        {
            _dataProcessorOperations = App.DataProcessorOperations;
            _dataProcessorBrokers = App.DataProcessorBrokers;
            _dataProcessorAnalytics = App.DataProcessorAnalytics;
        }

        public string AssetName
        {
            get => _assetName;
            set
            {
                _assetName = value;
                OnPropertyChanged(nameof(AssetName));
            }
        }
        
        public string AssetTicker
        {
            get => _assetTicker;
            set
            {
                _assetTicker = value;
                OnPropertyChanged(nameof(AssetTicker));
            }
        }
        
        public string AssetType
        {
            get => _assetType;
            set
            {
                _assetType = value;
                OnPropertyChanged(nameof(AssetType));
            }
        }
        
        public string BrokerName
        {
            get => _brokerName;
            set
            {
                _brokerName = value;
                OnPropertyChanged(nameof(BrokerName));
            }
        }
        
        public float Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public string Datetime
        {
            get => _datetime;
            set
            {
                _datetime = value;
                OnPropertyChanged(nameof(Datetime));
            }
        }
        
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged(nameof(Count));
            }
        }
        
        public RelayCommand BuyAssetCommand
        {
            get
            {
                return _buyAssetCommand ?? (_buyAssetCommand = new RelayCommand(obj => BuyAsset()));
            }
        }

        public void SetControl(PortfolioElementView portfolioElement)
        {
            _operationSample = _dataProcessorOperations.Operations.FirstOrDefault(op => op.Id == portfolioElement.Id);

            AssetName = portfolioElement.AssetName;
            AssetTicker = portfolioElement.AssetTicker;
            AssetType = portfolioElement.AssetType;
            BrokerName = portfolioElement.BrokerName;
            Datetime = DateTime.Now.ToString("dd-MM-yyyy");
            Price = 100;
            Count = 1;
        }

        private void BuyAsset()
        {
            try
            {
                var operationToAdd = (Operation) _operationSample.Clone();
                var broker = _dataProcessorBrokers.FindBrokerByName(BrokerName);
                if (broker == null)
                {
                    _dataProcessorBrokers.AddElement(new Broker {Name = BrokerName});
                    broker = _dataProcessorBrokers.Brokers.Last();
                }

                var assetAnalytic = _dataProcessorAnalytics.FindAnalyticByName(AssetName);
                operationToAdd.AssetName = AssetName;
                operationToAdd.AssetTicker = AssetTicker;
                operationToAdd.AssetType = AssetType;
                operationToAdd.Datetime = DateTime.Parse(Datetime);
                operationToAdd.Type = 1;
                operationToAdd.Price = Price;
                operationToAdd.BrokerId = broker.Id;
                operationToAdd.AssetAnalyticId = assetAnalytic?.Id ?? 3;

                for (var i = 0; i < Count; i++)
                    _dataProcessorOperations.AddElement(operationToAdd);
            }
            catch (ValidationException)
            {
                MessageBox.Show(Localization.Message.NotCorrectOperation);
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