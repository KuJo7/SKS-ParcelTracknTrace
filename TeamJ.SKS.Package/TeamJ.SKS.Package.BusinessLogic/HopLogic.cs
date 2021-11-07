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
using TeamJ.SKS.Package.DataAccess.Interfaces;

namespace TeamJ.SKS.Package.BusinessLogic
{
    public class HopLogic : IHopLogic
    {
        private readonly IValidator<BLWarehouse> blWarehouseValidator = new BLWarehouseValidator();
        private readonly IValidator<string> codeValidator = new BLCodeValidator();
        private readonly IHopRepository _repo;
        private readonly IMapper _mapper;

        public HopLogic(IHopRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public List<BLHop> ExportWarehouses()
        {
            var result = _mapper.Map<List<DALHop>, List<BLHop>>(_repo.GetAllHops());
            return result;
        }

        public bool ImportWarehouses(BLWarehouse blWarehouse)
        {
            var result = blWarehouseValidator.Validate(blWarehouse);
            if (result.IsValid)
            {
                DALHop dalWarehouse = _mapper.Map<DALHop>(blWarehouse);
                _repo.Create(dalWarehouse);
                return true;
            }
            return false;
        }

        public BLHop GetWarehouse(string code)
        {
            var result = codeValidator.Validate(code);
            if (result.IsValid)
            {
                return _mapper.Map<BLWarehouse>(_repo.GetByCode(code));
            }
            return null;
        }
    }
}
