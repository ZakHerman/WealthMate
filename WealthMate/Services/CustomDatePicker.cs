using System;
using System.Collections.Generic;
using Syncfusion.SfPicker.XForms;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using System.Globalization;

namespace WealthMate.Services
{
    public class CustomDatePicker : SfPicker
    {
        #region Public Properties

        // Months API is used to modify the Day collection as per change in Month

        internal Dictionary<string, string> Months { get; set; }

        /// <summary>

        /// Date is the actual DataSource for SfPicker control which will holds the collection of Day ,Month and Year

        /// </summary>

        /// <value>The date.</value>

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

            PopulateDateCollection();

            this.ItemsSource = Date;

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
    }
}
