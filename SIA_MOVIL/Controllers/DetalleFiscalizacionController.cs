using System.Web.Mvc;

namespace SIA_MOVIL.Controllers
{
    [AutorizeSession]
    public class DetalleFiscalizacionController : Controller
    {
        // GET: DetalleFiscalizacion
        public ActionResult Index()
        {
            return View();
        }
    }
}