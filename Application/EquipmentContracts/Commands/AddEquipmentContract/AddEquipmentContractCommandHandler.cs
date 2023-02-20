using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.EquipmentContracts.Commands.AddEquipmentContract
{
    internal sealed class AddEquipmentContractCommandHandler : ICommandHandler<AddEquipmentContractCommand>
    {
        private readonly IEquipmentContractRepository _equipmentContractRepository;
        private readonly IProductionFacilityRepository _productionFacilityRepository;
        private readonly IEquipmentTypeRepository _equipmentTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddEquipmentContractCommandHandler(IEquipmentContractRepository equipmentContractRepository, IUnitOfWork unitOfWork, IProductionFacilityRepository productionFacilityRepository, IEquipmentTypeRepository equipmentTypeRepository)
        {
            _equipmentContractRepository= equipmentContractRepository;
            _unitOfWork= unitOfWork;
            _productionFacilityRepository= productionFacilityRepository;
            _equipmentTypeRepository = equipmentTypeRepository;
        }

        public async Task<Result> Handle(AddEquipmentContractCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var availableSpace = await _productionFacilityRepository.GetAreaAsync(request.ProductionFacilityId, cancellationToken);
                
                if (availableSpace == 0)
                {
                    return Result.Failure(DomainErrors.ProductionFacility.NotFound(request.ProductionFacilityId));
                }

                var equipmentArea = await _equipmentTypeRepository.GetAreaByIdAsync(request.EquipmentTypeId, cancellationToken);

                if (equipmentArea == 0)
                {
                    return Result.Failure(DomainErrors.EquipmentType.NotFound(request.EquipmentTypeId));
                }

                var usedSpace = await _equipmentContractRepository.GetUsedSpaceAsync(request.ProductionFacilityId, cancellationToken);

                if (availableSpace-usedSpace<equipmentArea*request.EquipmentQuantity)
                {
                    return Result.Failure(DomainErrors.Contracts.NotEnoughSpace(availableSpace-usedSpace));
                }

                var contract = new EquipmentContract(Guid.NewGuid(), request.ProductionFacilityId, request.EquipmentTypeId, request.EquipmentQuantity);

                await _equipmentContractRepository.AddAsync(contract, cancellationToken);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }

            return Result.Success();
        }
    }
}
