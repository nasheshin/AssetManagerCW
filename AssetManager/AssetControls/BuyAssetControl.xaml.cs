using System;
using System.Windows.Controls;

namespace AssetManager.AssetControls
{
    public partial class BuyAssetControl
    {
        public BuyAssetControl()
        {
            InitializeComponent();
        }

        private void OnDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDate = ((DatePicker) sender).SelectedDate;
            if (selectedDate == null) 
                return;
            if (!(DataContext is BuyAssetControlVm viewModel))
                return;

            var datetime = (DateTime)selectedDate;
            viewModel.Datetime = datetime.ToString("dd-MM-yyyy");
        }
    }
}