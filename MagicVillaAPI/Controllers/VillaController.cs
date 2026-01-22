using Asp.Versioning;
using AutoMapper;
using MagicVillaAPI.Models.Dto.Villa;
using MagicVillaAPI.Services;
using Microsoft.AspNetCore.Mvc;


namespace MagicVillaAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[ApiVersion(1)]
public class VillasController : ControllerBase
{

    private readonly IVillaService _villaService;
    private readonly IMapper _mapper;

    public VillasController(IVillaService villaService, IMapper mapper)
    {
        _villaService = villaService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VillaResponse>>> GetVillas()
    {
        var villas = await _villaService.GetAllVillasAsync();
        var response = _mapper.Map<List<VillaResponse>>(villas);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VillaResponse>> GetVillaById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest();
        }

        var villa = await _villaService.GetVillaAsync(id);
        var response = _mapper.Map<VillaResponse>(villa);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VillaResponse>> CreateVilla([FromBody] VillaCreateRequest request)
    {
        var villa = await _villaService.CreateVillaAsync(request);
        var response = _mapper.Map<VillaResponse>(villa);
        
        return Created($"/api/v1/villas/{villa.Id}", response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VillaResponse>> UpdateVilla([FromBody] VillaUpdateRequest request, Guid id)
    {
        var villa = await _villaService.UpdateVillaAsync(request, id);
        var response = _mapper.Map<VillaResponse>(villa);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteVilla(Guid id)
    {
        await _villaService.DeleteVillaAsync(id);
        return NoContent();
    }
}
