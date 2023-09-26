using mvcproject.Enitiy;

namespace mvcproject.Model
{
    public class AddEducation
    {
        public string Tranding { get; set; }
        public List<string?> courseСompletion { get; set; }
        public string? Description { get; set; }
        public int? CourseID { get; set; }
    }
}
