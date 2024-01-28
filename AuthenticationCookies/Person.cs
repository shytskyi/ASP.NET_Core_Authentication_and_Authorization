using Microsoft.AspNetCore.Identity;

namespace AuthenticationCookies
{
    public class Person
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public Person(string email, string password, Role role)
        {
            Email = email;  
            Password = password;
            Role = role;
        }
    }
}
