using HomeWorkForMVC5.Models;
using System.Linq;
using System.Web.Mvc;

namespace HomeWorkForMVC5.Controllers
{
    public class 客戶清單Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: 客戶清單
        public ActionResult Index()
        {
            return View(db.客戶清單.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}