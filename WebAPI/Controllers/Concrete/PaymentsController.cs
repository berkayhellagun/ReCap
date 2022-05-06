using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService, ICreditCardService creditCardService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("paymentInfo")]
        public async Task<IActionResult> CheckPaymentInfo(CreditCard creditCard, int carId)
        {
            var result = await _paymentService.AsyncPay(creditCard, carId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
