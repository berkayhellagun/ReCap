using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concreate;
using DataAccess.Abstract;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concreate
{
    class BrandManager : IBrandService
    {

        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandService)
        {
            _brandDal = brandService;
        }

        public IResult Add(Brand t)
        {
            _brandDal.Add(t);
            return new SuccessResult(Messages.BrandAdded);
        }

        public IResult Delete(Brand t)
        {
            _brandDal.Delete(t);
            return new SuccessResult(Messages.BrandRemoved);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(),Messages.BrandAdded);
        }

        public IDataResult<Brand> GetById(int id)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(i =>i.BrandId==id));
        }

        public IResult Update(Brand t)
        {
            _brandDal.Update(t);
            return new SuccessResult(Messages.BrandUpdated);
        }
    }
}
