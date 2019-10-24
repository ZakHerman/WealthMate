using System;
using WealthMate.Helpers;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views.Portfolio.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditAssetModalPage
    {
        public OwnedAsset OwnedAsset { get; set; }

        public EditAssetModalPage(OwnedAsset asset)
        {
            OwnedAsset = asset;
            BindingContext = this;

            InitializeComponent();
        }

        private async void CancelButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void AddButton_OnClicked(object sender, EventArgs e)
        {
            float.TryParse(InvestedEntry.Text, out var investment);
            float.TryParse(GoalEntry.Text, out var goal);
            var date = PurchaseDate.Date;

            if (investment <= 0.0f)
                await DisplayAlert(null, "Please enter an investment amount above 0!", "OK");
            else
            {
                OwnedAsset.EditAsset(investment, OwnedAsset.InterestRate, GetInvestmentCompoundRate(), OwnedAsset.Length, goal);
                ((App)Application.Current).User.Portfolio.UpdatePortfolio();

                Helper.DisplayToastNotification("Edited portfolio");

                await Navigation.PopModalAsync();
            }
        }

        private int GetInvestmentCompoundRate()
        {
            var index = InterestDropdown.SelectedIndex;

            switch (index)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 2:
                    return 4;
                case 3:
                    return 12;
                default :
                    return 0;
            }
        }
    }
}