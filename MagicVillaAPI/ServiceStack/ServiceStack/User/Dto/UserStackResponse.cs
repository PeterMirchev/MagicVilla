using MagicVillaAPI.ServiceStack.ServiceStack.User.Model;

namespace MagicVillaAPI.Services.ServiceStack.User.Dto
{
    public class UserStackResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
