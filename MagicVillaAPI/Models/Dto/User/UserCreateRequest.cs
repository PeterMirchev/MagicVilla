using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Models.Dto.User
{
    public class UserCreateRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
    }
}
