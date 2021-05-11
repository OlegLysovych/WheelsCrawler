using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Data.Models.Account
{
    public class User : IdentityUser<int>, IEntity
    {
        // public int Id { get; set; }
        // public string UserName { get; set; }
        // public byte[] PasswordHash { get; set; }
        // public byte[] PasswordSalt { get; set; }

        public User() => InterestedUrls = new HashSet<Url>();
        public virtual ICollection<Url> InterestedUrls { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}