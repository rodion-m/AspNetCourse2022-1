using System.ComponentModel.DataAnnotations;

namespace Lesson14.Models.Requests;

public class LogInRequest
{
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
}