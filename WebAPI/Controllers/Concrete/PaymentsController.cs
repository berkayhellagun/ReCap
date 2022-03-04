﻿using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService, ICreditCardService creditCardService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("paymentInfo")]
        public IActionResult CheckPaymentInfo(CreditCard creditCard, Car car)
        {
            var result = _paymentService.Pay(creditCard, car);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
