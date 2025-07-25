using System;
using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using WebUi.Infrastructure;
using WebUi.lib;
using WebUi.Lib;
using WebUi.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WebUi.Controllers
{
    public class BookingController : BaseController
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IMapper _mapper;
        private readonly IEmailSender _email;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IActionContextAccessor _accessor;

#if DEBUG
        //private readonly List<string> _emailsList = new(new string[] { "pmx@ukr.net", "nc@ukr.net", "ncmail@ukr.net", "maxim.paramonov.ua@gmail.com" });
        //private readonly List<string> _emailsList = new(new string[] { "v.bokhonov@serverpipe.com", "v.bokhonov@gmail.com", "vbokhonov@outlook.com", "stef.haeb34@hotmail.com" });
        private readonly List<string> _emailsList = new(new string[] { "bookings@usescortagency.com", "support@usescortagency.com" });
#else
        private readonly List<string> _emailsList = new(new string[] { "support@usescortagency.com", "bookings@usescortagency.com" });
        //private readonly List<string> _emailsList = new(new string[] { "v.bokhonov@serverpipe.com", "v.bokhonov@gmail.com", "vbokhonov@outlook.com", "stef.haeb34@hotmail.com" });
#endif

        private static List<IpItem> IpList { get; set; } = new List<IpItem>();

        public BookingController(IEscortRepository escortRepository,
            IMapper mapper,
            IEmailSender email,
            IMenuRepository menuRepository,
            IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache,
            ITextRepository textRepository,
            ILogger<BookingController> logger, IActionContextAccessor accessor) : base(escortRepository, textRepository, menuRepository, memoryCache)
        {
            _logger = logger;
            _accessor = accessor;
            _mapper = mapper;
            _email = email;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

#if !DEBUG
                [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
#endif
        [Route("booking.php")]
        public async Task<IActionResult> Index()
        {
            var m = new BookingViewModel();

            var escorts = await GetAllEscorts();
            //var texts = await GetAllTexts();

            foreach (var p in escorts)
            {
                m.List.Add(_mapper.Map<BookingViewItem>(p));
            }

            //ViewBag.BackGroundImage = $"{WorkLib.GetRandomNumber(2, 14)}.jpg";
            ViewBag.CanonicalUrl = GetCanonicalUrl();

            ViewBag.SiteTitle = "Book Escort At SinCityExperience";
            ViewBag.SiteDescription = "Book Escort. The hottest girls in Las Vegas direct to your room — SinCityExperience";
            
            // Оголошуємо список breadcrumbs один раз
            var breadcrumbs = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Las Vegas Escorts", Url = "/" },
            };
            breadcrumbs.Add(new BreadcrumbItem { Name = "Booking", Url = "/booking.php" });
            ViewData["Breadcrumbs"] = breadcrumbs;

            //ViewBag.MenuEscorts = await GetAllMenu();
            //ViewBag.GoogleAnalyticsObject = texts.Where(z => z.Position == "GoogleAnalyticsObject").Select(z => z.Description)
            //    .FirstOrDefault();

            return View(m);
        }

#if !DEBUG
                [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
#endif
        [Route("contact.php")]
        public async Task<IActionResult> Contact()
        {
            //var texts = await GetAllTexts();

            //ViewBag.BackGroundImage = $"{WorkLib.GetRandomNumber(2, 14)}.jpg";
            ViewBag.CanonicalUrl = GetCanonicalUrl();

            ViewBag.SiteTitle = "Contact SinCityExperience Agency";
            ViewBag.SiteDescription = "Contact Us. The hottest girls in Las Vegas direct to your room — SinCityExperience";

            // Оголошуємо список breadcrumbs один раз
            var breadcrumbs = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Las Vegas Escorts", Url = "/" },
            };
            breadcrumbs.Add(new BreadcrumbItem { Name = "Contact", Url = "/contact.php" });
            ViewData["Breadcrumbs"] = breadcrumbs;

            //ViewBag.MenuEscorts = await GetAllMenu();
            //ViewBag.GoogleAnalyticsObject = texts.Where(z => z.Position == "GoogleAnalyticsObject").Select(z => z.Description)
            //    .FirstOrDefault();

            return View();
        }

#if !DEBUG
                [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
#endif
        [Route("faq.php")]
        public IActionResult Faq()
        {
            ViewBag.CanonicalUrl = GetCanonicalUrl();

            ViewBag.SiteTitle = "Frequently Asked Questions — SinCityExperience";
            ViewBag.SiteDescription = "Frequently Asked Questions. The hottest girls in Las Vegas direct to your room — SinCityExperience";
            
            // Оголошуємо список breadcrumbs один раз
            var breadcrumbs = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Las Vegas Escorts", Url = "/" },
            };
            breadcrumbs.Add(new BreadcrumbItem { Name = "FAQ", Url = "/faq.php" });
            ViewData["Breadcrumbs"] = breadcrumbs;

            return View();
        }

#if !DEBUG
                [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
#endif
        [Route("sitemap.php")]
        public IActionResult SiteMap()
        {
            ViewBag.CanonicalUrl = GetCanonicalUrl();

            ViewBag.SiteTitle = "Site Map - SinCityExperience agency";
            ViewBag.SiteDescription = "Navigate pages of SinCityExperience here on our Site Map. You'll find full list of links to pages here on sincityexperience.com";
            
            // Оголошуємо список breadcrumbs один раз
            var breadcrumbs = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Las Vegas Escorts", Url = "/" },
            };
            breadcrumbs.Add(new BreadcrumbItem { Name = "Sitemap", Url = "/sitemap.php" });
            ViewData["Breadcrumbs"] = breadcrumbs;

            return View();
        }

#if !DEBUG
                [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
#endif
        [Route("about.php")]
        public IActionResult About()
        {
            //var texts = await GetAllTexts();

            ViewBag.BackGroundImage = $"{WorkLib.GetRandomNumber(2, 14)}.jpg";
            ViewBag.CanonicalUrl = GetCanonicalUrl();

            ViewBag.SiteTitle = "About Escort Agency In Las Vegas SinCityExperience";
            ViewBag.SiteDescription = "We, as an escort agency in Las Vegas take pride in keeping a high rate of loyal clients. Call us now - SinCityExperience";

            // Оголошуємо список breadcrumbs один раз
            var breadcrumbs = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Las Vegas Escorts", Url = "/" },
            };
            breadcrumbs.Add(new BreadcrumbItem { Name = "About", Url = "/about.php" });
            ViewData["Breadcrumbs"] = breadcrumbs;

            //ViewBag.MenuEscorts = await GetAllMenu();
            //ViewBag.GoogleAnalyticsObject = texts.Where(z => z.Position == "GoogleAnalyticsObject").Select(z => z.Description)
            //    .FirstOrDefault();

            return View();
        }

#if !DEBUG
                [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
#endif
        [Route("terms-of-service.php")]
        public async Task<IActionResult> TermsOfService()
        {
            var texts = await GetAllTexts();

            ViewBag.BackGroundImage = $"{WorkLib.GetRandomNumber(2, 14)}.jpg";
            ViewBag.CanonicalUrl = GetCanonicalUrl();

            // Оголошуємо список breadcrumbs один раз
            var breadcrumbs = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Las Vegas Escorts", Url = "/" },
            };
            breadcrumbs.Add(new BreadcrumbItem { Name = "Terms Of Service", Url = "/terms-of-service.php" });
            ViewData["Breadcrumbs"] = breadcrumbs;

            ViewBag.SiteTitle = texts.Where(z => z.Position == "SiteTitleContact").Select(z => z.Description)
                .FirstOrDefault();
            ViewBag.SiteDescription = texts.Where(z => z.Position == "SiteDescriptionContact").Select(z => z.Description)
                .FirstOrDefault();

            //ViewBag.MenuEscorts = await GetAllMenu();
            ViewBag.GoogleAnalyticsObject = texts.Where(z => z.Position == "GoogleAnalyticsObject").Select(z => z.Description)
                .FirstOrDefault();

            return View();
        }

#if !DEBUG
                [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
#endif
        [Route("join.php")]
        public IActionResult Employment()
        {
            var m = new EmploymentForm();
            
            ViewBag.CanonicalUrl = GetCanonicalUrl();

            ViewBag.SiteTitle = "Join Sin City — Sin City Experience";
            ViewBag.SiteDescription = "Join Sin City. The hottest girls in Las Vegas direct to your room — Sin City Experience";
            
            // Оголошуємо список breadcrumbs один раз
            var breadcrumbs = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Las Vegas Escorts", Url = "/" },
            };
            breadcrumbs.Add(new BreadcrumbItem { Name = "Join", Url = "/join.php" });
            ViewData["Breadcrumbs"] = breadcrumbs;

            return View(m);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("thankyou")]
        public async Task<IActionResult> ThankYou(BookingForm m)
        {
            if(!IpCheck()) return RedirectToAction("Error","Home");

            var escorts = await GetAllEscorts();
            var body = string.Empty;
            if (m.escortname != 0)
            {

                var t = escorts
                    .Where(z => z.Id == m.escortname).Select(z => z.EscortName).First();
                body = $"Preferred Escort: {t}<br/>";
            }
            if (m.escortname1 != 0)
            {
                var t = escorts
                    .Where(z => z.Id == m.escortname1).Select(z => z.EscortName).First();
                body = $"{body}<br/>Alternate Escort: {t}<br/>";
            }

            body += m.ToString();

            foreach (var email in _emailsList)
            {
                await _email.SendEmailAsync(email, Constants.SiteName, body);
            }

            //ViewBag.BackGroundImage = $"{WorkLib.GetRandomNumber(2, 14)}.jpg";
            ViewBag.CanonicalUrl = GetCanonicalUrl();

            ViewBag.SiteTitle = "";
            ViewBag.SiteDescription = "";

            //ViewBag.MenuEscorts = await GetAllMenu();

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("ThankYouContact")]
        public async Task<IActionResult> ThankYouContact(ContactUsForm m)
        {
            if(!IpCheck()) return RedirectToAction("Error","Home");

            foreach (var email in _emailsList)
            {
                await _email.SendEmailAsync(email, Constants.SiteName, m.ToString());
            }

            ViewBag.BackGroundImage = $"{WorkLib.GetRandomNumber(2, 14)}.jpg";
            ViewBag.CanonicalUrl = GetCanonicalUrl();

            ViewBag.SiteTitle = "";
            ViewBag.SiteDescription = "";

            ViewBag.MenuEscorts = await GetAllMenu();

            return View("ThankYou");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("ThankYouEmployment")]
        public async Task<IActionResult> ThankYouEmployment(EmploymentForm m)
        {
            if(!IpCheck()) return RedirectToAction("Error","Home");

            if (m.FileUpload != null && m.FileUpload.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads, m.FileUpload.FileName);
                await using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await m.FileUpload.CopyToAsync(fileStream);
                }

                foreach (var email in _emailsList)
                {
                    await _email.SendEmailFileAsync(email, Constants.SiteName, m.ToString(), filePath);
                }
            }
            else
            {
                foreach (var email in _emailsList)
                {
                    await _email.SendEmailAsync(email, Constants.SiteName, m.ToString());
                }
            }

            ViewBag.BackGroundImage = $"{WorkLib.GetRandomNumber(2, 14)}.jpg";
            ViewBag.CanonicalUrl = GetCanonicalUrl();

            ViewBag.SiteTitle = "";
            ViewBag.SiteDescription = "";

            ViewBag.MenuEscorts = await GetAllMenu();

            return View("ThankYou");
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

        private bool IpCheck()
        {
            var ip = _accessor.ActionContext.HttpContext.Connection.RemoteIpAddress?.ToString();
            var item = IpList.FirstOrDefault(z => z.Ip == ip);
            if (item == null)
            {
                IpList.Add(new IpItem
                {
                    Ip = ip, DateTime = DateTime.Now, Count = 1
                });
                return true;
            }
            else
            {
                if (item.DateTime < DateTime.Now.AddHours(-1))
                {
                    item.Count = 1;
                    item.DateTime = DateTime.Now;
                    return true;
                }
                if (item.Count >= 2)
                {
                    item.Count++;
                    return false;
                }

                item.Count++;
                item.DateTime = DateTime.Now;
                return true;
            }
        }
    }

    public class BookingViewModel
    {
        public List<BookingViewItem> List { get; set; } = new List<BookingViewItem>();
    }

    public class BookingViewItem
    {
        public int Id { get; set; }
        public string EscortName { get; set; }
    }

    public class BookingForm
    {
        public int escortname { get; set; }
        public int escortname1 { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string hotelname { get; set; }
        public string hotelroom { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string request { get; set; }
        public override string ToString()
        {
            return $"First Name: {this.firstname}<br/>Last Name: {this.lastname}<br/>" +
                   $"Hotel Name: {this.hotelname}<br/>Room Number: {this.hotelroom}<br/>" +
                   $"Email: {this.email}<br/>Phone No.: {this.phone}<br/>" +
                   $"Special Request: {this.request}";
        }
    }

    public class ContactUsForm
    {
        public string name { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public override string ToString()
        {
            return $"Name: {this.name}<br/>Email: {this.email}<br/>" +
                   $"Subject: {this.subject}<br/>Your Message: {this.message}";
        }
    }

    public class EmploymentForm
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int age { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string sdescription { get; set; }
        //public string file_name { get; set; }
        public IFormFile FileUpload { get; set; }
        public override string ToString()
        {
            return $"First Name: {this.firstname}<br/>Last Name: {this.lastname}<br/>" +
                   $"Age: {this.age}<br/>Email: {this.email}<br/>" +
                   $"Phone No.: {this.phone}<br/>Short Description: {this.sdescription}";
        }
    }

    public class IpItem
    {
        public DateTime DateTime { get; set; }
        public string Ip { get; set; }
        public int Count { get; set; }
    }
}
