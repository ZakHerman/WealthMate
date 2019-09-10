using System.Collections.ObjectModel;
using WealthMate.Models;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermDepositListPage
    {
        public ObservableCollection<TermDeposit> TermDeposits { get; } = new ObservableCollection<TermDeposit>();

        public TermDepositListPage()
        {
            InitializeComponent();

            GenerateExample();
        }

        public void GenerateExample()
        {
            TermDeposits.Add(new TermDeposit {Logo = "WBC", Provider = "Westpac", InterestRate = 12.5f});
            TermDeposits.Add(new TermDeposit {Logo = "WBC", Provider = "Westpac", InterestRate = 3});
        }
    }
}