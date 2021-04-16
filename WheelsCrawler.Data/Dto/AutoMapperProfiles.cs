using System;
using System.Data.Entity.Core;
using System.Globalization;
using System.Text.RegularExpressions;
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
            CreateMap<CarSearchRiaDto, Car>()
                .ForMember(destinationMember => destinationMember.Price,
                opt => opt.MapFrom(src => DecimalTypeConverter.Convert(src.Price)))
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
                opt => opt.MapFrom(src => DecimalTypeConverter.Convert(src.Price)))
                .ForMember(destinationMember => destinationMember.Kilometrage,
                opt => opt.MapFrom(src => IntTypeConverter.Convert(src.Kilometrage)))
                .ForMember(destinationMember => destinationMember.EngineСapacity,
                opt => opt.MapFrom(src => DoubleTypeConverter.Convert(src.EngineСapacity)))
                .ForMember(destinationMember => destinationMember.PublishDate,
                opt => opt.MapFrom(src => DateTimeTypeConverter.Convert(src.PublishDate)))
                ;
            //Mobile
            CreateMap<CarMobileDto, Car>();

            // CreateMap<string, int>().ConvertUsing<IntTypeConverter>();
            // CreateMap<string, int?>().ConvertUsing<NullIntTypeConverter>();
            // CreateMap<string, decimal?>().ConvertUsing<NullDecimalTypeConverter>();
            // CreateMap<string, decimal>().ConvertUsing<DecimalTypeConverter>();
            // CreateMap<string, bool?>().ConvertUsing<NullBooleanTypeConverter>();
            // CreateMap<string, bool>().ConvertUsing<BooleanTypeConverter>();
            // CreateMap<string, Int64?>().ConvertUsing<NullInt64TypeConverter>();
            // CreateMap<string, Int64>().ConvertUsing<Int64TypeConverter>();
            // CreateMap<string, DateTime?>().ConvertUsing<NullDateTimeTypeConverter>();
            // CreateMap<string, DateTime>().ConvertUsing<DateTimeTypeConverter>();
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