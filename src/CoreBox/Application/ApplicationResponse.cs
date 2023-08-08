namespace CoreBox.Application;

public class ApplicationResponse<T>
{
    public int StatusCode { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    public T Data { get; set; }

    public static ApplicationResponse<T> Success(int statusCode, T data)
        => statusCode == 0 || statusCode >= 400
            ? throw new Exception("HttpStatusCode inválido para este tipo de resposta")
            : new() { StatusCode = statusCode, Data = data };

    public static ApplicationResponse<T> Fail(int statusCode, string error)
        => statusCode == 0 || statusCode < 400
            ? throw new Exception("HttpStatusCode inválido para este tipo de resposta")
            : new() { StatusCode = statusCode, Errors = new List<string> { error } };

    public static ApplicationResponse<T> FailWithErrors(int statusCode, List<string> errors)
        => statusCode == 0 || statusCode < 400
            ? throw new Exception("HttpStatusCode inválido para este tipo de resposta")
            : new() { StatusCode = statusCode, Errors = errors };
}