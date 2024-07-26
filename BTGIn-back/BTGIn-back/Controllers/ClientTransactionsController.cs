using BTGIn_back.Business.Contracts;
using BTGIn_back.Entitites.DTO.Request;
using BTGIn_back.Entitites.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BTGIn_back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientTransactionsController(IClientTransactionsService _clientTransactionsService) : ControllerBase
    {
        [HttpPost("/fund")]
        public async Task<IActionResult> FundInscription([FromBody] FundInscriptionRequest fundInscriptionRequest)
        {
            await _clientTransactionsService.FundInscription(fundInscriptionRequest);
            return Created();
        }

        [HttpDelete("/fund")]
        public async Task<IActionResult> FundDisenrollment([FromBody] FundDisenrollmentRequest fundDisenrollmentRequest)
        {
            await _clientTransactionsService.FundDisenrollment(fundDisenrollmentRequest);
            return NoContent();
        }

        [HttpGet("user/{clientIdentification}")]
        public async Task<IActionResult> GetTransactionsHistory(int clientIdentification)
        {
            List<TransactionsHistoryResponse> transactionsHistories = await _clientTransactionsService.GetTransactionsHistory(clientIdentification);
            return Ok(transactionsHistories);
        }
    }
}
