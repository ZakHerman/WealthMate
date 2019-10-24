using System;
using Syncfusion.DataSource.Extensions;
using Syncfusion.SfNumericTextBox.XForms;
using Syncfusion.XForms.ComboBox;
using WealthMate.Helpers;
using WealthMate.Views.Markets.Modal;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SelectionChangedEventArgs = Syncfusion.XForms.ComboBox.SelectionChangedEventArgs;

namespace WealthMate.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            Themes.DataSource = Enum.GetValues(typeof(Theme)).ToList<Enum>();
        }

        private void OnThemeClicked(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is SfComboBox themes))
                return;

            Helper.SetTheme(themes.SelectedItem.ToString());
        }

        private void PortfolioGoal_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PortfolioReturnGoalModalPage());
        }
    }
}