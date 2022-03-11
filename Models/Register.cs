using System.ComponentModel.DataAnnotations;

namespace cms_bd.Models;

public class Register
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }

    public User CreateUser() {
        return new User {
            UserName = UserName,
            Email = Email
        };
    }
}