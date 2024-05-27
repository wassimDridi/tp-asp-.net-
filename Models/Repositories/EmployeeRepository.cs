using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeWebAPl.Models.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;
        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Employee>>GetEmployees()
        {
            return await _appDbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int employeeid)
        {
            return await _appDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeid);
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result =await _appDbContext.Employees.AddAsync(employee);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId==employee.EmployeeId);
            if(result != null)
            {
                result.Email = employee.Email;
                result.FirstName = employee.FirstName;
                result.LastName = employee.LastName;
                result.PhotoPath = employee.PhotoPath;
                result.Gender = employee.Gender;
                result.DateOfBirth= employee.DateOfBirth;
                result.Departmentld = employee.Departmentld;   
                await _appDbContext.SaveChangesAsync();

                return result;
            }
            return null;
        }
        public async Task<Employee> DeleteEmployee(int employeeid)
        {
            var result = await _appDbContext.Employees.FirstOrDefaultAsync(e=> e.EmployeeId == employeeid);
            if (result != null)
            {
                _appDbContext.Employees.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            return await _appDbContext.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<IEnumerable<Employee>> Search(string name ,Gender? gender)
        {
            IQueryable<Employee> query = _appDbContext.Employees;
            if (!string.IsNullOrEmpty(name)){
                query = query.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name));
            }
            if (gender != null)
            {
                query = query.Where(e => e.Gender == gender);
            }

            return await  query.ToListAsync();
        }
        


    }
}
