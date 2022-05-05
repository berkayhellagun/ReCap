using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService:IManagerService<Car>
    {
        Task<IDataResult<List<CarDetailDto>>> AsyncGetCarByBrandId(int id);
        Task<IDataResult<List<CarDetailDto>>> AsyncGetCarByColorId(int id);
        Task<IDataResult<List<CarDetailDto>>> AsyncGetCarDetails();
    }
}
