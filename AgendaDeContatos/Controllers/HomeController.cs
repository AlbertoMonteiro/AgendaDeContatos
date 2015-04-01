using System.Web.Mvc;
using System.Web.Security;
using AgendaDeContatos.Models;

namespace AgendaDeContatos.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Login(LoginModel login)
        {
            FormsAuthentication.SetAuthCookie(login.Usuario, true);
            return RedirectToAction("Index");
        }

        public ActionResult Logof()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
