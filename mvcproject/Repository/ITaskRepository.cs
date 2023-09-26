using mvcproject.Dto;

namespace mvcproject.Repository;

public interface ITaskRepository
{
    Task<List<TaskDto>> GetAllTasks();
    Task<Enitiy.Task> GetTasksById(int id);
    Task CreateTask(Enitiy.Task task);
    Task UpdateTask(int id, TaskDto taskDto);
    Task DeleteTask(int id);
}
