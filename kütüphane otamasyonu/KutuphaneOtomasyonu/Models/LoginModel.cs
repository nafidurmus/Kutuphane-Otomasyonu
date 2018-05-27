using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace KutuphaneOtomasyonu.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Lütfen isminizi giriniz.")]
        [Display(Name = "İsim")]
        public string isim { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}