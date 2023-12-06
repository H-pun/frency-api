using Frency.Base;

namespace Frency.DataAccess.Models
{
    public class LoginResponse : BaseModel
    {
        public Guid IdUser { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string AppToken { get; set; }
    }
}
