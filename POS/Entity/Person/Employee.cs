using System.ComponentModel.DataAnnotations;

namespace POS.Entity.Person
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public User? User { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
