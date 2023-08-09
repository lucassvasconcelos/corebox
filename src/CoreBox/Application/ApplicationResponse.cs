namespace CoreBox.Application;

public class ApplicationResponse<T>
{
    public int StatusCode { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    public T Data { get; set; }

    public static ApplicationResponse<T> Ok()
        => new() { StatusCode = 200 };

    public static ApplicationResponse<T> Ok(T data)
        => new() { StatusCode = 200, Data = data };

    public static ApplicationResponse<T> Created(T data)
        => new() { StatusCode = 201, Data = data };

    public static ApplicationResponse<T> Fail(int statusCode, string error)
        => statusCode == 0 || statusCode < 400
            ? throw new Exception("HttpStatusCode inválido para este tipo de resposta")
            : new() { StatusCode = statusCode, Errors = new List<string> { error } };

    public static ApplicationResponse<T> Fail(int statusCode, List<string> errors)
        => statusCode == 0 || statusCode < 400
            ? throw new Exception("HttpStatusCode inválido para este tipo de resposta")
            : new() { StatusCode = statusCode, Errors = errors };
}