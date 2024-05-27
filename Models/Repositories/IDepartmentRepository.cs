namespace EmployeeWebAPl.Models.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetDepartment(int DepartmentId);
    }
}
