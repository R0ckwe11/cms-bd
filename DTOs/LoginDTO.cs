using System.ComponentModel.DataAnnotations;

namespace cms_bd.DTOs;

public class LoginDTO {
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}