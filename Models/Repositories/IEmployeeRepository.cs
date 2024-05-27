namespace EmployeeWebAPl.Models.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int employeeid);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee> DeleteEmployee(int employeeid);
        Task<Employee> GetEmployeeByEmail(string email);

        Task<IEnumerable<Employee>> Search(String name ,Gender? gender);
    }
}
