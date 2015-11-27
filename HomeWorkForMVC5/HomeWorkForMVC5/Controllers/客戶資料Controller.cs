using HomeWorkForMVC5.Models;
using System;
using System.Data.Entity;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace HomeWorkForMVC5.Controllers
{
    public class 客戶資料Controller : Controller
    {
        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();

        /// <summary>
        /// 客戶資料管理
        /// </summary>
        /// <param name="search">客戶名稱</param>
        public ActionResult Index(string search, string 客戶分類)
        {
            var list = this.repo.Get客戶分類();

            ViewBag.客戶分類 = new SelectList(list);

            var data = this.repo.GetByKeyword(search, 客戶分類, false);

            return View(data);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶資料 客戶資料 = this.repo.GetById(id);

            if (客戶資料 == null)
            {
                return HttpNotFound();
            }

            return View(客戶資料);
        }

        public ActionResult Create()
        {
            return View();
        }

        public static string Sha256encrypt(string phrase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(phrase));
            return Convert.ToBase64String(hashedDataBytes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,是否已刪除,客戶分類,精度,緯度,帳號,密碼")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                客戶資料.密碼 = Sha256encrypt(客戶資料.密碼);

                this.repo.Add(客戶資料);

                this.repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶資料 客戶資料 = this.repo.GetById(id);

            if (客戶資料 == null)
            {
                return HttpNotFound();
            }

            return View(客戶資料);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,是否已刪除,客戶分類,精度,緯度,帳號,密碼")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                ((客戶資料Entities)this.repo.UnitOfWork.Context).Entry(客戶資料).State = EntityState.Modified;

                this.repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶資料 客戶資料 = this.repo.GetById(id);

            if (客戶資料 == null)
            {
                return HttpNotFound();
            }

            return View(客戶資料);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            客戶資料 客戶資料 = this.repo.GetById(id);

            this.repo.Delete(客戶資料);

            ((客戶資料Entities)this.repo.UnitOfWork.Context).Entry(客戶資料).State = EntityState.Modified;

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