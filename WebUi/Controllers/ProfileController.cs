using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Infrastructure;
using WebUi.Lib;
using WebUi.Models;

namespace WebUi.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileController(IEscortRepository escortRepository,
            IMapper mapper,
            ITextRepository textRepository,
            IMenuRepository menuRepository,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache,
            ILogger<ProfileController> logger) : base(escortRepository, textRepository, menuRepository, memoryCache)
        {
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

#if !DEBUG
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
#endif
        [Route("profile/{name?}")]
        public async Task<IActionResult> Index(string name)
        {
            name = name[..^4];
            var escorts = await GetAllEscorts();
            //var texts = await GetAllTexts();


            var escort = escorts.FirstOrDefault(z => z.EscortName.ToLower() == name);


            if (escort == null) return RedirectToAction("Error", "Home");

            var m = _mapper.Map<ProfileViewModel>(escort);

            //var list = escorts.Where(z => z.Id != escort.Id && z.City == escort.City).ToList();
            var list = escorts.Where(z => z.Id != escort.Id).ToList();

            var rnd = new Random();
            foreach (var r in list.Select(p => rnd.Next(list.Count - 1)))
            {
                m.List.Add(list[r]);
                if (m.List.Count == 8) break;
            }
            m.List = m.List.Where(z => z.Id != escort.Id).DistinctBy(z => z.Id).Take(4).ToList();

            //ViewBag.BackGroundImage = $"{WorkLib.GetRandomNumber(2, 15)}.jpg";
            ViewBag.CanonicalUrl = GetCanonicalUrl();

            //ViewBag.SiteTitle = texts.Where(z => z.Position == $"SiteTitleProfile-{escort.EscortId}").Select(z => z.Description)
            //    .FirstOrDefault();
            //ViewBag.SiteDescription = texts.Where(z => z.Position == $"SiteDescriptionProfile-{escort.EscortId}").Select(z => z.Description)
            //    .FirstOrDefault();
            if (name == "Aleха")
            {
                ViewBag.SiteTitle = $"Alexa - one of our Las Vegas TS Escorts - Sin City Experience";
                ViewBag.SiteDescription = $"Alexa. TS escort service in Las Vegas, Nevada direct to your room - Sin City Experience";
            }
            else
            {
                ViewBag.SiteTitle = $"{escort.EscortName} – one of the finest Las Vegas Escorts. Choose one of our beautiful escorts – Sin City Experience";
                ViewBag.SiteDescription = $"{escort.EscortName}. Escort service in Las Vegas, Nevada direct to your room — Sin City Experience";
            }

            // Оголошуємо список breadcrumbs один раз
            var breadcrumbs = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Las Vegas Escorts", Url = "/" },
            };
            breadcrumbs.Add(new BreadcrumbItem { Name = @escort.EscortName, Url = "/"+ @escort.EscortName.ToLower() });
            ViewData["Breadcrumbs"] = breadcrumbs;

            switch (escort.EscortName)
            {
                case "Kara":
                    m.IsVideo = true;
                    m.VideoFile = "kara-sincity.mp4";
                    break;
                //case "Cami":
                //    m.IsVideo = true;
                //    m.VideoFile = "cami.mp4";
                //    break;
                case "Jade":
                    m.IsVideo = true;
                    m.VideoFile = "jade.mp4";
                    break;
                case "Cherie":
                    m.IsVideo = true;
                    m.VideoFile = "cherie.mp4";
                    break;
                case "Aleysa":
                    m.IsVideo = true;
                    m.VideoFile = "aleysa.mp4";
                    break;
                case "Anna":
                    m.IsVideo = true;
                    m.VideoFile = "anna.mp4";
                    break;
                case "Karina":
                    m.IsVideo = true;
                    m.VideoFile = "karina3.mp4";
                    break;
                case "Linda":
                    m.IsVideo = true;
                    m.VideoFile = "linda1.mp4";
                    break;
                case "Nika":
                    m.IsVideo = true;
                    m.VideoFile = "nika2.mp4";
                    break;
                case "Stefani":
                    m.IsVideo = true;
                    m.VideoFile = "stefani.mp4";
                    break;
                case "Maria":
                    m.IsVideo = true;
                    m.VideoFile = "maria-sincity.mp4";
                    break;
                case "Helena":
                    m.IsVideo = true;
                    m.VideoFile = "helena-sincity.mp4";
                    break;

                    //default:
                    //    return RedirectToAction("Error","Home");
            }

            return View(m);
        }


        private string GetCanonicalUrl()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                var request = _httpContextAccessor.HttpContext.Request;
                return string.Concat(
                    request.Scheme,
                    "://",
                    request.Host.ToUriComponent(),
                    request.PathBase.ToUriComponent(),
                    request.Path.ToUriComponent(),
                    request.QueryString.ToUriComponent());
            }

            return string.Empty;
        }
    }
}
