using mvcproject.Dto;
using mvcproject.Enitiy;
using Task = System.Threading.Tasks.Task;

namespace mvcproject.Repository;

public interface IContactRepository
{
    Task<List<ContactDto>> GetAllContact();
    Task<Contact> GetContactByIdAsync(int id);
    Task CreateContactAsync(Contact contact);
    Task DeleteContactAsync(int id);
}
