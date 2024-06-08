using System.ComponentModel.DataAnnotations;

namespace asd123.Presenters.Department
{
    public class CreateDepartmentRequest
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
