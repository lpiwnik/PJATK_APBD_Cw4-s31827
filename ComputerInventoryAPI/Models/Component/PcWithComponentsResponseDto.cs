namespace ComputerInventoryAPI.DTOs;



public record PcWithComponentsResponseDto(
    int Id, 
    string Name, 
    float Weight, 
    int Warranty, 
    DateTime CreatedAt, 
    int Stock, 
    List<PcComponentDetailDto> Components
);

public record PcComponentDetailDto(
    int Amount, 
    ComponentDetailDto Component
);

public record ComponentDetailDto (
    string Code, 
    string Name, 
    string? Description, 
    ManufacturerDto Manufacturer, 
    TypeDto Type
);

public record ManufacturerDto
    (int Id,string Abbreviation, string FullName, string FoundationDate);

public record TypeDto
    (int Id, string Abbreviation, string Name);