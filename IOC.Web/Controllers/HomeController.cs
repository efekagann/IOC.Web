using IOC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IOC.Web.Controllers
{
    public class HomeController : Controller
    {

        //#region Singleton
        //private readonly ISingletonDateService _singletonDateService;

        //public HomeController(ISingletonDateService singletonDateService)
        //{
        //    _singletonDateService = singletonDateService;
        //}

        ////Singleton olduðu için üstte bir nesne oluþturuyoruz altta tekrar bir nesne oluþtuðunda onu ciddiye almayacak.
        ////Alttaki nesne örneði test amaçlý oluþturuldu.
        ////Bir uygulama boyunca bir defa nesne oluþturuluyor.
        //public IActionResult Index([FromServices] ISingletonDateService singletonDateService2)
        //{
        //    ViewBag.time1 = _singletonDateService.GetDateTime.TimeOfDay.ToString();
        //    ViewBag.time2 = singletonDateService2.GetDateTime.TimeOfDay.ToString();

        //    return View();
        //}
        //#endregion

        //#region Scoped
        //private readonly IScopedDateService _scopedDateService;

        //public HomeController(IScopedDateService scopedDateService)
        //{
        //    _scopedDateService = scopedDateService;
        //}

        ////Bir requestte bir nesne örneði oluþacak ve o request boyunca o nesne örneði kullanýlacak.
        ////Sayfayý yenilediðimiz zaman yeni bir request isteði atacaðýmýz için yeni bir nesne oluþur Singleton da sayfa yenilendiðinde yeni nesne oluþmaz.
        //public IActionResult Index([FromServices] IScopedDateService scopedDateService)
        //{
        //    ViewBag.scoped_Time1 = _scopedDateService.GetDateTime.TimeOfDay.ToString();
        //    ViewBag.scoped_Time2 = scopedDateService.GetDateTime.TimeOfDay.ToString();

        //    return View();
        //}
        //#endregion

        #region Transient
        private readonly ITransientDateService _transientDateService;
        private readonly ISingletonDateService _singletonDateService;
        private readonly IScopedDateService _scopedDateService;
        public HomeController(ITransientDateService transientDateService, ISingletonDateService singletonDateService, IScopedDateService scopedDateService)
        {
            _transientDateService = transientDateService;
            _singletonDateService = singletonDateService;
            _scopedDateService = scopedDateService;
        }

        //Her bir enjekte de yeni bir tane nesne örneði oluþturdu.
        public IActionResult Index([FromServices] ITransientDateService transientDateService, [FromServices] IScopedDateService scopedDateService, [FromServices] ISingletonDateService singletonDateService2)
        {
            //Transient
            ViewBag.transient_Time1 = _transientDateService.GetDateTime.TimeOfDay.ToString();
            ViewBag.transient_Time2 = transientDateService.GetDateTime.TimeOfDay.ToString();

            //Scoped
            ViewBag.scoped_Time1 = _scopedDateService.GetDateTime.TimeOfDay.ToString();
            ViewBag.scoped_Time2 = scopedDateService.GetDateTime.TimeOfDay.ToString();

            //Singleton
            ViewBag.singleton_Time1 = _singletonDateService.GetDateTime.TimeOfDay.ToString();
            ViewBag.singleton_Time2 = singletonDateService2.GetDateTime.TimeOfDay.ToString();

            return View();
        }
        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
