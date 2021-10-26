using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.BusinessLogic.DTOs.Validators;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;
using TeamJ.SKS.Package.DataAccess.DTOs;

namespace TeamJ.SKS.Package.BusinessLogic
{
    public class HopLogic : IHopLogic
    {
        readonly IValidator<BLWarehouse> blWarehouseValidator = new BLWarehouseValidator();
        readonly IValidator<string> codeValidator = new BLCodeValidator();
        public List<BLHop> ExportWarehouses()
        {

            return new List<BLHop>();
        }

        public bool ImportWarehouses(BLWarehouse blWarehouse, IMapper mapper)
        {
            var result = blWarehouseValidator.Validate(blWarehouse);
            if (result.IsValid)
            {
                DALWarehouse dalWarehouse = mapper.Map<DALWarehouse>(blWarehouse);
                //Datenbankaccess wird hier gemacht
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
