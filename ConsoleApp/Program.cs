using ConsoleApp.Lib;
using Domain.Models;
using Domain.Repositories;
using Domain.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ConsoleApp
{

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var serviceProvider = new ServiceCollection()
                .AddDbContext<EfDbContext>(options =>
                    options.UseSqlServer(Constants.DefaultConnection))
                .AddSingleton<ISettingRepository, EfSettingRepository>()
                .AddSingleton<IEscortRepository, EfEscortRepository>()
                .AddSingleton<ITextRepository, EfTextRepository>()
                .AddSingleton<IMenuRepository, EfMenuRepository>()
                .AddSingleton<IFileImageRepository, EfFileImageRepository>()
                .BuildServiceProvider();

            
            var l = new CsvToBd(serviceProvider);
            //await l.RemoveBad(Constants.SiteName);
            //await l.TextBuildMenuEscorts(Constants.SiteName);
            //await l.TextBuildMenuHome(Constants.SiteName);

            //await l.AddEscort1(Constants.SiteName, "escorts.csv", 1);
            //await l.AddEscort1(Constants.SiteName, "escortsfetish.csv", 2);
            //await l.AddEscort3(Constants.SiteName, "escortsnew.csv", 3);
            //await l.AddEscort4(Constants.SiteName, "escortsreno.csv", 4);
            //await l.AddEscort3(Constants.SiteName, "massage.csv", 5);

            //await l.AddSiteTitleSiteDescription(Constants.SiteName, "EXP.csv");


            //await l.BuildEscorts(Constants.SiteName, @"D:\!Download\indianapolis_massage_girls\indianapolis_massage_girls\images\escort_pic");
            //await l.AddParserTexts(Constants.SiteName);
            //await l.BuildSection(Constants.SiteName);
        }
    }
}
