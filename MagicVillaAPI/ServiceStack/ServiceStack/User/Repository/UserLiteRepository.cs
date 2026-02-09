using MagicVillaAPI.Models;
using MagicVillaAPI.ServiceStack.ServiceStack.User.Model;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace MagicVillaAPI.ServiceStack.ServiceStack.User.Repository
{
    public class UserLiteRepository : IUserLiteRepository
    {
        private readonly IDbConnectionFactory _dbFactory;

        public UserLiteRepository(IDbConnectionFactory reppository)
        {
            _dbFactory = reppository;
        }

        public async Task<UserLite> CreateAsync(UserLite user)
        {
            using var db = _dbFactory.Open();
            await db.InsertAsync(user);
            return user;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using var db = _dbFactory.Open();
            var rows = await db.DeleteAsync<UserLite>(x => x.Id == id);
            return rows > 0;
        }

        public async Task<List<UserLite>> GetAllAsync()
        {
            using var db = _dbFactory.Open();
            return await db.SelectAsync<UserLite>();
        }

        public async Task<UserLite> GetByIdAsync(Guid id)
        {
            using var db = _dbFactory.Open();
            return await db.SingleByIdAsync<UserLite>(id);
        }

        public async Task<UserLite> GetByEmailAsync(string email)
        {
            using var db = _dbFactory.Open();
            return await db.SingleAsync<UserLite>(u => u.Email == email);
        }

        public async Task<UserLite> UpdateAsync(UserLite user)
        {
            using var db = _dbFactory.Open();
            await db.UpdateAsync(user);
            return user;
        }
    }
}
