using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardsController : Controller
    {
        readonly ICreditCardService _creditCardService;

        public CreditCardsController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CreditCardDto creditCardDto)
        {
            var result = await _creditCardService.AsyncAdd(creditCardDto);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(CreditCard creditCard)
        {
            var result = await _creditCardService.AsyncDelete(creditCard);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("getcard")]
        public async Task<IActionResult> GetCard(CreditCardDto creditCardDto)
        {
            var result = await _creditCardService.AsyncGetCard(creditCardDto);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _creditCardService.AsyncGetById(id);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(CreditCard creditCard)
        {
            var result = await _creditCardService.AsyncUpdate(creditCard);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

    }
}
