using System.Linq;

namespace HomeWorkForMVC5.Models
{
    public class 客戶清單Repository : EFRepository<客戶清單>, I客戶清單Repository
    {
        public IQueryable<客戶清單> GetByKeyword(string search)
        {
            var data = this.All();

            if (string.IsNullOrEmpty(search) == false)
            {
                data = data.Where(d => d.客戶名稱.Contains(search));
            }

            return data;
        }
    }

    public interface I客戶清單Repository : IRepository<客戶清單>
    {

    }
}