using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class OwnedStockDetailsPage : ContentPage
    {
        public OwnedStock OwnedStock { get; }
        public OwnedStockDetailsPage(OwnedStock ownedStock)
        {
            OwnedStock = ownedStock;
            BindingContext = this;
            InitializeComponent();
            
        }
    }
}