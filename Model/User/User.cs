using static Model.Enums;

namespace Model.User
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
    }
}
