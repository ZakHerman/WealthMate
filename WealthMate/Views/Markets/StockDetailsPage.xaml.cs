using System;
using WealthMate.Models;
using WealthMate.Services;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using Syncfusion.SfNumericTextBox.XForms;
using WealthMate.ViewModels;
using System.Collections.ObjectModel;
using System.Collections;
using System.Globalization;

namespace WealthMate.Views.Markets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockDetailsPage
    {
        public OwnedStock OwnedStock { get; set; }
        public CustomDatePicker CustDate { get; set; }
        public StockDetailsPage(Stock stock)
        {
            OwnedStock = new OwnedStock{Stock = stock, PurchaseDate = DateTime.Now, AssetName = stock.CompanyName};
            CustDate = new CustomDatePicker();
            BindingContext = new StockDetailsViewModel(stock);
            //CustDate.SelectionChanged += DatePicker_Changed;
            updateDatePicker(CustDate);
            InitializeComponent();
        }

        private void updateDatePicker(CustomDatePicker picker)
        {
            picker.Headers.Add("Month");
            picker.Headers.Add("Day");
            picker.Headers.Add("Year");
            picker.HeaderText = "Date Picker";
            picker.ColumnHeaderText = picker.Headers;
            picker.ShowHeader = true;
            picker.ShowColumnHeader = true;
        }
        //private void DatePicker_Changed(object sender, SelectionChangedEventArgs e)
        //{
        //    UpdateDays(CustDate, e);
        //}

        private void DatePicker_Clicked(object sender, EventArgs e)

        {

            Date.IsOpen = !Date.IsOpen;

        }

        public void UpdateDays(ObservableCollection<object> Date, SelectionChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>

            {

                if (Date.Count == 3)
                {
                    bool flag = false;
                    if (e.PreviousSelection != null && e.CurrentSelection != null && (e.PreviousSelection as ObservableCollection<object>).Count == 3 && (e.CurrentSelection as ObservableCollection<object>).Count == 3)
                    //if (e.OldValue != null && e.NewValue != null && (e.OldValue as ObservableCollection<object>).Count == 3 && (e.NewValue as ObservableCollection<object>).Count == 3)
                    {
                        if (!object.Equals((e.PreviousSelection as IList)[0], (e.CurrentSelection as IList)[0]))
                        //if (!object.Equals((e.OldValue as IList)[0], (e.NewValue as IList)[0]))
                            {
                            flag = true;
                        }
                        if (!object.Equals((e.PreviousSelection as IList)[2], (e.CurrentSelection as IList)[2]))
                        //if (!object.Equals((e.OldValue as IList)[2], (e.NewValue as IList)[2]))
                            {
                            flag = true;
                        }
                    }

                    if (flag)
                    {

                        ObservableCollection<object> days = new ObservableCollection<object>();
                        int month = DateTime.ParseExact(CustDate.Months[(e.CurrentSelection as IList)[0].ToString()], "MMMM", CultureInfo.InvariantCulture).Month;
                        int year = int.Parse((e.CurrentSelection as IList)[2].ToString());
                        //int month = DateTime.ParseExact(Months[(e.NewValue as IList)[0].ToString()], "MMMM", CultureInfo.InvariantCulture).Month;
                        //int year = int.Parse((e.NewValue as IList)[2].ToString());
                        for (int j = 1; j <= DateTime.DaysInMonth(year, month); j++)
                        {
                            if (j < 10)
                            {
                                days.Add("0" + j);
                            }
                            else
                                days.Add(j.ToString());
                        }
                        ObservableCollection<object> PreviousValue = new ObservableCollection<object>();

                        foreach (var item in e.CurrentSelection as IList)
                        //foreach (var item in e.NewValue as IList)
                        {
                            PreviousValue.Add(item);
                        }
                        if (days.Count > 0)
                        {
                            Date.RemoveAt(1);
                            Date.Insert(1, days);
                        }

                        if ((Date[1] as IList).Contains(PreviousValue[1]))
                        {
                            CustDate.SelectedItem = PreviousValue;
                        }
                        else
                        {
                            PreviousValue[1] = (Date[1] as IList)[(Date[1] as IList).Count - 1];
                            CustDate.SelectedItem = PreviousValue;
                        }
                    }
                }
            });
        }

        //Asks for details of shares purchased
        private void AddToPortfolioClicked(object sender, EventArgs e)
        {
            StockPortfolioForm.IsOpen = true;
            CustDate.IsOpen = true;
        }

        //Adds purchased shares of stock to the users portfolio
        private void AddInPopupClicked(object sender, EventArgs e)
        {
            if (OwnedStock.PurchasedPrice <= 0)
            {
                DisplayAlert (null, "Please enter purchase price!", "OK");
            }
            else
            {
                OwnedStock.UpdateOwnedAsset();
                ((App)Application.Current).User.Portfolio.AddAsset(OwnedStock);
                StockPortfolioForm.IsOpen = false;
            }
        }

        private void Handle_NumSharesChanged(object sender, ValueEventArgs e)
        {
            int.TryParse(e.Value.ToString(), out var value);
            OwnedStock.SharesPurchased = value;
        }

        private void Handle_PriceChanged(object sender, ValueEventArgs e)
        {
            float.TryParse(e.Value.ToString(), out var value);
            OwnedStock.PurchasedPrice = value;
        }

        public void Handle_ReturnGoalChanged(object sender, ValueEventArgs e)
        {
            float.TryParse(e.Value.ToString(), out var value);
            OwnedStock.ReturnGoal = value;
        }
    }
}