using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICreditCardService
    {
        IDataResult<CreditCard> GetCard(CreditCardDto creditCardDto);
        IDataResult<CreditCard> GetById(int creditCardId);
        IResult Update(CreditCard creditCard);
        IResult Add(CreditCardDto creditCardDto);
        IResult Delete(CreditCard creditCard);
        IResult Validate(CreditCardDto creditCardDto);
    }
}
