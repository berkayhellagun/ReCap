using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Abstract
{
    public interface IGeneric<TEntity> where TEntity : class,IEntity , new()
    {
        Task<IActionResult> GetAll();
        Task<IActionResult> Add(TEntity t);
        Task<IActionResult> Update(TEntity t);
        Task<IActionResult> Delete(TEntity t);
        Task<IActionResult> GetById(int id);
    }
}
