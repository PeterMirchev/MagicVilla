using ServiceStack;

namespace MagicVillaAPI.Services.ServiceStack.User.Dto
{
    [Route("/users", verbs: "POST")]
    public class UserStackRequest : IReturn<UserStackResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber {  get; set; }
        public string Email { get; set; }
    }
}
