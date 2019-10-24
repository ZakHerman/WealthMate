using Syncfusion.XForms.ComboBox;
using System;
using WealthMate.Helpers;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SelectionChangedEventArgs = Syncfusion.XForms.ComboBox.SelectionChangedEventArgs;

namespace WealthMate.Views.Markets.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermDepositModalPage
    {
        public TermDeposit TermDeposit { get; set; }
        public int _compundRate;

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
                var asset = new OwnedAsset(TermDeposit.Provider, date, "Term Deposit", investment, (TermDeposit.InterestRate / 100),
                    TermDeposit.LengthInMonths, _compundRate, 0, goal);
                ((App)Application.Current).User.Portfolio.AddAsset(asset);

                Helper.DisplayToastNotification("Added to portfolio");

                await Navigation.PopModalAsync();
            }
        }

        private void SfComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = ((SfComboBox)sender).SelectedIndex;

            switch (index)
            {
                case 0:
                    _compundRate = 1;
                    break;
                case 1:
                    _compundRate = 2;
                    break;
                case 2:
                    _compundRate = 4;
                    break;
                case 3:
                    _compundRate = 12;
                    break;
                default:
                    _compundRate = 0;
                    break;
            }
        }
    }
}