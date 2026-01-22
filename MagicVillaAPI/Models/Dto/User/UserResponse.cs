using MagicVillaAPI.Models.Dto.Wallet;

namespace MagicVillaAPI.Models.Dto.User
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public List<WalletResponse> wallets { get; set; } = new List<WalletResponse>();
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
