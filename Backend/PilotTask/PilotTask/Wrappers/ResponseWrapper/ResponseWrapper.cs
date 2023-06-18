namespace PilotTask.Wrappers.ResponseWrapper
{
    public class ResponseWrapper<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Payload { get; set; } = default;

        public static ResponseWrapper<T> Fail(string message)
        {
            return new ResponseWrapper<T>
            {
                Succeeded = false,
                Message = message
            };
        }

        public static ResponseWrapper<T> Success(string message, T payload)
        {
            return new ResponseWrapper<T>
            {
                Succeeded = true,
                Message = message,
                Payload = payload
            };
        }
    }
}
