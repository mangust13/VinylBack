using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseStatusController : ControllerBase
    {
        private readonly IPurchaseStatusService _service;

        public PurchaseStatusController(IPurchaseStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PurchaseStatusDTO>>> GetAll()
        {
            return Ok(await _service.GetPurchaseStatusServicesAll());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PurchaseStatusDTO>> GetById(int id)
        {
            var item = await _service.GetPurchaseStatusServiceById(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseStatusDTO>> Create([FromBody] PurchaseStatusDTO dto)
        {
            var created = await _service.CreatePurchaseStatusService(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.PurchaseStatusId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PurchaseStatusDTO dto)
        {
            var updated = await _service.UpdatePurchaseStatusService(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeletePurchaseStatusService(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
