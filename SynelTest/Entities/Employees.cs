using System.ComponentModel.DataAnnotations;

namespace SynelTest.Entities
{
    public class Employees
    {
        [Key]
        public int EmployeeId { get; set; }
        public string PayrollNumber { get; set; }
        public string Forenames { get; set; }
        public string Surname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Address_2 { get; set; }
        public string Postcode { get; set; }
        public string EMail_Home { get; set; }
        public DateOnly Start_Date { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
