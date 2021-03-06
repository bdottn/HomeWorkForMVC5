using System.Linq;

namespace HomeWorkForMVC5.Models
{
    public class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
    {

        public IQueryable<客戶聯絡人> All(bool 是否已刪除)
        {
            var data = this.All();

            data = data.Where(d => d.是否已刪除 == 是否已刪除);

            return data;
        }

        public IQueryable<客戶聯絡人> GetByKeyword(string search, string 職稱, bool? 是否已刪除 = null)
        {
            IQueryable<客戶聯絡人> data;

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
                data = data.Where(d => d.姓名.Contains(search) || d.客戶資料.客戶名稱.Contains(search));
            }

            if (string.IsNullOrEmpty(職稱) == false)
            {
                data = data.Where(d => 職稱.Equals(d.職稱));
            }

            return data;
        }

        public IQueryable<string> Get職稱()
        {
            return this.All(false).Where(d => string.IsNullOrEmpty(d.職稱) == false).Select(d => d.職稱).Distinct();
        }

        public 客戶聯絡人 GetById(int? id)
        {
            return this.All().FirstOrDefault(d => d.Id == id);
        }

        public override void Delete(客戶聯絡人 客戶聯絡人)
        {
            客戶聯絡人.是否已刪除 = true;
        }
    }

    public interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
    {

    }
}