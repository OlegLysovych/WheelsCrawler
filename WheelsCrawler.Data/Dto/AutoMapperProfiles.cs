using AutoMapper;
using WheelsCrawler.Data.Models;

namespace WheelsCrawler.Data.Dto
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CarRiaDto, Car>();
            CreateMap<CarRstDto, Car>();
            CreateMap<CarMobileDto, Car>();
        }
    }
}