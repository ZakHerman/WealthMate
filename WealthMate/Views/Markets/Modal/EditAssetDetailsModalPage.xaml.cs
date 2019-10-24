using Syncfusion.XForms.ComboBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMate.Helpers;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SelectionChangedEventArgs = Syncfusion.XForms.ComboBox.SelectionChangedEventArgs;

namespace WealthMate.Views.Markets.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditAssetDetailsModalPage : ContentPage
    {
        public OwnedAsset OwnedAsset { get; set; }
        public int CurrentCompoundRateIndex { get; set; }
        public int CompoundRate;
        public EditAssetDetailsModalPage(OwnedAsset oa)
        {
            OwnedAsset = oa;
            CurrentCompoundRateIndex = GetDefaultIndex(oa.CompoundRate);
            BindingContext = this;
            InitializeComponent();
        }

        private int GetDefaultIndex(int compoundRate)
        {
            switch (compoundRate)
            {
                case 1:
                    return 0;
                case 2:
                    return 1;
                case 4:
                    return 2;
                case 12:
                    return 3;
                default:
                    return 4;
            }
        }

        private async void CancelButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void SaveButton_OnClicked(object sender, EventArgs e)
        {
            float.TryParse(InvestedEntry.Text, out var investment);
            float.TryParse(GoalEntry.Text, out var goal);
            var date = PurchaseDate.Date;

            OwnedAsset.EditAsset(investment, CompoundRate, goal, date);
            ((App)Application.Current).User.Portfolio.UpdatePortfolio();

            Helper.DisplayToastNotification("Owned Asset Saved!");

            await Navigation.PopModalAsync();
        }

        private void SfComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = ((SfComboBox)sender).SelectedIndex;

            switch (index)
            {
                case 0:
                    CompoundRate = 1;
                    break;
                case 1:
                    CompoundRate = 2;
                    break;
                case 2:
                    CompoundRate = 4;
                    break;
                case 3:
                    CompoundRate = 12;
                    break;
                default:
                    CompoundRate = 0;
                    break;
            }
        }
    }
}