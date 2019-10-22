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

            this.ItemsSource = Date;
            this.SelectionChanged += CustomDatePicker_Changed;

        }

        /**
         * This method populates months, years and days which will be available in the picker
         */
        private void PopulateDateCollection()

        {

            for (int i = 1; i <= 12; i++)

            {

                if (!Months.ContainsKey(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3)))

                    Months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3), CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));

                Month.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3));

            }

            //Available years = 2000-2025
            for (int i = 2000; i < 2025; i++)

            {

                Year.Add(i.ToString());

            }

            for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)

            {

                if (i < 10)

                {

                    Day.Add("0" + i);

                }

                else

                    Day.Add(i.ToString());

            }
            Date.Add(Month);

            Date.Add(Day);

            Date.Add(Year);

        }


        private void updateDatePicker()
        {
            
            this.Headers.Add("Month");
            this.Headers.Add("Day");
            this.Headers.Add("Year");
            this.HeaderText = "Date Picker";
            this.ColumnHeaderText = Headers;
            this.ShowHeader = true;
            this.ShowColumnHeader = true;
            this.ShowFooter = true;
        }

        private void CustomDatePicker_Changed(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            UpdateDays(Date, e);
        }

        public void UpdateDays(ObservableCollection<object> Date, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)

        {

            Device.BeginInvokeOnMainThread(() =>

            {

                if (Date.Count == 3)
                {
                    bool flag = false;
                    if (e.OldValue != null && e.NewValue != null && (e.OldValue as ObservableCollection<object>).Count == 3 && (e.NewValue as ObservableCollection<object>).Count == 3)
                    {
                        if (!object.Equals((e.OldValue as IList)[0], (e.NewValue as IList)[0]))
                        {
                            flag = true;
                        }
                        if (!object.Equals((e.OldValue as IList)[2], (e.NewValue as IList)[2]))
                        {
                            flag = true;
                        }
                    }

                    if (flag)
                    {

                        ObservableCollection<object> days = new ObservableCollection<object>();
                        int month = DateTime.ParseExact(Months[(e.NewValue as IList)[0].ToString()], "MMMM", CultureInfo.InvariantCulture).Month;
                        int year = int.Parse((e.NewValue as IList)[2].ToString());
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

                        foreach (var item in e.NewValue as IList)
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
                            this.SelectedItem = PreviousValue;
                        }
                        else
                        {
                            PreviousValue[1] = (Date[1] as IList)[(Date[1] as IList).Count - 1];
                            this.SelectedItem = PreviousValue;
                        }
                    }
                }
            });
        }
    }
}
