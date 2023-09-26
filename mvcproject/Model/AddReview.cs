namespace mvcproject.Model
{
    public class AddReview
    {
        public int Id { get; set; }
        public string Course_name { get; set; }
        public string Course_info { get; set; }
        public string comment { get; set; }
        public int TaskId { get; set; }
    }
}
