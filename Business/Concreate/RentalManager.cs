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
    public class RentalManager : IRentalService
    {
        IRentalsDal _rentalDal;

        public RentalManager(IRentalsDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Add(Rentals t)
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

        public IResult Delete(Rentals t)
        {
            _rentalDal.Delete(t);
            return new SuccessResult();
        }

        public IDataResult<List<Rentals>> GetAll()
        {
            return new SuccessDataResult<List<Rentals>>(_rentalDal.GetAll());
        }

        public IDataResult<Rentals> GetById(int id)
        {
            return new SuccessDataResult<Rentals>(_rentalDal.Get(r=>r.Id==id));
        }

        public IResult Update(Rentals t)
        {
            _rentalDal.Update(t);
            return new SuccessResult();
        }
    }
}
