using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AssetManager.Annotations;
using AssetManager.DataUtils;
using AssetManager.Models;
using AssetManager.Utils;

namespace AssetManager.AssetControls
{
    public class SellAssetControlVm : INotifyPropertyChanged
    {
        private readonly DataProcessorOperations _dataProcessorOperations;

        private float _price;
        private int _count;
        private string _assetInfo;

        private Operation _operationSample;
        private string _datetime;
        private RelayCommand _sellAssetCommand;

        public SellAssetControlVm()
        {
            _dataProcessorOperations = App.DataProcessorOperations;
        }

        public string AssetInfo
        {
            get => _assetInfo;
            set
            {
                _assetInfo = value;
                OnPropertyChanged(nameof(AssetInfo));
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
        
        public RelayCommand SellAssetCommand
        {
            get
            {
                return _sellAssetCommand ?? (_sellAssetCommand = new RelayCommand(obj => SellAsset()));
            }
        }

        public void SetControl(PortfolioElementView portfolioElement)
        {
            _operationSample = _dataProcessorOperations.Operations.FirstOrDefault(op => op.Id == portfolioElement.Id);

            AssetInfo = portfolioElement.AssetName;
            Datetime = DateTime.Now.ToString("dd-MM-yyyy");
            Price = 100;
            Count = 1;
        }

        private void SellAsset()
        {
            var operationToAdd = (Operation)_operationSample.Clone();
            operationToAdd.Datetime = DateTime.Parse(Datetime);
            operationToAdd.Price = Price;
            operationToAdd.Type = -1;

            for (var i = 0; i < Count; i++)
                _dataProcessorOperations.AddElement(operationToAdd);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}