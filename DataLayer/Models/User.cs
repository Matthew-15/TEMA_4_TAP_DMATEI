namespace DataLayer.Models
{
    public class User
    {
        public User(string name, string email, string password, Guid typeId)
        {
            Name = name;
            Email = email;
            Password = password;
            TypeId = typeId;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid TypeId { get; set; }
        public UserType Type { get; set; }
    }
}
