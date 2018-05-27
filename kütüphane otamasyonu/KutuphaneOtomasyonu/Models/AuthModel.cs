using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KutuphaneOtomasyonu.Models
{
    public class AuthModel
    {
        public Uye uye{ get; set; }
        public LoginModel login { get; set; }
    }
}