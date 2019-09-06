﻿using System;
using System.ComponentModel;
using WealthMate.Models;
using WealthMate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WealthMate.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class PortfolioPage : ContentPage
    {
        public PortfolioPage()
        {
            InitializeComponent();

            BindingContext = new PieChart();
        }
    }
}