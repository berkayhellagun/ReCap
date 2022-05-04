using Business.Abstract;
using Core.Entities.Concrete;
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
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<IResult> AsyncAdd(User t)
        {
            await _userDal.AsyncAdd(t);
            return new SuccessResult();
        }

        public async Task<IResult> AsyncDelete(User t)
        {
            await _userDal.AsyncDelete(t);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<User>>> AsyncGetAll()
        {
            return new SuccessDataResult<List<User>>(await _userDal.AsyncGetAll());
        }

        public async Task<IDataResult<User>> AsyncGetById(int id)
        {
            return new SuccessDataResult<User>(await _userDal.AsyncGet(u => u.Id == id));
        }

        public async Task<User> AsyncGetByMail(string email)
        {
            return await _userDal.AsyncGet(u => u.Email == email);
        }
        
        public async Task<List<OperationClaim>> AsyncGetClaims(User user)
        {
            return await _userDal.AsyncGetClaims(user);
        }

        public async Task<IResult> AsyncUpdate(User t)
        {
            await _userDal.AsyncUpdate(t);
            return new SuccessResult();
        }
    }
}
