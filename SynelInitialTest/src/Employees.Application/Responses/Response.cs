namespace Employees.Application.Responses
{
    // using generic type to use it at any Data type
    public class Response<T>
    {
        public bool IsSuccess {  get; set; }
        public string Message { get; set; }=string.Empty;
        public T Data { get; set; }
    }
}
