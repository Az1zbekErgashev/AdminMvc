using mvcproject.Dto;
using mvcproject.Enitiy;
using Task = System.Threading.Tasks.Task;

namespace mvcproject.Repository
{
    public interface IReviewRepository
    {
        Task<List<ReviewDto>> GetAllReviews();
        Task<Review> GetReviewById(int id);
        Task CreateReview(Review review);
        Task UpdateReview(int id, ReviewDto reviewDto);
        Task DeleteReview(int id);

    }
}