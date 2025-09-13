namespace MHPlatform.Api.Common.Response
{
    public class ApiResponseOptions
    {
        public string? Message { get; set; }
        public bool IsSuccess { get; set; } = true;
        public int? ErrorCode { get; set; }
        public string? ErrorDetails { get; set; }
    }
}
