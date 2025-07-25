using System;
using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Infrastructure;
using WebUi.Lib;
using WebUi.Models;

namespace WebUi.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _env;

        public HomeController(IEscortRepository escortRepository,
            ITextRepository textRepository,
            IMenuRepository menuRepository,
            IMapper mapper,
            IWebHostEnvironment env,
            IMemoryCache memoryCache,
            IHttpContextAccessor httpContextAccessor,
            ILogger<HomeController> logger) : base(escortRepository, textRepository, menuRepository, memoryCache)
        {
            _logger = logger;
            _mapper = mapper;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

#if !DEBUG
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
#endif
        public async Task<IActionResult> Index()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

            if (baseUrl.Contains("fetish.sincityexperience.com"))
                return Redirect("https://www.sincityexperience.com/fetish.php");
            if (baseUrl.Contains("backpage.sincityexperience.com"))
                return Redirect("https://www.sincityexperience.com/backpage.php");

            var list = await GetAllTexts();

            ViewBag.BackGroundImage = "home_bg.jpg";
            ViewBag.CanonicalUrl = GetCanonicalUrl();
            ViewBag.SiteTitle = list
                .Where(z => z.Position == "SiteTitleHome").Select(z => z.Description)
                .FirstOrDefault();
            ViewBag.SiteDescription = list
                .Where(z => z.Position == "SiteDescriptionHome").Select(z => z.Description)
                .FirstOrDefault();

            return View();
        }

        [Route("escort-profile.php")]
        public async Task<IActionResult> Redirect301(int escort_id)
        {
            var escorts = await GetAllEscorts();

            string name;
            var q = escorts.Where(z => z.SiteName == Constants.SiteName);
            var f = escorts.Any(z => z.EscortId == escort_id);
            if (f) name = escorts.Where(z => z.EscortId == escort_id).Select(z => z.EscortName).First();
            else
            {
                var r = new Random();
                name = escorts.Skip(r.Next(0, q.Count())).Take(1).Select(z => z.EscortName).First();
            }

            //https://metanit.com/sharp/aspnet5/5.5.php
            return RedirectPermanent($"profile/{name.ToLower()}.php");
        }

        [Route("massage-girl-profile.php")]
        public async Task<IActionResult> Redirect301A(int escort_id)
        {
            var escorts = await GetAllEscorts();

            string name;
            var q = escorts.Where(z => z.SiteName == Constants.SiteName);
            var f = escorts.Any(z => z.EscortId == escort_id);
            if (f) name = escorts.Where(z => z.EscortId == escort_id).Select(z => z.EscortName).First();
            else
            {
                var r = new Random();
                name = escorts.Skip(r.Next(0, q.Count())).Take(1).Select(z => z.EscortName).First();
            }

            //https://metanit.com/sharp/aspnet5/5.5.php
            return RedirectPermanent($"profile/{name.ToLower()}.php");
        }

        [Route("escort-profile-new.php")]
        public async Task<IActionResult> Redirect301B(int escort_id)
        {
            var escorts = await GetAllEscorts();

            string name;
            var q = escorts.Where(z => z.SiteName == Constants.SiteName);
            var f = escorts.Any(z => z.EscortId == escort_id);
            if (f) name = escorts.Where(z => z.EscortId == escort_id).Select(z => z.EscortName).First();
            else
            {
                var r = new Random();
                name = escorts.Skip(r.Next(0, q.Count())).Take(1).Select(z => z.EscortName).First();
            }

            //https://metanit.com/sharp/aspnet5/5.5.php
            return RedirectPermanent($"profile/{name.ToLower()}.php");
        }

#if !DEBUG
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
#endif
        [Route("all.php")]
        public IActionResult All(string name)
        {
            ViewBag.BackGroundImage = "home_bg.jpg";
            ViewBag.CanonicalUrl = GetCanonicalUrl();
            ViewBag.SiteTitle = "All Escort Girls You Can Hire At Sin City Experience";
            ViewBag.SiteDescription = "Browse through huge selection of Las Vegas escort girls - Sin City Experience.";

            return View();
        }

        [Route("test.php")]
        public IActionResult Test()
        {
            ViewData["Breadcrumbs"] = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string> { { "name", "Home" }, { "url", "/" } },
            new Dictionary<string, string> { { "name", "Test" }, { "url", "/test.php" } }
        };
            return View();
        }

        [Route("test1.php")]
        public IActionResult Test1()
        {
            var breadcrumbs = new List<BreadcrumbItem>
        {
            new BreadcrumbItem { Name = "Home", Url = "/" },
            new BreadcrumbItem { Name = "Test", Url = "/test.php" },
            new BreadcrumbItem { Name = "Test1", Url = "" }
        };

        ViewData["Breadcrumbs"] = breadcrumbs;
            return View();
        }
        [Route("test2.php")]
        public IActionResult test2()
        {
            ViewData["Breadcrumbs"] = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string> { { "name", "Home" }, { "url", "/" } },
            new Dictionary<string, string> { { "name", "Test" }, { "url", "/test.php" } },
            new Dictionary<string, string> { { "name", "Test2" }, { "url", "" } }        };
            return View();
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

        //#if !DEBUG
        //        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
        //#endif
        //        [Route("services.php")]
        //        public IActionResult Services()
        //        {
        //            ViewBag.BackGroundImage = $"{WorkLib.GetRandomNumber(2, 14)}.jpg";
        //            ViewBag.CanonicalUrl = GetCanonicalUrl();
        //            ViewBag.SiteDescription = "There are different massage services for different purposes. Here, on VegasMassageGirls, we offer all of them. Call now!";
        //            ViewBag.SiteTitle = "Massage Services In Las Vegas At VegasMassageGirls";
        //            return View();
        //        }

#if !DEBUG
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
#endif
        [Route("about-us.php")]
        public async Task<IActionResult> AboutUs()
        {
            var texts = await GetAllTexts();
            var m = new AboutUsViewModel
            {
                PositionAbout = texts.Where(z => z.Position == "PositionAbout")
                    .Select(z => z.Description).Single(),
                SiteDescriptionAbout = texts.Where(z => z.Position == "SiteDescriptionPageAbout")
                    .Select(z => z.Description).Single()
            };

            ViewBag.BackGroundImage = $"{WorkLib.GetRandomNumber(2, 15)}.jpg";
            ViewBag.CanonicalUrl = GetCanonicalUrl();
            ViewBag.SiteTitle = texts.Where(z => z.Position == "SiteTitleAboutUs")
                .Select(z => z.Description).FirstOrDefault();
            ViewBag.SiteDescription = texts.Where(z => z.Position == "SiteDescriptionAboutUs").Select(z => z.Description)
                .FirstOrDefault();

            //ViewBag.MenuEscorts = await GetAllMenu();

            ViewBag.GoogleAnalyticsObject = texts.Where(z => z.Position == "GoogleAnalyticsObject").Select(z => z.Description)
               .FirstOrDefault();

            return View(m);
        }


#if !DEBUG
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
#endif
        [Route("trafficking.php")]
        public async Task<IActionResult> Trafficking()
        {
            var texts = await GetAllTexts();


            ViewBag.BackGroundImage = $"{WorkLib.GetRandomNumber(2, 15)}.jpg";
            ViewBag.CanonicalUrl = GetCanonicalUrl();
            ViewBag.SiteTitle = "Report Trafficking - SinCityExperience";
            ViewBag.SiteDescription = "Report Trafficking - SinCityExperience";

            //ViewBag.MenuEscorts = await GetAllMenu();

            ViewBag.GoogleAnalyticsObject = texts.Where(z => z.Position == "GoogleAnalyticsObject").Select(z => z.Description)
                .FirstOrDefault();

            return View();
        }

        public async Task<IActionResult> Massage(string name, bool isMassage)
        {
            var m = new MassageViewModel { IsMassage = isMassage };

            var escorts = await GetAllEscorts();
            var texts = await GetAllTexts();
            var list = new List<Escort>();
            var rnd = new Random();
            var massageName = string.Empty;
            switch (name)
            {
                case "asian-escorts":
                    list = escorts.Where(z => z.Nationality == "Asian").OrderByDescending(z => z.Id).ToList();
                    massageName = "Asian";
                    break;
                case "blonde-escorts":
                    list = escorts.Where(z => z.Hair == "Blonde").OrderByDescending(z => z.Id).ToList();
                    massageName = "Blonde";
                    break;
                case "black-escorts":
                    list = escorts.Where(z => z.Nationality == "Black").OrderByDescending(z => z.Id).ToList();
                    massageName = "Black";
                    break;
                case "vip-escorts":
                    foreach (var r in escorts.Select(p => rnd.Next(escorts.Count - 1)))
                    {
                        list.Add(escorts[r]);
                        if (list.Count == 12) break;
                    }
                    list = list.DistinctBy(z => z.EscortName).Take(8).ToList();
                    massageName = "Vip";
                    break;
            }

            var s = name.Replace("-", " ");
            m.EscortsNavTitle = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

            m.PositionMassageTitle = massageName;
            m.PositionMassageTop = texts.Where(z => z.Position == $"Position{massageName}Top").Select(z => z.Description)
                .FirstOrDefault();
            m.PositionMassageBottom = texts.Where(z => z.Position == $"Position{massageName}Bottom").Select(z => z.Description)
                .FirstOrDefault();
            ViewBag.SiteTitle = texts.Where(z => z.Position == $"SiteTitle{massageName}Massage").Select(z => z.Description)
                .FirstOrDefault();
            ViewBag.SiteDescription = texts.Where(z => z.Position == $"SiteDescription{massageName}Massage").Select(z => z.Description)
                .FirstOrDefault();

            foreach (var i in list.Select(p => _mapper.Map<HomeViewItem>(p)))
            {
                m.List.Add(i);
            }

            ViewBag.BackGroundImage = $"{WorkLib.GetRandomNumber(2, 14)}.jpg";
            ViewBag.CanonicalUrl = GetCanonicalUrl();
            ViewBag.MenuEscorts = await GetAllMenu();
            ViewBag.GoogleAnalyticsObject = texts.Where(z => z.Position == "GoogleAnalyticsObject").Select(z => z.Description)
                .FirstOrDefault();

            return View("Massage", m);
        }

        //#if !DEBUG
        //        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
        //#endif
        //        [Route("blog/{name}")]
        //        public async Task<IActionResult> BlogMassage(string name)
        //        {
        //            return await Massage(name.Substring(0, name.Length - 4), false);
        //        }

#if !DEBUG
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
#endif
        [Route("{name}")]
        public IActionResult Escorts(string name)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

            if (baseUrl.Contains("fetish") && name == "foot-fetish.php")
                return Redirect("https://www.sincityexperience.com/foot-fetish.php");

            ViewBag.CanonicalUrl = GetCanonicalUrl();

            // Оголошуємо список breadcrumbs один раз
            var breadcrumbs = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Las Vegas Escorts", Url = "/" },
            };

            switch (name)
            {
                case "asian-massage.php":
                    ViewBag.SiteDescription = "Come to us for authentic Thai massages, body treatments, and more! Our therapists are skilled professionals ready to give you top quality service - Sin City Experience.";
                    ViewBag.SiteTitle = " Las Vegas Asian Massage At Sin City Experience";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Asian Massage", Url = "/asian-massage.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs; 
                    return View("asian-massage");

                case "erotic-massage.php":
                    ViewBag.SiteDescription = "Do you want to have fun with our sexy girls? Our masseuses will give you amazing massages! You'll love our affordable prices - Sin City Experience.";
                    ViewBag.SiteTitle = "Las Vegas Erotic Massage At Sin City Experience";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Erotic Massage", Url = "/erotic-massage.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("erotic-massage");

                case "body-rubs.php":
                    ViewBag.SiteDescription = "Hire one of our amazing girls if you want fantastic Las Vegas body rubs services. You will not be disappointed with quality of our service – Sin City Experience.";
                    ViewBag.SiteTitle = "Las Vegas Body Rubs At Sin City Experience";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Body Rubs", Url = "/body-rubs.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("body-rubs");

                case "tantra-massage.php":
                    ViewBag.SiteDescription = "Get pampered by an experienced masseuse who knows just what she’s doing.We offer tantric massages, body wraps, erotic treatments and more - Sin City Experience.";
                    ViewBag.SiteTitle = "Las Vegas Tantra (Tantric) Massage At Sin City Experience";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Tantra Massage", Url = "/tantra-massage.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("tantra-massage");

                case "asian-escorts.php":
                    ViewBag.SiteDescription = "Call one of our fine Las Vegas Asian escorts now! The finest girls are waiting to have a fun night with you! We look forward to meeting you soon. Call now! Sin City Experience";
                    ViewBag.SiteTitle = "Asian Escorts Of Las Vegas At Sin City Experience";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Asian Escorts", Url = "/asian-escorts.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("asian-escorts");

                case "bachelor.php":
                    ViewBag.SiteDescription = "Our Las Vegas bachelor party escorts spell class, professionalism and exclusivity and are trained to be attentive and devoted to your needs — Sin City Experience";
                    ViewBag.SiteTitle = "Las Vegas Bachelor Party: Ultimate Planning Guide At Sin City Experience";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Bachelor Party", Url = "/bachelor.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("bachelor");

                case "big-booty-escorts.php":
                    ViewBag.SiteDescription = "Are you searching for a big booty escort in Las Vegas? We have the hottest curvaceous beauties Vegas has to offer. All you need to do is call Sin City Experience.";
                    ViewBag.SiteTitle = "Big Booty Escorts In Las Vegas — Sin City Experience";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Big Booty Escorts", Url = "/big-booty-escorts.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("big-booty-escorts");

                case "blonde-escorts.php":
                    ViewBag.SiteDescription = "Choose from one of our delightful blonde escorts in Las Vegas. They are here to satisfy your needs! Have a fun night during your stay in Vegas — call now! Sin City Experience";
                    ViewBag.SiteTitle = "Blonde Escorts Of Las Vegas At Sin City Experience";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Blonde Escorts", Url = "/blonde-escorts.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("blonde-escorts");

                case "black-escorts.php":
                    ViewBag.SiteDescription = "Call one of our fine Las Vegas black escorts now! We have the finest black girls in industry! Looking forward to meeting you soon. Call now! Sin City Experience";
                    ViewBag.SiteTitle = "Black Escorts Of Las Vegas At Sin City Experienc";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Black Escorts", Url = "/black-escorts.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("black-escorts");

                case "brunette-escorts.php":
                    ViewBag.SiteDescription = "View our large list of brunette escorts in Las Vegas. Choose from one of our hottest brunette escorts to enhance your Vegas stay! We have the finest girls in industry — call now! Sin City Experience";
                    ViewBag.SiteTitle = "Brunette Escorts Of Las Vegas At Sin City Experience";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Brunette Escorts", Url = "/brunette-escorts.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("brunette-escorts");

                case "red-head-escorts.php":
                    ViewBag.SiteDescription = "Lonely? Looking to find your dream redhead escort in Las Vegas? We have the finest girls in industry and they are here to satisfy your needs! Call now! Sin City Experience";
                    ViewBag.SiteTitle = "Redhead Escorts Of Las Vegas At Sin City Experience";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Red Head Escorts", Url = "/red-head-escorts.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("red-head-escorts");

                case "russian-escorts.php":
                    ViewBag.SiteDescription = "Call one of our fine Las Vegas russian escorts now! We have the finest girls in industry! We look forward to meeting you soon. Call now! Sin City Experience";
                    ViewBag.SiteTitle = "Russian Escorts Of Las Vegas At Sin City Experience";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Russian Escorts", Url = "/russian-escorts.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("russian-escorts");

                case "vip-escorts.php":
                    ViewBag.SiteDescription = "Search for unforgettable Las Vegas VIP escorts to entertain you during the day or night. Our delightful VIP escorts will enhance your Vegas vacation! Call now! Sin City Experience";
                    ViewBag.SiteTitle = "VIP Escorts Of Las Vegas At Sin City Experience Agency";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "VIP Escorts", Url = "/vip-escorts.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("vip-escorts");

                case "gfe-las-vegas.php":
                    ViewBag.SiteDescription = "You can easily call GFE Las Vegas for companionship while on vacation. Do not look for more - Sin City Experience";
                    ViewBag.SiteTitle = "Las Vegas Girlfriend Experience (GFE Escorts) At Sin City Experience";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "GFE Las Vegas", Url = "/gfe-las-vegas.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("gfe-las-vegas");

                case "las-vegas-escorts-for-couples.php":
                    ViewBag.SiteDescription = "Do you and your partner want to spice things up in the bedroom? These talented ladies know exactly how to help. Call us now for the ultimate Sin City Experience";
                    ViewBag.SiteTitle = "Couples Escorts Of Las Vegas At Sin City Experience";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Las Vegas Escorts For Couples", Url = "/las-vegas-escorts-for-couples.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("las-vegas-escorts-for-couples");

                case "las-vegas-happy-ending-massage.php":
                    ViewBag.SiteDescription = "Happy ending massage Las Vegas is very popular and sought after by various people for different type of occasions and events. If you need to relax or you want to rejuvenate, call us now and we will be of help to you - Sin City Experience";
                    ViewBag.SiteTitle = "Las Vegas Happy Ending Massage At Sin City Experience";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Las Vegas Escorts For Couples", Url = "/las-vegas-escorts-for-couples.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("las-vegas-happy-ending-massage");

                case "las-vegas-nuru-massage.php":
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Las Vegas Nuru Massage", Url = "/las-vegas-nuru-massage.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return RedirectPermanent($"nuru.php");

                case "nuru.php":
                    ViewBag.SiteDescription = "Massage is a personal and intimate experience that one can get from Nuru massage Las Vegas. Call now! Let us help you with your booking needs - Sin City Experience";
                    ViewBag.SiteTitle = "NURU Massage In Las Vegas At Sin City Experience";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Nuru", Url = "/nuru.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("las-vegas-nuru-massage");

                case "fetish.php":
                    ViewBag.SiteDescription = "Find professional BDSM and Fetish services in Las Vegas. Book your outcall fetish session with ultimate dominatrix of Las Vegas - Sin City Experience";
                    ViewBag.SiteTitle = "Las Vegas Femdom Dominatrix, Mistress, Nevada BDSM At Sin City Experience";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Fetish", Url = "/fetish.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("fetish");

                case "foot-fetish.php":
                    ViewBag.SiteDescription = "Las Vegas Foot Fetish Escorts At Sin City Experience";
                    ViewBag.SiteTitle = "Book Las Vegas foot fetish escorts for the pure enjoyment of pleasures. Let Sin City Experience help you enjoy the pleasures of Sin City";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Fetish", Url = "http://fetish.sincityexperience.com" });
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Foot Fetish", Url = "/foot-fetish.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("foot-fetish");

                case "backpage.php":
                    ViewBag.SiteDescription = "Here is the truth about escorts on Backpage Las Vegas. Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "Backpage Las Vegas Escorts At Sin City Experience";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Blog", Url = "/blog.php" });
                    breadcrumbs.Add(new BreadcrumbItem { Name = "Backpage", Url = "/backpage.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("backpage");
                case "ts-escorts.php":
                    ViewBag.SiteDescription = "Look through our collection of TS escorts in Las Vegas. Choose from one of our hottest trans sexual escorts to enhance your Vegas stay! Call Sin City Experience now!";
                    ViewBag.SiteTitle = "TS (Shemale) Escorts Of Las Vegas At Sin City Experience";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "TS Escorts", Url = "/ts-escorts.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("ts-escorts");
                case "massage-fbsm.php":
                    ViewBag.SiteDescription = "Experience ultimate relaxation and pampering with a full-body sensual massage in Las Vegas. FBSM is the perfect escape from everyday stress - Sin City Experience.";
                    ViewBag.SiteTitle = "Las Vegas FBSM At Sin City Experience.";
                    breadcrumbs.Add(new BreadcrumbItem { Name = "FBSM", Url = "/massage-fbsm.php" });
                    ViewData["Breadcrumbs"] = breadcrumbs;
                    return View("massage-fbsm");
                default:
                    return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> Profile(string name)
        {
            name = name.Substring(0, name.Length - 4);
            var escorts = await GetAllEscorts();
            var texts = await GetAllTexts();

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

            ViewBag.BackGroundImage = $"{WorkLib.GetRandomNumber(2, 15)}.jpg";
            ViewBag.CanonicalUrl = GetCanonicalUrl();

            //ViewBag.SiteTitle = texts.Where(z => z.Position == $"SiteTitleProfile-{escort.EscortId}").Select(z => z.Description)
            //    .FirstOrDefault();
            //ViewBag.SiteDescription = texts.Where(z => z.Position == $"SiteDescriptionProfile-{escort.EscortId}").Select(z => z.Description)
            //    .FirstOrDefault();

            ViewBag.SiteTitle = $"Masseuse {escort.EscortName} is experienced 24 years old girl - Vegas Massage Girls";
            ViewBag.SiteDescription = $"{escort.EscortName} is experienced {escort.Age} years old masseuse. She can provide for relaxing massage of all types. Call now to Vegas Massage Girls";

            //ViewBag.MenuEscorts = await GetAllMenu();
            ViewBag.GoogleAnalyticsObject = texts.Where(z => z.Position == "GoogleAnalyticsObject").Select(z => z.Description)
                .FirstOrDefault();

            return View("Profile", m);
        }


#if !DEBUG
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
#endif
        [Route("blog.php")]
        public IActionResult Blog()
        {
            ViewBag.CanonicalUrl = GetCanonicalUrl();
            ViewBag.SiteDescription = "Find the information you have to know about Las Vegas escorts — Sin City Experience";
            ViewBag.SiteTitle = "Blog — Sin City Experience";
            
            // Оголошуємо список breadcrumbs один раз
            var breadcrumbs = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Las Vegas Escorts", Url = "/" },
            }; 
            breadcrumbs.Add(new BreadcrumbItem { Name = "Blog", Url = "/blog.php" });
            ViewData["Breadcrumbs"] = breadcrumbs;

            return View("Blog");
        }

        //#if !DEBUG
        //        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
        //#endif
        //        [Route("blog/page-2.php")]
        //        public async Task<IActionResult> Page2()
        //        {
        //            ViewBag.BackGroundImage = $"{WorkLib.GetRandomNumber(2, 14)}.jpg";
        //            ViewBag.CanonicalUrl = GetCanonicalUrl();
        //            ViewBag.SiteDescription = "Find the most relevant articles about erotic and sensual massage in Las Vegas on VegasMassageGirls";
        //            ViewBag.SiteTitle = "Erotic Massage Blog on VegasMassageGirls";
        //            var texts = await GetAllTexts();
        //            ViewBag.GoogleAnalyticsObject = texts.Where(z => z.Position == "GoogleAnalyticsObject").Select(z => z.Description)
        //                .FirstOrDefault();
        //            return View("Page2");
        //        }

        [Route("robots.txt")]
        public ContentResult RobotsTxt()
        {
            var filePath = Path.Combine(_env.WebRootPath, "robots.txt");
            var s = System.IO.File.ReadAllText(filePath);
            return this.Content(s, "text/plain", Encoding.UTF8);
        }

        [Route("sitemap.xml")]
        public ContentResult SiteMap()
        {
            var filePath = Path.Combine(_env.WebRootPath, "sitemap.xml");
            var s = System.IO.File.ReadAllText(filePath);
            return this.Content(s, "text/plain", Encoding.UTF8);
        }

        private async Task<string> GetEscortsHeading(string position)
        {
            var texts = await GetAllTexts();
            return texts.Where(z => z.Position == position)
                .Select(z => z.Description).FirstOrDefault();
        }

        [Route("{seg1?}/{seg2}")]
        public IActionResult BadUrl()
        {
            string url = Request.Path;
            if (url.Contains("escorts-profile"))
            {
                url = url.Replace("escorts-profile", "profile");
                return RedirectPermanent(url);
            }
            return RedirectToAction("Error");
        }

        [Route("404.php")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            ViewBag.BackGroundImage = $"{WorkLib.GetRandomNumber(2, 14)}.jpg";
            ViewBag.CanonicalUrl = GetCanonicalUrl();

            ViewBag.SiteTitle = "";
            ViewBag.SiteDescription = "";

            Response.StatusCode = 404;

            ViewBag.MenuEscorts = await GetAllMenu();

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

    public class HomeViewModel
    {
        public string PositionHomeTopTitle { get; set; }
        public string PositionHomeTop { get; set; }
        public string PositionHomeBottom { get; set; }
        public List<HomeViewItem> List { get; set; } = new List<HomeViewItem>();
    }

    public class EscortsViewModel
    {
        public string EscortsHeading { get; set; }
        public string EscortsNavTitle { get; set; }
        public string PositionEscortsTop { get; set; }
        public string PositionEscortsBottom { get; set; }
        public List<HomeViewItem> List { get; set; } = new List<HomeViewItem>();
    }

    public class MassageViewModel
    {
        public string EscortsNavTitle { get; set; }
        public string PositionMassageTitle { get; set; }
        public string PositionMassageTop { get; set; }
        public string PositionMassageBottom { get; set; }
        public bool IsMassage { get; set; }
        public List<HomeViewItem> List { get; set; } = new List<HomeViewItem>();
    }

    public class BodyRubsViewModel
    {
        public string PositionBodyRubsTitle { get; set; }
        public string PositionBodyRubsTop { get; set; }
        public string PositionBodyRubsBottom { get; set; }
        public List<HomeViewItem> List { get; set; } = new List<HomeViewItem>();
    }

    public class AboutUsViewModel
    {
        public string PositionAbout { get; set; }
        public string SiteDescriptionAbout { get; set; }
        public List<HomeViewItem> List { get; set; } = new List<HomeViewItem>();
    }

    public class HomeViewItem : Escort
    {

    }
}
