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

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car t)
        {
            _carDal.Add(t);
            return new SuccessResult(Messages.CarInsert);
        }

        public IResult Delete(Car t)
        {
            _carDal.Delete(t);
            return new SuccessResult(Messages.CarRemoved);
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == id));
        }
        
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IDataResult<List<CarDetailDto>> GetCarByBrandId(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.BrandId == id));

        }

        public IDataResult<List<CarDetailDto>> GetCarByColorId(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.ColorId == id));
        }

        public IResult Update(Car t)
        {
            _carDal.Update(t);
            return new SuccessResult(Messages.CarUpdated);
        }
    }
}
