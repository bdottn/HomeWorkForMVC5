using System.Linq;

namespace HomeWorkForMVC5.Models
{
    public class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
    {
        public IQueryable<客戶銀行資訊> All(bool 是否已刪除)
        {
            var data = this.All();

            data = data.Where(d => d.是否已刪除 == 是否已刪除);

            return data;
        }

        public IQueryable<客戶銀行資訊> GetByKeyword(string search, bool? 是否已刪除 = null)
        {
            IQueryable<客戶銀行資訊> data;

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
                data = data.Where(d => d.帳戶名稱.Contains(search) || d.客戶資料.客戶名稱.Contains(search));
            }

            return data;
        }

        public 客戶銀行資訊 GetById(int? id)
        {
            return this.All().FirstOrDefault(d => d.Id == id);
        }

        public override void Delete(客戶銀行資訊 客戶銀行資訊)
        {
            客戶銀行資訊.是否已刪除 = true;
        }
    }

    public interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
    {

    }
}