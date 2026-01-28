using Asp.Versioning;
using MagicVillaAPI.Models;
using MagicVillaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiVersion(1)]
    public class AuditRecordsController : ControllerBase
    {
        private readonly IAuditRecordService _auditRecordService;

        public AuditRecordsController(IAuditRecordService auditRecordService)
        {
            _auditRecordService = auditRecordService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuditRecord>> GetById(Guid id)
        {
            var record = await _auditRecordService.GetByIdAsync(id);
            return Ok(record);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuditRecord>> GetByUserId(Guid userId)
        {
            var record = await _auditRecordService.GetAllByUserId(userId);
            return Ok(record);
        }
    }
}
