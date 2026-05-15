using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerInventoryAPI.Models;

[Table("Components")]
public class Component
{
    [Key,Column("Code", TypeName = "char(10)")]
    public required string Code { get; set; }
    
    [Required, Column(TypeName = "nvarchar(300)")]
    public required string Name { get; set; }
    
    [Column(TypeName = "nvarchar(max)")]
    public string? Description { get; set; }
    
    public int ComponentManufacturersId { get; set; }
    public int ComponentTypesId { get; set; }

    [ForeignKey(nameof(ComponentManufacturersId))]
    public virtual ComponentManufacturer Manufacturer { get; set; }
    
    [ForeignKey(nameof(ComponentTypesId))]
    public virtual  ComponentType Type { get; set; }
    
    public virtual ICollection<PcComponent> PcComponents { get; set; } = new List<PcComponent>();
}