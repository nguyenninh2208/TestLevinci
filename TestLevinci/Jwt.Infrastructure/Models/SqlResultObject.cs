namespace Jwt.Infrastructure.Models
{
    public class SqlResultObject
    {
        private int _code { get; set; }
        private string _message { get; set; }

        public int Code { get { return _code; } set { _code = value; } }
        public string Message { get { return _message; } set { _message = value; } }

        public SqlResultObject(int code, string message)
        {
            _code = code;
            _message = message;
        }
    }

    public class NullModel
    {

    }

}
