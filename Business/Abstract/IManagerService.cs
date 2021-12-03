using Core.Entities;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IManagerService<T> where T : class,IEntity,new()
    {
        IDataResult<List<T>> GetAll();
        IResult Add(T t);
        IResult Update(T t);
        IResult Delete(T t);
        IDataResult<T> GetById(int id);
    }
}
