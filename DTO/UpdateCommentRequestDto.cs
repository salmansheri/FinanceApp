using System.ComponentModel.DataAnnotations;

namespace Backend.DTO
{
    public class UpdateCommentRequestDto
    {
         [Required]
        [MinLength(5,ErrorMessage = "Title must be at least 5 characters")]
        [MaxLength(280, ErrorMessage  = "Title must be less than 280 characters")]
        public string Title { get; set; } = string.Empty; 
        
         [Required]
        [MinLength(5,ErrorMessage = "Title must be at least 5 characters")]
        [MaxLength(280, ErrorMessage  = "Title must be less than 280 characters")]
        public string Content { get; set; } = string.Empty; 
    }
}