using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public static class Messages
    {
        public static string BrandAdded="Brand added.";
        public static string BrandRemoved = "Brand removed.";
        public static string BrandUpdated = "Brand updated";

        public static string CarNotInsertPriceZero = "Car couldn't insert. Car price must be greater than zero.";
        public static string CarNotInsertNameLenght = "Car couldn't insert. Lenght of car name must be greater than two char.";
        public static string CarInsert = "Car added.";
        public static string CarRemoved = "Car removed.";
        public static string CarUpdated = "Car updated.";

        public static string ColorAdded = "Color added.";
        public static string ColorRemoved = "Color removed.";
        public static string ColorUpdated = "Color updated.";
        
        public static string RentalAdded= "Car rental is successful.";
        public static string RentalNotAdded = "Car rental is failure, Car not-exist.";
    }
}
