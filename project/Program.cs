using TaskHub.Models;
using TaskHub.Services;
using TaskHub.Utils;
using TaskStatus = TaskHub.Models.TaskStatus;

TaskManager taskManager = new();
using FileService fileService = new();
DeadlineChecker deadlineChecker = new(taskManager);

string filePath = "Data/tasks.json";

deadlineChecker.Start();

bool isRunning = true;

while (isRunning)
{
    PrintMenu();

    Console.Write("> ");
    string? choice = Console.ReadLine();

    Console.WriteLine();

    switch (choice)
    {
        case "1":
            CreateTask();
            break;

        case "2":
            ShowAllTasks();
            break;

        case "3":
            ShowDoneTasks();
            break;

        case "4":
            ShowNotDoneTasks();
            break;

        case "5":
            ShowHighPriorityTasks();
            break;

        case "6":
            EditTask();
            break;

        case "7":
            DeleteTask();
            break;

        case "8":
            SearchTasks();
            break;

        case "9":
            StatisticsService.PrintStatistics(taskManager.GetAll());
            break;

        case "10":
            await fileService.SaveAsync(taskManager.GetAll(), filePath);
            Console.WriteLine("Tasks saved.");
            break;

        case "11":
            List<TaskItem> loadedTasks = await fileService.LoadAsync(filePath);
            taskManager.SetTasks(loadedTasks);
            Console.WriteLine("Tasks loaded.");
            break;

        case "0":
            isRunning = false;
            deadlineChecker.Stop();
            break;

        default:
            Console.WriteLine("Unknown command.");
            break;
    }

    Console.WriteLine();
}

static void PrintMenu()
{
    Console.WriteLine("=== TaskHub ===");
    Console.WriteLine("1. Create task");
    Console.WriteLine("2. Show all tasks");
    Console.WriteLine("3. Show done tasks");
    Console.WriteLine("4. Show not done tasks");
    Console.WriteLine("5. Show high priority tasks");
    Console.WriteLine("6. Edit task");
    Console.WriteLine("7. Delete task");
    Console.WriteLine("8. Search tasks");
    Console.WriteLine("9. Show statistics");
    Console.WriteLine("10. Save tasks");
    Console.WriteLine("11. Load tasks");
    Console.WriteLine("0. Exit");
}

void CreateTask()
{
    string title = ConsoleHelper.ReadString("Title: ");
    string description = ConsoleHelper.ReadString("Description: ");
    TaskPriority priority = ConsoleHelper.ReadPriority();
    DateTime deadline = ConsoleHelper.ReadDeadline();
    TaskStatus status = ConsoleHelper.ReadStatus();

    TaskItem task = new()
    {
        Title = title,
        Description = description,
        Priority = priority,
        Deadline = deadline,
        Status = status
    };

    taskManager.Add(task);

    Console.WriteLine("Task created.");
}

void ShowAllTasks()
{
    ConsoleHelper.PrintTasks(taskManager.GetAll());
}

void ShowDoneTasks()
{
    List<TaskItem> tasks = taskManager.Filter(t => t.Status == TaskStatus.Done);
    ConsoleHelper.PrintTasks(tasks);
}

void ShowNotDoneTasks()
{
    List<TaskItem> tasks = taskManager.Filter(t => t.Status != TaskStatus.Done);
    ConsoleHelper.PrintTasks(tasks);
}

void ShowHighPriorityTasks()
{
    List<TaskItem> tasks = taskManager.Filter(t => t.Priority == TaskPriority.High);
    ConsoleHelper.PrintTasks(tasks);
}

void EditTask()
{
    Guid id = ConsoleHelper.ReadGuid();
    TaskItem? task = taskManager.FindById(id);

    if (task == null)
    {
        Console.WriteLine("Task not found.");
        return;
    }

    Console.WriteLine("Leave empty if you do not want to change value.");

    string newTitle = ConsoleHelper.ReadString($"New title ({task.Title}): ");
    if (!string.IsNullOrWhiteSpace(newTitle))
    {
        task.Title = newTitle;
    }

    string newDescription = ConsoleHelper.ReadString($"New description ({task.Description}): ");
    if (!string.IsNullOrWhiteSpace(newDescription))
    {
        task.Description = newDescription;
    }

    Console.Write("Change priority? y/n: ");
    if (Console.ReadLine()?.ToLower() == "y")
    {
        task.Priority = ConsoleHelper.ReadPriority();
    }

    Console.Write("Change status? y/n: ");
    if (Console.ReadLine()?.ToLower() == "y")
    {
        task.Status = ConsoleHelper.ReadStatus();
    }

    Console.WriteLine("Task updated.");
}

void DeleteTask()
{
    Guid id = ConsoleHelper.ReadGuid();

    bool deleted = taskManager.Delete(id);

    Console.WriteLine(deleted ? "Task deleted." : "Task not found.");
}

void SearchTasks()
{
    Console.WriteLine("Search by:");
    Console.WriteLine("1. Title");
    Console.WriteLine("2. Status");
    Console.WriteLine("3. Priority");
    Console.Write("> ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            string title = ConsoleHelper.ReadString("Enter title: ");
            ConsoleHelper.PrintTasks(taskManager.SearchByTitle(title));
            break;

        case "2":
            TaskStatus status = ConsoleHelper.ReadStatus();
            ConsoleHelper.PrintTasks(taskManager.SearchByStatus(status));
            break;

        case "3":
            TaskPriority priority = ConsoleHelper.ReadPriority();
            ConsoleHelper.PrintTasks(taskManager.SearchByPriority(priority));
            break;

        default:
            Console.WriteLine("Unknown search type.");
            break;
    }
}
