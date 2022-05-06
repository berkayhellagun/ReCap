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
        readonly ICarImageDal _carImageDal;
        readonly IFileHelper _fileHelper;
        readonly ICarService _carService;

        public CarImageManager(ICarImageDal carImageDal, IFileHelper fileHelper, ICarService carService)
        {
            _carImageDal = carImageDal;
            _fileHelper = fileHelper;
            _carService = carService;
        }

        public async Task<IResult> AsyncAdd(IFormFile formFile, CarImage t)
        {
            IResult result = BusinessRules.Run(CheckIfImageExist(t.CarId).Result, CheckIfCarImageLimit(t.CarId).Result);
            if (result != null)
            {
                return new ErrorResult(result.Message);
            }
            t.ImagePath = _fileHelper.Upload(formFile, PathConstants.ImagesPath);
            t.Date = DateTime.Now;
            await _carImageDal.AsyncAdd(t);
            return new SuccessResult();
        }

        public async Task<IResult> AsyncDelete(CarImage t)
        {
            _fileHelper.Delete(PathConstants.ImagesPath);
            await _carImageDal.AsyncDelete(t);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<CarImage>>> AsyncGetAll()
        {
            return new SuccessDataResult<List<CarImage>>(await _carImageDal.AsyncGetAll());
        }

        public async Task<IDataResult<CarImage>> AsyncGetById(int id)
        {
            return new SuccessDataResult<CarImage>(await _carImageDal.AsyncGet(c => c.Id == id));
        }

        public async Task<IDataResult<List<CarImage>>> AsyncGetImageByCarId(int carId)
        {
            var checkImage = BusinessRules.Run(CheckIfNotExistImage(carId).Result);
            var checkCarId = BusinessRules.Run(CheckCarId(carId).Result);

            if (checkCarId != null)
            {
                return new ErrorDataResult<List<CarImage>>("Car not exist.");
            }

            if (checkImage != null)
            {
                return new SuccessDataResult<List<CarImage>>(GetDefaultImage(carId).Data);
            }

            return new SuccessDataResult<List<CarImage>>(await _carImageDal.AsyncGetAll(c => c.CarId == carId));
        }

        public async Task<IResult> AsyncUpdate(IFormFile formFile, CarImage t)
        {
            t.ImagePath = _fileHelper.Update(formFile, PathConstants.ImagesPath + t.ImagePath, PathConstants.ImagesPath);
            await _carImageDal.AsyncUpdate(t);
            return new SuccessResult();
        }

        private async Task<IResult> CheckIfCarImageLimit(int carId)
        {
            var result = await _carImageDal.AsyncGetAll(c => c.CarId == carId);

            if (result.Count > 5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        private async Task<IResult> CheckIfImageExist(int carId)
        {
            var result = await _carImageDal.AsyncGetAll(c => c.CarId == carId);
            if (result.Count > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        private async Task<IResult> CheckIfNotExistImage(int id)
        {
            var result = await _carImageDal.AsyncGetAll(c => c.CarId == id);
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
        private async Task<IResult> CheckCarId(int carId)
        {
            var result = await _carService.AsyncGetAll();
            foreach (var item in result.Data)
            {
                if (item.Id == carId)
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
