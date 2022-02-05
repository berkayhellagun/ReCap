using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        IFileHelper _fileHelper;
        ICarService _carService;

        public CarImageManager(ICarImageDal carImageDal, IFileHelper fileHelper, ICarService carService)
        {
            _carImageDal = carImageDal;
            _fileHelper = fileHelper;
            _carService = carService;
        }

        public IResult Add(IFormFile formFile, CarImage t)
        {
            IResult result = BusinessRules.Run(CheckIfImageExist(t.CarId), CheckIfCarImageLimit(t.CarId));
            if (result != null)
            {
                return new ErrorResult();
            }
            t.ImagePath = _fileHelper.Upload(formFile, PathConstants.ImagesPath);
            t.Date = DateTime.Now;
            _carImageDal.Add(t);
            return new SuccessResult();
        }

        public IResult Delete(CarImage t)
        {
            _fileHelper.Delete(PathConstants.ImagesPath);
            _carImageDal.Delete(t);
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.Id == id));
        }

        public IDataResult<List<CarImage>> GetImageByCarId(int carId)
        {
            var result = BusinessRules.Run(CheckIfNotExistImage(carId));
            var result2 = BusinessRules.Run(CheckCarId(carId));
            
            if (result2 != null)
            {
                return new ErrorDataResult<List<CarImage>>("Car not exist.");
            }

            if (result != null)
            {
                return new SuccessDataResult<List<CarImage>>(GetDefaultImage(carId).Data);
            }

            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == carId));
        }

        public IResult Update(IFormFile formFile, CarImage t)
        {
            t.ImagePath = _fileHelper.Update(formFile, PathConstants.ImagesPath + t.ImagePath, PathConstants.ImagesPath);
            _carImageDal.Update(t);
            return new SuccessResult();
        }

        private IResult CheckIfCarImageLimit(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result > 5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        private IResult CheckIfImageExist(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        private IResult CheckIfNotExistImage(int id)
        {
            var result = _carImageDal.GetAll(c => c.CarId == id);
            if (result.Count == 0)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        private IDataResult<List<CarImage>> GetDefaultImage(int carId)
        {
            List<CarImage> carImage = new List<CarImage>();
            carImage.Add(new CarImage { CarId = carId, Date = DateTime.Now, ImagePath = "DefaultImage.jpg" });
            return new SuccessDataResult<List<CarImage>>(carImage);
        }
        private IResult CheckCarId(int carId)
        {
            var result = _carService.GetAll();
            foreach (var item in result.Data)
            {
                if (item.CarId == carId)
                {
                    return new SuccessResult();
                }
                else
                {
                    continue;
                }
            }
            return new ErrorResult();
        }
    }
}
