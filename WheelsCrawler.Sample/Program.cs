﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WheelsCrawler.Core;
using WheelsCrawler.Data.Dto;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Downloader;
using WheelsCrawler.Pipeline;
using WheelsCrawler.Processor;
using WheelsCrawler.Request;

namespace WheelsCrawler.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }
        static async Task MainAsync(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            // var crawlerRia = new WheelsCrawler<CarPageRiaDto, Car>()
            //                      .AddRequest(new WheelsCrawlerRequest { Url = "https://auto.ria.com/uk/legkovie/mercedes-benz/cl-class/?countpage=100", Regex = @".*/auto_[^ria].+", TimeOut = 5000 })
            //                      .AddDownloader(new WheelsCrawlerDownloader { DownloderType = WheelsCrawlerDownloaderType.FromFile, DownloadPath = "C:/Users/PC/source/repos/WheelsCrawler/htmls/RIA/"})
            //                      .AddProcessor(new WheelsCrawlerProcessor<CarPageRiaDto, Car> { })
            //                      .AddPipeline(new WheelsCrawlerPipeline<Car> { });

            // await crawlerRia.Crawle();

            // var crawlerRST = new WheelsCrawler<CarPageRstDto, Car>()
            //                      .AddRequest(new WheelsCrawlerRequest { Url = "https://rst.ua/ukr/oldcars/mercedes/cl-class/", Regex = @".*/oldcars/.+_.+\.html$", TimeOut = 5000 })//[^\d{}]
            //                      .AddDownloader(new WheelsCrawlerDownloader { DownloderType = WheelsCrawlerDownloaderType.FromFile, DownloadPath = @"C:/Users/PC/source/repos/WheelsCrawler/htmls/RST/"})//
            //                      .AddProcessor(new WheelsCrawlerProcessor<CarPageRstDto, Car> { })
            //                      .AddPipeline(new WheelsCrawlerPipeline<Car> { });

            // await crawlerRST.Crawle();

            // var crawlerMobile = new WheelsCrawler<CarMobileDto, Car>()
            //                      .AddRequest(new WheelsCrawlerRequest { Url = "https://suchen.mobile.de/fahrzeuge/search.html?dam=0&isSearchRequest=true&ms=17200%3B%3B8%3B%3B&s=Car&sfmr=false&vc=Car", Regex = @".*suchen.mobile.de/fahrzeuge.+html[^contact]", TimeOut = 5000 })//[^\d{}]
            //                      .AddDownloader(new WheelsCrawlerDownloader { DownloderType = WheelsCrawlerDownloaderType.FromFile, DownloadPath = @"C:/Users/PC/source/repos/WheelsCrawler/htmls/Mobile/"})//
            //                      .AddProcessor(new WheelsCrawlerProcessor<CarMobileDto, Car> { })
            //                      .AddPipeline(new WheelsCrawlerPipeline<Car> { });

            // await crawlerMobile.Crawle();

            // var crawlerRia = new WheelsCrawler<CarSearchRiaDto, Car>()
            //                      .AddRequest(new WheelsCrawlerRequest { Url = "https://auto.ria.com/uk/legkovie/mercedes-benz/gl-class/", Regex = @"\?page=[0-9]+$", TimeOut = 5000 })
            //                      .AddDownloader(new WheelsCrawlerDownloader { DownloderType = WheelsCrawlerDownloaderType.FromFile, DownloadPath = "C:/Users/PC/source/repos/WheelsCrawler/htmls/RIA/" })
            //                      .AddProcessor(new WheelsCrawlerProcessor<CarSearchRiaDto, Car> { })
            //                      .AddPipeline(new WheelsCrawlerPipeline<Car> { });

            // await crawlerRia.Crawle();
            // var crawlerRST = new WheelsCrawler<CarSearchRstDto, Car>()
            //                      .AddRequest(new WheelsCrawlerRequest { Url = "https://rst.ua/ukr/oldcars/mercedes/gl/", Regex = @".*/oldcars/.+/[0-9]+\.html$", TimeOut = 5000 })//[^\d{}]
            //                      .AddDownloader(new WheelsCrawlerDownloader { DownloderType = WheelsCrawlerDownloaderType.FromFile, DownloadPath = @"C:/Users/PC/source/repos/WheelsCrawler/htmls/RST/" })//
            //                      .AddProcessor(new WheelsCrawlerProcessor<CarSearchRstDto, Car> { })
            //                      .AddPipeline(new WheelsCrawlerPipeline<Car> { });

            // await crawlerRST.Crawle();

            var crawlerRia = new WheelsCrawler<CarSearchRiaDto, Car>()
                                 .AddRequest(new WheelsCrawlerRequest { Url = "https://auto.ria.com/uk/legkovie/volkswagen/golf/", Regex = @"\?page=[0-9]+$", TimeOut = 5000 })
                                 .AddDownloader(new WheelsCrawlerDownloader { DownloderType = WheelsCrawlerDownloaderType.FromFile, DownloadPath = "C:/Users/PC/source/repos/WheelsCrawler/htmls/RIA/" })
                                 .AddProcessor(new WheelsCrawlerProcessor<CarSearchRiaDto, Car> { })
                                 .AddPipeline(new WheelsCrawlerPipeline<Car> { });

            await crawlerRia.Crawle();
            var crawlerRST = new WheelsCrawler<CarSearchRstDto, Car>()
                                 .AddRequest(new WheelsCrawlerRequest { Url = "https://rst.ua/ukr/oldcars/volkswagen/golf/", Regex = @".*/oldcars/.+/[0-9]+\.html$", TimeOut = 5000 })//[^\d{}]
                                 .AddDownloader(new WheelsCrawlerDownloader { DownloderType = WheelsCrawlerDownloaderType.FromFile, DownloadPath = @"C:/Users/PC/source/repos/WheelsCrawler/htmls/RST/" })//
                                 .AddProcessor(new WheelsCrawlerProcessor<CarSearchRstDto, Car> { })
                                 .AddPipeline(new WheelsCrawlerPipeline<Car> { });

            await crawlerRST.Crawle();

            UrlBuilder<Car> urlBuilder = new UrlBuilder<Car>();
            UrlRequestToSearch urlRequestToSearch = new UrlRequestToSearch { Brand = "Mercedes", Model = "GL-Class" };

            urlBuilder.RiaUrlBuilder(urlRequestToSearch);
            urlBuilder.RstUrlBuilder(urlRequestToSearch);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format(
                "{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours,
                ts.Minutes,
                ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}
