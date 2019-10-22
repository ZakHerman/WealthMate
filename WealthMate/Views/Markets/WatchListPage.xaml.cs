using System.Linq;
using Syncfusion.ListView.XForms;
using WealthMate.Models;
using WealthMate.ViewModels;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace WealthMate.Views.Markets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchlistPage
    {
        public WatchlistPage()
        {
            InitializeComponent();
        }

        // Event handler for watchlist stock being pressed
        private async void WatchlistView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
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
    }
}