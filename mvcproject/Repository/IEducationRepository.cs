using mvcproject.Dto;
using mvcproject.Enitiy;

namespace mvcproject.Repository;

public interface IEducationRepository
{

    Task<List<EducationDto>> GetAllEducation();
    Task<Education> GetEducationById(int id);
    System.Threading.Tasks.Task CreateEducation(Education education);
    System.Threading.Tasks.Task UpdateEducation(int id, EducationDto educationDto);
    System.Threading.Tasks.Task DeleteEducationById(int id);
}
