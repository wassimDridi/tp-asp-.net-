using System.ComponentModel.DataAnnotations;

namespace EmployeeWebAPl.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(100, MinimumLength =2)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required][EmailAddress]
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public int Departmentld { get; set; }
        public string PhotoPath { get; set; }
        public Department Department { get; set; }

}
}
