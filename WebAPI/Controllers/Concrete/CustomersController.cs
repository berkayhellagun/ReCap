using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : GenericController<Customer>
    {
        ICustomerService _customerService;

        public CustomersController(ICustomerService customerService):base(customerService)
        {
            _customerService = customerService;
        }
    }
}
