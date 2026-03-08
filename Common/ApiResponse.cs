namespace PortfolioManager.Api.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public T? Data { get; set; }

        public List<string>? Errors { get; set; }

        public static ApiResponse<T> Ok(T data)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data
            };
        }

        public static ApiResponse<T> Fail(List<string> errors)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Errors = errors
            };
        }
    }
}
