namespace WheelsCrawler.API.DTO
{
    public class SearchRequest
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public bool IsNeedToSave { get; set; }

    }
}