using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concreate;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concreate.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, DBContex>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (DBContex contex=new DBContex())
            {
                var result = from c in contex.Car
                             join b in contex.Brand on c.BrandId equals b.BrandId
                             join cn in contex.Color on c.ColorId equals cn.ColorId
                             select new CarDetailDto
                             {
                                 name = c.CarName,
                                 colorName=cn.ColorName,
                                 brandName=b.BrandName,
                                 dailyPrice=c.DailyPrice
                             };

                return result.ToList();
            }
        }
    }
}
