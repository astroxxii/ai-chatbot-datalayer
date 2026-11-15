using System.Text.Json.Serialization;

namespace TugasUtils.HttpUtils.Model
{
    /// <summary>
    /// Standard generic API response wrapper for service and HTTP responses.
    /// </summary>
    /// <typeparam name="T">Type of the payload data</typeparam>
    public class ApiResponse<T>
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public T? Data { get; set; }

        [JsonPropertyName("error")]
        public object? Error { get; set; }

        public ApiResponse() { }

        public ApiResponse(bool success, string message, T? data = default, object? error = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Error = error;
        }

        /// <summary>
        /// Creates a successful response with payload data.
        /// </summary>
        /// <param name="data">Response payload data</param>
        /// <param name="message">Optional success message</param>
        public static ApiResponse<T> Ok(T data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                Error = null
            };
        }

        /// <summary>
        /// Creates a failure response with error details.
        /// </summary>
        /// <param name="errorDetail">Detailed error payload or message</param>
        /// <param name="message">Failure message</param>
        public static ApiResponse<T> Fail(object? errorDetail = null, string message = "Failed")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                Error = errorDetail
            };
        }
    }
}
