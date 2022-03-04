using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardsController : Controller
    {
        ICreditCardService _creditCardService;

        public CreditCardsController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        [HttpPost("add")]
        public IActionResult Add(CreditCardDto creditCardDto)
        {
            var result = _creditCardService.Add(creditCardDto);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("delete")]
        public IActionResult Delete(CreditCard creditCard)
        {
            var result= _creditCardService.Delete(creditCard);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("getcard")]
        public IActionResult GetCard(CreditCardDto creditCardDto)
        {
            var result = _creditCardService.GetCard(creditCardDto);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result=_creditCardService.GetById(id);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("update")]
        public IActionResult Update(CreditCard creditCard)
        {
            var result = _creditCardService.Update(creditCard);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

    }
}
