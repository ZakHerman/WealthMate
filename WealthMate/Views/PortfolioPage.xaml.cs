using System.ComponentModel;
using System.Collections.ObjectModel;
using WealthMate.Models;
using Xamarin.Forms;
using System.Collections.Generic;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace WealthMate.Views
{
    [DesignTimeVisible(false)]
    public partial class PortfolioPage
    {
        public float TotalValue { get; set; }
        public Portfolio CurrentPortfolio { get; set; }
        public List<OwnedAsset> OwnedAssets { get; set; }
        public ObservableCollection<PieData> pieChart { get; set; }
        private PieData _termD = new PieData();
        private PieData _bond = new PieData();
        private PieData _stock = new PieData();

        public PortfolioPage()
        {
            TotalValue = 634635.5623f;
            CurrentPortfolio = (Application.Current as App).User.Portfolio;
            OwnedAssets = CurrentPortfolio.OwnedAssets;
            pieChart = new ObservableCollection<PieData>();
            SetData();
            BindingContext = this;
            InitializeComponent();

            NavBarLayout.Children.Add(
                NavBarTitle,
                // Center the text of the titleview
                new Rectangle(0.5, 0.5, 0.9, 1),
                AbsoluteLayoutFlags.All

            );
        }

        //method sorts assets within portfolio into types and assigns a total value 
        public void SetData()
        {
            foreach (OwnedAsset asset in CurrentPortfolio.OwnedAssets)
            {
                if (asset.Type.Equals("Term Deposit"))
                {
                    this._termD.AssetType = "Term Deposits";
                    this._termD.Quantity += asset.CurrentValue;
                    this._termD.ReturnPercentage = asset.CurrentValue / asset.TotalReturn;
                }
                if (asset.Type.Equals("Bond"))
                {
                    this._bond.AssetType = "Bonds";
                    this._bond.Quantity += asset.CurrentValue;
                    this._bond.ReturnPercentage = asset.CurrentValue / asset.TotalReturn;
                }
                if (asset is OwnedStock)
                {
                    this._stock.AssetType = "Stocks";
                    this._stock.Quantity += asset.CurrentValue; //asset.CurrentValue; NOT WORKING CURRENTLY
                    this._stock.ReturnPercentage = asset.CurrentValue / asset.TotalReturn;

                }
                // insert more code for any other possible types      
            }

            pieChart.Add(_bond);
            pieChart.Add(_termD);
            pieChart.Add(_stock);
        }
    }
}