using mvcproject.Enitiy;

namespace mvcproject.Dto;

public class LessonsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Video_url { get; set; }
    public string Description { get; set; }
    public string Vedeo_url2 { get; set; }
    public Course Course { get; set; }
}
