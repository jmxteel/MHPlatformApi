using Microsoft.Extensions.Options;

namespace MHPlatform.Api.Common.Response
{
    public class ApiResponseFactory
    {
        public static ApiResponse<T> Create<T>(T data, ApiResponseOptions options)
        {
            return new ApiResponse<T>
            {
                Status = options.IsSuccess ? "success" : "error",
                Data = options.IsSuccess ? data : default,
                Message = options.Message ?? (options.IsSuccess ? "Request successful" : "An error occurred"),
                Errors = options.IsSuccess || options.ErrorCode == null ? null
                : new ApiError
                {
                    Code = options.ErrorCode.Value,
                    Details = options.ErrorDetails
                }
            };
        }
    }
}
