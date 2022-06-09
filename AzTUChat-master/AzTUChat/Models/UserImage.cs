using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AzTUChat.Models
{
    public class UserImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
