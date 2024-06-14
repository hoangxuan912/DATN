namespace asd123.DTO;

public class SubjectDto
{
    public int SubjectId { get; set; }
    public string Name { get; set; }
    public ICollection<MarksDto> Marks { get; set; }
}