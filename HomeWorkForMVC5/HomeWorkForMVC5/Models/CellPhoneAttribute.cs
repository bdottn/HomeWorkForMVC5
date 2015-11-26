using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace HomeWorkForMVC5.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class CellPhoneAttribute : DataTypeAttribute
    {
        private static Regex regex = new Regex(@"^\d{4}-\d{6}$", RegexOptions.IgnoreCase);

        public CellPhoneAttribute()
            : base(DataType.PhoneNumber)
        {
            ErrorMessage = "手機格式錯誤，應為 0911-111111 格式";
        }
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string valueAsString = value as string;

            return valueAsString != null && regex.Match(valueAsString).Length > 0;
        }
    }
}