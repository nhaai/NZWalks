using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SA51_CA_Project_Team10.Models.DTO
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string FileDescription { get; set; }
    }
}
