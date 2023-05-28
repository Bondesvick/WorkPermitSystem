using System.ComponentModel.DataAnnotations;

namespace WorkPermitSystem.Models.ViewModels
{
    public class CreatePermitViewModel
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "File is required.")]
        public IFormFile File { get; set; }
    }
}