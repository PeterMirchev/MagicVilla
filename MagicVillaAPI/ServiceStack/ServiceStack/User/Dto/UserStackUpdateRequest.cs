using MagicVillaAPI.Services.ServiceStack.User.Dto;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace MagicVillaAPI.ServiceStack.ServiceStack.User.Dto
{
    [Route("/users", verbs: "PUT")]
    public class UserStackUpdateRequest : IReturn<UserStackResponse>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
