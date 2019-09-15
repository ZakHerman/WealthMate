using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    //[DesignTimeVisible{false)]
    public partial class PortfolioChart : ContentPage
    {
        public float TotalValue { get; set; }
        public PortfolioChart()
        {
            TotalValue = 634635.5623f;
            InitializeComponent();

        }

    }
}