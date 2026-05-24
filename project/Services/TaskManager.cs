using TaskHub.Models;
using TaskStatus = TaskHub.Models.TaskStatus;

namespace TaskHub.Services;

public class TaskManager
{
    private readonly List<TaskItem> _tasks = new();

    public delegate bool TaskFilter(TaskItem task);

    public List<TaskItem> GetAll()
    {
        return _tasks;
    }

    public void SetTasks(List<TaskItem> tasks)
    {
        _tasks.Clear();
        _tasks.AddRange(tasks);
    }

    public void Add(TaskItem task)
    {
        _tasks.Add(task);
    }

    public bool Delete(Guid id)
    {
        TaskItem? task = _tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            return false;
        }

        _tasks.Remove(task);
        return true;
    }

    public TaskItem? FindById(Guid id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    public List<TaskItem> Filter(TaskFilter filter)
    {
        return _tasks.Where(t => filter(t)).ToList();
    }

    public List<TaskItem> SearchByTitle(string title)
    {
        return _tasks
            .Where(t => t.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<TaskItem> SearchByStatus(TaskStatus status)
    {
        return _tasks.Where(t => t.Status == status).ToList();
    }

    public List<TaskItem> SearchByPriority(TaskPriority priority)
    {
        return _tasks.Where(t => t.Priority == priority).ToList();
    }
}
