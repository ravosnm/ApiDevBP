
namespace ApiDevBP.Models
{
    public class ResponseResultModel
    {
        public object _data;
        public bool Failed { get; private set; }
        public string Message { get; private set; }
        public string Code { get; private set; }

        public ResponseResultModel() { }
        public static ResponseResultModel Ok(object data) => new ResponseResultModel { Failed = false, _data = data };
        public static ResponseResultModel Fail(string code, string message) => new ResponseResultModel { Failed = true, Message = message, Code = code };

        public object GetResult => Failed? new
        {
            message = Code,
            description = Message
        } : _data; 
    }
}