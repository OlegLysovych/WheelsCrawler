using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WheelsCrawler.Data.Models.Account
{
    public class AppRole : IdentityRole<int>
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}