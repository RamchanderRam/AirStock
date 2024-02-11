using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirStock.Common.Models
{
    public class InterpreterMatchingConstants
    {
        public const string LanguageCoefficientX2= "Language - Coefficient of x^2";
        public const string LanguageCoefficientX= "Language - Coefficient of x";
        public const string LanguageConstant= "Language - Constant";
        public const string UnavaillableInterpreter= "Points given for Unavailable Interpreter";
        public const string AvailableInterpreter= "Points given for Available Interpreter";
        public const string DistanceFacilityCoefficientX2= "Distance from Facility - Coefficient of x^2";
        public const string DistanceFacilityCoefficientX = "Distance from Facility - Coefficient of x";
        public const string DistanceFacilityConstant= "Distance from Facility - Constant";
        public const string ProfessionalismCoefficientX2= "Professionalism - Coefficient of x^2";
        public const string ProfessionalismCoefficientX= "Professionalism - Coefficient of x";
        public const string ProfessionalissConstant= "Professionalism - Constant";
        public const string TimelinessCoefficientX2= "Timeliness - Coefficient of x^2";
        public const string TimelinessCoefficientX= "Timeliness - Coefficient of x";
        public const string TimelinessConstant= "Timeliness - Constant";
        public const string SecondaryLanguageCoefficientX2 = "Secondary Language Capability - Coefficient of x^2";
        public const string SecondaryLanguageCoefficientX = "Secondary Language Capability - Coefficient of x";
        public const string SecondaryLanguageConstant = "Secondary Language Capability - Constant";
        public const string InterpreterCostCoefficientX2 = "Interpreter Cost - Coefficient of x^2";
        public const string InterpreterCostCoefficientX = "Interpreter Cost - Coefficient of x";
        public const string InterpreterCostConstant = "Interpreter Cost - Constant";
        public const string AlreadyBookedNearby = "Already booked nearby";
        public const string MilesUnder = "# of Miles under which to give preference";
        public const string MinutesUnder = "# of Minutes under which to give preference";
        public const string MatchingGender = "Matching Requested Gender";
    }
    public class GeneralSettingConstant
    {
        public const string CHECKINMINUTESBEFOREAPPOINTMENT = "Appointment - Minutes Before Appointment to Check In";
        public const string CHECKINMINUTESAFTERAPPOINTMENTSTART = "Appointment - Minutes After Start to Check In";
        public const string CHECKOUTBEFOREAPPOINTMENTEND = "Appointment - Minutes Before Appointment End to Allow Check-Out";
        public const string NOTCHECKINTIMEAFTERAPPOINTMENTSTRAT = "Appointment - Time After Start to Mark Not Checked In (Minutes)";
        public const string NOTYETSCHEDULED = "Appointment - Hours Before Appointment to Set Not Yet Scheduled";
        public const string NOTCHECKEDIN = "Appointment - Time After Start to Mark Not Checked In (Minutes)";
        public const string MINUTESTOACCEPTOFFER = "Appointment - Minutes to Accept Offer";
        public const string APPOINTMENTIDCOUNTER = "Appointment ID Counter";
        public const string APPOINTMENTDASHBOARDHISTORICALDAYS = "Appointmnet Dashboard - Historical Days to Include";
        public const string APPLICATIONURL = "Application URL";
        public const string PLSIBUSINESSHOURSEND = "PLSI Business Hours End";
        public const string PLSIBUSINESSHOURSSTART = "PLSI Business Hours Start";
        public const string EMERGENCYINTERVAL = "Emergency Interval (Hours)";
        public const string RESETPASSWORDEMAIL = "Reset Password From Email Address";
        public const string LOGINPAGECONTENT = "Login Page Content";





    }
    public class FinancialConstants
    {
        public const string DaysOfWeek = "PLSI Standard Hours - Days of the Week";
        public const string StartHour = "PLSI Standard Hours - Start";
        public const string EndHour = "PLSI Standard Hours - End";
        public const string AppointmentCancellationClientWithin24HoursCharge = "Appointment Cancellation - Minimum Hours to Charge Clients (Within 24 Hours of Appt)";
        public const string AppointmentCancellationInterpreterMin2HoursCharge = "Appointment Cancellation - Minimum Hours to Pay Interpreters (Within 2 Hours of Appt)";
        public const string SurgeryAppointmentClientChargeForCancellation = "Appointment Cancellation - Minimum Hours to Charge Clients (Surgery Appointments Cancelled Before Surgery Time Known)";
        public const string MinInterpreterPayForAppointmentCancellation = "Appointment Cancellation - Minimum Amount to Pay Interpreters (Between 2 and 24 Hours of Appt)";
        public const string MinAppointmentInterpreterPay = "Appointments - Minimum Hours for Interpreter Pay";
        public const string SurgeryAppointmentInterpreterPayForCancellation = "Appointment Cancellation - Minimum Hours to Pay Interpreters (Surgery Appointments Cancelled Before Surgery Time Known)";
        public const string MinAppointmentClientBilling = "Appointments - Minimum Hours for Client Billing";
        public const string AppointmentGap = "Appointments - Maximum Gap Between Appointments to Treat as Single Appt (Minutes)";

    }
    public class ColorSettingConstants
    {
        public const string NewstatusTextColor = "New Status Text Color";
    }
}
