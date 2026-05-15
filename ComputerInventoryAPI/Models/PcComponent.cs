using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerInventoryAPI.Models;

[Table("PCComponents")]
public class PcComponent
{
    public int PcId { get; set; }
    
    [Required,Column(TypeName = "char(10)")]
    public required string ComponentCode { get; set; }
    
    public int Amount { get; set; }

    [ForeignKey(nameof(PcId))]
    public virtual PC Pc { get; set; }
    
    [ForeignKey(nameof(ComponentCode))]
    public virtual Component Component { get; set; }
}