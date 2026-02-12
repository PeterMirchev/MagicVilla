using MagicVillaAPI.ServiceStack.ServiceStack.Requests.Models.RecommendationRequests;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace MagicVillaAPI.ServiceStack.ServiceStack.Requests.Repository
{
    public class RecommendationRequestRepository : IRequestRepository<RecommendationRequest>
    {
        private readonly IDbConnectionFactory _dbFactory;
        public RecommendationRequestRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<RecommendationRequest> CreateAsync(RecommendationRequest request)
        {
            using var db = _dbFactory.Open();
            await db.InsertAsync(request);
            return request;
        }

        public async Task<bool> DeleteByIDAsync(Guid id)
        {
            using var db = _dbFactory.Open();
            var rows = await db.DeleteAsync<RecommendationRequest>(r => r.Id == id);
            return rows > 0;
        }

        public async Task<List<RecommendationRequest>> GetAllByUserIdAsync(Guid userId)
        {
            using var db = _dbFactory.Open();
            return await db.SelectAsync<RecommendationRequest>(r => r.UserId == userId);
        }

        public async Task<RecommendationRequest> GetByIdAsync(Guid id)
        {
            using var db = _dbFactory.Open();
            return await db.SingleByIdAsync<RecommendationRequest>(id);
        }

        public async Task<RecommendationRequest> UpdateAsync(RecommendationRequest request)
        {
            using var db = _dbFactory.Open();
            await db.UpdateAsync(request);
            return request;
        }
    }
}
