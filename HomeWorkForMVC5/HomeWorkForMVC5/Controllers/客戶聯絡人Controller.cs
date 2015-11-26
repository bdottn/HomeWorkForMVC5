using HomeWorkForMVC5.Models;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;

namespace HomeWorkForMVC5.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();
        客戶資料Repository _repo = RepositoryHelper.Get客戶資料Repository();

        /// <summary>
        /// 客戶聯絡人管理
        /// </summary>
        /// <param name="search">聯絡人姓名</param>
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

            客戶聯絡人 客戶聯絡人 = this.repo.GetById(id);

            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }

            return View(客戶聯絡人);
        }

        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(this._repo.All(false), "Id", "客戶名稱");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,是否已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                this.repo.Add(客戶聯絡人);

                this.repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(this._repo.All(false), "Id", "客戶名稱", 客戶聯絡人.客戶Id);

            return View(客戶聯絡人);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶聯絡人 客戶聯絡人 = this.repo.GetById(id);

            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }

            ViewBag.客戶Id = new SelectList(this._repo.All(false), "Id", "客戶名稱", 客戶聯絡人.客戶Id);

            return View(客戶聯絡人);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,是否已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                ((客戶資料Entities)this.repo.UnitOfWork.Context).Entry(客戶聯絡人).State = EntityState.Modified;

                this.repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(this._repo.All(false), "Id", "客戶名稱", 客戶聯絡人.客戶Id);

            return View(客戶聯絡人);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶聯絡人 客戶聯絡人 = this.repo.GetById(id);

            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }

            return View(客戶聯絡人);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = this.repo.GetById(id);

            this.repo.Delete(客戶聯絡人);

            ((客戶資料Entities)this.repo.UnitOfWork.Context).Entry(客戶聯絡人).State = EntityState.Modified;

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