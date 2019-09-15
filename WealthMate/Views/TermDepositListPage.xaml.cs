using System.Collections.ObjectModel;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermDepositListPage
    {
        private SearchBar _searchBar;
        public ObservableCollection<TermDeposit> TermDeposits { get; } = new ObservableCollection<TermDeposit>();

        public TermDepositListPage()
        {
            InitializeComponent();

            GenerateExample();
        }

        public void GenerateExample()
        {
            TermDeposits.Add(new TermDeposit {Logo = "WBC", Provider = "Westpac", InterestRate = 12.5f, LengthInMonths = 6, MaxDeposit = 50000, MinDeposit = 10000 });
            TermDeposits.Add(new TermDeposit {Logo = "ANZ", Provider = "ANZ", InterestRate = 4.5f, LengthInMonths = 12, MaxDeposit = 10000, MinDeposit = 1000});
            TermDeposits.Add(new TermDeposit {Logo = "WBC", Provider = "Westpac", InterestRate = 3.6f, LengthInMonths = 9, MaxDeposit = 50000, MinDeposit = 10000 });
            TermDeposits.Add(new TermDeposit {Logo = "ANZ", Provider = "ANZ", InterestRate = 4.5f, LengthInMonths = 18, MaxDeposit = 15000, MinDeposit = 1000 });
            TermDeposits.Add(new TermDeposit {Logo = "WBC", Provider = "Westpac", InterestRate = 12.5f, LengthInMonths = 6, MaxDeposit = 8000, MinDeposit = 10000 });
            TermDeposits.Add(new TermDeposit {Logo = "ANZ", Provider = "ANZ", InterestRate = 4.5f, LengthInMonths = 3, MaxDeposit = 0, MinDeposit = 1000 });
            TermDeposits.Add(new TermDeposit {Logo = "WBC", Provider = "Westpac", InterestRate = 12.5f, LengthInMonths = 6, MaxDeposit = 1000000, MinDeposit = 1000 });
            TermDeposits.Add(new TermDeposit {Logo = "ANZ", Provider = "ANZ", InterestRate = 4.5f, LengthInMonths = 24, MaxDeposit = 20000, MinDeposit = 1000 });
            TermDeposits.Add(new TermDeposit {Logo = "WBC", Provider = "Westpac", InterestRate = 12.5f, LengthInMonths = 6, MaxDeposit = 15000, MinDeposit = 10000 });
            TermDeposits.Add(new TermDeposit {Logo = "ANZ", Provider = "ANZ", InterestRate = 4.5f, LengthInMonths = 18, MaxDeposit = 1000, MinDeposit = 1000 });
        }

        /// <summary>
        /// Search bar functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchBar = (sender as SearchBar);

            if (TDList.DataSource != null)
            {
                TDList.DataSource.Filter = FilterTDeposits;
                TDList.DataSource.RefreshFilter();
            }
        }

        /// <summary>
        /// method for filtering the list view as text changes within the search bar
        /// </summary>
        /// <param name="obj"></param> 
        /// <returns></returns>
        private bool FilterTDeposits(object obj)
        {
            if (_searchBar?.Text == null)
                return true;

            return obj is TermDeposit termD && (termD.Provider.ToLower().Contains(_searchBar.Text.ToLower()));
        }

    }
}