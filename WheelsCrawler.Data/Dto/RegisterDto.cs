using System.ComponentModel.DataAnnotations;

namespace WheelsCrawler.Data.Dto
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string Password { get; set; }
    }
}