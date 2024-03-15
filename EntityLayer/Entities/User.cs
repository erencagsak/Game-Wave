using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez !")]
        [Display(Name = "Ad")]
        [StringLength(50, ErrorMessage = "Maksimum 50 karakter olmalıdır !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez !")]
        [Display(Name = "Soyad")]
        [StringLength(50, ErrorMessage = "Maksimum 50 karakter olmalıdır !")]
        public string Surname { get; set; }

        //[Required(ErrorMessage = "Boş Geçilemez !")]
        //[Display(Name = "E-posta")]
        //[StringLength(50, ErrorMessage = "Maksimum 50 karakter olmalıdır !")]
        //[EmailAddress(ErrorMessage ="E-mail formatına uygun şekilde giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez !")]
        [Display(Name = "Ad")]
        [StringLength(50, ErrorMessage = "Maksimum 50 karakter olmalıdır !")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "Boş Geçilemez !")]
        //[Display(Name = "Şifre")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez !")]
        [Display(Name = "Rol")]
        [StringLength(10, ErrorMessage = "Maksimum 10 karakter olmalıdır !")]
        public string Role { get; set; }


    }
}
