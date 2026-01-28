using AutoMapper;
using MagicVillaAPI.Models.Dto.User;
using MagicVillaAPI.Services;
using MagicVillaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace MagicVillaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiVersion(1)]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserResponse>> CreateUser([FromBody] UserCreateRequest request)
        {
            var user = await _userService.CreateUserAsync(request);
            var response = _mapper.Map<UserResponse>(user);
            return Created($"/api/v1/users/{user.Id}", response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponse>> GetUserNyId(Guid id)
        {
            var users = await _userService.GetUserByIdAsync(id);
            var response = _mapper.Map<UserResponse>(users);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UserResponse>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            var response = _mapper.Map<List<UserResponse>>(users);

            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserResponse>> UpdateUser(Guid id, [FromBody] UserUpdateRequest request)
        {
            var user = await _userService.UpdateUserAsync(id, request);
            var response = _mapper.Map<UserResponse>(user);

            return Ok(response);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserResponse>> EnableUserAsync(Guid id, [FromBody] EnableDisableUserRequest request)
        {
            User user = await _userService.EnableDisableUserAsync(id, request);
            var response = _mapper.Map<UserResponse>(user);

            return Ok(response);
        }
    }
}
