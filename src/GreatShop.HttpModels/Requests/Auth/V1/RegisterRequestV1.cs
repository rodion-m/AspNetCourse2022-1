using System.ComponentModel.DataAnnotations;

namespace GreatShop.HttpModels.Requests.Auth.V1;

public class RegisterRequestV1
{
    [Required] public string Name { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
}