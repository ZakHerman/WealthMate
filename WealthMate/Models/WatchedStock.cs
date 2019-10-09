using SQLite;

namespace WealthMate.Models
{
    public class WatchedStock
    {
        [PrimaryKey]
        public string Symbol { get; set; }
    }
}
