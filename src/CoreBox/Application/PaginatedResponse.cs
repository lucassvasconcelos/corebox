namespace CoreBox.Application;

public class PaginatedResponse<T>
{
    public int Count { get; set; }
    public IEnumerable<T> Items { get; set; }

    public static PaginatedResponse<T> Create()
        => Activator.CreateInstance<PaginatedResponse<T>>();
}