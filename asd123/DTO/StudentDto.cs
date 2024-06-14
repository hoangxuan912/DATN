namespace asd123.DTO;

public class StudentDto
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public ICollection<MarksDto> Marks { get; set; }
}