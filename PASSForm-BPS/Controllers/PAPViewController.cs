using Microsoft.AspNetCore.Mvc;

namespace PASSForm_BPS.Controllers
{
    public class PAPViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Submitted()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create(string RequestId, string PAPType)
        {
            if (RequestId == null)
            {
                return PartialView("Create_PartialView");
            }
            else
            {
                return View();
             }

    }
    }
}
