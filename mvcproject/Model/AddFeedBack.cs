using mvcproject.Enitiy;

namespace mvcproject.Model
{
    public class AddFeedBack
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone_Number { get; set; }
        public string Date { get; set; }
        public int User_id { get; set; }
        public int Course_id { get; set; }
    }
}
