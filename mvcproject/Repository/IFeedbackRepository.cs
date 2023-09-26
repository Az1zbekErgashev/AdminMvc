using mvcproject.Dto;
using mvcproject.Enitiy;
using Task = System.Threading.Tasks.Task;

namespace mvcproject.Repository;

public interface IFeedbackRepository

{
    Task<List<FeedbackDto>> GetAllFeetback();
    Task<Feedback> GetFeedbackById(int id);
    Task CreateFeedback(Feedback feedback);
    Task DeleteFeedback(int id);
    public Task <List<FeedbackDto>> GetUserFeedback(int id);
}
