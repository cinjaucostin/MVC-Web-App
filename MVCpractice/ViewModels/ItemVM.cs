namespace MVCpractice.ViewModels
{
    public class ItemVM
    {
        public int Records { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public string SortField { get; set; }
        public bool Ascending { get; set; }
        public string CurrentField { get; set; }

    }
}
