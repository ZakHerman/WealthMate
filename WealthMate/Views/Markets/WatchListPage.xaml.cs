using System.Linq;
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.ComboBox;
using WealthMate.Models;
using WealthMate.ViewModels;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;
using SelectionChangedEventArgs = Syncfusion.XForms.ComboBox.SelectionChangedEventArgs;

namespace WealthMate.Views.Markets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchlistPage
    {
        public WatchlistPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = false;
        }

        // Event handler for watchlist stock being pressed
        private async void WatchlistView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var selected = (Stock)e.ItemData;

            if (selected == null)
                return;

            // Push stock details page on top of stack
            await Navigation.PushAsync(new StockDetailsPage(selected));
  
            ((SfListView)sender).SelectedItem = null;
        }

        // Search bar functionality
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = BindingContext as WatchListViewModel;

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                Watchlist.ItemsSource = vm?.WatchListStocks;
            else
                Watchlist.ItemsSource = vm?.WatchListStocks.Where(stock => stock.CompanyName.ToLower().Contains(e.NewTextValue.ToLower()) 
                                                                           || stock.Symbol.ToLower().Contains(e.NewTextValue.ToLower()));
        }

        private void SfComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = BindingContext as WatchListViewModel;
            var index = ((SfComboBox)sender).SelectedIndex;

            switch (index)
            {
                case 0:
                    Watchlist.ItemsSource = vm?.WatchListStocks.OrderBy(stock => stock.CompanyName);
                    break;
                case 1:
                    Watchlist.ItemsSource = vm?.WatchListStocks.OrderByDescending(stock => stock.CompanyName);
                    break;
                case 2:
                    Watchlist.ItemsSource = vm?.WatchListStocks.OrderBy(stock => stock.DayReturnRate);
                    break;
                case 3:
                    Watchlist.ItemsSource = vm?.WatchListStocks.OrderByDescending(stock => stock.DayReturnRate);
                    break;
                case 4:
                    Watchlist.ItemsSource = vm?.WatchListStocks.OrderBy(stock => stock.CurrentPrice);
                    break;
                case 5:
                    Watchlist.ItemsSource = vm?.WatchListStocks.OrderByDescending(stock => stock.CurrentPrice);
                    break;
            }
        }
    }
}