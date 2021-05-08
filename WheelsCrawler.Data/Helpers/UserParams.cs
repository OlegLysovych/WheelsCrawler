namespace WheelsCrawler.Data.Helpers
{
    public class UserParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 21;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public string City { get; set; }
        public double EngineCapacityFrom { get; set; } = 0.0;
        public double EngineCapacityTo { get; set; } = 10.0;
        public int PriceFrom { get; set; } = 0;
        public int PriceTo { get; set; } = 1_000_000;
        public int KilometrageFrom { get; set; } = 0;
        public int KilometrageTo { get; set; } = 1_000_000;
        public string OrderBy { get; set; } = "lastAdded";
        
    }
}