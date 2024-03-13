using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUi.Lib;

namespace WebUi.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

#if !DEBUG
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
#endif
        [Route("{name}")]
        public IActionResult Index(string name)
        {
            ViewBag.CanonicalUrl = GetCanonicalUrl();

            switch (name)
            {
                case "are-escorts-legal.php":
                    ViewBag.SiteDescription = "Click here and read are escorts in Las Vegas really legal or not. Find surprising fact about escort business in Las Vegas on Sin City Experience Blog";
                    ViewBag.SiteTitle = "Are Escorts In Las Vegas Really Legal? Sin City Experience Blog";
                    return View("are-escorts-legal");

                case "fun-with-escorts.php":
                    ViewBag.SiteDescription = "Click here and read are escorts in Las Vegas really legal or not. Find surprising fact about escort business in Las Vegas on Sin City Experience Blog";
                    ViewBag.SiteTitle = "Find ultimate list on what to do with your escort girl to have a really great time. Sin City Experience Blog";
                    return View("fun-with-escort");

                case "escort-hotels.php":
                    ViewBag.SiteDescription = "Find out list of the most popular and escort friendly hotels in Las Vegas. You'll find a place for any budget - Sin City Experience Blog";
                    ViewBag.SiteTitle = "The Best Escort Friendly Hotels In Las Vegas. Sin City Experience Blog";
                    return View("escort-hotels");

                case "newbies-guide-to-escorts.php":
                    ViewBag.SiteDescription = "Hiring an escort in Las Vegas is a great way to enhance the experience that an individual is able to have while in the city. Sin City Experience Blog";
                    ViewBag.SiteTitle = "The Newbies Guide to Escorts in Las Vegas. Sin City Experience Blog";
                    return View("newbies-guide-to-escorts");

                case "how-do-escort-services-work.php":
                    ViewBag.SiteDescription = "How do escort services work? There are many Las Vegas escort services in the market today. Choosing the best one could be hard, but here, we will tell you how. Sin City Experience Blog";
                    ViewBag.SiteTitle = "How do escort services work in Las Vegas. Sin City Experience Blog";
                    return View("how-do-escort-services-work");

                case "top-25-playboy-covers.php":
                    ViewBag.SiteDescription = "Here we want to refresh your memories about Top 25 Playboy covers since 1953 up to now. Sin City Experience Blog";
                    ViewBag.SiteTitle = "Top 25 Playboy covers since 1953 up to now. Sin City Experience Blog";
                    return View("top-25-playboy-covers");

                case "pretty-lady-escorts.php":
                    ViewBag.SiteDescription = "Pretty lady escorts in Las Vegas. Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "Pretty lady escorts in Las Vegas. Sin City Experience Blog";
                    return View("pretty-lady-escorts");

                case "escorts-in-the-usa.php":
                    ViewBag.SiteDescription = "Escorts in the USA – Do You Have The Right One? Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "Escorts in the USA – Do You Have The Right One? Sin City Experience Blog";
                    return View("escorts-in-the-usa");

                case "vegas-the-home-of-beautiful-escort-girls.php":
                    ViewBag.SiteDescription = "Vegas the home of beautiful escort girls. Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "Vegas the home of beautiful escort girls. Sin City Experience Blog";
                    return View("vegas-the-home-of-beautiful-escort-girls");

                case "have-fun-with-large-breasted-escorts.php":
                    ViewBag.SiteDescription = "Have fun with large breasted escorts. Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "Have fun with large breasted escorts. Sin City Experience Blog";
                    return View("have-fun-with-large-breasted-escorts");

                case "gorgeous-escorts-in-las-vegas.php":
                    ViewBag.SiteDescription = "Gorgeous escorts in Las Vegas. Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "Gorgeous escorts in Las Vegas. Sin City Experience Blog";
                    return View("gorgeous-escorts-in-las-vegas");

                case "how-to-find-escorts-on-craiglist.php":
                    ViewBag.SiteDescription = "How to find escorts on Craigslist. Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "How to find escorts on Craigslist. Sin City Experience Blog";
                    return View("how-to-find-escorts-on-craiglist");

                case "high-class-escorts-for-a-weekend-in-vegas.php":
                    ViewBag.SiteDescription = "High class escorts for a weekend in Vegas. Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "High class escorts for a weekend in Vegas. Sin City Experience Blog";
                    return View("high-class-escorts-for-a-weekend-in-vegas");

                case "the-party-never-ends-with-delight-escorts-in-vegas.php":
                    ViewBag.SiteDescription = "The party never ends with delight escorts in Vegas. Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "The party never ends with delight escorts in Vegas. Sin City Experience Blog";
                    return View("the-party-never-ends-with-delight-escorts-in-vegas");

                case "go-with-escort.php":
                    ViewBag.SiteDescription =
                        "Find ultimate list of places where you can take your escort girl. You'll be surprised with some of places listed in this article - Sin City Experience Blog";
                    ViewBag.SiteTitle = "Ideas On Where To Go With Las Vegas Escort Girl. Sin City Experience Blog";
                    return View("go-with-escort");

                case "hire-agency-escort.php":
                    ViewBag.SiteDescription =
                        "Wondering why it is better to hire escorts from trusted agency in Las Vegas? Read here and stay safe - Sin City Experience Blog";
                    ViewBag.SiteTitle = "Find Why It Is Better To Hire Las Vegas Escorts From Agency. Sin City Experience Blog";
                    return View("hire-agency-escort");

                case "what-to-watch-out-for-when-looking-for-an-escort.php":
                    ViewBag.SiteDescription =
                        "What to watch out for when looking for an petite escort. Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "What to watch out for when looking for an petite escort. Sin City Experience Blog";
                    return View("what-to-watch-out-for-when-looking-for-an-escort");

                case "reasons-for-hiring-an-flawless-escorts-in-las-vegas.php":
                    ViewBag.SiteDescription =
                        "What to watch out for when looking for an petite escort. Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "What to watch out for when looking for an petite escort. Sin City Experience Blog";
                    return View("reasons-for-hiring-an-flawless-escorts-in-las-vegas");

                case "your-nightlife-guide-to-las-vegas.php":
                    ViewBag.SiteDescription =
                        "Your nightlife guide to Las Vegas. Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "Your nightlife guide to Las Vegas. Sin City Experience Blog";
                    return View("your-nightlife-guide-to-las-vegas");

                case "the-lowdown-on-vegas-nightlife.php":
                    ViewBag.SiteDescription =
                        "The lowdown on Vegas nightlife. Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "The lowdown on Vegas nightlife. Sin City Experience Blog";
                    return View("the-lowdown-on-vegas-nightlife");

                case "wondering-where-to-find-escorts-in-las-vegas.php":
                    ViewBag.SiteDescription =
                        "Wondering where to find escorts in Vegas? Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "Wondering where to find escorts in Vegas? Sin City Experience Blog";
                    return View("wondering-where-to-find-escorts-in-las-vegas");

                case "sincityexperience-is-now-accepting-bitcoins.php":
                    ViewBag.SiteDescription =
                        "Sin City Experience is now accepting bitcoins. Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "Sin City Experience is now accepting bitcoins. Sin City Experience Blog";
                    return View("sincityexperience-is-now-accepting-bitcoins");

                case "how-to-pic-up-girls-in-las-vegas.php":
                    ViewBag.SiteDescription =
                        "How to pick up girls in Las Vegas? Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "How to pick up girls in Las Vegas. Sin City Experience Blog";
                    return View("how-to-pic-up-girls-in-las-vegas");

                case "how-to-get-an-escort-in-las-vegas.php":
                    ViewBag.SiteDescription =
                        "How To Get An Escort In Las Vegas? Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "How To Get An Escort In Las Vegas. Sin City Experience Blog";
                    return View("how-to-get-an-escort-in-las-vegas");

                case "how-much-are-las-vegas-escort-prices.php":
                    ViewBag.SiteDescription =
                        "How Much Are Las Vegas Escort Prices? Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "How Much Are Las Vegas Escort Prices. Sin City Experience Blog";
                    return View("how-much-are-las-vegas-escort-prices");

                case "a-las-vegas-guide-for-man.php":
                    ViewBag.SiteDescription =
                        "A Las Vegas guide for man. Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "A Las Vegas guide for man. Sin City Experience Blog";
                    return View("a-las-vegas-guide-for-man");

                case "is-it-easy-to-pick-up-girls-in-vegas.php":
                    ViewBag.SiteDescription =
                        "Is it easy to pick up girls in Vegas. Find the most interesting articles at Sin City Experience Blog";
                    ViewBag.SiteTitle = "Is it easy to pick up girls in Vegas. Sin City Experience Blog";
                    return View("is-it-easy-to-pick-up-girls-in-vegas");
                default:
                    return Redirect("~/404.php");
            }
        }

//#if !DEBUG
//        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, NoStore = false)]
//#endif
//        [Route("page-2")]
//        public IActionResult Page2()
//        {
//            ViewBag.BackGroundImage = $"{WorkLib.GetRandomNumber(2, 14)}.jpg";
//            ViewBag.CanonicalUrl = GetCanonicalUrl();
//            ViewBag.SiteDescription = "Find the most relevant articles about erotic and sensual massage in Las Vegas on VegasMassageGirls";
//            ViewBag.SiteTitle = "Erotic Massage Blog on VegasMassageGirls";
//            return View();
//        }

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
