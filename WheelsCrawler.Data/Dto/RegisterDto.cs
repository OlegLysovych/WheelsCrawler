using System.ComponentModel.DataAnnotations;

namespace WheelsCrawler.Data.Dto
{
    public class RegisterDto
    {
        public string Username { get; set; }
        [StringLength(30, MinimumLength = 4)]
        public string Password { get; set; }
    }
}