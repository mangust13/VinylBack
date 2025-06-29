﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetAll()
        {
            return Ok(await _service.GetAllRoles());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<RoleDTO>> GetById(int id)
        {
            var item = await _service.GetRoleById(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<RoleDTO>> Create([FromBody] RoleDTO dto)
        {
            var created = await _service.CreateRole(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.RoleId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoleDTO dto)
        {
            var updated = await _service.UpdateRole(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteRole(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
