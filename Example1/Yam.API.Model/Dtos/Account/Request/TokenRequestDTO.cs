using System.ComponentModel.DataAnnotations;

namespace Yam.API.Model.Dtos.Account.Request;

public class TokenRequestDTO
{
    [Required]
    public string Token { get; set; }
    [Required]
    public string RefreshToken { get; set; }

}
