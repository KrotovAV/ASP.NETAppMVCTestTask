using WebApplication1.Entities.Enums;
using WebApplication1.Entities;
using CURDOperationWithImageUploadCore5_Demo.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool UnDeleteAble { get; set; } = false;
        public PriorityType? PriorityType { get; set; }
        [Required]
        public string Mobile { get; set; }
        //public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public string? PhotoPath { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime? BirthDate { get; set; }
    }
}
