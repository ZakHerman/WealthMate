using System;
using System.Diagnostics;
using Syncfusion.DataSource.Extensions;
using Syncfusion.SfNumericTextBox.XForms;
using Syncfusion.XForms.ComboBox;
using WealthMate.Themes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SelectionChangedEventArgs = Syncfusion.XForms.ComboBox.SelectionChangedEventArgs;

namespace WealthMate.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage
    {
        private SfNumericTextBox editPortfolioReturnGoal;

        public SettingsPage()
        {
            InitializeComponent();
            Themes.DataSource = Enum.GetValues(typeof(Theme)).ToList<Enum>();

            editPortfolioReturnGoal = new SfNumericTextBox { Value = 0 };
            editPortfolioReturnGoal.ValueChanged += Handle_PortfolioGoalChanged;
        }

        private void OnThemeClicked(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender is SfComboBox themes))
                return;

            Enum.TryParse(themes.SelectedItem.ToString(), out Theme theme);

            //var theme = (Theme)themes.SelectedItem;
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;

            if (mergedDictionaries == null)
                return;

            mergedDictionaries.Clear();

            switch (theme)
            {
                case Theme.Dark:
                    mergedDictionaries.Add(new DarkTheme());
                    ((App)Application.Current).ThemeChanger.ApplyTheme(Theme.Dark);
                    break;
                default:
                    mergedDictionaries.Add(new LightTheme());
                    ((App)Application.Current).ThemeChanger.ApplyTheme(Theme.Light);
                    break;
            }
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