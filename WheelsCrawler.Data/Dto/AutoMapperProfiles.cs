using AutoMapper;
using WheelsCrawler.Data.Models;

namespace WheelsCrawler.Data.Dto
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Ria
            CreateMap<CarPageRiaDto, Car>();
            CreateMap<CarSearchRiaDto, Car>();
            //RST
            CreateMap<CarPageRstDto, Car>();
            CreateMap<CarSearchRstDto, Car>();
            //Mobile
            CreateMap<CarMobileDto, Car>();
        }
    }
}