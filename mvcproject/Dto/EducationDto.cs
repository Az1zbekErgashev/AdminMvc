using mvcproject.Enitiy;

namespace mvcproject.Dto
{
    public class EducationDto
    {
        public int Id { get; set; }
        public string Tranding { get; set; }
        public List<string?> courseСompletion { get; set; }
        public string? Description { get; set; }
        public Course? Course { get; set; }
        public int? CourseID { get; set; }

    }
}
