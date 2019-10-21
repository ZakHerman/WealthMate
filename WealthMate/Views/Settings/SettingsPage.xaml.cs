using Syncfusion.SfNumericTextBox.XForms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage
    {
        private SfNumericTextBox editPortfolioReturnGoal;
        public SettingsPage()
        {
            InitializeComponent();

            editPortfolioReturnGoal = new SfNumericTextBox { Value = 0 };
            editPortfolioReturnGoal.ValueChanged += Handle_PortfolioGoalChanged;

        }

        private async void OnThemeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ThemeSelectionPage());
        }

        private void PortfolioGoal_Clicked(object sender, EventArgs e)
        {
            PortfolioGoalForm.IsOpen = true;
        }

        private void Handle_PortfolioGoalChanged(object sender, Syncfusion.SfNumericTextBox.XForms.ValueEventArgs e)
        {
            editPortfolioReturnGoal.Value = e.Value.ToString();
        }

        protected void SaveInPopupClicked(object sender, EventArgs args)
        {
            PortfolioGoalForm.IsOpen = false;

            var newPortfolioGoal = float.Parse(editPortfolioReturnGoal.Value.ToString());

            ((App)Application.Current).User.Portfolio.EditPortfolioGoal(newPortfolioGoal);
        }
    }
}