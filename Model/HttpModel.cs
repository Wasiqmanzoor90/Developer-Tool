using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class HttpModel
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string Method { get; set; } = "GET"; // GET, POST, PUT, DELETE, PATCH



            // Store as JSON string
        [Column(TypeName = "nvarchar(max)")]
    public string? Headers { get; set; } // JSON: {"Content-Type": "application/json"}
        
        [Column(TypeName = "nvarchar(max)")]
    public string? QueryParams { get; set; } // JSON: {"page": "1", "limit": "10"}
        

        [Column(TypeName = "nvarchar(max)")]
    public string? Body { get; set; }
        
         [MaxLength(20)]
        public string BodyType { get; set; } = "none"; // json, xml, form, raw, none
        
        [MaxLength(20)]
        public string AuthType { get; set; } = "none"; // none, bearer, basic, apikey
        
        [MaxLength(500)]
        public string? AuthValue { get; set; }
        
        // Foreign Key
        public Guid? CollectionId { get; set; }
        
        // [ForeignKey(nameof(CollectionId))]
        // public Collection? Collection { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
}