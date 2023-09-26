using Microsoft.EntityFrameworkCore;
using mvcproject.Data;
using mvcproject.Dto;
using mvcproject.Enitiy;
using Task = System.Threading.Tasks.Task;

namespace mvcproject.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context) => _context = context;

        public async Task CreateTask(Enitiy.Task task)
        {
            _context.Task.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTask(int id)
        {
            var cur = await _context.Task.FindAsync(id);
            if (cur != null)
            {
                _context.Task.Remove(cur);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<TaskDto>> GetAllTasks()
        {
            var TaskAll = await _context.Task
          .Include(e => e.Lessons)
          .Select(e => new TaskDto()
          {
              Id = e.Id,
              Title = e.Title,
              Lessons = e.Lessons,
              complete = e.complete,
              Data = e.Data,
              Description = e.Description,
          })
          .ToListAsync();
            return TaskAll;
        }

        public async Task<Enitiy.Task> GetTasksById(int id)
        {
            var taskDto = await _context.Task
            .Include(e => e.Lessons)
             .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
            var task = new TaskDto();
            task.Id = id;
            task.Description = taskDto.Description;
            task.Lessons = taskDto.Lessons;
            task.Title = taskDto.Title;
            task.Data = taskDto.Data;
            task.complete = taskDto.complete;

            return taskDto;
        }

        public async Task UpdateTask(int id, TaskDto taskDto)
        {
            var Cur = await _context.Task.FirstOrDefaultAsync(i => i.Id == id);
            if (Cur != null)
            {
                Cur.Title = taskDto.Title;
                Cur.Data = taskDto.Data;
                Cur.Description = taskDto.Description;
                Cur.Lessons = taskDto.Lessons;
                Cur.complete = taskDto.complete;

                _context.Entry(Cur).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}
