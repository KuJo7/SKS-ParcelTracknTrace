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
        private readonly IHopRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<HopLogic> _logger;
        private string msg;
        private string msgException;
        private string msgMapper = "An error occured while trying to map hops.";

        public HopLogic(IHopRepository repo, IMapper mapper, ILogger<HopLogic> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public BLWarehouse ExportWarehouses()
        {
            try
            {
                _logger.LogInformation("HopLogic ExportWarehouses started.");
                var root = _mapper.Map<BLWarehouse>(_repo.GetRootWarehouse());
                return root;
            }
            catch (DataAccessException ex)
            {
                msg = "An error occured while trying to export warehouses.";
                _logger.LogError(msg, ex);
                throw new BusinessLogicException(nameof(ExportWarehouses), msg, ex);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(msgMapper, ex);
                throw new BusinessLogicException(nameof(ExportWarehouses), msgMapper, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to export warehouses.";
                _logger.LogError(msgException, ex);
                throw new BusinessLogicException(nameof(ExportWarehouses), msgException, ex);
            }
            
        }

        public bool ImportWarehouses(BLWarehouse blWarehouse)
        {
            try
            {
                _logger.LogInformation("HopLogic ImportWarehouses started.");
                var result = blWarehouseValidator.Validate(blWarehouse);
                if (result.IsValid)
                {
                    DALHop dalWarehouse = _mapper.Map<DALHop>(blWarehouse);
                    _repo.DeleteAllHops();
                    _repo.Create(dalWarehouse);
                    _logger.LogInformation("HopLogic ImportWarehouses ended successful.");
                    return true;
                }
                _logger.LogInformation("HopLogic ImportWarehouses ended unsuccessful.");
                return false;
            }
            catch (DataAccessException ex)
            {
                msg = "An error occured while trying to import warehouses.";
                _logger.LogError(msg, ex);
                throw new BusinessLogicException(nameof(ImportWarehouses), msg, ex);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(msgMapper, ex);
                throw new BusinessLogicException(nameof(ImportWarehouses), msgMapper, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to import warehouses.";
                _logger.LogError(msgException, ex);
                throw new BusinessLogicException(nameof(ImportWarehouses), msgException, ex);
            }

        }

        public BLHop GetWarehouse(string code)
        {
            try
            {
                _logger.LogInformation("HopLogic GetWarehouse started.");
                _logger.LogInformation("HopLogic GetWarehouse ended successful.");
                return _mapper.Map<BLWarehouse>(_repo.GetByCode(code));
            }
            catch (DataAccessException ex)
            {
                msg = "An error occured while trying to get a warehouse.";
                _logger.LogError(msg, ex);
                throw new BusinessLogicException(nameof(GetWarehouse), msg, ex);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(msgMapper, ex);
                throw new BusinessLogicException(nameof(GetWarehouse), msgMapper, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to get a warehouse.";
                _logger.LogError(msgException, ex);
                throw new BusinessLogicException(nameof(GetWarehouse), msgException, ex);
            }
            
        }
    }
}
