using TiacPraksaP1.Models;

namespace TiacPraksaP1.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }
    }
}
