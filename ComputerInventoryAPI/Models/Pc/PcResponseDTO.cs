namespace ComputerInventoryAPI.DTOs;

public record PcResponseDto(
    int Id, 
    string Name, 
    float Weight, 
    int Warranty,
    DateTime CreatedAt, 
    int Stock
    );