using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : GenericController<Rental>
    {
        readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService):base(rentalService)
        {
            _rentalService = rentalService;
        }
    }
}
