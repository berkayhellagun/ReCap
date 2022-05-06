using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController:GenericController<Brand>
    {
        readonly IBrandService _brandService;
        
        public BrandsController(IBrandService brandService):base(brandService)
        {
            _brandService = brandService;
        }
        
    }
}
