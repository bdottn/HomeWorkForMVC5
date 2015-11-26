namespace HomeWorkForMVC5.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.是否已刪除 == true)
            {
                yield return null;
            }
            else
            {
                客戶資料 客戶資料 = this.db.客戶資料.Find(this.客戶Id);

                if (客戶資料.客戶聯絡人.Any(d => d.是否已刪除 == false && Email.Equals(d.Email)))
                {
                    yield return new ValidationResult
                    ("同一個客戶下的聯絡人，其 Email 不能重複。", new[] { "Email" });
                }
            }
        }
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }

        [StringLength(250, ErrorMessage = "欄位長度不得大於 250 個字元")]
        [Required]
        [EmailAddress(ErrorMessage = "Email 格式不正確")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [CellPhone]
        public string 手機 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }

        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
