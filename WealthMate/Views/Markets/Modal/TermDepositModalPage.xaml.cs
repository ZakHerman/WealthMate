using System;
using WealthMate.Helpers;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views.Markets.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermDepositModalPage
    {
        public TermDeposit TermDeposit { get; set; }

        public TermDepositModalPage(TermDeposit termDeposit)
        {
            TermDeposit = termDeposit;
            BindingContext = termDeposit;

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
                await DisplayAlert(null, "Please enter invested amount!", "OK");
            else
            {
                var asset = new OwnedAsset(TermDeposit.Provider, date, "Term Deposit", investment, TermDeposit.InterestRate,
                    TermDeposit.LengthInMonths, GetInvestmentCompoundRate(), 0, goal);
                ((App)Application.Current).User.Portfolio.AddAsset(asset);

                Helper.DisplayToastNotification("Added to portfolio");

                await Navigation.PopModalAsync();
            }
        }

        private int GetInvestmentCompoundRate()
        {
            var index = InterestDropdown.SelectedIndex;

            switch (index)
            {
                case 0:
                    return 12;
                case 1:
                    return 6;
                case 2:
                    return 3;
                case 3:
                    return 1;
                default :
                    return 0;
            }
        }
    }
}