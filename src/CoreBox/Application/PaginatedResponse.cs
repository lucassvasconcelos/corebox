namespace CoreBox.Application;

public abstract class PaginatedResponse<T>
{
    public int Count { get; set; }
    public IEnumerable<T> Items { get; set; }
}