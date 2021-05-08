using System;
using System.Data.Entity.Core;
using System.Globalization;
using System.Text.RegularExpressions;
using AutoMapper;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.Models.Account;

namespace WheelsCrawler.Data.Dto
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Ria
            CreateMap<CarPageRiaDto, Car>();
            CreateMap<CarSearchRiaDto, Car>()
                .ForMember(destinationMember => destinationMember.Price,
                opt => opt.MapFrom(src => DoubleTypeConverter.Convert(src.Price)))
                .ForMember(destinationMember => destinationMember.Kilometrage,
                opt => opt.MapFrom(src => IntTypeConverter.Convert(src.Kilometrage)))
                .ForMember(destinationMember => destinationMember.EngineСapacity,
                opt => opt.MapFrom(src => DoubleTypeConverter.Convert(src.EngineСapacity)))
                .ForMember(destinationMember => destinationMember.PublishDate,
                opt => opt.MapFrom(src => DateTimeTypeConverter.Convert(src.PublishDate)))
                ;
            //RST
            CreateMap<CarPageRstDto, Car>();
            CreateMap<CarSearchRstDto, Car>()
                .ForMember(destinationMember => destinationMember.Price,
                opt => opt.MapFrom(src => DoubleTypeConverter.Convert(src.Price)))
                .ForMember(destinationMember => destinationMember.Kilometrage,
                opt => opt.MapFrom(src => IntTypeConverter.Convert(src.Kilometrage)))
                .ForMember(destinationMember => destinationMember.EngineСapacity,
                opt => opt.MapFrom(src => DoubleTypeConverter.Convert(src.EngineСapacity)))
                .ForMember(destinationMember => destinationMember.PublishDate,
                opt => opt.MapFrom(src => DateTimeTypeConverter.Convert(src.PublishDate)))
                .ForMember(destinationMember => destinationMember.PictureUri,
                opt => opt.MapFrom(src => src.PictureUri.Contains("thumb") ? src.PictureUri.Replace("thumb", "big")
                                        : src.PictureUri.Contains("middle") ? src.PictureUri.Replace("middle", "big")
                                        : src.PictureUri.Contains("small") ? src.PictureUri.Replace("small", "big")
                                        : src.PictureUri.Replace("ua", "ua")))
                .ForMember(destinationMember => destinationMember.CarUri,
                opt => opt.MapFrom(src => "https://rst.ua" + src.CarUri))
                ;
            //Mobile
            CreateMap<CarMobileDto, Car>();

            //Client side
            CreateMap<User, MemberDTO>();
            CreateMap<Car, CarDto>()
                .ForMember(destinationMember => destinationMember.Publishdate,
                opt => opt.MapFrom(src => src.PublishDate.ToShortDateString()))
                .ForMember(destinationMember => destinationMember.Enginecapacity,
                opt => opt.MapFrom(src => src.EngineСapacity))
                .ForMember(destinationMember => destinationMember.Cargearbox,
                opt => opt.MapFrom(src => src.CarGearbox.WheelsName.ToString()))
                .ForMember(destinationMember => destinationMember.Carbrand,
                opt => opt.MapFrom(src => src.CarBrand.WheelsName.ToString()))
                .ForMember(destinationMember => destinationMember.Cartype,
                opt => opt.MapFrom(src => src.CarType.WheelsName.ToString()))
                .ForMember(destinationMember => destinationMember.Carfuel,
                opt => opt.MapFrom(src => src.CarFuel.WheelsName.ToString()))
                .ForMember(destinationMember => destinationMember.Carmodel,
                opt => opt.MapFrom(src => src.CarModel.WheelsName.ToString()));
        }
    }

    // Automapper string to int
    internal static class IntTypeConverter
    {
        public static int Convert(string source)
        {
            if (source == null)
                return 0;
            else
            {
                Regex rgx = new Regex(@"[-+]?[0-9]*\.?[0-9]+?([-+]?[0-9]*\s?[0-9]+)?([-+]?[0-9]*\'?[0-9]+)?");
                var a = rgx.Match(source).Value.Replace(" ", "");
                return source.Contains("тис. км") ? int.Parse(a) * 1000 : a.Length == 0 ? 0 : int.Parse(a);
                // return Int32.Parse(source);
            }
        }
    }
    // Automapper string to double
    internal static class DoubleTypeConverter
    {
        public static double Convert(string source)
        {
            if (source == null)
                return 0.0d;
            else
            {
                Regex rgx = new Regex(@"[-+]?[0-9]*\.?[0-9]+?([-+]?[0-9]*\s?[0-9]+)?([-+]?[0-9]*\'?[0-9]+)?");
                var a = rgx.Match(source).Value.Replace("'", "").Replace(" ", "");
                return a.Length == 0 ? 0d : double.Parse(a, CultureInfo.InvariantCulture);
                // return Int32.Parse(source);
            }
        }
    }

    // Automapper string to decimal
    internal static class DecimalTypeConverter
    {
        public static decimal Convert(string source)
        {
            if (source == null)
                return 0.0m;
            else
            {
                Regex rgx = new Regex(@"[-+]?[0-9]*\.?[0-9]+?([-+]?[0-9]*\s?[0-9]+)?([-+]?[0-9]*\'?[0-9]+)?");
                try
                {
                    var a = rgx.Match(source).Value.Replace("'", "").Replace(" ", "");
                    return a.Length != 0 ? decimal.Parse(a, CultureInfo.InvariantCulture) : 0m;
                }
                catch (System.Exception)
                {
                    return 0m;
                }
                // return Decimal.Parse(source);
            }
        }
    }

    // Automapper string to DateTime
    internal static class DateTimeTypeConverter
    {
        public static DateTime Convert(string source)
        {
            if (source == null)
                return DateTime.Now;
            else
            {
                try
                {
                    var date = DateTime.Parse(source);
                    return date;
                }
                catch (System.Exception)
                {
                    var sourceNow = DateTime.Now;
                    if (source.Contains("місяц"))
                    {
                        sourceNow = sourceNow.AddMonths(int.Parse(source[source.IndexOf("місяц") - 2].ToString()) * -1);
                    }
                    if (source.Contains("тиж"))
                    {
                        sourceNow = sourceNow.AddDays(int.Parse(source[source.IndexOf("тиж") - 2].ToString()) * -7);
                    }
                    if (source.Contains("дн"))
                    {
                        sourceNow = sourceNow.AddDays(-1);
                    }

                    return sourceNow;
                }
            }
        }
    }

}