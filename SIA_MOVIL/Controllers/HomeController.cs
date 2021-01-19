using System.Web.Mvc;

namespace SIA_MOVIL.Controllers
{
    [AutorizeSession]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}