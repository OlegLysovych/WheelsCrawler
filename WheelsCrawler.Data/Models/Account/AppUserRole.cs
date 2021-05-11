using Microsoft.AspNetCore.Identity;

namespace WheelsCrawler.Data.Models.Account
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public virtual User User { get; set; }
        public virtual AppRole Role { get; set; }
    }
}