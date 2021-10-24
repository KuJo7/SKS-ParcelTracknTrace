using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.BusinessLogic.DTOs.Validators;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;

namespace TeamJ.SKS.Package.BusinessLogic
{
    public class HopLogic : IHopLogic
    {
        readonly IValidator<BLWarehouse> blWarehouseValidator = new BLWarehouseValidator();
        readonly IValidator<string> codeValidator = new BLCodeValidator();
        public List<BLHop> ExportWarehouses()
        {
            //var list = new List<BLHop>();
            //if (list.Count != 0)
            //{
                return new List<BLHop>();
            //}
            //return null;

        }

        public bool ImportWarehouses(BLWarehouse blWarehouse)
        {
            var result = blWarehouseValidator.Validate(blWarehouse);
            if (result.IsValid)
            {
                return true;
            }
            return false;
        }

        public BLHop GetWarehouse(string code)
        {
            var result = codeValidator.Validate(code);
            if (result.IsValid)
            {
                return new BLWarehouse();
            }
            return null;
        }
    }
}
