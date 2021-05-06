using System.Collections.Generic;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Data.Models.Account
{
    public class User: IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public virtual ICollection<Url> InterestedUrls { get; set; }
    }
}