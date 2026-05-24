namespace TaskHub.Models;

public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public TaskPriority Priority { get; set; }

    public DateTime Deadline { get; set; }

    public TaskStatus Status { get; set; }

    public bool IsOverdue()
    {
        return Status != TaskStatus.Done && Deadline < DateTime.Now;
    }

    public override string ToString()
    {
        return $"""
        ID: {Id}
        Title: {Title}
        Description: {Description}
        Priority: {Priority}
        Deadline: {Deadline:yyyy-MM-dd HH:mm}
        Status: {Status}
        Overdue: {(IsOverdue() ? "Yes" : "No")}
        """;
    }
}
