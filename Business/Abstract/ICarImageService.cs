using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        Task<IDataResult<List<CarImage>>> AsyncGetImageByCarId(int id);
        Task<IResult> AsyncAdd(IFormFile formFile, CarImage t);
        Task<IResult> AsyncDelete(CarImage t);
        Task<IDataResult<List<CarImage>>> AsyncGetAll();
        Task<IDataResult<CarImage>> AsyncGetById(int id);
        Task<IResult> AsyncUpdate(IFormFile formFile, CarImage t);

    }
}
