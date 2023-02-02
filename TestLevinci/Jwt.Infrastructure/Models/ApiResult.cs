using System.Threading.Tasks;

namespace Jwt.Infrastructure.Models
{
    public class ApiResult<T>
    {
        private ApiResultCode _code { get; set; }
        public ApiResultCode Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResult()
        {
            _code = ApiResultCode.Ok;
        }

        public ApiResult(ApiResultCode code, string message, T data)
        {
            _code = code;
            Message = message;
            Data = data;
        }
    }

    public class NullDataObject
    {

    }

    public static class ApiResult
    {
        public static ApiResult<NullDataObject> Error(string message, string exMessage,
           ApiResultCode code = ApiResultCode.Error)
        {
            return new ApiResult<NullDataObject>(code, exMessage, default);
        }
        public static Task<ApiResult<NullDataObject>> OkAsync()
        {
            return Task.FromResult(new ApiResult<NullDataObject>(ApiResultCode.Ok, "Thành công", default));
        }
        public static Task<ApiResult<T>> OkAsync<T>(T data, string message = "Thành công")
        {
            return Task.FromResult(new ApiResult<T>(ApiResultCode.Ok, message, data));
        }

        public static Task<ApiResult<NullDataObject>> FailAsync(string message = "Đã xảy ra lỗi trong quá trình xử lý.")
        {
            return Task.FromResult(new ApiResult<NullDataObject>(ApiResultCode.Fail, message: message, default));
        }
        public static Task<ApiResult<T>> FailAsync<T>(string message = "Đã xảy ra lỗi trong quá trình xử lý.")
        {
            return Task.FromResult(new ApiResult<T>(ApiResultCode.Fail, message: message, default));
        }

        public static Task<ApiResult<T>> UnAuthorizeAsync<T>(string message = "Unauthorize.")
        {
            return Task.FromResult(new ApiResult<T>(ApiResultCode.Unauthorize, message: message, default));
        }
    }

    public enum ApiResultCode
    {
        Ok = 200,
        Fail = -200,
        Unauthorize = 401,
        Error = -2000
    }
}
