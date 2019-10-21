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
            //BindingContext = new WatchListPageVM();
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

        /// <summary>
        /// Search bar functionality
        /// </summary>
        /// <param name="sender"></param> reference to object sending the data
        /// <param name="e"></param> event data
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = BindingContext as WatchListViewModel;

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                Watchlist.ItemsSource = vm?.WatchListStocks;
            else
                Watchlist.ItemsSource = vm?.WatchListStocks.Where(stock => stock.CompanyName.ToLower().Contains(e.NewTextValue.ToLower()) 
                                                                          || stock.Symbol.ToLower().Contains(e.NewTextValue.ToLower()));
        }

        private void Watchlist_OnItemDragging(object sender, ItemDraggingEventArgs e)
        {
            if (e.Action == DragAction.Start)  
            {
                //Watchlist.SelectedItem = Color.Aqua;
            }
        }
    }
}