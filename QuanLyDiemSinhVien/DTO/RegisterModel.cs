using System.ComponentModel.DataAnnotations;

namespace asd123.DTO;

public class RegisterModel
{
    [Required]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
}
