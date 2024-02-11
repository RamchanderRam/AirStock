#nullable disable
namespace AirStock.Common.Models
{
    public class ErrorResponse
    {
        public bool IsSuccess { get; set; } = false;
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
