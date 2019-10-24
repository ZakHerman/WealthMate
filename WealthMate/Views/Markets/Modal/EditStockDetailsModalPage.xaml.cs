using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMate.Helpers;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views.Markets.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditStockDetailsModalPage : ContentPage
    {
        public OwnedStock OwnedStock { get; set; }
        public EditStockDetailsModalPage(OwnedStock os)
        {
            OwnedStock = os;
            BindingContext = this;
            InitializeComponent();
        }

        private async void CancelButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void SaveButton_OnClicked(object sender, EventArgs e)
        {
            int.TryParse(SharesEntry.Text, out var shares);
            float.TryParse(PriceEntry.Text, out var price);
            float.TryParse(GoalEntry.Text, out var goal);
            var date = PurchaseDate.Date;

     
            OwnedStock.EditStock(shares, price, goal, date);
            ((App)Application.Current).User.Portfolio.UpdatePortfolio();
            Helper.DisplayToastNotification("Owned Stock Saved!");
            await Navigation.PopModalAsync();
        }
    }
}