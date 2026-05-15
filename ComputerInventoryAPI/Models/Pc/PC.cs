using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ComputerInventoryAPI.Models;

[Table("PCs")]
public class PC
{
    [Key]
    public int Id{get; set;}
    
    [Required, Column(TypeName = "nvarchar(50)")]
    public required string Name{get; set; }
    
    [Column(TypeName = "float(5)")]
    public float Weight{get;set;}
    
    public int Warranty{get;set;}
    
    [Column(TypeName = "datetime")]
    public DateTime CreatedAt{get;set;}
    
    public int Stock{get;set;}
    
    public virtual ICollection<PcComponent> PcComponents{get;set;} = new List<PcComponent>();
}