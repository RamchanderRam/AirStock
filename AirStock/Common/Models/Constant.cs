using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirStock.Common.Models
{
    public static class Constant
    {
        public const string DEFAULT_SHEET_NAME = "Sheet1";
        public const string DEFAULT_FILE_DATETIME = "yyyyMMdd_HHmm";
        public const string DATETIME_FORMAT = "dd/MM/yyyy hh:mm:ss";
        public const string EXCEL_MEDIA_TYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public const string DISPOSITION_TYPE_ATTACHMENT = "attachment";
        public const string STRING = "string";
        public const string INT32 = "int32";
        public const string DOUBLE = "double";
        public const string DATETIME = "datetime";

    }

    public static class PricingTypeConstant
    {
        public const string TieredMileage = "Tiered Mileage";
        public const string FlatRate = "Flat Rate";
        public const string MileageTravelTime = "Mileage and/or TravelTime";
    }
    public static class InterpreterHourConstant
    {
        public const string ExtraPay = "Extra Pay";
        public const string TimeAndaHalfPay = "Time-and-a-Half Pay";
    }
    public static class ApplicationConstants
    {
        public const string DefaultTenant = "PLSI";
        public const string System = "SYSTEM";

    }
    public static class AppointmentNoteStatusConstant
    {
        public const string NotYetProcessed = "Not Yet Processed";
        public const string Processed = "Processed";
    }
}