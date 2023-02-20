using Application.EquipmentContracts.Commands.AddEquipmentContract;
using Application.EquipmentContracts.Queries.GetAllEquipmentContracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Authentication;

namespace Presentation.Controllers
{
    [Route("api/contract")]
    [ApiKeyAuthorization]
    public sealed class ContractController : ApiController
    {

        public ContractController(ISender sender):base(sender)
        {
        }

        [HttpGet("AllContracts")]
        public async Task<IActionResult> AllContracts(CancellationToken cancellationToken)
        {
            
            var query = new GetAllEquipmentContractsQuery();

            var result = await Sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost("AddContract")]
        public async Task<IActionResult> AddContract([FromBody] AddContractRequest request,
            CancellationToken cancellationToken)
        {
            var command = new AddEquipmentContractCommand(request.ProductionFacilityId, request.EquipmentTypeId,
                request.Quantity);
            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok() : HandleFailure(result);
        }
    }
}
