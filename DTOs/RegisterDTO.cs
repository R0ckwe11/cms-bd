using System.ComponentModel.DataAnnotations;
using cms_bd.Models;

namespace cms_bd.DTOs;

public class RegisterDTO
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