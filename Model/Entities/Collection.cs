
using System.ComponentModel.DataAnnotations;
namespace MyApiProject.Model.Entites;


public class Collection
{
    [Key]
    public Guid CollectionId { get; set; }

    [MaxLength(200)]
    public required string Name { get; set; }

    public List<HttpModel>? Request { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}