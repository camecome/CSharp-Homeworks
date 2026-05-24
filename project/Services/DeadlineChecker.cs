using TaskHub.Models;
using TaskStatus = TaskHub.Models.TaskStatus;

namespace TaskHub.Services;

public class DeadlineChecker
{
    private readonly TaskManager _taskManager;
    private readonly CancellationTokenSource _cts = new();

    public DeadlineChecker(TaskManager taskManager)
    {
        _taskManager = taskManager;
    }

    public void Start()
    {
        Task.Run(async () =>
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                List<TaskItem> overdueTasks = _taskManager
                    .GetAll()
                    .Where(t => t.Status != TaskStatus.Done && t.Deadline < DateTime.Now)
                    .ToList();

                foreach (TaskItem task in overdueTasks)
                {
                    Console.WriteLine();
                    Console.WriteLine($"[WARNING] Task is overdue: {task.Title}");
                    Console.WriteLine();
                }

                await Task.Delay(5000, _cts.Token);
            }
        });
    }

    public void Stop()
    {
        _cts.Cancel();
    }
}
