using Asp.Versioning;
using AutoMapper;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto.Reservation;
using MagicVillaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiVersion(1)]
    public class ReservationsController :ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReservationResponse>> CreateReservation([FromBody] ReservationCreateRequest request)
        {
            Reservation reservation = await _reservationService.CreateReservationAsync(request);
            ReservationResponse response = _mapper.Map<ReservationResponse>(reservation);

            return Created($"/api/v1/reservations/{reservation.Id}", response);
        }

        [HttpGet("{reservationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReservationResponse>> GetReservationById(Guid reservationId)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);
            var response = _mapper.Map<ReservationResponse>(reservation);

            return Ok(response);
        }
        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ReservationResponse>>> GetAllReservationsByUserId(Guid userId)
        {
            var reservations = await _reservationService.GetAllReservationsByUserIdAsync(userId);
            var response = _mapper.Map<ICollection<ReservationResponse>>(reservations);

            return Ok(response);
        }

    }
}
