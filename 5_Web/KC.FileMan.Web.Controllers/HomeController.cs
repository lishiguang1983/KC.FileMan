using Microsoft.AspNetCore.Mvc;

namespace KC.FileMan.Web.Controllers
{
    public class HomeController : BaseController
    {
        //private IProduceOrderApp produceOrderApp;
        //public HomeController(IProduceOrderApp produceOrderApp)
        //{
        //    this.produceOrderApp = produceOrderApp;
        //}

        //[DataSession]
        //public ActionResult Index(ProduceOrderQueryCriteria criteria)
        //{
        //    var result = produceOrderApp.GetList(criteria);
        //    return View();
        //}

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
