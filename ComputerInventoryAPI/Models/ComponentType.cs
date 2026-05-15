using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerInventoryAPI.Models;

[Table("ComponentTypes")]
public class ComponentType
{
    [Key]
    public int Id { get; set; }
    
    [Required, Column(TypeName = "nvarchar(30)")]
    public required string Abbreviation { get; set; }
    
    [Required, Column(TypeName = "nvarchar(150)")]
    public required string Name { get; set; }

    public virtual ICollection<Component> Components { get; set; } = new List<Component>();
}