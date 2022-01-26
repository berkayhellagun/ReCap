using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IResult Add(Car t)
        {
            if (t.Descripton.Length >= 2)
            {
                if (t.DailyPrice > 0)
                {
                    _carDal.Add(t);
                    return new SuccessResult(Messages.CarInsert);
                }
                else
                {
                    return new ErrorResult(Messages.CarNotInsertPriceZero);
                }
            }
            else
            {
                return new ErrorResult(Messages.CarNotInsertNameLenght);
            }
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

        public IDataResult<List<Car>> GetCarByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id));

        }

        public IDataResult<List<Car>> GetCarByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id));
        }

        public IResult Update(Car t)
        {
            _carDal.Update(t);
            return new SuccessResult(Messages.CarUpdated);
        }
    }
}
