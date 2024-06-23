using System.ComponentModel.DataAnnotations;
using WebApplication1.Entities.Enums;

namespace WebApplication1.ViewModels
{
    public class ContactEditViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }


        [EmailAddress]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        public bool UnDeleteAble { get; set; }
        public PriorityType PriorityType { get; set; }

        [Range(1000000000, 10000000000)]
        [Required]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "Category")]
        [RegularExpression("^\\d+$", ErrorMessage = "Please select a category")]
        public int? CategoryId { get; set; }
        //public virtual Category? Category { get; set; }
        public string? PhotoPath { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime? BirthDate { get; set; }
        public IFormFile? UploadFile { get; set; }
    }
}
