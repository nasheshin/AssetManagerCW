using System;

namespace AssetManager.AssetControls
{
    public partial class PortfolioControl
    {
        public PortfolioControl()
        {
            InitializeComponent();

            var sellAssetControl = (SellAssetControl)FindName("SellAssetControl");
            if (sellAssetControl == null)
                throw new Exception();

            var buyAssetControl = (BuyAssetControl)FindName("BuyAssetControl");
            if (buyAssetControl == null)
                throw new Exception();
            
            var sellAssetControlVm = new SellAssetControlVm();
            var buyAssetControlVm = new BuyAssetControlVm();
            
            sellAssetControl.DataContext = sellAssetControlVm;
            buyAssetControl.DataContext = buyAssetControlVm;
            
            DataContext = new PortfolioControlVm(sellAssetControlVm, buyAssetControlVm);
        }
    }
}