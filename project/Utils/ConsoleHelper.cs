using TaskHub.Models;
using TaskStatus = TaskHub.Models.TaskStatus;

namespace TaskHub.Utils;

public static class ConsoleHelper
{
    public static string ReadString(string message)
    {
        Console.Write(message);
        return Console.ReadLine() ?? string.Empty;
    }

    public static TaskPriority ReadPriority()
    {
        while (true)
        {
            Console.WriteLine("Choose priority:");
            Console.WriteLine("1. Low");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. High");
            Console.Write("> ");

            if (int.TryParse(Console.ReadLine(), out int value)
                && Enum.IsDefined(typeof(TaskPriority), value))
            {
                return (TaskPriority)value;
            }

            Console.WriteLine("Invalid priority.");
        }
    }

    public static TaskStatus ReadStatus()
    {
        while (true)
        {
            Console.WriteLine("Choose status:");
            Console.WriteLine("1. New");
            Console.WriteLine("2. InProgress");
            Console.WriteLine("3. Done");
            Console.Write("> ");

            if (int.TryParse(Console.ReadLine(), out int value)
                && Enum.IsDefined(typeof(TaskStatus), value))
            {
                return (TaskStatus)value;
            }

            Console.WriteLine("Invalid status.");
        }
    }

    public static DateTime ReadDeadline()
    {
        while (true)
        {
            Console.Write("Enter deadline, for example 2026-05-30 18:00: ");

            if (DateTime.TryParse(Console.ReadLine(), out DateTime deadline))
            {
                return deadline;
            }

            Console.WriteLine("Invalid date.");
        }
    }

    public static Guid ReadGuid()
    {
        while (true)
        {
            Console.Write("Enter task ID: ");

            if (Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                return id;
            }

            Console.WriteLine("Invalid ID.");
        }
    }

    public static void PrintTasks(List<TaskItem> tasks)
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        foreach (TaskItem task in tasks)
        {
            Console.WriteLine();
            Console.WriteLine(task);
            Console.WriteLine("----------------------");
        }
    }
}
