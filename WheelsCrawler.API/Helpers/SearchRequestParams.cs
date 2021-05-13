using WheelsCrawler.Data.Helpers;

namespace WheelsCrawler.API.Helpers
{
    public class SearchRequestParams : UserParams
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Fuel { get; set; }
        public string Gearbox { get; set; }
        public string Type { get; set; }
        public bool IsNeedToSave { get; set; }

    }
}