using ServiceStack.DataAnnotations;

namespace MagicVillaAPI.ServiceStack.ServiceStack.User.Model
{
    public class UserLite
    {
        [PrimaryKey]
        [Default("gen_random_uuid()")]
        public Guid Id { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        [Unique]
        public string PhoneNumber { get; set; }
        [Unique]
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
