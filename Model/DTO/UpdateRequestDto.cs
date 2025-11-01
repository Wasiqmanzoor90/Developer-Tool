using System.ComponentModel.DataAnnotations;

namespace MyApiProject.Model.DTO;
public class UpdateHttpModelDto
{
    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    [Required, MaxLength(10)]
    public string Method { get; set; } = "GET";
    [Required, MaxLength(500)]
    public string Url { get; set; } = string.Empty;

    public string? Headers { get; set; }
    public string? QueryParams { get; set; }
    public string? Body { get; set; }
    public string BodyType { get; set; } = "none";
    public string AuthType { get; set; } = "none";
    public string? AuthValue { get; set; }
    public Guid? CollectionId { get; set; }
}
