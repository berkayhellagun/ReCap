using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CarDetailDto:IDto
    {
        public  int carId { get; set; }
        public string name { get; set; }
        public string brandName { get; set; }
        public string colorName { get; set; }
        public int dailyPrice { get; set; }
    }
}
