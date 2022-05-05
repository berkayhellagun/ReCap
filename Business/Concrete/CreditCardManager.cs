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
        readonly ICreditCardDal _creditCardDal;

        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }

        public async Task<IResult> AsyncAdd(CreditCardDto creditCardDto)
        {
            var card = await GetCreditCard(creditCardDto);
            if (card == null)
            {
                card.Balance = 0; //provider of this service is bank
                await _creditCardDal.AsyncAdd(card);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public async Task<IResult> AsyncDelete(CreditCard creditCard)
        {
            await _creditCardDal.AsyncDelete(creditCard);
            return new SuccessResult();
        }

        public async Task<IDataResult<CreditCard>> AsyncGetCard(CreditCardDto creditCardDto)
        {
            var creditCard = await GetCreditCard(creditCardDto);
            if (creditCard != null)
            {
                return new SuccessDataResult<CreditCard>(creditCard);
            }
            return new ErrorDataResult<CreditCard>(Messages.CreditCardError);
        }

        public async Task<IDataResult<CreditCard>> AsyncGetById(int creditCardId)
        {
            var creditCard = await _creditCardDal.AsyncGet(c => c.Id == creditCardId);
            if (creditCard != null)
            {
                return new SuccessDataResult<CreditCard>(creditCard);
            }
            return new ErrorDataResult<CreditCard>();
        }

        public async Task<IResult> AsyncUpdate(CreditCard creditCard)
        {
            await _creditCardDal.AsyncUpdate(creditCard);
            return new SuccessResult();
        }

        public async Task<IResult> AsyncValidate(CreditCardDto creditCardDto)
        {
            var creditCardInfo = await GetCreditCard(creditCardDto);
            if (creditCardInfo != null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        private async Task<CreditCard> GetCreditCard(CreditCardDto creditCardDto)
        {
            return await _creditCardDal.AsyncGet(c =>
                c.CardNumber == creditCardDto.CardNumber &&
                c.ExpireYear == creditCardDto.ExpireYear &&
                c.ExpireMonth == creditCardDto.ExpireMonth &&
                c.Cvc == creditCardDto.Cvc
            );
        }
    }
}
