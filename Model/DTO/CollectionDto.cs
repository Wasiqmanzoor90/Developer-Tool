using System.ComponentModel.DataAnnotations;
using MyApiProject.Model.Entites;

namespace MyApiProject.Model.DTOs
{
    public class CollectionDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

    }
}
