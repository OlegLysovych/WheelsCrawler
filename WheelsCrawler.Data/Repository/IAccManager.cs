using System;
using WheelsCrawler.Data.Models.Account;

namespace WheelsCrawler.Data.Repository
{
    public interface IAccManager : IDisposable
    {
        void Create(User item);
    }
}