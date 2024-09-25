using System.ComponentModel.DataAnnotations;

namespace Yam.API.Model.Dtos.Account.Request;

public class LoginUserDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
