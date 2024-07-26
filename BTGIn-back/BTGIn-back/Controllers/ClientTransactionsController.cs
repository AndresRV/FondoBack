using BTGIn_back.Business.Contracts;
using BTGIn_back.Entitites;
using BTGIn_back.Entitites.DTO.Request;
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










        /*
         
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entities = await _clientService.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var entity = await _clientService.GetAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Client entity)
        {
            await _clientService.CreateAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Client entity)
        {
            var existingEntity = await _clientService.GetAsync(id);
            if (existingEntity == null)
            {
                return NotFound();
            }

            await _clientService.UpdateAsync(id, entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingEntity = await _clientService.GetAsync(id);
            if (existingEntity == null)
            {
                return NotFound();
            }

            await _clientService.DeleteAsync(id);
            return NoContent();
        }         
         
         */
    }
}
