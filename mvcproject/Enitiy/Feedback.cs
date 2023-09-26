namespace mvcproject.Enitiy
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone_Number { get; set; }
        public string Date { get; set; }
        public Course Course { get; set; }
        public User User { get; set; }
    }
}
