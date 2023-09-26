namespace mvcproject.Enitiy
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Lessons Lessons { get; set; }
        public bool complete { get; set; }
        private DateTime _data;

        public DateTime Data
        {
            get { return _data; }
            set { _data = value.ToUniversalTime(); }
        }
    }
}
