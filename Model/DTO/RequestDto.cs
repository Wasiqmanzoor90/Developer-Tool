

using System.ComponentModel.DataAnnotations;

namespace MyApiProject.Model.DTO;

public class RequestDto
{
    public required Guid CollectionId { get; set; }
    public required string Name { get; set; }
    public required string Method { get; set; } = "GET";
    [MaxLength(400)]
    [Url]
    public required string Url { get; set; } = string.Empty;
       
    
}