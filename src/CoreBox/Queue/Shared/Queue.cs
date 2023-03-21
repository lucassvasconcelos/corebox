namespace CoreBox.Queue.Shared;

public class Queue
{
    public string Name { get; set; }
    public bool Durable { get; set; }
    public bool AutoDelete { get; set; }
    public bool Exclusive { get; set; }
    public IDictionary<string, object> Arguments { get; set; }
}