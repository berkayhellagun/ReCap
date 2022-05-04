using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService : IManagerService<User>
    {
        Task<List<OperationClaim>> AsyncGetClaims(User user);
        Task<User> AsyncGetByMail(string email);
    }
}
