namespace Domain.Entities
{
    public class UserEntity : Entity<UserEntity>
    {
        public UserEntity(string email, string password, string name, string surname, int age)
        {
            Email = email;
            Password = password;
            Name = name;
            Surname = surname;
            Age = age;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}