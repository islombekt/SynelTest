namespace Employees.MVC.Models.DTOs
{
    public class CreatingEmployees
    {
        public int TotalItems {  get; set; } // total items in csv file
        public int Q_Added { get; set; } // quantity of added items from csv file
        public int Q_NotAdd {  get; set; } // how much records could not be added from csv file
        public List<EmployeesDTO> Employees { get; set; } = new List<EmployeesDTO>();
        public string? Errors {  get; set; }
    }
}
