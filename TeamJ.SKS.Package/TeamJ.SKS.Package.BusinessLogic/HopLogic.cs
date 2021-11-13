using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<HopLogic> _logger;

        public HopLogic(IHopRepository repo, IMapper mapper, ILogger<HopLogic> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public List<BLHop> ExportWarehouses()
        {
            _logger.LogInformation("HopLogic ExportWarehouses started.");
            var result = _mapper.Map<List<DALHop>, List<BLHop>>(_repo.GetAllHops());
            _logger.LogInformation("HopLogic ExportWarehouses ended.");
            return result;
        }

        public bool ImportWarehouses(BLWarehouse blWarehouse)
        {
            _logger.LogInformation("HopLogic ImportWarehouses started.");
            var result = blWarehouseValidator.Validate(blWarehouse);
            if (result.IsValid)
            {
                DALHop dalWarehouse = _mapper.Map<DALHop>(blWarehouse);
                _repo.Create(dalWarehouse);
                _logger.LogInformation("HopLogic ImportWarehouses ended successful.");
                return true;
            }
            _logger.LogInformation("HopLogic ImportWarehouses ended unsuccessful.");
            return false;
        }

        public BLHop GetWarehouse(string code)
        {
            _logger.LogInformation("HopLogic GetWarehouse started.");
            var result = codeValidator.Validate(code);
            if (result.IsValid)
            {
                _logger.LogInformation("HopLogic GetWarehouse ended successful.");
                return _mapper.Map<BLWarehouse>(_repo.GetByCode(code));
            }
            _logger.LogInformation("HopLogic GetWarehouse ended unsuccessful.");
            return null;
        }
    }
}
