using System.ComponentModel.DataAnnotations;
using MagicVillaAPI.Services.ServiceStack.User.Dto;
using ServiceStack;

namespace MagicVillaAPI.ServiceStack.ServiceStack.User.Dto
{
    [Route("/users/{Id}", verbs: "GET")]
    public class UserStackGetByIdReuqest : IReturn<UserStackResponse>
    {
        public Guid Id { get; set; }
    }
}
