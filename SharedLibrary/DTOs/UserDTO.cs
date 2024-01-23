

namespace SharedLibrary.Models.DTOs
{
    public class UserDTO : BaseClass
    {
        public UserDTO(User user) { Id = user.Id; Username = user.Username; }
        public string Username { get; set; }
    }
}
