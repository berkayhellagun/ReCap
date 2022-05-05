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
        Task<IDataResult<CreditCard>> AsyncGetCard(CreditCardDto creditCardDto);
        Task<IDataResult<CreditCard>> AsyncGetById(int creditCardId);
        Task<IResult> AsyncUpdate(CreditCard creditCard);
        Task<IResult> AsyncAdd(CreditCardDto creditCardDto);
        Task<IResult> AsyncDelete(CreditCard creditCard);
        Task<IResult> AsyncValidate(CreditCardDto creditCardDto);
    }
}
