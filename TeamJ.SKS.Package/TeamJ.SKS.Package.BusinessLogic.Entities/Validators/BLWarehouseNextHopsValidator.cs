using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace TeamJ.SKS.Package.BusinessLogic.DTOs.Validators
{
    [ExcludeFromCodeCoverage]
    public class BLWarehouseNextHopsValidator : AbstractValidator<BLWarehouseNextHops>
    {
        public BLWarehouseNextHopsValidator()
        {
            RuleFor(x => x.Hop).NotNull();
        }
    }
}
