﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace TeamJ.SKS.Package.BusinessLogic.DTOs.Validators
{
    public class BLCodeValidator : AbstractValidator<string>
    {
        public BLCodeValidator()
        {
            RuleFor(x => x).Matches(@"^[A-Z]{4}\d{1,4}$");
        }
    }
}
