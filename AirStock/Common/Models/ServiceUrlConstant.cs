using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirStock.Common.Models
{
    public class ServiceUrlConstant
    {
        public const string Template = "TemplateServiceUrl";
        public const string Communication = "CommunicationServiceUrl";
        public const string Identity = "IdentityServiceUrl";
        public const string User = "UserServiceUrl"; 
        public const string Recipient = "RecipientServiceUrl";
        public const string MessageLog = "MessageLogServiceUrl";

    }
    public enum DispatchType
    {
        Text=1,
        Voice=2,
        Recipient=3,
    }
    public class StatusConstant
    {
        public const string Delivered = "delivered";
        public const string Partial = "partial";
        public const string UnDelivered = "undelivered";
        public const string Completed = "completed";


    }
}
