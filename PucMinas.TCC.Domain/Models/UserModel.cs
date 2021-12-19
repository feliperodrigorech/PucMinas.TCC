namespace PucMinas.TCC.Domain.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsSuspended { get; set; }
    }
}
