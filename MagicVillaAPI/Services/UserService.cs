using AutoMapper;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto.User;
using MagicVillaAPI.Repositories;

namespace MagicVillaAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuditRecordService _auditRecordService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, 
            IAuditRecordService auditRecordService, 
            IMapper mapper, 
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _auditRecordService = auditRecordService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<User> CreateUserAsync(UserCreateRequest request)
        {
            User user = _mapper.Map<UserCreateRequest, User>(request);
            user.IsActive = true;
            user.CreatedOn = DateTime.UtcNow;
            user.UpdatedOn = DateTime.UtcNow;

            await _userRepository.CreateAsync(user);

            string Details = "User successfully registered.";
            await _auditRecordService.CreateAsync(user, ActionType.USER_REGISTRATION, user.Id, DateTime.UtcNow, Details);

            _logger.LogInformation("User created. UserId={ReservationId}", user.Id);

            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.ToList();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"User with ID '{id}' not found.");
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username)
                ?? throw new KeyNotFoundException($"User with username '{username}' not found.");
        }


        public async Task<User> UpdateUserAsync(Guid id, UserUpdateRequest request)
        {
            var user = await GetUserByIdAsync(id);
            _mapper.Map(request, user);
            user.UpdatedOn = DateTime.UtcNow;

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<User> EnableDisableUserAsync(Guid id, EnableDisableUserRequest request)
        {
            User user = await GetUserByIdAsync(id);
            user.IsActive = request.EnableDisable;
            user.UpdatedOn = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            return user;
        }
    }
}
