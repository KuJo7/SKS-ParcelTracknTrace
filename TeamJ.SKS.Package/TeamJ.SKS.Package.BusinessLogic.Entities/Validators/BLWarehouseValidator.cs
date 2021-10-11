using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace TeamJ.SKS.Package.BusinessLogic.DTOs.Validators
{
    public class BLWarehouseValidator : AbstractValidator<BLWarehouse>
    {
        public BLWarehouseValidator()
        {
            RuleFor(x => x.Description).Matches(@"^([a-zA-Z0-9-\s\p{L}]+)$");
            RuleFor(x => x.NextHops).NotNull();
        }
    }
}
