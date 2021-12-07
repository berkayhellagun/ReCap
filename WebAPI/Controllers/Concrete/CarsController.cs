using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : GenericController<Car>
    {
        ICarService _carService;
        public CarsController(ICarService carService):base(carService)
        {
            _carService = carService;
        }
        [HttpGet("getcarbybrandid")]
        public IActionResult GetCarByBrandId(int id)
        {
            var result =_carService.GetCarByBrandId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcarbycolorid")]
        public IActionResult GetCarByColorId(int id)
        {
            var result = _carService.GetCarByColorId(id);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            return BadRequest(result);
        }
    }
}
