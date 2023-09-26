using mvcproject.Enitiy;

namespace mvcproject.Dto;

public class ResultDto
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public Education Education { get; set; }
    public User User { get; set; }
}
