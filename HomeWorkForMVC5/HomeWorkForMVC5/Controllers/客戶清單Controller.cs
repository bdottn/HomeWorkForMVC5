using HomeWorkForMVC5.Models;
using System.Linq;
using System.Web.Mvc;

namespace HomeWorkForMVC5.Controllers
{
    public class 客戶清單Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        /// <summary>
        /// 客戶清單列表
        /// </summary>
        /// <param name="search">客戶名稱</param>
        public ActionResult Index(string search)
        {
            ViewBag.search = search;

            var data = db.客戶清單.AsQueryable();
            if (string.IsNullOrEmpty(search) == false)
            {
                data = data.Where(d => d.客戶名稱.Contains(search));
            }

            return View(data);
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