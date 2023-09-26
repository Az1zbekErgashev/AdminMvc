using mvcproject.Enitiy;

namespace mvcproject.Dto;

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Data { get; set; }
    public string Description { get; set; }
    public bool complete { get; set; }
    public Lessons Lessons { get; set; }
}
