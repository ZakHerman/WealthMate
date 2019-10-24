using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMate.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views.Markets.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortfolioReturnGoalModalPage : ContentPage
    {
        private Entry entry;
        public float CurrentReturnGoal { get; set; }
        private string newReturnGoal;
        public PortfolioReturnGoalModalPage()
        {
            CurrentReturnGoal = ((App)Application.Current).User.Portfolio.ReturnGoal;
            BindingContext = this;
            InitializeComponent();

            entry = new Entry();
            entry.TextChanged += Entry_TextChanged;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            newReturnGoal = e.NewTextValue.ToString();
        }

        private async void CancelButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void SaveButton_OnClicked(object sender, EventArgs e)
        {
            float.TryParse(newReturnGoal, out var goal);
            ((App)Application.Current).User.Portfolio.EditPortfolioGoal(goal);

            Helper.DisplayToastNotification("Return Goal Updated!");

            await Navigation.PopModalAsync();
        }
    }
}