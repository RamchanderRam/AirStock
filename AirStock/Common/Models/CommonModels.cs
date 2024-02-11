#nullable disable
using Humanizer;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace AirStock.Common.Models
{
    public class CommonModels
    {
    }
    public class CurrentUser
    {
        public string FullName { get; set; }
        public string LoginId { get; set; }
        public string Permissions { get; set; }
        public string Roles { get; set; }
    }
    public class TenantAdapter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PreviousName { get; set; }
        public string UrlPrefix { get; set; }
        public string Description { get; set; }
        public bool IsSystem { get; set; }
        public bool IsActive { get; set; }
        public string TimeZone { get; set; }
        public string AddressLine1 { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string FaceBook { get; set; }
        public string Fax { get; set; }
        public string FEIN { get; set; }
        public string LogoPath { get; set; }
        public string City { get; set; }
        public string DBSuffix { get; set; }
        public string CloneFrom { get; set; }
        public string CurrentUserLoginId { get; set; }
    }
    public class AppConstants
    {
        public const string HOST = "localhost";
        public const string UrlPrefix = "PLSI";
        public const string CSSClass = "primary";
    }
    public class EntityConstants
    {
        public const string APPOINTMENT = "Appointment";
        public const string INTERPRETER = "Interpreter";
        public const string FINANCIAL = "FinancialDetail";
        public const string PATIENT = "Patient";
        public const string APPOINTMENTNOTE = "AppointmentNote";
        public const string APPOINTMENTCHANGEREQUEST = "APPOINTMENTCHANGEREQUEST";
        public const string USER = "User";
        public const string CREDENTIAL = "Credential";
        public const string APPOINTMENTREMINDER = "Appointment Reminder";
        public const string APPOINTMENTSUMMARY = "Appointment Summary";
        public const string APPOINTMENTCONFIRMATION = "Appointment Confirmation";
        public const string EMERGENCYAPPOINTMENT = "Emergency Appointment";
    }
    public class EmailTemplateConstants
    {
        public const string APPOINTMENTSTATUSCHANGE = "Appointment Status Change";
        public const string REJECTEDFINANCE = "Finances Rejected";
        public const string APPROVEDFINANCE = "Financials Approved";
        public const string APPOINTMENTREMINDER = "Appointment Reminder";
        public const string APPOINTMENTSUMMARY = "Appointment Summary";
        public const string APPOINTMENTCONFIRMATION = "Appointment Confirmation";
        public const string APPOINTMENTCANCELLATION = "Appointment Cancelled";
        public const string FINANCESAPPROVED = "Finances Approved";
        public const string APPOINTMENTNOTEADDED = "Appointment Note Added";
        public const string APPOINTMENTEDITED = "Appointment Note Edited";
        public const string INTERPRETERUNAVAILABLE = "Interpreter Unavailable";
        public const string INTERPRETERCHECKEDIN = "Interpreter Checked In";
        public const string APPOINTMENTCHANGEREQUEST = "Appointment Change Request";
        public const string APPOINTMENTCANCELLATIONREQUEST = "Appointment Cancellation Request";
        public const string DOCUMENTEXPIRATION = "Document Expiration";
        public const string MFAOTPTOKEN = "MultiFactorAuthentication Token";
        public const string ForgotPassword = "Forgot Password";
        public const string NEWAPPOINTMENTNOTE = "New AppointmentNote";
        public const string APPOINTMENTNOTECHANGEREQUEST = "AppointmentNote Change Request";
        public const string APPOINTMENTNOTESTATUSCHANGE = "AppointmentNote Status Change";
    }
    public class SMSTemplateConstants
    {
        public const string APPOINTMENTSTATUSCHANGE = "Appointment Status Change";
        public const string REJECTEDFINANCE = "Finances Rejected";
        public const string APPROVEDFINANCE = "Financials Approved";
        public const string APPOINTMENTREMINDER = "Appointment Reminder";
        public const string APPOINTMENTSUMMARY = "Appointment Summary";
        public const string APPOINTMENTCONFIRMATION = "Appointment Confirmation";
        public const string APPOINTMENTCANCELLATION = "Appointment Cancelled";
        public const string FINANCESAPPROVED = "Finances Approved";
        public const string APPOINTMENTNOTEADDED = "Appointment Note Added";
        public const string APPOINTMENTEDITED = "Appointment Note Edited";
        public const string INTERPRETERUNAVAILABLE = "Interpreter Unavailable";
        public const string INTERPRETERCHECKEDIN = "Interpreter Checked In";
        public const string INTERPRETERCHECKEDOUT = "Interpreter Checked Out";
        public const string APPOINTMENTCHANGEREQUEST = "Appointment Change Request";
        public const string APPOINTMENTCANCELLATIONREQUEST = "Appointment Cancellation Request";
        public const string DOCUMENTEXPIRATION = "Document Expiration";
        public const string SMSMFAOTPTOKEN = "SMS MultiFactorAuthentication Token";
        public const string OFFERRECEIVED = "Offer Received";
        public const string OFFERDECLINED = "Offer Declined";
        public const string OFFERAVAILABLE = "Offer Available";
        public const string EMERGENCYAPPOINTMENT = "Emergency Appointment";
    }
    public class CommunicationTypeConstant
    {
        public const string SMS = "SMS";
        public const string EMAIL = "EMAIL";
        public const string Incoming = "Incoming";
        public const string Outgoing = "Outgoing";
    }
    public class ScheduleNotificationConstant
    {
        public const string DOCUMENTEXPIRATIONDAYS = "Documents - Days in Advance of Expiration to Send Notification";
        public const string DOCUMENTEXPIRATIONUSERS = "Documents - Users to Send Expiration Notifications To";
    }
    public class PaymentTypeConstants
    {
        public const string BusinessHours = "Standard";
        public const string AfterHours = "After-Hours";
        public const string AfterHoursHybrid = "Hybrid";

    }
    public class AuthenticationConstants
    {
        public const string DurationAfterMultiplefailedAttempts = "Duration for Account Lock After Multiple Failed Attempts (Minutes)";
        public const string FailedOtpAttempts = "Multi-Factor Authentication - Failed OTP Attempts to Allow";
        public const string MaximumFailedAttempts = "Maximum Login Attempts to Lock Account";
        public const string DeviceDurationDays = "Multi-Factor Authentication - Device Duration (Days)";
    }
    public class CommonApiResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public CommonApiResponse(HttpStatusCode statusCode, object result = null, string errorMessage = "Successfull", string status = "SUCCESS")
        {
            Status = status;
            Data = result;
            Message = errorMessage;
        }
    }
    public class ResponseReturnCode
    {
        public ResponseReturnCode(string data)
        {
            Data = data;
        }
        public string Data { get; set; }
        public const string SUCCESS = "SUCCESS";
        public const string INVALIDINPUT = "INVALIDINPUT";
        public const string DUPLICATEENTRY = "DUPLICATEENTRY";
        public const string DOESNOTEXIST = "DOESNOTEXIST";
        public const string INTEGRITYISSUE = "INTEGRITYISSUE";
        public const string DATABASEEXCEPTON = "DATABASEEXCEPTON";
        public const string UNKNOWNERROR = "UNKNOWNERROR";
        public const string INVALIDFILE = "INVALIDFILE";
        public const string FILEDOESNOTEXIST = "FILEDOESNOTEXIST";
        public bool IsFailureData { get { return !string.IsNullOrWhiteSpace(Data) && (Data.Equals(INVALIDINPUT) || Data.Equals(DUPLICATEENTRY) || Data.Equals(DOESNOTEXIST) || Data.Equals(INTEGRITYISSUE) || Data.Equals(DATABASEEXCEPTON) || Data.Equals(UNKNOWNERROR) || Data.Equals(INVALIDFILE) || Data.Equals(FILEDOESNOTEXIST)); } }
        public bool IsModelFailure { get { return !string.IsNullOrWhiteSpace(Data) && (!Data.Equals(SUCCESS)); } }
    }

    public class UploadAdapter
    {
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public string FilePath { get; set; }
        public string FileContent { get; set; }
        public string UploadUrl { get; set; }
        public string ConnectionString { get; set; }
    }

    public class GroupConstant
    {
        public const string Interpreter = "Interpreter";
        public const string Requester = "Requester";
        public const string MasterScheduler = "MasterScheduler";
        public const string Scheduler = "Scheduler";
        public const string Administrator = "Compliance Admin";
        public const string ApplicationManager = "ApplicationManager";
        public const string FinanceAdmin = "FinanceAdmin";
        public const string All = "All";
    }
    public class AddressCoordinates
    {
        public string FormatedAddress { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string FormatedCoordinates { get { return string.Concat(lat, ",", lng); } }

    }
    public class GeoAdapter
    {
        public string MobileCountryCode { get; set; }
        public string MobileNetWorkCode { get; set; }
        public string RadioType { get; set; }
        public string Carrier { get; set; }
        public bool ConsiderIp { get; set; }
        public string[] CellTowers { get; set; }
    }
    public class DistanceMatrixAdapter
    {
        public string Units { get; set; }
        public string Origins { get; set; }
        public string Destination { get; set; }//this destination will be pole(|) seperated
    }
    public class AppointmentStatusConstant
    {
        public const string Pending_offer = "Pending Offer";
        public const string Pending_Availability = "Pending Availability";
        public const string Offered = "Offered";
        public const string Accepted = "Accepted";
        public const string Confirm = "Confirmed";
        public const string Completed = "Completed";
        public const string New = "New";
        public const string Offer_Rejected = "Offer Rejected";
        public const string Unavailable = "Unavailable";
        public const string Urgent = "Urgent";
        public const string Returned = "Returned";
        public const string Cancelled = "Cancelled";
        public const string Pending = "Pending";

    }
    public class AppointmentColorConstant
    {
        public const string AppointmentId = "Appointment ID";
        public const string Note = "Note";
        public const string PatientName = "PatientName";
        public const string AppointmentDate = "AppointmentStartDate";

    }
    public class FinancialStatusConstant
    {
        public const string NotSubmitted = "Not Submitted";
        public const string Pending = "Pending";
        public const string Rejected = "Rejected";
        public const string Approved = "Approved";
    }
    public class CancelledStatusConstant
    {
        public const string LessThan2Hours = "Less Than 2 Hours in Advance";
        public const string Between2And24Hours = "Between 2 and 24 Hours in Advance";
        public const string MoreThan24Hours = "More Than 24 Hours in Advance";
        public const string SurgeryMoreThan2Days = "Surgery - More Than 2 Days in Advance";
        public const string SurgeryLessThan2Days = "Surgery - Less Than 2 Days and Before Surgery Scheduled";
        public const string SurgeryAfterScheduled = "Surgery - After Surgery Scheduled";

    }
    public class DropDownType
    {
        public const string Facility = "Facility";
        public const string Client = "Client";
        public const string Department = "Department";
        public const string Building = "Building";
    }
    public class ModificationRequestStatus
    {
        public const string Processed = "Processed";
        public const string Reviewed = "Reviewed";
        public const string Pending = "Pending";

    }
    public class AzureKeyValutConstant
    {
        public const string EncryptionKey = "EncryptionKey";
        public const string EncryptionVI = "EncryptionVI";
        public const string StorageKey = "StorageKey";
    }
    public class DeviceTypeConstant
    {
        public const string Phone = "Phone";
        public const string Email = "Email";
    }
    public class AutomatedResponsesConstant
    {
        public const string AppointmentNotFound = "Appointment Not Found";
        public const string NoAvailableAppointments = "No Available Appointments";
        public const string NotAssigned = "Not Assigned";
        public const string NoAppointmentstoCheckIn = "No Appointments to Check In";
        public const string NoAppointmentstoCheckOut = "No Appointments to Check Out";
        public const string UserNotFound = "User Not Found";
        public const string InvalidAppointment = "User Not Found";
        public const string InvalidTextReceived = "Invalid Text Received";
        public const string HelpText = "Help Text";
    }
    public static class AppointmentAlertConstant
    {
        public const string NOTYETSCHEDULED = "Not Yet Scheduled";
        public const string NOTYETCHECKEDIN = "Not Checked In";
        public const string RESPONSEFLAGGED = "Response/Flagged";
        public const string NOINTERPRETERS = "No Interpreters";
    }
    public class ColorAdapter
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string OldValue { get; set; }
        public bool? IsBackground { get; set; }
        public bool? IsStatus { get; set; }


    }
}