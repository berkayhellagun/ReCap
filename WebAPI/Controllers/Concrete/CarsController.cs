using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : GenericController<Car>
    {
        protected ICarService _carService;
        public CarsController(ICarService carService) : base(carService)
        {
            _carService = carService;
        }
        [HttpGet("getcarbybrandid")]
        public async Task<IActionResult> GetCarByBrandId(int id)
        {
            var result = await _carService.AsyncGetCarByBrandId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcarbycolorid")]
        public async Task<IActionResult> GetCarByColorId(int id)
        {
            var result = await _carService.AsyncGetCarByColorId(id);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            return BadRequest(result);
        }
    }
}
