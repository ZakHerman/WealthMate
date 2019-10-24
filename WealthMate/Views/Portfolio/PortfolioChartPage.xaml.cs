using Syncfusion.XForms.ComboBox;
using WealthMate.Models;
using WealthMate.ViewModels;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views.Portfolio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortfolioChartPage
    {
        public PortfolioChartPage()
        {
            InitializeComponent();
            BindingContext = new PortfolioViewModel();
            //picker.SelectedIndex = 0;
        }

        private void SfComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = BindingContext as PortfolioViewModel;
            var index = ((SfComboBox)sender).SelectedIndex;

            switch (index)
            {
                case 0:
                    vm?.pieChart.Clear();
                    PieData termDCat = new PieData("Term Deposits"); //Pie data for each section/category of pie chart
                    PieData stockCat = new PieData("Stocks");

                    foreach (var asset in vm?.CurrentPortfolio.OwnedAssets)
                    {
                        asset.UpdateOwnedAsset();

                        if (asset.Type.Equals("Term Deposit"))
                            termDCat.UpdateValues(asset.CurrentValue, asset.PrincipalValue);
                        else if (asset is OwnedStock)
                            stockCat.UpdateValues(asset.CurrentValue, asset.PrincipalValue);
                    }

                    termDCat.CalculateReturnPercentage();
                    stockCat.CalculateReturnPercentage();

                    // XAML Flag to see if label should be red or green (negative/positive returns)
                    termDCat.PositiveChecker();
                    stockCat.PositiveChecker();

                    // Adds asset categories to Pie chart
                    vm?.pieChart.Add(termDCat);
                    vm?.pieChart.Add(stockCat);

                    break;
                case 1:
                    vm?.pieChart.Clear();
                    foreach (var asset in vm?.CurrentPortfolio.OwnedAssets)
                    {
                        if (asset.Type.Equals("Term Deposit"))
                        {
                            asset.UpdateOwnedAsset();
                            var termD = new PieData(asset.AssetName);

                            termD.UpdateValues(asset.CurrentValue, asset.PrincipalValue);
                            termD.CalculateReturnPercentage();
                            termD.PositiveChecker();

                            vm?.pieChart.Add(termD);
                        }
                    }
                    break;
                case 2:
                    vm?.pieChart.Clear();
                    foreach (var asset in vm?.CurrentPortfolio.OwnedAssets)
                    {
                        if (asset is OwnedStock)
                        {
                            asset.UpdateOwnedAsset();
                            var stock = new PieData(asset.AssetName);

                            stock.UpdateValues(asset.CurrentValue, asset.PrincipalValue);
                            stock.CalculateReturnPercentage();
                            stock.PositiveChecker();

                            vm?.pieChart.Add(stock);
                        }
                    }
                    break;
            }
        }
    }
}