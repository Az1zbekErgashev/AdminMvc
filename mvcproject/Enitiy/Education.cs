namespace mvcproject.Enitiy
{
    public class Education
    {
        public int Id { get; set; }
        public Course? course { get; set; }
        public string? Tranding { get; set; }
        public List<string?> courseСompletion { get; set; }
        public string Description { get; set; }
    }
}
