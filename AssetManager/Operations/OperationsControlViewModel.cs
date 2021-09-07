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

namespace AssetManager.Operations
{
    public sealed class OperationsControlViewModel : INotifyPropertyChanged
    {
        private readonly DataProcessorOperations _dataProcessorOperations;
        private readonly DataContext _database;
        private readonly List<Broker> _brokers;
        private readonly List<AssetAnalytic> _assetAnalytics;
        
        private readonly ObservableCollection<OperationView> _operationsView;
        private OperationView _selectedOperation;
        private RelayCommand _copyOperationCommand;
        private RelayCommand _removeOperationCommand;

        public OperationsControlViewModel()
        {
            _dataProcessorOperations = MainWindow.DataProcessorOperations;
            _dataProcessorOperations.DatabaseChanged += UpdateData;
            _database = new DataContext();
            _brokers = _database.Brokers.ToList();
            _assetAnalytics = _database.AssetAnalytics.ToList();

            var userOperations =
                _database.Operations.ToList().Where(operation => operation.UserId == SessionInfo.UserId);
            _operationsView = new ObservableCollection<OperationView>(userOperations.Select(ConvertOperation));
        }

        public ObservableCollection<OperationView> OperationsView => _operationsView;

        public OperationView SelectedOperation
        {
            get => _selectedOperation;
            set
            {
                _selectedOperation = value;
                OnPropertyChanged(nameof(SelectedOperation));
            }
        }

        public RelayCommand CopyOperationCommand
        {
            get
            {
                return _copyOperationCommand ?? (_copyOperationCommand = new RelayCommand(obj => CopyOperation()));
            }
        }
        
        public RelayCommand RemoveOperationCommand
        {
            get
            {
                return _removeOperationCommand ?? (_removeOperationCommand = new RelayCommand(obj => RemoveOperation()));
            }
        }

        private void CopyOperation()
        {
            var operations = _database.Operations.ToList();
            var operationCopy = operations.FirstOrDefault(operation => operation.Id == SelectedOperation.Id);
            
            if (operationCopy == null)
                return;

            _dataProcessorOperations.AddElement(operationCopy);
        }
        
        private void RemoveOperation()
        {
            var operations = _database.Operations.ToList();
            var operationRemove = operations.FirstOrDefault(operation => operation.Id == SelectedOperation.Id);

            if (operationRemove == null)
                return;
            
            _dataProcessorOperations.RemoveElement(operationRemove);
        }
            
        private OperationView ConvertOperation(Operation operation)
        {
            var typeName = operation.Type == 1 ? "Покупка" : "Продажа";
            var brokerName = _brokers.FirstOrDefault(broker => broker.Id == operation.BrokerId)?.Name;
            var buyRate = _assetAnalytics.FirstOrDefault(analytic => analytic.Id == operation.AssetAnalyticId)?.StringBuyRate;
            var sellRate = _assetAnalytics.FirstOrDefault(analytic => analytic.Id == operation.AssetAnalyticId)?.StringSellRate;

            return new OperationView{
                Id = operation.Id, AssetName = operation.AssetName, AssetTicker = operation.AssetTicker,
                AssetType = operation.AssetType, Datetime = operation.Datetime, Type = typeName, Price = operation.Price,
                BrokerName = brokerName, BuyRate = buyRate, SellRate = sellRate
            };
        }
        
        private void UpdateData(object sender, EventArgs e)
        {
            if (!(e is OperationEventArgs commandInfo))
                return;

            if (commandInfo.CommandType == OperationCommandType.Add)
            {
                _operationsView.Add(ConvertOperation(commandInfo.Operation));
            }
            else
            {
                var removedOperationView =
                    _operationsView.FirstOrDefault(operation => operation.Id == commandInfo.Operation.Id);
                _operationsView.Remove(removedOperationView);
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