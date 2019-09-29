using System;
using System.Threading.Tasks;
using WealthMate.Themes;
using Xamarin.Forms;

namespace WealthMate.Views
{
    public partial class ThemeSelectionPage
    {
        public ThemeSelectionPage()
        {
            InitializeComponent();
        }

        private void OnPickerSelectionChanged(object sender, EventArgs e)
        {
            if (!(sender is Picker picker))
                return;

            var theme = (Theme)picker.SelectedItem;
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;

            if (mergedDictionaries == null)
                return;

            mergedDictionaries.Clear();

            switch (theme)
            {
                case Theme.Dark:
                    mergedDictionaries.Add(new DarkTheme());
                    break;
                case Theme.Light:
                default:
                    mergedDictionaries.Add(new LightTheme());
                    break;
            }
            StatusLabel.Text = $"{theme.ToString()} theme loaded.";
        }

        public async Task Dismiss()
        {
            await Navigation.PopModalAsync();
        }
    }
}