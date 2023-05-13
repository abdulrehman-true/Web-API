namespace Web_API.Models
{
    public class ServiceResponse<T>
    {
        //Wrapper class with generic, means it would get additional information with the response, tell front end if everything went right
        //or not
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}