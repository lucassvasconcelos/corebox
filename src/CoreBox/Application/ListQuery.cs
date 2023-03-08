namespace CoreBox.Application;

public abstract class ListQuery
{
    public int Offset { get; set; }
    public int Limit { get; set; } = 30;
    public string Term { get; set; }
    public string OrderBy { get; set; }
    public bool? Descending { get; set; }
}