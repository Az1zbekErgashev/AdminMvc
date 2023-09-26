using mvcproject.Dto;
using mvcproject.Enitiy;

namespace mvcproject.Repository;

public interface IResultRepository
{
    Task<List<ResultDto>> GetAllResults();
    Task<Result> GetResultById(int id);
    System.Threading.Tasks.Task CreateResult(Result result);
    System.Threading.Tasks.Task UpdateResult(int id, ResultDto resultDto);
    System.Threading.Tasks.Task DeleteResultById(int id);

    Task<List<ResultDto>> GetUserResult(int id);
}
