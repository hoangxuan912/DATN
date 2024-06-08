using System.ComponentModel.DataAnnotations;

namespace asd123.Presenters.Major;

public class UpdateMajorPresenter
{
    [Required]
    public string Code { get; set; }
    [Required]
    public string Name { get; set; }
}