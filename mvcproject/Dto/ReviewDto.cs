namespace mvcproject.Dto;

public class ReviewDto
{
    public int Id { get; set; }
    public string Course_name { get; set; }
    public string Course_info { get; set; }
    public string comment { get; set; }
    public Enitiy.Task Task { get; set; }
}
