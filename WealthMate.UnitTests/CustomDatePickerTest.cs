using Microsoft.VisualStudio.TestTools.UnitTesting;
using WealthMate.Models;
using System;
using WealthMate.Services;
using System.Collections.ObjectModel;

namespace WealthMate.UnitTests
{
    [TestClass]
    public class CustomDatePickerTest
    {
        private CustomDatePicker CDP1;
        private ObservableCollection<object> Date;

        public CustomDatePickerTest()
        {
            //CDP1 = new CustomDatePicker();
            Date = new ObservableCollection<object>();
            string testMonth = "Aug";
            string testDay = "12";
            string testYear = "2000";
            Date.Add(testMonth);
            Date.Add(testDay);
            Date.Add(testYear);
        }

        [TestMethod]
        public void TestUpdatePurchaseDate()
        {
            string month = Date[0].ToString();
            int[] actualInts = new int[3] ; 
            int[] expectedInts = new int[] { 8, 12, 2000 }; //12th August, 2000

            switch (month)
            {
                case "Jan":
                    actualInts[0] = 1;
                    break;
                case "Feb":
                    actualInts[0] = 2;
                    break;
                case "Mar":
                    actualInts[0] = 3;
                    break;
                case "Apr":
                    actualInts[0] = 4;
                    break;
                case "May":
                    actualInts[0] = 5;
                    break;
                case "Jun":
                    actualInts[0] = 6;
                    break;
                case "Jul":
                    actualInts[0] = 7;
                    break;
                case "Aug":
                    actualInts[0] = 8;
                    break;
                case "Sep":
                    actualInts[0] = 9;
                    break;
                case "Oct":
                    actualInts[0] = 10;
                    break;
                case "Nov":
                    actualInts[0] = 11;
                    break;
                case "Dec":
                    actualInts[0] = 12;
                    break;
            }

            string day = Date[1].ToString();
            actualInts[1] = Int32.Parse(day);

            string year = Date[2].ToString();
            actualInts[2] = Int32.Parse(year);
            Assert.AreEqual(expectedInts[0], actualInts[0]);
            Assert.AreEqual(expectedInts[1], actualInts[1]);
            Assert.AreEqual(expectedInts[2], actualInts[2]);
        }
    }
}
