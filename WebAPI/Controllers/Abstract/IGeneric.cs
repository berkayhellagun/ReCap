using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Abstract
{
    public interface IGeneric<TEntity> where TEntity : class,IEntity , new()
    {
        IActionResult GetAll();
        IActionResult Add(TEntity t);
        IActionResult Update(TEntity t);
        IActionResult Delete(TEntity t);
        IActionResult GetById(int id);
    }
}
