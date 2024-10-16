namespace Employees.Application.Responses
{
    public class CreatingEmployees
    {
        public int TotalItems {  get; set; } // total items in csv file
        public int Q_Added { get; set; } // quantity of added items from csv file
        public int Q_Failure {  get; set; } // how much records could not be added from csv file
        public List<EmployeesDTO> Employees { get; set; } = new List<EmployeesDTO>();
        public List<string>? Errors {  get; set; }
    }
}
