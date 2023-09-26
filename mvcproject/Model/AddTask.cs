using mvcproject.Enitiy;

namespace mvcproject.Model
{
    public class AddTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Data { get; set; }
        public string Description { get; set; }
        public int LessonsId { get; set; }
        public bool complete { get; set; }
    }
}
