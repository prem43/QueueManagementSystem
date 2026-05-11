namespace QueueManagement.Domain.Enums;

public enum TokenStatus
{
    Created = 1,
    Waiting = 2,
    Called = 3,
    Serving = 4,
    Completed = 5,
    Skipped = 6,
    Transferred = 7,
    Cancelled = 8,
    ReOpened = 9
}