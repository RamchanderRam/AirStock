using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace AirStock.Common.Models
{
    public class ConfigurationModel
    {
        public string HostName { get; set; }
        public List<KeyValueModel> DbConnections { get; set; }
        public List<KeyValueModel> ServiceUrls { get; set; }
        public List<KeyValueModel> JobUrls { get; set; }

        public TwilioModel Twilio { get; set; }
        public SendGridModel SendGrid { get; set; }
        public SmtpExchangeModel SMTPExchange { get; set; }
        public string AzureTableConnection { get; set; }
    }

    public class TwilioModel
    {
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
        public string TwilioNumber { get; set; }
        public string TwilioWhatsappNumber { get; set; }
    }
    public class SendGridModel
    {
        public string ApiKey { get; set; }
        public string From { get; set; } 
    }
    public class SmtpExchangeModel
    {
        public string Url { get; set; }
        public string From { get; set; }
        public string Key { get; set; }
        public string Domain { get; set; }
    }
    public class KeyValueModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class ConnectionConstants
    {
        public const string UserDb = "UserConnection";
        public const string IdentityDb = "IdentityConnection";
        public const string TemplateDb = "TemplateConnection";
        public const string CommunicationDb = "CommunicationConnection";
    }
}
