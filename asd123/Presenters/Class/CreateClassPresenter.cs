using asd123.Model;
using System.ComponentModel.DataAnnotations;

namespace asd123.Presenters.Class
{
    public class CreateClassPresenter
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string MajorName { get; set; }
    }
}
