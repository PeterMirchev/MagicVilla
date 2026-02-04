using AutoMapper;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto.User;
using MagicVillaAPI.Repositories;
using MagicVillaAPI.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace MagicVillaAPI.Test
{
    public class UserServiceTest
    {

        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IAuditRecordService> _auditMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<UserService>> _loggerMock;

        private readonly UserService _userService;

        public UserServiceTest()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _auditMock = new Mock<IAuditRecordService>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<UserService>>();

            _userService = new UserService(
                _userRepoMock.Object,
                _auditMock.Object,
                _mapperMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task CreateUserAsync_ThenHappyPathAsync()
        {
            // Arrange
            var request = new UserCreateRequest
            {
                Username = "Test",
                Email = "test@test.com"
            };
            var mappedUser = new User
            {
                Id = Guid.NewGuid(),
            };

            _mapperMock
                .Setup(u => u.Map<UserCreateRequest, User>(request))
                .Returns(mappedUser);
            _userRepoMock
                .Setup(r => r.CreateAsync(It.IsAny<User>()))
                .Returns(Task.FromResult(mappedUser));
            _auditMock
                .Setup(a => a.CreateAsync(
                    It.IsAny<User>(),
                    ActionType.USER_REGISTRATION,
                    It.IsAny<Guid>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<string>()))
                .ReturnsAsync(new AuditRecord());
            // Act
            var result = await _userService.CreateUserAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsActive);

            _userRepoMock.Verify(r => r.CreateAsync(It.IsAny<User>()), Times.Once);
            _auditMock.Verify(a => a.CreateAsync(
                It.IsAny<User>(),
                ActionType.USER_REGISTRATION,
                It.IsAny<Guid>(),
                It.IsAny<DateTime>(),
                It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task GetAllUsersAsync_ThenRetrnCollection()
        {
            // Arrange
            var users = new List<User>
            {
                new User
                {
                   Id = Guid.NewGuid(),
                   Username = "userOne",
                   Email = "test1@test.com",
                   IsActive = true
                },
                new User
                    {
                    Id = Guid.NewGuid(),
                    Username = "userTwo",
                    Email = "test2@test.com",
                    IsActive = true
                    }
            };

            _userRepoMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetUserById_ThenHappyPath()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "Test",
                Email = "test@test.com",
                IsActive = true,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            _userRepoMock
                .Setup(r => r.GetByIdAsync(user.Id))
                .ReturnsAsync(user);


            var result = await _userService.GetUserByIdAsync(user.Id);

            _userRepoMock.Verify(r => r.GetByIdAsync(user.Id), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.True(result.IsActive);
        }

        [Fact]
        public async Task GetUserByUsername_ThenHappyPath()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "Test",
                Email = "test@test.com",
                IsActive = true,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            _userRepoMock
                .Setup(r => r.GetByUsernameAsync("Test"))
                .ReturnsAsync(user);

            var result = await _userService.GetUserByUsernameAsync("Test");

            _userRepoMock.Verify(r => r.GetByUsernameAsync(user.Username), Times.Once);
            
            Assert.NotNull(result);
            Assert.Equal("Test", user.Username);
        }

        [Fact]
        public async Task GetUserById_ThenThrowKeyNotFoundException()
        {
            var id = Guid.NewGuid();
            _userRepoMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((User?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _userService.GetUserByIdAsync(id));
        }

        [Fact]
        public async Task GetUserByUsername_ThenThrowKeyNotFoundException()
        {
            _userRepoMock
                .Setup(r => r.GetByUsernameAsync("Username"))
                .ReturnsAsync((User?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _userService.GetUserByUsernameAsync("Username"));
        }

        [Fact]
        public async Task UpdateUserAsync_ThenHappyPath()
        {
            var id = Guid.NewGuid();
            var request = new UserUpdateRequest
            {
                Username = "New"
            };
            var existingUser = new User
            {
                Id = id,
                Username = "Old"
            };

            _userRepoMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(existingUser);

            _userRepoMock
                .Setup(r => r.UpdateAsync(existingUser))
                .ReturnsAsync(existingUser);

            _mapperMock
                .Setup(m => m.Map(request, existingUser));

            // Act
            var result = await _userService.UpdateUserAsync(id, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingUser, result);

            _userRepoMock.Verify(r => r.GetByIdAsync(id), Times.Once);
            _userRepoMock.Verify(r => r.UpdateAsync(existingUser), Times.Once);
            _mapperMock.Verify(m => m.Map(request, existingUser), Times.Once);
        }

        [Fact]
        public async Task EnableDisableUser_ThenHappyPath()
        {
            var id = Guid.NewGuid();
            var request = new EnableDisableUserRequest
            {
                EnableDisable = false
            };
            var user = new User
            {
                Id = id,
                IsActive = true
            };

            _userRepoMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(user);
            _userRepoMock
                .Setup(r => r.UpdateAsync(user))
                .ReturnsAsync(user);

            var result = await _userService.EnableDisableUserAsync(id, request);

            Assert.NotNull(result);
            Assert.False(result.IsActive);

            _userRepoMock.Verify(r => r.GetByIdAsync(id), Times.Once);
            _userRepoMock.Verify(r => r.UpdateAsync(user), Times.Once);
        }
    }
}
