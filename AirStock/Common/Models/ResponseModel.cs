#nullable disable

namespace AirStock.Common.Models
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; } = true;
        public int StatusCode { get; set; }
        public object Data { get; set; }
    }
    public class AdhocResponse
    {
        public string message { get; set; }
    }
    public enum ReturnCode
    {
        SUCCESS = 0,
        INVALIDINPUT = 51,
        DUPLICATEENTRY = 52,
        DOESNOTEXIST = 53,
        INTEGRITYISSUE = 54,
        UNAUTHORIZEDREQUESTER = 55,
        LOGINFAILED = 56,
        SIGNATUREDOESNOTEXIST = 57,
        DATABASEEXCEPTON = 58,
        UNKNOWNERROR = 59,
        ENGINERUNNING = 73,
        ENGINESTOPPED = 74,
        NOTELIGIBLE = 71,
        DOESNOTMATCH = 72,
        TWITTERERROR = 72,
        LINKEDINERROR = 73,


    }
 
}
