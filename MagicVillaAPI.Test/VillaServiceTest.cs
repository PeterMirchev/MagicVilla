using AutoMapper;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto.Villa;
using MagicVillaAPI.Repositories;
using MagicVillaAPI.Services;
using Moq;

namespace MagicVillaAPI.Test
{
    public class VillaServiceTest
    {
        private readonly Mock<IVillaRepository> _villaRepoMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly VillaService _villaService;

        public VillaServiceTest()
        {
            _villaRepoMock = new Mock<IVillaRepository>();
            _mapperMock = new Mock<IMapper>();
            _villaService = new VillaService
                (
                _villaRepoMock.Object, 
                _mapperMock.Object
                );
        }

        [Fact]
        public async Task GetVillaByIdAsync_ThenHappyPath()
        {
            Guid id = Guid.NewGuid();
            var villa = new Villa
            {
                Id = id,
                Name = "Test",
            };
            _villaRepoMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(villa);

            var result = await _villaService.GetVillaByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Name);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetVillaByIdAsync_ThenThrowsKeyNotFoundException()
        {
            _villaRepoMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Villa?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _villaService.GetVillaByIdAsync(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetAllVillasAsync_ThenHappyPath()
        {
            var villas = new List<Villa> {
                new Villa
                {
                    Id = Guid.NewGuid(),
                    Name = "One",
                },
                new Villa
                {
                    Id = Guid.NewGuid(),
                    Name = "Two",
                }
            };

            _villaRepoMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(villas);

            var result = await _villaService.GetAllVillasAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.LongCount());
        }

        [Fact]
        public async Task CreateVillaAsync_ThenHappyPath()
        {
            var request = new VillaCreateRequest
            {
                Name = "name",
                PricePerDay = 10,
            };

            var villa = new Villa
            {
                Id = Guid.NewGuid()
            };

            _mapperMock
                .Setup(m => m.Map<VillaCreateRequest, Villa>(request))
                .Returns(villa);
            _villaRepoMock
                .Setup(r => r.CreateAsync(villa))
                .ReturnsAsync(villa);

            var result = await _villaService.CreateVillaAsync(request);

            Assert.NotNull(result);
            Assert.Equal(villa.Id, result.Id);

            _villaRepoMock.Verify(r => r.CreateAsync(It.IsAny<Villa>()), Times.Once);
        }
    }
}
