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
    public class RentalManager : IRentalService
    {
        IRentalsDal _rentalDal;

        public RentalManager(IRentalsDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Add(Rental t)
        { 
            var result=_rentalDal.Get(r=>r.CarId==t.CarId);
            if (result==null||result.ReturnDate<DateTime.Now.Date)
            {
                t.RentDate = DateTime.Now.Date;
                _rentalDal.Add(t);
                return new SuccessResult(Messages.RentalAdded);
            }
            else
            {
                return new ErrorResult(Messages.RentalNotAdded);
            }
        }

        public IResult Delete(Rental t)
        {
            _rentalDal.Delete(t);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r=>r.Id==id));
        }

        public IResult Update(Rental t)
        {
            _rentalDal.Update(t);
            return new SuccessResult();
        }
    }
}
