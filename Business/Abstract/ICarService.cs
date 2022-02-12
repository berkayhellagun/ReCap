﻿using Core.Utilities.Results.Abstract;
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
        IDataResult<List<CarDetailDto>> GetCarByBrandId(int id);
        IDataResult<List<CarDetailDto>> GetCarByColorId(int id);
    }
}
