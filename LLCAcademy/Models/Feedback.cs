using System.ComponentModel.DataAnnotations;

namespace LLCAcademy.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Feedback")]
        public string FeedbackText { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [MaxLength(12, ErrorMessage = "Your telephone number cannot be more than 12 numbers")]
        public string Telephone { get; set; }

        public bool? Approved { get; set; }
    }
}