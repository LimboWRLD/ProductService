using TiacPraksaP1.Models;

namespace TiacPraksaP1.DTOs.Post
{
    public class UserPostRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }
    }
}
