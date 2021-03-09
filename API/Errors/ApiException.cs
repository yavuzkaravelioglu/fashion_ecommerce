namespace API.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string message = null, string details = null ) 
            : base(statusCode, message)
        {
            Details = details;
        }
        public string Details { get; set; } // this is gonna contain stack tree error.

    }
}