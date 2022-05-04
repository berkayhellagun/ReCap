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
        Task<IDataResult<List<T>>> AsyncGetAll();
        Task<IResult> AsyncAdd(T t);
        Task<IResult> AsyncUpdate(T t);
        Task<IResult> AsyncDelete(T t);
        Task<IDataResult<T>> AsyncGetById(int id);
    }
}
