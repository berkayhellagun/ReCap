using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        readonly ICreditCardService _creditCardService;
        readonly ICarService _carService;

        public PaymentManager(ICreditCardService creditCardService, ICarService carService)
        {
            _creditCardService = creditCardService;
            _carService = carService;
        }

        public async Task<IResult> AsyncPay(CreditCard creditCard, int carId)
        {
            var result = await _carService.AsyncGetById(carId);
            if (creditCard.Balance >= result.Data.DailyPrice)
            {
                creditCard.Balance = creditCard.Balance - result.Data.DailyPrice;
                await _creditCardService.AsyncUpdate(creditCard);
                return new SuccessResult(Messages.PaymentSuccess);
            }
            return new ErrorResult();
        }
    }
}
