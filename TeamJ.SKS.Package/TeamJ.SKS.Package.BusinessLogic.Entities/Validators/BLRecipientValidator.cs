using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace TeamJ.SKS.Package.BusinessLogic.DTOs.Validators
{
    public class BLRecipientValidator : AbstractValidator<BLRecipient>
    {
        public BLRecipientValidator()
        {
            RuleFor(x => x.PostalCode).Matches("^([A][-])([0-9]{4})$")
                .When(x => x.Country == "Austria" || x.Country == "Österreich");

            RuleFor(x => x.Street).Matches(@"^[a-zA-Z/0-9\s\p{L}]+")
                .When(x => x.Country == "Austria" || x.Country == "Österreich");

            RuleFor(x => x.City).Matches(@"^([A-Z])([a-zA-Z-\s\p{L}]+)")
                .When(x => x.Country == "Austria" || x.Country == "Österreich");

            RuleFor(x => x.Name).Matches(@"^([A-Z])([a-zA-Z-\s\p{L}]+)")
                .When(x => x.Country == "Austria" || x.Country == "Österreich");
    }
    }
}
