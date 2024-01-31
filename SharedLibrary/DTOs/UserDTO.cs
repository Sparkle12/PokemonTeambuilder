

namespace SharedLibrary.Models.DTOs
{
    public class UserDTO : BaseClass
    {
        public UserDTO() { }
        public UserDTO(User user) 
        { 
            Id = user.Id; 
            Username = user.Username;
            TeamId = user.TeamId;
            Role = user.Role.Name;
        }
        public string Username { get; set; }
        public string Role { get; set; }
        public int TeamId { get; set; }
    }
}
