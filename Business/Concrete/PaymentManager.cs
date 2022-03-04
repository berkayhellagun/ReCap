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
        ICreditCardService _creditCardService;
        ICarService _carService;

        public PaymentManager(ICreditCardService creditCardService, ICarService carService)
        {
            _creditCardService = creditCardService;
            _carService = carService;
        }

        public IResult Pay(CreditCard creditCard, int carId)
        {
            var result = _carService.GetById(carId);
            if (creditCard.Balance >= result.Data.DailyPrice)
            {
                creditCard.Balance = creditCard.Balance - result.Data.DailyPrice;
                _creditCardService.Update(creditCard);
                return new SuccessResult(Messages.PaymentSuccess);
            }
            return new ErrorResult();
        }
    }
}
