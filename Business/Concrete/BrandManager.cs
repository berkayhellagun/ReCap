using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandService)
        {
            _brandDal = brandService;
        }

        public async Task<IResult> AsyncAdd(Brand t)
        {
            await _brandDal.AsyncAdd(t);
            return new SuccessResult(Messages.BrandAdded);
        }

        public async Task<IResult> AsyncDelete(Brand t)
        {
            await _brandDal.AsyncDelete(t);
            return new SuccessResult(Messages.BrandRemoved);
        }

        public async Task<IDataResult<List<Brand>>> AsyncGetAll()
        {
            return new SuccessDataResult<List<Brand>>(await _brandDal.AsyncGetAll());
        }

        public async Task<IDataResult<Brand>> AsyncGetById(int id)
        {
            return new SuccessDataResult<Brand>(await _brandDal.AsyncGet(i =>i.Id==id));
        }

        public async Task<IResult> AsyncUpdate(Brand t)
        {
            await _brandDal.AsyncUpdate(t);
            return new SuccessResult(Messages.BrandUpdated);
        }
    }
}
