using HomeWorkForMVC5.Models;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;

namespace HomeWorkForMVC5.Controllers
{
    public class 客戶銀行資訊Controller : Controller
    {
        客戶銀行資訊Repository repo = RepositoryHelper.Get客戶銀行資訊Repository();
        客戶資料Repository _repo = RepositoryHelper.Get客戶資料Repository();

        /// <summary>
        /// 客戶銀行資訊管理
        /// </summary>
        /// <param name="search">帳戶名稱</param>
        public ActionResult Index(string search)
        {
            ViewBag.search = search;

            var data = this.repo.GetByKeyword(search, false);

            return View(data);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶銀行資訊 客戶銀行資訊 = this.repo.GetById(id);

            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }

            return View(客戶銀行資訊);
        }

        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(this._repo.All(false), "Id", "客戶名稱");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼,是否已刪除")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                this.repo.Add(客戶銀行資訊);

                this.repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(this._repo.All(false), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);

            return View(客戶銀行資訊);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶銀行資訊 客戶銀行資訊 = this.repo.GetById(id);

            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }

            ViewBag.客戶Id = new SelectList(this._repo.All(false), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼,是否已刪除")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                ((客戶資料Entities)this.repo.UnitOfWork.Context).Entry(客戶銀行資訊).State = EntityState.Modified;

                this.repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(this._repo.All(false), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);

            return View(客戶銀行資訊);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶銀行資訊 客戶銀行資訊 = this.repo.GetById(id);

            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }

            return View(客戶銀行資訊);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶銀行資訊 客戶銀行資訊 = this.repo.GetById(id);

            this.repo.Delete(客戶銀行資訊);

            ((客戶資料Entities)this.repo.UnitOfWork.Context).Entry(客戶銀行資訊).State = EntityState.Modified;

            this.repo.UnitOfWork.Commit();

            return RedirectToAction("Index");
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