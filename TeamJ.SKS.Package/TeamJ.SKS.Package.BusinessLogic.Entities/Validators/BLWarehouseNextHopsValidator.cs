using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace TeamJ.SKS.Package.BusinessLogic.DTOs.Validators
{
    public class BLWarehouseNextHopsValidator : AbstractValidator<BLWarehouseNextHops>
    {
        public BLWarehouseNextHopsValidator()
        {
            RuleFor(x => x.Hop).NotNull();
        }
    }
}
