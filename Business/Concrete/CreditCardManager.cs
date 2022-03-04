using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CreditCardManager : ICreditCardService
    {
        ICreditCardDal _creditCardDal;
        
        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }

        public IResult Add(CreditCardDto creditCardDto)
        {
            var checkCard = GetCreditCard(creditCardDto);
            if (checkCard == null)
            {
                checkCard.Balance = 0; //provider of this service is bank
                _creditCardDal.Add(checkCard);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IResult Delete(CreditCard creditCard)
        {
            _creditCardDal.Delete(creditCard);
            return new SuccessResult();
        }

        public IDataResult<CreditCard> GetCard(CreditCardDto creditCardDto)
        {
            var creditCard = GetCreditCard(creditCardDto);
            if (creditCard!=null)
            {
                return new SuccessDataResult<CreditCard>(creditCard);
            }
            return new ErrorDataResult<CreditCard>(Messages.CreditCardError);
        }

        public IDataResult<CreditCard> GetById(int creditCardId)
        {
            var creditCard = _creditCardDal.Get(c => c.Id == creditCardId);
            if (creditCard!=null)
            {
                return new SuccessDataResult<CreditCard>(creditCard);
            }
            return new ErrorDataResult<CreditCard>();
        }

        public IResult Update(CreditCard creditCard)
        {
            _creditCardDal.Update(creditCard);
            return new SuccessResult();
        }

        public IResult Validate(CreditCardDto creditCardDto)
        {
            var creditCardInfo = GetCreditCard(creditCardDto);
            if (creditCardInfo!=null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        private CreditCard GetCreditCard(CreditCardDto creditCardDto)
        {
            return _creditCardDal.Get(c =>
                c.CardNumber == creditCardDto.CardNumber &&
                c.ExpireYear == creditCardDto.ExpireYear &&
                c.ExpireMonth == creditCardDto.ExpireMonth &&
                c.Cvc == creditCardDto.Cvc
            );
        }
    }
}
