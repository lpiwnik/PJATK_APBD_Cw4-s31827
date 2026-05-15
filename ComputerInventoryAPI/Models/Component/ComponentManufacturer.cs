using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerInventoryAPI.Models;

[Table("ComponentManufacturers")]
public class ComponentManufacturer
{
    [Key]
    public int Id { get; set; }
    
    [Required, Column(TypeName = "nvarchar(30)")]
    public required string Abbreviation { get; set; }
    
    [Required, Column(TypeName = "nvarchar(300)")]
    public required string FullName { get; set; }
    
    [Column(TypeName = "date")]
    public DateTime FoundationDate { get; set; }

    public virtual ICollection<Component> Components { get; set; } = new List<Component>();
}