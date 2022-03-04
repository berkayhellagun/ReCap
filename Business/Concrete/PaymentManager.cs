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

        public PaymentManager(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        public IResult Pay(CreditCard creditCard, Car car)
        {
            if (creditCard.Balance >= car.DailyPrice)
            {
                creditCard.Balance = creditCard.Balance - car.DailyPrice;
                _creditCardService.Update(creditCard);
                return new SuccessResult(Messages.PaymentSuccess);
            }
            return new ErrorResult();
        }
    }
}
