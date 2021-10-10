using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;

namespace TeamJ.SKS.Package.BusinessLogic.DTOs.Validators
{
    public class BLParcelValidator : AbstractValidator<BLParcel>
    {
        public BLParcelValidator()
        {
            RuleFor(x => x.TrackingId).Matches("^[A-Z0-9]{9}$");
            RuleFor(x => x.Weight).GreaterThan(0);
            RuleFor(x => x.Recipient).NotNull();
            RuleFor(x => x.Sender).NotNull();
            RuleFor(x => x.FutureHops).NotNull();
            RuleFor(x => x.VisitedHops).NotNull();
        }
    }
}
