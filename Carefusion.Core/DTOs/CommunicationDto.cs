using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carefusion.Core.DTOs;

public class CommunicationDto
{
    [Key]
    [JsonIgnore]
    public int Identifier { get; init; }
    [StringLength(50)]
    [DefaultValue("self")]
    public string? Contact { get; init; }
    [StringLength(15)]
    [DefaultValue(null)]
    public string? PhoneNumber { get; init; }
    [StringLength(100)]
    [DefaultValue(null)]
    public string? Email { get; init; }
    [Required]
    [StringLength(50)]
    [DefaultValue("tr")]
    public required string Language { get; init; }
    [Required]
    [DefaultValue(false)]
    public required bool NeedDisabilityAssistance { get; init; }
    [StringLength(255)]
    [DefaultValue(null)]
    public string? Note { get; init; }
    public int DebugCommunicationId => Identifier;
}