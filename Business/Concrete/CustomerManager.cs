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
    public class CustomerManager : ICustomerService
    {
        readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public async Task<IResult> AsyncAdd(Customer t)
        {
            await _customerDal.AsyncAdd(t);
            return new SuccessResult();
        }

        public async Task<IResult> AsyncDelete(Customer t)
        {
            await _customerDal.AsyncDelete(t);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<Customer>>> AsyncGetAll()
        {
            return new SuccessDataResult<List<Customer>>(await _customerDal.AsyncGetAll());
        }

        public async Task<IDataResult<Customer>> AsyncGetById(int id)
        {
            return new SuccessDataResult<Customer>(await _customerDal.AsyncGet(c => c.Id == id));
        }

        public async Task<IResult> AsyncUpdate(Customer t)
        {
            await _customerDal.AsyncUpdate(t);
            return new SuccessResult();
        }
    }
}
