using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirStock.Common.Models;
#nullable disable
namespace AirStock.Common.Models
{
    public static class TimeZoneAbbrModel
    {
        
        public  static string GetDisplayName(string abbr)
        {

            List<TimeZoneModel>?  zoneData =JsonConvert.DeserializeObject<List<TimeZoneModel>>(TimeZoneData.timeZoneData);
            var displayName = zoneData?.Where(x => x.abbr == abbr).Select(s => s.value).FirstOrDefault();
            if (string.IsNullOrEmpty(displayName)) { return abbr; }
            else return displayName;

        }
        public static string GetDisplayAbbr(string name)
        {

            List<TimeZoneModel>? zoneData = JsonConvert.DeserializeObject<List<TimeZoneModel>>(TimeZoneData.timeZoneData);
            var abbr = zoneData?.Where(x => x.value == name).Select(s => s.abbr).FirstOrDefault();
            if (string.IsNullOrEmpty(abbr)) { return name; }
            else return abbr;

        }
    }
    public class TZModel
    {
        public List<TimeZoneModel> TimeZones { get; set; }
    }
    public class TimeZoneModel
        {
            public string value { get; set; }
            public string abbr { get; set; }
            public float offset { get; set; }
            public bool isdst { get; set; }
            public string text { get; set; }
            public List<string> utc { get; set; }
        }
}
