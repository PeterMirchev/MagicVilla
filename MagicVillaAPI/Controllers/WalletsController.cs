using AutoMapper;
using MagicVillaAPI.Models.Dto.Wallet;
using MagicVillaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
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
            var user = await _userService.GetUserByIdAsync(request.UserId);
            var wallet = await _walletService.CreateWalletAsync(request);

            WalletResponse response = _mapper.Map<WalletResponse>(wallet);

            return Created($"/api/v1/wallets/{wallet.Id}", response);
        }
    }
}
