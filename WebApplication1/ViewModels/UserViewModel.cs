using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WebApplication1.Entities;

namespace WebApplication1.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Category Category { get; set; }
        public string? PhotoPath { get; set; }
        public string Email { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
