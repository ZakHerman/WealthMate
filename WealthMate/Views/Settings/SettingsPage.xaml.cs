using System;
using System.Diagnostics;
using Syncfusion.DataSource.Extensions;
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
        public SettingsPage()
        {
            InitializeComponent();
            Themes.DataSource = Enum.GetValues(typeof(Theme)).ToList<Enum>();
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
    }
}