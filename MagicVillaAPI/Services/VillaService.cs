using AutoMapper;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto.Villa;
using MagicVillaAPI.Repositories;

namespace MagicVillaAPI.Services
{
    public class VillaService : IVillaService
    {
        
        private readonly IVillaRepository _repository;
        private readonly IMapper _mapper;

        public VillaService(IVillaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Villa?> GetVillaByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id)
                   ?? throw new KeyNotFoundException($"Villa with id {id} not found");
        }

        public async Task<List<Villa>> GetAllVillasAsync()
        {
            var villas = await _repository.GetAllAsync();
            return villas.ToList();
        }

        public async Task<Villa> CreateVillaAsync(VillaCreateRequest request)
        {
            Villa villa = _mapper.Map<VillaCreateRequest, Villa>(request);
            villa.CreatedDate = DateTime.UtcNow;
            return await _repository.CreateAsync(villa);
        }

        public async Task<Villa> UpdateVillaAsync(VillaUpdateRequest request, Guid id)
        {
            var villa = await GetVillaByIdAsync(id);
            _mapper.Map(request, villa);
            return await _repository.UpdateAsync(villa);
        }

        public async Task DeleteVillaAsync(Guid id)
        {
            var villa = await GetVillaByIdAsync(id);
            await _repository.DeleteAsync(villa); 
        }
    }
}
