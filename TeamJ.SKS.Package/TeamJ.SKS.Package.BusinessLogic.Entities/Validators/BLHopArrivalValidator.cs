using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace TeamJ.SKS.Package.BusinessLogic.DTOs.Validators
{
    public class BLHopArrivalValidator : AbstractValidator<BLHopArrival>
    {
        public BLHopArrivalValidator()
        {
            RuleFor(x => x.Code).Matches(@"^[A-Z]{4}\d{1,4}$");
        }
    }
}
