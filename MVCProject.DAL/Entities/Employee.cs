using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.DAL.Entities
{
   public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is Requred")]
        [MaxLength(50,ErrorMessage ="Max lenrht 50 char")]
        [MinLength(5, ErrorMessage = "Max lenrht 5 char")]

        public string Name { get; set; }
        [Range(22,35,ErrorMessage ="Age must be from 22 to 35")]

        public int? Age { get; set; }
        [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{4,20}-[a-zA-Z]{4,20}$",ErrorMessage ="Address must be like 123-Street-country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActiv {  get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }
        public DateTime CreateionDate { get; set; } = DateTime.Now;

        [ForeignKey("Department")]
        public int? dept_Id { get; set; }
        [InverseProperty("Employees")]
        public Department Department { get; set; }

    }
}
