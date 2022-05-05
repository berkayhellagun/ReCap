using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
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
    public class RentalManager : IRentalService
    {
        readonly IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public async Task<IResult> AsyncAdd(Rental t)
        {
            t.RentDate = DateTime.Now;
            await _rentalDal.AsyncAdd(t);
            return new SuccessResult(Messages.RentalAdded);
        }

        public async Task<IResult> AsyncDelete(Rental t)
        {
            await _rentalDal.AsyncDelete(t);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<Rental>>> AsyncGetAll()
        {
            return new SuccessDataResult<List<Rental>>(await _rentalDal.AsyncGetAll());
        }

        public async Task<IDataResult<Rental>> AsyncGetById(int id)
        {
            return new SuccessDataResult<Rental>(await _rentalDal.AsyncGet(r => r.Id == id));
        }

        public async Task<IResult> AsyncUpdate(Rental t)
        {
            await _rentalDal.AsyncUpdate(t);
            return new SuccessResult();
        }
    }
}
