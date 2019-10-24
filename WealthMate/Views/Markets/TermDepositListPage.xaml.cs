using Syncfusion.SfNumericTextBox.XForms;
using WealthMate.Models;
using WealthMate.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Collections.Generic;
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.ComboBox;
using WealthMate.Views.Markets.Modal;
using SelectionChangedEventArgs = Syncfusion.XForms.ComboBox.SelectionChangedEventArgs;

namespace WealthMate.Views.Markets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermDepositListPage
    {
        private SearchBar _searchBar;
        private TermDeposit TermDeposit { get; set; }
        public ObservableCollection<TermDeposit> TDList { get; set; }

        public TermDepositListPage()
        {
            TDList = new ObservableCollection<TermDeposit>();
            LoadTermDeposits();
            InitializeComponent();          
        }

        private async void LoadTermDeposits()
        {
            await DataService.FetchTermDepositsAsync();
            TDList = DataService.TermDeposits;
            TermDepositList.ItemsSource = TDList;
        }

        //clears TDList list and re-adds assets to collection
        //this is required due to the return type of OrderBy and OrderByDescending methods
        private void SortList(IEnumerable<TermDeposit> linqResults)
        {
            var observableC = new ObservableCollection<TermDeposit>(linqResults);
            TDList.Clear();

            foreach (var termD in observableC)
            {
                TDList.Add(termD);
            }
        }

        // Sorts StockList list according to picker value upon picker index value changing
        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;

            if (picker.SelectedIndex == 0)
            {
                SortList(TDList.OrderBy(termD => termD.Provider));
            }
            else if (picker.SelectedIndex == 1)
            {
                SortList(TDList.OrderByDescending(termD => termD.InterestRate));
            }
            else if (picker.SelectedIndex == 2)
            {
                SortList(TDList.OrderBy(termD => termD.MinDeposit));
            }
            else if (picker.SelectedIndex == 3)
            {
                SortList(TDList.OrderBy(termD => termD.LengthInMonths));
            }
        }

        // Search bar functionality

        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchBar = sender as SearchBar;

            if (TermDepositList.DataSource != null)
            {
                // Filters the data source
                TermDepositList.DataSource.Filter = FilterTDeposits;
                TermDepositList.DataSource.RefreshFilter();
            }
        }

        // Filtering the list view as text changes within the search bar
        private bool FilterTDeposits(object obj)
        {
            if (_searchBar?.Text == null)
            {
                return true;
            }

            return obj is TermDeposit termD && (termD.Provider.ToLower().Contains(_searchBar.Text.ToLower()));
        }

        // Handles a term deposit being clicked from the term deposit page
        private async void TermDepositClicked(object sender, ItemTappedEventArgs e)
        {
            var selected = (TermDeposit)e.ItemData;

            if (selected == null)
                return;

            // Push term deposit modal page on top of modal stack
            await Navigation.PushModalAsync(new TermDepositModalPage(selected));

            ((SfListView)sender).SelectedItem = null;
        }

        private void SfComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = ((SfComboBox)sender).SelectedIndex;

            switch (index)
            {
                case 0:
                    TermDepositList.ItemsSource = TDList.OrderBy(t => t.Provider);
                    break;
                case 1:
                    TermDepositList.ItemsSource = TDList.OrderByDescending(t => t.Provider);
                    break;
                case 2:
                    TermDepositList.ItemsSource = TDList.OrderBy(t => t.InterestRate);
                    break;
                case 3:
                    TermDepositList.ItemsSource = TDList.OrderByDescending(t => t.InterestRate);
                    break;
                case 4:
                    TermDepositList.ItemsSource = TDList.OrderBy(t => t.MinDeposit);
                    break;
                case 5:
                    TermDepositList.ItemsSource = TDList.OrderByDescending(t => t.MinDeposit);
                    break;
                case 6:
                    TermDepositList.ItemsSource = TDList.OrderByDescending(t => t.LengthInMonths);
                    break;
                case 7:
                    TermDepositList.ItemsSource = TDList.OrderByDescending(t => t.LengthInMonths);
                    break;
            }
        }
    }
}