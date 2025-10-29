
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiProject.Model.Entites;

public class RequestHistory
{
    [Key]
    public Guid RequestHistoryId { get; set; }

    public Guid HttpModelId { get; set; }

    [ForeignKey("HttpModelId")]
    public HttpModel? HttpModel { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? ResponseBody { get; set; }

    public int? StatusCode { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? ResponseHeaders { get; set; }


    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
}
