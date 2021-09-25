using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using AssetManager.Annotations;
using AssetManager.DataUtils;
using AssetManager.Models;
using AssetManager.Utils;

namespace AssetManager.Operations
{
    public sealed class OperationsControlViewModel : INotifyPropertyChanged
    {
        private readonly DataProcessorOperations _dataProcessorOperations;
        private readonly DataProcessorBrokers _dataProcessorBrokers;
        private readonly DataProcessorAnalytics _dataProcessorAnalytics;

        private readonly ObservableCollection<OperationView> _operationsView;
        private OperationView _selectedOperation;
        private RelayCommand _copyOperationCommand;
        private RelayCommand _removeOperationCommand;

        public OperationsControlViewModel()
        {
            _dataProcessorOperations = App.DataProcessorOperations;
            _dataProcessorBrokers = App.DataProcessorBrokers;
            _dataProcessorAnalytics = App.DataProcessorAnalytics;
            
            _dataProcessorOperations.DatabaseChanged += UpdateData;
            
            var userOperations = _dataProcessorOperations.Operations;
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
            try
            {
                var operations = _dataProcessorOperations.Operations;
                var operationCopy = operations.FirstOrDefault(operation => operation.Id == SelectedOperation.Id);

                if (operationCopy == null)
                    return;

                _dataProcessorOperations.AddElement(operationCopy);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                MessageBox.Show(Localization.Error.Standard);
            }
        }
        
        private void RemoveOperation()
        {
            try
            {
                var operations = _dataProcessorOperations.Operations;
                var operationRemove = operations.FirstOrDefault(operation => operation.Id == SelectedOperation.Id);

                if (operationRemove == null)
                    return;
                
                _dataProcessorOperations.RemoveElement(operationRemove);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                MessageBox.Show(Localization.Error.Standard);
            }
        }
            
        private OperationView ConvertOperation(Operation operation)
        {
            var typeName = operation.Type == 1 ? "Покупка" : "Продажа";
            var brokerName = _dataProcessorBrokers.Brokers.FirstOrDefault(br => br.Id == operation.BrokerId)?.Name;
            var analytic = _dataProcessorAnalytics.AssetAnalytics.FirstOrDefault(a => a.Id == operation.AssetAnalyticId);
            var buyRate = analytic?.StringBuyRate;
            var sellRate = analytic?.StringSellRate;

            return new OperationView{
                Id = operation.Id, AssetName = operation.AssetName, AssetTicker = operation.AssetTicker,
                AssetType = operation.AssetType, Datetime = operation.Datetime, Type = typeName, Price = operation.Price,
                BrokerName = brokerName, BuyRate = buyRate, SellRate = sellRate
            };
        }
        
        private void UpdateData(object sender, EventArgs e)
        {
            try
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