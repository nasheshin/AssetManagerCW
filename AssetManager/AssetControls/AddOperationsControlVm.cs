using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AssetManager.Annotations;
using AssetManager.Models;
using AssetManager.Utils;

namespace AssetManager.AssetControls
{
    public class AddOperationsControlVm : INotifyPropertyChanged
    {
        private readonly DataContext _database;
        
        private string _assetName;
        private string _assetTicker;
        private string _assetType;
        private DateTime _datetime;
        private float _price;
        private string _brokerName;
        private int _count;
        
        private RelayCommand _addOperationsCommand;

        public AddOperationsControlVm()
        {
            _database = new DataContext();
            Datetime = DateTime.Now;
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
        public DateTime Datetime
        {
            get => _datetime;
            set
            {
                _datetime = value;
                OnPropertyChanged(nameof(Datetime));
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
        public string BrokerName
        {
            get => _brokerName;
            set
            {
                _brokerName = value;
                OnPropertyChanged(nameof(BrokerName));
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
        
        public RelayCommand AddOperationsCommand
        {
            get
            {
                return _addOperationsCommand ?? (_addOperationsCommand = new RelayCommand(obj => AddOperations()));
            }
        }

        private void AddOperations()
        {
            var assetAnalyticId = _database.AssetAnalytics.ToList().FirstOrDefault(analytic =>
                string.Equals(analytic.AssetName, AssetName, StringComparison.CurrentCultureIgnoreCase))?.Id ?? 3;
            var broker = _database.Brokers.ToList().FirstOrDefault(curBroker => curBroker.Name == BrokerName);
            if (broker == null)
            {
                _database.Brokers.Add(new Broker {Name = BrokerName});
                _database.SaveChanges();
            }


            var brokerId = broker?.Id ?? _database.Brokers.ToList().Last().Id;

            for (var i = 0; i < Count; i++)
            {
                _database.Operations.Add(new Operation
                {
                    AssetName = AssetName, AssetTicker = AssetTicker, AssetType = AssetType, Datetime = Datetime,
                    Type = 1, Price = Price, UserId = SessionInfo.UserId, AssetAnalyticId = assetAnalyticId,
                    BrokerId = brokerId
                });
            }
            
            _database.SaveChanges();
            
            OnUpdatedDatabase();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public delegate void DatabaseUpdatedEventHandler();
        public event DatabaseUpdatedEventHandler UpdatedDatabase;
        private void OnUpdatedDatabase()
        {
            UpdatedDatabase?.Invoke();
        }
    }
}