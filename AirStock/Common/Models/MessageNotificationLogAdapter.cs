using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace AirStock.Common.Models
{
    public class NotificationRequestAdapter : KeyFields
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string SMSCapableNumber { get; set; }
        public string FromName { get; set; }
        public string ToName { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public string Message { get; set; }
        public int ParentId { get; set; }
        public string Status { get; set; }
        public string Direction { get; set; }
        public string Subject { get; set; }
        public string CurrentUserLoginId { get; set; }
        public string Template { get; set; }
        public string FileContent { get; set; }
        public DateTime SentOn { get; set; }
        public string JsonData { get; set; }
        public List<string> MediaUrl { get; set; }
        public string FileName { get; set; }
    }
    public class KeyFields
    {
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public string CurrentUserLogin { get; set; }
    }
}
