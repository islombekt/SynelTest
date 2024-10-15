using CsvHelper.Configuration.Attributes;

namespace Employees.MVC.Models.DTOs
{
    public class EmployeesDTO
    {
        [Index(0)]
        public string PayrollNumber { get; set; }         // Personnel_Records.Payroll_Number
        [Index(1)]
        public string Forenames { get; set; }             // Personnel_Records.Forenames
        [Index(2)]
        public string Surname { get; set; }               // Personnel_Records.Surname
        [Index(3)]
        public string DateOfBirth { get; set; }         // Personnel_Records.Date_of_Birth
        [Index(4)]
        public string Telephone { get; set; }             // Personnel_Records.Telephone
        [Index(5)]
        public string Mobile { get; set; }                // Personnel_Records.Mobile
        [Index(6)]
        public string Address { get; set; }               // Personnel_Records.Address
        [Index(7)]
        public string Address2 { get; set; }              // Personnel_Records.Address_2
        [Index(8)]
        public string Postcode { get; set; }              // Personnel_Records.Postcode
        [Index(9)]
        public string EmailHome { get; set; }             // Personnel_Records.EMail_Home
        [Index(10)]
        public string StartDate { get; set; }           // Personnel_Records.Start_Date
    }
}
