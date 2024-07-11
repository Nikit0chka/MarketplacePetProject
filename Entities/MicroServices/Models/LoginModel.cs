using System.ComponentModel.DataAnnotations;

namespace Entities.MicroServices.Models;

public class LoginModel
{
    [StringLength(250, MinimumLength = 1)] public required string Login { get; set; }

    [StringLength(250, MinimumLength = 1)] public required string Password { get; set; }
}