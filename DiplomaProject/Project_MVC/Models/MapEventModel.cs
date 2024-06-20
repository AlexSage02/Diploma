using System.ComponentModel.DataAnnotations;

namespace Project_MVC.Models
{
    public class MapEventModel
    {
        [Required]
        public string? Name { get; set; }
    }
}