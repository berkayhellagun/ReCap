using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.CarName).MinimumLength(2).WithMessage("car name must be greater than 2");
            RuleFor(c => c.DailyPrice).GreaterThan(0).WithMessage("daily price of car must be greater than 0");
        }
    }
}
