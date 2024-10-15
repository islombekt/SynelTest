using System.ComponentModel.DataAnnotations;

namespace Employees.MVC.Models.Entities
{
    public class EntityBase
    {
        public int EmployeeId { get; set; }
        public DateTime CreatedAt {  get; set; }
        public string CreatedBy {  get; set; }
        public DateTime UpdatedAt {  get; set; }
        public string UpdatedBy { get; set;} 
    }
}
