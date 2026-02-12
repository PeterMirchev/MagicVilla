using MagicVillaAPI.Services.ServiceStack.User.Dto;
using ServiceStack;

namespace MagicVillaAPI.ServiceStack.ServiceStack.User.Dto
{
    [Route("/users/email", verbs: "GET")]
    public class UserStackGetByEmailRequest : IReturn<UserStackResponse>
    {
        public string? Email { get; set; }
    }
}
