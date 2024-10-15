using Employees.Core.Common;

namespace Employees.Core.Entities
{
    public class Employee : EntityBase
    {
        public string PayrollNumber { get; set; }         // Personnel_Records.Payroll_Number
        public string Forenames { get; set; }             // Personnel_Records.Forenames
        public string Surname { get; set; }               // Personnel_Records.Surname
        public DateTime DateOfBirth { get; set; }         // Personnel_Records.Date_of_Birth
        public string Telephone { get; set; }             // Personnel_Records.Telephone
        public string Mobile { get; set; }                // Personnel_Records.Mobile
        public string Address { get; set; }               // Personnel_Records.Address
        public string Address2 { get; set; }              // Personnel_Records.Address_2
        public string Postcode { get; set; }              // Personnel_Records.Postcode
        public string EmailHome { get; set; }             // Personnel_Records.EMail_Home
        public DateTime StartDate { get; set; }           // Personnel_Records.Start_Date
    }
}
