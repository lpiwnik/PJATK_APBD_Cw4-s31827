namespace ComputerInventoryAPI.DTOs;
using System.ComponentModel.DataAnnotations;

public class PcCreateRequestDto
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
    public string Name { get; set; } = null!;

    [Range(0.1, 100.0, ErrorMessage = "Weight cannot be less than {1} and more than {2}")]
    public float Weight { get; set; }

    [Range(0, 120, ErrorMessage = "Warranty has to be between {1} and {2} month ")]
    public int Warranty { get; set; }

    [Required] public DateTime CreatedAt { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
    public int Stock { get; set; }
}