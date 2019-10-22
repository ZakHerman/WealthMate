using System;
using System.Collections.Generic;
using Syncfusion.SfPicker.XForms;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Collections;
using Xamarin.Forms;

namespace WealthMate.Services
{
    public class CustomDatePicker : SfPicker
    {
        #region Public Properties

        internal Dictionary<string, string> Months { get; set; }

        public ObservableCollection<object> Date { get; set; }

        internal ObservableCollection<object> Day { get; set; }

        internal ObservableCollection<object> Month { get; set; }

        internal ObservableCollection<object> Year { get; set; }

        public ObservableCollection<string> Headers { get; set; }

        #endregion
        public CustomDatePicker()
        {
            Months = new Dictionary<string, string>();

            Date = new ObservableCollection<object>();

            Day = new ObservableCollection<object>();

            Month = new ObservableCollection<object>();

            Year = new ObservableCollection<object>();

            Headers = new ObservableCollection<string>();

            updateDatePicker();

            PopulateDateCollection();

            ItemsSource = Date;
            SelectionChanged += CustomDatePicker_Changed;
        }

        // Populates months, years and days which will be available in the picker
         
        private void PopulateDateCollection()

        {
            for (var i = 1; i <= 12; i++)
            {
                if (!Months.ContainsKey(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3)))
                    Months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3), CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));

                Month.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3));
            }

            // Available years = 2000-2025
            for (var i = 2000; i < 2025; i++)
            {
                Year.Add(i.ToString());
            }

            for (var i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
            {
                if (i < 10)
                    Day.Add("0" + i);
                else
                    Day.Add(i.ToString());
            }

            Date.Add(Month);
            Date.Add(Day);
            Date.Add(Year);
        }


        private void updateDatePicker()
        {
            Headers.Add("Month");
            Headers.Add("Day");
            Headers.Add("Year");
            HeaderText = "Date Picker";
            ColumnHeaderText = Headers;
            ShowHeader = true;
            ShowColumnHeader = true;
            ShowFooter = true;
        }

        private void CustomDatePicker_Changed(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            UpdateDays(Date, e);
        }

        public void UpdateDays(ObservableCollection<object> date, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (date.Count == 3)
                {
                    var flag = false;
                    if (e.OldValue != null && e.NewValue != null && ((ObservableCollection<object>) e.OldValue).Count == 3 && ((ObservableCollection<object>) e.NewValue).Count == 3)
                    {
                        if (!Equals(((IList) e.OldValue)[0], ((IList) e.NewValue)[0]))
                        {
                            flag = true;
                        }
                        if (!Equals(((IList) e.OldValue)[2], ((IList) e.NewValue)[2]))
                        {
                            flag = true;
                        }
                    }

                    if (flag)
                    {
                        var days = new ObservableCollection<object>();
                        var month = DateTime.ParseExact(Months[((IList) e.NewValue)[0].ToString()], "MMMM", CultureInfo.InvariantCulture).Month;
                        var year = int.Parse(((IList) e.NewValue)[2].ToString());
                        
                        for (var j = 1; j <= DateTime.DaysInMonth(year, month); j++)
                        {
                            if (j < 10)
                                days.Add("0" + j);
                            else
                                days.Add(j.ToString());
                        }

                        var previousValue = new ObservableCollection<object>();

                        foreach (var item in (IList) e.NewValue)
                        {
                            previousValue.Add(item);
                        }

                        if (days.Count > 0)
                        {
                            date.RemoveAt(1);
                            date.Insert(1, days);
                        }

                        if (((IList) date[1]).Contains(previousValue[1]))
                        {
                            SelectedItem = previousValue;
                        }
                        else
                        {
                            previousValue[1] = ((IList) date[1])[((IList) date[1]).Count - 1];
                            SelectedItem = previousValue;
                        }
                    }
                }
            });
        }
    }
}
