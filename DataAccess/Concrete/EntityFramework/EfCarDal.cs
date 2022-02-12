using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, DBContex>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (DBContex contex = new DBContex())
            {
                var result = from c in contex.Cars
                             join b in contex.Brands on c.Id equals b.Id
                             join cn in contex.Colors on c.Id equals cn.Id
                             select new CarDetailDto
                             {
                                 name = c.Name,
                                 colorName = cn.Name,
                                 brandName = b.Name,
                                 dailyPrice = c.DailyPrice
                             };

                return result.ToList();
            }
        }
    }
}
