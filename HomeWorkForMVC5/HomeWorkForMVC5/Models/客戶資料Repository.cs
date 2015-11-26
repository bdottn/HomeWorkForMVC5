using System.Linq;

namespace HomeWorkForMVC5.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public IQueryable<客戶資料> All(bool 是否已刪除)
        {
            var data = this.All();

            data = data.Where(d => d.是否已刪除 == 是否已刪除);

            return data;
        }

        public IQueryable<客戶資料> GetByKeyword(string search, bool? 是否已刪除 = null)
        {
            IQueryable<客戶資料> data;

            if (是否已刪除.HasValue)
            {
                data = this.All(是否已刪除.Value);
            }
            else
            {
                data = this.All();
            }

            if (string.IsNullOrEmpty(search) == false)
            {
                data = data.Where(d => d.客戶名稱.Contains(search));
            }

            return data;
        }

        public 客戶資料 GetById(int? id)
        {
            return this.All().FirstOrDefault(d => d.Id == id);
        }

        public override void Delete(客戶資料 客戶資料)
        {
            客戶資料.是否已刪除 = true;

            // 連動刪除客戶資料中的客戶聯絡人
            foreach (var item in 客戶資料.客戶聯絡人)
            {
                item.是否已刪除 = true;
            }

            // 連動刪除客戶資料中的客戶銀行資訊
            foreach (var item in 客戶資料.客戶銀行資訊)
            {
                item.是否已刪除 = true;
            }
        }
    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}