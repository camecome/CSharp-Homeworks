using TaskHub.Models;
using TaskStatus = TaskHub.Models.TaskStatus;

namespace TaskHub.Services;

public static class StatisticsService
{
    public static void PrintStatistics(List<TaskItem> tasks)
    {
        int total = tasks.Count;
        int done = tasks.Count(t => t.Status == TaskStatus.Done);
        int overdue = tasks.Count(t => t.IsOverdue());

        Dictionary<TaskPriority, int> priorityStats = tasks
            .GroupBy(t => t.Priority)
            .ToDictionary(g => g.Key, g => g.Count());

        Console.WriteLine();
        Console.WriteLine("=== Statistics ===");
        Console.WriteLine($"Total tasks: {total}");
        Console.WriteLine($"Done tasks: {done}");
        Console.WriteLine($"Overdue tasks: {overdue}");

        Console.WriteLine();
        Console.WriteLine("By priority:");

        foreach (TaskPriority priority in Enum.GetValues<TaskPriority>())
        {
            priorityStats.TryGetValue(priority, out int count);
            Console.WriteLine($"{priority}: {count}");
        }
    }
}
