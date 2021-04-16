namespace WheelsCrawler.Data.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        public string WheelsName { get; set; }
        public string RiaName { get; set; }
        public string RstName { get; set; }

        public virtual CarBrand CarBrand { get; set; }
    }
}