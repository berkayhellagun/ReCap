using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public static class Messages
    {
        public static string BrandAdded="Marka eklendi.";
        public static string BrandRemoved = "Marka silindi.";
        public static string BrandUpdated = "Marka güncellendi";

        public static string CarNotInsertPriceZero = "Araba eklenemedi. Tutar sıfırdan büyük olmalı";
        public static string CarNotInsertNameLenght = "Araba eklenemedi. İsim 2 karakterden fazla olmalı";
        public static string CarInsert = "Araba eklendi";
        public static string CarRemoved = "Araba silindi";
        public static string CarUpdated = "Araba güncellendi";

        public static string ColorAdded = "Renk eklendi.";
        public static string ColorRemoved = "Renk silindi.";
        public static string ColorUpdated = "Renk güncellendi.";
        
        public static string RentalAdded="Kiralama başarılı";
        public static string RentalNotAdded = "Kiralama başarısız, Araç kullanımda";
    }
}
