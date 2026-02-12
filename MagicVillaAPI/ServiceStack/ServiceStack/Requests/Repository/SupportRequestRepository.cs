using MagicVillaAPI.ServiceStack.ServiceStack.Requests.Models.SupportRequests;
using MagicVillaAPI.ServiceStack.ServiceStack.Requests.Repository;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace MagicVillaAPI.ServiceStack.ServiceStack.User.Repository
{
    public class SupportRequestRepository : IRequestRepository<SupportRequest>
    {
        private readonly IDbConnectionFactory _dbFactory;

        public SupportRequestRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<SupportRequest> CreateAsync(SupportRequest request)
        {
            using var db = _dbFactory.Open();
            await db.InsertAsync(request);
            return request;
        }

        public async Task<bool> DeleteByIDAsync(Guid id)
        {
            using var db = _dbFactory.Open();
            var rows = await db.DeleteAsync<SupportRequest>(x => x.Id == id);
            return rows > 0;
        }

        public async Task<List<SupportRequest>> GetAllByUserIdAsync(Guid userId)
        {
            using var db = _dbFactory.Open();
            return await db.SelectAsync<SupportRequest>(s => s.UserId == userId);
        }

        public async Task<SupportRequest> GetByIdAsync(Guid id)
        {
            using var db = _dbFactory.Open();
            return await db.SingleByIdAsync<SupportRequest>(id);
        }

        public async Task<SupportRequest> UpdateAsync(SupportRequest request)
        {
            using var db = _dbFactory.Open();
            await db.UpdateAsync(request);
            return request;
        }
    }
}
