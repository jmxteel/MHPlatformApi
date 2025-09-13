namespace MHPlatform.Api.Common.Response
{
    public class ApiResponse<T>
    {
        public string? Status { get; set; } = string.Empty;
        public T? Data { get; set; }
        public string? Message { get; set; }
        public object? Errors { get; set; }
    }
}
