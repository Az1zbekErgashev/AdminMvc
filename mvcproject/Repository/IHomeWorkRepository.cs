using mvcproject.Dto;
using mvcproject.Enitiy;
using Task = System.Threading.Tasks.Task;

namespace mvcproject.Repository
{
    public interface IHomeWorkRepository
    {
        Task<List<HomeWorkDto>> GetAllHomeWork();
        Task<HomeWork> GetHomeWorkById(int id);
        Task CreateHome(HomeWork homeWork);
        Task UpdateHomeWork(int id, HomeWorkDto homeWorkDto);
        Task DeleteHomeWorkById(int id);
    }
}