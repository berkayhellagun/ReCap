using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : GenericController<Color>
    {
        IColorService _colorService;
        public ColorsController(IColorService colorService):base(colorService)
        {
            _colorService = colorService;
        }
    }
}
