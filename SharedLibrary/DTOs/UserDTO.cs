

namespace SharedLibrary.Models.DTOs
{
    public class UserDTO : BaseClass
    {
        public UserDTO(User user) 
        { 
            Id = user.Id; 
            Username = user.Username;
            TeamId = user.TeamId;
        }
        public string Username { get; set; }
        public int TeamId { get; set; }
    }
}
