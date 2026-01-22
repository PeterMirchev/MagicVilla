using Asp.Versioning;
using AutoMapper;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto.Wallet;
using MagicVillaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiVersion(1)]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public WalletsController(
            IWalletService walletService,
            IUserService userService,
            IMapper mapper)
        {
            _walletService = walletService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateWalletAsync([FromBody] WalletCreateRequest request)
        {
            var wallet = await _walletService.CreateWalletAsync(request);
            WalletResponse response = _mapper.Map<WalletResponse>(wallet);

            return Created($"/api/v1/wallets/{wallet.Id}", response);
        }

        [HttpGet("{walletId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WalletResponse>> GetWalletByIdAsync(Guid walletId)
        {
            var wallet = await _walletService.GetWalletByIdAsync(walletId);
            WalletResponse response = _mapper.Map<WalletResponse>(wallet);

            return Ok(response);
        }

        [HttpGet("/user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ICollection<WalletResponse>>> GetAllWalletsAsync(Guid userId)
        {
            var wallets = await _walletService.GetWalletsByUserIdAsync(userId);
            var response = _mapper.Map<ICollection<WalletResponse>>(wallets);

            return Ok(response);
        }

    }
}
