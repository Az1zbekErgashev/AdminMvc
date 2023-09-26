using mvcproject.Enitiy;

namespace mvcproject.Dto;

public class HomeWorkDto
{
    public int Id { get; set; }
    public string Img { get; set; }
    public string Title { get; set; }
    public string Name { get; set; }
    public Enitiy.Task Task { get; set; }
}