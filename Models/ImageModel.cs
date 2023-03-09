using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace XenonHealth.Task.Models
{
    public class ImageModel
    {
        public string Id { get; set; }
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }
        public byte[] ImageData { get; set; }
        public DateTime CreateDate  { get; set; } 
    }

    public class ImageInputDTO
    {
        [Required]
        [DisplayName("Name")]
        public string ImageName { get; set; }
        [Required]
        [DisplayName("File")]
        public IFormFile ImageFile { get; set; }
    }
}
