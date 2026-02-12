using ServiceStack;

namespace MagicVillaAPI.ServiceStack.ServiceStack.User.Dto
{
    [Route("/users", verbs: "GET")]
    public class UserStackGetAllUsersRequest : IReturn<UserStackCollectionResponse>
    {
    }
}
