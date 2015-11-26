using HomeWorkForMVC5.Models;
using System.Web.Mvc;

namespace HomeWorkForMVC5.Controllers
{
    public class 客戶清單Controller : Controller
    {
        客戶清單Repository repo = RepositoryHelper.Get客戶清單Repository();

        /// <summary>
        /// 客戶清單列表
        /// </summary>
        /// <param name="search">客戶名稱</param>
        public ActionResult Index(string search)
        {
            ViewBag.search = search;

            var data = this.repo.GetByKeyword(search);

            return View(data);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ((客戶資料Entities)this.repo.UnitOfWork.Context).Dispose();
            }
            base.Dispose(disposing);
        }
    }
}