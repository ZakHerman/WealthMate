using System;
using System.Collections.Generic;
using System.Text;

namespace WealthMate.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Test
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
