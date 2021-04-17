using System;
using System.Collections.Generic;
using System.Linq;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Sample
{
    public class UrlBuilder<TEntity> where TEntity : class, IEntity
    {
        private readonly IGenericRepository<Car> _carRepo;
        private readonly IGenericRepository<CarBrand> _brandRepo;
        private readonly IGenericRepository<CarModel> _modelRepo;
        IEnumerable<CarBrand> brandList;
        IEnumerable<CarModel> modelList;
        public UrlBuilder()
        {
            _carRepo = new GenericRepository<Car>();
            _brandRepo = new GenericRepository<CarBrand>();
            _modelRepo = new GenericRepository<CarModel>();

            brandList = _brandRepo.GetAll().AsEnumerable();
            modelList = _modelRepo.GetAll().AsEnumerable();
        }

        public string RiaUrlBuilder(UrlRequestToSearch requestToSearch)
        {
            string url = String.Empty;

            var brand = brandList.FirstOrDefault(x => x.WheelsName.Equals(requestToSearch.Brand)).RiaName;
            var model = modelList.FirstOrDefault(x => x.WheelsName.Equals(requestToSearch.Model)).RiaName;
            System.Console.WriteLine($"const = https://auto.ria.com/uk/legkovie/mercedes-benz/gl-class/");
            System.Console.WriteLine($"compu = https://auto.ria.com/uk/legkovie/{brand}/{model}/");
            return url;
        }
        public string RstUrlBuilder(UrlRequestToSearch requestToSearch)
        {
            string url = String.Empty;

            var brand = brandList.FirstOrDefault(x => x.WheelsName.Equals(requestToSearch.Brand)).RstName;
            var model = modelList.FirstOrDefault(x => x.WheelsName.Equals(requestToSearch.Model)).RstName;
            System.Console.WriteLine($"const = https://rst.ua/ukr/oldcars/mercedes/gl/");
            System.Console.WriteLine($"compu = https://rst.ua/ukr/oldcars/{brand}/{model}/");
            return url;
        }

    }
}