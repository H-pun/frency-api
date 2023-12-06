using Frency.Base;
using Frency.DataAccess.Entities;

namespace Frency.DataAccess.Models
{
    public class UpdateUserRequest : BaseModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
