using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Validation;
using Entities.DTOs;
using Business.BusinessAspect.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        readonly ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [LogAspect(typeof(Database))]
        [SecuredOperation("admin,")]
        [ValidationAspect(typeof(CarValidator))]
        [PerformanceAspect(5)]
        public async Task<IResult> AsyncAdd(Car t)
        {
            await _carDal.AsyncAdd(t);
            return new SuccessResult(Messages.CarInsert);
        }

        public async Task<IResult> AsyncDelete(Car t)
        {
            await _carDal.AsyncDelete(t);
            return new SuccessResult(Messages.CarRemoved);
        }

        [LogAspect(typeof(File))]
        [CacheAspect]
        public async Task<IDataResult<List<Car>>> AsyncGetAll()
        {
            return new SuccessDataResult<List<Car>>(await _carDal.AsyncGetAll());
        }

        public async Task<IDataResult<Car>> AsyncGetById(int id)
        {
            return new SuccessDataResult<Car>(await _carDal.AsyncGet(c => c.Id == id));
        }

        public async Task<IDataResult<List<CarDetailDto>>> AsyncGetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(await _carDal.AsyncGetCarDetails());
        }

        public async Task<IDataResult<List<CarDetailDto>>> AsyncGetCarByBrandId(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(await _carDal.AsyncGetCarDetails(c => c.BrandId == id));

        }

        public async Task<IDataResult<List<CarDetailDto>>> AsyncGetCarByColorId(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(await _carDal.AsyncGetCarDetails(c => c.ColorId == id));
        }

        public async Task<IResult> AsyncUpdate(Car t)
        {
            await _carDal.AsyncUpdate(t);
            return new SuccessResult(Messages.CarUpdated);
        }
    }
}
