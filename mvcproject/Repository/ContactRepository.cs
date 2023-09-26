using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;
using Task = System.Threading.Tasks.Task;

namespace mvcproject.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;
        public ContactRepository(AppDbContext context) => _context = context;


        public async Task<List<ContactDto>> GetAllContact()
        {
            var les = await _context.Contact
                .Select(i => new ContactDto()
                {
                    Email = i.Email,
                    Location = i.Location,
                    Id = i.Id,
                    PhoneNumber = i.PhoneNumber,
                })
                .ToListAsync();
            return les;
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            var leso = await _context.Contact
                .FirstOrDefaultAsync(i => i.Id == id) ?? throw new BadHttpRequestException("Not Found");
            var lesson = new ContactDto();
            lesson.Id = id;
            lesson.Email = leso.Email;
            lesson.Location = leso.Location;
            lesson.PhoneNumber = leso.PhoneNumber;
            return leso;
        }
        public async System.Threading.Tasks.Task CreateContactAsync(Contact contact)
        {
            await _context.Contact.AddAsync(contact);
            await _context.SaveChangesAsync();

        }
        public async Task DeleteContactAsync(int id)
        {
            var CurContact = await _context.Contact.FindAsync(id);
            if (CurContact != null)
            {
                _context.Contact.Remove(CurContact);
                await _context.SaveChangesAsync();
            }
        }

    }

}